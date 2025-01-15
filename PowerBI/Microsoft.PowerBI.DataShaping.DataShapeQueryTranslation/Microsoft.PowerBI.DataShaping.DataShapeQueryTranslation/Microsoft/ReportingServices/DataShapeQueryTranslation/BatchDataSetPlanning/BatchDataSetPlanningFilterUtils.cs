using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200016A RID: 362
	internal static class BatchDataSetPlanningFilterUtils
	{
		// Token: 0x06000D16 RID: 3350 RVA: 0x00035FC4 File Offset: 0x000341C4
		public static PlanOperation ApplyInstanceFiltersAsPostFilter(WritableExpressionTable expressionTable, TranslationErrorContext errorContext, DataShapeAnnotations annotations, BatchGroupAndJoinBuilder scopedTableBuilder, PlanOperation plannedTable, HashSet<FilterCondition> instanceFiltersRequiringPostProcessing)
		{
			List<PlanProjectItem> list;
			FilterCondition filterCondition = InstanceFiltersBuilder.BuildInstanceFilters(scopedTableBuilder.PrimaryGroupingBucket.Select((PlanGroupByMember gb) => gb.Member).ToReadOnlyCollection<DataMember>(), expressionTable, errorContext, annotations, instanceFiltersRequiringPostProcessing, out list);
			if (filterCondition == null)
			{
				return plannedTable;
			}
			return BatchDataSetPlanningFilterUtils.ApplyInstanceFilterAndProjections(plannedTable, filterCondition, list);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0003601C File Offset: 0x0003421C
		public static PlanOperationContext ApplyInstanceFiltersAsPostFilter(WritableExpressionTable expressionTable, TranslationErrorContext errorContext, DataShapeAnnotations annotations, PlanOperationContext inputTable, HashSet<FilterCondition> instanceFiltersRequiringPostProcessing)
		{
			List<DataMember> list = (from g in inputTable.GetAllGroups()
				where annotations.IsPrimaryMember(g)
				select g).ToList<DataMember>();
			return BatchDataSetPlanningFilterUtils.ApplyInstanceFiltersAsPostFilter(expressionTable, errorContext, annotations, inputTable, list, instanceFiltersRequiringPostProcessing);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00036064 File Offset: 0x00034264
		internal static PlanOperationContext ApplyInstanceFiltersAsPostFilter(WritableExpressionTable expressionTable, TranslationErrorContext errorContext, DataShapeAnnotations annotations, PlanOperationContext inputTable, IReadOnlyList<DataMember> groups, HashSet<FilterCondition> instanceFiltersRequiringPostFiltering)
		{
			if (groups.Count == 0)
			{
				return inputTable;
			}
			List<PlanProjectItem> list;
			FilterCondition filterCondition = InstanceFiltersBuilder.BuildInstanceFilters(groups, expressionTable, errorContext, annotations, instanceFiltersRequiringPostFiltering, out list);
			if (filterCondition == null)
			{
				return inputTable;
			}
			PlanOperation planOperation = BatchDataSetPlanningFilterUtils.ApplyInstanceFilterAndProjections(inputTable.Table, filterCondition, list);
			return inputTable.ReplaceTable(planOperation, null, null, null);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000360A8 File Offset: 0x000342A8
		private static PlanOperation ApplyInstanceFilterAndProjections(PlanOperation plannedTable, FilterCondition filter, List<PlanProjectItem> projections)
		{
			projections.Add(PlanPreserveAllColumnsProjectItem.Instance);
			return plannedTable.Project(projections, false).FilterBy(filter);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x000360C4 File Offset: 0x000342C4
		public static IList<PlanProjectItem> GetProjectionForCalcs(IReadOnlyList<Calculation> calcs)
		{
			List<PlanProjectItem> list = new List<PlanProjectItem>(calcs.Count);
			list.Add(PlanOperationBuilder.ToAllColumnsProjectItem());
			for (int i = 0; i < calcs.Count; i++)
			{
				list.Add(calcs[i].ToNewColumnProjectItem());
			}
			return list;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0003610C File Offset: 0x0003430C
		public static bool ShouldApplyValueFilterAsContextTable(IValueFilterPlanningContext plannerContext, BatchGroupAndJoinBuilder joinBuilder, IScope filterTargetScope)
		{
			if (!BatchDataSetPlanningUtils.AreEquivalentScopes(filterTargetScope, joinBuilder.InnermostScope, plannerContext.ScopeTree))
			{
				return true;
			}
			if (joinBuilder.GroupByMembers.Any((PlanGroupByMember m) => m.RequiresRollupGroup || m.Member.UsesShowItemsWithNoData()))
			{
				return true;
			}
			return joinBuilder.HasExpressionFeature(plannerContext.OutputExpressionTable, plannerContext.CalculationMap, (ExpressionNode node) => MeasureAnalyzer.IsModelMeasure(node) || EvaluateScopeAnalyzer.UsesEvaluateWithScope(node));
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00036194 File Offset: 0x00034394
		public static PlanOperation CreateValueFilterTable(IValueFilterPlanningContext plannerContext, BatchGroupAndJoinBuilder joinBuilder, PlanDeclarationCollection declarations, Filter valueFilter, IScope innermostScope, bool hasShowAll)
		{
			IEnumerable<Calculation> enumerable = joinBuilder.Calculations.Where((Calculation c) => plannerContext.Annotations.IsMeasure(c));
			IScope resolvedScope = valueFilter.Target.GetResolvedScope(plannerContext.OutputExpressionTable);
			return ValueFilterTableBuilder.BuildValueFilterTable(plannerContext, valueFilter, joinBuilder.ContextTables, enumerable, joinBuilder.MeasureTransformColumns, joinBuilder.AdditionalColumns, declarations, innermostScope, hasShowAll).DeclareIfNotDeclared(PlanNames.ValueFilter(resolvedScope.Id), declarations, false, false, null, false);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00036216 File Offset: 0x00034416
		public static PlanOperation CreateExistsFilterTable(BatchGroupAndJoinBuilder joinBuilder, ExistsFilterCondition existsFilter, IEnumerable<PlanOperation> contextTables, string suggestedTableName, PlanDeclarationCollection declarations, PlanGroupAndJoinPredicateBehavior predicateBehavior)
		{
			return ExistsFilterTableBuilder.BuildExistsFilterTable(joinBuilder.PrimaryGroupingBucket, joinBuilder.SecondaryGroupingBucket, joinBuilder.GroupingTransformColumns, existsFilter, contextTables, predicateBehavior).DeclareIfNotDeclared(PlanNames.ExistsFilter(suggestedTableName), declarations, false, false, null, false);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00036244 File Offset: 0x00034444
		public static PlanOperation ApplyExistsFilter(BatchGroupAndJoinBuilder joinBuilder, ExistsFilterCondition existsFilter, IEnumerable<PlanOperation> contextTables, string suggestedTableName, PlanDeclarationCollection declarations, PlanGroupAndJoinPredicateBehavior predicateBehavior)
		{
			if (existsFilter == null)
			{
				return null;
			}
			PlanOperation planOperation = BatchDataSetPlanningFilterUtils.CreateExistsFilterTable(joinBuilder, existsFilter, contextTables, suggestedTableName, declarations, predicateBehavior);
			joinBuilder.AddContextTable(planOperation);
			joinBuilder.AddExistsFilters(existsFilter);
			return planOperation;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00036274 File Offset: 0x00034474
		internal static QueryStageForInstanceFilters DetermineQueryStageForInstanceFilters(DataShapeContext dsContext, IFederatedConceptualSchema schema, IFeatureSwitchProvider featureSwitches)
		{
			if (!dsContext.HasInstanceFilters)
			{
				return QueryStageForInstanceFilters.None;
			}
			if (dsContext.HasAnyVisualCalculations)
			{
				return QueryStageForInstanceFilters.PostCoreTableAndInShowAllPostFilter;
			}
			bool flag;
			if (flag = schema.GetDefaultSchemaDaxCapabilitiesAnnotation().SupportsRollupContextTables())
			{
				if (dsContext.HasPrimaryInstanceFilters && !dsContext.HasAllPrimaryOutputTotals)
				{
					flag = false;
					DataMember topLevelDynamic = dsContext.FirstPrimaryDynamic;
					if (topLevelDynamic.InstanceFilters.IsNullOrEmpty<FilterCondition>())
					{
						flag = dsContext.PrimaryDynamics.All((DataMember m) => m == topLevelDynamic || m.HasOutputTotal(dsContext.Annotations));
					}
				}
				flag = flag && (!dsContext.HasSecondaryInstanceFilters || dsContext.HasAllSecondaryOutputTotals);
			}
			IReadOnlyList<Calculation> dataShapeAggregatesOverScopes = dsContext.DataShapeAggregatesOverScopes;
			if (!dsContext.HasDataShapeAggregatesAndProjections || dataShapeAggregatesOverScopes.Count == 0)
			{
				if (flag)
				{
					return QueryStageForInstanceFilters.CoreTableAndShowAllRollupContextTables;
				}
				return QueryStageForInstanceFilters.CoreTableAndShowAllPostFilter;
			}
			else
			{
				if (!dsContext.HasShowItemsWithNoData)
				{
					return QueryStageForInstanceFilters.PostCoreTableAndInShowAllPostFilter;
				}
				DataMember dataMember = dsContext.InnermostScope as DataMember;
				bool flag2 = dataMember != null && dataMember.UsesShowItemsWithNoData();
				if (!BatchDataSetPlanningFilterUtils.AllCalculationsTargetOnlyInnermostScope(dataShapeAggregatesOverScopes, dsContext.InnermostScope, dsContext.ScopeTree, dsContext.Annotations) || flag2)
				{
					return QueryStageForInstanceFilters.PostShowAll;
				}
				if (flag)
				{
					return QueryStageForInstanceFilters.PostCoreTableAndInShowAllRollupContextTables;
				}
				return QueryStageForInstanceFilters.PostCoreTableAndInShowAllPostFilter;
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000363C4 File Offset: 0x000345C4
		private static bool AllCalculationsTargetOnlyInnermostScope(IReadOnlyList<Calculation> calculations, IScope innermostScope, ScopeTree scopeTree, DataShapeAnnotations annotations)
		{
			foreach (Calculation calculation in calculations)
			{
				IReadOnlyList<IScope> referencedScopes = annotations.GetReferencedScopes(calculation);
				if (referencedScopes.Count != 1 || !scopeTree.AreSameScope(referencedScopes[0], innermostScope))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00036430 File Offset: 0x00034630
		internal static IReadOnlyList<PlanOperation> GetInstanceFilterTables(DataMember dataMember, IFilterDeclarationCollection instanceFilterDeclarations)
		{
			if (dataMember.InstanceFilters.IsNullOrEmpty<FilterCondition>())
			{
				return null;
			}
			List<PlanOperation> list = new List<PlanOperation>(dataMember.InstanceFilters.Count);
			foreach (FilterCondition filterCondition in dataMember.InstanceFilters)
			{
				PlanOperationDeclarationReference planOperationDeclarationReference;
				if (instanceFilterDeclarations.TryGetFilterDeclaration(filterCondition, out planOperationDeclarationReference))
				{
					list.Add(planOperationDeclarationReference);
				}
			}
			return list;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000364B0 File Offset: 0x000346B0
		internal static FilterCondition CollectSubqueryOutputFilters(List<Filter> filters)
		{
			if (filters.IsNullOrEmpty<Filter>())
			{
				return null;
			}
			FilterCondition filterCondition = null;
			foreach (Filter filter in filters)
			{
				if (filter.UsageKind == FilterUsageKind.SubqueryOutput)
				{
					filterCondition = filter.Condition;
				}
			}
			return filterCondition;
		}
	}
}
