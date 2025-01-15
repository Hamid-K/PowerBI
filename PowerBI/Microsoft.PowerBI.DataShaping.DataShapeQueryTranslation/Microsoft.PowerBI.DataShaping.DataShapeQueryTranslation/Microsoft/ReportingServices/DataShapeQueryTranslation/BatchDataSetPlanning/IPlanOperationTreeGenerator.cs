using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000198 RID: 408
	internal interface IPlanOperationTreeGenerator
	{
		// Token: 0x06000E56 RID: 3670
		PlanOperationTreeGeneratorResult GenerateTables(DataShapeContext dsContext, bool omitOrderBy, bool suppressCoreTableUnconstrainedJoinCheck, BatchPlanningStrategy translationStrategy);
	}
}
