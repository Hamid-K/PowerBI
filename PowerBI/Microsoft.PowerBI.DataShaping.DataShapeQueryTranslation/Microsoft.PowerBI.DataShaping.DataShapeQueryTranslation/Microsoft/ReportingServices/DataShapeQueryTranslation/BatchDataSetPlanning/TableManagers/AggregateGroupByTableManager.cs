using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers
{
	// Token: 0x020001AD RID: 429
	internal sealed class AggregateGroupByTableManager
	{
		// Token: 0x06000F17 RID: 3863 RVA: 0x0003D68C File Offset: 0x0003B88C
		internal AggregateGroupByTableManager(IAggregatesPlanningContext plannerContext, RowScopesMetadata joinPredicatesRowScopes, bool omitSubtotalIndicatorColumnsInGroups)
		{
			this.m_plannerContext = plannerContext;
			this.m_joinPredicatesRowScopes = joinPredicatesRowScopes;
			this.m_aggregateTables = new List<AggregateGroupByTable>();
			this.m_referenceTables = new List<AggregateReferenceTable>();
			this.m_subqueryInputTables = new List<AggregateSubqueryTable>();
			this.m_subExpressionMapping = new Dictionary<ExpressionNode, Expression>();
			this.m_outputScopeToNamingContextMapping = new Dictionary<IScope, NamingContext>();
			this.m_omitSubtotalIndicatorColumnsInGroups = omitSubtotalIndicatorColumnsInGroups;
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0003D6EB File Offset: 0x0003B8EB
		internal IReadOnlyList<AggregateGroupByTable> Tables
		{
			get
			{
				return this.m_aggregateTables;
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0003D6F4 File Offset: 0x0003B8F4
		internal void AddReferenceTable(TableReference table)
		{
			AggregateReferenceTable aggregateReferenceTable = new AggregateReferenceTable(table);
			this.AddReferenceTable(aggregateReferenceTable);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0003D70F File Offset: 0x0003B90F
		private void AddReferenceTable(AggregateReferenceTable refTable)
		{
			this.m_referenceTables.Add(refTable);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0003D720 File Offset: 0x0003B920
		public AggregateGroupByTable GetOrCreateAggregateTable(IAggregateInputTable inputTable, IScope outputScope)
		{
			AggregateGroupByTable aggregateGroupByTable;
			if (this.TryGetAggregateTable(inputTable, outputScope, out aggregateGroupByTable))
			{
				return aggregateGroupByTable;
			}
			NamingContext orCreateNamingContext = this.GetOrCreateNamingContext(outputScope);
			aggregateGroupByTable = new AggregateGroupByTable(inputTable, outputScope, orCreateNamingContext, this.m_omitSubtotalIndicatorColumnsInGroups);
			this.AddAggregateTable(aggregateGroupByTable);
			return aggregateGroupByTable;
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0003D75A File Offset: 0x0003B95A
		private void AddAggregateTable(AggregateGroupByTable aggTable)
		{
			this.m_aggregateTables.Add(aggTable);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0003D768 File Offset: 0x0003B968
		public bool TryGetAggregateTable(IAggregateInputTable inputTable, IScope outputScope, out AggregateGroupByTable result)
		{
			ScopeTree scopeTree = this.m_plannerContext.ScopeTree;
			foreach (AggregateGroupByTable aggregateGroupByTable in this.m_aggregateTables)
			{
				if (scopeTree.AreSameScope(aggregateGroupByTable.OutputRowScope, outputScope) && aggregateGroupByTable.Input == inputTable)
				{
					result = aggregateGroupByTable;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003D7E8 File Offset: 0x0003B9E8
		internal IAggregateInputTable GetInlineAggregationTable(IScope stopScope, IScope rowScope, ExpressionNode expressionNode, ExpressionContext expressionContext, WritableExpressionTable expressionTable, bool aggregateIgnoresNulls, RowResultSetType rowResultSetType, out ExpressionNode newNode)
		{
			ScopeTree scopeTree = this.m_plannerContext.ScopeTree;
			List<DataMember> list = this.DetermineRequiredShowAllState(rowScope);
			AggregateReferenceTable aggregateReferenceTable;
			if (this.TryGetReferenceTable(rowScope, null, list, false, rowResultSetType, out aggregateReferenceTable))
			{
				newNode = expressionNode;
				return aggregateReferenceTable;
			}
			return this.GetSubqueryInputTable(stopScope, rowScope, expressionNode, expressionContext, expressionTable, aggregateIgnoresNulls, out newNode);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0003D830 File Offset: 0x0003BA30
		public IAggregateInputTable GetOrCreateInputTable(IScope stopScope, IScope rowScope, Calculation referencedCalc, bool aggregateIgnoresNulls, ExpressionContext expressionContext, PlanDeclarationCollection declarations, bool respectsInstanceFilters, RowResultSetType rowResultSetType)
		{
			bool flag = ExpressionAnalysisUtils.IsDetail(referencedCalc, this.m_plannerContext.Annotations);
			AggregateGroupByTableManager.RequiredShowAllState requiredShowAllState = this.DetermineRequiredShowAllState(rowScope, aggregateIgnoresNulls, flag);
			AggregateReferenceTable aggregateReferenceTable;
			if (this.TryGetReferenceTable(rowScope, referencedCalc, requiredShowAllState.GroupByTableState, respectsInstanceFilters, rowResultSetType, out aggregateReferenceTable))
			{
				return aggregateReferenceTable;
			}
			if (rowResultSetType == RowResultSetType.Single)
			{
				IAggregateInputTable orCreateInputTable = this.GetOrCreateInputTable(stopScope, rowScope, referencedCalc, aggregateIgnoresNulls, expressionContext, declarations, respectsInstanceFilters, RowResultSetType.Unrestricted);
				return this.CreateSingleRowReferenceTable(orCreateInputTable, declarations);
			}
			AggregateGroupByTable aggregateGroupByTable;
			if (this.TryGetAggregateTable(rowScope, requiredShowAllState.GroupByTableState, out aggregateGroupByTable))
			{
				aggregateGroupByTable.AddReferencedDetail(referencedCalc);
				return aggregateGroupByTable;
			}
			IAggregateInputTable aggregateInputTable;
			if (this.TryGetRegroupableReferenceTable(rowScope, referencedCalc, requiredShowAllState.GroupByTableState, respectsInstanceFilters, out aggregateInputTable))
			{
				NamingContext orCreateNamingContext = this.GetOrCreateNamingContext(rowScope);
				AggregateGroupByTable aggregateGroupByTable2 = new AggregateGroupByTable(aggregateInputTable, rowScope, orCreateNamingContext, this.m_omitSubtotalIndicatorColumnsInGroups);
				aggregateGroupByTable2.AddReferencedDetail(referencedCalc);
				this.AddAggregateTable(aggregateGroupByTable2);
				return aggregateGroupByTable2;
			}
			string text;
			return this.GetSubqueryInputTable(stopScope, rowScope, referencedCalc.Value, expressionContext, requiredShowAllState.SubqueryTableState, out text);
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0003D90C File Offset: 0x0003BB0C
		private AggregateGroupByTableManager.RequiredShowAllState DetermineRequiredShowAllState(IScope rowScope, bool aggregateIgnoresNulls, bool isDetail)
		{
			if (isDetail || !aggregateIgnoresNulls)
			{
				List<DataMember> list = this.DetermineRequiredShowAllState(rowScope);
				return new AggregateGroupByTableManager.RequiredShowAllState(list, list);
			}
			if (!this.m_joinPredicatesRowScopes.IsInnermostScope(this.m_plannerContext.ScopeTree, rowScope))
			{
				return new AggregateGroupByTableManager.RequiredShowAllState(this.DetermineRequiredShowAllState(rowScope), null);
			}
			return AggregateGroupByTableManager.RequiredShowAllState.Empty;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0003D95C File Offset: 0x0003BB5C
		private List<DataMember> DetermineRequiredShowAllState(IScope rowScope)
		{
			List<DataMember> list = null;
			foreach (IScope scope in this.m_plannerContext.ScopeTree.GetAllParentScopes(rowScope))
			{
				DataMember dataMember = scope as DataMember;
				if (dataMember != null && dataMember.UsesShowItemsWithNoData())
				{
					Util.AddToLazyList<DataMember>(ref list, dataMember);
				}
			}
			return list;
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0003D9C8 File Offset: 0x0003BBC8
		private bool TryGetReferenceTable(IScope targetRowScope, Calculation referencedCalc, IReadOnlyList<DataMember> showAllState, bool respectsInstanceFilters, RowResultSetType rowResultSetType, out AggregateReferenceTable result)
		{
			ScopeTree scopeTree = this.m_plannerContext.ScopeTree;
			foreach (AggregateReferenceTable aggregateReferenceTable in this.m_referenceTables)
			{
				if (scopeTree.AreSameScope(targetRowScope, aggregateReferenceTable.OutputRowScope) && (referencedCalc == null || aggregateReferenceTable.ContainsCalculation(referencedCalc)) && this.HasRequiredShowAll(aggregateReferenceTable, showAllState) && respectsInstanceFilters == aggregateReferenceTable.RespectsInstanceFilters && rowResultSetType == aggregateReferenceTable.RowResultSetType)
				{
					result = aggregateReferenceTable;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0003DA68 File Offset: 0x0003BC68
		private bool TryGetRegroupableReferenceTable(IScope targetRowScope, Calculation referencedCalc, IReadOnlyList<DataMember> showAllState, bool respectsInstanceFilters, out IAggregateInputTable result)
		{
			ScopeTree scopeTree = this.m_plannerContext.ScopeTree;
			foreach (AggregateReferenceTable aggregateReferenceTable in this.m_referenceTables)
			{
				if (aggregateReferenceTable.RowScopes.IsSameWithAny(scopeTree, targetRowScope) && !scopeTree.AreSameScope(aggregateReferenceTable.OutputRowScope, targetRowScope) && aggregateReferenceTable.ContainsCalculation(referencedCalc) && this.HasRequiredShowAll(aggregateReferenceTable, showAllState) && respectsInstanceFilters == aggregateReferenceTable.RespectsInstanceFilters && aggregateReferenceTable.RowResultSetType == RowResultSetType.Unrestricted)
				{
					result = aggregateReferenceTable;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0003DB14 File Offset: 0x0003BD14
		private AggregateReferenceTable CreateSingleRowReferenceTable(IAggregateInputTable inputTable, PlanDeclarationCollection declarations)
		{
			string tableName = inputTable.TableName;
			PlanOperation planOperation = inputTable.ToPlanOperation(this.m_plannerContext.Annotations, this.m_plannerContext.ScopeTree);
			PlanExpression planExpression = BatchDataSetPlanningUtils.CreateSegmentSizeExpression(1, tableName, this.m_plannerContext.ErrorContext);
			PlanSortItem planSortItem = planOperation.ToAllColumnsSortItems(SortDirection.Ascending);
			PlanOperation planOperation2 = planOperation.TopN(planExpression, planSortItem.AsReadOnlyList<PlanSortItem>(), false);
			AggregateReferenceTable aggregateReferenceTable = inputTable as AggregateReferenceTable;
			PlanOperationContext planOperationContext;
			if (aggregateReferenceTable != null)
			{
				planOperationContext = aggregateReferenceTable.OperationContext.ReplaceTable(planOperation2, null, null, null);
			}
			else
			{
				planOperationContext = new PlanOperationContext(planOperation2, inputTable.OutputRowScope.AsReadOnlyList<IScope>(), null);
			}
			AggregateReferenceTable aggregateReferenceTable2 = new AggregateReferenceTable(new TableReference(planOperationContext, PlanNames.SingleValueTable(tableName), declarations, RowResultSetType.Single));
			this.AddReferenceTable(aggregateReferenceTable2);
			return aggregateReferenceTable2;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003DBC8 File Offset: 0x0003BDC8
		private bool TryGetAggregateTable(IScope targetRowScope, IReadOnlyList<DataMember> showAllState, out AggregateGroupByTable result)
		{
			ScopeTree scopeTree = this.m_plannerContext.ScopeTree;
			foreach (AggregateGroupByTable aggregateGroupByTable in this.m_aggregateTables)
			{
				if (scopeTree.AreSameScope(aggregateGroupByTable.OutputRowScope, targetRowScope) && this.HasRequiredShowAll(aggregateGroupByTable, showAllState))
				{
					result = aggregateGroupByTable;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0003DC48 File Offset: 0x0003BE48
		private bool HasRequiredShowAll(IAggregateInputTable table, IReadOnlyList<DataMember> showAllState)
		{
			return showAllState == null || table.HasRequiredShowAll(showAllState);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0003DC58 File Offset: 0x0003BE58
		private IAggregateInputTable GetSubqueryInputTable(IScope stopScope, IScope rowScope, Expression expression, ExpressionContext expressionContext, IReadOnlyList<DataMember> requiredShowAllState, out string expressionPlanName)
		{
			if (requiredShowAllState != null && requiredShowAllState.Count > 0)
			{
				expressionContext.ErrorContext.Register(TranslationMessages.UnsupportedAggregateOverShowItemsWithNoData(EngineMessageSeverity.Error, expressionContext.ObjectType, expressionContext.ObjectId, expressionContext.PropertyName));
				throw new DataSetPlanningException("Unsupported aggregate over show all group.");
			}
			AggregateSubqueryTable aggregateSubqueryTable;
			if (!this.TryGetSubqueryInputTable(stopScope, rowScope, expression.ExpressionId.Value, out aggregateSubqueryTable))
			{
				Contract.RetailFail("Failed to get a subquery input table for stopScope '{0}', rowScope '{1}' and expressionId '{2}'.", stopScope.Id.Value, rowScope.Id.Value, expression.ExpressionId.Value.Value);
			}
			expressionPlanName = aggregateSubqueryTable.ExpressionPlanName;
			return aggregateSubqueryTable;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0003DD04 File Offset: 0x0003BF04
		internal IAggregateInputTable GetSubqueryInputTable(IScope stopScope, IScope rowScope, ExpressionNode expressionNode, ExpressionContext expressionContext, WritableExpressionTable expressionTable, bool aggregateIgnoresNulls, out ExpressionNode newNode)
		{
			Expression expression;
			if (!this.m_subExpressionMapping.TryGetValue(expressionNode, out expression))
			{
				SubExpressionNode subExpressionNode = expressionTable.CreateSubExpression(expressionNode);
				expression = new Expression(ExprNodes.Literal("SubExpressionHolder"), new ExpressionId?(subExpressionNode.ExpressionId));
				this.m_subExpressionMapping.Add(expressionNode, expression);
			}
			AggregateGroupByTableManager.RequiredShowAllState requiredShowAllState = this.DetermineRequiredShowAllState(rowScope, aggregateIgnoresNulls, false);
			string text;
			IAggregateInputTable subqueryInputTable = this.GetSubqueryInputTable(stopScope, rowScope, expression, expressionContext, requiredShowAllState.SubqueryTableState, out text);
			newNode = new BatchColumnReferenceExpressionNode(text);
			return subqueryInputTable;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0003DD79 File Offset: 0x0003BF79
		public void AddSubqueryInputTable(AggregateSubqueryTable table)
		{
			this.m_subqueryInputTables.Add(table);
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0003DD88 File Offset: 0x0003BF88
		public bool TryGetSubqueryInputTable(IScope stopScope, IScope rowScope, ExpressionId expressionId, out AggregateSubqueryTable result)
		{
			ScopeTree scopeTree = this.m_plannerContext.ScopeTree;
			foreach (AggregateSubqueryTable aggregateSubqueryTable in this.m_subqueryInputTables)
			{
				if (scopeTree.AreSameScope(aggregateSubqueryTable.OutputRowScope, rowScope) && scopeTree.AreSameScope(aggregateSubqueryTable.StopScope, stopScope) && aggregateSubqueryTable.ExpressionId.Equals(expressionId))
				{
					result = aggregateSubqueryTable;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0003DE20 File Offset: 0x0003C020
		internal bool TryGetSubqueryTableColumnReference(IAggregateInputTable table, out ExpressionNode newNode)
		{
			AggregateSubqueryTable aggregateSubqueryTable = table as AggregateSubqueryTable;
			if (aggregateSubqueryTable != null)
			{
				newNode = new BatchColumnReferenceExpressionNode(aggregateSubqueryTable.ExpressionPlanName);
				return true;
			}
			newNode = null;
			return false;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0003DE4C File Offset: 0x0003C04C
		internal NamingContext GetOrCreateNamingContext(IScope outputScope)
		{
			NamingContext namingContext;
			if (!this.m_outputScopeToNamingContextMapping.TryGetValue(outputScope, out namingContext))
			{
				namingContext = new NamingContext(null);
				this.m_outputScopeToNamingContextMapping.Add(outputScope, namingContext);
			}
			return namingContext;
		}

		// Token: 0x04000721 RID: 1825
		private readonly IAggregatesPlanningContext m_plannerContext;

		// Token: 0x04000722 RID: 1826
		private readonly RowScopesMetadata m_joinPredicatesRowScopes;

		// Token: 0x04000723 RID: 1827
		private readonly List<AggregateGroupByTable> m_aggregateTables;

		// Token: 0x04000724 RID: 1828
		private readonly List<AggregateReferenceTable> m_referenceTables;

		// Token: 0x04000725 RID: 1829
		private readonly List<AggregateSubqueryTable> m_subqueryInputTables;

		// Token: 0x04000726 RID: 1830
		private readonly Dictionary<ExpressionNode, Expression> m_subExpressionMapping;

		// Token: 0x04000727 RID: 1831
		private readonly Dictionary<IScope, NamingContext> m_outputScopeToNamingContextMapping;

		// Token: 0x04000728 RID: 1832
		private readonly bool m_omitSubtotalIndicatorColumnsInGroups;

		// Token: 0x02000302 RID: 770
		private struct RequiredShowAllState
		{
			// Token: 0x0600170E RID: 5902 RVA: 0x00052551 File Offset: 0x00050751
			internal RequiredShowAllState(IReadOnlyList<DataMember> groupByTableState, IReadOnlyList<DataMember> subqueryTableState)
			{
				this.GroupByTableState = groupByTableState;
				this.SubqueryTableState = subqueryTableState;
			}

			// Token: 0x04000B1C RID: 2844
			internal static readonly AggregateGroupByTableManager.RequiredShowAllState Empty = new AggregateGroupByTableManager.RequiredShowAllState(null, null);

			// Token: 0x04000B1D RID: 2845
			internal IReadOnlyList<DataMember> GroupByTableState;

			// Token: 0x04000B1E RID: 2846
			internal IReadOnlyList<DataMember> SubqueryTableState;
		}
	}
}
