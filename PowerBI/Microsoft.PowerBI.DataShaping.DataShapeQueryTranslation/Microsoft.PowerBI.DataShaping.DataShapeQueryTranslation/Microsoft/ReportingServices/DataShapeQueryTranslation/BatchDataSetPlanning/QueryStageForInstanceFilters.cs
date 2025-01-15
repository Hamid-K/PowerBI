using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200018F RID: 399
	internal enum QueryStageForInstanceFilters
	{
		// Token: 0x040006BC RID: 1724
		None,
		// Token: 0x040006BD RID: 1725
		CoreTableAndShowAllRollupContextTables,
		// Token: 0x040006BE RID: 1726
		CoreTableAndShowAllPostFilter,
		// Token: 0x040006BF RID: 1727
		PostCoreTableAndInShowAllRollupContextTables,
		// Token: 0x040006C0 RID: 1728
		PostCoreTableAndInShowAllPostFilter,
		// Token: 0x040006C1 RID: 1729
		PostShowAll
	}
}
