using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001F6 RID: 502
	internal interface IPlanBinnedLineSampleItemVisitor
	{
		// Token: 0x0600115A RID: 4442
		void Visit(PlanBinnedLineSampleMember item);

		// Token: 0x0600115B RID: 4443
		void Visit(PlanBinnedLineSampleCalculation item);
	}
}
