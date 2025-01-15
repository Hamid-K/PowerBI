using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000350 RID: 848
	internal enum PartitionState
	{
		// Token: 0x040010E3 RID: 4323
		Healthy,
		// Token: 0x040010E4 RID: 4324
		UnderReconfiguration,
		// Token: 0x040010E5 RID: 4325
		NoWriteQuorum,
		// Token: 0x040010E6 RID: 4326
		NotPrimary,
		// Token: 0x040010E7 RID: 4327
		Throttled
	}
}
