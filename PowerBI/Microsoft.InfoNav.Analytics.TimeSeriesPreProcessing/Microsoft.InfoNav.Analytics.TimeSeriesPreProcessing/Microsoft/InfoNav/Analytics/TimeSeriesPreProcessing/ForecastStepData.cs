using System;

namespace Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing
{
	// Token: 0x0200000B RID: 11
	internal sealed class ForecastStepData
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00003198 File Offset: 0x00001398
		internal ForecastStepData()
		{
			this.m_xnumNumericTimeStep = 0.0;
			this.m_drwStepCount = 0;
			this.m_xnumLastTimePoint = 0.0;
			this.m_fValidDates = true;
			this.m_nMonthsInStep = -2;
			this.m_xnumCalculatedTimeStep = 0.0;
			this.m_f1904 = false;
			this.m_fDateCompat = false;
			this.DayMax = (DateTime.MaxValue - DateTime.MinValue).TotalDays;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003219 File Offset: 0x00001419
		internal void Init()
		{
			this.m_fDateCompat = false;
			this.m_f1904 = false;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000322C File Offset: 0x0000142C
		private bool FXnumValidForConversionToDtr(double pxnum, bool f1904, bool fDateCompat, out int pnNum)
		{
			pnNum = 0;
			int num = (int)Math.Floor(pxnum);
			pnNum = num;
			return num < 0 || pxnum < 0.0 || (double)num > this.DayMax;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003268 File Offset: 0x00001468
		internal void VerifyBusinessDates(double pxnumMinTime, double pxnumMaxTime)
		{
			int num;
			this.m_fValidDates = !this.FXnumValidForConversionToDtr(pxnumMinTime, this.m_f1904, this.m_fDateCompat, out num) && !this.FXnumValidForConversionToDtr(pxnumMaxTime, this.m_f1904, this.m_fDateCompat, out num);
			this.m_nMonthsInStep = (this.m_fValidDates ? (-1) : 0);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000032C0 File Offset: 0x000014C0
		internal void SetHistogramStep(double pxnumLow, double pxnumHigh, double? pxnumSelected, int[] rgnHistogram, double pxnumMinStep, double pxnumBinSize, int nHistSize)
		{
			this.m_xnumNumericTimeStep = ((pxnumSelected == null) ? pxnumLow : pxnumSelected.Value);
			this.m_nMonthsInStep = 0;
			if (this.m_fValidDates)
			{
				bool flag = true;
				if (ForecastStepData.FOverlapsStep(pxnumLow, pxnumHigh, 30.0, 31.0))
				{
					int num = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 27.5, 31.5);
					if (num > 1)
					{
						int num2 = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 27.5, 29.5);
						int num3 = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 29.5, 30.5);
						int num4 = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 30.5, 31.5);
						int num5 = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 29.5, 31.5);
						bool flag2 = num5 + num2 == num;
						if ((num4 == 0 && (num3 > 2 || num2 > 2)) || (num3 == 0 && (num4 > 4 || num2 > 2)) || (num2 > 2 && flag2 && num5 < 10))
						{
							flag = false;
						}
					}
					if (flag)
					{
						this.m_xnumCalculatedTimeStep = 30.5;
						this.m_nMonthsInStep = 1;
						return;
					}
				}
				else
				{
					if (ForecastStepData.FOverlapsStep(pxnumLow, pxnumHigh, 363.0, 367.0))
					{
						this.m_xnumCalculatedTimeStep = 365.0;
						this.m_nMonthsInStep = 12;
						return;
					}
					if (ForecastStepData.FOverlapsStep(pxnumLow, pxnumHigh, 90.0, 92.0))
					{
						int num = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 89.5, 92.5);
						if (num > 1)
						{
							int num6 = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 89.5, 90.5);
							int num7 = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 90.5, 91.5);
							int num8 = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 91.5, 92.5);
							int num9 = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 90.5, 92.5);
							bool flag2 = num9 + num6 == num;
							if ((num7 == 0 && (num8 > 3 || num6 > 2)) || (num8 == 0 && (num7 > 3 || num6 > 2)) || (num6 > 2 && flag2 && num9 < 2))
							{
								flag = false;
							}
						}
						if (flag)
						{
							this.m_xnumCalculatedTimeStep = 91.0;
							this.m_nMonthsInStep = 3;
							return;
						}
					}
					else if (ForecastStepData.FOverlapsStep(pxnumLow, pxnumHigh, 181.5, 184.5))
					{
						int num = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 89.5, 92.5);
						if (num > 1)
						{
							int num10 = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 180.5, 181.5);
							int num11 = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 181.5, 182.5);
							int num12 = this.CBusinessStepOccurences(pxnumMinStep, pxnumBinSize, rgnHistogram, nHistSize, 183.5, 184.5);
							bool flag2 = num10 + num11 + num12 == num;
							if ((num10 == 0 && (num12 > 2 || num11 > 2)) || (num12 == 0 && (num10 > 2 || num11 > 2)) || (num11 > 2 && flag2 && num10 + num12 < 6))
							{
								flag = false;
							}
						}
						if (flag)
						{
							this.m_xnumCalculatedTimeStep = 183.0;
							this.m_nMonthsInStep = 6;
							return;
						}
					}
					else if (pxnumLow > 365.0)
					{
						this.CheckStepOfSeveralYears(pxnumLow, pxnumHigh);
					}
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003682 File Offset: 0x00001882
		internal void SetStepCount(int drwStepCount)
		{
			this.m_drwStepCount = drwStepCount;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000368C File Offset: 0x0000188C
		internal void SetUniqueStep(double pxnumSelected, int cOccurences)
		{
			this.m_xnumNumericTimeStep = pxnumSelected;
			this.m_nMonthsInStep = 0;
			this.SetStepCount(cOccurences);
			if (this.m_fValidDates)
			{
				if (ForecastStepData.FOverlapsStep(pxnumSelected, pxnumSelected, 30.0, 31.0))
				{
					double num = 29.5;
					if ((num < pxnumSelected && cOccurences <= 3) || (num >= pxnumSelected && cOccurences <= 2))
					{
						this.m_xnumCalculatedTimeStep = 30.5;
						this.m_nMonthsInStep = 1;
						return;
					}
				}
				else
				{
					if (ForecastStepData.FOverlapsStep(pxnumSelected, pxnumSelected, 363.0, 367.0))
					{
						this.m_xnumCalculatedTimeStep = 365.0;
						this.m_nMonthsInStep = 12;
						return;
					}
					if (ForecastStepData.FOverlapsStep(pxnumSelected, pxnumSelected, 90.0, 92.0))
					{
						double num = 90.5;
						if ((num < pxnumSelected && cOccurences <= 3) || (num >= pxnumSelected && cOccurences <= 2))
						{
							this.m_xnumCalculatedTimeStep = 91.0;
							this.m_nMonthsInStep = 3;
							return;
						}
					}
					else if (ForecastStepData.FOverlapsStep(pxnumSelected, pxnumSelected, 181.5, 184.5))
					{
						if (cOccurences <= 2)
						{
							this.m_xnumCalculatedTimeStep = 183.0;
							this.m_nMonthsInStep = 6;
							return;
						}
					}
					else if (pxnumSelected > 365.0)
					{
						this.CheckStepOfSeveralYears(pxnumSelected, pxnumSelected);
					}
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000037D4 File Offset: 0x000019D4
		internal void SetStartTime(double pxnumStartTime)
		{
			this.m_xnumLastTimePoint = pxnumStartTime;
			if (this.m_nMonthsInStep > 0)
			{
				this.m_dtrLastInput = DateTime.MinValue.AddDays(this.m_xnumLastTimePoint);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000380A File Offset: 0x00001A0A
		internal double PxnumGetNumericStep()
		{
			return this.m_xnumNumericTimeStep;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003812 File Offset: 0x00001A12
		internal int CrwGetStepCount()
		{
			return this.m_drwStepCount;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000381A File Offset: 0x00001A1A
		internal int NGetBusinessStep()
		{
			return this.m_nMonthsInStep;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003822 File Offset: 0x00001A22
		internal bool FIsBusinessStep()
		{
			return this.m_nMonthsInStep > 0;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003830 File Offset: 0x00001A30
		internal ForecastErrorType HrGetBusinessStepCalculatedStep(double pxnumBaseTimePoint, double pxnumOtherTimePoint, out double pxnumFoundBusinessTimePoint, out int pcSteps)
		{
			bool flag = pxnumBaseTimePoint < pxnumOtherTimePoint;
			int num = (flag ? 1 : (-1));
			int num2 = (flag ? this.m_nMonthsInStep : (-this.m_nMonthsInStep));
			if (this.m_nMonthsInStep <= 0)
			{
				pxnumFoundBusinessTimePoint = 0.0;
				pcSteps = -1;
				return ForecastErrorType.NoBusinessStep;
			}
			pcSteps = 0;
			pxnumFoundBusinessTimePoint = pxnumBaseTimePoint;
			DateTime dateTime = DateTime.MinValue.AddDays(pxnumBaseTimePoint);
			double num3 = Math.Abs(pxnumBaseTimePoint - pxnumOtherTimePoint);
			for (;;)
			{
				double num4;
				this.AddMonthsAndGetNum(ref dateTime, num2, out num4);
				double num5 = Math.Abs(num4 - pxnumOtherTimePoint);
				if (num5 >= num3)
				{
					break;
				}
				pxnumFoundBusinessTimePoint = num4;
				num3 = num5;
				pcSteps += num;
			}
			return ForecastErrorType.NoError;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000038C4 File Offset: 0x00001AC4
		internal ForecastErrorType HrInitBusinessStepTimepointByShift(double pxnumBaseTimePoint)
		{
			if (this.m_nMonthsInStep <= 0)
			{
				return ForecastErrorType.NoBusinessStep;
			}
			this.m_dtrShiftDate = DateTime.MinValue.AddDays(pxnumBaseTimePoint);
			return ForecastErrorType.NoError;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000038F2 File Offset: 0x00001AF2
		internal void GetBusinessStepTimepointSingleShift(bool fFwd, out double pxnumTimePoint)
		{
			this.AddMonthsAndGetNum(ref this.m_dtrShiftDate, fFwd ? this.m_nMonthsInStep : (-this.m_nMonthsInStep), out pxnumTimePoint);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003914 File Offset: 0x00001B14
		internal ForecastErrorType HrGetOffset(double pxnumTimePoint, out double pxnumOffset, out bool pfNeedInterpolation)
		{
			pfNeedInterpolation = false;
			if (this.m_xnumNumericTimeStep == 0.0)
			{
				pxnumOffset = 0.0;
				return ForecastErrorType.NumericalStepNotAssigned;
			}
			if (this.m_nMonthsInStep > 0)
			{
				int num = (int)Math.Floor((pxnumTimePoint - this.m_xnumLastTimePoint) / this.m_xnumCalculatedTimeStep);
				DateTime dateTime;
				this.GetRefreshedStartTimeCopy(out dateTime);
				double num2;
				this.AddMonthsAndGetNum(ref dateTime, this.m_nMonthsInStep * num, out num2);
				double num3;
				for (num3 = num2; num3 > pxnumTimePoint; num3 = num2)
				{
					num--;
					this.AddMonthsAndGetNum(ref dateTime, -this.m_nMonthsInStep, out num2);
				}
				if (num3 == pxnumTimePoint)
				{
					pxnumOffset = (double)num;
					return ForecastErrorType.NoError;
				}
				int num4 = num;
				double num5 = num2;
				for (num3 = num2; num3 < pxnumTimePoint; num3 = num5)
				{
					num4++;
					this.AddMonthsAndGetNum(ref dateTime, this.m_nMonthsInStep, out num5);
				}
				if (num3 == pxnumTimePoint)
				{
					pxnumOffset = (double)num4;
					return ForecastErrorType.NoError;
				}
				pfNeedInterpolation = true;
				if (num != num4 - 1)
				{
					num = num4 - 1;
					this.AddMonthsAndGetNum(ref dateTime, -this.m_nMonthsInStep, out num2);
				}
				pxnumOffset = (double)num + (pxnumTimePoint - num2) / (num5 - num2);
				return ForecastErrorType.NoError;
			}
			else
			{
				pxnumOffset = (pxnumTimePoint - this.m_xnumLastTimePoint) / this.m_xnumNumericTimeStep;
				if (Math.Round(pxnumOffset) - pxnumOffset == 0.0)
				{
					return ForecastErrorType.NoError;
				}
				double num6 = Math.Round(pxnumOffset);
				if (Math.Abs(num6 - pxnumOffset) * 1000.0 <= 1.0)
				{
					pxnumOffset = num6;
				}
				else
				{
					pfNeedInterpolation = true;
				}
				return ForecastErrorType.NoError;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003A68 File Offset: 0x00001C68
		internal ForecastErrorType HrGetTimepointFromOffset(int nForecastOffset, out double pxnumTimePoint)
		{
			if (this.m_nMonthsInStep < 0 || this.m_xnumNumericTimeStep == 0.0)
			{
				pxnumTimePoint = 0.0;
				return ForecastErrorType.NoBusinessStep;
			}
			if (this.m_nMonthsInStep > 0)
			{
				DateTime dateTime;
				this.GetRefreshedStartTimeCopy(out dateTime);
				this.AddMonthsAndGetNum(ref dateTime, this.m_nMonthsInStep * nForecastOffset, out pxnumTimePoint);
			}
			else
			{
				pxnumTimePoint = this.m_xnumLastTimePoint + this.m_xnumNumericTimeStep * (double)nForecastOffset;
				ForecastMathUtils.ZeroRounding(this.m_xnumNumericTimeStep, ref pxnumTimePoint);
			}
			return ForecastErrorType.NoError;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003AE0 File Offset: 0x00001CE0
		private void AddMonthsAndGetNum(ref DateTime pdtr, int cMonths, out double pxnumTime)
		{
			if (pdtr.Year * 12 + pdtr.Month - 1 + cMonths >= 0)
			{
				DateTime dateTime = pdtr.AddMonths(cMonths);
				pxnumTime = (dateTime - DateTime.MinValue).TotalDays;
				pdtr = dateTime;
				return;
			}
			pxnumTime = 0.0;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003B34 File Offset: 0x00001D34
		private static bool FOverlapsStep(double pxnumLowStep, double pxnumHighStep, double dblLowChecked, double dblHighChecked)
		{
			ForecastStepData.SGN sgn;
			if (dblLowChecked < pxnumLowStep)
			{
				sgn = ForecastStepData.SGN.sgnLT;
			}
			else if (dblLowChecked > pxnumHighStep)
			{
				sgn = ForecastStepData.SGN.sgnGT;
			}
			else
			{
				sgn = ForecastStepData.SGN.sgnEQ;
			}
			ForecastStepData.SGN sgn2;
			if (dblHighChecked < pxnumLowStep)
			{
				sgn2 = ForecastStepData.SGN.sgnLT;
			}
			else if (dblHighChecked > pxnumHighStep)
			{
				sgn2 = ForecastStepData.SGN.sgnGT;
			}
			else
			{
				sgn2 = ForecastStepData.SGN.sgnEQ;
			}
			return sgn != sgn2 || sgn == ForecastStepData.SGN.sgnEQ;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003B6F File Offset: 0x00001D6F
		private void GetRefreshedStartTimeCopy(out DateTime dtrCopy)
		{
			dtrCopy = this.m_dtrLastInput;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003B80 File Offset: 0x00001D80
		private int CBusinessStepOccurences(double pxnumMinStep, double pxnumBinSize, int[] rgnHistogram, int nHistSize, double dblStepStart, double dblStepEnd)
		{
			int num = 0;
			double num2 = (dblStepStart - pxnumMinStep) / pxnumBinSize;
			int num3 = ((num2 < 0.0) ? (-1) : ((int)Math.Floor(num2)));
			num2 = (dblStepEnd - pxnumMinStep) / pxnumBinSize;
			int num4 = ((num2 < 0.0) ? (-1) : ((int)Math.Floor(num2)));
			if (num3 < 0 && num4 >= 0)
			{
				num3 = 0;
			}
			if (num4 >= nHistSize && num3 < nHistSize)
			{
				num4 = nHistSize - 1;
			}
			num3 = Math.Max(num3, 0);
			while (num3 <= num4 && num3 < nHistSize)
			{
				num += rgnHistogram[num3];
				num3++;
			}
			return num;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003C04 File Offset: 0x00001E04
		private void CheckStepOfSeveralYears(double pxnumLowStep, double pxnumHighStep)
		{
			uint num = (uint)Math.Round(pxnumLowStep / 365.0);
			if (num > 1U && num <= 100U)
			{
				uint num2 = (uint)(num * 365.0 + num / 4U);
				uint num3 = (uint)(2.0 + Math.Log(num, 2.0));
				if (ForecastStepData.FOverlapsStep(pxnumLowStep, pxnumHighStep, num2 - num3, num2 + num3))
				{
					this.m_xnumCalculatedTimeStep = num2;
					this.m_nMonthsInStep = (int)(12U * num);
				}
			}
		}

		// Token: 0x04000077 RID: 119
		private const double dblMonthlyLow = 30.0;

		// Token: 0x04000078 RID: 120
		private const double dblMonthlyHigh = 31.0;

		// Token: 0x04000079 RID: 121
		private const double dblMonthlyNumeric = 30.5;

		// Token: 0x0400007A RID: 122
		private const double dblQuarteryLow = 90.0;

		// Token: 0x0400007B RID: 123
		private const double dblQuarteryHigh = 92.0;

		// Token: 0x0400007C RID: 124
		private const double dblQuarteryNumeric = 91.0;

		// Token: 0x0400007D RID: 125
		private const double dblHalfYearlyLow = 181.5;

		// Token: 0x0400007E RID: 126
		private const double dblHalfYearlyHigh = 184.5;

		// Token: 0x0400007F RID: 127
		private const double dblHalfYearlyNumeric = 183.0;

		// Token: 0x04000080 RID: 128
		private const double dblYearlyLow = 363.0;

		// Token: 0x04000081 RID: 129
		private const double dblYearlyHigh = 367.0;

		// Token: 0x04000082 RID: 130
		private const double dblYearlyNumeric = 365.0;

		// Token: 0x04000083 RID: 131
		private const int c_cTolerance = 1;

		// Token: 0x04000084 RID: 132
		private double DayMax;

		// Token: 0x04000085 RID: 133
		private double m_xnumNumericTimeStep;

		// Token: 0x04000086 RID: 134
		private int m_drwStepCount;

		// Token: 0x04000087 RID: 135
		private double m_xnumLastTimePoint;

		// Token: 0x04000088 RID: 136
		private bool m_fValidDates;

		// Token: 0x04000089 RID: 137
		private int m_nMonthsInStep;

		// Token: 0x0400008A RID: 138
		private double m_xnumCalculatedTimeStep;

		// Token: 0x0400008B RID: 139
		private bool m_f1904;

		// Token: 0x0400008C RID: 140
		private bool m_fDateCompat;

		// Token: 0x0400008D RID: 141
		private DateTime m_dtrLastInput;

		// Token: 0x0400008E RID: 142
		private DateTime m_dtrShiftDate;

		// Token: 0x0200000F RID: 15
		internal enum BusinessStepMonthsType
		{
			// Token: 0x040000B4 RID: 180
			bsmtUnverified = -2,
			// Token: 0x040000B5 RID: 181
			bsmtUninitialized,
			// Token: 0x040000B6 RID: 182
			bsmtNoBusinessStep,
			// Token: 0x040000B7 RID: 183
			bsmtMonthly,
			// Token: 0x040000B8 RID: 184
			bsmtQuarterly = 3,
			// Token: 0x040000B9 RID: 185
			bsmtHalfYearly = 6,
			// Token: 0x040000BA RID: 186
			bsmtYearly = 12
		}

		// Token: 0x02000010 RID: 16
		internal enum SGN
		{
			// Token: 0x040000BC RID: 188
			sgnLT = -1,
			// Token: 0x040000BD RID: 189
			sgnEQ,
			// Token: 0x040000BE RID: 190
			sgnGT,
			// Token: 0x040000BF RID: 191
			sgnNE
		}
	}
}
