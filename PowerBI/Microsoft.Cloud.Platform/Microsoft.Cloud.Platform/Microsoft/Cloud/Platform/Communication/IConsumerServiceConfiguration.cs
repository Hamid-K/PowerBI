using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004A2 RID: 1186
	public interface IConsumerServiceConfiguration : IServiceConfiguration
	{
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002483 RID: 9347
		bool AllowImpersonation { get; }

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002484 RID: 9348
		ClientCertificateData ClientCertificateData { get; }

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002485 RID: 9349
		bool UseDoubleWrap { get; }

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002486 RID: 9350
		EndpointInfo EndpointInfo { get; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06002487 RID: 9351
		bool TraceHttpResponseHeadersOnFaultException { get; }
	}
}
