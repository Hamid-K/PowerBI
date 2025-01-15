using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing
{
	// Token: 0x02000008 RID: 8
	internal sealed class ForecastDataProcessor
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021D4 File Offset: 0x000003D4
		internal static ForecastErrorType Process(DateTime[] timestamps, double[] value, uint inputLength, DateTime[] correctedTimeStamps, double[] correctedValue, int outputLength, out int sizeAfterCorrection, out ForecastStepData stepData)
		{
			sizeAfterCorrection = 0;
			stepData = null;
			double[] array = new double[inputLength];
			DateTime dateTime = new DateTime(1, 1, 1);
			int num = 0;
			while ((long)num < (long)((ulong)inputLength))
			{
				array[num] = (timestamps[num] - dateTime).TotalDays;
				num++;
			}
			double[] array2 = new double[outputLength];
			ForecastErrorType forecastErrorType = ForecastDataProcessor.Process(array, value, inputLength, array2, correctedValue, outputLength, out sizeAfterCorrection, out stepData);
			if (forecastErrorType != ForecastErrorType.NoError)
			{
				return forecastErrorType;
			}
			for (int i = 0; i < sizeAfterCorrection; i++)
			{
				correctedTimeStamps[i] = dateTime.AddDays(array2[i]);
			}
			return ForecastErrorType.NoError;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000226C File Offset: 0x0000046C
		internal static ForecastErrorType Process(IReadOnlyList<double> timestamps, IReadOnlyList<double> value, uint inputLength, double[] correctedTimeStamps, double[] correctedValue, int outputLength, out int sizeAfterCorrection, out ForecastStepData stepData)
		{
			sizeAfterCorrection = -1;
			ForecastStepDetector forecastStepDetector = new ForecastStepDetector();
			stepData = new ForecastStepData();
			bool flag;
			ForecastErrorType forecastErrorType = forecastStepDetector.HrDetectStepSize(timestamps, inputLength, null, stepData, out flag);
			forecastStepDetector.Destroy();
			if (forecastErrorType != ForecastErrorType.NoError)
			{
				return forecastErrorType;
			}
			ForecastDataProcessor forecastDataProcessor = new ForecastDataProcessor();
			forecastErrorType = forecastDataProcessor.HrInit(timestamps, value, correctedTimeStamps, correctedValue, outputLength, inputLength, stepData, ForecastConstants.CompletionPolicy.cpInterpolateCompletionValue, ForecastConstants.DupAggrPolicy.dapAverage, null);
			if (forecastErrorType != ForecastErrorType.NoError)
			{
				return forecastErrorType;
			}
			bool[] array;
			int num;
			forecastErrorType = forecastDataProcessor.HrCorrectData(out array, out num);
			if (forecastErrorType != ForecastErrorType.NoError)
			{
				return forecastErrorType;
			}
			sizeAfterCorrection = num;
			return forecastDataProcessor.HrValidateData((uint)num);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022EC File Offset: 0x000004EC
		private ForecastErrorType HrInit(IReadOnlyList<double> originalTimeStamps, IReadOnlyList<double> originalValues, double[] rgxnumTimeDest, double[] rgxnumValDest, int cMaxValues, uint cInputSize, ForecastStepData pfsData, ForecastConstants.CompletionPolicy cp, ForecastConstants.DupAggrPolicy dap, int? pxnumUsedLimitIdx)
		{
			if (2L > (long)cMaxValues)
			{
				return ForecastErrorType.DestinationArrayTooSmall;
			}
			this.m_rgxnumTimeOrig = originalTimeStamps;
			this.m_rgxnumValOrig = originalValues;
			this.m_cInputSize = cInputSize;
			this.m_pxnumTimeLimitIdx = this.m_cInputSize - 1U;
			this.m_rgxnumTimeDest = rgxnumTimeDest;
			this.m_rgxnumValDest = rgxnumValDest;
			this.m_rgfOriginalData = null;
			this.m_cMaxValues = cMaxValues;
			this.m_pfsData = pfsData;
			this.m_pxnumTimeStep = this.m_pfsData.PxnumGetNumericStep();
			this.m_cp = cp;
			this.m_dap = dap;
			this.m_fCountingAggregation = this.m_dap == ForecastConstants.DupAggrPolicy.dapCount || this.m_dap == ForecastConstants.DupAggrPolicy.dapCountA;
			this.m_xnumTolerance = 10.0;
			this.m_xnumTolerance = this.m_pxnumTimeStep / this.m_xnumTolerance;
			this.m_xnumHalfTimeStep = this.m_pxnumTimeStep / 2.0;
			this.m_iIndex = this.m_cMaxValues - 1;
			this.m_pxnumUsedLimitIdx = pxnumUsedLimitIdx;
			int? pxnumUsedLimitIdx2 = this.m_pxnumUsedLimitIdx;
			int num = 0;
			if ((pxnumUsedLimitIdx2.GetValueOrDefault() >= num) & (pxnumUsedLimitIdx2 != null))
			{
				this.m_fExposeNonOriginals = true;
			}
			return ForecastErrorType.NoError;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002400 File Offset: 0x00000600
		private ForecastErrorType HrCorrectData(out bool[] prgfOriginalData, out int pcFinalSize)
		{
			prgfOriginalData = null;
			pcFinalSize = -1;
			double xnumNextCalculatedTimeDest;
			double xnumNextCalculatedValDest;
			uint pxnumOrigNextLastAggrTimeIdx;
			ForecastErrorType forecastErrorType = this.HrFindStartingPointAndValue(out xnumNextCalculatedTimeDest, out this.m_pxnumNextExposedTimeline, out xnumNextCalculatedValDest, out pxnumOrigNextLastAggrTimeIdx);
			if (forecastErrorType != ForecastErrorType.NoError)
			{
				prgfOriginalData = this.m_rgfOriginalData;
				return forecastErrorType;
			}
			forecastErrorType = this.HrInsertRow(this.m_pxnumNextExposedTimeline, xnumNextCalculatedValDest, true);
			if (forecastErrorType != ForecastErrorType.NoError)
			{
				prgfOriginalData = this.m_rgfOriginalData;
				return forecastErrorType;
			}
			while (pxnumOrigNextLastAggrTimeIdx > 0U)
			{
				forecastErrorType = this.HrCalcNextExistingStepAggregation(pxnumOrigNextLastAggrTimeIdx, xnumNextCalculatedTimeDest);
				if (forecastErrorType != ForecastErrorType.NoError)
				{
					return forecastErrorType;
				}
				if (this.m_nFoundDistance < 1)
				{
					prgfOriginalData = this.m_rgfOriginalData;
					return ForecastErrorType.NextTimePointDistanceTooSmall;
				}
				double num = 0.0;
				if (this.m_nFoundDistance > 1)
				{
					double num2 = xnumNextCalculatedTimeDest;
					if (!this.m_pfsData.FIsBusinessStep())
					{
						double num3 = xnumNextCalculatedTimeDest - this.m_xnumNextCalculatedTimeDest;
						while (this.m_nFoundDistance > 1)
						{
							num2 -= this.m_pxnumTimeStep;
							if (this.m_cp == ForecastConstants.CompletionPolicy.cpInterpolateCompletionValue)
							{
								ForecastMathUtils.InterpolateValue(num3, this.m_xnumNextCalculatedTimeDest, this.m_xnumNextCalculatedValDest, xnumNextCalculatedValDest, num2, out num);
							}
							forecastErrorType = this.HrInsertRow(num2, num, false);
							if (forecastErrorType != ForecastErrorType.NoError)
							{
								prgfOriginalData = this.m_rgfOriginalData;
								return forecastErrorType;
							}
							this.m_nFoundDistance--;
						}
					}
					else
					{
						double num3 = (double)this.m_nFoundDistance;
						forecastErrorType = this.m_pfsData.HrInitBusinessStepTimepointByShift(num2);
						if (forecastErrorType != ForecastErrorType.NoError)
						{
							prgfOriginalData = this.m_rgfOriginalData;
							return forecastErrorType;
						}
						while (this.m_nFoundDistance > 1)
						{
							this.m_nFoundDistance--;
							this.m_pfsData.GetBusinessStepTimepointSingleShift(false, out num2);
							if (this.m_cp == ForecastConstants.CompletionPolicy.cpInterpolateCompletionValue)
							{
								double num4 = (double)this.m_nFoundDistance;
								ForecastMathUtils.InterpolateValue(num3, 0.0, this.m_xnumNextCalculatedValDest, xnumNextCalculatedValDest, num4, out num);
							}
							forecastErrorType = this.HrInsertRow(num2, num, false);
							if (forecastErrorType != ForecastErrorType.NoError)
							{
								prgfOriginalData = this.m_rgfOriginalData;
								return forecastErrorType;
							}
						}
					}
				}
				forecastErrorType = this.HrInsertRow(this.m_pxnumNextExposedTimeline, this.m_xnumNextCalculatedValDest, true);
				pxnumOrigNextLastAggrTimeIdx = this.m_pxnumOrigNextLastAggrTimeIdx;
				xnumNextCalculatedTimeDest = this.m_xnumNextCalculatedTimeDest;
				xnumNextCalculatedValDest = this.m_xnumNextCalculatedValDest;
			}
			ForecastDataProcessor.BltBBuf<double>(this.m_rgxnumTimeDest, (uint)(this.m_iIndex + 1), this.m_cInserted, 0U);
			ForecastDataProcessor.BltBBuf<double>(this.m_rgxnumValDest, (uint)(this.m_iIndex + 1), this.m_cInserted, 0U);
			if (this.m_rgfOriginalData != null)
			{
				ForecastDataProcessor.BltBBuf<bool>(this.m_rgfOriginalData, (uint)(this.m_iIndex + 1), this.m_cInserted, 0U);
			}
			pcFinalSize = this.m_cInserted;
			prgfOriginalData = this.m_rgfOriginalData;
			return forecastErrorType;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002640 File Offset: 0x00000840
		private static void BltBBuf<T>(T[] array, uint sourceIdx, int cnt, uint destIdx)
		{
			if (sourceIdx == destIdx)
			{
				return;
			}
			if (sourceIdx < destIdx)
			{
				for (int i = cnt - 1; i >= 0; i--)
				{
					checked
					{
						array[(int)((IntPtr)(unchecked((ulong)destIdx + (ulong)((long)i))))] = array[(int)((IntPtr)(unchecked((ulong)sourceIdx + (ulong)((long)i))))];
					}
				}
				return;
			}
			for (int j = 0; j < cnt; j++)
			{
				checked
				{
					array[(int)((IntPtr)(unchecked((ulong)destIdx + (ulong)((long)j))))] = array[(int)((IntPtr)(unchecked((ulong)sourceIdx + (ulong)((long)j))))];
				}
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000026A1 File Offset: 0x000008A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int NGetNumberOfNonOriginalPoints()
		{
			return this.m_cNonOriginals;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000026A9 File Offset: 0x000008A9
		private static uint CMaxCorrectedSize(uint cInputSize)
		{
			if (30 < 100)
			{
				return cInputSize * 100U / 70U;
			}
			return cInputSize * 100U;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026C0 File Offset: 0x000008C0
		private static uint CMaxCorrectedSizeByValues(uint cInputSize, double pxnumMinValue, double pxnumMaxValue, double pxnumStep, bool ensureNoOverflow)
		{
			uint num = ForecastDataProcessor.CMaxCorrectedSize(cInputSize);
			double num2 = 1000000.0;
			double num3 = (pxnumMaxValue - pxnumMinValue) / pxnumStep;
			uint num4;
			if (Math.Ceiling(num3) > num2)
			{
				num4 = Math.Max(1000000U, num);
			}
			else
			{
				num4 = (uint)(Math.Floor(num3) + 1.0);
			}
			num4 += Math.Max(num4 / 5U, 10U);
			if (ensureNoOverflow)
			{
				return num4;
			}
			if (num4 < 1000000U && num > num4)
			{
				num = num4;
			}
			return num;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002730 File Offset: 0x00000930
		private ForecastErrorType HrValidateData(uint cCheckedSize)
		{
			bool flag = false;
			if (this.m_cInserted <= 0 || (ulong)cCheckedSize > (ulong)((long)this.m_cInserted))
			{
				return ForecastErrorType.Unexpected;
			}
			if (this.m_cNonOriginals == 0)
			{
				return ForecastErrorType.NoError;
			}
			if (cCheckedSize == 0U || cCheckedSize == (uint)this.m_cInserted)
			{
				if (this.m_cInserted * 30 < this.m_cNonOriginals * 100)
				{
					flag = true;
				}
			}
			else
			{
				int num = 0;
				int num2 = this.m_cInserted - 1;
				while ((long)num2 > (long)((ulong)cCheckedSize))
				{
					if (!this.m_rgfOriginalData[num2])
					{
						num++;
					}
					num2--;
				}
				if ((ulong)(cCheckedSize * 30U) < (ulong)((long)((this.m_cNonOriginals - num) * 100)))
				{
					flag = true;
				}
			}
			if (flag)
			{
				return ForecastErrorType.TooManyMissingValues;
			}
			return ForecastErrorType.NoError;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000027C4 File Offset: 0x000009C4
		private static bool FOriginalExposed()
		{
			return false;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027C8 File Offset: 0x000009C8
		private ForecastErrorType HrAllocAndInitOriginals()
		{
			if (!this.m_fExposeNonOriginals)
			{
				return ForecastErrorType.Unexpected;
			}
			this.m_rgfOriginalData = new bool[this.m_cMaxValues];
			for (int i = 0; i < this.m_cMaxValues; i++)
			{
				this.m_rgfOriginalData[i] = true;
			}
			return ForecastErrorType.NoError;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000280C File Offset: 0x00000A0C
		private ForecastErrorType HrFindStartingPointAndValue(out double pxnumDestTimeline, out double pxnumNextExposedTimeline, out double pxnumDestValue, out uint ppxnumOrigLastAggrTimeIdx)
		{
			pxnumDestValue = 0.0;
			ppxnumOrigLastAggrTimeIdx = 0U;
			int num = 0;
			uint num2 = 0U;
			DataProcessorAnchorLocation dataProcessorAnchorLocation = new DataProcessorAnchorLocation(this.m_rgxnumTimeOrig[0], this.m_rgxnumTimeOrig[(int)this.m_pxnumTimeLimitIdx]);
			pxnumDestTimeline = -1.0;
			pxnumNextExposedTimeline = -1.0;
			int i;
			if (this.m_pxnumUsedLimitIdx != null && this.m_pxnumUsedLimitIdx != null)
			{
				i = (int)this.m_pxnumTimeLimitIdx;
				while (i > 0 && this.m_rgxnumTimeOrig[i] > this.m_rgxnumTimeOrig[this.m_pxnumUsedLimitIdx.Value])
				{
					i--;
				}
				i = this.PxnumLastDupBelow(i, this.m_rgxnumTimeOrig, true);
			}
			else
			{
				i = this.PxnumLastDupBelow((int)this.m_pxnumTimeLimitIdx, this.m_rgxnumTimeOrig, true);
			}
			int num3;
			while (i > 0)
			{
				num3 = this.PxnumLastDupBelow(i - 1, this.m_rgxnumTimeOrig, false);
				if (Math.Abs(this.m_rgxnumTimeOrig[i] - this.m_rgxnumTimeOrig[num3] - this.m_pxnumTimeStep) <= this.m_xnumTolerance)
				{
					num2 += 1U;
					if (num2 == 1U)
					{
						num = i;
					}
					else if (num2 >= 20U)
					{
						dataProcessorAnchorLocation.StoreAnchorCandidate(num2, (uint)num, this.m_rgxnumTimeOrig[num]);
						num = 0;
						break;
					}
				}
				else
				{
					if (num2 >= 2U && dataProcessorAnchorLocation.ShouldStore(num2))
					{
						dataProcessorAnchorLocation.StoreAnchorCandidate(num2, (uint)num, this.m_rgxnumTimeOrig[num]);
						num = 0;
					}
					num2 = 0U;
				}
				i = num3;
			}
			if (num2 >= 2U && dataProcessorAnchorLocation.ShouldStore(num2))
			{
				dataProcessorAnchorLocation.StoreAnchorCandidate(num2, (uint)num, this.m_rgxnumTimeOrig[num]);
			}
			else if (num2 == 1U && dataProcessorAnchorLocation.IsEmpty())
			{
				dataProcessorAnchorLocation.StoreAnchorCandidate(2U, (uint)num, this.m_rgxnumTimeOrig[num]);
			}
			if (dataProcessorAnchorLocation.IsEmpty())
			{
				return ForecastErrorType.AnchorSequenceNotFound;
			}
			i = dataProcessorAnchorLocation.PxnumGetBestAnchorTimelineHighEnd();
			if (i == -1)
			{
				return ForecastErrorType.AnchorSequenceNotFound;
			}
			num3 = this.PxnumLastDupBelow((int)this.m_pxnumTimeLimitIdx, this.m_rgxnumTimeOrig, false);
			if (i == num3)
			{
				pxnumDestTimeline = this.m_rgxnumTimeOrig[(int)this.m_pxnumTimeLimitIdx];
				pxnumNextExposedTimeline = pxnumDestTimeline;
			}
			else
			{
				int num4 = i;
				if (this.m_pfsData.FIsBusinessStep())
				{
					int num5;
					ForecastErrorType forecastErrorType = this.m_pfsData.HrGetBusinessStepCalculatedStep(this.m_rgxnumTimeOrig[i], this.m_rgxnumTimeOrig[num3], out pxnumDestTimeline, out num5);
					if (forecastErrorType != ForecastErrorType.NoError)
					{
						return forecastErrorType;
					}
				}
				else
				{
					pxnumDestTimeline = this.m_rgxnumTimeOrig[num4] + this.m_pxnumTimeStep * Math.Round((this.m_rgxnumTimeOrig[(int)this.m_pxnumTimeLimitIdx] - this.m_rgxnumTimeOrig[num4]) / this.m_pxnumTimeStep);
				}
				double num6 = double.MaxValue;
				i = this.PxnumLastDupBelow((int)this.m_pxnumTimeLimitIdx, this.m_rgxnumTimeOrig, false);
				int num7 = i;
				while (i >= num4)
				{
					double num8 = Math.Abs(pxnumDestTimeline - this.m_rgxnumTimeOrig[i]);
					if (num8 >= num6)
					{
						break;
					}
					num7 = i;
					num6 = num8;
					i = this.PxnumLastDupBelow(i - 1, this.m_rgxnumTimeOrig, false);
				}
				if (num6 <= this.m_xnumTolerance)
				{
					pxnumDestTimeline = this.m_rgxnumTimeOrig[num7];
					pxnumNextExposedTimeline = pxnumDestTimeline;
				}
				else
				{
					pxnumNextExposedTimeline = this.m_rgxnumTimeOrig[num7];
				}
			}
			double num9 = pxnumDestTimeline - this.m_xnumHalfTimeStep;
			return this.HrAggregateRange(num9, this.m_pxnumTimeLimitIdx, out pxnumDestValue, out ppxnumOrigLastAggrTimeIdx);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002B44 File Offset: 0x00000D44
		private ForecastErrorType HrInsertRow(double pxnumTime, double pxnumValue, bool fOriginal)
		{
			if (this.m_iIndex > this.m_cMaxValues || this.m_iIndex < 0)
			{
				return ForecastErrorType.TooManyMissingValues;
			}
			this.m_rgxnumTimeDest[this.m_iIndex] = pxnumTime;
			this.m_rgxnumValDest[this.m_iIndex] = pxnumValue;
			if (!fOriginal)
			{
				if (this.m_fExposeNonOriginals)
				{
					if (this.m_rgfOriginalData == null)
					{
						ForecastErrorType forecastErrorType = this.HrAllocAndInitOriginals();
						if (forecastErrorType != ForecastErrorType.NoError)
						{
							return forecastErrorType;
						}
					}
					this.m_rgfOriginalData[this.m_iIndex] = false;
				}
				this.m_cNonOriginals++;
			}
			this.m_cInserted++;
			this.m_iIndex--;
			return ForecastErrorType.NoError;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002BE0 File Offset: 0x00000DE0
		private ForecastErrorType HrAggregateRange(double pxnumTimeRangeLow, uint pxnumTimeRangeHighIdx, out double pxnumValue, out uint ppxnumRealLowIdx)
		{
			int num = 1;
			pxnumValue = -1.0;
			ppxnumRealLowIdx = 0U;
			if (this.PrevInRange(pxnumTimeRangeLow, pxnumTimeRangeHighIdx))
			{
				switch (this.m_dap)
				{
				case ForecastConstants.DupAggrPolicy.dapNoDuplicated:
					return ForecastErrorType.NoAggregationAllowed;
				case ForecastConstants.DupAggrPolicy.dapAverage:
				case ForecastConstants.DupAggrPolicy.dapSum:
				{
					double num2 = this.m_rgxnumValOrig[(int)pxnumTimeRangeHighIdx];
					double num3 = num2;
					while (this.PrevInRange(pxnumTimeRangeLow, pxnumTimeRangeHighIdx))
					{
						pxnumTimeRangeHighIdx -= 1U;
						num2 = this.m_rgxnumValOrig[(int)pxnumTimeRangeHighIdx];
						num3 += num2;
						num++;
					}
					if (this.m_dap == ForecastConstants.DupAggrPolicy.dapAverage)
					{
						double num4 = (double)num;
						pxnumValue = num3 / num4;
						goto IL_013C;
					}
					pxnumValue = num3;
					goto IL_013C;
				}
				case ForecastConstants.DupAggrPolicy.dapCount:
				case ForecastConstants.DupAggrPolicy.dapCountA:
					while (this.PrevInRange(pxnumTimeRangeLow, pxnumTimeRangeHighIdx))
					{
						pxnumTimeRangeHighIdx -= 1U;
						num++;
					}
					pxnumValue = (double)num;
					goto IL_013C;
				case ForecastConstants.DupAggrPolicy.dapMax:
				case ForecastConstants.DupAggrPolicy.dapMin:
				{
					ForecastStepData.SGN sgn = ((this.m_dap == ForecastConstants.DupAggrPolicy.dapMin) ? ForecastStepData.SGN.sgnGT : ForecastStepData.SGN.sgnLT);
					double num2 = this.m_rgxnumValOrig[(int)pxnumTimeRangeHighIdx];
					pxnumValue = num2;
					while (this.PrevInRange(pxnumTimeRangeLow, pxnumTimeRangeHighIdx))
					{
						pxnumTimeRangeHighIdx -= 1U;
						num2 -= 1.0;
						if (ForecastDataProcessor.SgnCmpXnum(pxnumValue, num2) == sgn)
						{
							pxnumValue = num2;
						}
					}
					goto IL_013C;
				}
				}
				return ForecastErrorType.DupAggrPolicyNotImplemented;
			}
			pxnumValue = (this.m_fCountingAggregation ? 1.0 : this.m_rgxnumValOrig[(int)pxnumTimeRangeHighIdx]);
			IL_013C:
			ppxnumRealLowIdx = pxnumTimeRangeHighIdx;
			return ForecastErrorType.NoError;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002D2E File Offset: 0x00000F2E
		private static ForecastStepData.SGN SgnCmpXnum(double a, double b)
		{
			if (a == b)
			{
				return ForecastStepData.SGN.sgnEQ;
			}
			if (a > b)
			{
				return ForecastStepData.SGN.sgnGT;
			}
			if (a < b)
			{
				return ForecastStepData.SGN.sgnLT;
			}
			return ForecastStepData.SGN.sgnNE;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002D44 File Offset: 0x00000F44
		private ForecastErrorType HrCalcNextExistingStepAggregation(uint pxnumTimeOrigLastAggregatedIdx, double pxnumLastTimeDest)
		{
			int i;
			double num2;
			if (this.m_pfsData.FIsBusinessStep())
			{
				i = this.PxnumLastDupBelow((int)(pxnumTimeOrigLastAggregatedIdx - 1U), this.m_rgxnumTimeOrig, false);
				ForecastErrorType forecastErrorType = this.m_pfsData.HrGetBusinessStepCalculatedStep(pxnumLastTimeDest, this.m_rgxnumTimeOrig[i], out this.m_xnumNextCalculatedTimeDest, out this.m_nFoundDistance);
				this.m_pxnumNextExposedTimeline = this.m_xnumNextCalculatedTimeDest;
				if (forecastErrorType != ForecastErrorType.NoError)
				{
					return forecastErrorType;
				}
				if (this.m_nFoundDistance >= 0)
				{
					return ForecastErrorType.FoundDistanceIsNotNegative;
				}
				this.m_nFoundDistance = -this.m_nFoundDistance;
			}
			else
			{
				double num = pxnumLastTimeDest - this.m_pxnumTimeStep;
				num2 = num - this.m_xnumHalfTimeStep;
				i = this.PxnumLastDupBelow((int)(pxnumTimeOrigLastAggregatedIdx - 1U), this.m_rgxnumTimeOrig, false);
				if (ForecastDataProcessor.SgnCmpXnum(num2, this.m_rgxnumTimeOrig[i]) != ForecastStepData.SGN.sgnGT)
				{
					this.m_nFoundDistance = 1;
					this.m_xnumNextCalculatedTimeDest = num;
					this.m_pxnumNextExposedTimeline = this.m_xnumNextCalculatedTimeDest;
				}
				else
				{
					this.m_nFoundDistance = (int)Math.Round((pxnumLastTimeDest - this.m_rgxnumTimeOrig[i]) / this.m_pxnumTimeStep);
					if (this.m_nFoundDistance <= 1)
					{
						return ForecastErrorType.NextStepTooClose;
					}
					this.m_xnumNextCalculatedTimeDest = pxnumLastTimeDest - (double)this.m_nFoundDistance * this.m_pxnumTimeStep;
					this.m_pxnumNextExposedTimeline = this.m_xnumNextCalculatedTimeDest;
				}
			}
			uint num3 = (uint)i;
			double num4 = double.MaxValue;
			while (i >= 0)
			{
				double num5 = Math.Abs(this.m_xnumNextCalculatedTimeDest - this.m_rgxnumTimeOrig[i]);
				if (num5 >= num4)
				{
					break;
				}
				num3 = (uint)i;
				num4 = num5;
				i = this.PxnumLastDupBelow(i - 1, this.m_rgxnumTimeOrig, false);
			}
			if (ForecastDataProcessor.SgnCmpXnum(num4, this.m_xnumTolerance) != ForecastStepData.SGN.sgnGT)
			{
				this.m_xnumNextCalculatedTimeDest = this.m_rgxnumTimeOrig[(int)num3];
				this.m_pxnumNextExposedTimeline = this.m_xnumNextCalculatedTimeDest;
			}
			else
			{
				this.m_pxnumNextExposedTimeline = this.m_rgxnumTimeOrig[(int)num3];
			}
			num2 = this.m_xnumNextCalculatedTimeDest - this.m_xnumHalfTimeStep;
			return this.HrAggregateRange(num2, pxnumTimeOrigLastAggregatedIdx - 1U, out this.m_xnumNextCalculatedValDest, out this.m_pxnumOrigNextLastAggrTimeIdx);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002F18 File Offset: 0x00001118
		private bool PrevInRange(double pxnumTimeRangeLow, uint pxnumTimeRangeCurIdx)
		{
			return pxnumTimeRangeCurIdx > 0U && pxnumTimeRangeLow <= this.m_rgxnumTimeOrig[(int)(pxnumTimeRangeCurIdx - 1U)];
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002F34 File Offset: 0x00001134
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int PxnumLastDupBelow(int pxnumTimelineCurIdx, IReadOnlyList<double> timeLine, bool fAboveBase)
		{
			if (!fAboveBase)
			{
				while (pxnumTimelineCurIdx > 0)
				{
					if (timeLine[pxnumTimelineCurIdx] != timeLine[pxnumTimelineCurIdx - 1])
					{
						break;
					}
					pxnumTimelineCurIdx--;
				}
			}
			else
			{
				while (pxnumTimelineCurIdx > 0 && timeLine[pxnumTimelineCurIdx - 1] != this.m_rgxnumTimeOrig[0] && timeLine[pxnumTimelineCurIdx] == timeLine[pxnumTimelineCurIdx - 1])
				{
					pxnumTimelineCurIdx--;
				}
			}
			return pxnumTimelineCurIdx;
		}

		// Token: 0x04000052 RID: 82
		internal const int c_nMissingValuesPercentile = 30;

		// Token: 0x04000053 RID: 83
		internal const uint c_cMinNumOfValidStepSeries = 2U;

		// Token: 0x04000054 RID: 84
		internal const uint c_cMaxNumOfValidStepSeries = 20U;

		// Token: 0x04000055 RID: 85
		internal const bool c_fCountAlwaysFillMissingWithZero = true;

		// Token: 0x04000056 RID: 86
		internal const bool c_fExposeNonOriginals = false;

		// Token: 0x04000057 RID: 87
		internal const uint c_cMaxCorrectedDataAllocationByValues = 1000000U;

		// Token: 0x04000058 RID: 88
		private IReadOnlyList<double> m_rgxnumTimeOrig;

		// Token: 0x04000059 RID: 89
		private IReadOnlyList<double> m_rgxnumValOrig;

		// Token: 0x0400005A RID: 90
		private uint m_pxnumTimeLimitIdx;

		// Token: 0x0400005B RID: 91
		private uint m_cInputSize;

		// Token: 0x0400005C RID: 92
		private int? m_pxnumUsedLimitIdx;

		// Token: 0x0400005D RID: 93
		private double[] m_rgxnumTimeDest;

		// Token: 0x0400005E RID: 94
		private double[] m_rgxnumValDest;

		// Token: 0x0400005F RID: 95
		private bool[] m_rgfOriginalData;

		// Token: 0x04000060 RID: 96
		private int m_cMaxValues;

		// Token: 0x04000061 RID: 97
		private ForecastStepData m_pfsData;

		// Token: 0x04000062 RID: 98
		private double m_pxnumTimeStep;

		// Token: 0x04000063 RID: 99
		private double m_xnumHalfTimeStep;

		// Token: 0x04000064 RID: 100
		private ForecastConstants.CompletionPolicy m_cp;

		// Token: 0x04000065 RID: 101
		private ForecastConstants.DupAggrPolicy m_dap = ForecastConstants.DupAggrPolicy.dapAverage;

		// Token: 0x04000066 RID: 102
		private bool m_fCountingAggregation;

		// Token: 0x04000067 RID: 103
		private double m_xnumTolerance;

		// Token: 0x04000068 RID: 104
		private int m_iIndex;

		// Token: 0x04000069 RID: 105
		private int m_cInserted;

		// Token: 0x0400006A RID: 106
		private int m_cNonOriginals;

		// Token: 0x0400006B RID: 107
		private bool m_fExposeNonOriginals;

		// Token: 0x0400006C RID: 108
		private uint m_pxnumOrigNextLastAggrTimeIdx;

		// Token: 0x0400006D RID: 109
		private double m_xnumNextCalculatedTimeDest;

		// Token: 0x0400006E RID: 110
		private double m_xnumNextCalculatedValDest;

		// Token: 0x0400006F RID: 111
		private double m_pxnumNextExposedTimeline;

		// Token: 0x04000070 RID: 112
		private int m_nFoundDistance;
	}
}
