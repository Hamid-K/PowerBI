using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001D1 RID: 465
	internal sealed class HierarchyPlanOperationApplier
	{
		// Token: 0x06001049 RID: 4169 RVA: 0x000438E4 File Offset: 0x00041AE4
		internal HierarchyPlanOperationApplier(WritableExpressionTable outputExpressionTable, PlanDeclarationCollection declarations)
		{
			this.m_outputExpressionTable = outputExpressionTable;
			this.m_declarations = declarations;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x000438FC File Offset: 0x00041AFC
		internal PlanOperationContext ApplyPrimaryHierarchyPreLimitOperations(IShowAllTableBuilderContext showAllContext, ScopeTree scopeTree, BatchSortByMeasureExpressionMappings sortByMeasureExpressionMappings, DataShapeContext dsContext, InstanceFiltersContext instanceFiltersContext, ContextTableManager attributeFilterContextTableManager, PlanOperationContext coreTable, PlanOperationContext primaryTableWithTotals, AggregatesInputTableCollector aggregatesInputCollector, CoreTableArtifacts coreTableArtifacts)
		{
			PlanOperationContext planOperationContext = this.ApplyPrimaryShowAllOperations(showAllContext, dsContext, instanceFiltersContext, attributeFilterContextTableManager, coreTableArtifacts.ContextTables, primaryTableWithTotals, coreTableArtifacts.JoinPredicates, coreTableArtifacts.AttributeFilters, coreTableArtifacts.CoreTableFragments, coreTable.RowScopes);
			if (planOperationContext != primaryTableWithTotals)
			{
				aggregatesInputCollector.RegisterHierarchyShowAllNoInstanceFiltersTable(dsContext, planOperationContext, true);
			}
			if (instanceFiltersContext.QueryStageForInstanceFilters == QueryStageForInstanceFilters.PostShowAll)
			{
				planOperationContext = BatchDataSetPlanningFilterUtils.ApplyInstanceFiltersAsPostFilter(this.m_outputExpressionTable, showAllContext.ErrorContext, showAllContext.Annotations, planOperationContext, null);
				planOperationContext = planOperationContext.DeclareIfNotDeclared(PlanNames.PrimaryShowAllInstanceFilters(dsContext.Id), this.m_declarations, false, null, false);
			}
			return this.ApplyPrimaryHierarchySortByMeasureOperations(showAllContext, scopeTree, sortByMeasureExpressionMappings, dsContext, this.m_declarations, coreTable, planOperationContext);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x000439A4 File Offset: 0x00041BA4
		internal PlanOperationContext ApplySecondaryHierarchyPreLimitOperations(IShowAllTableBuilderContext showAllContext, DataShapeContext dsContext, InstanceFiltersContext instanceFiltersContext, ContextTableManager attributeFilterContextTableManager, PlanOperationContext secondary, AggregatesInputTableCollector aggregatesInputCollector, CoreTableArtifacts coreTableArtifacts, RowScopesMetadata coreTableRowScopes)
		{
			if (dsContext.HasAnySecondaryDynamic)
			{
				PlanOperationContext planOperationContext = this.ApplySecondaryShowAllOperations(showAllContext, dsContext, instanceFiltersContext, attributeFilterContextTableManager, coreTableArtifacts.ContextTables, secondary, coreTableArtifacts.JoinPredicates, coreTableArtifacts.AttributeFilters, coreTableArtifacts.CoreTableFragments, coreTableRowScopes);
				if (secondary != planOperationContext)
				{
					aggregatesInputCollector.RegisterHierarchyShowAllNoInstanceFiltersTable(dsContext, planOperationContext, false);
				}
				if (instanceFiltersContext.QueryStageForInstanceFilters == QueryStageForInstanceFilters.PostShowAll)
				{
					planOperationContext = BatchDataSetPlanningFilterUtils.ApplyInstanceFiltersAsPostFilter(showAllContext.OutputExpressionTable, showAllContext.ErrorContext, showAllContext.Annotations, planOperationContext, instanceFiltersContext.InstanceFiltersRequiringPostFiltering);
					planOperationContext = planOperationContext.DeclareIfNotDeclared(PlanNames.SecondaryShowAllInstanceFilters(dsContext.Id), this.m_declarations, false, null, false);
				}
				secondary = planOperationContext;
			}
			return secondary;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00043A40 File Offset: 0x00041C40
		private PlanOperationContext ApplyPrimaryShowAllOperations(IShowAllTableBuilderContext showAllContext, DataShapeContext dsContext, InstanceFiltersContext instanceFiltersContext, ContextTableManager attributeFilterContextTableManager, IReadOnlyList<PlanOperation> contextTables, PlanOperationContext primaryTableWithTotals, BatchDataSetPlannerJoinPredicates joinPredicates, IReadOnlyList<FilterCondition> attributeFilters, IReadOnlyList<PlanOperationContext> coreTableFragments, RowScopesMetadata coreTableRowScopes)
		{
			if (!dsContext.HasAnyPrimaryDynamic)
			{
				return primaryTableWithTotals;
			}
			IReadOnlyList<DataMember> primaryDynamics = dsContext.PrimaryDynamics;
			IEnumerable<DataMember> enumerable = primaryDynamics.Where((DataMember d) => d.UsesShowItemsWithNoData());
			if (enumerable.IsNullOrEmpty<DataMember>())
			{
				return primaryTableWithTotals;
			}
			PlanOperationContext planOperationContext = ShowAllTableBuilder.Build(showAllContext, contextTables, this.m_declarations, primaryTableWithTotals, primaryDynamics, enumerable.ToList<DataMember>(), joinPredicates, PlanNames.Primary(dsContext.Id), attributeFilters, attributeFilterContextTableManager, instanceFiltersContext, !dsContext.DataShape.HasFilterEmptyGroups(), SubtotalUsage.SortByMeasure);
			if (BatchDataSetPlanningUtils.DetermineScopedMeasureShowAllRestorationMode(dsContext) == ScopedMeasureShowAllRestorationMode.PreAggregates)
			{
				planOperationContext = BatchDataSetPlanningUtils.RestoreScopedMeasureShowAllValues(dsContext, coreTableFragments, planOperationContext, this.m_declarations, coreTableRowScopes);
			}
			return planOperationContext.DeclareIfNotDeclared(PlanNames.PrimaryShowAll(dsContext.Id), this.m_declarations, false, null, false);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00043B00 File Offset: 0x00041D00
		private PlanOperationContext ApplySecondaryShowAllOperations(IShowAllTableBuilderContext showAllContext, DataShapeContext dsContext, InstanceFiltersContext instanceFiltersContext, ContextTableManager attributeFilterContextTableManager, IReadOnlyList<PlanOperation> contextTables, PlanOperationContext secondaryTable, BatchDataSetPlannerJoinPredicates joinPredicates, IReadOnlyList<FilterCondition> attributeFilters, IReadOnlyList<PlanOperationContext> coreTableFragments, RowScopesMetadata coreTableRowScopes)
		{
			if (!dsContext.HasAnySecondaryDynamic)
			{
				return secondaryTable;
			}
			IReadOnlyList<DataMember> secondaryDynamics = dsContext.SecondaryDynamics;
			IEnumerable<DataMember> enumerable = secondaryDynamics.Where((DataMember d) => d.UsesShowItemsWithNoData());
			if (enumerable.IsNullOrEmpty<DataMember>())
			{
				return secondaryTable;
			}
			PlanOperationContext planOperationContext = ShowAllTableBuilder.Build(showAllContext, contextTables, this.m_declarations, secondaryTable, secondaryDynamics, enumerable.ToList<DataMember>(), joinPredicates, PlanNames.Secondary(dsContext.Id), attributeFilters, attributeFilterContextTableManager, instanceFiltersContext, !dsContext.DataShape.HasFilterEmptyGroups(), SubtotalUsage.Output);
			if (BatchDataSetPlanningUtils.DetermineScopedMeasureShowAllRestorationMode(dsContext) == ScopedMeasureShowAllRestorationMode.PreAggregates)
			{
				planOperationContext = BatchDataSetPlanningUtils.RestoreScopedMeasureShowAllValues(dsContext, coreTableFragments, planOperationContext, this.m_declarations, coreTableRowScopes);
			}
			return planOperationContext.DeclareIfNotDeclared(PlanNames.SecondaryShowAll(dsContext.DataShape.Id), this.m_declarations, false, null, false);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00043BC4 File Offset: 0x00041DC4
		private PlanOperationContext ApplyPrimaryHierarchySortByMeasureOperations(IShowAllTableBuilderContext showAllContext, ScopeTree scopeTree, BatchSortByMeasureExpressionMappings sortByMeasureExpressionMappings, DataShapeContext dsContext, PlanDeclarationCollection declarations, PlanOperationContext coreTable, PlanOperationContext primaryTable)
		{
			if (dsContext.HasAnyPrimaryDynamic)
			{
				SortByMeasureTranslator.Context context = new SortByMeasureTranslator.Context(showAllContext.Annotations, this.m_outputExpressionTable, showAllContext.ErrorContext, sortByMeasureExpressionMappings, declarations, scopeTree, coreTable.Table);
				PlanOperation planOperation = SortByMeasureTranslator.ApplySortByMeasureTransforms(dsContext, context, primaryTable, true);
				primaryTable = primaryTable.ReplaceTable(planOperation, null, null, null);
			}
			return primaryTable;
		}

		// Token: 0x0400079F RID: 1951
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x040007A0 RID: 1952
		private readonly PlanDeclarationCollection m_declarations;
	}
}
