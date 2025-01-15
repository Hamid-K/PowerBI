using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000050 RID: 80
	internal interface IServiceHelperFactory
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060002A1 RID: 673
		object ServiceHelper { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060002A2 RID: 674
		ITrustedHeaderVerification TrustedHeaderVerification { get; }
	}
}
