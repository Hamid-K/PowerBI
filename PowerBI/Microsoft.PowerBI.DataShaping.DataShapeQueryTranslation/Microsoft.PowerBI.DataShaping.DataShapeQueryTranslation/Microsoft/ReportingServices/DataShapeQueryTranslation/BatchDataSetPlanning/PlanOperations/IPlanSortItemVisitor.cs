using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x0200022D RID: 557
	internal interface IPlanSortItemVisitor
	{
		// Token: 0x06001320 RID: 4896
		void Visit(PlanMemberSortItem item);

		// Token: 0x06001321 RID: 4897
		void Visit(PlanColumnSortItem item);

		// Token: 0x06001322 RID: 4898
		void Visit(PlanAllColumnsSortItem item);
	}
}
