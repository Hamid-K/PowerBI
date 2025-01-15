using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x0200004E RID: 78
	public static class TelemetryClientExtensions
	{
		// Token: 0x06000284 RID: 644 RVA: 0x0000CD78 File Offset: 0x0000AF78
		public static MetricManager GetMetricManager(this TelemetryClient telemetryClient, MetricAggregationScope aggregationScope)
		{
			Util.ValidateNotNull(telemetryClient, "telemetryClient");
			if (aggregationScope == MetricAggregationScope.TelemetryConfiguration)
			{
				return telemetryClient.TelemetryConfiguration.GetMetricManager();
			}
			if (aggregationScope != MetricAggregationScope.TelemetryClient)
			{
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Invalid value of {0} ({1}). Only the following values are supported:", new object[] { "aggregationScope", aggregationScope })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" ['{0}.{1}',", new object[]
				{
					"MetricAggregationScope",
					MetricAggregationScope.TelemetryClient.ToString()
				})) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" '{0}.{1}'].", new object[]
				{
					"MetricAggregationScope",
					MetricAggregationScope.TelemetryConfiguration.ToString()
				})));
			}
			return TelemetryClientExtensions.GetOrCreateMetricManager(telemetryClient);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000CE3C File Offset: 0x0000B03C
		internal static bool TryGetMetricManager(this TelemetryClient telemetryClient, out MetricManager metricManager)
		{
			if (telemetryClient == null)
			{
				metricManager = null;
				return false;
			}
			ConditionalWeakTable<TelemetryClient, MetricManager> conditionalWeakTable = TelemetryClientExtensions.metricManagersForTelemetryClients;
			if (conditionalWeakTable == null)
			{
				metricManager = null;
				return false;
			}
			return conditionalWeakTable.TryGetValue(telemetryClient, out metricManager);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000CE68 File Offset: 0x0000B068
		private static MetricManager GetOrCreateMetricManager(TelemetryClient telemetryClient)
		{
			ConditionalWeakTable<TelemetryClient, MetricManager> conditionalWeakTable = TelemetryClientExtensions.metricManagersForTelemetryClients;
			if (conditionalWeakTable == null)
			{
				ConditionalWeakTable<TelemetryClient, MetricManager> conditionalWeakTable2 = new ConditionalWeakTable<TelemetryClient, MetricManager>();
				conditionalWeakTable = Interlocked.CompareExchange<ConditionalWeakTable<TelemetryClient, MetricManager>>(ref TelemetryClientExtensions.metricManagersForTelemetryClients, conditionalWeakTable2, null) ?? conditionalWeakTable2;
			}
			MetricManager createdManager = null;
			MetricManager value = conditionalWeakTable.GetValue(telemetryClient, delegate(TelemetryClient tc)
			{
				createdManager = new MetricManager(new ApplicationInsightsTelemetryPipeline(tc));
				return createdManager;
			});
			if (createdManager != null && createdManager != value)
			{
				createdManager.StopDefaultAggregationCycleAsync();
			}
			return value;
		}

		// Token: 0x0400011E RID: 286
		private static ConditionalWeakTable<TelemetryClient, MetricManager> metricManagersForTelemetryClients;
	}
}
