using System;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x0200004A RID: 74
	public static class MetricSeriesExtensions
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000CB68 File Offset: 0x0000AD68
		public static IMetricSeriesConfiguration GetConfiguration(this MetricSeries metricSeries)
		{
			return metricSeries.configuration;
		}
	}
}
