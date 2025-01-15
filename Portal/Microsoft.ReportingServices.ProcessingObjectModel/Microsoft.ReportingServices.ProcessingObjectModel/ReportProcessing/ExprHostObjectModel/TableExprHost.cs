using System;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x0200003D RID: 61
	public abstract class TableExprHost : DataRegionExprHost
	{
		// Token: 0x04000051 RID: 81
		public TableGroupExprHost TableGroupsHost;

		// Token: 0x04000052 RID: 82
		public IndexedExprHost TableRowVisibilityHiddenExpressions;

		// Token: 0x04000053 RID: 83
		public IndexedExprHost TableColumnVisibilityHiddenExpressions;
	}
}
