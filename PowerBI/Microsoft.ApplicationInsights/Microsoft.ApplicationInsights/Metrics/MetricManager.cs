using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Metrics.Extensibility;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x02000030 RID: 48
	public sealed class MetricManager
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00009C98 File Offset: 0x00007E98
		public MetricManager(IMetricTelemetryPipeline telemetryPipeline)
		{
			Util.ValidateNotNull(telemetryPipeline, "telemetryPipeline");
			this.telemetryPipeline = telemetryPipeline;
			this.aggregationManager = new MetricAggregationManager();
			this.aggregationCycle = new DefaultAggregationPeriodCycle(this.aggregationManager, this);
			this.metrics = new MetricsCollection(this);
			this.aggregationCycle.Start();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009CF4 File Offset: 0x00007EF4
		~MetricManager()
		{
			if (this.aggregationCycle != null)
			{
				this.aggregationCycle.StopAsync();
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009D30 File Offset: 0x00007F30
		public MetricsCollection Metrics
		{
			get
			{
				return this.metrics;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00009D38 File Offset: 0x00007F38
		internal MetricAggregationManager AggregationManager
		{
			get
			{
				return this.aggregationManager;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00009D40 File Offset: 0x00007F40
		internal DefaultAggregationPeriodCycle AggregationCycle
		{
			get
			{
				return this.aggregationCycle;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009D48 File Offset: 0x00007F48
		public MetricSeries CreateNewSeries(string metricNamespace, string metricId, IMetricSeriesConfiguration config)
		{
			return this.CreateNewSeries(metricNamespace, metricId, null, config);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00009D54 File Offset: 0x00007F54
		public MetricSeries CreateNewSeries(string metricNamespace, string metricId, IEnumerable<KeyValuePair<string, string>> dimensionNamesAndValues, IMetricSeriesConfiguration config)
		{
			List<string> list = null;
			if (dimensionNamesAndValues != null)
			{
				list = new List<string>();
				foreach (KeyValuePair<string, string> keyValuePair in dimensionNamesAndValues)
				{
					list.Add(keyValuePair.Key);
				}
			}
			MetricIdentifier metricIdentifier = new MetricIdentifier(metricNamespace, metricId, list);
			return this.CreateNewSeries(metricIdentifier, dimensionNamesAndValues, config);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00009DC0 File Offset: 0x00007FC0
		public MetricSeries CreateNewSeries(MetricIdentifier metricIdentifier, IEnumerable<KeyValuePair<string, string>> dimensionNamesAndValues, IMetricSeriesConfiguration config)
		{
			Util.ValidateNotNull(metricIdentifier, "metricIdentifier");
			Util.ValidateNotNull(config, "config");
			return new MetricSeries(this.aggregationManager, metricIdentifier, dimensionNamesAndValues, config);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009DE6 File Offset: 0x00007FE6
		public void Flush()
		{
			this.Flush(true);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009DF0 File Offset: 0x00007FF0
		internal void Flush(bool flushDownstreamPipeline)
		{
			DateTimeOffset now = DateTimeOffset.Now;
			AggregationPeriodSummary aggregationPeriodSummary = this.aggregationManager.StartOrCycleAggregators(MetricAggregationCycleKind.Default, now, null);
			this.TrackMetricAggregates(aggregationPeriodSummary, flushDownstreamPipeline);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009E1C File Offset: 0x0000801C
		internal void TrackMetricAggregates(AggregationPeriodSummary aggregates, bool flush)
		{
			int num = ((((aggregates != null) ? aggregates.NonpersistentAggregates : null) == null) ? 0 : aggregates.NonpersistentAggregates.Count);
			int num2 = ((((aggregates != null) ? aggregates.PersistentAggregates : null) == null) ? 0 : aggregates.PersistentAggregates.Count);
			int num3 = num + num2;
			if (num3 == 0)
			{
				return;
			}
			Task[] array = new Task[num3];
			int num4 = 0;
			if (num != 0)
			{
				foreach (MetricAggregate metricAggregate in aggregates.NonpersistentAggregates)
				{
					if (metricAggregate != null)
					{
						Task task = this.telemetryPipeline.TrackAsync(metricAggregate, CancellationToken.None);
						array[num4++] = task;
					}
				}
			}
			if (aggregates.PersistentAggregates != null && aggregates.PersistentAggregates.Count != 0)
			{
				foreach (MetricAggregate metricAggregate2 in aggregates.PersistentAggregates)
				{
					if (metricAggregate2 != null)
					{
						Task task2 = this.telemetryPipeline.TrackAsync(metricAggregate2, CancellationToken.None);
						array[num4++] = task2;
					}
				}
			}
			Task.WaitAll(array);
			if (flush)
			{
				this.telemetryPipeline.FlushAsync(CancellationToken.None).Wait();
			}
		}

		// Token: 0x040000CE RID: 206
		private readonly MetricAggregationManager aggregationManager;

		// Token: 0x040000CF RID: 207
		private readonly DefaultAggregationPeriodCycle aggregationCycle;

		// Token: 0x040000D0 RID: 208
		private readonly IMetricTelemetryPipeline telemetryPipeline;

		// Token: 0x040000D1 RID: 209
		private readonly MetricsCollection metrics;
	}
}
