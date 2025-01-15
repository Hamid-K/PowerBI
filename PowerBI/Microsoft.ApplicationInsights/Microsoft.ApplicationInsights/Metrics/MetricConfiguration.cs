using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x0200002D RID: 45
	public class MetricConfiguration : IEquatable<MetricConfiguration>
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000900C File Offset: 0x0000720C
		public MetricConfiguration(int seriesCountLimit, int valuesPerDimensionLimit, IMetricSeriesConfiguration seriesConfig)
		{
			if (seriesCountLimit < 1)
			{
				throw new ArgumentOutOfRangeException("seriesCountLimit", global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Metrics must allow at least one data series (but {0} was specified).", new object[] { seriesCountLimit })));
			}
			this.SeriesCountLimit = seriesCountLimit;
			if (valuesPerDimensionLimit < 0)
			{
				throw new ArgumentOutOfRangeException("valuesPerDimensionLimit");
			}
			for (int i = 0; i < this.valuesPerDimensionLimits.Length; i++)
			{
				this.valuesPerDimensionLimits[i] = valuesPerDimensionLimit;
			}
			Util.ValidateNotNull(seriesConfig, "seriesConfig");
			this.SeriesConfig = seriesConfig;
			this.hashCode = this.ComputeHashCode();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000090AC File Offset: 0x000072AC
		public MetricConfiguration(int seriesCountLimit, IEnumerable<int> valuesPerDimensionLimits, IMetricSeriesConfiguration seriesConfig)
		{
			if (seriesCountLimit < 1)
			{
				throw new ArgumentOutOfRangeException("seriesCountLimit", global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Metrics must allow at least one data series (but {0} was specified).", new object[] { seriesCountLimit })));
			}
			this.SeriesCountLimit = seriesCountLimit;
			if (valuesPerDimensionLimits == null)
			{
				throw new ArgumentNullException("valuesPerDimensionLimits");
			}
			int num = 0;
			int num2 = 0;
			using (IEnumerator<int> enumerator = valuesPerDimensionLimits.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num = enumerator.Current;
					if (num < 0)
					{
						throw new ArgumentOutOfRangeException("valuesPerDimensionLimits[" + num2 + "]");
					}
					this.valuesPerDimensionLimits[num2] = num;
					num2++;
					if (num2 >= 10)
					{
						break;
					}
				}
				goto IL_00BD;
			}
			IL_00B0:
			this.valuesPerDimensionLimits[num2] = num;
			num2++;
			IL_00BD:
			if (num2 >= this.valuesPerDimensionLimits.Length)
			{
				Util.ValidateNotNull(seriesConfig, "seriesConfig");
				this.SeriesConfig = seriesConfig;
				this.hashCode = this.ComputeHashCode();
				return;
			}
			goto IL_00B0;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000091B0 File Offset: 0x000073B0
		public int SeriesCountLimit { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000091B8 File Offset: 0x000073B8
		public IMetricSeriesConfiguration SeriesConfig { get; }

		// Token: 0x06000190 RID: 400 RVA: 0x000091C0 File Offset: 0x000073C0
		[SuppressMessage("Microsoft.Usage", "CA2233: Operations should not overflow", Justification = "No overflow")]
		public int GetValuesPerDimensionLimit(int dimensionNumber)
		{
			MetricIdentifier.ValidateDimensionNumberForGetter(dimensionNumber, 10);
			int num = dimensionNumber - 1;
			return this.valuesPerDimensionLimits[num];
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000091E4 File Offset: 0x000073E4
		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				MetricConfiguration metricConfiguration = obj as MetricConfiguration;
				if (metricConfiguration != null)
				{
					return this.Equals(metricConfiguration);
				}
			}
			return false;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009208 File Offset: 0x00007408
		public virtual bool Equals(MetricConfiguration other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this.SeriesCountLimit == other.SeriesCountLimit)
			{
				int[] array = this.valuesPerDimensionLimits;
				int? num = ((array != null) ? new int?(array.Length) : null);
				int[] array2 = other.valuesPerDimensionLimits;
				int? num2 = ((array2 != null) ? new int?(array2.Length) : null);
				if (((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))) && base.GetType().Equals(other.GetType()) && this.SeriesConfig.Equals(other.SeriesConfig))
				{
					if (this.valuesPerDimensionLimits == other.valuesPerDimensionLimits)
					{
						return true;
					}
					if (this.valuesPerDimensionLimits == null || other.valuesPerDimensionLimits == null)
					{
						return false;
					}
					for (int i = 0; i < this.valuesPerDimensionLimits.Length; i++)
					{
						if (this.valuesPerDimensionLimits[i] != other.valuesPerDimensionLimits[i])
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009300 File Offset: 0x00007500
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00009308 File Offset: 0x00007508
		private int ComputeHashCode()
		{
			return Util.CombineHashCodes(this.SeriesCountLimit.GetHashCode(), Util.CombineHashCodes(this.valuesPerDimensionLimits), this.SeriesConfig.GetType().FullName.GetHashCode(), this.SeriesConfig.GetHashCode());
		}

		// Token: 0x040000B7 RID: 183
		private readonly int hashCode;

		// Token: 0x040000B8 RID: 184
		private readonly int[] valuesPerDimensionLimits = new int[10];
	}
}
