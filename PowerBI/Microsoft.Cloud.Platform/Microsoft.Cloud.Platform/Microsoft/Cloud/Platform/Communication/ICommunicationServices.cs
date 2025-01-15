using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004DE RID: 1246
	public interface ICommunicationServices
	{
		// Token: 0x060025D5 RID: 9685
		T GetService<T>(ServiceIdentification serviceId, RouterIdentifier router, OnProxyCreating onProxyCreating) where T : class;

		// Token: 0x060025D6 RID: 9686
		T GetService<T>(ServiceIdentification serviceId, Router router, OnProxyCreating onProxyCreating) where T : class;

		// Token: 0x060025D7 RID: 9687
		T GetService<T>(ServiceIdentification serviceId, Router router, bool routerIdentifiable, OnProxyCreating onProxyCreating) where T : class;

		// Token: 0x060025D8 RID: 9688
		void PublishService(ServiceIdentification serviceId, object singletonInstance, OnServiceCreating onServiceCreating);

		// Token: 0x060025D9 RID: 9689
		void PublishService(ServiceIdentification serviceId, object singletonInstance, IEndpointProvider endpointProvider, OnServiceCreating onServiceCreating);
	}
}
