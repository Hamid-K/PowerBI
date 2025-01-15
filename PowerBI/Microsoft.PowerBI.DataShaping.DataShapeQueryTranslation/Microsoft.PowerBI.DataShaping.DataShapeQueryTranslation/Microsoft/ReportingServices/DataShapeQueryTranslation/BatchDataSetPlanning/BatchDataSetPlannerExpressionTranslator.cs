using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000166 RID: 358
	internal sealed class BatchDataSetPlannerExpressionTranslator : DataShapeVisitor
	{
		// Token: 0x06000D01 RID: 3329 RVA: 0x00035A44 File Offset: 0x00033C44
		private BatchDataSetPlannerExpressionTranslator(WritableExpressionTable outputExpressionTable, BatchDataSetPlannerContext plannerContext, IFilterDeclarationCollection filterDeclarations)
		{
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_plannerContext = plannerContext;
			this.m_activeDataShapes = new Stack<DataShape>();
			this.m_filterDeclarations = filterDeclarations;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00035A6C File Offset: 0x00033C6C
		public static void Translate(WritableExpressionTable outputExpressionTable, BatchDataSetPlannerContext plannerContext, DataShape dataShape, IFilterDeclarationCollection filterDeclarations)
		{
			new BatchDataSetPlannerExpressionTranslator(outputExpressionTable, plannerContext, filterDeclarations).Visit(dataShape);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00035A7C File Offset: 0x00033C7C
		protected override void Visit(Calculation calculation)
		{
			IScope containingScope = this.m_plannerContext.ScopeTree.GetContainingScope(calculation);
			DataShape dataShape = this.m_activeDataShapes.Peek();
			foreach (ExpressionId expressionId in this.m_plannerContext.CalculationMap.GetExpressions(calculation))
			{
				ExpressionContext expressionContext = new ExpressionContext(this.m_plannerContext.ErrorContext, calculation.ObjectType, calculation.Id, "Value");
				ExpressionNode node = this.m_outputExpressionTable.GetNode(expressionId);
				ExpressionNode expressionNode = this.TranslateNode(expressionContext, containingScope, node);
				if (expressionNode != null)
				{
					expressionNode = CalculationFilterTransform.TranslateToInlineFilter(expressionNode, this.m_plannerContext.OutputExpressionTable, calculation, dataShape);
					expressionNode = this.ApplyFilterDeclarationInlining(expressionNode);
					this.m_outputExpressionTable.SetNode(expressionId, expressionNode);
				}
			}
			if (this.m_plannerContext.Annotations.CanBeHandledByProcessing(calculation))
			{
				this.m_outputExpressionTable.SetNode(calculation.Value.ExpressionId.Value, this.m_plannerContext.OutputExpressionTable.GetNode(calculation.Value.ExpressionId.Value));
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00035BB4 File Offset: 0x00033DB4
		private ExpressionNode ApplyFilterDeclarationInlining(ExpressionNode outputNode)
		{
			FilterInlinedCalculationExpressionNode filterInlinedCalculationExpressionNode = outputNode as FilterInlinedCalculationExpressionNode;
			PlanOperationDeclarationReference planOperationDeclarationReference;
			if (filterInlinedCalculationExpressionNode != null && this.m_filterDeclarations.TryGetFilterDeclaration(filterInlinedCalculationExpressionNode.FilterCondition, out planOperationDeclarationReference))
			{
				Contract.RetailAssert(planOperationDeclarationReference != null, "filterTableDeclaration != null");
				outputNode = new BatchFilterInlinedDeclarationCalculationExpressionNode(filterInlinedCalculationExpressionNode.ExpressionNode, planOperationDeclarationReference);
			}
			return outputNode;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00035C00 File Offset: 0x00033E00
		protected override void Visit(DataTransformTableColumn column)
		{
			if (!this.m_plannerContext.ApplyTransformsInQuery)
			{
				return;
			}
			IScope innermostScopeInDataShape = this.m_plannerContext.ScopeTree.GetInnermostScopeInDataShape(this.m_activeDataShapes.Peek());
			ExpressionContext expressionContext = column.CreateValueExpressionContext(this.m_plannerContext.ErrorContext);
			ExpressionNode node = this.m_outputExpressionTable.GetNode(column.Value);
			ExpressionNode expressionNode = this.TranslateNode(expressionContext, innermostScopeInDataShape, node);
			if (expressionNode != null)
			{
				this.m_outputExpressionTable.SetNode(column.Value, expressionNode);
			}
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00035C7C File Offset: 0x00033E7C
		private ExpressionNode TranslateNode(ExpressionContext context, IScope containingScope, ExpressionNode inputNode)
		{
			return BatchDataSetPlannerExpressionTreeTranslator.Translate(context, inputNode, this.m_plannerContext.ScopeTree, this.m_plannerContext.Annotations, containingScope, this.m_plannerContext.OutputExpressionTable, this.m_plannerContext.TransformReferenceMap, this.m_plannerContext.ApplyTransformsInQuery);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00035CC8 File Offset: 0x00033EC8
		protected override void Enter(DataShape dataShape)
		{
			this.m_activeDataShapes.Push(dataShape);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00035CD6 File Offset: 0x00033ED6
		protected override void Exit(DataShape dataShape)
		{
			this.m_activeDataShapes.Pop();
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00035CE4 File Offset: 0x00033EE4
		protected override void Enter(DataMember dataMember)
		{
			if (!dataMember.IsDynamic || dataMember.Group.SortKeys == null)
			{
				return;
			}
			List<SortKey> sortKeys = dataMember.Group.SortKeys;
			ScopeIdDefinition scopeIdDefinition = dataMember.Group.ScopeIdDefinition;
			SortByMeasureInfoCollection sortByMeasureInfos = this.m_plannerContext.Annotations.DataMemberAnnotations.GetSortByMeasureInfos(dataMember);
			for (int i = 0; i < sortKeys.Count; i++)
			{
				SortKey sortKey = sortKeys[i];
				this.TransformSortExpression(sortKey, dataMember, sortKey.Value, sortByMeasureInfos);
				if (scopeIdDefinition != null)
				{
					this.TransformSortExpression(sortKey, dataMember, scopeIdDefinition.Values[i].Value, sortByMeasureInfos);
				}
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00035D80 File Offset: 0x00033F80
		private void TransformSortExpression(SortKey sortKey, IScope containingScope, Expression expression, SortByMeasureInfoCollection sortByMeasureInfos)
		{
			if (sortByMeasureInfos != null && sortByMeasureInfos.IsAtMeasureScope)
			{
				BatchDataSetPlanningUtils.ExtractExpressionFromEvaluateRollup(expression, this.m_plannerContext.OutputExpressionTable, this.m_outputExpressionTable);
			}
			ExpressionNode expressionNode = BatchDataSetPlannerFilterExpressionTreeTranslator.Translate(new ExpressionContext(this.m_plannerContext.ErrorContext, sortKey.ObjectType, sortKey.Id, "Value"), this.m_outputExpressionTable.GetNode(expression), this.m_plannerContext.ScopeTree, this.m_plannerContext.Annotations, containingScope, this.m_plannerContext.OutputExpressionTable, this.m_plannerContext.TransformReferenceMap, this.m_plannerContext.ApplyTransformsInQuery);
			this.m_outputExpressionTable.SetNode(expression, expressionNode);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00035E2C File Offset: 0x0003402C
		protected override void Visit(Filter filter, Identifier dataShapeId)
		{
			BatchDataSetPlannerFilterExpressionTranslator.Translate(new BatchDataSetPlannerFilterExpressionTreeTranslator(this.m_plannerContext.ScopeTree, this.m_plannerContext.Annotations, this.m_plannerContext.OutputExpressionTable, this.m_plannerContext.TransformReferenceMap, this.m_plannerContext.ApplyTransformsInQuery), filter, this.m_plannerContext.ErrorContext, this.m_plannerContext.OutputExpressionTable, this.m_outputExpressionTable, this.m_plannerContext.ScopeTree);
			ContextFilterDataShapeVisitor.Visit(filter, delegate(DataShape dataShape, ObjectType filterConditionType)
			{
				this.Visit(dataShape);
			});
		}

		// Token: 0x04000675 RID: 1653
		private readonly BatchDataSetPlannerContext m_plannerContext;

		// Token: 0x04000676 RID: 1654
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x04000677 RID: 1655
		private readonly Stack<DataShape> m_activeDataShapes;

		// Token: 0x04000678 RID: 1656
		private readonly IFilterDeclarationCollection m_filterDeclarations;
	}
}
