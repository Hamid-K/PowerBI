using System;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ModelReconciliation
{
	// Token: 0x020000A2 RID: 162
	internal sealed class ModelReconciliationResult
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x0001D4EF File Offset: 0x0001B6EF
		internal ModelReconciliationResult(ReadOnlyExpressionTable expressionTable, BatchSubtotalAnnotations subtotalAnnotations)
		{
			this.m_expressionTable = expressionTable;
			this.m_subtotalAnnotations = subtotalAnnotations;
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0001D505 File Offset: 0x0001B705
		internal ReadOnlyExpressionTable ExpressionTable
		{
			get
			{
				return this.m_expressionTable;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001D50D File Offset: 0x0001B70D
		internal BatchSubtotalAnnotations SubtotalAnnotations
		{
			get
			{
				return this.m_subtotalAnnotations;
			}
		}

		// Token: 0x04000399 RID: 921
		private readonly ReadOnlyExpressionTable m_expressionTable;

		// Token: 0x0400039A RID: 922
		private readonly BatchSubtotalAnnotations m_subtotalAnnotations;
	}
}
