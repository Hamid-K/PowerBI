using System;
using System.Runtime.CompilerServices;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Metrics;
using Microsoft.ApplicationInsights.Metrics.Extensibility;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Metrics
{
	// Token: 0x0200008D RID: 141
	internal class DependencyMetricsExtractor : ISpecificAutocollectedMetricsExtractor
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x000137D5 File Offset: 0x000119D5
		public string ExtractorName { get; } = "Dependencies";

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x000137DD File Offset: 0x000119DD
		public string ExtractorVersion { get; } = "1.1";

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x000137E5 File Offset: 0x000119E5
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x000137ED File Offset: 0x000119ED
		public int MaxDependencyTypesToDiscover
		{
			get
			{
				return this.maxDependencyTypesToDiscover;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", value, "MaxDependencyTypesToDiscover value may not be negative.");
				}
				TelemetryClient telemetryClient = this.metricTelemetryClient;
				if (telemetryClient != null)
				{
					telemetryClient.Flush();
				}
				this.ReinitializeMetrics(value);
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00013821 File Offset: 0x00011A21
		public void InitializeExtractor(TelemetryClient metricTelemetryClient)
		{
			this.metricTelemetryClient = metricTelemetryClient;
			this.ReinitializeMetrics(this.maxDependencyTypesToDiscover);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00013838 File Offset: 0x00011A38
		public void ExtractMetrics(ITelemetry fromItem, out bool isItemProcessed)
		{
			DependencyTelemetry dependencyTelemetry = fromItem as DependencyTelemetry;
			if (dependencyTelemetry == null)
			{
				isItemProcessed = false;
				return;
			}
			Metric metric = this.dependencyCallDurationMetric;
			if (metric == null)
			{
				throw new InvalidOperationException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Cannot execute {0}.", new object[] { "ExtractMetrics" })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" There is no {0}.", new object[] { "dependencyCallDurationMetric" })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" Either this metrics extractor has not been initialized, or it has been disposed.", new object[0])));
			}
			bool flag2;
			if (dependencyTelemetry.Success != null)
			{
				bool? success = dependencyTelemetry.Success;
				bool flag = false;
				flag2 = (success.GetValueOrDefault() == flag) & (success != null);
			}
			else
			{
				flag2 = false;
			}
			string text = (flag2 ? bool.FalseString : bool.TrueString);
			MetricSeries metricSeries = null;
			if (this.MaxDependencyTypesToDiscover == 0)
			{
				metric.TryGetDataSeries(out metricSeries, "dependencies/duration", text, "Other");
			}
			else
			{
				string text2 = dependencyTelemetry.Type;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = "Unknown";
				}
				if (!metric.TryGetDataSeries(out metricSeries, "dependencies/duration", text, text2))
				{
					metric.TryGetDataSeries(out metricSeries, "dependencies/duration", text, "Other");
				}
			}
			metricSeries.TrackValue(dependencyTelemetry.Duration.TotalMilliseconds);
			isItemProcessed = true;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00013970 File Offset: 0x00011B70
		private void ReinitializeMetrics(int maxDependencyTypesToDiscoverCount)
		{
			if (maxDependencyTypesToDiscoverCount < 0)
			{
				throw new ArgumentOutOfRangeException("maxDependencyTypesToDiscoverCount", maxDependencyTypesToDiscoverCount, global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0} value may not be negative.", new object[] { "MaxDependencyTypesToDiscover" })));
			}
			object obj = this.initializationLock;
			lock (obj)
			{
				if (maxDependencyTypesToDiscoverCount > 2147483644)
				{
					maxDependencyTypesToDiscoverCount = 2147483644;
				}
				int num = ((maxDependencyTypesToDiscoverCount == 0) ? 1 : (maxDependencyTypesToDiscoverCount + 2));
				TelemetryClient telemetryClient = this.metricTelemetryClient;
				if (telemetryClient == null)
				{
					this.dependencyCallDurationMetric = null;
					this.maxDependencyTypesToDiscover = maxDependencyTypesToDiscoverCount;
				}
				else
				{
					MetricManager metricManager;
					if (telemetryClient.TryGetMetricManager(out metricManager))
					{
						Metric metric = this.dependencyCallDurationMetric;
						metricManager.Metrics.Remove(metric);
					}
					MetricConfiguration metricConfiguration = new MetricConfigurationForMeasurement(2 * num + 1, new int[] { 1, 2, num }, new MetricSeriesConfigurationForMeasurement(false));
					Metric metric2 = telemetryClient.GetMetric("Dependency duration", MetricDimensionNames.TelemetryContext.Property("_MS.MetricId"), "Dependency.Success", "Dependency.Type", metricConfiguration, MetricAggregationScope.TelemetryClient);
					MetricSeries metricSeries;
					metric2.TryGetDataSeries(out metricSeries, "dependencies/duration", bool.TrueString, "Other");
					metric2.TryGetDataSeries(out metricSeries, "dependencies/duration", bool.FalseString, "Other");
					if (maxDependencyTypesToDiscoverCount != 0)
					{
						metric2.TryGetDataSeries(out metricSeries, "dependencies/duration", bool.TrueString, "Unknown");
						metric2.TryGetDataSeries(out metricSeries, "dependencies/duration", bool.FalseString, "Unknown");
					}
					this.dependencyCallDurationMetric = metric2;
					this.maxDependencyTypesToDiscover = maxDependencyTypesToDiscoverCount;
				}
			}
		}

		// Token: 0x040001C2 RID: 450
		public const int MaxDependenctTypesToDiscoverDefault = 15;

		// Token: 0x040001C3 RID: 451
		private readonly object initializationLock = new object();

		// Token: 0x040001C4 RID: 452
		private TelemetryClient metricTelemetryClient;

		// Token: 0x040001C5 RID: 453
		private Metric dependencyCallDurationMetric;

		// Token: 0x040001C6 RID: 454
		private int maxDependencyTypesToDiscover = 15;
	}
}
