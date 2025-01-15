using System;
using System.Runtime.CompilerServices;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000381 RID: 897
	public sealed class MinMaxSngAggregator : IColumnAggregator<VBuffer<float>>
	{
		// Token: 0x06001352 RID: 4946 RVA: 0x0006CDA0 File Offset: 0x0006AFA0
		public MinMaxSngAggregator(int size)
		{
			Contracts.Check(size > 0);
			this._min = new float[size];
			this._max = new float[size];
			this._vCount = new long[size];
			for (int i = 0; i < size; i++)
			{
				this._min[i] = float.PositiveInfinity;
				this._max[i] = float.NegativeInfinity;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0006CE06 File Offset: 0x0006B006
		public float[] Min
		{
			get
			{
				return this._min;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0006CE0E File Offset: 0x0006B00E
		public float[] Max
		{
			get
			{
				return this._max;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x0006CE16 File Offset: 0x0006B016
		public long[] Count
		{
			get
			{
				return this._vCount;
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0006CE20 File Offset: 0x0006B020
		public void ProcessValue(ref VBuffer<float> value)
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
				float[] values = value.Values;
				for (int i = 0; i < count; i++)
				{
					float num2 = values[i];
					this._vCount[i] += 1L;
					this.Update(i, num2);
				}
				return;
			}
			int[] indices = value.Indices;
			float[] values2 = value.Values;
			for (int j = 0; j < count; j++)
			{
				float num3 = values2[j];
				int num4 = indices[j];
				this._vCount[num4] += 1L;
				this.Update(num4, num3);
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0006CEF4 File Offset: 0x0006B0F4
		public void Finish()
		{
			int num = this._min.Length;
			for (int i = 0; i < num; i++)
			{
				if (this._vCount[i] < this._trainCount)
				{
					this.Update(i, 0f);
				}
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0006CF32 File Offset: 0x0006B132
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Update(int j, float val)
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

		// Token: 0x04000B1C RID: 2844
		private readonly float[] _min;

		// Token: 0x04000B1D RID: 2845
		private readonly float[] _max;

		// Token: 0x04000B1E RID: 2846
		private readonly long[] _vCount;

		// Token: 0x04000B1F RID: 2847
		private long _trainCount;
	}
}
