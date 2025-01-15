using System;
using System.Collections.Concurrent;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x0200000A RID: 10
	internal class CallSiteValidator : CallSiteVisitor<CallSiteValidator.CallSiteValidatorState, Type>
	{
		// Token: 0x0600005B RID: 91 RVA: 0x000031DC File Offset: 0x000013DC
		public void ValidateCallSite(Type serviceType, IServiceCallSite callSite)
		{
			Type type = this.VisitCallSite(callSite, default(CallSiteValidator.CallSiteValidatorState));
			if (type != null)
			{
				this._scopedServices[serviceType] = type;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000320C File Offset: 0x0000140C
		public void ValidateResolution(Type serviceType, ServiceProvider serviceProvider)
		{
			Type type;
			if (serviceProvider != serviceProvider.Root || !this._scopedServices.TryGetValue(serviceType, out type))
			{
				return;
			}
			if (serviceType == type)
			{
				throw new InvalidOperationException(Resources.FormatDirectScopedResolvedFromRootException(serviceType, "Scoped".ToLowerInvariant()));
			}
			throw new InvalidOperationException(Resources.FormatScopedResolvedFromRootException(serviceType, type, "Scoped".ToLowerInvariant()));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003263 File Offset: 0x00001463
		protected override Type VisitTransient(TransientCallSite transientCallSite, CallSiteValidator.CallSiteValidatorState state)
		{
			return this.VisitCallSite(transientCallSite.Service, state);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003274 File Offset: 0x00001474
		protected override Type VisitConstructor(ConstructorCallSite constructorCallSite, CallSiteValidator.CallSiteValidatorState state)
		{
			Type type = null;
			foreach (IServiceCallSite serviceCallSite in constructorCallSite.ParameterCallSites)
			{
				Type type2 = this.VisitCallSite(serviceCallSite, state);
				if (type == null)
				{
					type = type2;
				}
			}
			return type;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000032B0 File Offset: 0x000014B0
		protected override Type VisitClosedIEnumerable(ClosedIEnumerableCallSite closedIEnumerableCallSite, CallSiteValidator.CallSiteValidatorState state)
		{
			Type type = null;
			foreach (IServiceCallSite serviceCallSite in closedIEnumerableCallSite.ServiceCallSites)
			{
				Type type2 = this.VisitCallSite(serviceCallSite, state);
				if (type == null)
				{
					type = type2;
				}
			}
			return type;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000032E9 File Offset: 0x000014E9
		protected override Type VisitSingleton(SingletonCallSite singletonCallSite, CallSiteValidator.CallSiteValidatorState state)
		{
			state.Singleton = singletonCallSite;
			return this.VisitCallSite(singletonCallSite.ServiceCallSite, state);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003300 File Offset: 0x00001500
		protected override Type VisitScoped(ScopedCallSite scopedCallSite, CallSiteValidator.CallSiteValidatorState state)
		{
			if (scopedCallSite.ServiceCallSite is ServiceScopeService)
			{
				return null;
			}
			if (state.Singleton != null)
			{
				throw new InvalidOperationException(Resources.FormatScopedInSingletonException(scopedCallSite.Key.ServiceType, state.Singleton.Key.ServiceType, "Scoped".ToLowerInvariant(), "Singleton".ToLowerInvariant()));
			}
			return scopedCallSite.Key.ServiceType;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000336B File Offset: 0x0000156B
		protected override Type VisitConstant(ConstantCallSite constantCallSite, CallSiteValidator.CallSiteValidatorState state)
		{
			return null;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000336B File Offset: 0x0000156B
		protected override Type VisitCreateInstance(CreateInstanceCallSite createInstanceCallSite, CallSiteValidator.CallSiteValidatorState state)
		{
			return null;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000336B File Offset: 0x0000156B
		protected override Type VisitInstanceService(InstanceService instanceCallSite, CallSiteValidator.CallSiteValidatorState state)
		{
			return null;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000336B File Offset: 0x0000156B
		protected override Type VisitServiceProviderService(ServiceProviderService serviceProviderService, CallSiteValidator.CallSiteValidatorState state)
		{
			return null;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000336B File Offset: 0x0000156B
		protected override Type VisitEmptyIEnumerable(EmptyIEnumerableCallSite emptyIEnumerableCallSite, CallSiteValidator.CallSiteValidatorState state)
		{
			return null;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000336B File Offset: 0x0000156B
		protected override Type VisitServiceScopeService(ServiceScopeService serviceScopeService, CallSiteValidator.CallSiteValidatorState state)
		{
			return null;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000336B File Offset: 0x0000156B
		protected override Type VisitFactoryService(FactoryService factoryService, CallSiteValidator.CallSiteValidatorState state)
		{
			return null;
		}

		// Token: 0x04000018 RID: 24
		private readonly ConcurrentDictionary<Type, Type> _scopedServices = new ConcurrentDictionary<Type, Type>();

		// Token: 0x02000028 RID: 40
		internal struct CallSiteValidatorState
		{
			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060000DC RID: 220 RVA: 0x00003F2D File Offset: 0x0000212D
			// (set) Token: 0x060000DD RID: 221 RVA: 0x00003F35 File Offset: 0x00002135
			public SingletonCallSite Singleton { get; set; }
		}
	}
}
