using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C1 RID: 193
	internal enum MiningNodeTreeOpType
	{
		// Token: 0x04000724 RID: 1828
		TreeopChildren = 1,
		// Token: 0x04000725 RID: 1829
		TreeopSiblings,
		// Token: 0x04000726 RID: 1830
		TreeopParent = 4,
		// Token: 0x04000727 RID: 1831
		TreeopSelf = 8,
		// Token: 0x04000728 RID: 1832
		TreeopDescendants = 16,
		// Token: 0x04000729 RID: 1833
		TreeopAncestors = 32
	}
}
