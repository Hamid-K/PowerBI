using System;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x0200011A RID: 282
	internal enum SecurityCredentialUsage
	{
		// Token: 0x04000A00 RID: 2560
		Reserved = -1,
		// Token: 0x04000A01 RID: 2561
		Inbound = 1,
		// Token: 0x04000A02 RID: 2562
		Outbount,
		// Token: 0x04000A03 RID: 2563
		Both,
		// Token: 0x04000A04 RID: 2564
		Default
	}
}
