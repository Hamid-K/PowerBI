using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AE3 RID: 2787
	[Flags]
	public enum CapabilityFlag2
	{
		// Token: 0x04004571 RID: 17777
		None = 0,
		// Token: 0x04004572 RID: 17778
		DistributionListCapable = 1,
		// Token: 0x04004573 RID: 17779
		FastMessageRequest = 2,
		// Token: 0x04004574 RID: 17780
		ResponderConversion = 4,
		// Token: 0x04004575 RID: 17781
		DualUnitOfWork = 8,
		// Token: 0x04004576 RID: 17782
		XaRequest = 16,
		// Token: 0x04004577 RID: 17783
		XaRuntimeTypeApplication = 32,
		// Token: 0x04004578 RID: 17784
		SpiRequest = 64,
		// Token: 0x04004579 RID: 17785
		TraceRouteCapable = 128
	}
}
