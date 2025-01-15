using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Analytics.ExponentialSmoothing
{
	// Token: 0x02000010 RID: 16
	public class SeasonalityDetector
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00004E60 File Offset: 0x00003060
		public static bool Fail(int hr)
		{
			return hr != 0 && hr != 1;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004E70 File Offset: 0x00003070
		public SeasonalityDetector()
		{
			this.m_rgxnumData = null;
			this.m_dataSize = 0U;
			this.m_xnumDataSlope = 0.0;
			this.m_xnumDataIntercept = 0.0;
			this.m_rgxnumDetrended = null;
			this.m_rgxnumDetrendedZNormalized = null;
			this.m_lowFreqPeriod = 1U;
			this.m_highFreqPeriod = 1U;
			this.m_finalPeriod = 1U;
			this.m_xnumHighFreqPower = 0.0;
			this.m_candidatePeriodsPlex = null;
			this.m_intervalsPlex = null;
			this.m_periodPowersPlex = null;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004EF8 File Offset: 0x000030F8
		public int Init(double[] rgxnumData, uint dataSize)
		{
			if (this.m_rgxnumDetrended != null || this.m_rgxnumDetrendedZNormalized != null || this.m_candidatePeriodsPlex != null || this.m_intervalsPlex != null || this.m_periodPowersPlex != null)
			{
				throw new Exception("array already allocated");
			}
			this.m_lowFreqPeriod = 1U;
			this.m_highFreqPeriod = 1U;
			this.m_finalPeriod = 1U;
			this.m_xnumHighFreqPower = 0.0;
			if (dataSize > 2048U)
			{
				uint num = dataSize;
				this.m_dataSize = 1U;
				for (;;)
				{
					dataSize >>= 1;
					if (dataSize == 0U)
					{
						break;
					}
					this.m_dataSize <<= 1;
				}
				this.m_rgxnumData = rgxnumData.Skip((int)(num - this.m_dataSize)).ToArray<double>();
			}
			else
			{
				this.m_dataSize = dataSize;
				this.m_rgxnumData = rgxnumData;
			}
			this.PrepareData();
			this.m_spectrumComputerDetrended = new SpectrumComputer();
			int num2 = this.m_spectrumComputerDetrended.Init(this.m_rgxnumDetrended, this.m_dataSize);
			if (SeasonalityDetector.Fail(num2))
			{
				this.LErrorHelper();
				return num2;
			}
			this.m_spectrumComputerDetrendedZNormalized = new SpectrumComputer();
			num2 = this.m_spectrumComputerDetrendedZNormalized.Init(this.m_rgxnumDetrendedZNormalized, this.m_dataSize);
			if (SeasonalityDetector.Fail(num2))
			{
				this.LErrorHelper();
				return num2;
			}
			this.m_correlationComputerPearson = new CorrelationComputer();
			num2 = this.m_correlationComputerPearson.Init(this.m_rgxnumDetrendedZNormalized, this.m_dataSize);
			if (SeasonalityDetector.Fail(num2))
			{
				this.LErrorHelper();
				return num2;
			}
			this.m_correlationComputerSpearman = new CorrelationComputer();
			num2 = this.m_correlationComputerSpearman.Init(this.m_rgxnumDetrendedZNormalized, this.m_dataSize);
			if (SeasonalityDetector.Fail(num2))
			{
				this.LErrorHelper();
				return num2;
			}
			this.m_candidatePeriodsPlex = new FastPlex<UintComplexNumPair>(0);
			this.m_intervalsPlex = new FastPlex<PeriodInterval>(0);
			this.m_periodPowersPlex = new FastPlex<PeriodPower>(0);
			return num2;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000050A8 File Offset: 0x000032A8
		private void LErrorHelper()
		{
			this.m_candidatePeriodsPlex = null;
			this.m_intervalsPlex = null;
			this.m_periodPowersPlex = null;
			this.m_spectrumComputerDetrended.Destroy();
			this.m_spectrumComputerDetrendedZNormalized.Destroy();
			this.m_correlationComputerPearson.Destroy();
			this.m_correlationComputerSpearman.Destroy();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000050F8 File Offset: 0x000032F8
		public void Destroy()
		{
			this.m_spectrumComputerDetrended.Destroy();
			this.m_spectrumComputerDetrendedZNormalized.Destroy();
			this.m_correlationComputerPearson.Destroy();
			this.m_correlationComputerSpearman.Destroy();
			this.m_rgxnumDetrended = null;
			this.m_rgxnumDetrendedZNormalized = null;
			this.m_candidatePeriodsPlex = null;
			this.m_intervalsPlex = null;
			this.m_periodPowersPlex = null;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005154 File Offset: 0x00003354
		public int DetectSeasonality(out int seasonality)
		{
			seasonality = 0;
			int num = this.FindHighFrequencies();
			if (SeasonalityDetector.Fail(num))
			{
				return num;
			}
			num = this.FindLowFrequencies();
			if (SeasonalityDetector.Fail(num))
			{
				return num;
			}
			this.FinalizePeriods();
			seasonality = (int)this.m_finalPeriod;
			return num;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005198 File Offset: 0x00003398
		public SeasonStats ComputeSeasonStatistics()
		{
			double num = SeasonalityDetector.ComputeLengthNormalizedPower(this.m_lowFreqPeriod, this.m_rgxnumData, this.m_dataSize);
			double num2 = SeasonalityDetector.ComputeLengthNormalizedPower(this.m_highFreqPeriod, this.m_rgxnumData, this.m_dataSize);
			return new SeasonStats((int)this.m_finalPeriod, new List<int>
			{
				(int)this.m_lowFreqPeriod,
				(int)this.m_highFreqPeriod
			}, new List<double> { num, num2 });
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005210 File Offset: 0x00003410
		private int _SgnComparePairFirstElement(UintComplexNumPair ppair1, UintComplexNumPair ppair2)
		{
			if (ppair1.First > ppair2.First)
			{
				return 1;
			}
			if (ppair1.First < ppair2.First)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005234 File Offset: 0x00003434
		private static void LinearSolve(int[] refData, double[] data, uint start, uint end, out double slope, out double intercept)
		{
			slope = 0.0;
			intercept = 0.0;
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			double num4 = 0.0;
			for (uint num5 = start; num5 < end; num5 += 1U)
			{
				num += (double)refData[(int)num5];
				num3 += (double)(refData[(int)num5] * refData[(int)num5]);
				num2 += data[(int)num5];
				num4 += (double)refData[(int)num5] * data[(int)num5];
			}
			double num6 = end - start;
			double num7 = num6 * num3 - num * num;
			double num8 = num6 * num4 - num * num2;
			slope = num8 / num7;
			intercept = num2 / num6 - slope * num / num6;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000052F0 File Offset: 0x000034F0
		private void PrepareData()
		{
			double num = 0.0;
			double num2 = 0.0;
			int[] array = new int[this.m_dataSize];
			this.m_rgxnumDetrended = new double[this.m_dataSize];
			this.m_rgxnumDetrendedZNormalized = new double[this.m_dataSize];
			int num3 = 0;
			while ((long)num3 < (long)((ulong)this.m_dataSize))
			{
				array[num3] = num3 + 1;
				num3++;
			}
			SeasonalityDetector.LinearSolve(array, this.m_rgxnumData, 0U, this.m_dataSize, out this.m_xnumDataSlope, out this.m_xnumDataIntercept);
			for (uint num4 = 0U; num4 < this.m_dataSize; num4 += 1U)
			{
				this.m_rgxnumDetrended[(int)num4] = this.m_rgxnumData[(int)num4] - (this.m_xnumDataSlope * (num4 + 1U) + this.m_xnumDataIntercept);
				num += this.m_rgxnumDetrended[(int)num4];
				num2 += this.m_rgxnumDetrended[(int)num4] * this.m_rgxnumDetrended[(int)num4];
			}
			double num5 = this.m_dataSize;
			double num6 = this.m_dataSize - 1U;
			num /= num5;
			num2 = Math.Sqrt(num2 / num6 - num * num);
			if (num2 == 0.0)
			{
				int num7 = 0;
				while ((long)num7 < (long)((ulong)this.m_dataSize))
				{
					this.m_rgxnumDetrendedZNormalized[num7] = this.m_rgxnumDetrended[num7];
					num7++;
				}
				return;
			}
			for (uint num8 = 0U; num8 < this.m_dataSize; num8 += 1U)
			{
				this.m_rgxnumDetrendedZNormalized[(int)num8] = (this.m_rgxnumDetrended[(int)num8] - num) / num2;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005468 File Offset: 0x00003668
		private int FindHighFrequencies()
		{
			uint num = Math.Min(2U * this.m_dataSize / 5U, 5U);
			int num2 = this.m_spectrumComputerDetrended.ComputePower();
			if (SeasonalityDetector.Fail(num2))
			{
				return num2;
			}
			if (this.m_spectrumComputerDetrended.NGetPowerSize < 2U)
			{
				return 0;
			}
			LocationPower[] plpGetPower = this.m_spectrumComputerDetrended.PlpGetPower;
			this.m_xnumHighFreqPower = plpGetPower[0].Power;
			double num3 = plpGetPower[1].Power;
			uint num4 = (uint)Math.Round(1.0 / plpGetPower[0].Location);
			if (num3 > this.m_xnumHighFreqPower)
			{
				double xnumHighFreqPower = this.m_xnumHighFreqPower;
				this.m_xnumHighFreqPower = num3;
				num3 = xnumHighFreqPower;
				num4 = (uint)Math.Round(1.0 / plpGetPower[1].Location - 3E-07);
			}
			for (uint num5 = 2U; num5 < this.m_spectrumComputerDetrended.NGetPowerSize; num5 += 1U)
			{
				if (plpGetPower[(int)num5].Power > this.m_xnumHighFreqPower)
				{
					num3 = this.m_xnumHighFreqPower;
					this.m_xnumHighFreqPower = plpGetPower[(int)num5].Power;
					num4 = (uint)Math.Round(1.0 / plpGetPower[(int)num5].Location - 3E-07);
				}
				else if (plpGetPower[(int)num5].Power > num3)
				{
					num3 = plpGetPower[(int)num5].Power;
				}
			}
			if (num4 >= 2U && num4 <= num)
			{
				double num6 = 0.0;
				if (num3 != 0.0)
				{
					num6 = this.m_xnumHighFreqPower / num3;
				}
				double num7 = 2.0 - 3.0 / this.m_dataSize;
				if (num6 > num7 && this.m_xnumHighFreqPower > this.m_spectrumComputerDetrended.PXNUMGetPowerMean)
				{
					this.m_highFreqPeriod = num4;
				}
				return num2;
			}
			double num8 = this.m_spectrumComputerDetrended.PXNUMGetPowerMean + 2.5 * this.m_spectrumComputerDetrended.PXNUMGetPowerStDev;
			if (this.m_xnumHighFreqPower > num8 && num4 * 2.5 <= this.m_dataSize)
			{
				this.m_highFreqPeriod = num4;
			}
			return num2;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005660 File Offset: 0x00003860
		private int FindLowFrequencies()
		{
			int num = this.m_spectrumComputerDetrendedZNormalized.ComputePower();
			if (SeasonalityDetector.Fail(num))
			{
				return num;
			}
			num = this.BuildCandidatePeriods();
			if (SeasonalityDetector.Fail(num))
			{
				return num;
			}
			if (this.m_candidatePeriodsPlex.GetCount() == 0)
			{
				return num;
			}
			num = this.BuildIntervals();
			if (SeasonalityDetector.Fail(num))
			{
				return num;
			}
			num = this.ProcessIntervals();
			if (SeasonalityDetector.Fail(num))
			{
				return num;
			}
			return this.SelectTopPeak();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000056D0 File Offset: 0x000038D0
		private int BuildCandidatePeriods()
		{
			int num = 0;
			FastPlex<UintComplexNumPair> fastPlex = null;
			LocationPower[] plpGetPower = this.m_spectrumComputerDetrendedZNormalized.PlpGetPower;
			fastPlex = new FastPlex<UintComplexNumPair>(0);
			HashSet<uint> hashSet = new HashSet<uint> { 4U, 7U, 24U, 29U, 30U, 31U, 60U, 120U, 365U };
			Dictionary<uint, UintComplexNumPair> dictionary = new Dictionary<uint, UintComplexNumPair>();
			for (uint num2 = 0U; num2 < this.m_spectrumComputerDetrendedZNormalized.NGetPowerSize; num2 += 1U)
			{
				LocationPower locationPower = plpGetPower[(int)num2];
				if (locationPower.Power > this.m_spectrumComputerDetrendedZNormalized.PXNUMGetPowerMean)
				{
					uint num3 = (uint)Math.Round(1.0 / locationPower.Location, MidpointRounding.AwayFromZero);
					UintComplexNumPair uintComplexNumPair = new UintComplexNumPair(num3, locationPower.Power);
					if (num3 >= 4U && 2U * num3 < this.m_dataSize)
					{
						num = this.m_candidatePeriodsPlex.HrAddItem(uintComplexNumPair);
						if (SeasonalityDetector.Fail(num))
						{
							return num;
						}
						if (num2 != 0U && num2 != this.m_spectrumComputerDetrendedZNormalized.NGetPowerSize - 1U)
						{
							double power = plpGetPower[(int)(num2 - 1U)].Power;
							double power2 = plpGetPower[(int)(num2 + 1U)].Power;
							if (power == 0.0 || power2 == 0.0)
							{
								if (power == 0.0 && power2 == 0.0)
								{
									num = fastPlex.HrAddItem(uintComplexNumPair);
									if (SeasonalityDetector.Fail(num))
									{
										return num;
									}
								}
							}
							else
							{
								double num4 = locationPower.Power / power;
								double num5 = locationPower.Power / power2;
								if (num4 > 4.0 && num5 > 4.0)
								{
									num = fastPlex.HrAddItem(uintComplexNumPair);
									if (SeasonalityDetector.Fail(num))
									{
										return num;
									}
								}
							}
						}
					}
				}
			}
			this.m_candidatePeriodsPlex.Sort(new SeasonalityDetector._SgnComparePairSecondElementReverse());
			while (this.m_candidatePeriodsPlex.GetCount() > 3)
			{
				UintComplexNumPair uintComplexNumPair2 = this.m_candidatePeriodsPlex.PFromI(this.m_candidatePeriodsPlex.GetCount() - 1);
				if (hashSet.Contains(uintComplexNumPair2.First))
				{
					UintComplexNumPair uintComplexNumPair3;
					if (!dictionary.TryGetValue(uintComplexNumPair2.First, out uintComplexNumPair3))
					{
						dictionary.Add(uintComplexNumPair2.First, uintComplexNumPair2);
					}
					else if (uintComplexNumPair3.Second < uintComplexNumPair2.Second)
					{
						dictionary[uintComplexNumPair2.First] = uintComplexNumPair2;
					}
				}
				this.m_candidatePeriodsPlex.DeleteLastItem();
			}
			if (dictionary.Any<KeyValuePair<uint, UintComplexNumPair>>())
			{
				foreach (UintComplexNumPair uintComplexNumPair4 in from x in dictionary
					select x.Value into x
					orderby -x.Second
					select x)
				{
					this.m_candidatePeriodsPlex.HrAddItem(uintComplexNumPair4);
				}
			}
			for (int i = 0; i < fastPlex.GetCount(); i++)
			{
				UintComplexNumPair uintComplexNumPair5 = fastPlex.PFromI(i);
				bool flag = false;
				for (int j = 0; j < this.m_candidatePeriodsPlex.GetCount(); j++)
				{
					if (this._SgnComparePairFirstElement(this.m_candidatePeriodsPlex.PFromI(j), uintComplexNumPair5) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					num = this.m_candidatePeriodsPlex.HrAddItem(uintComplexNumPair5);
					if (SeasonalityDetector.Fail(num))
					{
						return num;
					}
				}
			}
			fastPlex = null;
			return num;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005A58 File Offset: 0x00003C58
		private int BuildIntervals()
		{
			int num = 0;
			double num2 = this.m_dataSize;
			for (int i = 0; i < this.m_candidatePeriodsPlex.GetCount(); i++)
			{
				UintComplexNumPair uintComplexNumPair = this.m_candidatePeriodsPlex.PFromI(i);
				double num3 = num2 / uintComplexNumPair.First;
				uint num4 = (uint)Math.Round(num2 / (num3 + 1.0) - 1.0);
				uint num5 = (uint)Math.Round(num2 / (num3 - 1.0) + 1.0);
				if (uintComplexNumPair.First != 6U)
				{
					if (uintComplexNumPair.First != 7U)
					{
						goto IL_0095;
					}
				}
				while (num5 - num4 + 1U < 5U)
				{
					num4 -= 1U;
					num5 += 1U;
				}
				IL_0095:
				if (num4 <= 3U)
				{
					num4 = 3U;
					if (num5 < num4)
					{
						num5 = num4 + 2U;
					}
				}
				if (num5 > this.m_dataSize)
				{
					num5 = num4;
				}
				PeriodInterval periodInterval = new PeriodInterval(uintComplexNumPair.First, num4, num5);
				num = this.m_intervalsPlex.HrAddItem(periodInterval);
				if (SeasonalityDetector.Fail(num))
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005B5C File Offset: 0x00003D5C
		private int ProcessIntervals()
		{
			int num = 0;
			for (int i = 0; i < this.m_intervalsPlex.GetCount(); i++)
			{
				PeriodInterval periodInterval = this.m_intervalsPlex.PFromI(i);
				foreach (CorrelationComputer correlationComputer in new CorrelationComputer[] { this.m_correlationComputerPearson, this.m_correlationComputerSpearman })
				{
					PeriodInterval periodInterval2 = new PeriodInterval(periodInterval.Period, periodInterval.Min, periodInterval.Max);
					uint num2 = (uint)Math.Floor(periodInterval.Period / 2.0);
					periodInterval2.Min = ((num2 < periodInterval.Max) ? Math.Max(periodInterval.Min, num2) : periodInterval.Min);
					PeriodPower periodPower;
					num = this.CheckPeak(periodInterval2, false, correlationComputer, out periodPower);
					if (SeasonalityDetector.Fail(num))
					{
						return num;
					}
					if (num == 1 && 2U * periodInterval2.Max < this.m_dataSize)
					{
						periodInterval2.Period *= 2U;
						periodInterval2.Min *= 2U;
						periodInterval2.Max *= 2U;
						num = this.CheckPeak(periodInterval2, true, correlationComputer, out periodPower);
						if (SeasonalityDetector.Fail(num))
						{
							return num;
						}
					}
					double num3 = 0.45;
					if (num != 1 && periodPower.Correlation > num3)
					{
						int num4 = -1;
						for (int k = 0; k < this.m_periodPowersPlex.GetCount(); k++)
						{
							if (this.m_periodPowersPlex.PFromI(k).Period == periodPower.Period)
							{
								num4 = k;
								break;
							}
						}
						if (num4 == -1)
						{
							num = this.m_periodPowersPlex.HrAddItem(periodPower);
							if (SeasonalityDetector.Fail(num))
							{
								return num;
							}
						}
						else if (this.m_periodPowersPlex.PFromI(num4).Correlation < periodPower.Correlation)
						{
							this.m_periodPowersPlex.PFromI(num4).Correlation = periodPower.Correlation;
						}
					}
				}
			}
			LocationPower[] plpGetPower = this.m_spectrumComputerDetrendedZNormalized.PlpGetPower;
			for (int l = 0; l < this.m_periodPowersPlex.GetCount(); l++)
			{
				PeriodPower periodPower2 = this.m_periodPowersPlex.PFromI(l);
				double num5 = double.MaxValue;
				double num6 = 1.0 / periodPower2.Period;
				for (uint num7 = 0U; num7 < this.m_spectrumComputerDetrendedZNormalized.NGetPowerSize; num7 += 1U)
				{
					double num8 = Math.Abs(num6 - plpGetPower[(int)num7].Location);
					if (num8 < num5)
					{
						num5 = num8;
						periodPower2.SpectrumPower = plpGetPower[(int)num7].Power;
					}
				}
			}
			return num;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005DF0 File Offset: 0x00003FF0
		private int CheckPeak(PeriodInterval crInterval, bool fHarmony, CorrelationComputer correlationComputer, out PeriodPower periodPowerPeak)
		{
			uint num = crInterval.Max - crInterval.Min + 1U;
			double num2 = double.MinValue;
			uint num3 = 0U;
			periodPowerPeak = null;
			double[] array = new double[num];
			int num4 = (int)crInterval.Min;
			while ((long)num4 <= (long)((ulong)crInterval.Max))
			{
				checked
				{
					int laggedCorrelation = correlationComputer.GetLaggedCorrelation((uint)num4, ref array[(int)((IntPtr)(unchecked((long)num4 - (long)((ulong)crInterval.Min))))]);
					if (SeasonalityDetector.Fail(laggedCorrelation))
					{
						return laggedCorrelation;
					}
					if (array[(int)((IntPtr)(unchecked((long)num4 - (long)((ulong)crInterval.Min))))] > num2)
					{
						num2 = array[(int)((IntPtr)(unchecked((long)num4 - (long)((ulong)crInterval.Min))))];
						num3 = (uint)num4;
					}
				}
				num4++;
			}
			uint num5 = num3;
			if (fHarmony)
			{
				num5 = (uint)Math.Floor(num3 / 2.0);
			}
			if (2.5 * num5 > this.m_dataSize)
			{
				return 1;
			}
			if (num3 == crInterval.Min || num3 == crInterval.Max)
			{
				return 1;
			}
			if ((num3 >= 3U && num3 < 6U) || (fHarmony && num3 >= 6U && num3 < 12U))
			{
				return this.CheckPeakSmallPeriod(num3, num5, correlationComputer, out periodPowerPeak);
			}
			if (num < 5U)
			{
				return 1;
			}
			return this.CheckPeakLargePeriod(crInterval, num5, array, correlationComputer, out periodPowerPeak);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005F1C File Offset: 0x0000411C
		private int CheckPeakSmallPeriod(uint peakPeriod, uint actualPeakPeriod, CorrelationComputer correlationComputer, out PeriodPower periodPowerPeak)
		{
			uint num = ((peakPeriod == 5U) ? 4U : 2U);
			periodPowerPeak = null;
			int[] array = new int[2U * num + 1U];
			double[] array2 = new double[2U * num + 1U];
			int num2 = 0;
			while ((long)num2 <= (long)((ulong)(2U * num)))
			{
				array[num2] = (int)((ulong)(peakPeriod - num) + (ulong)((long)num2));
				int num3 = correlationComputer.GetLaggedCorrelation((uint)array[num2], ref array2[num2]);
				if (SeasonalityDetector.Fail(num3))
				{
					return num3;
				}
				num2++;
			}
			double num4;
			double num5;
			SeasonalityDetector.LinearSolve(array, array2, 0U, num + 1U, out num4, out num5);
			double num6;
			double num7;
			SeasonalityDetector.LinearSolve(array, array2, num, 2U * num + 1U, out num6, out num7);
			if (num4 > 0.0 && num6 < 0.0)
			{
				periodPowerPeak = new PeriodPower(actualPeakPeriod, 0.0, 0.0);
				double correlation = periodPowerPeak.Correlation;
				int num3 = correlationComputer.GetLaggedCorrelation(actualPeakPeriod, ref correlation);
				periodPowerPeak.Correlation = correlation;
				return num3;
			}
			return 1;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00006008 File Offset: 0x00004208
		private int CheckPeakLargePeriod(PeriodInterval crInterval, uint actualPeakPeriod, double[] rgxnumCorrelationInInterval, CorrelationComputer correlationComputer, out PeriodPower periodPowerPeak)
		{
			uint num = crInterval.Max - crInterval.Min + 1U;
			int[] array = new int[6];
			double[] array2 = new double[6];
			uint num2 = 0U;
			int[] array3 = null;
			double[] array4 = null;
			bool flag = false;
			double num3 = double.MaxValue;
			periodPowerPeak = null;
			int num4;
			for (int i = -2; i <= 0; i++)
			{
				if ((ulong)crInterval.Min + (ulong)((long)i) >= 3UL)
				{
					array[(int)num2] = (int)(crInterval.Min + (uint)i);
					num4 = correlationComputer.GetLaggedCorrelation((uint)(array[(int)num2] - 1), ref array2[(int)num2]);
					if (SeasonalityDetector.Fail(num4))
					{
						if (array3 != null)
						{
						}
						if (array4 != null)
						{
						}
						return num4;
					}
					num2 += 1U;
				}
			}
			for (int j = 0; j <= 2; j++)
			{
				if ((ulong)crInterval.Max + (ulong)((long)j) <= (ulong)this.m_dataSize)
				{
					array[(int)num2] = (int)(crInterval.Max + (uint)j);
					num4 = correlationComputer.GetLaggedCorrelation((uint)(array[(int)num2] - 1), ref array2[(int)num2]);
					if (SeasonalityDetector.Fail(num4))
					{
						if (array3 != null)
						{
						}
						if (array4 != null)
						{
						}
						return num4;
					}
					num2 += 1U;
				}
			}
			double num5;
			double num6;
			SeasonalityDetector.LinearSolve(array, array2, 0U, num2, out num5, out num6);
			array3 = new int[num];
			array4 = new double[num];
			for (uint num7 = 0U; num7 < num; num7 += 1U)
			{
				array4[(int)num7] = rgxnumCorrelationInInterval[(int)num7] - num5 * (crInterval.Min + num7) - num6;
				array3[(int)num7] = (int)(crInterval.Min + num7);
			}
			uint num8 = (uint)Math.Ceiling((num - 4U) / 100.0);
			for (uint num9 = 2U; num9 < num - 2U; num9 += num8)
			{
				double num10 = 0.0;
				double num11 = 0.0;
				double num12;
				double num13;
				SeasonalityDetector.LinearSolve(array3, array4, 0U, num9 + 1U, out num12, out num13);
				double num14;
				double num15;
				SeasonalityDetector.LinearSolve(array3, array4, num9, num, out num14, out num15);
				for (uint num16 = 0U; num16 <= num9; num16 += 1U)
				{
					double num17 = array4[(int)num16] - num12 * (crInterval.Min + num16) - num13;
					num10 += num17 * num17;
				}
				num10 /= num9 + 1U;
				int num18 = (int)num9;
				while ((long)num18 <= (long)((ulong)(num - 1U)))
				{
					double num19 = array4[num18] - num14 * (double)((ulong)crInterval.Min + (ulong)((long)num18)) - num15;
					num11 += num19 * num19;
					num18++;
				}
				num11 /= num - num9;
				num10 += num11;
				if (num10 < num3)
				{
					num3 = num10;
					if (num12 > 0.0 && num14 < 0.0)
					{
						periodPowerPeak = new PeriodPower(actualPeakPeriod, 0.0, 0.0);
						double correlation = periodPowerPeak.Correlation;
						num4 = correlationComputer.GetLaggedCorrelation(actualPeakPeriod, ref correlation);
						periodPowerPeak.Correlation = correlation;
						if (SeasonalityDetector.Fail(num4))
						{
							if (array3 != null)
							{
							}
							if (array4 != null)
							{
							}
							return num4;
						}
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
			}
			num4 = (flag ? 0 : 1);
			if (array3 != null)
			{
			}
			if (array4 != null)
			{
			}
			return num4;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000062F8 File Offset: 0x000044F8
		private int SelectTopPeak()
		{
			int num = 0;
			FastPlex<PeriodPower> fastPlex = null;
			double num2 = double.MinValue;
			int i = 1;
			if (this.m_periodPowersPlex.GetCount() == 0)
			{
				if (fastPlex != null)
				{
				}
				return num;
			}
			if (this.m_periodPowersPlex.GetCount() == 1)
			{
				this.m_lowFreqPeriod = this.m_periodPowersPlex.PFromI(0).Period;
				if (fastPlex != null)
				{
				}
				return num;
			}
			fastPlex = new FastPlex<PeriodPower>(0);
			for (int j = 0; j < this.m_periodPowersPlex.GetCount(); j++)
			{
				num = fastPlex.HrAddItem(this.m_periodPowersPlex.PFromI(j));
				if (SeasonalityDetector.Fail(num))
				{
					if (fastPlex != null)
					{
					}
					return num;
				}
			}
			if (this.m_highFreqPeriod >= 2U && this.m_highFreqPeriod <= 5U)
			{
				double num3 = 0.0;
				int num4 = -1;
				num = this.m_correlationComputerSpearman.GetLaggedCorrelation(this.m_highFreqPeriod, ref num3);
				if (SeasonalityDetector.Fail(num))
				{
					if (fastPlex != null)
					{
					}
					return num;
				}
				PeriodPower periodPower = new PeriodPower(this.m_highFreqPeriod, this.m_xnumHighFreqPower, num3);
				for (int k = 0; k < fastPlex.GetCount(); k++)
				{
					if (fastPlex.PFromI(k).Period == periodPower.Period)
					{
						num4 = k;
						break;
					}
				}
				if (num4 == -1)
				{
					num = fastPlex.HrAddItem(periodPower);
				}
				else
				{
					PeriodPower periodPower2 = fastPlex.PFromI(num4);
					periodPower2.SpectrumPower = ((periodPower2.SpectrumPower > this.m_xnumHighFreqPower) ? periodPower2.SpectrumPower : this.m_xnumHighFreqPower);
					periodPower2.Correlation = ((periodPower2.Correlation > num3) ? periodPower2.Correlation : num3);
				}
			}
			fastPlex.Sort(new SeasonalityDetector._SgnComparePeriodPowerSpectrumReverse());
			int l = 1;
			while (l < fastPlex.GetCount())
			{
				if (fastPlex.PFromI(l - 1).SpectrumPower / fastPlex.PFromI(l).SpectrumPower > 2.0)
				{
					i = l - 1;
					break;
				}
				l++;
				i++;
			}
			if (i >= fastPlex.GetCount())
			{
				i = fastPlex.GetCount() - 1;
			}
			while (i >= 0)
			{
				if (fastPlex.PFromI(i).Correlation > num2)
				{
					num2 = fastPlex.PFromI(i).Correlation;
					this.m_lowFreqPeriod = fastPlex.PFromI(i).Period;
				}
				i--;
			}
			if (fastPlex != null)
			{
			}
			return num;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00006528 File Offset: 0x00004728
		private void FinalizePeriods()
		{
			uint[] array = new uint[] { this.m_lowFreqPeriod, this.m_highFreqPeriod };
			double[] array2 = new double[2];
			double num = double.MinValue;
			if (this.m_lowFreqPeriod == 1U || this.m_highFreqPeriod == 1U)
			{
				this.m_finalPeriod = ((this.m_lowFreqPeriod == 1U) ? this.m_highFreqPeriod : this.m_lowFreqPeriod);
				return;
			}
			if (this.m_lowFreqPeriod >= 6U)
			{
				this.m_finalPeriod = this.m_lowFreqPeriod;
				return;
			}
			if (2U * this.m_highFreqPeriod > this.m_dataSize)
			{
				this.m_finalPeriod = this.m_lowFreqPeriod;
				return;
			}
			for (uint num2 = 0U; num2 < 2U; num2 += 1U)
			{
				array2[(int)num2] = SeasonalityDetector.ComputeLengthNormalizedPower(array[(int)num2], this.m_rgxnumData, this.m_dataSize);
			}
			for (uint num3 = 0U; num3 < 2U; num3 += 1U)
			{
				if (array2[(int)num3] > num)
				{
					num = array2[(int)num3];
					this.m_finalPeriod = array[(int)num3];
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000660C File Offset: 0x0000480C
		public static double ComputeLengthNormalizedPower(uint period, IReadOnlyList<double> data, uint dataSize)
		{
			double num = dataSize;
			double num2 = period;
			uint num3 = (uint)Math.Floor(num / num2);
			uint num4 = dataSize - num3 * period;
			double num5 = 0.0;
			double num6 = 0.0;
			for (uint num7 = num4; num7 < dataSize; num7 += 1U)
			{
				double num8 = -6.283185307179586 * (num7 - num4) / num2;
				num5 += data[(int)num7] * Math.Cos(num8);
				num6 += data[(int)num7] * Math.Sin(num8);
			}
			if (num3 == 0U)
			{
				throw new Exception("expecting nFullPeriods != 0");
			}
			double num9 = num3 * period;
			return Math.Sqrt(num5 * num5 + num6 * num6) / num9;
		}

		// Token: 0x0400006C RID: 108
		private const int ndftThreshold = 2048;

		// Token: 0x0400006D RID: 109
		private const double ndataSizeThreshold = 2.5;

		// Token: 0x0400006E RID: 110
		private const double npowerThreshold = 2.5;

		// Token: 0x0400006F RID: 111
		private const double ncorrelationThreshold = 0.45;

		// Token: 0x04000070 RID: 112
		private double[] m_rgxnumData;

		// Token: 0x04000071 RID: 113
		private uint m_dataSize;

		// Token: 0x04000072 RID: 114
		private double m_xnumDataSlope;

		// Token: 0x04000073 RID: 115
		private double m_xnumDataIntercept;

		// Token: 0x04000074 RID: 116
		private double[] m_rgxnumDetrended;

		// Token: 0x04000075 RID: 117
		private double[] m_rgxnumDetrendedZNormalized;

		// Token: 0x04000076 RID: 118
		private uint m_lowFreqPeriod;

		// Token: 0x04000077 RID: 119
		private uint m_highFreqPeriod;

		// Token: 0x04000078 RID: 120
		private uint m_finalPeriod;

		// Token: 0x04000079 RID: 121
		private double m_xnumHighFreqPower;

		// Token: 0x0400007A RID: 122
		private FastPlex<UintComplexNumPair> m_candidatePeriodsPlex;

		// Token: 0x0400007B RID: 123
		private FastPlex<PeriodInterval> m_intervalsPlex;

		// Token: 0x0400007C RID: 124
		private FastPlex<PeriodPower> m_periodPowersPlex;

		// Token: 0x0400007D RID: 125
		private SpectrumComputer m_spectrumComputerDetrended;

		// Token: 0x0400007E RID: 126
		private SpectrumComputer m_spectrumComputerDetrendedZNormalized;

		// Token: 0x0400007F RID: 127
		private CorrelationComputer m_correlationComputerPearson;

		// Token: 0x04000080 RID: 128
		private CorrelationComputer m_correlationComputerSpearman;

		// Token: 0x04000081 RID: 129
		public const int hrFalse = 1;

		// Token: 0x02000014 RID: 20
		private class _SgnComparePairSecondElementReverse : IComparer<UintComplexNumPair>
		{
			// Token: 0x06000083 RID: 131 RVA: 0x000066F2 File Offset: 0x000048F2
			public int Compare(UintComplexNumPair ppair1, UintComplexNumPair ppair2)
			{
				if (ppair1.Second > ppair2.Second)
				{
					return -1;
				}
				if (ppair1.Second < ppair2.Second)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x02000015 RID: 21
		private class _SgnComparePeriodPowerPeriod : IComparer<PeriodPower>
		{
			// Token: 0x06000085 RID: 133 RVA: 0x0000671D File Offset: 0x0000491D
			public int Compare(PeriodPower pPeriodPower1, PeriodPower pPeriodPower2)
			{
				if (pPeriodPower1.SpectrumPower > pPeriodPower2.SpectrumPower)
				{
					return 1;
				}
				if (pPeriodPower1.SpectrumPower < pPeriodPower2.SpectrumPower)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x02000016 RID: 22
		private class _SgnComparePeriodPowerSpectrumReverse : IComparer<PeriodPower>
		{
			// Token: 0x06000087 RID: 135 RVA: 0x00006748 File Offset: 0x00004948
			public int Compare(PeriodPower pPeriodPower1, PeriodPower pPeriodPower2)
			{
				if (pPeriodPower1.SpectrumPower > pPeriodPower2.SpectrumPower)
				{
					return -1;
				}
				if (pPeriodPower1.SpectrumPower < pPeriodPower2.SpectrumPower)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x02000017 RID: 23
		internal enum SGN
		{
			// Token: 0x0400008E RID: 142
			sgnLT = -1,
			// Token: 0x0400008F RID: 143
			sgnEQ,
			// Token: 0x04000090 RID: 144
			sgnGT,
			// Token: 0x04000091 RID: 145
			sgnNE
		}
	}
}
