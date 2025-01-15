using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x02000040 RID: 64
	public interface IMetricTelemetryPipeline
	{
		// Token: 0x06000238 RID: 568
		Task TrackAsync(MetricAggregate metricAggregate, CancellationToken cancelToken);

		// Token: 0x06000239 RID: 569
		Task FlushAsync(CancellationToken cancelToken);
	}
}
