using System;
using System.Collections.Generic;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000041 RID: 65
	internal sealed class ExponentialHistogramAggregator : Aggregator
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00008EA8 File Offset: 0x000070A8
		public ExponentialHistogramAggregator(QuantileAggregation config)
		{
			this._config = config;
			this._counters = new int[4096][];
			if (this._config.MaxRelativeError < 0.0001)
			{
				throw new ArgumentException();
			}
			int num = (int)Math.Ceiling(Math.Log(1.0 / this._config.MaxRelativeError, 2.0)) - 1;
			this._mantissaShift = 52 - num;
			this._mantissaMax = 1 << num;
			this._mantissaMask = this._mantissaMax - 1;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008F40 File Offset: 0x00007140
		public override IAggregationStatistics Collect()
		{
			int[][] counters;
			int num;
			lock (this)
			{
				counters = this._counters;
				num = this._count;
				this._counters = new int[4096][];
				this._count = 0;
			}
			QuantileValue[] array = new QuantileValue[this._config.Quantiles.Length];
			int num2 = 0;
			if (num2 == this._config.Quantiles.Length)
			{
				return new HistogramStatistics(array);
			}
			num -= this.GetInvalidCount(counters);
			int num3 = this.QuantileToRank(this._config.Quantiles[num2], num);
			int i = 0;
			foreach (ExponentialHistogramAggregator.Bucket bucket in this.IterateBuckets(counters))
			{
				i += bucket.Count;
				while (i > num3)
				{
					array[num2] = new QuantileValue(this._config.Quantiles[num2], bucket.Value);
					num2++;
					if (num2 == this._config.Quantiles.Length)
					{
						return new HistogramStatistics(array);
					}
					num3 = this.QuantileToRank(this._config.Quantiles[num2], num);
				}
			}
			return new HistogramStatistics(Array.Empty<QuantileValue>());
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000090A0 File Offset: 0x000072A0
		private int GetInvalidCount(int[][] counters)
		{
			int[] array = counters[2047];
			int[] array2 = counters[4095];
			int num = 0;
			if (array != null)
			{
				foreach (int num2 in array)
				{
					num += num2;
				}
			}
			if (array2 != null)
			{
				foreach (int num3 in array2)
				{
					num += num3;
				}
			}
			return num;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009107 File Offset: 0x00007307
		private IEnumerable<ExponentialHistogramAggregator.Bucket> IterateBuckets(int[][] counters)
		{
			int num2;
			for (int exponent = 4094; exponent >= 2048; exponent = num2 - 1)
			{
				int[] mantissaCounts = counters[exponent];
				if (mantissaCounts != null)
				{
					for (int mantissa = this._mantissaMax - 1; mantissa >= 0; mantissa = num2 - 1)
					{
						int num = mantissaCounts[mantissa];
						if (num > 0)
						{
							yield return new ExponentialHistogramAggregator.Bucket(this.GetBucketCanonicalValue(exponent, mantissa), num);
						}
						num2 = mantissa;
					}
					mantissaCounts = null;
				}
				num2 = exponent;
			}
			for (int exponent = 0; exponent < 2047; exponent = num2 + 1)
			{
				int[] mantissaCounts = counters[exponent];
				if (mantissaCounts != null)
				{
					for (int mantissa = 0; mantissa < this._mantissaMax; mantissa = num2 + 1)
					{
						int num3 = mantissaCounts[mantissa];
						if (num3 > 0)
						{
							yield return new ExponentialHistogramAggregator.Bucket(this.GetBucketCanonicalValue(exponent, mantissa), num3);
						}
						num2 = mantissa;
					}
					mantissaCounts = null;
				}
				num2 = exponent;
			}
			yield break;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009120 File Offset: 0x00007320
		public override void Update(double measurement)
		{
			lock (this)
			{
				ulong num = (ulong)BitConverter.DoubleToInt64Bits(measurement);
				int num2 = (int)(num >> 52);
				int num3 = (int)(num >> this._mantissaShift) & this._mantissaMask;
				ref int[] ptr = ref this._counters[num2];
				if (ptr == null)
				{
					ptr = new int[this._mantissaMax];
				}
				ptr[num3]++;
				this._count++;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000091B8 File Offset: 0x000073B8
		private int QuantileToRank(double quantile, int count)
		{
			return Math.Min(Math.Max(0, (int)(quantile * (double)count)), count - 1);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000091D0 File Offset: 0x000073D0
		private double GetBucketCanonicalValue(int exponent, int mantissa)
		{
			long num = ((long)exponent << 52) | ((long)mantissa << this._mantissaShift);
			return BitConverter.Int64BitsToDouble(num);
		}

		// Token: 0x040000EB RID: 235
		private const int ExponentArraySize = 4096;

		// Token: 0x040000EC RID: 236
		private const int ExponentShift = 52;

		// Token: 0x040000ED RID: 237
		private const double MinRelativeError = 0.0001;

		// Token: 0x040000EE RID: 238
		private readonly QuantileAggregation _config;

		// Token: 0x040000EF RID: 239
		private int[][] _counters;

		// Token: 0x040000F0 RID: 240
		private int _count;

		// Token: 0x040000F1 RID: 241
		private readonly int _mantissaMax;

		// Token: 0x040000F2 RID: 242
		private readonly int _mantissaMask;

		// Token: 0x040000F3 RID: 243
		private readonly int _mantissaShift;

		// Token: 0x02000094 RID: 148
		private struct Bucket
		{
			// Token: 0x060003C0 RID: 960 RVA: 0x0000D50F File Offset: 0x0000B70F
			public Bucket(double value, int count)
			{
				this.Value = value;
				this.Count = count;
			}

			// Token: 0x040001B4 RID: 436
			public double Value;

			// Token: 0x040001B5 RID: 437
			public int Count;
		}
	}
}
