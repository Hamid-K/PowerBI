using System;

namespace Microsoft.AnalysisServices.Tabular.DDL
{
	// Token: 0x0200011F RID: 287
	internal enum DDLRowsetType
	{
		// Token: 0x040002E9 RID: 745
		Create,
		// Token: 0x040002EA RID: 746
		Alter,
		// Token: 0x040002EB RID: 747
		Delete,
		// Token: 0x040002EC RID: 748
		Refresh,
		// Token: 0x040002ED RID: 749
		Rename,
		// Token: 0x040002EE RID: 750
		Bindings,
		// Token: 0x040002EF RID: 751
		Upgrade,
		// Token: 0x040002F0 RID: 752
		MergePartitions,
		// Token: 0x040002F1 RID: 753
		AnalyzeRefreshPolicyImpact
	}
}
