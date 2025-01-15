using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x0200017A RID: 378
	internal static class BatchPlanningStrategyUtils
	{
		// Token: 0x06000D70 RID: 3440 RVA: 0x00037560 File Offset: 0x00035760
		public static BatchPlanningStrategy DeterminePlanningStrategy(this DataShapeContext dsContext)
		{
			if (dsContext.HasDetailGroups)
			{
				return BatchPlanningStrategy.DetailGroups;
			}
			if (!dsContext.HasAnyPrimaryDynamic && !dsContext.HasAnySecondaryDynamic)
			{
				return BatchPlanningStrategy.AggregatesOnly;
			}
			if (dsContext.DataShape.PrimaryHierarchy == null && dsContext.HasAnySecondaryDynamic)
			{
				return BatchPlanningStrategy.SecondaryOnly;
			}
			if (dsContext.HasAnyPrimaryDynamic && !dsContext.HasAnySecondaryDynamic)
			{
				return BatchPlanningStrategy.PrimaryOnly;
			}
			return BatchPlanningStrategy.PrimaryAndSecondary;
		}
	}
}
