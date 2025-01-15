using System;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x0200003B RID: 59
	public abstract class MatrixExprHost : DataRegionExprHost
	{
		// Token: 0x0400004E RID: 78
		public MatrixDynamicGroupExprHost RowGroupingsHost;

		// Token: 0x0400004F RID: 79
		public MatrixDynamicGroupExprHost ColumnGroupingsHost;
	}
}
