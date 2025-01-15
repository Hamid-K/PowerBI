using System;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x02000048 RID: 72
	internal static class MetricManagerExtensions
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000C752 File Offset: 0x0000A952
		public static AggregationPeriodSummary StopAggregators(this MetricManager metricManager, MetricAggregationCycleKind aggregationCycleKind, DateTimeOffset tactTimestamp)
		{
			Util.ValidateNotNull(metricManager, "metricManager");
			return metricManager.AggregationManager.StopAggregators(aggregationCycleKind, tactTimestamp);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000C76C File Offset: 0x0000A96C
		public static AggregationPeriodSummary StartOrCycleAggregators(this MetricManager metricManager, MetricAggregationCycleKind aggregationCycleKind, DateTimeOffset tactTimestamp, IMetricSeriesFilter futureFilter)
		{
			Util.ValidateNotNull(metricManager, "metricManager");
			return metricManager.AggregationManager.StartOrCycleAggregators(aggregationCycleKind, tactTimestamp, futureFilter);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000C787 File Offset: 0x0000A987
		public static Task StopDefaultAggregationCycleAsync(this MetricManager metricManager)
		{
			Util.ValidateNotNull(metricManager, "metricManager");
			metricManager.Flush();
			return metricManager.AggregationCycle.StopAsync();
		}
	}
}
