using System;
using System.Runtime.CompilerServices;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200037F RID: 895
	public sealed class MinMaxDblAggregator : IColumnAggregator<VBuffer<double>>
	{
		// Token: 0x06001340 RID: 4928 RVA: 0x0006C94C File Offset: 0x0006AB4C
		public MinMaxDblAggregator(int size)
		{
			Contracts.Check(size > 0);
			this._min = new double[size];
			this._max = new double[size];
			this._vCount = new long[size];
			for (int i = 0; i < size; i++)
			{
				this._min[i] = double.PositiveInfinity;
				this._max[i] = double.NegativeInfinity;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x0006C9BA File Offset: 0x0006ABBA
		public double[] Min
		{
			get
			{
				return this._min;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x0006C9C2 File Offset: 0x0006ABC2
		public double[] Max
		{
			get
			{
				return this._max;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0006C9CA File Offset: 0x0006ABCA
		public long[] Count
		{
			get
			{
				return this._vCount;
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0006C9D4 File Offset: 0x0006ABD4
		public void ProcessValue(ref VBuffer<double> value)
		{
			int num = this._min.Length;
			Contracts.Check(value.Length == num);
			this._trainCount += 1L;
			int count = value.Count;
			if (count == 0)
			{
				return;
			}
			if (count == num)
			{
				double[] values = value.Values;
				for (int i = 0; i < count; i++)
				{
					double num2 = values[i];
					this._vCount[i] += 1L;
					this.Update(i, num2);
				}
				return;
			}
			int[] indices = value.Indices;
			double[] values2 = value.Values;
			for (int j = 0; j < count; j++)
			{
				double num3 = values2[j];
				int num4 = indices[j];
				this._vCount[num4] += 1L;
				this.Update(num4, num3);
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0006CAA8 File Offset: 0x0006ACA8
		public void Finish()
		{
			int num = this._min.Length;
			for (int i = 0; i < num; i++)
			{
				if (this._vCount[i] < this._trainCount)
				{
					this.Update(i, 0.0);
				}
			}
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0006CAEA File Offset: 0x0006ACEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Update(int j, double val)
		{
			if (this._max[j] < val)
			{
				this._max[j] = val;
			}
			if (this._min[j] > val)
			{
				this._min[j] = val;
			}
		}

		// Token: 0x04000B12 RID: 2834
		private readonly double[] _min;

		// Token: 0x04000B13 RID: 2835
		private readonly double[] _max;

		// Token: 0x04000B14 RID: 2836
		private readonly long[] _vCount;

		// Token: 0x04000B15 RID: 2837
		private long _trainCount;
	}
}
