using System;

namespace Microsoft.AnalysisServices.Tabular.DDL
{
	// Token: 0x02000120 RID: 288
	internal enum DDLType
	{
		// Token: 0x040002F3 RID: 755
		Create,
		// Token: 0x040002F4 RID: 756
		Alter,
		// Token: 0x040002F5 RID: 757
		Delete,
		// Token: 0x040002F6 RID: 758
		Refresh,
		// Token: 0x040002F7 RID: 759
		Rename,
		// Token: 0x040002F8 RID: 760
		SequencePoint,
		// Token: 0x040002F9 RID: 761
		Upgrade,
		// Token: 0x040002FA RID: 762
		MergePartitions,
		// Token: 0x040002FB RID: 763
		AnalyzeRefreshPolicyImpact
	}
}
