using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x02000211 RID: 529
	internal interface IPlanAggregateItemVisitor
	{
		// Token: 0x06001264 RID: 4708
		void Visit(PlanAggregateCalculationItem item);

		// Token: 0x06001265 RID: 4709
		void Visit(PlanAggregateExpressionItem item);
	}
}
