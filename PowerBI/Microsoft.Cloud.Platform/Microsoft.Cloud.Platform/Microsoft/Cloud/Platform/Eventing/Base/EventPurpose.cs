using System;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003B2 RID: 946
	[Flags]
	public enum EventPurpose
	{
		// Token: 0x040009F0 RID: 2544
		None = 0,
		// Token: 0x040009F1 RID: 2545
		Logging = 1,
		// Token: 0x040009F2 RID: 2546
		Monitoring = 2,
		// Token: 0x040009F3 RID: 2547
		Testability = 4,
		// Token: 0x040009F4 RID: 2548
		TransactionDetailsRecord = 8,
		// Token: 0x040009F5 RID: 2549
		PerformanceAnalysis = 16,
		// Token: 0x040009F6 RID: 2550
		StoryTelling = 32,
		// Token: 0x040009F7 RID: 2551
		Billing = 64,
		// Token: 0x040009F8 RID: 2552
		Audit = 128,
		// Token: 0x040009F9 RID: 2553
		Reporting = 256
	}
}
