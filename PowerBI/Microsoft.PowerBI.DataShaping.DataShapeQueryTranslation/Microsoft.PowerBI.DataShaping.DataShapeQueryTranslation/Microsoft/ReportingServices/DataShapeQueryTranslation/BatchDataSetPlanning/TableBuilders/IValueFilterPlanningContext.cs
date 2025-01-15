using System;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001D7 RID: 471
	internal interface IValueFilterPlanningContext : ICommonPlanningContext
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06001064 RID: 4196
		CalculationExpressionMap CalculationMap { get; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06001065 RID: 4197
		DataTransformReferenceMap TransformReferenceMap { get; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06001066 RID: 4198
		bool ApplyTransformsInQuery { get; }
	}
}
