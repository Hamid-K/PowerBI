using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001B9 RID: 441
	internal sealed class AggregatesTableGroupByStrategy : AggregatesTableStrategy
	{
		// Token: 0x06000F73 RID: 3955 RVA: 0x0003E928 File Offset: 0x0003CB28
		internal AggregatesTableGroupByStrategy(IAggregatesPlanningContext plannerContext, DataShapeContext dsContext, PlanDeclarationCollection declarations, IReadOnlyList<TableReference> referenceTables, IReadOnlyList<PlanOperation> contextTables, WritableExpressionTable outputExpressionTable, RowScopesMetadata joinPredicatesRowScopes, IReadOnlyList<Calculation> aggregateCalculations, IScope containingScope, AggregatesTableJoiner aggregatesTableJoiner)
			: base(plannerContext, dsContext, contextTables)
		{
			this.m_declarations = declarations;
			this.m_referenceTables = referenceTables;
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_joinPredicatesRowScopes = joinPredicatesRowScopes;
			this.m_aggregateCalculations = aggregateCalculations;
			this.m_containingScope = containingScope;
			this.m_aggregateScopeIsDataShape = this.m_containingScope == dsContext.DataShape;
			this.m_aggregatesTableJoiner = aggregatesTableJoiner;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0003E98C File Offset: 0x0003CB8C
		internal override PlanOperationContext ToTableContext()
		{
			if (this.m_aggregateCalculations.Count == 0)
			{
				return null;
			}
			Contract.RetailAssert(!this.m_dsContext.HasComplexSlicer, "Aggregate measures are not allowed with complex slicers.");
			IEnumerable<AggregateGroupByTable> enumerable = this.RewriteAggregatesIntoTables(this.m_aggregateCalculations);
			IList<AggregateGroupByTable> list = this.FindAggregateTablesForContainingScope(enumerable).Evaluate<AggregateGroupByTable>();
			if (list.Count == 0)
			{
				return new AggregatesTableSingleRowOnlyStrategy(this.m_plannerContext, this.m_dsContext, this.m_contextTables, this.m_aggregateCalculations, this.m_containingScope).ToTableContext();
			}
			IEnumerable<AggregateGroupByTable> enumerable2 = this.OptimizeAggregateTables(list);
			List<PlanOperation> list2 = this.TranslateToPlanOperations(enumerable2).ToList<PlanOperation>();
			PlanOperation planOperation = this.CreateTableForMeasures(this.m_aggregateCalculations);
			if (planOperation != null)
			{
				list2.Insert(0, planOperation);
			}
			return this.m_aggregatesTableJoiner.JoinAggregateTables(list2, this.m_aggregateCalculations);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0003EA4D File Offset: 0x0003CC4D
		private IEnumerable<PlanOperation> TranslateToPlanOperations(IEnumerable<AggregateGroupByTable> optimizedTables)
		{
			return optimizedTables.Select((AggregateGroupByTable table) => table.ToPlanOperation(this.m_plannerContext.Annotations, this.m_plannerContext.ScopeTree));
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003EA64 File Offset: 0x0003CC64
		private IEnumerable<AggregateGroupByTable> RewriteAggregatesIntoTables(IReadOnlyList<Calculation> aggregateCalcs)
		{
			AggregateGroupByTableManager aggregateGroupByTableManager = new AggregateGroupByTableManager(this.m_plannerContext, this.m_joinPredicatesRowScopes, true);
			foreach (TableReference tableReference in this.m_referenceTables)
			{
				aggregateGroupByTableManager.AddReferenceTable(tableReference);
			}
			foreach (Calculation calculation in aggregateCalcs)
			{
				ExpressionContext expressionContext = new ExpressionContext(this.m_plannerContext.ErrorContext, calculation.ObjectType, calculation.Id, "Value");
				foreach (ExpressionId expressionId in this.m_plannerContext.CalculationMap.GetExpressions(calculation))
				{
					GroupByAggregateExpressionRewriter.Rewrite(expressionId, this.m_containingScope, this.m_outputExpressionTable, this.m_plannerContext, aggregateGroupByTableManager, expressionContext, this.m_declarations, !this.m_aggregateScopeIsDataShape, calculation.RespectInstanceFilters.GetValueOrDefault());
				}
			}
			return aggregateGroupByTableManager.Tables;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0003EBA4 File Offset: 0x0003CDA4
		private PlanOperation CreateTableForMeasures(IReadOnlyList<Calculation> aggregateCalcs)
		{
			ExistsFilterCondition existsFilter = this.m_plannerContext.Annotations.GetDataShapeAnnotation(this.m_dsContext.DataShape).ExistsFilter;
			bool flag = existsFilter != null;
			bool flag2 = this.m_contextTables.Count > 0;
			NamingContext namingContext = new NamingContext(null);
			AggregatesTableModelExpressionRewriter aggregatesTableModelExpressionRewriter = new AggregatesTableModelExpressionRewriter(this.m_plannerContext.ErrorContext, this.m_outputExpressionTable, namingContext);
			foreach (Calculation calculation in aggregateCalcs)
			{
				if (flag || flag2)
				{
					foreach (ExpressionId expressionId in this.m_plannerContext.CalculationMap.GetExpressions(calculation))
					{
						aggregatesTableModelExpressionRewriter.Rewrite(calculation, expressionId);
					}
				}
			}
			List<SingleRowAdditionalColumn> columns = aggregatesTableModelExpressionRewriter.Columns;
			if (columns == null || columns.Count == 0)
			{
				return null;
			}
			return PlanOperationBuilder.SingleRow(Enumerable.Empty<Calculation>(), this.m_contextTables, (existsFilter != null) ? existsFilter.Items : null, columns);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0003ECD0 File Offset: 0x0003CED0
		private IEnumerable<AggregateGroupByTable> FindAggregateTablesForContainingScope(IEnumerable<AggregateGroupByTable> tables)
		{
			return tables.Where((AggregateGroupByTable t) => this.m_plannerContext.ScopeTree.AreSameScope(this.m_containingScope, t.OutputRowScope));
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003ECE4 File Offset: 0x0003CEE4
		private IEnumerable<AggregateGroupByTable> OptimizeAggregateTables(IEnumerable<AggregateGroupByTable> tables)
		{
			List<AggregateGroupByTable> list = new List<AggregateGroupByTable>();
			foreach (AggregateGroupByTable aggregateGroupByTable in tables)
			{
				list.Add(this.FlattenRedundantAggregationLevels(aggregateGroupByTable));
			}
			return this.MergeEquivalentTables(list);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003ED40 File Offset: 0x0003CF40
		private IEnumerable<AggregateGroupByTable> MergeEquivalentTables(List<AggregateGroupByTable> resultTables)
		{
			ScopeTree scopeTree = this.m_plannerContext.ScopeTree;
			for (int i = 0; i < resultTables.Count; i++)
			{
				for (int j = i + 1; j < resultTables.Count; j++)
				{
					AggregateGroupByTable aggregateGroupByTable = resultTables[i];
					AggregateGroupByTable aggregateGroupByTable2 = resultTables[j];
					if (aggregateGroupByTable != null && aggregateGroupByTable2 != null && aggregateGroupByTable.Input == aggregateGroupByTable2.Input && scopeTree.AreSameScope(aggregateGroupByTable.OutputRowScope, aggregateGroupByTable2.OutputRowScope))
					{
						AggregateGroupByTable aggregateGroupByTable3 = new AggregateGroupByTable(aggregateGroupByTable.Input, aggregateGroupByTable.OutputRowScope, aggregateGroupByTable.ReferencedDetails.Concat(aggregateGroupByTable2.ReferencedDetails), aggregateGroupByTable.Aggregates.Concat(aggregateGroupByTable2.Aggregates), new NamingContext(null), true);
						resultTables[i] = aggregateGroupByTable3;
						resultTables[j] = null;
					}
				}
			}
			return resultTables.WhereNonNull<AggregateGroupByTable>();
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003EE1C File Offset: 0x0003D01C
		private AggregateGroupByTable FlattenRedundantAggregationLevels(AggregateGroupByTable table)
		{
			AggregateGroupByTable aggregateGroupByTable = table.Input as AggregateGroupByTable;
			if (aggregateGroupByTable == null)
			{
				return table;
			}
			if (aggregateGroupByTable.HasAggregates)
			{
				return table;
			}
			foreach (PlanAggregateExpressionItem planAggregateExpressionItem in table.Aggregates)
			{
				FunctionCallExpressionNode functionCallExpressionNode = this.m_outputExpressionTable.GetNode(planAggregateExpressionItem.ExpressionId) as FunctionCallExpressionNode;
				if (functionCallExpressionNode == null || !ExpressionAnalysisUtils.IsAggregateThatIgnoresInputCardinality(functionCallExpressionNode))
				{
					return table;
				}
			}
			return new AggregateGroupByTable(aggregateGroupByTable.Input, table.OutputRowScope, table.ReferencedDetails, table.Aggregates, new NamingContext(null), true);
		}

		// Token: 0x04000743 RID: 1859
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x04000744 RID: 1860
		private readonly IReadOnlyList<TableReference> m_referenceTables;

		// Token: 0x04000745 RID: 1861
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x04000746 RID: 1862
		private readonly RowScopesMetadata m_joinPredicatesRowScopes;

		// Token: 0x04000747 RID: 1863
		private readonly IScope m_containingScope;

		// Token: 0x04000748 RID: 1864
		private readonly IReadOnlyList<Calculation> m_aggregateCalculations;

		// Token: 0x04000749 RID: 1865
		private readonly bool m_aggregateScopeIsDataShape;

		// Token: 0x0400074A RID: 1866
		private readonly AggregatesTableJoiner m_aggregatesTableJoiner;
	}
}
