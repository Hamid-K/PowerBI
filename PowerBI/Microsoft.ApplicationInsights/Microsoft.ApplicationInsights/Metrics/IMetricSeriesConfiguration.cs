using System;
using Microsoft.ApplicationInsights.Metrics.Extensibility;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x02000026 RID: 38
	public interface IMetricSeriesConfiguration : IEquatable<IMetricSeriesConfiguration>
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000151 RID: 337
		bool RequiresPersistentAggregation { get; }

		// Token: 0x06000152 RID: 338
		IMetricSeriesAggregator CreateNewAggregator(MetricSeries dataSeries, MetricAggregationCycleKind aggregationCycleKind);
	}
}
