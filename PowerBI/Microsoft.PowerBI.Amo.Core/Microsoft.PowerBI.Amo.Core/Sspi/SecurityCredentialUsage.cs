using System;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x0200010F RID: 271
	internal enum SecurityCredentialUsage
	{
		// Token: 0x040009B9 RID: 2489
		Reserved = -1,
		// Token: 0x040009BA RID: 2490
		Inbound = 1,
		// Token: 0x040009BB RID: 2491
		Outbount,
		// Token: 0x040009BC RID: 2492
		Both,
		// Token: 0x040009BD RID: 2493
		Default
	}
}
