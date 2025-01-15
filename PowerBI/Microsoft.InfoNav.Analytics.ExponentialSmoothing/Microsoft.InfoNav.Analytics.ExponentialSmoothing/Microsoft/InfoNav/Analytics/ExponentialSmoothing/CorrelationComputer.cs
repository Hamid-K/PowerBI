using System;

namespace Microsoft.InfoNav.Analytics.ExponentialSmoothing
{
	// Token: 0x02000009 RID: 9
	internal sealed class CorrelationComputer
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000040FA File Offset: 0x000022FA
		internal CorrelationComputer()
		{
			this.m_rgxnumData = null;
			this.m_dataSize = 0U;
			this.m_nextPowerOfTwo = 0U;
			this.m_rgxnumPreparedData = null;
			this.m_rgxnumLaggedCorrelation = null;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004128 File Offset: 0x00002328
		internal int Init(double[] rgxnumData, uint dataSize)
		{
			if (rgxnumData == null || dataSize == 0U)
			{
				throw new ArgumentException("rgxnumData == null || cDataSize == 0");
			}
			if (this.m_rgxnumPreparedData != null || this.m_rgxnumLaggedCorrelation != null)
			{
				throw new ArgumentException("expecting m_rgxnumPreparedData == null && m_rgxnumLaggedCorrelation == null");
			}
			this.m_dataSize = dataSize;
			this.m_rgxnumData = rgxnumData;
			if ((this.m_dataSize & (this.m_dataSize - 1U)) == 0U)
			{
				this.m_nextPowerOfTwo = this.m_dataSize << 1;
			}
			else
			{
				this.m_nextPowerOfTwo = 1U;
				for (;;)
				{
					dataSize >>= 1;
					if (dataSize == 0U)
					{
						break;
					}
					this.m_nextPowerOfTwo <<= 1;
				}
				this.m_nextPowerOfTwo <<= 2;
			}
			this.m_rgxnumLaggedCorrelation = new double[this.m_dataSize];
			int num = this.PrepareData();
			if (SeasonalityDetector.Fail(num))
			{
				this.m_rgxnumLaggedCorrelation = null;
			}
			return num;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000041E3 File Offset: 0x000023E3
		internal void Destroy()
		{
			if (this.m_rgxnumPreparedData != null)
			{
				this.m_rgxnumPreparedData = null;
			}
			if (this.m_rgxnumLaggedCorrelation != null)
			{
				this.m_rgxnumLaggedCorrelation = null;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004203 File Offset: 0x00002403
		internal int GetLaggedCorrelation(uint lag, ref double xnumCorrelation)
		{
			int num = 0;
			xnumCorrelation = this.m_rgxnumLaggedCorrelation[(int)lag];
			return num;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004210 File Offset: 0x00002410
		internal int PrepareData()
		{
			double num = 0.0;
			this.m_rgxnumPreparedData = new double[this.m_nextPowerOfTwo];
			for (uint num2 = 0U; num2 < this.m_dataSize; num2 += 1U)
			{
				num = this.m_rgxnumData[(int)num2] + num;
			}
			num /= this.m_dataSize;
			if (num > 5E-324)
			{
				for (uint num2 = 0U; num2 < this.m_dataSize; num2 += 1U)
				{
					this.m_rgxnumPreparedData[(int)num2] = this.m_rgxnumData[(int)num2] - num;
				}
			}
			else
			{
				int num3 = (int)Math.Min(this.m_dataSize, this.m_nextPowerOfTwo);
				uint num2 = 0U;
				while ((ulong)num2 < (ulong)((long)num3))
				{
					this.m_rgxnumPreparedData[(int)num2] = this.m_rgxnumData[(int)num2];
					num2 += 1U;
				}
			}
			int num4 = this.ComputeCorrelation();
			if (SeasonalityDetector.Fail(num4))
			{
				this.m_rgxnumPreparedData = null;
			}
			return num4;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000042D8 File Offset: 0x000024D8
		private int ComputeCorrelation()
		{
			FourierTransform fourierTransform = new FourierTransform();
			fourierTransform.Init(this.m_nextPowerOfTwo);
			int num = this.FFTForwardBackwardNorm(fourierTransform);
			if (SeasonalityDetector.Fail(num))
			{
				fourierTransform.Destroy();
				return num;
			}
			ComplexNumber[] array = fourierTransform.PxcnumGetDFT();
			for (uint num2 = 0U; num2 < this.m_dataSize; num2 += 1U)
			{
				this.m_rgxnumLaggedCorrelation[(int)num2] = array[(int)num2].real;
			}
			for (uint num2 = 0U; num2 < this.m_nextPowerOfTwo; num2 += 1U)
			{
				this.m_rgxnumPreparedData[(int)num2] = 0.0;
			}
			for (uint num2 = 0U; num2 < this.m_dataSize; num2 += 1U)
			{
				this.m_rgxnumPreparedData[(int)num2] = 1.0;
			}
			num = this.FFTForwardBackwardNorm(fourierTransform);
			array = fourierTransform.PxcnumGetDFT();
			double num3 = this.m_rgxnumLaggedCorrelation[0] / array[0].real / this.m_dataSize;
			this.m_rgxnumLaggedCorrelation[0] = 1.0;
			for (uint num2 = 1U; num2 < this.m_dataSize; num2 += 1U)
			{
				this.m_rgxnumLaggedCorrelation[(int)num2] = this.m_rgxnumLaggedCorrelation[(int)num2] / (array[(int)num2].real * num3 * this.m_dataSize);
			}
			fourierTransform.Destroy();
			return num;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004408 File Offset: 0x00002608
		private int FFTForwardBackwardNorm(FourierTransform fourierTransform)
		{
			int num = 0;
			fourierTransform.ComputeDFT(this.m_rgxnumPreparedData);
			ComplexNumber[] array = fourierTransform.PxcnumGetDFT();
			for (uint num2 = 0U; num2 < this.m_nextPowerOfTwo; num2 += 1U)
			{
				this.m_rgxnumPreparedData[(int)num2] = array[(int)num2].imaginary * array[(int)num2].imaginary + array[(int)num2].real * array[(int)num2].real;
			}
			fourierTransform.ComputeIDFT(this.m_rgxnumPreparedData);
			return num;
		}

		// Token: 0x04000052 RID: 82
		private double[] m_rgxnumData;

		// Token: 0x04000053 RID: 83
		private uint m_dataSize;

		// Token: 0x04000054 RID: 84
		private uint m_nextPowerOfTwo;

		// Token: 0x04000055 RID: 85
		private double[] m_rgxnumPreparedData;

		// Token: 0x04000056 RID: 86
		private double[] m_rgxnumLaggedCorrelation;
	}
}
