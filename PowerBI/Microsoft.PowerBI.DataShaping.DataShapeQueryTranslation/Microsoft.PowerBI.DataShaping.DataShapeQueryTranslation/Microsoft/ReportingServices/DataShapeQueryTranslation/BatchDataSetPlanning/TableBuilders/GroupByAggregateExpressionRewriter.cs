using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001CD RID: 461
	internal sealed class GroupByAggregateExpressionRewriter : ExpressionNodeTreeTransform
	{
		// Token: 0x0600102E RID: 4142 RVA: 0x00042F40 File Offset: 0x00041140
		private GroupByAggregateExpressionRewriter(WritableExpressionTable expressionTable, ICommonPlanningContext plannerContext, AggregateGroupByTableManager tableManager, ExpressionContext expressionContext, PlanDeclarationCollection declarations, IScope originalScope, bool preferPlanName, bool respectInstanceFilters)
			: base(false)
		{
			this.m_expressionTable = expressionTable;
			this.m_plannerContext = plannerContext;
			this.m_tableManager = tableManager;
			this.m_expressionContext = expressionContext;
			this.m_aggregateInputStack = new AggregateInputStack();
			this.m_declarations = declarations;
			this.m_originalContainingScope = originalScope;
			this.m_preferPlanName = preferPlanName;
			this.m_respectInstanceFilters = respectInstanceFilters;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00042F9C File Offset: 0x0004119C
		public static void Rewrite(ExpressionId id, IScope containingScope, WritableExpressionTable expressionTable, ICommonPlanningContext plannerContext, AggregateGroupByTableManager tableManager, ExpressionContext expressionContext, PlanDeclarationCollection declarations, bool preferPlanName, bool respectInstanceFilters)
		{
			new GroupByAggregateExpressionRewriter(expressionTable, plannerContext, tableManager, expressionContext, declarations, containingScope, preferPlanName, respectInstanceFilters).Rewrite(id, containingScope);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00042FB8 File Offset: 0x000411B8
		private void Rewrite(ExpressionId rootExprId, IScope containingScope)
		{
			ExpressionNode expressionNode = this.m_expressionTable.GetNode(rootExprId);
			expressionNode = this.VisitInScope(expressionNode, containingScope);
			this.m_expressionTable.SetNode(rootExprId, expressionNode);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00042FE8 File Offset: 0x000411E8
		private ExpressionNode VisitInScope(ExpressionNode node, IScope scope)
		{
			IScope containingScope = this.m_containingScope;
			this.m_containingScope = scope;
			ExpressionNode expressionNode = this.Visit(node);
			this.m_containingScope = containingScope;
			return expressionNode;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00043011 File Offset: 0x00041211
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			if (node.Descriptor.FunctionCategory == FunctionCategory.Aggregate)
			{
				return this.VisitAggregate(node);
			}
			if (node.Descriptor.Name == "Evaluate")
			{
				return this.VisitEvaluate(node);
			}
			return base.Visit(node);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00043050 File Offset: 0x00041250
		private ExpressionNode VisitAggregate(FunctionCallExpressionNode node)
		{
			if (node.Descriptor.Name == "PercentileInc" || node.Descriptor.Name == "PercentileExc" || node.Descriptor.Name == "Median" || node.Descriptor.Name == "Any")
			{
				return this.RewriteAggregateInline(node);
			}
			return this.RewriteAggregateForGroupBy(node);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x000430C8 File Offset: 0x000412C8
		private ExpressionNode RewriteAggregateForGroupBy(FunctionCallExpressionNode node)
		{
			IScope containingScope = this.m_containingScope;
			ExpressionNode expressionNode = node.Arguments[0];
			RowResultSetType rowResultSetType = ((node.Descriptor.Name == "SingleValue") ? RowResultSetType.Single : RowResultSetType.Unrestricted);
			this.m_aggregateInputStack.PushSlot(AggregateRewriteMode.GroupBy, node.Descriptor.IgnoresNulls, rowResultSetType);
			ExpressionNode expressionNode2 = this.Visit(expressionNode);
			AggregateInputStack.Slot slot = this.m_aggregateInputStack.PopSlot();
			if (slot.Scope == null)
			{
				if (slot.HasCalculationReference)
				{
					return AggregateExpressionFlattener.Rewrite(new FunctionCallExpressionNode(node.Descriptor, node.UsageKind, new ExpressionNode[] { expressionNode2 }));
				}
				return node;
			}
			else
			{
				if (this.m_plannerContext.ScopeTree.HaveEquivalentDataContext(containingScope, slot.Scope))
				{
					this.RecordOutputScope(slot.Scope, slot.Table);
					return AggregateExpressionFlattener.Rewrite(new FunctionCallExpressionNode(node.Descriptor, node.UsageKind, new ExpressionNode[] { expressionNode2 }));
				}
				AggregateGroupByTable orCreateAggregateTable = this.m_tableManager.GetOrCreateAggregateTable(slot.Table, containingScope);
				this.RecordOutputScope(containingScope, orCreateAggregateTable);
				return node.RewriteAggregateInGroupBy(expressionNode2, orCreateAggregateTable, this.m_expressionContext, this.m_expressionTable, this.m_preferPlanName);
			}
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x000431F0 File Offset: 0x000413F0
		private ExpressionNode RewriteAggregateInline(FunctionCallExpressionNode node)
		{
			this.m_aggregateInputStack.PushSlot(AggregateRewriteMode.Inline, node.Descriptor.IgnoresNulls, RowResultSetType.Unrestricted);
			IList<ExpressionNode> list = Util.VisitList<ExpressionNode>(node.Arguments, new Func<ExpressionNode, ExpressionNode>(this.Visit));
			AggregateInputStack.Slot slot = this.m_aggregateInputStack.PopSlot();
			FunctionCallExpressionNode functionCallExpressionNode = new FunctionCallExpressionNode(node.Descriptor, node.UsageKind, list);
			if (slot.Scope == null && slot.HasCalculationReference)
			{
				return AggregateExpressionFlattener.Rewrite(functionCallExpressionNode);
			}
			return functionCallExpressionNode;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00043268 File Offset: 0x00041468
		private ExpressionNode VisitEvaluate(FunctionCallExpressionNode node)
		{
			IScope evaluateOutputScope = ExpressionAnalysisUtils.GetEvaluateOutputScope(node);
			if (evaluateOutputScope == null)
			{
				return base.Visit(node);
			}
			AggregateInputStack.Slot activeSlot = this.m_aggregateInputStack.GetActiveSlot();
			ExpressionNode expressionNode = this.VisitInScope(node.Arguments[0], evaluateOutputScope);
			if (this.m_plannerContext.ScopeTree.HaveEquivalentDataContext(evaluateOutputScope, this.m_containingScope))
			{
				return expressionNode;
			}
			if (activeSlot == null || activeSlot.Mode == AggregateRewriteMode.Inline)
			{
				return this.RewriteEvaluateInline(evaluateOutputScope, activeSlot, expressionNode);
			}
			return this.RewriteEvaluateForGroupBy(evaluateOutputScope, activeSlot, expressionNode);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x000432E4 File Offset: 0x000414E4
		private ExpressionNode RewriteEvaluateForGroupBy(IScope evaluateScope, AggregateInputStack.Slot slot, ExpressionNode translatedNode)
		{
			if (slot != null && slot.Scope == null)
			{
				ExpressionNode expressionNode;
				IAggregateInputTable subqueryInputTable = this.m_tableManager.GetSubqueryInputTable(this.m_containingScope, evaluateScope, translatedNode, this.m_expressionContext, this.m_expressionTable, slot.AggregateIgnoresNulls, out expressionNode);
				this.RecordOutputScope(subqueryInputTable.OutputRowScope, subqueryInputTable);
				return expressionNode;
			}
			return translatedNode;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00043334 File Offset: 0x00041534
		private ExpressionNode RewriteEvaluateInline(IScope evaluateScope, AggregateInputStack.Slot slot, ExpressionNode translatedNode)
		{
			bool flag = slot != null && slot.AggregateIgnoresNulls;
			RowResultSetType rowResultSetType = ((slot == null) ? RowResultSetType.Unrestricted : slot.RowResultSetType);
			ExpressionNode expressionNode;
			IAggregateInputTable inlineAggregationTable = this.m_tableManager.GetInlineAggregationTable(this.m_containingScope, evaluateScope, translatedNode, this.m_expressionContext, this.m_expressionTable, flag, rowResultSetType, out expressionNode);
			this.RecordOutputScope(inlineAggregationTable.OutputRowScope, inlineAggregationTable);
			SubExpressionNode subExpressionNode = this.m_expressionTable.CreateSubExpression(expressionNode);
			PlanOperation planOperation = inlineAggregationTable.ToPlanOperation(this.m_plannerContext.Annotations, this.m_plannerContext.ScopeTree);
			return new AggregatableSubQueryExpressionNode(subExpressionNode.ExpressionId, null, planOperation);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x000433C4 File Offset: 0x000415C4
		public override ExpressionNode Visit(ResolvedCalculationReferenceExpressionNode node)
		{
			IScope containingScope = this.m_plannerContext.ScopeTree.GetContainingScope(node.Calculation);
			AggregateInputStack.Slot activeSlot = this.m_aggregateInputStack.GetActiveSlot();
			if (activeSlot == null)
			{
				return base.Visit(node);
			}
			activeSlot.HasCalculationReference = true;
			AggregateRewriteMode mode = activeSlot.Mode;
			if (mode == AggregateRewriteMode.GroupBy)
			{
				return this.RewriteCalcRefForGroupBy(node, containingScope, activeSlot.AggregateIgnoresNulls, activeSlot.RowResultSetType);
			}
			if (mode != AggregateRewriteMode.Inline)
			{
				throw new InvalidOperationException("Unknown mode");
			}
			return this.RewriteCalcRefInline(node, containingScope, activeSlot.AggregateIgnoresNulls, activeSlot.RowResultSetType);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0004344C File Offset: 0x0004164C
		private ExpressionNode RewriteCalcRefForGroupBy(ResolvedCalculationReferenceExpressionNode node, IScope referencedScope, bool aggregateIgnoresNulls, RowResultSetType rowResultSetType)
		{
			if (!this.m_plannerContext.ScopeTree.HaveEquivalentDataContext(this.m_originalContainingScope, referencedScope))
			{
				IAggregateInputTable orCreateInputTable = this.m_tableManager.GetOrCreateInputTable(this.m_originalContainingScope, referencedScope, node.Calculation, aggregateIgnoresNulls, this.m_expressionContext, this.m_declarations, this.m_respectInstanceFilters, rowResultSetType);
				this.RecordOutputScope(orCreateInputTable.OutputRowScope, orCreateInputTable);
				ExpressionNode expressionNode;
				if (this.m_tableManager.TryGetSubqueryTableColumnReference(orCreateInputTable, out expressionNode))
				{
					return expressionNode;
				}
			}
			return node;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000434C4 File Offset: 0x000416C4
		private ExpressionNode RewriteCalcRefInline(ResolvedCalculationReferenceExpressionNode node, IScope referencedScope, bool aggregateIgnoresNulls, RowResultSetType rowResultSetType)
		{
			if (!this.m_plannerContext.ScopeTree.HaveEquivalentDataContext(this.m_containingScope, referencedScope))
			{
				IAggregateInputTable orCreateInputTable = this.m_tableManager.GetOrCreateInputTable(this.m_originalContainingScope, referencedScope, node.Calculation, aggregateIgnoresNulls, this.m_expressionContext, this.m_declarations, this.m_respectInstanceFilters, rowResultSetType);
				this.RecordOutputScope(orCreateInputTable.OutputRowScope, orCreateInputTable);
				PlanOperation planOperation = orCreateInputTable.ToPlanOperation(this.m_plannerContext.Annotations, this.m_plannerContext.ScopeTree);
				return new AggregatableSubQueryExpressionNode(node.Calculation.Value.ExpressionId.Value, null, planOperation);
			}
			return node;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00043568 File Offset: 0x00041768
		private void RecordOutputScope(IScope scope, IAggregateInputTable inputTable)
		{
			AggregateInputStack.Slot activeSlot = this.m_aggregateInputStack.GetActiveSlot();
			if (activeSlot != null)
			{
				if (activeSlot.Scope != null)
				{
					if (!this.m_plannerContext.ScopeTree.AreSameScope(activeSlot.Scope, scope))
					{
						this.m_expressionContext.ErrorContext.Register(TranslationMessages.AggregateWithMultipleInputScopes(EngineMessageSeverity.Error, this.m_expressionContext.ObjectType, this.m_expressionContext.ObjectId, this.m_expressionContext.PropertyName, activeSlot.Scope.Id, scope.Id));
						return;
					}
					Contract.RetailAssert(activeSlot.Table == inputTable, "Found multiple aggregate input tables for the same scope");
					return;
				}
				else
				{
					activeSlot.Fill(scope, inputTable);
				}
			}
		}

		// Token: 0x0400078C RID: 1932
		private readonly WritableExpressionTable m_expressionTable;

		// Token: 0x0400078D RID: 1933
		private readonly ICommonPlanningContext m_plannerContext;

		// Token: 0x0400078E RID: 1934
		private readonly ExpressionContext m_expressionContext;

		// Token: 0x0400078F RID: 1935
		private readonly AggregateGroupByTableManager m_tableManager;

		// Token: 0x04000790 RID: 1936
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x04000791 RID: 1937
		private readonly IScope m_originalContainingScope;

		// Token: 0x04000792 RID: 1938
		private readonly bool m_preferPlanName;

		// Token: 0x04000793 RID: 1939
		private readonly bool m_respectInstanceFilters;

		// Token: 0x04000794 RID: 1940
		private readonly AggregateInputStack m_aggregateInputStack;

		// Token: 0x04000795 RID: 1941
		private IScope m_containingScope;
	}
}
