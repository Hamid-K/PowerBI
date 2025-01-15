using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000380 RID: 896
	public sealed class MeanVarDblAggregator
	{
		// Token: 0x06001347 RID: 4935 RVA: 0x0006CB14 File Offset: 0x0006AD14
		public MeanVarDblAggregator(int size, bool useLog)
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

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0006CB66 File Offset: 0x0006AD66
		public long[] Counts
		{
			get
			{
				return this._cnz;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x0006CB6E File Offset: 0x0006AD6E
		public double[] Mean
		{
			get
			{
				return this._mean;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0006CB88 File Offset: 0x0006AD88
		public double[] StdDev
		{
			get
			{
				return this._m2.Select((double m2, int i) => Math.Sqrt(m2 / (double)this._cnz[i])).ToArray<double>();
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x0006CBB3 File Offset: 0x0006ADB3
		public double[] MeanSquareError
		{
			get
			{
				return this._m2.Select((double m2, int i) => m2 / (double)this._cnz[i]).ToArray<double>();
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x0006CBD1 File Offset: 0x0006ADD1
		public double[] M2
		{
			get
			{
				return this._m2;
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0006CBDC File Offset: 0x0006ADDC
		public void ProcessValue(ref VBuffer<double> value)
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
				double[] values = value.Values;
				for (int i = 0; i < count; i++)
				{
					double num2 = values[i];
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
				this.Update(num4, num3);
			}
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0006CC6C File Offset: 0x0006AE6C
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

		// Token: 0x0600134F RID: 4943 RVA: 0x0006CCD8 File Offset: 0x0006AED8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Update(int j, double origVal)
		{
			if (origVal == 0.0)
			{
				return;
			}
			double num = (this._useLog ? Math.Log(origVal) : origVal);
			if (!FloatUtils.IsFinite(num))
			{
				if (!this._useLog)
				{
					this._cnan[j] += 1L;
				}
				return;
			}
			this._cnz[j] += 1L;
			double num2 = num - this._mean[j];
			this._mean[j] += num2 / (double)this._cnz[j];
			double num3 = num2 * (num - this._mean[j]);
			this._m2[j] += num3;
		}

		// Token: 0x04000B16 RID: 2838
		private readonly bool _useLog;

		// Token: 0x04000B17 RID: 2839
		private readonly double[] _mean;

		// Token: 0x04000B18 RID: 2840
		private readonly double[] _m2;

		// Token: 0x04000B19 RID: 2841
		private readonly long[] _cnan;

		// Token: 0x04000B1A RID: 2842
		private readonly long[] _cnz;

		// Token: 0x04000B1B RID: 2843
		private long _trainCount;
	}
}
