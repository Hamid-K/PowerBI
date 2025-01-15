using System;

namespace Microsoft.OData
{
	// Token: 0x02000027 RID: 39
	public interface IContainerBuilder
	{
		// Token: 0x06000173 RID: 371
		IContainerBuilder AddService(ServiceLifetime lifetime, Type serviceType, Type implementationType);

		// Token: 0x06000174 RID: 372
		IContainerBuilder AddService(ServiceLifetime lifetime, Type serviceType, Func<IServiceProvider, object> implementationFactory);

		// Token: 0x06000175 RID: 373
		IServiceProvider BuildContainer();
	}
}
