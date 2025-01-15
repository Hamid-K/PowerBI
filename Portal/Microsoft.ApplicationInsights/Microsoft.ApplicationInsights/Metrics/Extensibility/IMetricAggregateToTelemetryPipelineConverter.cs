using System;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x0200003D RID: 61
	internal interface IMetricAggregateToTelemetryPipelineConverter
	{
		// Token: 0x0600022E RID: 558
		object Convert(MetricAggregate aggregate);
	}
}
