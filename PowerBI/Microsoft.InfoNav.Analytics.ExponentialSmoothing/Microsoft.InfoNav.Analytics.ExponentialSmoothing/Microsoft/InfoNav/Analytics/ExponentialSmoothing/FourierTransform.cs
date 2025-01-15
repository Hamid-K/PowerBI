using System;

namespace Microsoft.InfoNav.Analytics.ExponentialSmoothing
{
	// Token: 0x0200000A RID: 10
	internal sealed class FourierTransform
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00004483 File Offset: 0x00002683
		internal FourierTransform()
		{
			this.m_dataSize = 0U;
			this.m_doFFT = false;
			this.m_coeffecients = null;
			this.m_result = null;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000044A7 File Offset: 0x000026A7
		internal void Init(uint size)
		{
			this.m_dataSize = size;
			this.m_doFFT = (this.m_dataSize & (this.m_dataSize - 1U)) == 0U;
			if (!this.m_doFFT)
			{
				this.InitDFTCoefficients();
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000044D6 File Offset: 0x000026D6
		internal void Destroy()
		{
			if (this.m_coeffecients != null)
			{
				this.m_coeffecients = null;
			}
			if (this.m_result != null)
			{
				this.m_result = null;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000044F6 File Offset: 0x000026F6
		internal void ComputeDFT(double[] rgxnumData)
		{
			this.Prepare(rgxnumData);
			if (this.m_doFFT)
			{
				this.DoFFT(rgxnumData, false);
				return;
			}
			this.DoDFT(rgxnumData);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004517 File Offset: 0x00002717
		internal void ComputeIDFT(double[] rgxnumData)
		{
			this.Prepare(rgxnumData);
			if (this.m_doFFT)
			{
				this.DoFFT(rgxnumData, true);
				return;
			}
			throw new NotImplementedException("ComputeIDFT when m_fDoFFT is false is not implemented");
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000453B File Offset: 0x0000273B
		internal ComplexNumber[] PxcnumGetDFT()
		{
			return this.m_result;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004544 File Offset: 0x00002744
		private void InitDFTCoefficients()
		{
			this.m_coeffecients = new ComplexNumber[this.m_dataSize];
			double num = 6.283185307179586 / this.m_dataSize;
			double num2 = 0.0;
			for (uint num3 = 0U; num3 < this.m_dataSize; num3 += 1U)
			{
				this.m_coeffecients[(int)num3].real = Math.Cos(num2);
				this.m_coeffecients[(int)num3].imaginary = -1.0 * Math.Sin(num2);
				num2 += num;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000045CC File Offset: 0x000027CC
		private void Prepare(double[] rgxnumData)
		{
			if (this.m_result == null)
			{
				this.m_result = new ComplexNumber[this.m_dataSize];
			}
			int num = 0;
			while ((long)num < (long)((ulong)this.m_dataSize))
			{
				this.m_result[num].real = 0.0;
				this.m_result[num].imaginary = 0.0;
				num++;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000463C File Offset: 0x0000283C
		private void DoDFT(double[] rgxnumData)
		{
			for (uint num = 0U; num < this.m_dataSize; num += 1U)
			{
				int num2 = 0;
				while ((long)num2 < (long)((ulong)this.m_dataSize))
				{
					double num3 = rgxnumData[num2];
					uint num4 = num * (uint)num2 % this.m_dataSize;
					double num5 = this.m_coeffecients[(int)num4].real * num3;
					this.m_result[(int)num].real = this.m_result[(int)num].real + num5;
					num5 = this.m_coeffecients[(int)num4].imaginary * num3;
					this.m_result[(int)num].imaginary = this.m_result[(int)num].imaginary + num5;
					num2++;
				}
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000046FC File Offset: 0x000028FC
		private void DoFFT(double[] rgxnumData, bool fInverse)
		{
			uint num = 2U;
			double[] array = new double[2U * this.m_dataSize + 2U];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)(2U * this.m_dataSize + 2U)))
			{
				array[num2] = 0.0;
				num2++;
			}
			uint num3 = 1U;
			uint num4;
			for (num4 = 0U; num4 < this.m_dataSize; num4 += 1U)
			{
				array[(int)num3] = rgxnumData[(int)num4];
				num3 += 2U;
			}
			num3 = 1U;
			num4 = 1U;
			while (num3 < 2U * this.m_dataSize)
			{
				if (num4 > num3)
				{
					double num5 = array[(int)num4];
					array[(int)num4] = array[(int)num3];
					array[(int)num3] = num5;
					num5 = array[(int)(num4 + 1U)];
					array[(int)(num4 + 1U)] = array[(int)(num3 + 1U)];
					array[(int)(num3 + 1U)] = num5;
				}
				uint num6 = this.m_dataSize;
				while (num6 >= 2U && num4 > num6)
				{
					num4 -= num6;
					num6 /= 2U;
				}
				num4 += num6;
				num3 += 2U;
			}
			while (2U * this.m_dataSize > num)
			{
				double num7 = 1.0;
				double num8 = 0.0;
				uint num9 = 2U * num;
				double num10 = 6.283185307179586 / num;
				if (fInverse)
				{
					num10 *= -1.0;
				}
				double num11 = num10;
				double num12 = Math.Sin(num11);
				double num13 = -2.0 * Math.Sin(0.5 * num11) * Math.Sin(0.5 * num11);
				for (uint num14 = 1U; num14 < num; num14 += 2U)
				{
					for (num3 = num14; num3 <= 2U * this.m_dataSize; num3 += num9)
					{
						num4 = num3 + num;
						double num15 = num7 * array[(int)num4] - num8 * array[(int)(num4 + 1U)];
						double num16 = num7 * array[(int)(num4 + 1U)] + num8 * array[(int)num4];
						array[(int)num4] = array[(int)num3] - num15;
						array[(int)(num4 + 1U)] = array[(int)(num3 + 1U)] - num16;
						array[(int)num3] = array[(int)num3] + num15;
						array[(int)(num3 + 1U)] = array[(int)(num3 + 1U)] + num16;
					}
					double num17 = num7;
					num7 = num7 + num7 * num13 - num8 * num12;
					num8 = num8 + num8 * num13 + num17 * num12;
				}
				num = num9;
			}
			for (num3 = 0U; num3 < this.m_dataSize; num3 += 1U)
			{
				this.m_result[(int)num3].real = array[(int)(2U * num3 + 1U)];
				this.m_result[(int)num3].imaginary = -1.0 * array[(int)(2U * num3 + 2U)];
			}
		}

		// Token: 0x04000057 RID: 87
		private uint m_dataSize;

		// Token: 0x04000058 RID: 88
		private bool m_doFFT;

		// Token: 0x04000059 RID: 89
		private ComplexNumber[] m_coeffecients;

		// Token: 0x0400005A RID: 90
		private ComplexNumber[] m_result;
	}
}
