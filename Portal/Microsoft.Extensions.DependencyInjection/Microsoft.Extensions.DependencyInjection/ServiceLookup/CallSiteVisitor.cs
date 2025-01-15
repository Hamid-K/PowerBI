using System;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x0200000B RID: 11
	internal abstract class CallSiteVisitor<TArgument, TResult>
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003384 File Offset: 0x00001584
		protected virtual TResult VisitCallSite(IServiceCallSite callSite, TArgument argument)
		{
			FactoryService factoryService = callSite as FactoryService;
			if (factoryService != null)
			{
				return this.VisitFactoryService(factoryService, argument);
			}
			ClosedIEnumerableCallSite closedIEnumerableCallSite = callSite as ClosedIEnumerableCallSite;
			if (closedIEnumerableCallSite != null)
			{
				return this.VisitClosedIEnumerable(closedIEnumerableCallSite, argument);
			}
			ConstructorCallSite constructorCallSite = callSite as ConstructorCallSite;
			if (constructorCallSite != null)
			{
				return this.VisitConstructor(constructorCallSite, argument);
			}
			TransientCallSite transientCallSite = callSite as TransientCallSite;
			if (transientCallSite != null)
			{
				return this.VisitTransient(transientCallSite, argument);
			}
			SingletonCallSite singletonCallSite = callSite as SingletonCallSite;
			if (singletonCallSite != null)
			{
				return this.VisitSingleton(singletonCallSite, argument);
			}
			ScopedCallSite scopedCallSite = callSite as ScopedCallSite;
			if (scopedCallSite != null)
			{
				return this.VisitScoped(scopedCallSite, argument);
			}
			ConstantCallSite constantCallSite = callSite as ConstantCallSite;
			if (constantCallSite != null)
			{
				return this.VisitConstant(constantCallSite, argument);
			}
			CreateInstanceCallSite createInstanceCallSite = callSite as CreateInstanceCallSite;
			if (createInstanceCallSite != null)
			{
				return this.VisitCreateInstance(createInstanceCallSite, argument);
			}
			InstanceService instanceService = callSite as InstanceService;
			if (instanceService != null)
			{
				return this.VisitInstanceService(instanceService, argument);
			}
			ServiceProviderService serviceProviderService = callSite as ServiceProviderService;
			if (serviceProviderService != null)
			{
				return this.VisitServiceProviderService(serviceProviderService, argument);
			}
			EmptyIEnumerableCallSite emptyIEnumerableCallSite = callSite as EmptyIEnumerableCallSite;
			if (emptyIEnumerableCallSite != null)
			{
				return this.VisitEmptyIEnumerable(emptyIEnumerableCallSite, argument);
			}
			ServiceScopeService serviceScopeService = callSite as ServiceScopeService;
			if (serviceScopeService != null)
			{
				return this.VisitServiceScopeService(serviceScopeService, argument);
			}
			throw new NotSupportedException(string.Format("Call site type {0} is not supported", new object[] { callSite.GetType() }));
		}

		// Token: 0x0600006B RID: 107
		protected abstract TResult VisitTransient(TransientCallSite transientCallSite, TArgument argument);

		// Token: 0x0600006C RID: 108
		protected abstract TResult VisitConstructor(ConstructorCallSite constructorCallSite, TArgument argument);

		// Token: 0x0600006D RID: 109
		protected abstract TResult VisitSingleton(SingletonCallSite singletonCallSite, TArgument argument);

		// Token: 0x0600006E RID: 110
		protected abstract TResult VisitScoped(ScopedCallSite scopedCallSite, TArgument argument);

		// Token: 0x0600006F RID: 111
		protected abstract TResult VisitConstant(ConstantCallSite constantCallSite, TArgument argument);

		// Token: 0x06000070 RID: 112
		protected abstract TResult VisitCreateInstance(CreateInstanceCallSite createInstanceCallSite, TArgument argument);

		// Token: 0x06000071 RID: 113
		protected abstract TResult VisitInstanceService(InstanceService instanceCallSite, TArgument argument);

		// Token: 0x06000072 RID: 114
		protected abstract TResult VisitServiceProviderService(ServiceProviderService serviceProviderService, TArgument argument);

		// Token: 0x06000073 RID: 115
		protected abstract TResult VisitEmptyIEnumerable(EmptyIEnumerableCallSite emptyIEnumerableCallSite, TArgument argument);

		// Token: 0x06000074 RID: 116
		protected abstract TResult VisitServiceScopeService(ServiceScopeService serviceScopeService, TArgument argument);

		// Token: 0x06000075 RID: 117
		protected abstract TResult VisitClosedIEnumerable(ClosedIEnumerableCallSite closedIEnumerableCallSite, TArgument argument);

		// Token: 0x06000076 RID: 118
		protected abstract TResult VisitFactoryService(FactoryService factoryService, TArgument argument);
	}
}
