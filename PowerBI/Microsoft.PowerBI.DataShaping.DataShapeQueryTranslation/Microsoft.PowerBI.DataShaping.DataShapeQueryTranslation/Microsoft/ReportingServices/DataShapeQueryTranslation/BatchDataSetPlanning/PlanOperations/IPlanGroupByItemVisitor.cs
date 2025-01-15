using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200020D RID: 525
	internal interface IPlanGroupByItemVisitor
	{
		// Token: 0x06001245 RID: 4677
		void Visit(PlanGroupByMember item);

		// Token: 0x06001246 RID: 4678
		void Visit(PlanGroupByCalculation item);

		// Token: 0x06001247 RID: 4679
		void Visit(PlanGroupByColumn item);

		// Token: 0x06001248 RID: 4680
		void Visit(PlanGroupByDataTransformColumn item);

		// Token: 0x06001249 RID: 4681
		void Visit(PlanGroupByGroupKey item);
	}
}
