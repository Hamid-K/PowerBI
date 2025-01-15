using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Analytics.ExponentialSmoothing
{
	// Token: 0x02000006 RID: 6
	public class ForecastingModel
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002135 File Offset: 0x00000335
		public ForecastStats Pfstats()
		{
			return this.m_stats;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002140 File Offset: 0x00000340
		public ForecastingModel()
		{
			this.m_rgxnumData = null;
			this.m_cData = 0U;
			this.m_rgxnumActualData = null;
			this.m_cActualData = 0U;
			this.m_xnumActualDataCount = 0.0;
			this.m_rgfOriginalData = null;
			this.m_rgxnumState = null;
			this.m_nSeasonality = 0U;
			this.m_xnumSeasonality = 0.0;
			this.m_xnumVariance = 0.0;
			this.m_cTrainWindow = 0U;
			this.m_cValidationWindow = 0U;
			this.m_cOriginalTrainWindow = 0U;
			this.m_cOriginalValidationWindow = 0U;
			this.m_xnumGradientDamper = 0.1;
			this.m_stats = null;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021E4 File Offset: 0x000003E4
		public void HrTrain(double[] rgxnumData, int cData, bool[] rgfOriginalData, uint season)
		{
			int num = 0;
			double[] array = null;
			bool flag = season > 1U;
			if (this.m_rgxnumState != null)
			{
				throw new ArgumentException("ETSForecastingModel is already trained");
			}
			if (rgxnumData == null || cData < 1)
			{
				throw new ArgumentException("rgxnumData == null|| cData < 1");
			}
			bool flag2 = true;
			for (int i = 0; i < rgxnumData.Length; i++)
			{
				if (rgxnumData[i] <= 0.0)
				{
					flag2 = false;
					break;
				}
			}
			this.m_rgxnumData = rgxnumData;
			this.m_cData = (uint)cData;
			this.m_cActualData = (uint)cData;
			this.m_xnumActualDataCount = this.m_cActualData;
			this.m_rgfOriginalData = rgfOriginalData;
			this.m_nSeasonality = Math.Max(2U, season);
			uint num2 = this.m_nSeasonality + 2U;
			this.m_xnumSeasonality = this.m_nSeasonality;
			this.CreateCompletedData(out array, out num, flag);
			this.m_rgxnumActualData = ((array == null) ? this.m_rgxnumData : array);
			this.ComputeWindows((int)this.m_cActualData, out this.m_cTrainWindow, out this.m_cValidationWindow);
			this.ComputeWindows((int)this.m_cData, out this.m_cOriginalTrainWindow, out this.m_cOriginalValidationWindow);
			ForecastStats forecastStats = null;
			for (int j = 0; j < 2; j++)
			{
				bool flag3 = j == 1;
				if (flag2 || !flag3)
				{
					this.m_rgxnumState = new double[num2];
					ForecastStats forecastStats2 = this.FindOptimalParameters(season, num2, flag3);
					if (j == 0)
					{
						forecastStats = forecastStats2;
					}
					else
					{
						double num3 = Math.Log(forecastStats2.GetAICCorrectedLoss()) + 2.0 * forecastStats2.LikelihoodCorrection;
						double num4 = Math.Log(forecastStats.GetAICCorrectedLoss());
						if (num3 < num4)
						{
							if (ForecastingModel.DetermineIfMultModelCanBeUsed(num4, num3, forecastStats2.Loss / this.m_cActualData / this.m_xnumGradientDamper, (int)this.m_nSeasonality, forecastStats2, forecastStats2.FinalState, this.m_rgxnumActualData, (int)this.m_cActualData))
							{
								forecastStats = forecastStats2;
							}
							else
							{
								forecastStats.BetterModelExistsButConfidenceBandTooWide = true;
							}
						}
					}
				}
			}
			this.m_stats = forecastStats;
			this.PropagateStateVector(forecastStats);
			ForecastingModel.NormalizeStateVector(this.m_rgxnumState, forecastStats.SeasonIsMultiplicative);
			this.m_rgxnumData = null;
			this.m_cData = 0U;
			this.m_rgxnumActualData = null;
			this.m_cActualData = 0U;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023F2 File Offset: 0x000005F2
		public double Forecast(int nOffset)
		{
			Contract.CheckValue<double[]>(this.m_rgxnumState, "state vector m_rgxnumState");
			Contract.CheckValue<ForecastStats>(this.m_stats, "parameter object m_stats");
			return ForecastingModel.ComputeForecast(nOffset, this.m_rgxnumState, this.m_nSeasonality, this.m_stats.SeasonIsMultiplicative);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002434 File Offset: 0x00000634
		public static Tuple<double, int> ComputeKStepForecastTrainingError(IReadOnlyList<double> data, ForecastStats parameters, int k, int dataLength)
		{
			IReadOnlyList<double> initialState = parameters.InitialState;
			double[] array = new double[initialState.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = initialState[i];
			}
			double num = 0.0;
			uint num2 = (uint)(array.Length - 1);
			int num3 = 0;
			for (int j = 0; j < dataLength - k + 1; j++)
			{
				double num4 = ForecastingModel.ComputeForecast(k, array, parameters.Season, parameters.SeasonIsMultiplicative);
				double num5 = data[j + k - 1];
				double num6 = num4 - num5;
				double num7;
				if (parameters.ErrorIsMultiplicative)
				{
					if (Math.Abs(num4) > 5E-324)
					{
						num7 = num5 / num4 - 1.0;
					}
					else
					{
						num7 = double.MaxValue;
					}
				}
				else
				{
					num7 = num5 - num4;
				}
				num += num6 * num6;
				num3++;
				ForecastingModel.UpdateStateVectorByOneStep(num7, parameters.Alpha, parameters.Beta, parameters.Gamma, ref num2, parameters.ErrorIsMultiplicative, parameters.SeasonIsMultiplicative, array, (uint)array.Length);
			}
			return new Tuple<double, int>(num, num3);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002554 File Offset: 0x00000754
		public static double[] GetConfidenceValues(int forecastSize, ForecastingModel model)
		{
			ForecastStats forecastStats = model.Pfstats();
			double[] array;
			if (forecastStats.ErrorIsMultiplicative)
			{
				array = ForecastingModel.ComputeVarianceForOffsetForMultErrorModel(forecastSize, forecastStats.Season, model.m_xnumVariance, forecastStats.Alpha, forecastStats.Beta, forecastStats.Gamma, model.m_rgxnumState, forecastStats.HasTrend, forecastStats.SeasonIsMultiplicative);
			}
			else
			{
				array = new double[forecastSize];
				for (int i = 0; i < forecastSize; i++)
				{
					double num = model.ComputeVarianceForAdditiveErrorModel(i + 1);
					array[i] = num;
				}
			}
			for (int j = 0; j < forecastSize; j++)
			{
				array[j] = Math.Sqrt(array[j]);
			}
			return array;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025EC File Offset: 0x000007EC
		private double ComputeVarianceForAdditiveErrorModel(int nOffset)
		{
			if (this.m_rgxnumState == null || this.m_nSeasonality < 2U)
			{
				throw new InvalidOperationException("m_rgxnumState == null || m_nSeasonality < 2");
			}
			return ForecastingModel.ComputeVarianceForOffsetForAdditiveErrorModel(nOffset, this.m_nSeasonality, this.m_xnumVariance, this.m_stats.Alpha, this.m_stats.Beta, this.m_stats.Gamma);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002648 File Offset: 0x00000848
		private void CreateCompletedData(out double[] prgxnumCompletedData, out int pcPointsAdded, bool fSeasonal)
		{
			int num = 0;
			double num2 = this.m_cData;
			if (this.m_cData * 2U > this.m_nSeasonality * 7U)
			{
				prgxnumCompletedData = null;
				pcPointsAdded = 0;
				return;
			}
			int num3 = (int)Math.Ceiling((this.m_nSeasonality * 7U - this.m_cData * 2U) / 2.0);
			pcPointsAdded = num3;
			prgxnumCompletedData = new double[(long)num3 + (long)((ulong)this.m_cData)];
			int num4 = 0;
			while ((long)num4 < (long)((ulong)this.m_cData))
			{
				prgxnumCompletedData[num4] = this.m_rgxnumData[num4];
				num4++;
			}
			double num6;
			if (this.m_cData < this.m_nSeasonality)
			{
				double num5 = prgxnumCompletedData[(int)(this.m_cData - 1U)];
				num = (int)(this.m_nSeasonality - this.m_cData);
				for (int i = 0; i < num; i++)
				{
					checked
					{
						prgxnumCompletedData[(int)((IntPtr)(unchecked((long)i + (long)((ulong)this.m_cData))))] = num5;
					}
				}
				num3 -= num;
				num6 = 0.0;
			}
			else if (!fSeasonal)
			{
				num6 = this.CalculateSlope(prgxnumCompletedData, (int)((ulong)this.m_cData + (ulong)((long)num)), null, false, 0);
			}
			else
			{
				double[] array = new double[4];
				double num7 = 0.0;
				for (uint num8 = 0U; num8 < this.m_nSeasonality; num8 += 1U)
				{
					uint num9 = 0U;
					while (num8 + num9 * this.m_nSeasonality < this.m_cData && num9 < 4U)
					{
						array[(int)num9] = this.m_rgxnumData[(int)(num8 + num9 * this.m_nSeasonality)];
						num9 += 1U;
					}
					num6 = this.CalculateSlope(array, (int)num9, null, false, 0);
					num7 += num6;
				}
				num6 = num7 / this.m_xnumSeasonality / this.m_xnumSeasonality;
			}
			for (int j = 0; j < num3; j++)
			{
				int num10 = (int)(((ulong)this.m_cData + (ulong)((long)j)) % (ulong)this.m_nSeasonality);
				int num11 = (int)((long)(j + num) + (long)((ulong)this.m_cData));
				double num12 = (double)num10;
				prgxnumCompletedData[num11] = prgxnumCompletedData[num10] + num6 * (num2 - num12);
				num2 += 1.0;
			}
			this.m_cActualData = (uint)((long)pcPointsAdded + (long)((ulong)this.m_cData));
			this.m_xnumActualDataCount = this.m_cActualData;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002860 File Offset: 0x00000A60
		private double CalculateSlope(double[] rgxData, int cPoints, double[] rgxnumX0, bool isMultiplicative, int startIdx = 0)
		{
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			double num4 = (double)cPoints;
			if (cPoints == 1)
			{
				return 0.0;
			}
			for (int i = startIdx; i < startIdx + cPoints; i++)
			{
				checked
				{
					if (rgxnumX0 != null)
					{
						if (!isMultiplicative)
						{
							unchecked
							{
								num3 = rgxData[i] - rgxnumX0[(int)(checked((IntPtr)(unchecked((ulong)(1U + this.m_nSeasonality) - (ulong)((long)i % (long)((ulong)this.m_nSeasonality))))))];
							}
						}
						else if (Math.Abs(rgxnumX0[(int)((IntPtr)(unchecked((ulong)(1U + this.m_nSeasonality) - (ulong)((long)i % (long)((ulong)this.m_nSeasonality)))))]) > 5E-324)
						{
							num3 = rgxData[i] / rgxnumX0[(int)((IntPtr)(unchecked((ulong)(1U + this.m_nSeasonality) - (ulong)((long)i % (long)((ulong)this.m_nSeasonality)))))];
						}
					}
					else
					{
						num3 = rgxData[i];
					}
				}
				num += num3;
				double num5 = (double)(i + 1 - startIdx);
				num2 += num5 * num3;
			}
			double num6 = Math.Round(num4 * (num4 + 1.0) / 2.0);
			double num7 = Math.Round(num4 / 3.0 * (num4 + 1.0) * (num4 + 0.5));
			double num8 = num6 / num4;
			return (num4 * num2 - num6 * num) / (num4 * num7 - num6 * num6);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000029C0 File Offset: 0x00000BC0
		private void ComputeWindows(int cData, out uint pcTrainWindow, out uint pcValidationWindow)
		{
			double num = 0.8;
			double num2 = 0.4;
			double num3 = (double)cData;
			int num4 = (int)Math.Floor(num * num3);
			pcTrainWindow = (uint)((cData > 10) ? num4 : cData);
			pcValidationWindow = (uint)((long)cData - (long)((ulong)pcTrainWindow));
			if (pcValidationWindow > 0U)
			{
				num4 = (int)Math.Floor(num2 * num3);
				pcValidationWindow = Math.Max(Math.Max(pcValidationWindow, 2U * this.m_nSeasonality), 10U);
				pcValidationWindow = (uint)Math.Min((long)((ulong)pcValidationWindow), (long)num4);
				pcTrainWindow = (uint)((long)cData - (long)((ulong)pcValidationWindow));
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002A40 File Offset: 0x00000C40
		private void AddSeasonalityCorrections(double[] rgxnumX0, bool seasonIsMultiplicative)
		{
			int num = (int)(this.m_nSeasonality * 2U);
			int num2 = (int)Math.Min(this.m_cActualData, 5U * this.m_nSeasonality);
			int num3 = num2 - num;
			double num4 = 0.0;
			int num5 = (int)(this.m_nSeasonality - this.m_nSeasonality / 2U);
			double num6 = (double)num;
			double[] array = new double[num3];
			for (int i = 0; i < num2; i++)
			{
				if (i >= num)
				{
					array[i - num] = num4 / num6;
					num4 -= this.m_rgxnumActualData[i - num];
				}
				num4 += this.m_rgxnumActualData[i];
			}
			int num7 = 0;
			while ((long)num7 < (long)((ulong)this.m_nSeasonality))
			{
				double num8 = 0.0;
				num4 = 0.0;
				for (int j = num7; j < num3; j += (int)this.m_nSeasonality)
				{
					num8 += 1.0;
					if (!seasonIsMultiplicative)
					{
						num4 = num4 + this.m_rgxnumActualData[(int)(checked((IntPtr)(unchecked((long)j + (long)((ulong)(this.m_nSeasonality / 2U)) - 1L))))] - array[j];
					}
					else if (Math.Abs(array[j]) >= 5E-324)
					{
						num4 += this.m_rgxnumActualData[(int)(checked((IntPtr)(unchecked((long)j + (long)((ulong)(this.m_nSeasonality / 2U)) - 1L))))] / array[j];
					}
				}
				if (num8 > 0.0)
				{
					num4 /= num8;
				}
				else
				{
					num4 = 0.0;
				}
				if (num7 > num5)
				{
					checked
					{
						rgxnumX0[(int)((IntPtr)(unchecked((ulong)(2U + this.m_nSeasonality) + (ulong)((long)num5) - (ulong)((long)num7))))] = num4;
					}
				}
				else
				{
					rgxnumX0[2 + num5 - num7] = num4;
				}
				num7++;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002BEC File Offset: 0x00000DEC
		private static void NormalizeStateVector(double[] rgxnumX0, bool seasonIsMultiplicative)
		{
			double num = 0.0;
			int num2 = rgxnumX0.Length - 2;
			for (int i = 0; i < num2; i++)
			{
				num += rgxnumX0[i + 2];
			}
			num /= (double)num2;
			if (!seasonIsMultiplicative)
			{
				rgxnumX0[0] = rgxnumX0[0] + num;
				for (int j = 0; j < num2; j++)
				{
					rgxnumX0[j + 2] = rgxnumX0[j + 2] - num;
				}
				return;
			}
			if (Math.Abs(num) > 5E-324)
			{
				rgxnumX0[0] = rgxnumX0[0] * num;
				for (int k = 0; k < num2; k++)
				{
					rgxnumX0[k + 2] = rgxnumX0[k + 2] / num;
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002C80 File Offset: 0x00000E80
		private ForecastStats FindOptimalParameters(uint season, uint numberOfStates, bool errorIsMultiplicative)
		{
			double[,] array;
			double[,] array2;
			if (season <= 1U)
			{
				array = ForecastingModel.verticesNoSeasonalNoTrend;
				array2 = ForecastingModel.verticesNonSeasonal;
			}
			else
			{
				array = ForecastingModel.verticesSeasonalNoTrend;
				array2 = ForecastingModel.verticesSeasonal;
			}
			List<ForecastStats> list = new List<ForecastStats>();
			bool flag = false;
			double[] array3 = this.ComputeInitialState(season > 1U, flag, numberOfStates);
			list.Add(ForecastingModel.FindOptimialParameterAlphaBetaGamma(array2, array3, this.m_rgxnumActualData, this.m_cActualData, season, errorIsMultiplicative, flag, true, double.MaxValue * this.m_xnumGradientDamper, this.m_xnumGradientDamper, this.m_cTrainWindow));
			list.Add(ForecastingModel.FindOptimialParameterAlphaBetaGamma(array, array3, this.m_rgxnumActualData, this.m_cActualData, season, errorIsMultiplicative, flag, false, double.MaxValue * this.m_xnumGradientDamper, this.m_xnumGradientDamper, this.m_cTrainWindow));
			if (errorIsMultiplicative)
			{
				flag = true;
				array3 = this.ComputeInitialState(season > 1U, flag, numberOfStates);
				list.Add(ForecastingModel.FindOptimialParameterAlphaBetaGamma(array2, array3, this.m_rgxnumActualData, this.m_cActualData, season, errorIsMultiplicative, flag, true, double.MaxValue * this.m_xnumGradientDamper, this.m_xnumGradientDamper, this.m_cTrainWindow));
				list.Add(ForecastingModel.FindOptimialParameterAlphaBetaGamma(array, array3, this.m_rgxnumActualData, this.m_cActualData, season, errorIsMultiplicative, flag, false, double.MaxValue * this.m_xnumGradientDamper, this.m_xnumGradientDamper, this.m_cTrainWindow));
			}
			ForecastStats forecastStats = list[0];
			double num = forecastStats.GetAICCorrectedLoss();
			for (int i = 1; i < list.Count; i++)
			{
				ForecastStats forecastStats2 = list[i];
				double aiccorrectedLoss = forecastStats2.GetAICCorrectedLoss();
				if (aiccorrectedLoss < num)
				{
					forecastStats = forecastStats2;
					num = aiccorrectedLoss;
				}
			}
			return forecastStats;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002E0C File Offset: 0x0000100C
		private double[] ComputeInitialState(bool hasSeason, bool seasonIsMultiplicative, uint numberOfStates)
		{
			double[] array = new double[numberOfStates];
			array[0] = this.m_rgxnumActualData[0];
			if (hasSeason)
			{
				this.AddSeasonalityCorrections(array, seasonIsMultiplicative);
				if (!seasonIsMultiplicative)
				{
					array[0] = array[0] - array[(int)(1U + this.m_nSeasonality)];
				}
				else if (Math.Abs(array[(int)(1U + this.m_nSeasonality)]) > 5E-324)
				{
					array[0] = array[0] / array[(int)(1U + this.m_nSeasonality)];
				}
			}
			ForecastingModel.NormalizeStateVector(array, seasonIsMultiplicative);
			double num = this.CalculateSlope(this.m_rgxnumActualData, (int)this.m_cActualData, array, seasonIsMultiplicative, 0);
			array[1] = num;
			array[0] = array[0] - array[1];
			return array;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002EA4 File Offset: 0x000010A4
		private void PropagateStateVector(ForecastStats result)
		{
			IReadOnlyList<double> initialState = result.InitialState;
			uint count = (uint)initialState.Count;
			uint num = count - 1U;
			uint num2 = 0U;
			uint num3 = ((this.m_cData > 10U) ? ((uint)Math.Ceiling(this.m_cData / 2U)) : 0U);
			uint num4 = ((this.m_cData > 10U) ? ((uint)Math.Ceiling(this.m_cData / 2U)) : this.m_cData);
			double num5 = 0.0;
			double num6 = 0.0;
			double num7 = 0.0;
			double num8 = 0.0;
			for (uint num9 = 0U; num9 < count; num9 += 1U)
			{
				this.m_rgxnumState[(int)num9] = initialState[(int)num9];
			}
			if (!result.HasTrend)
			{
				this.m_rgxnumState[0] += this.m_rgxnumState[1];
				this.m_rgxnumState[1] = 0.0;
			}
			for (uint num9 = 0U; num9 < this.m_cData; num9 += 1U)
			{
				double num10;
				ForecastingModel.TryComputeForecastAndDelta(result.ErrorIsMultiplicative, result.SeasonIsMultiplicative, this.m_rgxnumState, num, this.m_rgxnumActualData[(int)num9], this.m_cData, out num8, out num5, out num10);
				if (this.m_rgfOriginalData == null || this.m_rgfOriginalData[(int)num9])
				{
					this.m_xnumVariance += num5 * num5;
				}
				else
				{
					num2 += 1U;
				}
				ForecastingModel.UpdateStateVectorByOneStep(num5, result.Alpha, result.Beta, result.Gamma, ref num, result.ErrorIsMultiplicative, result.SeasonIsMultiplicative, this.m_rgxnumState, count);
				if (num9 > num3)
				{
					num6 += this.m_rgxnumState[1];
					num7 += this.m_rgxnumState[1] * this.m_rgxnumState[1];
				}
			}
			this.ShiftStateVector((int)num);
			double num11 = num4;
			double num12 = num6 / num11;
			num11 = num4 - 1U;
			double num13 = Math.Sqrt(Math.Abs((num7 - num6 * num6 / num11) / num11));
			double num14 = ((num4 > 1U) ? num13 : 0.0);
			if (result.HasTrend)
			{
				num11 = Math.Abs(num14 / num12);
				if (num12 == 0.0 || num11 > 1.0)
				{
					if (!result.SeasonIsMultiplicative)
					{
						this.m_rgxnumState[0] = this.m_rgxnumActualData[(int)(this.m_cData - 1U)] - this.m_rgxnumState[2];
					}
					else if (Math.Abs(this.m_rgxnumState[2]) < 5E-324)
					{
						this.m_rgxnumState[0] = 0.0;
					}
					else
					{
						this.m_rgxnumState[0] = this.m_rgxnumActualData[(int)(this.m_cData - 1U)] / this.m_rgxnumState[2];
					}
					if (!result.ErrorIsMultiplicative && result.Season <= 1U && result.HasTrend)
					{
						int num15 = Math.Max(10, (int)Math.Floor(this.m_cData * 0.3));
						num15 = Math.Min(num15, (int)Math.Floor(this.m_cData * 0.5));
						int num16 = ((this.m_cData > 10U) ? num15 : ((int)this.m_cData));
						int num17 = (int)(this.m_cData - (uint)num16);
						double num18 = this.CalculateSlope(this.m_rgxnumActualData, num16, this.m_rgxnumState, result.SeasonIsMultiplicative, num17);
						this.m_rgxnumState[1] = num18;
					}
					else
					{
						this.m_rgxnumState[1] = initialState[1];
					}
					result.ResetBeta(0.001);
				}
			}
			if (num2 != this.m_cData)
			{
				num11 = this.m_cData - num2;
				this.m_xnumVariance /= num11;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003261 File Offset: 0x00001461
		private void ShiftStateVector(int iLastElementIndex)
		{
			this.m_rgxnumState = ForecastingModel.ShiftStateVector(iLastElementIndex, this.m_rgxnumState);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003278 File Offset: 0x00001478
		private static double[] ShiftStateVector(int iLastElementIndex, IReadOnlyList<double> finalState)
		{
			uint count = (uint)finalState.Count;
			if (iLastElementIndex < 2 || (long)iLastElementIndex > (long)((ulong)(count - 1U)))
			{
				throw new ArgumentException("iLastElementIndex < 2 || iLastElementIndex > m_cState - 1");
			}
			double[] array = new double[finalState.Count];
			int num = (int)((ulong)(count - 1U) - (ulong)((long)iLastElementIndex));
			if (num != 0)
			{
				int num2 = finalState.Count - 2;
				double[] array2 = new double[num2];
				for (int i = 0; i < num2; i++)
				{
					array2[i] = finalState[i + 2];
				}
				for (int j = 0; j < num2; j++)
				{
					int num3 = j + 2 + num;
					if ((long)num3 > (long)((ulong)(count - 1U)))
					{
						num3 = num3 % (int)count + 2;
					}
					array[num3] = array2[j];
				}
				array[0] = finalState[0];
				array[1] = finalState[1];
			}
			else
			{
				for (int k = 0; k < finalState.Count; k++)
				{
					array[k] = finalState[k];
				}
			}
			return array;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003358 File Offset: 0x00001558
		private static bool DetermineIfMultModelCanBeUsed(double likelihoodAdditiveModel, double likelihoodMultModel, double trainingVariance, int season, ForecastStats parameters, IReadOnlyList<double> finalState, IReadOnlyList<double> trainingData, int lengthOfTrainingData)
		{
			int num = 3 * season;
			if (likelihoodMultModel * 1000.0 < likelihoodAdditiveModel)
			{
				return true;
			}
			double num2 = double.MaxValue;
			double num3 = double.MinValue;
			int num4 = (int)Math.Floor((double)lengthOfTrainingData / 2.0);
			for (int i = 0; i < num4; i++)
			{
				if (trainingData[i] < trainingData[lengthOfTrainingData - 1 - i])
				{
					num2 = Math.Min(trainingData[i], num2);
					num3 = Math.Max(trainingData[lengthOfTrainingData - 1 - i], num3);
				}
				else
				{
					num2 = Math.Min(trainingData[lengthOfTrainingData - 1 - i], num2);
					num3 = Math.Max(trainingData[i], num3);
				}
			}
			double[] array = ForecastingModel.ShiftStateVector((int)parameters.LastElementIndex, parameters.FinalState);
			ForecastingModel.NormalizeStateVector(array, parameters.SeasonIsMultiplicative);
			double[] array2 = ForecastingModel.ComputeVarianceForOffsetForMultErrorModel(num, parameters.Season, trainingVariance, parameters.Alpha, parameters.Beta, parameters.Gamma, array, parameters.HasTrend, parameters.SeasonIsMultiplicative);
			double num5 = ForecastingModel.ConfidenceValueTozScore(0.95);
			double num6 = 1000.0 * (num3 - num2);
			for (int j = 0; j < array2.Length; j++)
			{
				if (num5 * array2[j] - num2 > num6)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000034B4 File Offset: 0x000016B4
		private static double ComputeForecast(int offset, IReadOnlyList<double> state, uint season, bool seasonIsMultiplicative)
		{
			int num = (int)((long)state.Count - (long)(offset - 1) % (long)((ulong)season) - 1L);
			if (seasonIsMultiplicative)
			{
				return (state[0] + state[1] * (double)offset) * state[num];
			}
			return state[0] + state[1] * (double)offset + state[num];
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003510 File Offset: 0x00001710
		private static double[] ComputeVarianceForOffsetForMultErrorModel(int offset, uint seasonality, double variance, double alpha, double beta, double gamma, IReadOnlyList<double> states, bool hasTrend, bool seasonIsMultplicative)
		{
			uint count = (uint)states.Count;
			double[] array = new double[offset];
			double num = seasonality;
			if (!hasTrend && seasonIsMultplicative)
			{
				for (int i = 1; i <= offset; i++)
				{
					double num2 = Math.Floor((double)(i - 1) / num);
					int num3 = (int)((ulong)(count - 1U) - (ulong)((long)(i - 1) % (long)((ulong)seasonality)));
					array[i - 1] = states[num3] * states[num3] * states[0] * states[0] * ((1.0 + variance) * Math.Pow(1.0 + alpha * alpha * variance, (double)(i - 1)) * Math.Pow(1.0 + gamma * gamma * variance, num2) - 1.0);
				}
				return array;
			}
			if (hasTrend && seasonIsMultplicative)
			{
				double[] array2 = new double[offset];
				double[] array3 = new double[offset];
				double num4 = states[0];
				double num5 = states[1];
				array3[0] = states[0] + states[1];
				array2[0] = array3[0] * array3[0];
				for (int j = 2; j <= offset; j++)
				{
					double num6 = 0.0;
					for (int k = 1; k <= j - 1; k++)
					{
						double num7 = alpha + beta * (double)k;
						num6 += num7 * num7 * array2[j - k - 1];
					}
					num6 *= variance;
					double num8 = array3[j - 2] + states[1];
					num6 += num8 * num8;
					array2[j - 1] = num6;
					array3[j - 1] = num8;
				}
				for (int l = 1; l <= offset; l++)
				{
					int num9 = (int)((ulong)(count - 1U) - (ulong)((long)(l - 1) % (long)((ulong)seasonality)));
					int num10 = (int)Math.Floor((double)(l - 1) / num);
					array[l - 1] = array2[l - 1] * (1.0 + variance) * Math.Pow(1.0 + gamma * gamma * variance, (double)num10) - array3[l - 1] * array3[l - 1];
					array[l - 1] *= states[num9] * states[num9];
				}
			}
			else
			{
				double[] array4 = new double[offset];
				if (seasonality <= 1U)
				{
					gamma = 0.0;
				}
				if (!hasTrend)
				{
					beta = 0.0;
				}
				double num11 = states[0];
				double num12 = states[1];
				double num13 = ForecastingModel.ComputeForecast(1, states, seasonality, seasonIsMultplicative);
				array4[0] = num13 * num13;
				array[0] = (1.0 + variance) * array4[0] - num13 * num13;
				for (int m = 2; m <= offset; m++)
				{
					double num14 = 0.0;
					for (int n = 1; n <= m - 1; n++)
					{
						double num15 = alpha + beta * (double)n;
						if (gamma > 0.0)
						{
							int num16 = (((long)n % (long)((ulong)seasonality) == 0L) ? 1 : 0);
							num15 += gamma * (double)num16;
						}
						num14 += num15 * num15 * array4[m - n - 1];
					}
					num14 *= variance;
					num13 = ForecastingModel.ComputeForecast(m, states, seasonality, seasonIsMultplicative);
					num14 += num13 * num13;
					array4[m - 1] = num14;
					array[m - 1] = (1.0 + variance) * num14 - num13 * num13;
				}
			}
			return array;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003870 File Offset: 0x00001A70
		private static double ComputeVarianceForOffsetForAdditiveErrorModel(int nOffset, uint seasonality, double variance, double alpha, double beta, double gamma)
		{
			Contract.Check(nOffset >= 1, "nOffset >= 1");
			if (nOffset == 1)
			{
				return variance;
			}
			double num = (double)((int)((long)(nOffset - 1) / (long)((ulong)seasonality)));
			double num2 = (double)(nOffset - 1);
			double num3 = num;
			double num4 = (double)(2 * nOffset - 1);
			double num5 = (double)nOffset * num4 * beta * beta / 6.0;
			num4 = alpha * alpha;
			num5 = ((double)nOffset * alpha * beta + num5 + num4) * num2 + 1.0;
			num4 = gamma + beta * seasonality * (num3 + 1.0);
			return ((2.0 * alpha + num4) * (gamma * num3) + num5) * variance;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003908 File Offset: 0x00001B08
		private static ForecastStats FindOptimialParameterAlphaBetaGamma(double[,] parameterSpace, IReadOnlyList<double> initialState, IReadOnlyList<double> data, uint numberOfData, uint season, bool errorIsMultiplicative, bool seasonIsMultiplicative, bool considerTrend, double minLossSoFar, double gradientDamper, uint trainWindowLength)
		{
			ForecastStats forecastStats = new ForecastStats(errorIsMultiplicative, considerTrend, seasonIsMultiplicative, initialState, season, numberOfData, minLossSoFar);
			double num = 0.0;
			double num2 = 0.0;
			for (int i = 0; i < parameterSpace.GetLength(0); i++)
			{
				double num3;
				if (considerTrend)
				{
					num3 = parameterSpace[i, 0];
					num = parameterSpace[i, 1];
					if (season > 1U)
					{
						num2 = parameterSpace[i, 2];
					}
				}
				else
				{
					num3 = parameterSpace[i, 0];
					if (season > 1U)
					{
						num2 = parameterSpace[i, 1];
					}
				}
				double num4;
				double num5;
				double num6;
				uint num7;
				double[] array = ForecastingModel.ComputeLoss(num3, num, num2, initialState, data, numberOfData, minLossSoFar, considerTrend, errorIsMultiplicative, seasonIsMultiplicative, gradientDamper, trainWindowLength, out num4, out num5, out num6, out num7);
				if (num4 < minLossSoFar && num6 < 5.0)
				{
					minLossSoFar = num4;
					forecastStats.UpdateParameters(num3, num, num2, num4, num5, array, num6, num7);
					if (Math.Abs(num4) < 5E-324)
					{
						break;
					}
				}
			}
			return forecastStats;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003A04 File Offset: 0x00001C04
		private static double[] ComputeLoss(double alpha, double beta, double gamma, IReadOnlyList<double> initialState, IReadOnlyList<double> data, uint numberOfData, double currentMinLoss, bool considerTrend, bool errorIsMultiplicative, bool seasonIsMultiplicative, double gradientDamper, uint trainWindowLength, out double loss, out double likelihoodCorrection, out double trendDeviation, out uint lastElementIndex)
		{
			loss = 0.0;
			likelihoodCorrection = 0.0;
			trendDeviation = 0.0;
			double num = 0.0;
			uint count = (uint)initialState.Count;
			lastElementIndex = count - 1U;
			double[] array = new double[count];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)count))
			{
				array[num2] = initialState[num2];
				num2++;
			}
			if (!considerTrend)
			{
				array[0] += array[1];
				array[1] = 0.0;
				beta = 0.0;
			}
			double num3 = currentMinLoss / gradientDamper;
			double num4 = 0.0;
			double num5 = 0.0;
			int num6 = 0;
			while ((long)num6 < (long)((ulong)numberOfData))
			{
				bool flag = (long)num6 >= (long)((ulong)trainWindowLength);
				double num7;
				double num8;
				double num9;
				bool flag2 = ForecastingModel.TryComputeForecastAndDelta(errorIsMultiplicative, seasonIsMultiplicative, array, lastElementIndex, data[num6], numberOfData, out num7, out num8, out num9);
				likelihoodCorrection += num9;
				if (!flag2)
				{
					loss = currentMinLoss * 2.0 + 1.0;
					return array;
				}
				num += num8 * num8;
				if (num > num3)
				{
					loss = num * gradientDamper;
					return array;
				}
				if (flag)
				{
					num4 += array[1];
					num5 += array[1] * array[1];
				}
				ForecastingModel.UpdateStateVectorByOneStep(num8, alpha, beta, gamma, ref lastElementIndex, errorIsMultiplicative, seasonIsMultiplicative, array, count);
				num6++;
			}
			if (numberOfData > trainWindowLength)
			{
				uint num10 = numberOfData - trainWindowLength;
				double num11 = num4 / num10;
				double num12 = num5 / num10 - num11 * num11;
				if (Math.Abs(num11) > 5E-324)
				{
					trendDeviation = Math.Abs(num12 / num11 / num11);
				}
			}
			loss = num * gradientDamper;
			return array;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003BB8 File Offset: 0x00001DB8
		private static bool TryComputeForecastAndDelta(bool errorIsMultiplicative, bool seasonIsMultiplicative, IReadOnlyList<double> state, uint seasonIdx, double data, uint numberOfData, out double forecast, out double delta, out double likelihoodCorrection)
		{
			likelihoodCorrection = 0.0;
			if (!errorIsMultiplicative)
			{
				forecast = state[0] + state[1] + state[(int)seasonIdx];
				delta = data - forecast;
			}
			else
			{
				if (!seasonIsMultiplicative)
				{
					forecast = state[0] + state[1] + state[(int)seasonIdx];
					likelihoodCorrection = Math.Log(Math.Abs(forecast)) / numberOfData;
				}
				else
				{
					forecast = (state[0] + state[1]) * state[(int)seasonIdx];
					likelihoodCorrection = Math.Log(Math.Abs(forecast)) / numberOfData;
				}
				if (Math.Abs(forecast) <= 5E-324)
				{
					delta = double.MaxValue;
					return false;
				}
				delta = data / forecast - 1.0;
			}
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003C98 File Offset: 0x00001E98
		private static void UpdateStateVectorByOneStep(double xnumDelta, double xnumAlpha, double xnumBeta, double xnumGamma, ref uint piLastElementIndex, bool errorIsMultiplicative, bool seasonIsMultiplicative, double[] state, uint numberOfStates)
		{
			uint num = piLastElementIndex;
			if (!errorIsMultiplicative)
			{
				state[0] = state[0] + xnumAlpha * xnumDelta + state[1];
				state[1] = state[1] + xnumBeta * xnumDelta;
				state[(int)num] = state[(int)num] + xnumGamma * xnumDelta;
			}
			else if (!seasonIsMultiplicative)
			{
				double num2 = state[0] + state[1] + state[(int)num];
				state[0] = state[0] + state[1] + xnumAlpha * num2 * xnumDelta;
				state[1] = state[1] + xnumBeta * num2 * xnumDelta;
				state[(int)num] = state[(int)num] + xnumGamma * num2 * xnumDelta;
			}
			else
			{
				double num3 = state[0] + state[1];
				state[0] = num3 * (1.0 + xnumAlpha * xnumDelta);
				state[1] = state[1] + xnumBeta * num3 * xnumDelta;
				state[(int)num] = state[(int)num] * (1.0 + xnumGamma * xnumDelta);
			}
			piLastElementIndex = ((num == 2U) ? (numberOfStates - 1U) : (num - 1U));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003D74 File Offset: 0x00001F74
		private static double RationalApproximation(double t)
		{
			double[] array = new double[] { 2.515517, 0.802853, 0.010328 };
			double[] array2 = new double[] { 1.432788, 0.189269, 0.001308 };
			return t - ((array[2] * t + array[1]) * t + array[0]) / (((array2[2] * t + array2[1]) * t + array2[0]) * t + 1.0);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003DD4 File Offset: 0x00001FD4
		private static double NormalCDFInverse(double p)
		{
			if (p <= 0.0 || p >= 1.0)
			{
				throw new ArgumentException("p <= 0.0 || p >= 1.0");
			}
			if (p < 0.5)
			{
				return -ForecastingModel.RationalApproximation(Math.Sqrt(-2.0 * Math.Log(p)));
			}
			return ForecastingModel.RationalApproximation(Math.Sqrt(-2.0 * Math.Log(1.0 - p)));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003E50 File Offset: 0x00002050
		public static double ConfidenceValueTozScore(double conf)
		{
			return ForecastingModel.NormalCDFInverse(1.0 - (1.0 - conf) / 2.0);
		}

		// Token: 0x0400002A RID: 42
		private const uint DATA_COMPLETION_SEASON_COUNT = 4U;

		// Token: 0x0400002B RID: 43
		private double m_xnumGradientDamper;

		// Token: 0x0400002C RID: 44
		private double[] m_rgxnumData;

		// Token: 0x0400002D RID: 45
		private uint m_cData;

		// Token: 0x0400002E RID: 46
		private double[] m_rgxnumActualData;

		// Token: 0x0400002F RID: 47
		private uint m_cActualData;

		// Token: 0x04000030 RID: 48
		private double m_xnumActualDataCount;

		// Token: 0x04000031 RID: 49
		private bool[] m_rgfOriginalData;

		// Token: 0x04000032 RID: 50
		private double[] m_rgxnumState;

		// Token: 0x04000033 RID: 51
		private uint m_nSeasonality;

		// Token: 0x04000034 RID: 52
		private double m_xnumSeasonality;

		// Token: 0x04000035 RID: 53
		private double m_xnumVariance;

		// Token: 0x04000036 RID: 54
		private uint m_cTrainWindow;

		// Token: 0x04000037 RID: 55
		private uint m_cValidationWindow;

		// Token: 0x04000038 RID: 56
		private uint m_cOriginalTrainWindow;

		// Token: 0x04000039 RID: 57
		private uint m_cOriginalValidationWindow;

		// Token: 0x0400003A RID: 58
		private ForecastStats m_stats;

		// Token: 0x0400003B RID: 59
		private static double[,] verticesNoSeasonalNoTrend = new double[,]
		{
			{ 0.9, 0.0 },
			{ 0.75, 0.0 },
			{ 0.5, 0.0 },
			{ 0.25, 0.0 },
			{ 0.126, 0.0 },
			{ 0.1, 0.0 },
			{ 0.002, 0.0 }
		};

		// Token: 0x0400003C RID: 60
		private static double[,] verticesSeasonalNoTrend = new double[,]
		{
			{ 0.751, 0.001 },
			{ 0.501, 0.001 },
			{ 0.251, 0.001 },
			{ 0.126, 0.001 },
			{ 0.833333, 0.0833333 },
			{ 0.5, 0.0833333 },
			{ 0.166667, 0.0833333 },
			{ 0.9, 0.099 },
			{ 0.45, 0.099 },
			{ 0.1, 0.099 },
			{ 0.8, 0.125 },
			{ 0.5, 0.125 },
			{ 0.002, 0.125 },
			{ 0.666667, 0.166667 },
			{ 0.333333, 0.166667 },
			{ 0.75, 0.249 },
			{ 0.5, 0.25 },
			{ 0.251, 0.25 },
			{ 0.002, 0.25 },
			{ 0.1, 0.5 },
			{ 0.333333, 0.5 },
			{ 0.45, 0.5 },
			{ 0.25, 0.749 },
			{ 0.002, 0.75 },
			{ 0.166667, 0.75 },
			{ 0.1, 0.899 }
		};

		// Token: 0x0400003D RID: 61
		private static double[,] verticesSeasonal = new double[,]
		{
			{ 0.751, 0.0001, 0.001 },
			{ 0.501, 0.0001, 0.001 },
			{ 0.251, 0.0001, 0.001 },
			{ 0.126, 0.0001, 0.001 },
			{ 0.751, 0.001, 0.001 },
			{ 0.501, 0.001, 0.001 },
			{ 0.251, 0.001, 0.001 },
			{ 0.126, 0.001, 0.001 },
			{ 0.998, 0.099, 0.001 },
			{ 0.126, 0.125, 0.001 },
			{ 0.998, 0.249, 0.001 },
			{ 0.5, 0.25, 0.001 },
			{ 0.251, 0.25, 0.001 },
			{ 0.998, 0.499, 0.001 },
			{ 0.501, 0.5, 0.001 },
			{ 0.998, 0.749, 0.001 },
			{ 0.751, 0.75, 0.001 },
			{ 0.998, 0.899, 0.001 },
			{ 0.833333, 0.0833333, 0.0833333 },
			{ 0.166667, 0.0833333, 0.0833333 },
			{ 0.833333, 0.75, 0.0833333 },
			{ 0.9, 0.0001, 0.099 },
			{ 0.9, 0.001, 0.099 },
			{ 0.9, 0.899, 0.099 },
			{ 0.002, 0.0001, 0.125 },
			{ 0.002, 0.001, 0.125 },
			{ 0.666667, 0.166667, 0.166667 },
			{ 0.333333, 0.166667, 0.166667 },
			{ 0.666667, 0.5, 0.166667 },
			{ 0.75, 0.0001, 0.249 },
			{ 0.75, 0.001, 0.249 },
			{ 0.75, 0.749, 0.249 },
			{ 0.251, 0.0001, 0.25 },
			{ 0.251, 0.001, 0.25 },
			{ 0.002, 0.0001, 0.25 },
			{ 0.002, 0.001, 0.25 },
			{ 0.5, 0.25, 0.25 },
			{ 0.251, 0.25, 0.25 },
			{ 0.5, 0.0001, 0.499 },
			{ 0.5, 0.001, 0.499 },
			{ 0.5, 0.499, 0.499 },
			{ 0.002, 0.001, 0.5 },
			{ 0.333333, 0.166667, 0.5 },
			{ 0.25, 0.0001, 0.749 },
			{ 0.25, 0.001, 0.749 },
			{ 0.25, 0.249, 0.749 },
			{ 0.002, 0.0001, 0.75 },
			{ 0.002, 0.001, 0.75 },
			{ 0.166667, 0.0833333, 0.75 },
			{ 0.1, 0.0001, 0.899 },
			{ 0.1, 0.001, 0.899 },
			{ 0.1, 0.099, 0.899 }
		};

		// Token: 0x0400003E RID: 62
		private static double[,] verticesNonSeasonal = new double[,]
		{
			{ 0.9, 0.001 },
			{ 0.75, 0.001 },
			{ 0.5, 0.001 },
			{ 0.25, 0.001 },
			{ 0.126, 0.001 },
			{ 0.1, 0.001 },
			{ 0.002, 0.001 },
			{ 0.833333, 0.0833333 },
			{ 0.166667, 0.0833333 },
			{ 0.998, 0.099 },
			{ 0.1, 0.099 },
			{ 0.126, 0.125 },
			{ 0.666667, 0.166667 },
			{ 0.333333, 0.166667 },
			{ 0.998, 0.249 },
			{ 0.25, 0.249 },
			{ 0.5, 0.25 },
			{ 0.998, 0.499 },
			{ 0.5, 0.499 },
			{ 0.666667, 0.5 },
			{ 0.998, 0.749 },
			{ 0.75, 0.749 },
			{ 0.833333, 0.75 },
			{ 0.998, 0.899 },
			{ 0.9, 0.899 }
		};
	}
}
