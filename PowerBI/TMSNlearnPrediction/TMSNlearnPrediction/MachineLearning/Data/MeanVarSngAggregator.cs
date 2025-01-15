using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000382 RID: 898
	public sealed class MeanVarSngAggregator
	{
		// Token: 0x06001359 RID: 4953 RVA: 0x0006CF5C File Offset: 0x0006B15C
		public MeanVarSngAggregator(int size, bool useLog)
		{
			this._useLog = useLog;
			this._mean = new double[size];
			this._m2 = new double[size];
			if (!this._useLog)
			{
				this._cnan = new long[size];
			}
			this._cnz = new long[size];
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x0006CFAE File Offset: 0x0006B1AE
		public long[] Counts
		{
			get
			{
				return this._cnz;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x0006CFB6 File Offset: 0x0006B1B6
		public double[] Mean
		{
			get
			{
				return this._mean;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x0006CFD0 File Offset: 0x0006B1D0
		public double[] StdDev
		{
			get
			{
				return this._m2.Select((double m2, int i) => Math.Sqrt(m2 / (double)this._cnz[i])).ToArray<double>();
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0006CFFB File Offset: 0x0006B1FB
		public double[] MeanSquareError
		{
			get
			{
				return this._m2.Select((double m2, int i) => m2 / (double)this._cnz[i]).ToArray<double>();
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0006D019 File Offset: 0x0006B219
		public double[] M2
		{
			get
			{
				return this._m2;
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0006D024 File Offset: 0x0006B224
		public void ProcessValue(ref VBuffer<float> value)
		{
			this._trainCount += 1L;
			int num = this._mean.Length;
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
				this.Update(num4, num3);
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0006D0B4 File Offset: 0x0006B2B4
		public void Finish()
		{
			if (!this._useLog)
			{
				for (int i = 0; i < this._mean.Length; i++)
				{
					MeanVarUtils.AdjustForZeros(ref this._mean[i], ref this._m2[i], ref this._cnz[i], this._trainCount - this._cnan[i] - this._cnz[i]);
				}
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0006D120 File Offset: 0x0006B320
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Update(int j, float origVal)
		{
			if (origVal == 0f)
			{
				return;
			}
			float num = (this._useLog ? ((float)Math.Log((double)origVal)) : origVal);
			if (!FloatUtils.IsFinite(num))
			{
				if (!this._useLog)
				{
					this._cnan[j] += 1L;
				}
				return;
			}
			this._cnz[j] += 1L;
			double num2 = (double)num - this._mean[j];
			this._mean[j] += num2 / (double)this._cnz[j];
			double num3 = num2 * ((double)num - this._mean[j]);
			this._m2[j] += num3;
		}

		// Token: 0x04000B20 RID: 2848
		private readonly bool _useLog;

		// Token: 0x04000B21 RID: 2849
		private readonly double[] _mean;

		// Token: 0x04000B22 RID: 2850
		private readonly double[] _m2;

		// Token: 0x04000B23 RID: 2851
		private readonly long[] _cnan;

		// Token: 0x04000B24 RID: 2852
		private readonly long[] _cnz;

		// Token: 0x04000B25 RID: 2853
		private long _trainCount;
	}
}
