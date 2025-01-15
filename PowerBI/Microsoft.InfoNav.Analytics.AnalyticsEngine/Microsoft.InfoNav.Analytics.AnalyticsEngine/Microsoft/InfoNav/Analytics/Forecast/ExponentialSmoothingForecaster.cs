using System;
using Microsoft.InfoNav.Analytics.ExponentialSmoothing;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000029 RID: 41
	internal sealed class ExponentialSmoothingForecaster
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00004BA2 File Offset: 0x00002DA2
		internal ExponentialSmoothingForecaster(ForecastStatistics forecastStatistics)
		{
			this._forecastStatistics = forecastStatistics;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public ETSForecastResult Forecast(ForecastContext context, DataPreprocessResult preprocessedResult, out int seasonality, int numberOfStepsAhead, int? realTrainSize = null, int? realForecastLength = null, int? suggestedSeason = null)
		{
			int num = ((realTrainSize != null) ? realTrainSize.Value : preprocessedResult.SizeAfterCorrection);
			int num2 = ((realForecastLength != null) ? realForecastLength.Value : preprocessedResult.ForecastLength);
			seasonality = -1;
			if (num2 < 1)
			{
				return ETSForecastResult.Empty;
			}
			if (num < 2)
			{
				throw ForecasterUtils.CreateNotEnoughDataException();
			}
			double[] array = new double[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = preprocessedResult.CorrectedYValues[i];
			}
			ForecastingModel forecastingModel = new ForecastingModel();
			SeasonStats seasonStats = null;
			if (context.MaxSeasonality != null && context.MaxSeasonality != null)
			{
				int? num3 = context.MaxSeasonality;
				int num4 = 0;
				if ((num3.GetValueOrDefault() > num4) & (num3 != null))
				{
					seasonality = context.MaxSeasonality.Value;
					if (seasonality > array.Length)
					{
						seasonality = array.Length;
						this._forecastStatistics.InvalidSeasonality = true;
						goto IL_0164;
					}
					goto IL_0164;
				}
			}
			if (suggestedSeason != null && suggestedSeason != null)
			{
				int? num3 = suggestedSeason;
				int num4 = 0;
				if ((num3.GetValueOrDefault() > num4) & (num3 != null))
				{
					seasonality = suggestedSeason.Value;
					goto IL_0164;
				}
			}
			SeasonalityDetector seasonalityDetector = new SeasonalityDetector();
			if (SeasonalityDetector.Fail(seasonalityDetector.Init(array, (uint)array.Length)))
			{
				throw new TransformException("Failed to Initialize Seasonality detector to determine the seasonality.");
			}
			if (SeasonalityDetector.Fail(seasonalityDetector.DetectSeasonality(out seasonality)))
			{
				throw new TransformException("Failed to determine the seasonality.");
			}
			seasonStats = seasonalityDetector.ComputeSeasonStatistics();
			IL_0164:
			forecastingModel.HrTrain(array, array.Length, null, (uint)seasonality);
			double num5 = ForecastingModel.ConfidenceValueTozScore((double)context.ConfidenceLevel);
			ForecastPoint[] array2 = new ForecastPoint[num2];
			bool flag = false;
			bool flag2 = false;
			double[] confidenceValues = ForecastingModel.GetConfidenceValues(num2, forecastingModel);
			for (int j = 0; j < num2; j++)
			{
				double num6 = forecastingModel.Forecast(j + 1);
				double num7 = num6 - num5 * confidenceValues[j];
				double num8 = num6 + num5 * confidenceValues[j];
				flag |= Utils.IsAnyInfinity(num6, num7, num8);
				flag2 |= Utils.IsAnyNan(num6, num7, num8);
				array2[j] = new ForecastPoint(num6, num7, num8);
			}
			if (flag)
			{
				this._forecastStatistics.AddHandledError("infinite outputs", "ETS");
			}
			if (flag2)
			{
				this._forecastStatistics.AddHandledError("non-number outputs", "ETS");
			}
			double num9 = 0.0;
			if (flag || flag2)
			{
				num9 = double.MaxValue;
			}
			else if (numberOfStepsAhead > 0)
			{
				Tuple<double, int> tuple = ForecastingModel.ComputeKStepForecastTrainingError(array, forecastingModel.Pfstats(), numberOfStepsAhead, array.Length);
				if (tuple.Item2 == 0)
				{
					num9 = double.MaxValue;
				}
				else
				{
					num9 = tuple.Item1 / (double)tuple.Item2;
				}
			}
			return new ETSForecastResult(seasonStats, forecastingModel.Pfstats(), array2, num9, flag, flag2);
		}

		// Token: 0x040000AD RID: 173
		private const string TelemetryForecasterName = "ETS";

		// Token: 0x040000AE RID: 174
		private readonly ForecastStatistics _forecastStatistics;
	}
}
