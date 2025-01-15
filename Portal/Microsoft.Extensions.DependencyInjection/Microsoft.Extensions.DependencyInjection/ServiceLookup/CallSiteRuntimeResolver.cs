using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x02000009 RID: 9
	internal class CallSiteRuntimeResolver : CallSiteVisitor<ServiceProvider, object>
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002FAC File Offset: 0x000011AC
		public object Resolve(IServiceCallSite callSite, ServiceProvider provider)
		{
			return this.VisitCallSite(callSite, provider);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002FB6 File Offset: 0x000011B6
		protected override object VisitTransient(TransientCallSite transientCallSite, ServiceProvider provider)
		{
			return provider.CaptureDisposable(this.VisitCallSite(transientCallSite.Service, provider));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002FCC File Offset: 0x000011CC
		protected override object VisitConstructor(ConstructorCallSite constructorCallSite, ServiceProvider provider)
		{
			object[] array = new object[constructorCallSite.ParameterCallSites.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.VisitCallSite(constructorCallSite.ParameterCallSites[i], provider);
			}
			object obj;
			try
			{
				obj = constructorCallSite.ConstructorInfo.Invoke(array);
			}
			catch (Exception ex) when (ex.InnerException != null)
			{
				ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				throw;
			}
			return obj;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003058 File Offset: 0x00001258
		protected override object VisitSingleton(SingletonCallSite singletonCallSite, ServiceProvider provider)
		{
			return this.VisitScoped(singletonCallSite, provider.Root);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003068 File Offset: 0x00001268
		protected override object VisitScoped(ScopedCallSite scopedCallSite, ServiceProvider provider)
		{
			Dictionary<object, object> resolvedServices = provider.ResolvedServices;
			object obj;
			lock (resolvedServices)
			{
				if (!provider.ResolvedServices.TryGetValue(scopedCallSite.Key, out obj))
				{
					obj = this.VisitCallSite(scopedCallSite.ServiceCallSite, provider);
					provider.ResolvedServices.Add(scopedCallSite.Key, obj);
				}
			}
			return obj;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000030D8 File Offset: 0x000012D8
		protected override object VisitConstant(ConstantCallSite constantCallSite, ServiceProvider provider)
		{
			return constantCallSite.DefaultValue;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000030E0 File Offset: 0x000012E0
		protected override object VisitCreateInstance(CreateInstanceCallSite createInstanceCallSite, ServiceProvider provider)
		{
			object obj;
			try
			{
				obj = Activator.CreateInstance(createInstanceCallSite.Descriptor.ImplementationType);
			}
			catch (Exception ex) when (ex.InnerException != null)
			{
				ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				throw;
			}
			return obj;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003140 File Offset: 0x00001340
		protected override object VisitInstanceService(InstanceService instanceCallSite, ServiceProvider provider)
		{
			return instanceCallSite.Descriptor.ImplementationInstance;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000029B2 File Offset: 0x00000BB2
		protected override object VisitServiceProviderService(ServiceProviderService serviceProviderService, ServiceProvider provider)
		{
			return provider;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000314D File Offset: 0x0000134D
		protected override object VisitEmptyIEnumerable(EmptyIEnumerableCallSite emptyIEnumerableCallSite, ServiceProvider provider)
		{
			return emptyIEnumerableCallSite.ServiceInstance;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003155 File Offset: 0x00001355
		protected override object VisitServiceScopeService(ServiceScopeService serviceScopeService, ServiceProvider provider)
		{
			return new ServiceScopeFactory(provider);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003160 File Offset: 0x00001360
		protected override object VisitClosedIEnumerable(ClosedIEnumerableCallSite closedIEnumerableCallSite, ServiceProvider provider)
		{
			Array array = Array.CreateInstance(closedIEnumerableCallSite.ItemType, new int[] { closedIEnumerableCallSite.ServiceCallSites.Length });
			for (int i = 0; i < closedIEnumerableCallSite.ServiceCallSites.Length; i++)
			{
				object obj = this.VisitCallSite(closedIEnumerableCallSite.ServiceCallSites[i], provider);
				array.SetValue(obj, new int[] { i });
			}
			return array;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000031BF File Offset: 0x000013BF
		protected override object VisitFactoryService(FactoryService factoryService, ServiceProvider provider)
		{
			return factoryService.Descriptor.ImplementationFactory(provider);
		}
	}
}
