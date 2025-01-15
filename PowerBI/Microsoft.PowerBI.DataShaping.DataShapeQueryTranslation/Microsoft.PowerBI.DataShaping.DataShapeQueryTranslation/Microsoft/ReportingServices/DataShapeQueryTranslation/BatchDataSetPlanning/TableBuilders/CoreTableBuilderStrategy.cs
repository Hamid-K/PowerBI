using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001BF RID: 447
	internal abstract class CoreTableBuilderStrategy
	{
		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003FB15 File Offset: 0x0003DD15
		protected CoreTableBuilderStrategy(BatchDataSetPlannerContext plannerContext, DataShapeContext dsContext, ContextTableManager attributeFilterContextTableManager, InstanceFiltersContext instanceFiltersContext, PlanDeclarationCollection declarations, bool suppressUnconstrainedJoinCheck)
		{
			this.m_plannerContext = plannerContext;
			this.m_dsContext = dsContext;
			this.m_attributeFilterContextTableManager = attributeFilterContextTableManager;
			this.m_instanceFiltersContext = instanceFiltersContext;
			this.m_declarations = declarations;
			this.m_suppressUnconstrainedJoinCheck = suppressUnconstrainedJoinCheck;
		}

		// Token: 0x06000FC5 RID: 4037
		internal abstract PlanOperationContext Build(out CoreTableArtifacts coreArtifacts);

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0003FB4C File Offset: 0x0003DD4C
		internal IList<BatchGroupAndJoinBuilder> PopulateScopedTables(out IScope innermostScope)
		{
			ScopedTablesBuilder scopedTablesBuilder = new ScopedTablesBuilder(!this.m_dsContext.DataShape.HasFilterEmptyGroups(), this.m_suppressUnconstrainedJoinCheck);
			ScopedTablesCollector.Collect(this.m_plannerContext, this.m_dsContext, scopedTablesBuilder, this.m_attributeFilterContextTableManager, this.m_instanceFiltersContext.InstanceFilterDeclarations, this.m_instanceFiltersContext.QueryStageForInstanceFilters == QueryStageForInstanceFilters.CoreTableAndShowAllRollupContextTables, out innermostScope);
			this.AddClearDefaultContextTable(scopedTablesBuilder);
			return scopedTablesBuilder.TableBuilders.Evaluate<BatchGroupAndJoinBuilder>();
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0003FBBC File Offset: 0x0003DDBC
		private void AddClearDefaultContextTable(ScopedTablesBuilder scopedTablesBuilder)
		{
			PlanOperation planOperation = this.m_plannerContext.DefaultValueContextManager.ToPlanOperation(this.m_dsContext.DataShape);
			if (planOperation != null)
			{
				scopedTablesBuilder.AddContextTable(planOperation);
			}
		}

		// Token: 0x04000769 RID: 1897
		protected readonly BatchDataSetPlannerContext m_plannerContext;

		// Token: 0x0400076A RID: 1898
		protected readonly DataShapeContext m_dsContext;

		// Token: 0x0400076B RID: 1899
		protected readonly ContextTableManager m_attributeFilterContextTableManager;

		// Token: 0x0400076C RID: 1900
		protected readonly InstanceFiltersContext m_instanceFiltersContext;

		// Token: 0x0400076D RID: 1901
		protected readonly PlanDeclarationCollection m_declarations;

		// Token: 0x0400076E RID: 1902
		protected readonly bool m_suppressUnconstrainedJoinCheck;
	}
}
