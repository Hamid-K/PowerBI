using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ApplicationInsights.Metrics.ConcurrentDatastructures;
using Microsoft.ApplicationInsights.Metrics.Extensibility;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x0200002A RID: 42
	internal class MetricAggregationManager
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00008500 File Offset: 0x00006700
		internal MetricAggregationManager()
		{
			DateTimeOffset dateTimeOffset = Util.RoundDownToSecond(DateTimeOffset.Now);
			this.aggregatorsForDefault = new MetricAggregationManager.AggregatorCollection(dateTimeOffset, null);
			this.aggregatorsForPersistent = new MetricAggregationManager.AggregatorCollection(dateTimeOffset, null);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008538 File Offset: 0x00006738
		public AggregationPeriodSummary StartOrCycleAggregators(MetricAggregationCycleKind aggregationCycleKind, DateTimeOffset tactTimestamp, IMetricSeriesFilter futureFilter)
		{
			switch (aggregationCycleKind)
			{
			case MetricAggregationCycleKind.Default:
				if (futureFilter != null)
				{
					throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Cannot specify non-null {0} when {1} is {2}.", new object[] { "futureFilter", "aggregationCycleKind", aggregationCycleKind })));
				}
				return this.CycleAggregators(ref this.aggregatorsForDefault, tactTimestamp, futureFilter, false);
			case MetricAggregationCycleKind.QuickPulse:
				return this.CycleAggregators(ref this.aggregatorsForQuickPulse, tactTimestamp, futureFilter, false);
			case MetricAggregationCycleKind.Custom:
				return this.CycleAggregators(ref this.aggregatorsForCustom, tactTimestamp, futureFilter, false);
			default:
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Unexpected value of {0}: {1}.", new object[] { "aggregationCycleKind", aggregationCycleKind })));
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000085EC File Offset: 0x000067EC
		public AggregationPeriodSummary StopAggregators(MetricAggregationCycleKind aggregationCycleKind, DateTimeOffset tactTimestamp)
		{
			switch (aggregationCycleKind)
			{
			case MetricAggregationCycleKind.Default:
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Cannot invoke {0} for Default {1}: Default aggregators are always active.", new object[] { "StopAggregators", "MetricAggregationCycleKind" })));
			case MetricAggregationCycleKind.QuickPulse:
				return this.CycleAggregators(ref this.aggregatorsForQuickPulse, tactTimestamp, null, true);
			case MetricAggregationCycleKind.Custom:
				return this.CycleAggregators(ref this.aggregatorsForCustom, tactTimestamp, null, true);
			default:
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Unexpected value of {0}: {1}.", new object[] { "aggregationCycleKind", aggregationCycleKind })));
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008684 File Offset: 0x00006884
		internal bool IsCycleActive(MetricAggregationCycleKind aggregationCycleKind, out IMetricSeriesFilter filter)
		{
			switch (aggregationCycleKind)
			{
			case MetricAggregationCycleKind.Default:
				filter = null;
				return true;
			case MetricAggregationCycleKind.QuickPulse:
			{
				MetricAggregationManager.AggregatorCollection aggregatorCollection = this.aggregatorsForQuickPulse;
				filter = ((aggregatorCollection != null) ? aggregatorCollection.Filter : null);
				return aggregatorCollection != null;
			}
			case MetricAggregationCycleKind.Custom:
			{
				MetricAggregationManager.AggregatorCollection aggregatorCollection2 = this.aggregatorsForCustom;
				filter = ((aggregatorCollection2 != null) ? aggregatorCollection2.Filter : null);
				return aggregatorCollection2 != null;
			}
			default:
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Unexpected value of {0}: {1}.", new object[] { "aggregationCycleKind", aggregationCycleKind })));
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000870C File Offset: 0x0000690C
		internal bool AddAggregator(IMetricSeriesAggregator aggregator, MetricAggregationCycleKind aggregationCycleKind)
		{
			Util.ValidateNotNull(aggregator, "aggregator");
			if (aggregator.DataSeries.configuration.RequiresPersistentAggregation)
			{
				return MetricAggregationManager.AddAggregator(aggregator, this.aggregatorsForPersistent);
			}
			switch (aggregationCycleKind)
			{
			case MetricAggregationCycleKind.Default:
				return MetricAggregationManager.AddAggregator(aggregator, this.aggregatorsForDefault);
			case MetricAggregationCycleKind.QuickPulse:
				return MetricAggregationManager.AddAggregator(aggregator, this.aggregatorsForQuickPulse);
			case MetricAggregationCycleKind.Custom:
				return MetricAggregationManager.AddAggregator(aggregator, this.aggregatorsForCustom);
			default:
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Unexpected value of {0}: {1}.", new object[] { "aggregationCycleKind", aggregationCycleKind })));
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000087AC File Offset: 0x000069AC
		private static bool AddAggregator(IMetricSeriesAggregator aggregator, MetricAggregationManager.AggregatorCollection aggregatorCollection)
		{
			if (aggregatorCollection == null)
			{
				return false;
			}
			IMetricValueFilter metricValueFilter;
			if (!Util.FilterWillConsume(aggregatorCollection.Filter, aggregator.DataSeries, out metricValueFilter))
			{
				return false;
			}
			aggregator.Reset(aggregatorCollection.PeriodStart);
			aggregatorCollection.Aggregators.Add(aggregator);
			return true;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000087F0 File Offset: 0x000069F0
		private static List<MetricAggregate> GetNonpersistentAggregations(DateTimeOffset tactTimestamp, MetricAggregationManager.AggregatorCollection aggregators)
		{
			GrowingCollection<IMetricSeriesAggregator> growingCollection = ((aggregators != null) ? aggregators.Aggregators : null);
			if (growingCollection == null || growingCollection.Count == 0)
			{
				return new List<MetricAggregate>(0);
			}
			List<MetricAggregate> list = new List<MetricAggregate>(growingCollection.Count);
			foreach (IMetricSeriesAggregator metricSeriesAggregator in growingCollection)
			{
				if (metricSeriesAggregator != null)
				{
					MetricAggregate metricAggregate = metricSeriesAggregator.CompleteAggregation(tactTimestamp);
					if (metricAggregate != null)
					{
						list.Add(metricAggregate);
					}
				}
			}
			return list;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008878 File Offset: 0x00006A78
		private AggregationPeriodSummary CycleAggregators(ref MetricAggregationManager.AggregatorCollection aggregators, DateTimeOffset tactTimestamp, IMetricSeriesFilter futureFilter, bool stopAggregators)
		{
			if (aggregators == this.aggregatorsForPersistent)
			{
				throw new InvalidOperationException("Internal SDK bug. Please report. Cannot cycle persistent aggregators.");
			}
			tactTimestamp = Util.RoundDownToSecond(tactTimestamp);
			MetricAggregationManager.AggregatorCollection aggregatorCollection;
			if (stopAggregators)
			{
				aggregatorCollection = Interlocked.Exchange<MetricAggregationManager.AggregatorCollection>(ref aggregators, null);
			}
			else
			{
				MetricAggregationManager.AggregatorCollection aggregatorCollection2 = new MetricAggregationManager.AggregatorCollection(tactTimestamp, futureFilter);
				aggregatorCollection = Interlocked.Exchange<MetricAggregationManager.AggregatorCollection>(ref aggregators, aggregatorCollection2);
			}
			IReadOnlyList<MetricAggregate> persistentAggregations = this.GetPersistentAggregations(tactTimestamp, (aggregatorCollection != null) ? aggregatorCollection.Filter : null);
			List<MetricAggregate> nonpersistentAggregations = MetricAggregationManager.GetNonpersistentAggregations(tactTimestamp, aggregatorCollection);
			return new AggregationPeriodSummary(persistentAggregations, nonpersistentAggregations);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000088E4 File Offset: 0x00006AE4
		private List<MetricAggregate> GetPersistentAggregations(DateTimeOffset tactTimestamp, IMetricSeriesFilter previousFilter)
		{
			GrowingCollection<IMetricSeriesAggregator>.Enumerator enumerator = this.aggregatorsForPersistent.Aggregators.GetEnumerator();
			List<MetricAggregate> list = new List<MetricAggregate>(enumerator.Count);
			try
			{
				while (enumerator.MoveNext())
				{
					IMetricSeriesAggregator metricSeriesAggregator = enumerator.Current;
					IMetricValueFilter metricValueFilter;
					if (metricSeriesAggregator != null && Util.FilterWillConsume(previousFilter, metricSeriesAggregator.DataSeries, out metricValueFilter))
					{
						MetricAggregate metricAggregate = metricSeriesAggregator.CompleteAggregation(tactTimestamp);
						if (metricAggregate != null)
						{
							list.Add(metricAggregate);
						}
					}
				}
			}
			finally
			{
				enumerator.Dispose();
			}
			return list;
		}

		// Token: 0x040000A7 RID: 167
		private MetricAggregationManager.AggregatorCollection aggregatorsForPersistent;

		// Token: 0x040000A8 RID: 168
		private MetricAggregationManager.AggregatorCollection aggregatorsForDefault;

		// Token: 0x040000A9 RID: 169
		private MetricAggregationManager.AggregatorCollection aggregatorsForQuickPulse;

		// Token: 0x040000AA RID: 170
		private MetricAggregationManager.AggregatorCollection aggregatorsForCustom;

		// Token: 0x020000F0 RID: 240
		private class AggregatorCollection
		{
			// Token: 0x06000892 RID: 2194 RVA: 0x0001B8ED File Offset: 0x00019AED
			public AggregatorCollection(DateTimeOffset periodStart, IMetricSeriesFilter filter)
			{
				this.PeriodStart = periodStart;
				this.Aggregators = new GrowingCollection<IMetricSeriesAggregator>();
				this.Filter = filter;
			}

			// Token: 0x17000272 RID: 626
			// (get) Token: 0x06000893 RID: 2195 RVA: 0x0001B90E File Offset: 0x00019B0E
			public DateTimeOffset PeriodStart { get; }

			// Token: 0x17000273 RID: 627
			// (get) Token: 0x06000894 RID: 2196 RVA: 0x0001B916 File Offset: 0x00019B16
			public GrowingCollection<IMetricSeriesAggregator> Aggregators { get; }

			// Token: 0x17000274 RID: 628
			// (get) Token: 0x06000895 RID: 2197 RVA: 0x0001B91E File Offset: 0x00019B1E
			public IMetricSeriesFilter Filter { get; }
		}
	}
}
