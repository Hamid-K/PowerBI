using System;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000156 RID: 342
	public enum DataIndexElementStatus
	{
		// Token: 0x04000681 RID: 1665
		None,
		// Token: 0x04000682 RID: 1666
		Indexed,
		// Token: 0x04000683 RID: 1667
		PartiallyIndexed,
		// Token: 0x04000684 RID: 1668
		MissingStatistics,
		// Token: 0x04000685 RID: 1669
		IndexLimitReached,
		// Token: 0x04000686 RID: 1670
		Cancelled
	}
}
