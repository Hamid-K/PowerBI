using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000224 RID: 548
	internal interface IPlanProjectItemVisitor
	{
		// Token: 0x060012DF RID: 4831
		void Visit(PlanNamedColumnProjectItem item);

		// Token: 0x060012E0 RID: 4832
		void Visit(PlanTransformExistingColumnProjectItem item);

		// Token: 0x060012E1 RID: 4833
		void Visit(PlanTransformExistingColumnWithSameNameProjectItem item);

		// Token: 0x060012E2 RID: 4834
		void Visit(PlanPreserveAllColumnsProjectItem item);

		// Token: 0x060012E3 RID: 4835
		void Visit(PlanPreserveColumnsProjectItem item);

		// Token: 0x060012E4 RID: 4836
		void Visit(PlanCalculationProjectItem item);

		// Token: 0x060012E5 RID: 4837
		void Visit(PlanPreserveCalculationProjectItem item);

		// Token: 0x060012E6 RID: 4838
		void Visit(PlanNewColumnProjectItem item);

		// Token: 0x060012E7 RID: 4839
		void Visit(PlanPreserveAllColumnsExceptProjectItem item);

		// Token: 0x060012E8 RID: 4840
		void Visit(PlanMapColumnIdentityProjectItem item);

		// Token: 0x060012E9 RID: 4841
		void Visit(PlanDataMemberProjectItem item);
	}
}
