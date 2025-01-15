using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x020001A8 RID: 424
	internal sealed class SubqueryPlanOperationGenerator
	{
		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003C72C File Offset: 0x0003A92C
		internal SubqueryPlanOperationGenerator(ISubqueryPlanOperationGeneratorContext context, PlanDeclarationCollection declarations, IPlanOperationTreeGenerator planOpTreeGenerator)
		{
			this.m_annotations = context.Annotations;
			this.m_scopeTree = context.ScopeTree;
			this.m_transformReferenceMap = context.TransformReferenceMap;
			this.m_outputExpressionTable = context.OutputExpressionTable;
			this.m_errorContext = context.ErrorContext;
			this.m_calculationMap = context.CalculationMap;
			this.m_declarations = declarations;
			this.m_planOpGenerator = planOpTreeGenerator;
			this.m_applyTransformsInQuery = context.ApplyTransformsInQuery;
			this.m_subqueryDataShapeCache = new SubqueryDataShapeTableCache();
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003C7AC File Offset: 0x0003A9AC
		internal bool ShouldPerformSubqueryRegrouping(DataShapeContext dsContext)
		{
			IReadOnlyList<DataShape> inputSubqueryDataShapes = dsContext.InputSubqueryDataShapes;
			if (inputSubqueryDataShapes.IsNullOrEmpty<DataShape>())
			{
				return false;
			}
			DataShapeContext nestedDataShapeContext = dsContext.GetNestedDataShapeContext(inputSubqueryDataShapes.Single("Expected only one subqueryDataShape", Array.Empty<string>()));
			bool flag = nestedDataShapeContext.HasAnyPrimaryDynamic || nestedDataShapeContext.HasAnySecondaryDynamic;
			bool flag2 = dsContext.HasAnyPrimaryDynamic || dsContext.HasAnySecondaryDynamic;
			bool flag3 = !dsContext.DataShape.Transforms.IsNullOrEmpty<DataTransform>() && this.m_applyTransformsInQuery;
			return (flag && flag2) || flag3;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0003C824 File Offset: 0x0003AA24
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "DataShapeContext", "PlanOperationContext" })]
		internal IReadOnlyList<global::System.ValueTuple<DataShapeContext, PlanOperationContext>> GenerateSubqueryTables(DataShapeContext dsContext)
		{
			IReadOnlyList<DataShapeContext> subqueryDataShapeContexts = SubqueryPlanOperationGenerator.GetSubqueryDataShapeContexts(dsContext, dsContext.InputSubqueryDataShapes);
			List<global::System.ValueTuple<DataShapeContext, PlanOperationContext>> list = new List<global::System.ValueTuple<DataShapeContext, PlanOperationContext>>(subqueryDataShapeContexts.Count);
			int count = subqueryDataShapeContexts.Count;
			for (int i = 0; i < count; i++)
			{
				DataShapeContext dataShapeContext = subqueryDataShapeContexts[i];
				PlanOperationContext planOperationContext = this.GenerateSubqueryTableForDataShape(dataShapeContext, dsContext, false);
				list.Add(new global::System.ValueTuple<DataShapeContext, PlanOperationContext>(dataShapeContext, planOperationContext));
			}
			return list;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0003C884 File Offset: 0x0003AA84
		internal PlanOperationContext GetOrCreateSubqueryTable(DataShapeContext subqueryDsContext, bool suppressCoreTableUnconstrainedJoinCheck)
		{
			PlanOperationContext planOperationContext;
			if (this.m_subqueryDataShapeCache.TryGetTable(subqueryDsContext.DataShape, out planOperationContext))
			{
				return planOperationContext;
			}
			BatchPlanningStrategy batchPlanningStrategy = subqueryDsContext.DeterminePlanningStrategy();
			PlanOperationContext planOperationContext2 = this.m_planOpGenerator.GenerateTables(subqueryDsContext, true, suppressCoreTableUnconstrainedJoinCheck, batchPlanningStrategy).OutputTables.OutputTables.Single("Only 1 table is expected for a subquery PlanOperationTree", Array.Empty<string>()).TableContext.DeclareIfNotDeclared(PlanNames.SubqueryTable(subqueryDsContext.DataShape.Id.Value), this.m_declarations, false, null, false);
			this.m_subqueryDataShapeCache.Add(subqueryDsContext.DataShape, planOperationContext2);
			return planOperationContext2;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0003C914 File Offset: 0x0003AB14
		private PlanOperationContext GenerateSubqueryTableForDataShape(DataShapeContext subqueryDsContext, DataShapeContext referringDsContext, bool suppressCoreTableUnconstrainedJoinCheck)
		{
			PlanOperationContext planOperationContext = this.GetOrCreateSubqueryTable(subqueryDsContext, suppressCoreTableUnconstrainedJoinCheck);
			FilterCondition filterCondition = BatchDataSetPlanningFilterUtils.CollectSubqueryOutputFilters(referringDsContext.DataShape.Filters);
			DataShape dataShape = null;
			if (filterCondition != null)
			{
				dataShape = SubqueryReferenceFilterAnalyzer.Analyze(filterCondition, referringDsContext.ScopeTree, this.m_outputExpressionTable, referringDsContext.DataShape);
			}
			PlanOperation planOperation = planOperationContext.Table;
			if (dataShape != null && dataShape.Equals(subqueryDsContext.DataShape))
			{
				planOperation = SubqueryFilterTableBuilder.ApplySubqueryFilter(filterCondition, planOperation);
				PlanOperationFilteringMetadata planOperationFilteringMetadata = new PlanOperationFilteringMetadata(planOperationContext.Totals.ToTotalsMetadata(), false);
				planOperation = planOperation.DeclareIfNotDeclared(PlanNames.SubqueryTableIn(subqueryDsContext.DataShape.Id.Value, referringDsContext.DataShape.Id.Value), this.m_declarations, false, false, null, false);
				planOperationContext = planOperationContext.ReplaceTable(planOperation, null, planOperationFilteringMetadata, null);
			}
			return planOperationContext;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0003C9D0 File Offset: 0x0003ABD0
		internal PlanOperationContext CreateCoreTableFromSubquery(PlanOperationContext subqueryTableContext, string subqueryDataShapeId, DataShapeContext dsContext)
		{
			GroupByRegroupingItems groupByRegroupingItems = RegroupingItemsCollector.Collect(this.m_annotations, this.m_transformReferenceMap, dsContext);
			PlanOperation planOperation = subqueryTableContext.Table;
			planOperation = this.ProjectGroupingItems(dsContext, planOperation, groupByRegroupingItems.DetailCalculations, null, groupByRegroupingItems.DynamicDataMembers, groupByRegroupingItems.DataTransformTableColumns);
			subqueryTableContext = subqueryTableContext.ReplaceTable(planOperation, null, null, null);
			TableReference tableReference = new TableReference(subqueryTableContext, PlanNames.SubqueryTableProjectedIn(subqueryDataShapeId, dsContext.DataShape.Id.Value), this.m_declarations, RowResultSetType.Unrestricted);
			IReadOnlyList<Calculation> readOnlyList = Util.EmptyReadOnlyList<Calculation>();
			PlanOperation planOperation2;
			if (!groupByRegroupingItems.DataTransformTableColumns.IsNullOrEmpty<DataTransformTableColumn>())
			{
				IEnumerable<PlanGroupByDataTransformColumn> enumerable = groupByRegroupingItems.DataTransformTableColumns.Select((DataTransformTableColumn column) => new PlanGroupByDataTransformColumn(column));
				planOperation2 = planOperation.GroupBy(enumerable);
			}
			else
			{
				readOnlyList = groupByRegroupingItems.AggregateCalculations;
				planOperation2 = this.GenerateAggregateGroupByTable(dsContext, tableReference, readOnlyList, groupByRegroupingItems.AggregateSortKeys, groupByRegroupingItems.InnermostScope);
			}
			return new PlanOperationContext(planOperation2, groupByRegroupingItems.InnermostScope.GetAllParentScopesWithoutTopDataShapes(this.m_scopeTree).ToReadOnlyList<IScope>(), readOnlyList.Concat(groupByRegroupingItems.DetailCalculations).ToList<Calculation>());
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0003CAE0 File Offset: 0x0003ACE0
		private PlanOperation GenerateAggregateGroupByTable(DataShapeContext dsContext, TableReference referenceTable, IReadOnlyList<Calculation> aggregateCalcs, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "SortKey", "SuggestedName" })] IReadOnlyList<global::System.ValueTuple<SortKey, string>> aggregateSortKeys, IScope innermostScope)
		{
			IAggregateInputTable aggregateInputTable = new AggregateReferenceTable(referenceTable);
			NamingContext namingContext = new NamingContext(null);
			AggregateGroupByTable aggregateGroupByTable = new AggregateGroupByTable(aggregateInputTable, innermostScope, namingContext, false);
			GroupByRegroupedAggregateExpressionRewriter.Rewrite(aggregateCalcs, aggregateSortKeys, this.m_outputExpressionTable, this.m_errorContext, this.m_calculationMap, aggregateGroupByTable);
			PlanOperation planOperation = aggregateGroupByTable.ToPlanOperation(this.m_annotations, this.m_scopeTree);
			return this.ProjectGroupingItems(dsContext, planOperation, aggregateCalcs, aggregateSortKeys, null, null);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003CB40 File Offset: 0x0003AD40
		private PlanOperation ProjectGroupingItems(DataShapeContext dsContext, PlanOperation inputTable, IReadOnlyList<Calculation> calculations, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "SortKey", "SuggestedName" })] IReadOnlyList<global::System.ValueTuple<SortKey, string>> aggregateSortKeys, IReadOnlyList<DataMember> members = null, IReadOnlyList<DataTransformTableColumn> dataTransformTableColumns = null)
		{
			List<PlanProjectItem> list = new List<PlanProjectItem>(calculations.Count);
			if (members != null)
			{
				foreach (DataMember dataMember in members)
				{
					list.Add(dataMember.ToDataMemberProjectItem(true));
				}
			}
			foreach (Calculation calculation in calculations)
			{
				list.Add(calculation.ToNewColumnProjectItem());
			}
			if (aggregateSortKeys != null)
			{
				foreach (global::System.ValueTuple<SortKey, string> valueTuple in aggregateSortKeys)
				{
					ExpressionContext expressionContext = new ExpressionContext(this.m_errorContext, valueTuple.Item1.ObjectType, valueTuple.Item2, "Value");
					list.Add(new PlanNewColumnProjectItem(valueTuple.Item1.Value.ExpressionId.Value, valueTuple.Item2, expressionContext, ColumnReuseKind.ByReference));
				}
			}
			if (!dataTransformTableColumns.IsNullOrEmpty<DataTransformTableColumn>())
			{
				foreach (DataTransformTableColumn dataTransformTableColumn in dataTransformTableColumns)
				{
					ExpressionContext expressionContext2 = new ExpressionContext(this.m_errorContext, ObjectType.DataTransformTableColumn, dataTransformTableColumn.Id.Value, "Column");
					list.Add(dataTransformTableColumn.ToNewColumnProjectItem(expressionContext2));
				}
			}
			list.Add(PlanPreserveAllColumnsProjectItem.Instance);
			return inputTable.Project(list, false);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003CCFC File Offset: 0x0003AEFC
		private static IReadOnlyList<DataShapeContext> GetSubqueryDataShapeContexts(DataShapeContext dsContext, IReadOnlyList<DataShape> subqueryDataShapes)
		{
			List<DataShapeContext> list = new List<DataShapeContext>(subqueryDataShapes.Count);
			foreach (DataShape dataShape in subqueryDataShapes)
			{
				list.Add(dsContext.GetNestedDataShapeContext(dataShape));
			}
			return list;
		}

		// Token: 0x04000703 RID: 1795
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x04000704 RID: 1796
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000705 RID: 1797
		private readonly DataTransformReferenceMap m_transformReferenceMap;

		// Token: 0x04000706 RID: 1798
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x04000707 RID: 1799
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000708 RID: 1800
		private readonly CalculationExpressionMap m_calculationMap;

		// Token: 0x04000709 RID: 1801
		private readonly PlanDeclarationCollection m_declarations;

		// Token: 0x0400070A RID: 1802
		private readonly IPlanOperationTreeGenerator m_planOpGenerator;

		// Token: 0x0400070B RID: 1803
		private readonly SubqueryDataShapeTableCache m_subqueryDataShapeCache;

		// Token: 0x0400070C RID: 1804
		private readonly bool m_applyTransformsInQuery;
	}
}
