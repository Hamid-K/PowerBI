using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x02000049 RID: 73
	internal abstract class MetricSeriesAggregatorBase<TBufferedValue> : IMetricSeriesAggregator
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		protected MetricSeriesAggregatorBase(Func<MetricValuesBufferBase<TBufferedValue>> metricValuesBufferFactory, IMetricSeriesConfiguration configuration, MetricSeries dataSeries, MetricAggregationCycleKind aggregationCycleKind)
		{
			Util.ValidateNotNull(metricValuesBufferFactory, "metricValuesBufferFactory");
			Util.ValidateNotNull(configuration, "configuration");
			this.dataSeries = dataSeries;
			this.aggregationCycleKind = aggregationCycleKind;
			this.isPersistent = configuration.RequiresPersistentAggregation;
			this.metricValuesBufferFactory = metricValuesBufferFactory;
			this.metricValuesBuffer = this.InvokeMetricValuesBufferFactory();
			this.Reset(default(DateTimeOffset), null);
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000C811 File Offset: 0x0000AA11
		public MetricSeries DataSeries
		{
			get
			{
				return this.dataSeries;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000C819 File Offset: 0x0000AA19
		public MetricAggregate CompleteAggregation(DateTimeOffset periodEnd)
		{
			if (!this.isPersistent)
			{
				this.DataSeries.ClearAggregator(this.aggregationCycleKind);
			}
			return this.CreateAggregateUnsafe(periodEnd);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000C83B File Offset: 0x0000AA3B
		public void Reset(DateTimeOffset periodStart)
		{
			this.periodStart = periodStart;
			this.metricValuesBuffer.ResetIndicesAndData();
			this.ResetAggregate();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000C857 File Offset: 0x0000AA57
		public void Reset(DateTimeOffset periodStart, IMetricValueFilter valueFilter)
		{
			this.valueFilter = valueFilter;
			this.Reset(periodStart);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000C868 File Offset: 0x0000AA68
		public void TrackValue(double metricValue)
		{
			if (double.IsNaN(metricValue))
			{
				return;
			}
			if (!Util.FilterWillConsume(this.valueFilter, this.dataSeries, metricValue))
			{
				return;
			}
			TBufferedValue tbufferedValue = this.ConvertMetricValue(metricValue);
			this.TrackFilteredConvertedValue(tbufferedValue);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		public void TrackValue(object metricValue)
		{
			if (metricValue == null)
			{
				return;
			}
			if (!Util.FilterWillConsume(this.valueFilter, this.dataSeries, metricValue))
			{
				return;
			}
			TBufferedValue tbufferedValue = this.ConvertMetricValue(metricValue);
			this.TrackFilteredConvertedValue(tbufferedValue);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000C8DC File Offset: 0x0000AADC
		public bool TryRecycle()
		{
			if (this.isPersistent)
			{
				return false;
			}
			this.Reset(default(DateTimeOffset), null);
			return true;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000C904 File Offset: 0x0000AB04
		public MetricAggregate CreateAggregateUnsafe(DateTimeOffset periodEnd)
		{
			this.UpdateAggregate(this.metricValuesBuffer);
			return this.CreateAggregate(periodEnd);
		}

		// Token: 0x06000261 RID: 609
		protected abstract MetricAggregate CreateAggregate(DateTimeOffset periodEnd);

		// Token: 0x06000262 RID: 610
		protected abstract void ResetAggregate();

		// Token: 0x06000263 RID: 611
		protected abstract TBufferedValue ConvertMetricValue(double metricValue);

		// Token: 0x06000264 RID: 612
		protected abstract TBufferedValue ConvertMetricValue(object metricValue);

		// Token: 0x06000265 RID: 613
		protected abstract object UpdateAggregate_Stage1(MetricValuesBufferBase<TBufferedValue> buffer, int minFlushIndex, int maxFlushIndex);

		// Token: 0x06000266 RID: 614
		protected abstract void UpdateAggregate_Stage2(object stage1Result);

		// Token: 0x06000267 RID: 615 RVA: 0x0000C91C File Offset: 0x0000AB1C
		protected void AddInfo_Timing_Dimensions_Context(MetricAggregate aggregate, DateTimeOffset periodEnd)
		{
			if (aggregate == null)
			{
				return;
			}
			aggregate.AggregationPeriodStart = this.periodStart;
			aggregate.AggregationPeriodDuration = periodEnd - this.periodStart;
			if (this.DataSeries != null && this.DataSeries.DimensionNamesAndValues != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.DataSeries.DimensionNamesAndValues)
				{
					aggregate.Dimensions[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		private void TrackFilteredConvertedValue(TBufferedValue metricValue)
		{
			MetricValuesBufferBase<TBufferedValue> metricValuesBufferBase = this.metricValuesBuffer;
			int i = metricValuesBufferBase.IncWriteIndex();
			if (i >= metricValuesBufferBase.Capacity)
			{
				SpinWait spinWait = default(SpinWait);
				metricValuesBufferBase = this.metricValuesBuffer;
				for (i = metricValuesBufferBase.IncWriteIndex(); i >= metricValuesBufferBase.Capacity; i = metricValuesBufferBase.IncWriteIndex())
				{
					spinWait.SpinOnce();
					if (spinWait.Count % 100 == 0)
					{
						Task.Delay(10).ConfigureAwait(false).GetAwaiter()
							.GetResult();
					}
					metricValuesBufferBase = this.metricValuesBuffer;
				}
			}
			metricValuesBufferBase.WriteValue(i, metricValue);
			if (i == metricValuesBufferBase.Capacity - 1)
			{
				MetricValuesBufferBase<TBufferedValue> metricValuesBufferBase2 = Interlocked.Exchange<MetricValuesBufferBase<TBufferedValue>>(ref this.metricValuesBufferRecycle, null);
				if (metricValuesBufferBase2 != null)
				{
					if (Interlocked.CompareExchange<MetricValuesBufferBase<TBufferedValue>>(ref this.metricValuesBuffer, metricValuesBufferBase2, metricValuesBufferBase) == metricValuesBufferBase)
					{
						metricValuesBufferBase2.ResetIndices();
					}
				}
				else
				{
					metricValuesBufferBase2 = this.InvokeMetricValuesBufferFactory();
					Interlocked.CompareExchange<MetricValuesBufferBase<TBufferedValue>>(ref this.metricValuesBuffer, metricValuesBufferBase2, metricValuesBufferBase);
				}
				this.UpdateAggregate(metricValuesBufferBase);
				Interlocked.CompareExchange<MetricValuesBufferBase<TBufferedValue>>(ref this.metricValuesBufferRecycle, metricValuesBufferBase, null);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
		private void UpdateAggregate(MetricValuesBufferBase<TBufferedValue> buffer)
		{
			if (buffer == null)
			{
				return;
			}
			object obj;
			lock (buffer)
			{
				int num = Math.Min(buffer.PeekLastWriteIndex(), buffer.Capacity - 1);
				int nextFlushIndex = buffer.NextFlushIndex;
				if (nextFlushIndex > num)
				{
					return;
				}
				obj = this.UpdateAggregate_Stage1(buffer, nextFlushIndex, num);
				buffer.NextFlushIndex = num + 1;
			}
			this.UpdateAggregate_Stage2(obj);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000CB28 File Offset: 0x0000AD28
		private MetricValuesBufferBase<TBufferedValue> InvokeMetricValuesBufferFactory()
		{
			MetricValuesBufferBase<TBufferedValue> metricValuesBufferBase = this.metricValuesBufferFactory();
			if (metricValuesBufferBase == null)
			{
				throw new InvalidOperationException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0}-delegate returned null. This is not allowed. Bad aggregator?", new object[] { "metricValuesBufferFactory" })));
			}
			return metricValuesBufferBase;
		}

		// Token: 0x04000113 RID: 275
		private readonly MetricSeries dataSeries;

		// Token: 0x04000114 RID: 276
		private readonly MetricAggregationCycleKind aggregationCycleKind;

		// Token: 0x04000115 RID: 277
		private readonly bool isPersistent;

		// Token: 0x04000116 RID: 278
		private readonly Func<MetricValuesBufferBase<TBufferedValue>> metricValuesBufferFactory;

		// Token: 0x04000117 RID: 279
		private DateTimeOffset periodStart;

		// Token: 0x04000118 RID: 280
		private IMetricValueFilter valueFilter;

		// Token: 0x04000119 RID: 281
		private volatile MetricValuesBufferBase<TBufferedValue> metricValuesBuffer;

		// Token: 0x0400011A RID: 282
		private volatile MetricValuesBufferBase<TBufferedValue> metricValuesBufferRecycle;
	}
}
