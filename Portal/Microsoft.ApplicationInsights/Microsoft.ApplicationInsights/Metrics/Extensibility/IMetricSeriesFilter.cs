using System;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x0200003F RID: 63
	internal interface IMetricSeriesFilter
	{
		// Token: 0x06000237 RID: 567
		bool WillConsume(MetricSeries dataSeries, out IMetricValueFilter valueFilter);
	}
}
