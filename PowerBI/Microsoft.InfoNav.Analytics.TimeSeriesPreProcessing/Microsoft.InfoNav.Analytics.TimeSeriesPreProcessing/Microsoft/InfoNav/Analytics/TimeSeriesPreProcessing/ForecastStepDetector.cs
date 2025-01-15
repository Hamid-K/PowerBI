using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing
{
	// Token: 0x0200000C RID: 12
	internal class ForecastStepDetector
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00003C84 File Offset: 0x00001E84
		internal ForecastStepDetector()
		{
			this.m_rgnHistogram = null;
			this.m_rgxnumFirstInBin = null;
			this.m_rgnSimilarsInBin = null;
			this.m_nHistSize = 0;
			this.m_nAllocatedHistSize = 0U;
			this.m_xnumStepMinNumericValue = 0.0;
			this.m_cBelowSelected = 0U;
			this.m_cAboveSelected = 0U;
			this.m_cZeroSteps = 0U;
			this.m_nDistinctivenessMinPercentile = 50U;
			this.m_fDuplicatesIgnored = true;
			this.m_xnumStepMinNumericValue = 1E-05;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003CFC File Offset: 0x00001EFC
		internal ForecastStepDetector(bool fDuplicatesIgnored, uint nDistinctivenessMinPercentile)
		{
			this.m_rgnHistogram = null;
			this.m_rgxnumFirstInBin = null;
			this.m_rgnSimilarsInBin = null;
			this.m_nHistSize = 0;
			this.m_nAllocatedHistSize = 0U;
			this.m_xnumStepMinNumericValue = 0.0;
			this.m_cBelowSelected = 0U;
			this.m_cAboveSelected = 0U;
			this.m_cZeroSteps = 0U;
			this.m_nDistinctivenessMinPercentile = nDistinctivenessMinPercentile;
			this.m_fDuplicatesIgnored = fDuplicatesIgnored;
			this.m_xnumStepMinNumericValue = 1E-05;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003D74 File Offset: 0x00001F74
		internal static void GetStepTolerance(double xnumStep, out double pxnumTolerance)
		{
			double num = 100000.0;
			num = 1.0 / num;
			double num2 = xnumStep;
			if (num2 < num)
			{
				num2 = num;
			}
			double num3 = 10.0;
			pxnumTolerance = num2 / num3;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003DAE File Offset: 0x00001FAE
		internal void Destroy()
		{
			this.FreeHistogramData();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003DB8 File Offset: 0x00001FB8
		internal ForecastErrorType HrDetectStepSize(IReadOnlyList<double> rgxnumTimeLine, uint cInputSize, ForecastMinMaxStepDetector pfmmDetector, ForecastStepData pfsd, out bool pfDataCorrrectionNeeded)
		{
			bool flag = false;
			ForecastStepDetector.HistogramMetadata histogramMetadata = new ForecastStepDetector.HistogramMetadata();
			bool flag2 = false;
			if (cInputSize < 2U)
			{
				this.FreeHistogramData();
				pfDataCorrrectionNeeded = false;
				return ForecastErrorType.DataIsTooSmall;
			}
			if (rgxnumTimeLine == null || pfsd == null || rgxnumTimeLine == null)
			{
				this.FreeHistogramData();
				pfDataCorrrectionNeeded = false;
				return ForecastErrorType.InvalidValueArguments;
			}
			pfDataCorrrectionNeeded = true;
			uint num = 0U;
			double num2 = rgxnumTimeLine[(int)num];
			if (pfmmDetector != null && pfmmDetector.FNotSorted())
			{
				this.FreeHistogramData();
				return ForecastErrorType.InputTimeStampsNotSorted;
			}
			if (pfmmDetector != null && pfmmDetector.FEmpty())
			{
				this.FreeHistogramData();
				return ForecastErrorType.Unexpected;
			}
			if (pfmmDetector != null)
			{
				histogramMetadata.MinMaxCopy(pfmmDetector);
			}
			else
			{
				histogramMetadata.Reset();
				uint num3 = cInputSize - 1U;
				while (num < num3)
				{
					histogramMetadata.UpdateLimitsFromValues(rgxnumTimeLine[(int)num], rgxnumTimeLine[(int)(num + 1U)]);
					num += 1U;
				}
			}
			if (histogramMetadata.FNotSorted())
			{
				this.FreeHistogramData();
				return ForecastErrorType.InputTimeStampsNotSorted;
			}
			pfsd.VerifyBusinessDates(rgxnumTimeLine[0], rgxnumTimeLine[(int)(cInputSize - 1U)]);
			uint num4 = histogramMetadata.CGetFoundZeroes();
			if (!this.DuplicatesIgnored() && num4 > 0U)
			{
				this.FreeHistogramData();
				return ForecastErrorType.NoDominantStepDetected;
			}
			if (histogramMetadata.FSingleValue())
			{
				double maxValue = histogramMetadata.GetMaxValue();
				if (maxValue == 0.0)
				{
					this.FreeHistogramData();
					return ForecastErrorType.DataIsTooSmall;
				}
				pfsd.SetUniqueStep(maxValue, (int)(cInputSize - 1U - num4));
				pfDataCorrrectionNeeded = num4 > 0U;
				this.FreeHistogramData();
				return ForecastErrorType.NoError;
			}
			else
			{
				double num5;
				int num6;
				double num7;
				histogramMetadata.GetBinSize(this.m_xnumStepMinNumericValue, this.DuplicatesIgnored(), out num5, out num6, out num7, out flag);
				double minValue = histogramMetadata.GetMinValue(this.DuplicatesIgnored());
				double maxValue2 = histogramMetadata.GetMaxValue();
				ForecastErrorType forecastErrorType = this.HrPopulateHistogram(rgxnumTimeLine, cInputSize, num6, num5, minValue, maxValue2, false);
				if (forecastErrorType != ForecastErrorType.NoError)
				{
					this.FreeHistogramData();
					return forecastErrorType;
				}
				this.GetMaxCenter(minValue, num5, num6, pfsd, out flag2);
				if (flag2)
				{
					flag = false;
				}
				int num8 = 1;
				while (flag)
				{
					double num9;
					histogramMetadata.GetFinerGranularity(num5, num7, pfsd.PxnumGetNumericStep(), this.DuplicatesIgnored(), out minValue, out maxValue2, out num9, out num6, out flag);
					this.GetMaxCenter(minValue, num9, num6, pfsd, out flag2);
					if (flag2)
					{
						flag = false;
					}
					if (flag)
					{
						num8++;
						if (num8 > 10)
						{
							this.FreeHistogramData();
							return ForecastErrorType.NoDominantStepDetected;
						}
						num5 = num9;
					}
				}
				if (pfsd.CrwGetStepCount() == (int)(cInputSize - 1U))
				{
					pfDataCorrrectionNeeded = false;
				}
				if (!this.FStepCounterAbovePercentileThreshold((uint)pfsd.CrwGetStepCount(), this.DuplicatesIgnored() ? (cInputSize - this.m_cZeroSteps) : this.m_cZeroSteps, this.m_nDistinctivenessMinPercentile))
				{
					this.FreeHistogramData();
					return ForecastErrorType.NoDominantStepDetected;
				}
				this.FreeHistogramData();
				return ForecastErrorType.NoError;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004006 File Offset: 0x00002206
		internal bool DuplicatesIgnored()
		{
			return this.m_fDuplicatesIgnored;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004010 File Offset: 0x00002210
		private ForecastErrorType HrPopulateHistogram(IReadOnlyList<double> rgxnumTimeLine, uint cInputSize, int nHistSize, double xnumBinSize, double xnumMinStep, double xnumMaxStep, bool fLimitCheck)
		{
			if (this.m_rgnHistogram != null && (ulong)this.m_nAllocatedHistSize < (ulong)((long)nHistSize))
			{
				this.FreeHistogramData();
			}
			if (this.m_rgnHistogram == null)
			{
				this.m_nAllocatedHistSize = (uint)nHistSize;
				this.m_rgnHistogram = new int[this.m_nAllocatedHistSize];
				this.m_rgnSimilarsInBin = new int[this.m_nAllocatedHistSize];
				this.m_rgxnumFirstInBin = new double[this.m_nAllocatedHistSize];
			}
			this.m_nHistSize = nHistSize;
			for (int i = 0; i < this.m_nHistSize; i++)
			{
				this.m_rgnHistogram[i] = 0;
			}
			this.m_cBelowSelected = 0U;
			this.m_cAboveSelected = 0U;
			this.m_cZeroSteps = 0U;
			double num = rgxnumTimeLine[(int)(cInputSize - 1U)];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)(cInputSize - 1U)))
			{
				double num3 = rgxnumTimeLine[num2];
				double num4 = rgxnumTimeLine[num2 + 1] - num3;
				if (this.DuplicatesIgnored() && num4 == 0.0)
				{
					this.m_cZeroSteps += 1U;
				}
				else
				{
					if (fLimitCheck)
					{
						if (num4 < xnumMinStep)
						{
							this.m_cBelowSelected += 1U;
							goto IL_01CC;
						}
						if (num4 > xnumMaxStep)
						{
							this.m_cAboveSelected += 1U;
							goto IL_01CC;
						}
					}
					double num5 = num4;
					double num6 = (num5 - xnumMinStep) / xnumBinSize;
					if (num6 < 0.0)
					{
						return ForecastErrorType.PopulatingHistogramFailed;
					}
					int num7 = (int)Math.Floor(num6);
					if (num7 >= this.m_nHistSize)
					{
						return ForecastErrorType.PopulatingHistogramFailed;
					}
					this.m_rgnHistogram[num7]++;
					if (this.m_rgnHistogram[num7] == 1)
					{
						this.m_rgxnumFirstInBin[num7] = num5;
						this.m_rgnSimilarsInBin[num7] = 1;
					}
					else
					{
						if (this.m_rgnSimilarsInBin[num7] <= 0 || this.m_rgnSimilarsInBin[num7] > this.m_rgnHistogram[num7])
						{
							return ForecastErrorType.PopulatingHistogramFailed;
						}
						if (Math.Abs(this.m_rgxnumFirstInBin[num7] - num5) <= 3E-07)
						{
							this.m_rgnSimilarsInBin[num7]++;
						}
					}
				}
				IL_01CC:
				num2++;
			}
			return ForecastErrorType.NoError;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000041FC File Offset: 0x000023FC
		private void GetMaxCenter(double xnumMinStep, double xnumBinSize, int nHistSize, ForecastStepData pfsd, out bool pfDominantStepFound)
		{
			int num = this.m_rgnHistogram[0];
			int i = 0;
			double? num2 = null;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			pfDominantStepFound = false;
			if (nHistSize < 5)
			{
				for (i = 1; i < nHistSize; i++)
				{
					num += this.m_rgnHistogram[i];
				}
				for (i = 0; i < nHistSize; i++)
				{
					if (num3 <= num && (num3 < num || this.m_rgnHistogram[num4] < this.m_rgnHistogram[i]))
					{
						num4 = i;
						num3 = num;
					}
				}
				num6 = nHistSize - 1;
			}
			else
			{
				double num7 = xnumMinStep / xnumBinSize;
				num3 = num;
				int j = 1;
				for (;;)
				{
					if (j < this.m_nHistSize)
					{
						num += this.m_rgnHistogram[j];
					}
					double num8 = (double)((j - i + 1) * 5 - (i + j) / 2);
					if (num7 < num8)
					{
						if (i >= 0)
						{
							num -= this.m_rgnHistogram[i];
						}
						i++;
					}
					else
					{
						i--;
						if (i >= 0)
						{
							num += this.m_rgnHistogram[i];
						}
					}
					int num9 = (i + j) / 2;
					if (num9 >= this.m_nHistSize)
					{
						break;
					}
					if (num3 < num || (num3 == num && this.m_rgnHistogram[num4] < this.m_rgnHistogram[num9]))
					{
						num4 = num9;
						num3 = num;
						num5 = i;
						num6 = j;
					}
					j++;
				}
				num5 = Math.Max(num5, 0);
				num6 = Math.Min(num6, this.m_nHistSize - 1);
				int num10 = 0;
				int num11 = 0;
				for (int k = num5; k <= num6; k++)
				{
					if (num11 < this.m_rgnHistogram[k])
					{
						num11 = this.m_rgnHistogram[k];
						num10 = k;
					}
				}
				if (num10 != num4 && ForecastStepDetector.FToleratedSmallerCovereage(num11, num3))
				{
					int num12 = (int)Math.Floor((xnumMinStep + (double)num10 * xnumBinSize) / 10.0 / xnumBinSize);
					int num13 = ((num10 - num12 < 0) ? 0 : (num10 - num12));
					int num14 = ((num10 + num12 >= this.m_nHistSize) ? (this.m_nHistSize - 1) : (num10 + num12));
					int k = num13;
					num11 = 0;
					while (k <= num14)
					{
						num11 += this.m_rgnHistogram[k];
						k++;
					}
					num4 = num10;
					num3 = num11;
					num5 = num13;
					num6 = num14;
				}
			}
			if (this.m_rgnHistogram[num4] > 0 && this.m_rgnSimilarsInBin[num4] == this.m_rgnHistogram[num4])
			{
				num2 = new double?(this.m_rgxnumFirstInBin[num4]);
				pfDominantStepFound = true;
			}
			double num15 = (double)num4;
			double num16 = xnumMinStep + xnumBinSize * num15;
			double num17 = num16 + xnumBinSize;
			pfsd.SetHistogramStep(num16, num17, num2, this.m_rgnHistogram, xnumMinStep, xnumBinSize, nHistSize);
			for (i = num5 - 1; i >= 0; i--)
			{
				this.m_cBelowSelected += (uint)this.m_rgnHistogram[i];
			}
			for (int j = num6 + 1; j < nHistSize; j++)
			{
				this.m_cAboveSelected += (uint)this.m_rgnHistogram[j];
			}
			pfsd.SetStepCount(num3);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000044AC File Offset: 0x000026AC
		private bool FStepCounterAbovePercentileThreshold(uint cCounter, uint cInputSize, uint nPercentageThreshold)
		{
			return (nPercentageThreshold == 0U && cCounter > 0U) || (nPercentageThreshold > 0U && (ulong)cCounter * 100UL > (ulong)(cInputSize - 1U) * (ulong)nPercentageThreshold);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000044CC File Offset: 0x000026CC
		private static bool FToleratedSmallerCovereage(int cCountNew, int cCountAct)
		{
			return cCountNew >= cCountAct - 1 || cCountAct * 15 > (cCountAct - cCountNew) * 100;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000044E2 File Offset: 0x000026E2
		private void FreeHistogramData()
		{
			this.m_rgnHistogram = null;
		}

		// Token: 0x0400008F RID: 143
		internal const int c_nUserTimeTolerancePart = 1000;

		// Token: 0x04000090 RID: 144
		internal const int c_nMinStepDetectionInputSize = 2;

		// Token: 0x04000091 RID: 145
		internal const int c_nStepTolerancePart = 5;

		// Token: 0x04000092 RID: 146
		internal const uint c_nStepDistinctivenessMinPercentile = 50U;

		// Token: 0x04000093 RID: 147
		internal const bool c_fDuplicatesIgnored = true;

		// Token: 0x04000094 RID: 148
		internal const int c_nIgnorableStepsInRangeByNum = 1;

		// Token: 0x04000095 RID: 149
		internal const int c_nIgnorableStepsInRangeByPercentile = 15;

		// Token: 0x04000096 RID: 150
		private const int c_nAllowedDetectionIterations = 10;

		// Token: 0x04000097 RID: 151
		private const int c_nBinsPerToleranceRange = 5;

		// Token: 0x04000098 RID: 152
		private const int c_cMaxHistogramSize = 3000;

		// Token: 0x04000099 RID: 153
		private const int c_nOneDivisorForMinStep = 100000;

		// Token: 0x0400009A RID: 154
		private int[] m_rgnHistogram;

		// Token: 0x0400009B RID: 155
		private double[] m_rgxnumFirstInBin;

		// Token: 0x0400009C RID: 156
		private int[] m_rgnSimilarsInBin;

		// Token: 0x0400009D RID: 157
		private int m_nHistSize;

		// Token: 0x0400009E RID: 158
		private uint m_nAllocatedHistSize;

		// Token: 0x0400009F RID: 159
		private readonly double m_xnumStepMinNumericValue;

		// Token: 0x040000A0 RID: 160
		private uint m_cBelowSelected;

		// Token: 0x040000A1 RID: 161
		private uint m_cAboveSelected;

		// Token: 0x040000A2 RID: 162
		private uint m_cZeroSteps;

		// Token: 0x040000A3 RID: 163
		private uint m_nDistinctivenessMinPercentile;

		// Token: 0x040000A4 RID: 164
		private bool m_fDuplicatesIgnored;

		// Token: 0x02000011 RID: 17
		internal sealed class HistogramMetadata : ForecastMinMaxStepDetector
		{
			// Token: 0x06000049 RID: 73 RVA: 0x000044EB File Offset: 0x000026EB
			internal HistogramMetadata()
			{
				this.m_xnumToleranceDivider = 5.0;
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00004504 File Offset: 0x00002704
			internal void CalcNewRange(double xnumStepValue, double xnumBinSize, out double pxnumRange)
			{
				double num = xnumStepValue / this.m_xnumToleranceDivider;
				double num2 = xnumBinSize / 2.0;
				num = ((num > num2) ? num : num2);
				pxnumRange = num * 8.0;
			}

			// Token: 0x0600004B RID: 75 RVA: 0x0000453C File Offset: 0x0000273C
			internal void GetBinSize(double xnumStepMinNumericValue, bool fIgnoreZeroes, out double pxnumBinSize, out int pnHistSize, out double pxnumDesiredBinSize, out bool pfFinerGranularityPhaseRequired)
			{
				bool flag = this.m_xnumMinNonZeroValue == 0.0;
				if (!flag)
				{
					flag = this.m_xnumMinNonZeroValue < xnumStepMinNumericValue && this.m_xnumMaxValue > xnumStepMinNumericValue;
				}
				double num = (flag ? xnumStepMinNumericValue : this.m_xnumMinNonZeroValue) / 25.0;
				double minValue = base.GetMinValue(fIgnoreZeroes);
				double num2 = this.m_xnumMaxValue - minValue;
				double num3 = num2 / num + 1.0;
				if (num3 > 3000.0)
				{
					pxnumBinSize = num2 / 2999.0;
					pnHistSize = 3000;
					pfFinerGranularityPhaseRequired = true;
					pxnumDesiredBinSize = num;
					return;
				}
				uint num4 = (uint)Math.Floor(num3);
				pxnumBinSize = num2 / num4;
				pxnumDesiredBinSize = pxnumBinSize;
				pnHistSize = (int)Math.Floor(num2 / pxnumBinSize + 1.5);
				pfFinerGranularityPhaseRequired = false;
			}

			// Token: 0x0600004C RID: 76 RVA: 0x00004608 File Offset: 0x00002808
			internal void GetFinerGranularity(double xnumBinSize, double xnumDesiredBinSize, double xnumCurStepValue, bool fIgnoreZeroes, out double pxnumMinStep, out double pxnumMaxStep, out double pxnumFinerBinSize, out int pnFinerHistSize, out bool pfFinerGranularityPhaseRequired)
			{
				bool flag = false;
				bool flag2 = true;
				double num = xnumDesiredBinSize;
				double minValue = base.GetMinValue(fIgnoreZeroes);
				double num2;
				this.CalcNewRange(xnumCurStepValue, xnumBinSize, out num2);
				double num3 = 2900.0;
				double num4 = 48000.0;
				double num5 = xnumCurStepValue;
				while (flag2)
				{
					double num6 = num2 / num;
					if (num6 < num3)
					{
						flag2 = false;
					}
					else
					{
						flag = true;
						double num7 = (double)((num6 > num4) ? 16 : 2);
						num *= num7;
					}
				}
				double num8 = (num5 - minValue) / num;
				int num9;
				if (num8 < 4294967295.0)
				{
					num9 = (int)Math.Floor(num8);
					num5 = minValue + (double)num9 * num;
				}
				this.CalcNewRange(num5, xnumBinSize, out num2);
				num9 = (int)Math.Floor(num2 / num);
				double num10 = (double)num9 * num / 2.0;
				pxnumMinStep = num5 - num10;
				pxnumMaxStep = num5 + num10;
				if (pxnumMinStep < minValue)
				{
					pxnumMinStep = minValue;
				}
				if (pxnumMaxStep > this.m_xnumMaxValue)
				{
					pxnumMaxStep = this.m_xnumMaxValue;
				}
				pnFinerHistSize = (int)Math.Floor((pxnumMaxStep - pxnumMinStep) / num + 1.0);
				if (flag)
				{
					flag = xnumBinSize > num;
				}
				pfFinerGranularityPhaseRequired = flag;
				pxnumFinerBinSize = num;
			}

			// Token: 0x040000C0 RID: 192
			private double m_xnumToleranceDivider;
		}
	}
}
