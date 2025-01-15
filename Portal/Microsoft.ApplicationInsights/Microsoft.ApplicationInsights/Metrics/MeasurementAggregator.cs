using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.ApplicationInsights.Metrics.Extensibility;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x02000029 RID: 41
	internal sealed class MeasurementAggregator : MetricSeriesAggregatorBase<double>
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00007FD0 File Offset: 0x000061D0
		public MeasurementAggregator(MetricSeriesConfigurationForMeasurement configuration, MetricSeries dataSeries, MetricAggregationCycleKind aggregationCycleKind)
			: base(MeasurementAggregator.MetricValuesBufferFactory, configuration, dataSeries, aggregationCycleKind)
		{
			Util.ValidateNotNull(configuration, "configuration");
			this.restrictToUInt32Values = configuration.RestrictToUInt32Values;
			this.ResetAggregate();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000801E File Offset: 0x0000621E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override double ConvertMetricValue(double metricValue)
		{
			if (this.restrictToUInt32Values)
			{
				return Util.RoundAndValidateValue(metricValue);
			}
			return metricValue;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008030 File Offset: 0x00006230
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override double ConvertMetricValue(object metricValue)
		{
			if (metricValue == null)
			{
				return double.NaN;
			}
			double num = Util.ConvertToDoubleValue(metricValue);
			return this.ConvertMetricValue(num);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008058 File Offset: 0x00006258
		protected override MetricAggregate CreateAggregate(DateTimeOffset periodEnd)
		{
			object obj = this.updateLock;
			int count;
			double num;
			double num2;
			double num3;
			double num4;
			lock (obj)
			{
				count = this.data.Count;
				num = this.data.Sum;
				num2 = 0.0;
				num3 = 0.0;
				num4 = 0.0;
				if (count > 0)
				{
					num2 = this.data.Min;
					num3 = this.data.Max;
					if (double.IsInfinity(this.data.SumOfSquares) || double.IsInfinity(num))
					{
						num4 = double.NaN;
					}
					else
					{
						double num5 = num / (double)count;
						num4 = Math.Sqrt(this.data.SumOfSquares / (double)count - num5 * num5);
					}
				}
			}
			num = Util.EnsureConcreteValue(num);
			if (count > 0)
			{
				num2 = Util.EnsureConcreteValue(num2);
				num3 = Util.EnsureConcreteValue(num3);
				num4 = Util.EnsureConcreteValue(num4);
			}
			MetricSeries dataSeries = base.DataSeries;
			string text = ((dataSeries != null) ? dataSeries.MetricIdentifier.MetricNamespace : null) ?? string.Empty;
			MetricSeries dataSeries2 = base.DataSeries;
			MetricAggregate metricAggregate = new MetricAggregate(text, ((dataSeries2 != null) ? dataSeries2.MetricIdentifier.MetricId : null) ?? "null", "Microsoft.Azure.Measurement");
			metricAggregate.Data["Count"] = count;
			metricAggregate.Data["Sum"] = num;
			metricAggregate.Data["Min"] = num2;
			metricAggregate.Data["Max"] = num3;
			metricAggregate.Data["StdDev"] = num4;
			base.AddInfo_Timing_Dimensions_Context(metricAggregate, periodEnd);
			return metricAggregate;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008220 File Offset: 0x00006420
		protected override void ResetAggregate()
		{
			object obj = this.updateLock;
			lock (obj)
			{
				this.data.Count = 0;
				this.data.Min = double.MaxValue;
				this.data.Max = double.MinValue;
				this.data.Sum = 0.0;
				this.data.SumOfSquares = 0.0;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000082B8 File Offset: 0x000064B8
		[SuppressMessage("Microsoft.Design", "CA1062", Justification = "buffer is validated by base")]
		protected override object UpdateAggregate_Stage1(MetricValuesBufferBase<double> buffer, int minFlushIndex, int maxFlushIndex)
		{
			MeasurementAggregator.Data data = new MeasurementAggregator.Data();
			data.Count = 0;
			data.Min = double.MaxValue;
			data.Max = double.MinValue;
			data.Sum = 0.0;
			data.SumOfSquares = 0.0;
			for (int i = minFlushIndex; i <= maxFlushIndex; i++)
			{
				double andResetValue = buffer.GetAndResetValue(i);
				if (!double.IsNaN(andResetValue))
				{
					data.Count++;
					data.Max = ((andResetValue > data.Max) ? andResetValue : data.Max);
					data.Min = ((andResetValue < data.Min) ? andResetValue : data.Min);
					data.Sum += andResetValue;
					data.SumOfSquares += andResetValue * andResetValue;
				}
			}
			if (this.restrictToUInt32Values)
			{
				data.Max = Math.Round(data.Max);
				data.Min = Math.Round(data.Min);
			}
			return data;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000083B4 File Offset: 0x000065B4
		protected override void UpdateAggregate_Stage2(object stage1Result)
		{
			MeasurementAggregator.Data data = (MeasurementAggregator.Data)stage1Result;
			if (data.Count == 0)
			{
				return;
			}
			object obj = this.updateLock;
			lock (obj)
			{
				this.data.Count += data.Count;
				this.data.Max = ((data.Max > this.data.Max) ? data.Max : this.data.Max);
				this.data.Min = ((data.Min < this.data.Min) ? data.Min : this.data.Min);
				this.data.Sum += data.Sum;
				this.data.SumOfSquares += data.SumOfSquares;
				if (this.restrictToUInt32Values)
				{
					this.data.Sum = Math.Round(this.data.Sum);
					this.data.SumOfSquares = Math.Round(this.data.SumOfSquares);
				}
			}
		}

		// Token: 0x040000A3 RID: 163
		private static readonly Func<MetricValuesBufferBase<double>> MetricValuesBufferFactory = () => new MetricValuesBuffer_Double(500);

		// Token: 0x040000A4 RID: 164
		private readonly object updateLock = new object();

		// Token: 0x040000A5 RID: 165
		private readonly bool restrictToUInt32Values;

		// Token: 0x040000A6 RID: 166
		private readonly MeasurementAggregator.Data data = new MeasurementAggregator.Data();

		// Token: 0x020000EE RID: 238
		private class Data
		{
			// Token: 0x0400034F RID: 847
			public int Count;

			// Token: 0x04000350 RID: 848
			public double Min;

			// Token: 0x04000351 RID: 849
			public double Max;

			// Token: 0x04000352 RID: 850
			public double Sum;

			// Token: 0x04000353 RID: 851
			public double SumOfSquares;
		}
	}
}
