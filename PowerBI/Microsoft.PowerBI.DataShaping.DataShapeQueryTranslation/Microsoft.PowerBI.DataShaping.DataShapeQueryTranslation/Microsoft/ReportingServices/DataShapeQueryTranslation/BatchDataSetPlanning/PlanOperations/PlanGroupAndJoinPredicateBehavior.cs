using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001EC RID: 492
	[Flags]
	internal enum PlanGroupAndJoinPredicateBehavior
	{
		// Token: 0x040007E9 RID: 2025
		SuppressAutoPredicates = 0,
		// Token: 0x040007EA RID: 2026
		ApplyAutoPredicates = 2,
		// Token: 0x040007EB RID: 2027
		ExistsPredicates = 4
	}
}
