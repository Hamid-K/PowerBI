using System;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Normalization
{
	// Token: 0x02000098 RID: 152
	internal sealed class DataShapeNormalizationResult
	{
		// Token: 0x06000725 RID: 1829 RVA: 0x0001B312 File Offset: 0x00019512
		internal DataShapeNormalizationResult(ReadOnlyExpressionTable expressionTable, BatchSubtotalAnnotations subtotalAnnotations)
		{
			this.m_expressionTable = expressionTable;
			this.m_subtotalAnnotations = subtotalAnnotations;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001B328 File Offset: 0x00019528
		internal ReadOnlyExpressionTable ExpressionTable
		{
			get
			{
				return this.m_expressionTable;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001B330 File Offset: 0x00019530
		internal BatchSubtotalAnnotations SubtotalAnnotations
		{
			get
			{
				return this.m_subtotalAnnotations;
			}
		}

		// Token: 0x04000374 RID: 884
		private readonly ReadOnlyExpressionTable m_expressionTable;

		// Token: 0x04000375 RID: 885
		private readonly BatchSubtotalAnnotations m_subtotalAnnotations;
	}
}
