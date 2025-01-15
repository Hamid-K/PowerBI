using System;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000027 RID: 39
	internal sealed class EnsembleForecaster : IForecaster
	{
		// Token: 0x0600009B RID: 155 RVA: 0x000044B0 File Offset: 0x000026B0
		internal EnsembleForecaster(ForecastStatistics forecastStatistics)
		{
			this._forecastStatistics = forecastStatistics;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000044C0 File Offset: 0x000026C0
		public ForecastResult Forecast(ForecastContext context, DataPreprocessResult preprocessedResult)
		{
			int sizeAfterCorrection = preprocessedResult.SizeAfterCorrection;
			int forecastLength = preprocessedResult.ForecastLength;
			if (sizeAfterCorrection < 2 && sizeAfterCorrection < 6)
			{
				throw ForecasterUtils.CreateNotEnoughDataException();
			}
			if (forecastLength < 1)
			{
				return ETSForecastResult.Empty;
			}
			int num = -1;
			if (context.Algorithm != null)
			{
				Algorithm? algorithm = context.Algorithm;
				Algorithm? algorithm2 = algorithm;
				Algorithm algorithm3 = Algorithm.SSA;
				if (((algorithm2.GetValueOrDefault() == algorithm3) & (algorithm2 != null)) && sizeAfterCorrection < 6)
				{
					throw ForecasterUtils.CreateNotEnoughDataException();
				}
				this._forecastStatistics.ForecastAlgorithm = algorithm.ToString();
				algorithm2 = algorithm;
				algorithm3 = Algorithm.SSA;
				if ((algorithm2.GetValueOrDefault() == algorithm3) & (algorithm2 != null))
				{
					ForecastResult forecastResult;
					bool flag;
					using (SingleSpectrumAnalysisForecaster singleSpectrumAnalysisForecaster = new SingleSpectrumAnalysisForecaster(this._forecastStatistics))
					{
						flag = this.TryRunSSA(context, preprocessedResult, singleSpectrumAnalysisForecaster, null, 0, out forecastResult, null);
					}
					if (!flag)
					{
						return this.RunETS(context, preprocessedResult, null, 0, out num);
					}
					return forecastResult;
				}
				else
				{
					algorithm2 = algorithm;
					algorithm3 = Algorithm.ETS;
					if ((algorithm2.GetValueOrDefault() == algorithm3) & (algorithm2 != null))
					{
						return this.RunETS(context, preprocessedResult, null, 0, out num);
					}
				}
			}
			ExponentialSmoothingForecaster exponentialSmoothingForecaster = new ExponentialSmoothingForecaster(this._forecastStatistics);
			ForecastResult forecastResult2 = this.RunETS(context, preprocessedResult, null, 10, out num);
			if (sizeAfterCorrection < 6)
			{
				return forecastResult2;
			}
			if (num <= 32 && !forecastResult2.HasInfinity && !forecastResult2.HasNan)
			{
				this.LogAlgorithmInUse(Algorithm.ETS);
				return forecastResult2;
			}
			ForecastResult forecastResult4;
			using (SingleSpectrumAnalysisForecaster singleSpectrumAnalysisForecaster2 = new SingleSpectrumAnalysisForecaster(this._forecastStatistics))
			{
				ForecasterSelectionResult forecasterSelectionResult = this.ComputeValidationError(context, preprocessedResult, singleSpectrumAnalysisForecaster2, exponentialSmoothingForecaster);
				if (forecasterSelectionResult.ETSValidationError != null && forecasterSelectionResult.SSAValidationError != null)
				{
					double? ssavalidationError = forecasterSelectionResult.SSAValidationError;
					double? etsvalidationError = forecasterSelectionResult.ETSValidationError;
					if (!((ssavalidationError.GetValueOrDefault() >= etsvalidationError.GetValueOrDefault()) & ((ssavalidationError != null) & (etsvalidationError != null))))
					{
						ForecastResult forecastResult3;
						if (!this.TryRunSSA(context, preprocessedResult, singleSpectrumAnalysisForecaster2, forecasterSelectionResult.SSAModelInfo, 10, out forecastResult3, null))
						{
							this.LogAlgorithmInUse(Algorithm.ETS);
							return forecastResult2;
						}
						if (forecastResult3.Score >= forecastResult2.Score && !forecastResult2.HasInfinity && !forecastResult2.HasNan)
						{
							this.LogAlgorithmInUse(Algorithm.ETS);
							return forecastResult2;
						}
						this.LogAlgorithmInUse(Algorithm.SSA);
						return forecastResult3;
					}
				}
				this.LogAlgorithmInUse(Algorithm.ETS);
				forecastResult4 = forecastResult2;
			}
			return forecastResult4;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000473C File Offset: 0x0000293C
		private void LogAlgorithmInUse(Algorithm algo)
		{
			this._forecastStatistics.ForecastAlgorithm = algo.ToString();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004758 File Offset: 0x00002958
		private bool TryRunSSA(ForecastContext context, DataPreprocessResult preprocessedResult, SingleSpectrumAnalysisForecaster forecaster, SingleSpectrumAnalysisForecaster.SSAModelInfo ssaModelInfo, int numberOfStepsAhead, out ForecastResult ssaForecastResult, int? seasonEstimate = null)
		{
			bool flag;
			try
			{
				if (ssaModelInfo != null)
				{
					ssaForecastResult = forecaster.ForecastWithModel(context, preprocessedResult, ssaModelInfo, preprocessedResult.ForecastLength, numberOfStepsAhead);
				}
				else
				{
					ssaForecastResult = forecaster.TrainAndForecast(context, preprocessedResult, out ssaModelInfo, numberOfStepsAhead, seasonEstimate, null, null, null);
				}
				flag = !ssaForecastResult.HasNan && !ssaForecastResult.HasInfinity;
			}
			catch (Exception ex) when (!ex.IsStoppingException())
			{
				if (this.SSAExceptionHandler(ex))
				{
					forecaster.Dispose();
					throw;
				}
				ssaForecastResult = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004810 File Offset: 0x00002A10
		private bool SSAExceptionHandler(Exception e)
		{
			if (e.ExceptionOrInnerExceptionsSatisfiesCondition((Exception ex) => ex is AppDomainUnloadedException || ex is BadImageFormatException || ex is CannotUnloadAppDomainException || ex is InvalidProgramException))
			{
				return true;
			}
			string text = e.ToString(3);
			this._forecastStatistics.AddHandledError("SSA", text);
			return false;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004860 File Offset: 0x00002A60
		private ForecastResult RunETS(ForecastContext context, DataPreprocessResult preprocessedResult, int? suggestedSeason, int numberOfStepsAhead, out int seasonality)
		{
			return new ExponentialSmoothingForecaster(this._forecastStatistics).Forecast(context, preprocessedResult, out seasonality, numberOfStepsAhead, null, null, suggestedSeason);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004898 File Offset: 0x00002A98
		private ForecasterSelectionResult ComputeValidationError(ForecastContext context, DataPreprocessResult preprocessedResult, SingleSpectrumAnalysisForecaster ssaForecaster, ExponentialSmoothingForecaster etsForecaster)
		{
			int sizeAfterCorrection = preprocessedResult.SizeAfterCorrection;
			if (sizeAfterCorrection > 2500 || sizeAfterCorrection < 6)
			{
				return ForecasterSelectionResult.Default;
			}
			int num = (int)((double)sizeAfterCorrection * 0.2);
			int num2 = sizeAfterCorrection - num;
			if (num2 < 6)
			{
				return ForecasterSelectionResult.Default;
			}
			int num3;
			ETSForecastResult etsforecastResult = etsForecaster.Forecast(context, preprocessedResult, out num3, 1, new int?(num2), new int?(num), null);
			if (etsforecastResult.HasInfinity || etsforecastResult.HasNan)
			{
				return ForecasterSelectionResult.Default;
			}
			double num4 = 0.0;
			for (int i = 0; i < num; i++)
			{
				num4 += EnsembleForecaster.ComputeSMAPE(etsforecastResult.Points[i].ForecastValue, preprocessedResult.CorrectedYValues[num2 + i]);
			}
			ForecastResult forecastResult = null;
			SingleSpectrumAnalysisForecaster.SSAModelInfo ssamodelInfo = null;
			try
			{
				forecastResult = ssaForecaster.TrainAndForecast(context, preprocessedResult, out ssamodelInfo, 0, null, new int?(num2), new int?(num), new int?(num));
			}
			catch (Exception ex) when (!ex.IsStoppingException())
			{
				if (this.SSAExceptionHandler(ex))
				{
					throw;
				}
				return new ForecasterSelectionResult(Algorithm.ETS, null, null, null, null, null);
			}
			if (forecastResult.HasInfinity || forecastResult.HasNan)
			{
				return new ForecasterSelectionResult(Algorithm.ETS, null, null, null, null, null);
			}
			double num5 = 0.0;
			for (int j = 0; j < num; j++)
			{
				num5 += EnsembleForecaster.ComputeSMAPE(forecastResult.Points[j].ForecastValue, preprocessedResult.CorrectedYValues[num2 + j]);
			}
			if (num4 < 1E-05 || 1.1 * num4 < num5)
			{
				return new ForecasterSelectionResult(Algorithm.ETS, new int?(num3), null, null, new double?(num4), new double?(num5));
			}
			if (20.0 * num5 < num4)
			{
				return new ForecasterSelectionResult(Algorithm.SSA, null, new int?(num), ssamodelInfo, new double?(num4), new double?(num5));
			}
			return new ForecasterSelectionResult(Algorithm.Auto, null, null, null, new double?(num4), new double?(num5));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004B30 File Offset: 0x00002D30
		private static double ComputeSMAPE(double x, double y)
		{
			double num = Math.Abs(x - y);
			if (num > 5E-324)
			{
				num /= Math.Abs(x) + Math.Abs(y);
			}
			return num;
		}

		// Token: 0x040000A9 RID: 169
		private readonly ForecastStatistics _forecastStatistics;
	}
}
