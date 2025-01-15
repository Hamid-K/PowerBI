using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Analytics.ExponentialSmoothing
{
	// Token: 0x0200000E RID: 14
	internal sealed class SpectrumComputer
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00004A00 File Offset: 0x00002C00
		internal SpectrumComputer()
		{
			this.m_rgxnumData = null;
			this.m_dataSize = 0U;
			this.m_rgxnumPreparedData = null;
			this.m_rgFreqPower = null;
			this.m_xnumPowerMean = 0.0;
			this.m_xnumPowerStDev = 0.0;
			this.m_fourierTransform = new FourierTransform();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004A58 File Offset: 0x00002C58
		internal List<double> GetPower()
		{
			return this.m_rgFreqPower.Select((LocationPower x) => x.Power).ToList<double>();
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00004A89 File Offset: 0x00002C89
		internal LocationPower[] PlpGetPower
		{
			get
			{
				return this.m_rgFreqPower;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00004A91 File Offset: 0x00002C91
		internal uint NGetPowerSize
		{
			get
			{
				return this.m_dataSize / 2U;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00004A9B File Offset: 0x00002C9B
		internal double PXNUMGetPowerMean
		{
			get
			{
				return this.m_xnumPowerMean;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00004AA3 File Offset: 0x00002CA3
		internal double PXNUMGetPowerStDev
		{
			get
			{
				return this.m_xnumPowerStDev;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004AAB File Offset: 0x00002CAB
		internal int Init(double[] rgxnumData, uint dataSize)
		{
			this.m_dataSize = dataSize;
			this.m_rgxnumData = rgxnumData;
			return this.PrepareData();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004AC1 File Offset: 0x00002CC1
		internal void Destroy()
		{
			this.m_fourierTransform.Destroy();
			if (this.m_rgxnumPreparedData != null)
			{
				this.m_rgxnumPreparedData = null;
			}
			if (this.m_rgFreqPower != null)
			{
				this.m_rgFreqPower = null;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004AEC File Offset: 0x00002CEC
		internal int ComputePower()
		{
			this.m_xnumPowerMean = 0.0;
			this.m_xnumPowerStDev = 0.0;
			int num = this.DoDFT();
			if (SeasonalityDetector.Fail(num))
			{
				this.m_rgFreqPower = null;
				return num;
			}
			ComplexNumber[] array = this.m_fourierTransform.PxcnumGetDFT();
			double num2 = this.m_dataSize;
			double num3 = 0.875 * this.m_dataSize;
			this.m_rgFreqPower = new LocationPower[this.NGetPowerSize];
			for (uint num4 = 1U; num4 <= this.NGetPowerSize; num4 += 1U)
			{
				double num5 = (array[(int)num4].real * array[(int)num4].real + array[(int)num4].imaginary * array[(int)num4].imaginary) / num3;
				this.m_xnumPowerMean = num5 + this.m_xnumPowerMean;
				this.m_xnumPowerStDev = num5 * num5 + this.m_xnumPowerStDev;
				double num6 = num4 / this.m_dataSize;
				this.m_rgFreqPower[(int)(num4 - 1U)] = new LocationPower(num6, num5);
			}
			if (this.NGetPowerSize > 0U)
			{
				num2 = this.NGetPowerSize;
				this.m_xnumPowerMean /= num2;
			}
			if (this.NGetPowerSize > 1U)
			{
				num2 = this.NGetPowerSize - 1U;
				num3 = this.m_xnumPowerMean * this.m_xnumPowerMean;
				this.m_xnumPowerStDev = Math.Sqrt(this.m_xnumPowerStDev / num2 - num3);
			}
			else
			{
				this.m_xnumPowerStDev = 0.0;
			}
			return num;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004C70 File Offset: 0x00002E70
		private int PrepareData()
		{
			double num = 0.0;
			double num2 = 0.0;
			int num3 = (int)(1U - (this.m_dataSize + 1U) / 2U);
			for (uint num4 = 0U; num4 < this.m_dataSize; num4 += 1U)
			{
				num += this.m_rgxnumData[(int)num4];
				double num5 = (double)((long)num3 + (long)((ulong)num4));
				num2 += this.m_rgxnumData[(int)num4] * num5;
			}
			double num6 = this.m_dataSize;
			num /= num6;
			double num7 = 2.0 * this.m_dataSize * (this.m_dataSize * this.m_dataSize - 1U);
			num2 /= num7 / 24.0;
			this.m_rgxnumPreparedData = new double[this.m_dataSize];
			for (uint num8 = 0U; num8 < this.m_dataSize; num8 += 1U)
			{
				this.m_rgxnumPreparedData[(int)num8] = this.m_rgxnumData[(int)num8] - num - (double)((long)num3 + (long)((ulong)num8)) * num2;
			}
			int num9 = this.DoTaper();
			if (SeasonalityDetector.Fail(num9))
			{
				this.m_rgxnumPreparedData = null;
			}
			return num9;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004D74 File Offset: 0x00002F74
		private int DoTaper()
		{
			int num = 0;
			uint num2 = (uint)Math.Floor(this.m_dataSize * 0.1);
			double num3 = num2 * 2U;
			for (uint num4 = 0U; num4 < num2; num4 += 1U)
			{
				double num5 = 0.5 * (1.0 - Math.Cos(3.141592653589793 * (2U * num4 + 1U) / num3));
				this.m_rgxnumPreparedData[(int)num4] = this.m_rgxnumPreparedData[(int)num4] * num5;
				this.m_rgxnumPreparedData[(int)(this.m_dataSize - num4 - 1U)] = this.m_rgxnumPreparedData[(int)(this.m_dataSize - num4 - 1U)] * num5;
			}
			return num;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004E15 File Offset: 0x00003015
		private int DoDFT()
		{
			int num = 0;
			this.m_fourierTransform.Init(this.m_dataSize);
			this.m_fourierTransform.ComputeDFT(this.m_rgxnumPreparedData);
			return num;
		}

		// Token: 0x04000063 RID: 99
		private double[] m_rgxnumData;

		// Token: 0x04000064 RID: 100
		private uint m_dataSize;

		// Token: 0x04000065 RID: 101
		private double[] m_rgxnumPreparedData;

		// Token: 0x04000066 RID: 102
		private FourierTransform m_fourierTransform;

		// Token: 0x04000067 RID: 103
		private LocationPower[] m_rgFreqPower;

		// Token: 0x04000068 RID: 104
		private double m_xnumPowerMean;

		// Token: 0x04000069 RID: 105
		private double m_xnumPowerStDev;
	}
}
