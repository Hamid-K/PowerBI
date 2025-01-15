using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.TimeSeriesProcessing;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000036 RID: 54
	internal sealed class SingleSpectrumAnalysisForecaster : IDisposable
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00005E24 File Offset: 0x00004024
		internal SingleSpectrumAnalysisForecaster(ForecastStatistics forecastStatistics)
		{
			this._forecastStatistics = forecastStatistics;
			this._host = new TlcEnvironment(null, false, 0, null, null);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005E56 File Offset: 0x00004056
		public void Dispose()
		{
			this._host.Dispose();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005E64 File Offset: 0x00004064
		public SSAForecastResult TrainAndForecast(ForecastContext context, DataPreprocessResult preprocessedResult, out SingleSpectrumAnalysisForecaster.SSAModelInfo modelInfo, int numberOfStepsAhead, int? seasonEstimate = null, int? realTrainSize = null, int? realForecastLength = null, int? consumeLength = null)
		{
			modelInfo = null;
			int num = ((realTrainSize != null) ? realTrainSize.Value : preprocessedResult.SizeAfterCorrection);
			int num2 = ((realForecastLength != null) ? realForecastLength.Value : preprocessedResult.ForecastLength);
			bool flag = consumeLength != null;
			if (flag)
			{
				int value = consumeLength.Value;
			}
			if (num2 < 1)
			{
				return SSAForecastResult.Empty;
			}
			if (num < 6)
			{
				throw ForecasterUtils.CreateNotEnoughDataException();
			}
			int num3 = 0;
			if (context.MaxSeasonality != null)
			{
				int? num4 = context.MaxSeasonality;
				int num5 = 0;
				if ((num4.GetValueOrDefault() > num5) & (num4 != null))
				{
					num3 = context.MaxSeasonality.Value * 2;
					goto IL_00EC;
				}
			}
			if (seasonEstimate != null)
			{
				int? num4 = seasonEstimate;
				int num5 = 1;
				if ((num4.GetValueOrDefault() > num5) & (num4 != null))
				{
					num3 = seasonEstimate.Value * 2;
				}
			}
			IL_00EC:
			Func<double, float> func;
			if (preprocessedResult.NeedNormalize)
			{
				func = (double x) => preprocessedResult.GetNormalizedValue(x);
			}
			else
			{
				func = null;
			}
			modelInfo = SingleSpectrumAnalysisForecaster.Train(preprocessedResult.CorrectedYValues, 0, num, this._host, num3, func, null);
			SSAForecastResult ssaforecastResult = SingleSpectrumAnalysisForecaster.ForecastWithTrainedModel(context, preprocessedResult, modelInfo, num2, this._forecastStatistics, numberOfStepsAhead);
			if (flag)
			{
				if (preprocessedResult.NeedNormalize)
				{
					for (int i = num; i < num + consumeLength.Value; i++)
					{
						float normalizedValue = preprocessedResult.GetNormalizedValue(preprocessedResult.CorrectedYValues[i]);
						modelInfo.Model.Consume(ref normalizedValue, false);
					}
				}
				else
				{
					for (int j = num; j < num + consumeLength.Value; j++)
					{
						float num6 = (float)preprocessedResult.CorrectedYValues[j];
						modelInfo.Model.Consume(ref num6, false);
					}
				}
			}
			return ssaforecastResult;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006057 File Offset: 0x00004257
		public SSAForecastResult ForecastWithModel(ForecastContext context, DataPreprocessResult preprocessedResult, SingleSpectrumAnalysisForecaster.SSAModelInfo modelInfo, int forecastSize, int numberOfStepsAhead)
		{
			return SingleSpectrumAnalysisForecaster.ForecastWithTrainedModel(context, preprocessedResult, modelInfo, forecastSize, this._forecastStatistics, numberOfStepsAhead);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000606C File Offset: 0x0000426C
		private static SSAForecastResult ForecastWithTrainedModel(ForecastContext context, DataPreprocessResult preprocessedResult, SingleSpectrumAnalysisForecaster.SSAModelInfo modelInfo, int forecastSize, ForecastStatistics forecastStatistics, int numberOfStepsAhead)
		{
			Func<float, double> func = (float x) => preprocessedResult.GetDenormalizedValue(x);
			if (!preprocessedResult.NeedNormalize)
			{
				func = null;
			}
			SSAForecastResult ssaforecastResult = SingleSpectrumAnalysisForecaster.ForecastWithTrainedModel(modelInfo, forecastSize, (double)context.ConfidenceLevel, func);
			if (ssaforecastResult.HasInfinity)
			{
				forecastStatistics.AddHandledError("infinite outputs", "SSA");
			}
			if (ssaforecastResult.HasNan)
			{
				forecastStatistics.AddHandledError("non-number outputs", "SSA");
			}
			Tuple<double, int> tuple = SingleSpectrumAnalysisForecaster.ComputeKStepTrainingForecastError(preprocessedResult.CorrectedYValues, modelInfo, numberOfStepsAhead, preprocessedResult.SizeAfterCorrection);
			double num;
			if (tuple.Item2 == 0)
			{
				num = double.MaxValue;
			}
			else
			{
				num = tuple.Item1 / (double)tuple.Item2;
			}
			return new SSAForecastResult(new SSAFeatures(modelInfo), ssaforecastResult.Points, num, ssaforecastResult.HasInfinity, ssaforecastResult.HasNan);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006154 File Offset: 0x00004354
		public static SingleSpectrumAnalysisForecaster.SSAModelInfo Train(IReadOnlyList<double> yValues, int startIdx, int trainSize, TlcEnvironment host, int windowSizeLimit = 0, Func<double, float> normalizer = null, int? rank = null)
		{
			bool flag = trainSize <= 20;
			int num = trainSize / 2 - 1;
			int num2 = trainSize / 3;
			int num3 = (flag ? num : num2);
			if (windowSizeLimit > 0)
			{
				num3 = Math.Min(windowSizeLimit, num3);
			}
			if (num3 < 2)
			{
				num3 = 2;
			}
			else if (num3 > 145)
			{
				num3 = 145;
			}
			if (num3 * 2 >= trainSize || num3 < 2)
			{
				throw ForecasterUtils.CreateNotEnoughDataException();
			}
			bool flag2 = normalizer != null;
			FixedSizeQueue<float> fixedSizeQueue = new FixedSizeQueue<float>(trainSize);
			if (flag2)
			{
				for (int i = 0; i < trainSize; i++)
				{
					fixedSizeQueue.AddLast(normalizer(yValues[startIdx + i]));
				}
			}
			else
			{
				for (int j = 0; j < trainSize; j++)
				{
					fixedSizeQueue.AddLast((float)yValues[startIdx + j]);
				}
			}
			AdaptiveSingularSpectrumSequenceModeler adaptiveSingularSpectrumSequenceModeler;
			if (rank != null)
			{
				int num4 = num3 + 1;
				int num5 = num3;
				float num6 = 1f;
				FixedSizeQueue<float> fixedSizeQueue2 = null;
				AdaptiveSingularSpectrumSequenceModeler.RankSelectionMethod rankSelectionMethod = 0;
				int? num7 = rank;
				int? num8 = null;
				adaptiveSingularSpectrumSequenceModeler = new AdaptiveSingularSpectrumSequenceModeler(host, trainSize, num4, num5, num6, fixedSizeQueue2, rankSelectionMethod, num7, num8, true, true, true, null);
			}
			else
			{
				int num9 = num3 + 1;
				int num10 = num3;
				float num11 = 1f;
				FixedSizeQueue<float> fixedSizeQueue3 = null;
				AdaptiveSingularSpectrumSequenceModeler.RankSelectionMethod rankSelectionMethod2 = 1;
				int? num8 = new int?(num3 / 2);
				adaptiveSingularSpectrumSequenceModeler = new AdaptiveSingularSpectrumSequenceModeler(host, trainSize, num9, num10, num11, fixedSizeQueue3, rankSelectionMethod2, null, num8, true, true, true, null);
			}
			adaptiveSingularSpectrumSequenceModeler.Train(fixedSizeQueue);
			return new SingleSpectrumAnalysisForecaster.SSAModelInfo(adaptiveSingularSpectrumSequenceModeler, num3);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000628C File Offset: 0x0000448C
		public static SSAForecastResult ForecastWithTrainedModel(SingleSpectrumAnalysisForecaster.SSAModelInfo model, int forecastSize, double confidenceLevel, Func<float, double> denormalizer = null)
		{
			AdaptiveSingularSpectrumSequenceModeler model2 = model.Model;
			if (forecastSize < 1)
			{
				return SSAForecastResult.Empty;
			}
			AdaptiveSingularSpectrumSequenceModeler.SsaForecastResult ssaForecastResult = new AdaptiveSingularSpectrumSequenceModeler.SsaForecastResult();
			ForecastResultBase<float> forecastResultBase = ssaForecastResult;
			model2.Forecast(ref forecastResultBase, forecastSize);
			AdaptiveSingularSpectrumSequenceModeler.ComputeForecastIntervals(ref ssaForecastResult, (float)confidenceLevel);
			ForecastPoint[] array = new ForecastPoint[forecastSize];
			bool flag = false;
			bool flag2 = false;
			if (denormalizer != null)
			{
				for (int i = 0; i < forecastSize; i++)
				{
					float itemOrDefault = ssaForecastResult.PointForecast.GetItemOrDefault(i);
					float itemOrDefault2 = ssaForecastResult.LowerBound.GetItemOrDefault(i);
					float itemOrDefault3 = ssaForecastResult.UpperBound.GetItemOrDefault(i);
					flag |= Utils.IsAnyInfinity(itemOrDefault, itemOrDefault2, itemOrDefault3);
					flag2 |= Utils.IsAnyNan(itemOrDefault, itemOrDefault2, itemOrDefault3);
					double num = denormalizer(itemOrDefault);
					double num2 = denormalizer(itemOrDefault2);
					double num3 = denormalizer(itemOrDefault3);
					array[i] = new ForecastPoint(num, num2, num3);
				}
			}
			else
			{
				for (int j = 0; j < forecastSize; j++)
				{
					float itemOrDefault4 = ssaForecastResult.PointForecast.GetItemOrDefault(j);
					float itemOrDefault5 = ssaForecastResult.LowerBound.GetItemOrDefault(j);
					float itemOrDefault6 = ssaForecastResult.UpperBound.GetItemOrDefault(j);
					flag |= Utils.IsAnyInfinity(itemOrDefault4, itemOrDefault5, itemOrDefault6);
					flag2 |= Utils.IsAnyNan(itemOrDefault4, itemOrDefault5, itemOrDefault6);
					array[j] = new ForecastPoint((double)itemOrDefault4, (double)itemOrDefault5, (double)itemOrDefault6);
				}
			}
			return new SSAForecastResult(new SSAFeatures(model), array, double.MaxValue, flag, flag2);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000063F4 File Offset: 0x000045F4
		public static Tuple<double, int> ComputeKStepTrainingForecastError(IReadOnlyList<double> yValues, SingleSpectrumAnalysisForecaster.SSAModelInfo modelInfo, int k, int yValuesLength)
		{
			if (k == 0)
			{
				return new Tuple<double, int>(double.MaxValue, 0);
			}
			AdaptiveSingularSpectrumSequenceModeler model = modelInfo.Model;
			int windowSize = modelInfo.WindowSize;
			ForecastResultBase<float> forecastResultBase = new AdaptiveSingularSpectrumSequenceModeler.SsaForecastResult();
			double num = 0.0;
			model.InitState();
			int num2 = 0;
			while (num2 < windowSize - 1 && num2 < yValuesLength)
			{
				float num3 = (float)yValues[num2];
				model.Consume(ref num3, false);
				num2++;
			}
			int num4 = 0;
			for (int i = windowSize - 1; i < yValuesLength; i++)
			{
				int num5 = i + k - 1;
				if (num5 >= yValuesLength)
				{
					break;
				}
				model.Forecast(ref forecastResultBase, k);
				double num6 = (double)forecastResultBase.PointForecast.GetItemOrDefault(k - 1) - yValues[num5];
				float num7 = (float)yValues[i];
				model.Consume(ref num7, false);
				num += num6 * num6;
				num4++;
			}
			return new Tuple<double, int>(num, num4);
		}

		// Token: 0x04000115 RID: 277
		internal const string TelemetryForecasterName = "SSA";

		// Token: 0x04000116 RID: 278
		private readonly ForecastStatistics _forecastStatistics;

		// Token: 0x04000117 RID: 279
		private readonly TlcEnvironment _host;

		// Token: 0x02000053 RID: 83
		internal sealed class SSAModelInfo
		{
			// Token: 0x06000168 RID: 360 RVA: 0x000082C3 File Offset: 0x000064C3
			internal SSAModelInfo(AdaptiveSingularSpectrumSequenceModeler model, int windowSize)
			{
				this.Model = model;
				this.WindowSize = windowSize;
			}

			// Token: 0x040001A7 RID: 423
			internal readonly AdaptiveSingularSpectrumSequenceModeler Model;

			// Token: 0x040001A8 RID: 424
			internal readonly int WindowSize;
		}
	}
}
