using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C1 RID: 193
	internal enum MiningNodeTreeOpType
	{
		// Token: 0x04000731 RID: 1841
		TreeopChildren = 1,
		// Token: 0x04000732 RID: 1842
		TreeopSiblings,
		// Token: 0x04000733 RID: 1843
		TreeopParent = 4,
		// Token: 0x04000734 RID: 1844
		TreeopSelf = 8,
		// Token: 0x04000735 RID: 1845
		TreeopDescendants = 16,
		// Token: 0x04000736 RID: 1846
		TreeopAncestors = 32
	}
}
