using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000CF RID: 207
	internal interface IExpressionNodeVisitor<TResult>
	{
		// Token: 0x060005BA RID: 1466
		TResult Visit(BinaryOperatorExpressionNode node);

		// Token: 0x060005BB RID: 1467
		TResult Visit(EntitySetExpressionNode node);

		// Token: 0x060005BC RID: 1468
		TResult Visit(LiteralExpressionNode node);

		// Token: 0x060005BD RID: 1469
		TResult Visit(StructureReferenceExpressionNode node);

		// Token: 0x060005BE RID: 1470
		TResult Visit(PropertyExpressionNode node);

		// Token: 0x060005BF RID: 1471
		TResult Visit(FunctionCallExpressionNode node);

		// Token: 0x060005C0 RID: 1472
		TResult Visit(DataTransformTableColumnReferenceExpressionNode node);

		// Token: 0x060005C1 RID: 1473
		TResult Visit(DaxTextExpressionNode node);

		// Token: 0x060005C2 RID: 1474
		TResult Visit(QueryParameterReferenceExpressionNode node);

		// Token: 0x060005C3 RID: 1475
		TResult Visit(VisualCalculationExpressionNode node);
	}
}
