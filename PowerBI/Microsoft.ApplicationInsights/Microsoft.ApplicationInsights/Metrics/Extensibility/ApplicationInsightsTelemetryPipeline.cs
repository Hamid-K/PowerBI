using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x0200003C RID: 60
	internal class ApplicationInsightsTelemetryPipeline : IMetricTelemetryPipeline
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0000BA78 File Offset: 0x00009C78
		public ApplicationInsightsTelemetryPipeline(TelemetryConfiguration telemetryPipeline)
		{
			Util.ValidateNotNull(telemetryPipeline, "telemetryPipeline");
			this.trackingClient = new TelemetryClient(telemetryPipeline);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000BAA3 File Offset: 0x00009CA3
		public ApplicationInsightsTelemetryPipeline(TelemetryClient telemetryClient)
		{
			Util.ValidateNotNull(telemetryClient, "telemetryClient");
			this.trackingClient = telemetryClient;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000BACC File Offset: 0x00009CCC
		public Task TrackAsync(MetricAggregate metricAggregate, CancellationToken cancelToken)
		{
			Util.ValidateNotNull(metricAggregate, "metricAggregate");
			Util.ValidateNotNull(metricAggregate.AggregationKindMoniker, "AggregationKindMoniker");
			cancelToken.ThrowIfCancellationRequested();
			IMetricAggregateToTelemetryPipelineConverter metricAggregateToTelemetryPipelineConverter;
			if (!MetricAggregateToTelemetryPipelineConverters.Registry.TryGet(typeof(ApplicationInsightsTelemetryPipeline), metricAggregate.AggregationKindMoniker, out metricAggregateToTelemetryPipelineConverter))
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Cannot track the specified {0}, because there is no {1}", new object[] { metricAggregate, "IMetricAggregateToTelemetryPipelineConverter" })),
					global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" registered for it. A converter must be added to {0}", new object[] { "MetricAggregateToTelemetryPipelineConverters" })),
					global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(".{0} for the pipeline type", new object[] { "Registry" })),
					global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" '{0}' and {1}", new object[] { "ApplicationInsightsTelemetryPipeline", "AggregationKindMoniker" })),
					global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" '{0}'.", new object[] { metricAggregate.AggregationKindMoniker }))
				}));
			}
			MetricTelemetry metricTelemetry = (MetricTelemetry)metricAggregateToTelemetryPipelineConverter.Convert(metricAggregate);
			this.trackingClient.Track(metricTelemetry);
			return this.completedTask;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000BBF9 File Offset: 0x00009DF9
		public Task FlushAsync(CancellationToken cancelToken)
		{
			cancelToken.ThrowIfCancellationRequested();
			this.trackingClient.Flush();
			return this.completedTask;
		}

		// Token: 0x04000106 RID: 262
		private readonly TelemetryClient trackingClient;

		// Token: 0x04000107 RID: 263
		private readonly Task completedTask = Task.FromResult<bool>(true);
	}
}
