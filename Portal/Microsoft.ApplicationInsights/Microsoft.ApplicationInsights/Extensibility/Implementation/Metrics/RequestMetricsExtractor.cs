using System;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Metrics;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Metrics
{
	// Token: 0x02000090 RID: 144
	internal class RequestMetricsExtractor : ISpecificAutocollectedMetricsExtractor
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00013B1E File Offset: 0x00011D1E
		public string ExtractorName { get; } = "Requests";

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00013B26 File Offset: 0x00011D26
		public string ExtractorVersion { get; } = "1.1";

		// Token: 0x06000479 RID: 1145 RVA: 0x00013B30 File Offset: 0x00011D30
		public void InitializeExtractor(TelemetryClient metricTelemetryClient)
		{
			if (metricTelemetryClient == null)
			{
				this.responseTimeSuccessSeries = null;
				this.responseTimeFailureSeries = null;
				return;
			}
			Metric metric = metricTelemetryClient.GetMetric("Server response time", "Request.Success", MetricDimensionNames.TelemetryContext.Property("_MS.MetricId"), MetricConfigurations.Common.Measurement(), MetricAggregationScope.TelemetryClient);
			metric.TryGetDataSeries(out this.responseTimeSuccessSeries, bool.TrueString, "requests/duration");
			metric.TryGetDataSeries(out this.responseTimeFailureSeries, bool.FalseString, "requests/duration");
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00013BA4 File Offset: 0x00011DA4
		public void ExtractMetrics(ITelemetry fromItem, out bool isItemProcessed)
		{
			RequestTelemetry requestTelemetry = fromItem as RequestTelemetry;
			if (requestTelemetry == null)
			{
				isItemProcessed = false;
				return;
			}
			MetricSeries metricSeries = ((requestTelemetry.Success != null && !requestTelemetry.Success.Value) ? this.responseTimeFailureSeries : this.responseTimeSuccessSeries);
			if (metricSeries != null)
			{
				isItemProcessed = true;
				metricSeries.TrackValue(requestTelemetry.Duration.TotalMilliseconds);
				return;
			}
			isItemProcessed = false;
		}

		// Token: 0x040001CA RID: 458
		private MetricSeries responseTimeSuccessSeries;

		// Token: 0x040001CB RID: 459
		private MetricSeries responseTimeFailureSeries;
	}
}
