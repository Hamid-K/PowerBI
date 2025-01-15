using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000AF RID: 175
	internal enum MetadataCacheState
	{
		// Token: 0x04000683 RID: 1667
		Empty,
		// Token: 0x04000684 RID: 1668
		UpToDate,
		// Token: 0x04000685 RID: 1669
		NeedsValidnessCheck,
		// Token: 0x04000686 RID: 1670
		Invalid,
		// Token: 0x04000687 RID: 1671
		Abandoned
	}
}
