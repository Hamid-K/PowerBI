using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200001C RID: 28
	internal interface IExpressionNodeVisitor<TResult> : IExpressionNodeVisitor<TResult>
	{
		// Token: 0x0600010F RID: 271
		TResult Visit(UnaryOperatorExpressionNode node);

		// Token: 0x06000110 RID: 272
		TResult Visit(BatchFilterInlinedDeclarationCalculationExpressionNode node);

		// Token: 0x06000111 RID: 273
		TResult Visit(BatchColumnReferenceExpressionNode node);

		// Token: 0x06000112 RID: 274
		TResult Visit(BatchColumnReferenceByExpressionIdExpressionNode node);

		// Token: 0x06000113 RID: 275
		TResult Visit(BatchScalarDeclarationReferenceExpressionNode node);

		// Token: 0x06000114 RID: 276
		TResult Visit(BatchSubQueryExpressionNode node);

		// Token: 0x06000115 RID: 277
		TResult Visit(RemoveGroupingsExpressionNode node);

		// Token: 0x06000116 RID: 278
		TResult Visit(ResolvedScopeReferenceExpressionNode node);

		// Token: 0x06000117 RID: 279
		TResult Visit(ResolvedEntitySetExpressionNode node);

		// Token: 0x06000118 RID: 280
		TResult Visit(ResolvedPropertyExpressionNode node);

		// Token: 0x06000119 RID: 281
		TResult Visit(FilterInlinedCalculationExpressionNode node);

		// Token: 0x0600011A RID: 282
		TResult Visit(ResolvedCalculationReferenceExpressionNode node);

		// Token: 0x0600011B RID: 283
		TResult Visit(AggregatableCurrentGroupExpressionNode node);

		// Token: 0x0600011C RID: 284
		TResult Visit(AggregatableSubQueryExpressionNode node);

		// Token: 0x0600011D RID: 285
		TResult Visit(ApplyContextFilterExpressionNode node);

		// Token: 0x0600011E RID: 286
		TResult Visit(SubExpressionNode node);

		// Token: 0x0600011F RID: 287
		TResult Visit(RollupExpressionNode node);

		// Token: 0x06000120 RID: 288
		TResult Visit(ResolvedRollupExpressionNode node);

		// Token: 0x06000121 RID: 289
		TResult Visit(ResolvedGroupKeyReferenceExpressionNode node);

		// Token: 0x06000122 RID: 290
		TResult Visit(ResolvedLimitReferenceExpressionNode node);

		// Token: 0x06000123 RID: 291
		TResult Visit(ResolvedSortKeyReferenceExpressionNode node);

		// Token: 0x06000124 RID: 292
		TResult Visit(ResolvedScopeValueDefinitionReferenceExpressionNode node);

		// Token: 0x06000125 RID: 293
		TResult Visit(RelatedResolvedPropertyExpressionNode node);

		// Token: 0x06000126 RID: 294
		TResult Visit(SingleValueExpressionNode node);

		// Token: 0x06000127 RID: 295
		TResult Visit(ResolvedDataTransformTableColumnReferenceExpressionNode node);

		// Token: 0x06000128 RID: 296
		TResult Visit(DataSetFieldReferenceExpressionNode node);
	}
}
