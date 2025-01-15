using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001C2 RID: 450
	internal sealed class CoreTableSimpleSlicerStrategy : CoreTableBuilderStrategy
	{
		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003FEC1 File Offset: 0x0003E0C1
		internal CoreTableSimpleSlicerStrategy(BatchDataSetPlannerContext plannerContext, DataShapeContext dsContext, PlanDeclarationCollection declarations, ContextTableManager attributeFilterContextTableManager, InstanceFiltersContext instanceFiltersContext, bool suppressUnconstrainedJoinCheck)
			: base(plannerContext, dsContext, attributeFilterContextTableManager, instanceFiltersContext, declarations, suppressUnconstrainedJoinCheck)
		{
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003FED4 File Offset: 0x0003E0D4
		internal override PlanOperationContext Build(out CoreTableArtifacts coreArtifacts)
		{
			IScope scope;
			IList<BatchGroupAndJoinBuilder> list = base.PopulateScopedTables(out scope);
			Contract.RetailAssert(list.Count >= 1, "There must be at least 1 table to create a core table.");
			IReadOnlyList<PlanOperationContext> readOnlyList;
			BatchDataSetPlannerJoinPredicates batchDataSetPlannerJoinPredicates;
			PlanOperationContext planOperationContext = this.CompleteCoreTable(list, scope, out readOnlyList, out batchDataSetPlannerJoinPredicates);
			ReadOnlyCollection<PlanOperation> readOnlyCollection = list[0].ContextTables.ToReadOnlyCollection<PlanOperation>();
			IReadOnlyList<FilterCondition> attributeFilters = list[0].AttributeFilters;
			coreArtifacts = new CoreTableArtifacts(readOnlyCollection, readOnlyList, batchDataSetPlannerJoinPredicates, attributeFilters);
			return planOperationContext;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003FF3C File Offset: 0x0003E13C
		private PlanOperationContext JoinScopedTables(IList<BatchGroupAndJoinBuilder> scopedTableBuilders, IList<PlanOperation> planOperationTables, out IReadOnlyList<PlanOperationContext> coreTableFragments, out BatchDataSetPlannerJoinPredicates joinPredicates)
		{
			Contract.RetailAssert(scopedTableBuilders.Count > 0, "There must be at least one scoped table to create a core table.");
			PlanOperationContext planOperationContext = null;
			List<PlanOperationContext> list = new List<PlanOperationContext>(scopedTableBuilders.Count);
			for (int i = 0; i < scopedTableBuilders.Count; i++)
			{
				PlanOperationContext planOperationContext2 = scopedTableBuilders[i].ToPlanOperationContext(this.m_plannerContext.ScopeTree, planOperationTables[i], null);
				if (scopedTableBuilders.Count > 1)
				{
					planOperationContext2 = planOperationContext2.DeclareIfNotDeclared(PlanNames.ScopedCore(planOperationContext2.RowScopes.InnermostScope.Id), this.m_declarations, false, null, false);
				}
				list.Add(planOperationContext2);
				if (planOperationContext != null)
				{
					planOperationContext = planOperationContext.LeftOuterJoin(planOperationContext2, this.m_plannerContext.ScopeTree);
				}
				else
				{
					planOperationContext = planOperationContext2;
				}
			}
			Contract.RetailAssert(planOperationContext != null, "Core table should not be null after joining scoped tables.");
			joinPredicates = new BatchDataSetPlannerJoinPredicates(scopedTableBuilders[0].CalculationsAsJoinPredicates, scopedTableBuilders[0].MeasureTransformColumns);
			coreTableFragments = list;
			return planOperationContext;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0004001C File Offset: 0x0003E21C
		private void ApplyExistsFilter(IList<BatchGroupAndJoinBuilder> scopedTableBuilders)
		{
			DataShape dataShape = this.m_dsContext.DataShape;
			DataShapeAnnotation dataShapeAnnotation = this.m_plannerContext.Annotations.GetDataShapeAnnotation(dataShape);
			ExistsFilterCondition existsFilter = dataShapeAnnotation.ExistsFilter;
			if (existsFilter == null)
			{
				return;
			}
			PlanOperation planOperation = null;
			BatchGroupAndJoinBuilder batchGroupAndJoinBuilder = scopedTableBuilders[0];
			bool flag = dataShapeAnnotation.ScopedValueFilters.Count == 0 && !this.m_dsContext.HasShowItemsWithNoData;
			for (int i = 0; i < scopedTableBuilders.Count; i++)
			{
				if (flag && !scopedTableBuilders[i].HasMeasures(this.m_plannerContext))
				{
					scopedTableBuilders[i].PredicateBehavior = PlanGroupAndJoinPredicateBehavior.ApplyAutoPredicates | PlanGroupAndJoinPredicateBehavior.ExistsPredicates;
				}
				else
				{
					if (planOperation == null)
					{
						planOperation = BatchDataSetPlanningFilterUtils.CreateExistsFilterTable(batchGroupAndJoinBuilder, existsFilter, batchGroupAndJoinBuilder.ContextTables, this.m_dsContext.DataShape.Id.Value, this.m_declarations, PlanGroupAndJoinPredicateBehavior.ExistsPredicates);
					}
					scopedTableBuilders[i].AddContextTable(planOperation);
				}
				scopedTableBuilders[i].AddExistsFilters(existsFilter);
			}
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0004010C File Offset: 0x0003E30C
		private PlanOperationContext CompleteCoreTable(IList<BatchGroupAndJoinBuilder> scopedTableBuilders, IScope innermostScope, out IReadOnlyList<PlanOperationContext> coreTableFragments, out BatchDataSetPlannerJoinPredicates joinPredicates)
		{
			this.SetupUniqueMeasureNamingBlock(scopedTableBuilders);
			this.ApplyExistsFilter(scopedTableBuilders);
			this.ApplyContextFilter(scopedTableBuilders);
			IList<PlanOperation> list = this.ApplyValueFilter(scopedTableBuilders, innermostScope);
			this.ApplyInstanceFiltersAsPostFilter(scopedTableBuilders, list);
			return this.JoinScopedTables(scopedTableBuilders, list, out coreTableFragments, out joinPredicates);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0004014C File Offset: 0x0003E34C
		private void ApplyInstanceFiltersAsPostFilter(IList<BatchGroupAndJoinBuilder> scopedTableBuilders, IList<PlanOperation> plannedTables)
		{
			bool flag = this.m_instanceFiltersContext.QueryStageForInstanceFilters == QueryStageForInstanceFilters.CoreTableAndShowAllPostFilter || !this.m_instanceFiltersContext.InstanceFiltersRequiringPostFiltering.IsNullOrEmpty<FilterCondition>();
			if (!this.m_dsContext.HasInstanceFilters || !flag)
			{
				return;
			}
			for (int i = 0; i < plannedTables.Count; i++)
			{
				plannedTables[i] = BatchDataSetPlanningFilterUtils.ApplyInstanceFiltersAsPostFilter(this.m_plannerContext.OutputExpressionTable, this.m_plannerContext.ErrorContext, this.m_plannerContext.Annotations, scopedTableBuilders[i], plannedTables[i], this.m_instanceFiltersContext.InstanceFiltersRequiringPostFiltering);
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000401E8 File Offset: 0x0003E3E8
		private void SetupUniqueMeasureNamingBlock(IList<BatchGroupAndJoinBuilder> scopedTableBuilders)
		{
			if (scopedTableBuilders.Count <= 1)
			{
				return;
			}
			foreach (BatchGroupAndJoinBuilder batchGroupAndJoinBuilder in scopedTableBuilders)
			{
				batchGroupAndJoinBuilder.InUniqueMeasureNamesBlock = true;
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00040238 File Offset: 0x0003E438
		private IList<PlanOperation> ApplyValueFilter(IList<BatchGroupAndJoinBuilder> scopedTableBuilders, IScope innermostScope)
		{
			DataShape dataShape = this.m_dsContext.DataShape;
			DataShapeAnnotation dataShapeAnnotation = this.m_plannerContext.Annotations.GetDataShapeAnnotation(dataShape);
			if (this.HasAnyFilterContextDataShape(dataShape))
			{
				return scopedTableBuilders.ToPlanOperations(this.m_dsContext.DataShape.Id.Value);
			}
			Filter filter = dataShapeAnnotation.ScopedValueFilters.SingleOrDefault<Filter>();
			if (filter == null)
			{
				return scopedTableBuilders.ToPlanOperations(this.m_dsContext.DataShape.Id.Value);
			}
			IScope resolvedScope = filter.Target.GetResolvedScope(this.m_plannerContext.OutputExpressionTable);
			DataShape containingDataShape = this.m_plannerContext.Annotations.GetContainingDataShape(filter);
			this.ValidateValueFilter(containingDataShape);
			if (this.m_dsContext.HasDataShapeProjections || this.ShouldApplyValueFilterAsContextTable(filter, scopedTableBuilders, resolvedScope))
			{
				this.ApplyValueFilterAsContextTable(scopedTableBuilders, filter, innermostScope, this.m_dsContext.HasShowItemsWithNoData);
				return scopedTableBuilders.ToPlanOperations(this.m_dsContext.DataShape.Id.Value);
			}
			Contract.RetailAssert(scopedTableBuilders.Count == 1, "A Value filter cannot be applied to multiple scoped tables as additional columns.");
			BatchGroupAndJoinBuilder batchGroupAndJoinBuilder = scopedTableBuilders[0];
			ICommonPlanningContext plannerContext = this.m_plannerContext;
			NamingContext namingContext = batchGroupAndJoinBuilder.NamingContext;
			Filter filter2 = filter;
			Func<string, Expression, ExpressionContext, PlanGroupAndJoinAdditionalColumn> func;
			if ((func = CoreTableSimpleSlicerStrategy.<>O.<0>__CreateValueFilterGroupAndJoinColumn) == null)
			{
				func = (CoreTableSimpleSlicerStrategy.<>O.<0>__CreateValueFilterGroupAndJoinColumn = new Func<string, Expression, ExpressionContext, PlanGroupAndJoinAdditionalColumn>(ValueFilterTableBuilder.CreateValueFilterGroupAndJoinColumn));
			}
			List<PlanGroupAndJoinAdditionalColumn> list = ValueFilterTableBuilder.CreateAdditionalColumns<PlanGroupAndJoinAdditionalColumn>(plannerContext, namingContext, filter2, func);
			batchGroupAndJoinBuilder.AddAdditionalColumns(list);
			PlanOperationGroupAndJoin planOperationGroupAndJoin = batchGroupAndJoinBuilder.ToPlanOperation(this.m_dsContext.DataShape.Id.Value);
			return new List<PlanOperation> { planOperationGroupAndJoin.FilterBy(filter.Condition) };
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x000403BC File Offset: 0x0003E5BC
		private bool ShouldApplyValueFilterAsContextTable(Filter valueFilter, IList<BatchGroupAndJoinBuilder> scopedTableBuilders, IScope filterTargetScope)
		{
			foreach (BatchGroupAndJoinBuilder batchGroupAndJoinBuilder in scopedTableBuilders)
			{
				if (BatchDataSetPlanningFilterUtils.ShouldApplyValueFilterAsContextTable(this.m_plannerContext, batchGroupAndJoinBuilder, filterTargetScope))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00040414 File Offset: 0x0003E614
		private void ValidateValueFilter(DataShape containingDataShape)
		{
			if (containingDataShape.ContextOnly.GetValueOrDefault<bool>() && !containingDataShape.IsIndependent)
			{
				Contract.RetailAssert(!containingDataShape.IsFilterContextDataShape(this.m_plannerContext.ScopeTree, this.m_plannerContext.Annotations), "Value filters on context filter data shapes should have been handled separately.");
				this.m_plannerContext.ErrorContext.Register(TranslationMessages.InvalidValueFilterOnContextOnlyDataShape(EngineMessageSeverity.Error, ObjectType.DataShape, containingDataShape.Id.Value, "Filters"));
				throw new DataSetPlanningException("Found invalid value filter on context only data shape.");
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00040498 File Offset: 0x0003E698
		private void ApplyContextFilter(IList<BatchGroupAndJoinBuilder> scopedTableBuilders)
		{
			DataShape dataShape = this.m_dsContext.DataShape;
			DataShapeAnnotation dataShapeAnnotation = this.m_plannerContext.Annotations.GetDataShapeAnnotation(dataShape);
			DataShape filterContextDataShape = this.GetFilterContextDataShape(dataShape);
			if (filterContextDataShape == null)
			{
				return;
			}
			IScope scope;
			BatchDataSetPlanningUtils.TryGetCoreTableRowScope(filterContextDataShape, this.m_plannerContext.ScopeTree, out scope);
			Filter filter = dataShapeAnnotation.ScopedValueFilters.SingleOrDefault<Filter>();
			if (filter == null)
			{
				return;
			}
			this.ApplyValueFilterAsContextTable(scopedTableBuilders, filter, scope, this.m_dsContext.HasShowItemsWithNoData);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0004050C File Offset: 0x0003E70C
		public void ApplyValueFilterAsContextTable(IList<BatchGroupAndJoinBuilder> scopedTableBuilders, Filter valueFilter, IScope innermostScope, bool hasShowAll)
		{
			BatchGroupAndJoinBuilder batchGroupAndJoinBuilder = scopedTableBuilders[0];
			PlanOperation planOperation = BatchDataSetPlanningFilterUtils.CreateValueFilterTable(this.m_plannerContext, batchGroupAndJoinBuilder, this.m_declarations, valueFilter, innermostScope, hasShowAll);
			for (int i = 0; i < scopedTableBuilders.Count; i++)
			{
				scopedTableBuilders[i].AddContextTable(planOperation);
			}
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00040558 File Offset: 0x0003E758
		private DataShape GetFilterContextDataShape(DataShape dataShape)
		{
			DataShape dataShape2;
			if (this.m_plannerContext.Annotations.TryGetFilterContextDataShape(dataShape, out dataShape2))
			{
				return dataShape2;
			}
			return null;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0004057D File Offset: 0x0003E77D
		private bool HasAnyFilterContextDataShape(DataShape dataShape)
		{
			return this.GetFilterContextDataShape(dataShape) != null;
		}

		// Token: 0x0200030B RID: 779
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000B31 RID: 2865
			public static Func<string, Expression, ExpressionContext, PlanGroupAndJoinAdditionalColumn> <0>__CreateValueFilterGroupAndJoinColumn;
		}
	}
}
