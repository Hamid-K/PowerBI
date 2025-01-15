using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200001D RID: 29
	internal abstract class ExpressionNodeVisitor<TResult> : ExpressionNodeVisitor<TResult>, IExpressionNodeVisitor<TResult>, IExpressionNodeVisitor<TResult>
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00005253 File Offset: 0x00003453
		protected ExpressionNodeVisitor(bool trackParents)
			: base(trackParents)
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000525C File Offset: 0x0000345C
		internal override TResult Visit(ExpressionNode node)
		{
			base.PushParentNode(node);
			TResult tresult;
			switch (node.Kind)
			{
			case ExpressionNodeKind.AggregatableCurrentGroupExpression:
				tresult = this.Visit((AggregatableCurrentGroupExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.AggregatableSubQueryExpression:
				tresult = this.Visit((AggregatableSubQueryExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.ApplyContextFilter:
				tresult = this.Visit((ApplyContextFilterExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.BatchColumnReference:
				tresult = this.Visit((BatchColumnReferenceExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.BatchColumnReferenceByExpressionId:
				tresult = this.Visit((BatchColumnReferenceByExpressionIdExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.BatchFilterInlinedDeclarationCalculation:
				tresult = this.Visit((BatchFilterInlinedDeclarationCalculationExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.BatchScalarDeclarationReference:
				tresult = this.Visit((BatchScalarDeclarationReferenceExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.BatchSubQuery:
				tresult = this.Visit((BatchSubQueryExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.DataSetFieldReference:
				tresult = this.Visit((DataSetFieldReferenceExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.FilterInlinedCalculation:
				tresult = this.Visit((FilterInlinedCalculationExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.RelatedResolvedProperty:
				tresult = this.Visit((RelatedResolvedPropertyExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.RemoveGroupings:
				tresult = this.Visit((RemoveGroupingsExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.ResolvedCalculationReference:
				tresult = this.Visit((ResolvedCalculationReferenceExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.ResolvedDataTransformTableColumnReference:
				tresult = this.Visit((ResolvedDataTransformTableColumnReferenceExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.ResolvedEntitySet:
				tresult = this.Visit((ResolvedEntitySetExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.ResolvedGroupKeyReference:
				tresult = this.Visit((ResolvedGroupKeyReferenceExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.ResolvedLimitReference:
				tresult = this.Visit((ResolvedLimitReferenceExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.ResolvedProperty:
				tresult = this.Visit((ResolvedPropertyExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.ResolvedScopeReference:
				tresult = this.Visit((ResolvedScopeReferenceExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.ResolvedScopeValueDefinitionReference:
				tresult = this.Visit((ResolvedScopeValueDefinitionReferenceExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.ResolvedSortKeyReference:
				tresult = this.Visit((ResolvedSortKeyReferenceExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.ResolvedRollup:
				tresult = this.Visit((ResolvedRollupExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.Rollup:
				tresult = this.Visit((RollupExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.SingleValue:
				tresult = this.Visit((SingleValueExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.SubExpression:
				tresult = this.Visit((SubExpressionNode)node);
				goto IL_0272;
			case ExpressionNodeKind.UnaryOperator:
				tresult = this.Visit((UnaryOperatorExpressionNode)node);
				goto IL_0272;
			}
			tresult = base.VisitInternal(node);
			IL_0272:
			base.PopParentNode();
			return tresult;
		}

		// Token: 0x0600012B RID: 299
		public abstract TResult Visit(UnaryOperatorExpressionNode node);

		// Token: 0x0600012C RID: 300
		public abstract TResult Visit(BatchFilterInlinedDeclarationCalculationExpressionNode node);

		// Token: 0x0600012D RID: 301
		public abstract TResult Visit(BatchColumnReferenceExpressionNode node);

		// Token: 0x0600012E RID: 302
		public abstract TResult Visit(BatchColumnReferenceByExpressionIdExpressionNode node);

		// Token: 0x0600012F RID: 303
		public abstract TResult Visit(BatchScalarDeclarationReferenceExpressionNode node);

		// Token: 0x06000130 RID: 304
		public abstract TResult Visit(BatchSubQueryExpressionNode node);

		// Token: 0x06000131 RID: 305
		public abstract TResult Visit(RemoveGroupingsExpressionNode node);

		// Token: 0x06000132 RID: 306
		public abstract TResult Visit(ResolvedScopeReferenceExpressionNode node);

		// Token: 0x06000133 RID: 307
		public abstract TResult Visit(ResolvedEntitySetExpressionNode node);

		// Token: 0x06000134 RID: 308
		public abstract TResult Visit(ResolvedPropertyExpressionNode node);

		// Token: 0x06000135 RID: 309
		public abstract TResult Visit(FilterInlinedCalculationExpressionNode node);

		// Token: 0x06000136 RID: 310
		public abstract TResult Visit(ResolvedCalculationReferenceExpressionNode node);

		// Token: 0x06000137 RID: 311
		public abstract TResult Visit(AggregatableCurrentGroupExpressionNode node);

		// Token: 0x06000138 RID: 312
		public abstract TResult Visit(AggregatableSubQueryExpressionNode node);

		// Token: 0x06000139 RID: 313
		public abstract TResult Visit(ApplyContextFilterExpressionNode node);

		// Token: 0x0600013A RID: 314
		public abstract TResult Visit(SubExpressionNode node);

		// Token: 0x0600013B RID: 315
		public abstract TResult Visit(RollupExpressionNode node);

		// Token: 0x0600013C RID: 316
		public abstract TResult Visit(ResolvedRollupExpressionNode node);

		// Token: 0x0600013D RID: 317
		public abstract TResult Visit(ResolvedGroupKeyReferenceExpressionNode node);

		// Token: 0x0600013E RID: 318
		public abstract TResult Visit(ResolvedLimitReferenceExpressionNode node);

		// Token: 0x0600013F RID: 319
		public abstract TResult Visit(ResolvedSortKeyReferenceExpressionNode node);

		// Token: 0x06000140 RID: 320
		public abstract TResult Visit(ResolvedScopeValueDefinitionReferenceExpressionNode node);

		// Token: 0x06000141 RID: 321
		public abstract TResult Visit(RelatedResolvedPropertyExpressionNode node);

		// Token: 0x06000142 RID: 322
		public abstract TResult Visit(SingleValueExpressionNode node);

		// Token: 0x06000143 RID: 323
		public abstract TResult Visit(ResolvedDataTransformTableColumnReferenceExpressionNode node);

		// Token: 0x06000144 RID: 324
		public abstract TResult Visit(DataSetFieldReferenceExpressionNode node);
	}
}
