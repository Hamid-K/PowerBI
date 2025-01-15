using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001C1 RID: 449
	internal sealed class CoreTableComplexSlicerStrategy : CoreTableBuilderStrategy
	{
		// Token: 0x06000FCE RID: 4046 RVA: 0x0003FC54 File Offset: 0x0003DE54
		internal CoreTableComplexSlicerStrategy(BatchDataSetPlannerContext plannerContext, DataShapeContext dsContext, PlanDeclarationCollection declarations, ContextTableManager attributeFilterContextTableManager, InstanceFiltersContext instanceFiltersContext, bool suppressUnconstrainedJoinCheck)
			: base(plannerContext, dsContext, attributeFilterContextTableManager, instanceFiltersContext, declarations, suppressUnconstrainedJoinCheck)
		{
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0003FC68 File Offset: 0x0003DE68
		internal override PlanOperationContext Build(out CoreTableArtifacts coreArtifacts)
		{
			IScope scope;
			IList<BatchGroupAndJoinBuilder> list = base.PopulateScopedTables(out scope);
			Contract.RetailAssert(list.Count == 1, "Violation of {0}", TranslationErrorCode.ComplexSlicerNotAllowedWithMeasures);
			BatchGroupAndJoinBuilder batchGroupAndJoinBuilder = list.Single<BatchGroupAndJoinBuilder>();
			PlanOperation planOperation = this.ApplyComplexSlicer(batchGroupAndJoinBuilder);
			PlanOperationContext planOperationContext = batchGroupAndJoinBuilder.ToPlanOperationContext(this.m_plannerContext.ScopeTree, planOperation, null);
			planOperationContext = this.ApplyInstanceFiltersAsPostFilter(batchGroupAndJoinBuilder, planOperationContext);
			ReadOnlyCollection<PlanOperation> readOnlyCollection = batchGroupAndJoinBuilder.ContextTables.ToReadOnlyCollection<PlanOperation>();
			PlanOperationContext[] array = new PlanOperationContext[] { planOperationContext };
			this.m_plannerContext.TelemetryInfo.UsedBatchComplexSlicer = true;
			BatchDataSetPlannerJoinPredicates batchDataSetPlannerJoinPredicates = new BatchDataSetPlannerJoinPredicates(batchGroupAndJoinBuilder.CalculationsAsJoinPredicates, batchGroupAndJoinBuilder.MeasureTransformColumns);
			coreArtifacts = new CoreTableArtifacts(readOnlyCollection, array, batchDataSetPlannerJoinPredicates, batchGroupAndJoinBuilder.AttributeFilters);
			return planOperationContext;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003FD10 File Offset: 0x0003DF10
		public PlanOperationContext ApplyInstanceFiltersAsPostFilter(BatchGroupAndJoinBuilder joinBuilder, PlanOperationContext tableContext)
		{
			bool flag = this.m_instanceFiltersContext.QueryStageForInstanceFilters == QueryStageForInstanceFilters.CoreTableAndShowAllPostFilter || !this.m_instanceFiltersContext.InstanceFiltersRequiringPostFiltering.IsNullOrEmpty<FilterCondition>();
			if (!this.m_dsContext.HasInstanceFilters || !flag)
			{
				return tableContext;
			}
			PlanOperation planOperation = BatchDataSetPlanningFilterUtils.ApplyInstanceFiltersAsPostFilter(this.m_plannerContext.OutputExpressionTable, this.m_plannerContext.ErrorContext, this.m_plannerContext.Annotations, joinBuilder, tableContext.Table, this.m_instanceFiltersContext.InstanceFiltersRequiringPostFiltering);
			return tableContext.ReplaceTable(planOperation, null, null, null);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003FD98 File Offset: 0x0003DF98
		private PlanOperation ApplyComplexSlicer(BatchGroupAndJoinBuilder joinBuilder)
		{
			DataShape dataShape = this.m_dsContext.DataShape;
			Filter filter = this.m_plannerContext.Annotations.GetFilter(dataShape, null);
			Contract.RetailAssert(filter != null && this.m_dsContext.HasComplexSlicer, "This codepath requires a complex slicer");
			Contract.RetailAssert(this.m_plannerContext.Annotations.GetDataShapeAnnotation(dataShape).ScopedValueFilters.Count == 0, "Value filters are not allowed with complex slicers");
			List<PlanOperation> list = ComplexSlicerTableBuilder.BuildSlicerTables(this.m_plannerContext.OutputExpressionTable, joinBuilder.ToPlanOperation(null), filter.Condition, true, this.m_plannerContext.Schema.SupportsHierarchicalFilterDisjunction(), this.m_plannerContext.Schema, this.m_declarations, dataShape.Id, this.m_plannerContext.ErrorContext).ToList<PlanOperation>();
			if (!joinBuilder.HasContextTables && list.Count == 1 && list.ElementAt(0) is PlanOperationUnion)
			{
				return list.ElementAt(0).GroupBy(joinBuilder.GroupByMembers.Select((PlanGroupByMember gbm) => gbm.RemoveContextTables()));
			}
			joinBuilder.AddContextTables(list);
			return joinBuilder.ToPlanOperation(null);
		}
	}
}
