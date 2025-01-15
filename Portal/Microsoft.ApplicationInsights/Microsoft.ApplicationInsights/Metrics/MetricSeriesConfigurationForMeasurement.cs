using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.ApplicationInsights.Metrics.Extensibility;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x02000033 RID: 51
	public class MetricSeriesConfigurationForMeasurement : IMetricSeriesConfiguration, IEquatable<IMetricSeriesConfiguration>
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000A8B4 File Offset: 0x00008AB4
		static MetricSeriesConfigurationForMeasurement()
		{
			MetricAggregateToTelemetryPipelineConverters.Registry.Add(typeof(ApplicationInsightsTelemetryPipeline), "Microsoft.Azure.Measurement", new MeasurementAggregateToApplicationInsightsPipelineConverter());
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A8D4 File Offset: 0x00008AD4
		public MetricSeriesConfigurationForMeasurement(bool restrictToUInt32Values)
		{
			this.restrictToUInt32Values = restrictToUInt32Values;
			this.hashCode = Util.CombineHashCodes(this.restrictToUInt32Values.GetHashCode());
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000A8F9 File Offset: 0x00008AF9
		public bool RequiresPersistentAggregation
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return false;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000A8FC File Offset: 0x00008AFC
		public bool RestrictToUInt32Values
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.restrictToUInt32Values;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A904 File Offset: 0x00008B04
		public IMetricSeriesAggregator CreateNewAggregator(MetricSeries dataSeries, MetricAggregationCycleKind aggregationCycleKind)
		{
			return new MeasurementAggregator(this, dataSeries, aggregationCycleKind);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000A910 File Offset: 0x00008B10
		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				MetricSeriesConfigurationForMeasurement metricSeriesConfigurationForMeasurement = obj as MetricSeriesConfigurationForMeasurement;
				if (metricSeriesConfigurationForMeasurement != null)
				{
					return this.Equals(metricSeriesConfigurationForMeasurement);
				}
			}
			return false;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000A933 File Offset: 0x00008B33
		public bool Equals(IMetricSeriesConfiguration other)
		{
			return this.Equals(other);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A93C File Offset: 0x00008B3C
		public bool Equals(MetricSeriesConfigurationForMeasurement other)
		{
			return other != null && (this == other || this.RestrictToUInt32Values == other.RestrictToUInt32Values);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A957 File Offset: 0x00008B57
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x040000E0 RID: 224
		private readonly bool restrictToUInt32Values;

		// Token: 0x040000E1 RID: 225
		private readonly int hashCode;

		// Token: 0x020000F3 RID: 243
		[EditorBrowsable(EditorBrowsableState.Never)]
		public sealed class AggregateKindConstants
		{
			// Token: 0x060008A0 RID: 2208 RVA: 0x0001BA4F File Offset: 0x00019C4F
			private AggregateKindConstants()
			{
			}

			// Token: 0x17000277 RID: 631
			// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0001BA57 File Offset: 0x00019C57
			[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Part of Public API and too late to change.")]
			public string AggregateKindMoniker
			{
				get
				{
					return "Microsoft.Azure.Measurement";
				}
			}

			// Token: 0x17000278 RID: 632
			// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0001BA5E File Offset: 0x00019C5E
			[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Part of Public API and too late to change.")]
			public MetricSeriesConfigurationForMeasurement.AggregateKindConstants.DataKeysConstants AggregateKindDataKeys
			{
				get
				{
					return MetricSeriesConfigurationForMeasurement.AggregateKindConstants.DataKeysConstants.Instance;
				}
			}

			// Token: 0x04000360 RID: 864
			internal static readonly MetricSeriesConfigurationForMeasurement.AggregateKindConstants Instance = new MetricSeriesConfigurationForMeasurement.AggregateKindConstants();

			// Token: 0x02000138 RID: 312
			[EditorBrowsable(EditorBrowsableState.Never)]
			public sealed class DataKeysConstants
			{
				// Token: 0x06000956 RID: 2390 RVA: 0x0001E246 File Offset: 0x0001C446
				private DataKeysConstants()
				{
				}

				// Token: 0x17000282 RID: 642
				// (get) Token: 0x06000957 RID: 2391 RVA: 0x0001E24E File Offset: 0x0001C44E
				[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Part of Public API and too late to change.")]
				public string Count
				{
					get
					{
						return "Count";
					}
				}

				// Token: 0x17000283 RID: 643
				// (get) Token: 0x06000958 RID: 2392 RVA: 0x0001E255 File Offset: 0x0001C455
				[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Part of Public API and too late to change.")]
				public string Sum
				{
					get
					{
						return "Sum";
					}
				}

				// Token: 0x17000284 RID: 644
				// (get) Token: 0x06000959 RID: 2393 RVA: 0x0001E25C File Offset: 0x0001C45C
				[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Part of Public API and too late to change.")]
				public string Min
				{
					get
					{
						return "Min";
					}
				}

				// Token: 0x17000285 RID: 645
				// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001E263 File Offset: 0x0001C463
				[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Part of Public API and too late to change.")]
				public string Max
				{
					get
					{
						return "Max";
					}
				}

				// Token: 0x17000286 RID: 646
				// (get) Token: 0x0600095B RID: 2395 RVA: 0x0001E26A File Offset: 0x0001C46A
				[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Part of Public API and too late to change.")]
				public string StdDev
				{
					get
					{
						return "StdDev";
					}
				}

				// Token: 0x04000474 RID: 1140
				internal static readonly MetricSeriesConfigurationForMeasurement.AggregateKindConstants.DataKeysConstants Instance = new MetricSeriesConfigurationForMeasurement.AggregateKindConstants.DataKeysConstants();
			}
		}

		// Token: 0x020000F4 RID: 244
		internal static class Constants
		{
			// Token: 0x04000361 RID: 865
			public const string AggregateKindMoniker = "Microsoft.Azure.Measurement";

			// Token: 0x02000139 RID: 313
			public static class AggregateKindDataKeys
			{
				// Token: 0x04000475 RID: 1141
				public const string Count = "Count";

				// Token: 0x04000476 RID: 1142
				public const string Sum = "Sum";

				// Token: 0x04000477 RID: 1143
				public const string Min = "Min";

				// Token: 0x04000478 RID: 1144
				public const string Max = "Max";

				// Token: 0x04000479 RID: 1145
				public const string StdDev = "StdDev";
			}
		}
	}
}
