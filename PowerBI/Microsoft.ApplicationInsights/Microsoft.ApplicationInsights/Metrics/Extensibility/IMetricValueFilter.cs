using System;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x02000041 RID: 65
	public interface IMetricValueFilter
	{
		// Token: 0x0600023A RID: 570
		bool WillConsume(MetricSeries dataSeries, double metricValue);

		// Token: 0x0600023B RID: 571
		bool WillConsume(MetricSeries dataSeries, object metricValue);
	}
}
