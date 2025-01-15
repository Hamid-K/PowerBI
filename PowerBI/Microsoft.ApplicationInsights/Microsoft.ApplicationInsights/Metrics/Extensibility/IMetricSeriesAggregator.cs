using System;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x0200003E RID: 62
	public interface IMetricSeriesAggregator
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600022F RID: 559
		MetricSeries DataSeries { get; }

		// Token: 0x06000230 RID: 560
		bool TryRecycle();

		// Token: 0x06000231 RID: 561
		void Reset(DateTimeOffset periodStart, IMetricValueFilter valueFilter);

		// Token: 0x06000232 RID: 562
		void Reset(DateTimeOffset periodStart);

		// Token: 0x06000233 RID: 563
		MetricAggregate CompleteAggregation(DateTimeOffset periodEnd);

		// Token: 0x06000234 RID: 564
		MetricAggregate CreateAggregateUnsafe(DateTimeOffset periodEnd);

		// Token: 0x06000235 RID: 565
		void TrackValue(double metricValue);

		// Token: 0x06000236 RID: 566
		void TrackValue(object metricValue);
	}
}
