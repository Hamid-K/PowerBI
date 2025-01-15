using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000AF RID: 175
	internal enum MetadataCacheState
	{
		// Token: 0x04000690 RID: 1680
		Empty,
		// Token: 0x04000691 RID: 1681
		UpToDate,
		// Token: 0x04000692 RID: 1682
		NeedsValidnessCheck,
		// Token: 0x04000693 RID: 1683
		Invalid,
		// Token: 0x04000694 RID: 1684
		Abandoned
	}
}
