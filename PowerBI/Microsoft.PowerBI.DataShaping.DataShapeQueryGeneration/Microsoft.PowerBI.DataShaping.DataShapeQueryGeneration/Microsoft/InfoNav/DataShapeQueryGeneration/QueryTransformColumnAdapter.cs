using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200009C RID: 156
	internal sealed class QueryTransformColumnAdapter : QueryColumnAdapter<IntermediateQueryTransformTableColumn>
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x0001676E File Offset: 0x0001496E
		private QueryTransformColumnAdapter()
		{
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00016776 File Offset: 0x00014976
		internal override ExpressionNode ToDsqExpression(IntermediateQueryTransformTableColumn column)
		{
			return column.DsqExpression();
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001677E File Offset: 0x0001497E
		internal override IConceptualColumn GetConceptualColumn(IntermediateQueryTransformTableColumn column)
		{
			return column.UnderlyingConceptualColumn;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00016786 File Offset: 0x00014986
		internal override string GetFormatString(IntermediateQueryTransformTableColumn column)
		{
			return column.FormatString;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001678E File Offset: 0x0001498E
		internal override IntermediateQueryTransformTableColumn GetOrCreateColumn(IConceptualColumn newColumn, IntermediateQueryTransformTableColumn existingColumn, DsqExpressionGenerator expressionGenerator, bool propagateRoleAndOmitFromOutput)
		{
			return IntermediateQueryTransformTable.GetOrCreateColumn(newColumn, TransformTableColumnActAs.GroupKey, existingColumn, expressionGenerator, propagateRoleAndOmitFromOutput);
		}

		// Token: 0x04000330 RID: 816
		internal static readonly QueryTransformColumnAdapter Instance = new QueryTransformColumnAdapter();
	}
}
