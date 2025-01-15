using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004A7 RID: 1191
	public interface IExtendedCommunication
	{
		// Token: 0x06002495 RID: 9365
		T GetService<T>(IConsumerServiceConfiguration serviceConfiguration, Router router, OnProxyCreating onProxyCreating) where T : class;

		// Token: 0x06002496 RID: 9366
		T GetService<T>(IConsumerServiceConfiguration serviceConfiguration, Router router, bool routerIdentifiable, OnProxyCreating onProxyCreating) where T : class;

		// Token: 0x06002497 RID: 9367
		void PublishService(IProviderServiceConfiguration serviceConfiguration, object singletonInstance, IEndpointProvider endpointProvider, OnServiceCreating onServiceCreating);

		// Token: 0x06002498 RID: 9368
		void PublishService(IProviderServiceConfiguration serviceConfiguration, object singletonInstance, OnServiceCreating onServiceCreating);
	}
}
