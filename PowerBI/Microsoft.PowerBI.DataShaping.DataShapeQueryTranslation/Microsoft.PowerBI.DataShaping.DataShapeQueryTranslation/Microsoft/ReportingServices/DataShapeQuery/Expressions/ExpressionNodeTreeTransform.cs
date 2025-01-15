using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200001E RID: 30
	internal abstract class ExpressionNodeTreeTransform : ExpressionNodeVisitor<ExpressionNode>
	{
		// Token: 0x06000145 RID: 325 RVA: 0x000054E3 File Offset: 0x000036E3
		protected ExpressionNodeTreeTransform(bool trackParents)
			: base(trackParents)
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000054EC File Offset: 0x000036EC
		public override ExpressionNode Visit(UnaryOperatorExpressionNode node)
		{
			ExpressionNode expressionNode = this.Visit(node.Operand);
			if (node.Operand != expressionNode)
			{
				node = new UnaryOperatorExpressionNode(node.OperatorKind, expressionNode);
			}
			return base.ReplaceCurrentNode<UnaryOperatorExpressionNode>(node);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005524 File Offset: 0x00003724
		public override ExpressionNode Visit(BinaryOperatorExpressionNode node)
		{
			ExpressionNode expressionNode = this.Visit(node.Left);
			ExpressionNode expressionNode2 = this.Visit(node.Right);
			if (node.Left != expressionNode || node.Right != expressionNode2)
			{
				node = new BinaryOperatorExpressionNode(node.OperatorKind, expressionNode, expressionNode2);
			}
			return base.ReplaceCurrentNode<BinaryOperatorExpressionNode>(node);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005573 File Offset: 0x00003773
		public override ExpressionNode Visit(EntitySetExpressionNode node)
		{
			return base.ReplaceCurrentNode<EntitySetExpressionNode>(node);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000557C File Offset: 0x0000377C
		public override ExpressionNode Visit(ResolvedEntitySetExpressionNode node)
		{
			return base.ReplaceCurrentNode<ResolvedEntitySetExpressionNode>(node);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005588 File Offset: 0x00003788
		public override ExpressionNode Visit(RemoveGroupingsExpressionNode node)
		{
			ExpressionNode expressionNode = this.Visit(node.Expression);
			IList<ExpressionNode> list = Util.VisitList<ExpressionNode>(node.GroupKeysToRemove, new Func<ExpressionNode, ExpressionNode>(this.Visit));
			if (node.Expression != expressionNode || node.GroupKeysToRemove != list)
			{
				node = new RemoveGroupingsExpressionNode(expressionNode, list);
			}
			return base.ReplaceCurrentNode<RemoveGroupingsExpressionNode>(node);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000055E0 File Offset: 0x000037E0
		public override ExpressionNode Visit(FilterInlinedCalculationExpressionNode node)
		{
			ExpressionNode expressionNode = this.Visit(node.ExpressionNode);
			if (node.ExpressionNode != expressionNode)
			{
				node = new FilterInlinedCalculationExpressionNode(expressionNode, node.Calculation, node.FilterCondition, node.FilterExpression);
			}
			return base.ReplaceCurrentNode<FilterInlinedCalculationExpressionNode>(node);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005624 File Offset: 0x00003824
		public override ExpressionNode Visit(LiteralExpressionNode node)
		{
			return base.ReplaceCurrentNode<LiteralExpressionNode>(node);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000562D File Offset: 0x0000382D
		public override ExpressionNode Visit(BatchFilterInlinedDeclarationCalculationExpressionNode node)
		{
			return base.ReplaceCurrentNode<BatchFilterInlinedDeclarationCalculationExpressionNode>(node);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005636 File Offset: 0x00003836
		public override ExpressionNode Visit(BatchColumnReferenceExpressionNode node)
		{
			return base.ReplaceCurrentNode<BatchColumnReferenceExpressionNode>(node);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000563F File Offset: 0x0000383F
		public override ExpressionNode Visit(BatchColumnReferenceByExpressionIdExpressionNode node)
		{
			return base.ReplaceCurrentNode<BatchColumnReferenceByExpressionIdExpressionNode>(node);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005648 File Offset: 0x00003848
		public override ExpressionNode Visit(BatchScalarDeclarationReferenceExpressionNode node)
		{
			return base.ReplaceCurrentNode<BatchScalarDeclarationReferenceExpressionNode>(node);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005651 File Offset: 0x00003851
		public override ExpressionNode Visit(BatchSubQueryExpressionNode node)
		{
			return base.ReplaceCurrentNode<BatchSubQueryExpressionNode>(node);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000565A File Offset: 0x0000385A
		public override ExpressionNode Visit(StructureReferenceExpressionNode node)
		{
			return base.ReplaceCurrentNode<StructureReferenceExpressionNode>(node);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005663 File Offset: 0x00003863
		public override ExpressionNode Visit(ResolvedScopeReferenceExpressionNode node)
		{
			return base.ReplaceCurrentNode<ResolvedScopeReferenceExpressionNode>(node);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000566C File Offset: 0x0000386C
		public override ExpressionNode Visit(PropertyExpressionNode node)
		{
			EntitySetExpressionNode entitySetExpressionNode = (EntitySetExpressionNode)this.Visit(node.EntitySet);
			if (node.EntitySet != entitySetExpressionNode)
			{
				node = new PropertyExpressionNode(entitySetExpressionNode, node.Name, node.Property);
			}
			return base.ReplaceCurrentNode<PropertyExpressionNode>(node);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000056AF File Offset: 0x000038AF
		public override ExpressionNode Visit(ResolvedPropertyExpressionNode node)
		{
			return base.ReplaceCurrentNode<ResolvedPropertyExpressionNode>(node);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000056B8 File Offset: 0x000038B8
		public override ExpressionNode Visit(RelatedResolvedPropertyExpressionNode node)
		{
			return base.ReplaceCurrentNode<RelatedResolvedPropertyExpressionNode>(node);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000056C1 File Offset: 0x000038C1
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			return this.Visit(node, new Func<ExpressionNode, ExpressionNode>(this.Visit));
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000056D8 File Offset: 0x000038D8
		internal ExpressionNode Visit(FunctionCallExpressionNode node, Func<ExpressionNode, ExpressionNode> visitArg)
		{
			IList<ExpressionNode> list = Util.VisitList<ExpressionNode>(node.Arguments, visitArg);
			if (node.Arguments != list)
			{
				node = new FunctionCallExpressionNode(node.Descriptor, node.UsageKind, list);
			}
			return base.ReplaceCurrentNode<FunctionCallExpressionNode>(node);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005716 File Offset: 0x00003916
		public override ExpressionNode Visit(ResolvedCalculationReferenceExpressionNode node)
		{
			return base.ReplaceCurrentNode<ResolvedCalculationReferenceExpressionNode>(node);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005720 File Offset: 0x00003920
		public override ExpressionNode Visit(AggregatableCurrentGroupExpressionNode node)
		{
			ExpressionNode expressionNode = this.Visit(node.Projection);
			if (node.Projection != expressionNode)
			{
				node = new AggregatableCurrentGroupExpressionNode(expressionNode);
			}
			return base.ReplaceCurrentNode<AggregatableCurrentGroupExpressionNode>(node);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005752 File Offset: 0x00003952
		public override ExpressionNode Visit(AggregatableSubQueryExpressionNode node)
		{
			return base.ReplaceCurrentNode<AggregatableSubQueryExpressionNode>(node);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000575C File Offset: 0x0000395C
		public override ExpressionNode Visit(ApplyContextFilterExpressionNode node)
		{
			ExpressionNode expressionNode = this.Visit(node.Expression);
			if (node.Expression != expressionNode)
			{
				node = new ApplyContextFilterExpressionNode(expressionNode, node.ContextTables);
			}
			return base.ReplaceCurrentNode<ApplyContextFilterExpressionNode>(node);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005794 File Offset: 0x00003994
		public override ExpressionNode Visit(SubExpressionNode node)
		{
			return base.ReplaceCurrentNode<SubExpressionNode>(node);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000579D File Offset: 0x0000399D
		public override ExpressionNode Visit(ResolvedGroupKeyReferenceExpressionNode node)
		{
			return base.ReplaceCurrentNode<ResolvedGroupKeyReferenceExpressionNode>(node);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000057A6 File Offset: 0x000039A6
		public override ExpressionNode Visit(ResolvedLimitReferenceExpressionNode node)
		{
			return base.ReplaceCurrentNode<ResolvedLimitReferenceExpressionNode>(node);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000057AF File Offset: 0x000039AF
		public override ExpressionNode Visit(ResolvedSortKeyReferenceExpressionNode node)
		{
			return base.ReplaceCurrentNode<ResolvedSortKeyReferenceExpressionNode>(node);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000057B8 File Offset: 0x000039B8
		public override ExpressionNode Visit(ResolvedScopeValueDefinitionReferenceExpressionNode node)
		{
			return base.ReplaceCurrentNode<ResolvedScopeValueDefinitionReferenceExpressionNode>(node);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000057C4 File Offset: 0x000039C4
		public override ExpressionNode Visit(RollupExpressionNode node)
		{
			ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode = (ResolvedCalculationReferenceExpressionNode)this.Visit(node.Argument);
			if (node.Argument != resolvedCalculationReferenceExpressionNode)
			{
				node = new RollupExpressionNode(resolvedCalculationReferenceExpressionNode);
			}
			return base.ReplaceCurrentNode<RollupExpressionNode>(node);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000057FC File Offset: 0x000039FC
		public override ExpressionNode Visit(ResolvedRollupExpressionNode node)
		{
			ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode = (ResolvedCalculationReferenceExpressionNode)this.Visit(node.Argument);
			if (node.Argument != resolvedCalculationReferenceExpressionNode)
			{
				node = new ResolvedRollupExpressionNode(resolvedCalculationReferenceExpressionNode);
			}
			return base.ReplaceCurrentNode<ResolvedRollupExpressionNode>(node);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005833 File Offset: 0x00003A33
		public override ExpressionNode Visit(SingleValueExpressionNode node)
		{
			return base.ReplaceCurrentNode<SingleValueExpressionNode>(node);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000583C File Offset: 0x00003A3C
		public override ExpressionNode Visit(DataTransformTableColumnReferenceExpressionNode node)
		{
			return base.ReplaceCurrentNode<DataTransformTableColumnReferenceExpressionNode>(node);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005845 File Offset: 0x00003A45
		public override ExpressionNode Visit(ResolvedDataTransformTableColumnReferenceExpressionNode node)
		{
			return base.ReplaceCurrentNode<ResolvedDataTransformTableColumnReferenceExpressionNode>(node);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000584E File Offset: 0x00003A4E
		public override ExpressionNode Visit(DataSetFieldReferenceExpressionNode node)
		{
			return base.ReplaceCurrentNode<DataSetFieldReferenceExpressionNode>(node);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005857 File Offset: 0x00003A57
		public override ExpressionNode Visit(DaxTextExpressionNode node)
		{
			return base.ReplaceCurrentNode<DaxTextExpressionNode>(node);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005860 File Offset: 0x00003A60
		public override ExpressionNode Visit(QueryParameterReferenceExpressionNode node)
		{
			return base.ReplaceCurrentNode<QueryParameterReferenceExpressionNode>(node);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005869 File Offset: 0x00003A69
		public override ExpressionNode Visit(VisualCalculationExpressionNode node)
		{
			return base.ReplaceCurrentNode<VisualCalculationExpressionNode>(node);
		}
	}
}
