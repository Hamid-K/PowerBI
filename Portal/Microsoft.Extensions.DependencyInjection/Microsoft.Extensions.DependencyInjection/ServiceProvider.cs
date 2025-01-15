using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.ServiceLookup;

namespace Microsoft.Extensions.DependencyInjection
{
	// Token: 0x02000006 RID: 6
	internal class ServiceProvider : IServiceProvider, IDisposable
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000216E File Offset: 0x0000036E
		internal ServiceProvider Root { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002176 File Offset: 0x00000376
		internal Dictionary<object, object> ResolvedServices { get; } = new Dictionary<object, object>();

		// Token: 0x06000018 RID: 24 RVA: 0x00002180 File Offset: 0x00000380
		public ServiceProvider(IEnumerable<ServiceDescriptor> serviceDescriptors, bool validateScopes)
		{
			this.Root = this;
			if (validateScopes)
			{
				this._callSiteValidator = new CallSiteValidator();
			}
			this._table = new ServiceTable(serviceDescriptors);
			this._table.Add(typeof(IServiceProvider), new ServiceProviderService());
			this._table.Add(typeof(IServiceScopeFactory), new ServiceScopeService());
			this._table.Add(typeof(IEnumerable<>), new OpenIEnumerableService(this._table));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002213 File Offset: 0x00000413
		internal ServiceProvider(ServiceProvider parent)
		{
			this.Root = parent.Root;
			this._table = parent._table;
			this._callSiteValidator = parent._callSiteValidator;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000224A File Offset: 0x0000044A
		public object GetService(Type serviceType)
		{
			Func<ServiceProvider, object> orAdd = this._table.RealizedServices.GetOrAdd(serviceType, ServiceProvider._createServiceAccessor, this);
			CallSiteValidator callSiteValidator = this._callSiteValidator;
			if (callSiteValidator != null)
			{
				callSiteValidator.ValidateResolution(serviceType, this);
			}
			return orAdd(this);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000227C File Offset: 0x0000047C
		private static Func<ServiceProvider, object> CreateServiceAccessor(Type serviceType, ServiceProvider serviceProvider)
		{
			IServiceCallSite serviceCallSite = serviceProvider.GetServiceCallSite(serviceType, new HashSet<Type>());
			if (serviceCallSite != null)
			{
				CallSiteValidator callSiteValidator = serviceProvider._callSiteValidator;
				if (callSiteValidator != null)
				{
					callSiteValidator.ValidateCallSite(serviceType, serviceCallSite);
				}
				return ServiceProvider.RealizeService(serviceProvider._table, serviceType, serviceCallSite);
			}
			return (ServiceProvider _) => null;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022D9 File Offset: 0x000004D9
		internal static Func<ServiceProvider, object> RealizeService(ServiceTable table, Type serviceType, IServiceCallSite callSite)
		{
			int callCount = 0;
			Action <>9__1;
			return delegate(ServiceProvider provider)
			{
				if (Interlocked.Increment(ref callCount) == 2)
				{
					Action action;
					if ((action = <>9__1) == null)
					{
						action = (<>9__1 = delegate
						{
							Func<ServiceProvider, object> func = new CallSiteExpressionBuilder(ServiceProvider._callSiteRuntimeResolver).Build(callSite);
							table.RealizedServices[serviceType] = func;
						});
					}
					Task.Run(action);
				}
				return ServiceProvider._callSiteRuntimeResolver.Resolve(callSite, provider);
			};
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002308 File Offset: 0x00000508
		internal IServiceCallSite GetServiceCallSite(Type serviceType, ISet<Type> callSiteChain)
		{
			IServiceCallSite serviceCallSite;
			try
			{
				if (callSiteChain.Contains(serviceType))
				{
					throw new InvalidOperationException(Resources.FormatCircularDependencyException(serviceType));
				}
				callSiteChain.Add(serviceType);
				ServiceEntry serviceEntry;
				if (this._table.TryGetEntry(serviceType, out serviceEntry))
				{
					serviceCallSite = this.GetResolveCallSite(serviceEntry.Last, callSiteChain);
				}
				else
				{
					object emptyIEnumerableOrNull = this.GetEmptyIEnumerableOrNull(serviceType);
					if (emptyIEnumerableOrNull != null)
					{
						serviceCallSite = new EmptyIEnumerableCallSite(serviceType, emptyIEnumerableOrNull);
					}
					else
					{
						serviceCallSite = null;
					}
				}
			}
			finally
			{
				callSiteChain.Remove(serviceType);
			}
			return serviceCallSite;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002388 File Offset: 0x00000588
		internal IServiceCallSite GetResolveCallSite(IService service, ISet<Type> callSiteChain)
		{
			IServiceCallSite serviceCallSite = service.CreateCallSite(this, callSiteChain);
			if (serviceCallSite is InstanceService)
			{
				return serviceCallSite;
			}
			if (service.Lifetime == ServiceLifetime.Transient)
			{
				return new TransientCallSite(serviceCallSite);
			}
			if (service.Lifetime == ServiceLifetime.Scoped)
			{
				return new ScopedCallSite(service, serviceCallSite);
			}
			return new SingletonCallSite(service, serviceCallSite);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023D0 File Offset: 0x000005D0
		public void Dispose()
		{
			Dictionary<object, object> resolvedServices = this.ResolvedServices;
			lock (resolvedServices)
			{
				if (!this._disposeCalled)
				{
					this._disposeCalled = true;
					if (this._transientDisposables != null)
					{
						foreach (IDisposable disposable in this._transientDisposables)
						{
							disposable.Dispose();
						}
						this._transientDisposables.Clear();
					}
					foreach (KeyValuePair<object, object> keyValuePair in this.ResolvedServices)
					{
						IDisposable disposable2 = keyValuePair.Value as IDisposable;
						if (disposable2 != null)
						{
							disposable2.Dispose();
						}
					}
					this.ResolvedServices.Clear();
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024D0 File Offset: 0x000006D0
		internal object CaptureDisposable(object service)
		{
			if (this != service)
			{
				IDisposable disposable = service as IDisposable;
				if (disposable != null)
				{
					Dictionary<object, object> resolvedServices = this.ResolvedServices;
					lock (resolvedServices)
					{
						if (this._transientDisposables == null)
						{
							this._transientDisposables = new List<IDisposable>();
						}
						this._transientDisposables.Add(disposable);
					}
				}
			}
			return service;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002538 File Offset: 0x00000738
		private object GetEmptyIEnumerableOrNull(Type serviceType)
		{
			TypeInfo typeInfo = serviceType.GetTypeInfo();
			if (typeInfo.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			{
				return Array.CreateInstance(typeInfo.GenericTypeArguments[0], new int[1]);
			}
			return null;
		}

		// Token: 0x04000002 RID: 2
		private readonly CallSiteValidator _callSiteValidator;

		// Token: 0x04000003 RID: 3
		private readonly ServiceTable _table;

		// Token: 0x04000004 RID: 4
		private bool _disposeCalled;

		// Token: 0x04000005 RID: 5
		private List<IDisposable> _transientDisposables;

		// Token: 0x04000008 RID: 8
		private static readonly Func<Type, ServiceProvider, Func<ServiceProvider, object>> _createServiceAccessor = new Func<Type, ServiceProvider, Func<ServiceProvider, object>>(ServiceProvider.CreateServiceAccessor);

		// Token: 0x04000009 RID: 9
		private static readonly CallSiteRuntimeResolver _callSiteRuntimeResolver = new CallSiteRuntimeResolver();
	}
}
