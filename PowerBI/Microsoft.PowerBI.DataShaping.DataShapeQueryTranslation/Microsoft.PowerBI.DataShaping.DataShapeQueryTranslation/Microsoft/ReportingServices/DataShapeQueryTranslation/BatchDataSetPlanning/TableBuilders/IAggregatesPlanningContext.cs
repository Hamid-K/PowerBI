using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001D2 RID: 466
	internal interface IAggregatesPlanningContext : ICommonPlanningContext
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600104F RID: 4175
		CalculationExpressionMap CalculationMap { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06001050 RID: 4176
		DataShapeQueryTranslationTelemetry TelemetryInfo { get; }
	}
}
