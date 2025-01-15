using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000024 RID: 36
	internal sealed class DataPostprocessor
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003A4B File Offset: 0x00001C4B
		internal DataPostprocessor(ForecastContext forecastContext, DataPreprocessResult trainData)
		{
			this._forecastContext = forecastContext;
			this._trainData = trainData;
			this._dataRowFactory = forecastContext.TransformContext.RowFactory;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003A72 File Offset: 0x00001C72
		internal DataTransformResult Postprocess(ForecastResult forecastResult, IReadOnlyList<DataTransformMessage> messages)
		{
			return new DataTransformResult(this.PostprocessCore(forecastResult), messages);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003A81 File Offset: 0x00001C81
		private IEnumerable<IDataRow> PostprocessCore(ForecastResult forecastResult)
		{
			DataPostprocessor.<>c__DisplayClass6_0 CS$<>8__locals1 = new DataPostprocessor.<>c__DisplayClass6_0();
			CS$<>8__locals1.forecastResult = forecastResult;
			CS$<>8__locals1.<>4__this = this;
			IReadOnlyList<IDataRow> dataRows = this._trainData.DataRows;
			int count = dataRows.Count;
			int num;
			for (int i = 0; i < this._trainData.LastTrainRowIndex; i = num + 1)
			{
				yield return dataRows[i].AddColumns(DataPostprocessor.EmptyForecastResult);
				num = i;
			}
			int num2 = this._trainData.LastTrainRowIndex;
			IDataRow dataRow;
			double? asDouble;
			for (;;)
			{
				dataRow = dataRows[num2];
				asDouble = dataRow.GetAsDouble(this._forecastContext.YValueColumnIndex);
				if (asDouble != null)
				{
					break;
				}
				num2--;
				if (num2 < 0)
				{
					goto IL_0184;
				}
			}
			object obj = asDouble.Value.ConvertTo(this._forecastContext.OutputDataType, ConvertType.Round, 0.0);
			yield return dataRow.AddColumns(new object[] { obj, obj, obj });
			IL_0184:
			CS$<>8__locals1.xColumnIndex = this._forecastContext.XValueColumnIndex;
			ISchemaRow inputSchema = this._forecastContext.TransformContext.InputSchema;
			CS$<>8__locals1.xColumnDataType = inputSchema.GetColumn(CS$<>8__locals1.xColumnIndex).DataType;
			CS$<>8__locals1.outputType = this._forecastContext.OutputDataType;
			DataType xColumnDataType = CS$<>8__locals1.xColumnDataType;
			if (xColumnDataType != DataType.DateTime)
			{
				if (xColumnDataType - DataType.Decimal > 2)
				{
					throw new TransformException("unsupported x axis datatype");
				}
				double num3 = this._trainData.CorrectedXValues[this._trainData.SizeAfterCorrection - 1];
				double[] forecastValues = DataPostprocessor.GetForecastXValues(num3, this._trainData, CS$<>8__locals1.forecastResult);
				IEnumerable<IDataRow> enumerable = DataPostprocessor.MergeIgnoredHistoricalValueWithForecastedValue<double>(this._trainData.DataRows, this._trainData.LastTrainRowIndex + 1, forecastValues, delegate(IDataRow data, double x)
				{
					double num5 = data.CoerceIntoDouble(CS$<>8__locals1.xColumnIndex);
					double num6 = x - num5;
					double num7 = Math.Abs(x);
					if (Math.Abs(num6) < Math.Min(1E-10, 1E-08 * num7))
					{
						return ForecastStepData.SGN.sgnEQ;
					}
					if (num5 < x)
					{
						return ForecastStepData.SGN.sgnLT;
					}
					if (num5 > x)
					{
						return ForecastStepData.SGN.sgnGT;
					}
					return ForecastStepData.SGN.sgnEQ;
				}, (int idx, double x, IDataRow data) => DataPostprocessor.CreateDataRowFromForecastPoint(CS$<>8__locals1.forecastResult.Points[idx - 1], forecastValues[idx].ConvertTo(CS$<>8__locals1.xColumnDataType, ConvertType.Round, 0.0), data, CS$<>8__locals1.outputType, CS$<>8__locals1.<>4__this._forecastContext, CS$<>8__locals1.<>4__this._dataRowFactory));
				foreach (IDataRow dataRow2 in enumerable)
				{
					yield return dataRow2;
				}
				IEnumerator<IDataRow> enumerator = null;
			}
			else
			{
				double num4 = this._trainData.DataStep.PxnumGetNumericStep();
				double stepsRoundingErrorThresholdInSeconds = ((num4 >= 0.5) ? 1.0 : (num4 * 2.0));
				DateTime dateTime = this._trainData.CorrectedXValues[this._trainData.SizeAfterCorrection - 1].ToDateTime();
				DateTime[] forecastDates = DataPostprocessor.GetForecastDates(dateTime, this._trainData, CS$<>8__locals1.forecastResult);
				IEnumerable<IDataRow> enumerable2 = DataPostprocessor.MergeIgnoredHistoricalValueWithForecastedValue<DateTime>(this._trainData.DataRows, this._trainData.LastTrainRowIndex + 1, forecastDates, delegate(IDataRow data, DateTime x)
				{
					DateTime dateTime2 = data.CoerceIntoDateTime(CS$<>8__locals1.xColumnIndex);
					if (((dateTime2 > x) ? (dateTime2 - x) : (x - dateTime2)).TotalSeconds < stepsRoundingErrorThresholdInSeconds)
					{
						return ForecastStepData.SGN.sgnEQ;
					}
					if (dateTime2 < x)
					{
						return ForecastStepData.SGN.sgnLT;
					}
					return ForecastStepData.SGN.sgnGT;
				}, (int idx, DateTime x, IDataRow data) => DataPostprocessor.CreateDataRowFromForecastPoint(CS$<>8__locals1.forecastResult.Points[idx - 1], forecastDates[idx], data, CS$<>8__locals1.outputType, CS$<>8__locals1.<>4__this._forecastContext, CS$<>8__locals1.<>4__this._dataRowFactory));
				foreach (IDataRow dataRow3 in enumerable2)
				{
					yield return dataRow3;
				}
				IEnumerator<IDataRow> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003A98 File Offset: 0x00001C98
		private static IEnumerable<IDataRow> MergeIgnoredHistoricalValueWithForecastedValue<T>(IReadOnlyList<IDataRow> historicalRows, int startingIdxInHistoricalValues, IReadOnlyList<T> forecastedXValues, Func<IDataRow, T, ForecastStepData.SGN> compare, Func<int, T, IDataRow, IDataRow> makeDataRow)
		{
			int i = startingIdxInHistoricalValues;
			int j = 1;
			while (i < historicalRows.Count)
			{
				if (j >= forecastedXValues.Count)
				{
					break;
				}
				switch (compare(historicalRows[i], forecastedXValues[j]))
				{
				case ForecastStepData.SGN.sgnLT:
				{
					yield return historicalRows[i].AddColumns(DataPostprocessor.EmptyForecastResult);
					int num = i;
					i = num + 1;
					break;
				}
				case ForecastStepData.SGN.sgnEQ:
				{
					IDataRow dataRow = makeDataRow(j, forecastedXValues[j], historicalRows[i]);
					yield return dataRow;
					int num = i;
					i = num + 1;
					num = j;
					j = num + 1;
					break;
				}
				case ForecastStepData.SGN.sgnGT:
				{
					IDataRow dataRow2 = makeDataRow(j, forecastedXValues[j], null);
					yield return dataRow2;
					int num = j;
					j = num + 1;
					break;
				}
				default:
					throw new TransformException("unrecognized compare value");
				}
			}
			while (i < historicalRows.Count)
			{
				yield return historicalRows[i].AddColumns(DataPostprocessor.EmptyForecastResult);
				int num = i;
				i = num + 1;
			}
			while (j < forecastedXValues.Count)
			{
				yield return makeDataRow(j, forecastedXValues[j], null);
				int num = j;
				j = num + 1;
			}
			yield break;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003AC8 File Offset: 0x00001CC8
		private static IDataRow CreateDataRowFromForecastPoint(ForecastPoint forecastPoint, object xValue, IDataRow ignoredData, DataType outputType, ForecastContext forecastContext, IDataRowFactory dataRowFactory)
		{
			object obj = forecastPoint.ForecastValue.ConvertTo(outputType, ConvertType.Round, 0.0);
			object obj2 = forecastPoint.LowerBoundValue.ConvertTo(outputType, ConvertType.LowerBound, 1E-05);
			object obj3 = forecastPoint.UpperboundValue.ConvertTo(outputType, ConvertType.UpperBound, 1E-05);
			object[] array = new object[forecastContext.OutputSchemaColumnCount];
			if (ignoredData != null)
			{
				for (int i = 0; i < ignoredData.Count; i++)
				{
					array[i] = ignoredData.GetObject(i);
				}
			}
			if (ignoredData == null)
			{
				array[forecastContext.XValueColumnIndex] = xValue;
			}
			array[forecastContext.ForecastValueColumnIndex] = obj;
			array[forecastContext.LowerBoundForecastValue] = obj2;
			array[forecastContext.UpperBoundForecastValue] = obj3;
			return dataRowFactory.CreateDataRow(array);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003B80 File Offset: 0x00001D80
		private static DateTime[] GetForecastDates(DateTime baseDate, DataPreprocessResult trainData, ForecastResult forecastResult)
		{
			DateTime[] array = new DateTime[forecastResult.Points.Count + 1];
			array[0] = baseDate;
			for (int i = 1; i <= forecastResult.Points.Count; i++)
			{
				if (trainData.DataStep.FIsBusinessStep())
				{
					ForecastStepData.BusinessStepMonthsType businessStepMonthsType = (ForecastStepData.BusinessStepMonthsType)trainData.DataStep.NGetBusinessStep();
					if (businessStepMonthsType <= ForecastStepData.BusinessStepMonthsType.bsmtQuarterly)
					{
						if (businessStepMonthsType == ForecastStepData.BusinessStepMonthsType.bsmtMonthly)
						{
							array[i] = Utils.AddMonthEndAware(array[i - 1], 1);
							goto IL_00F3;
						}
						if (businessStepMonthsType == ForecastStepData.BusinessStepMonthsType.bsmtQuarterly)
						{
							array[i] = Utils.AddMonthEndAware(array[i - 1], 3);
							goto IL_00F3;
						}
					}
					else
					{
						if (businessStepMonthsType == ForecastStepData.BusinessStepMonthsType.bsmtHalfYearly)
						{
							array[i] = Utils.AddMonthEndAware(array[i - 1], 6);
							goto IL_00F3;
						}
						if (businessStepMonthsType == ForecastStepData.BusinessStepMonthsType.bsmtYearly)
						{
							array[i] = Utils.AddMonthEndAware(array[i - 1], 12);
							goto IL_00F3;
						}
					}
					array[i] = Utils.AddMonthEndAware(array[i - 1], (int)businessStepMonthsType);
				}
				else
				{
					array[i] = array[i - 1].AddDays(trainData.DataStep.PxnumGetNumericStep());
				}
				IL_00F3:;
			}
			return array;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003C98 File Offset: 0x00001E98
		private static double[] GetForecastXValues(double baseValue, DataPreprocessResult trainData, ForecastResult forecastResult)
		{
			double[] array = new double[forecastResult.Points.Count + 1];
			array[0] = baseValue;
			for (int i = 1; i <= forecastResult.Points.Count; i++)
			{
				array[i] = array[i - 1] + trainData.DataStep.PxnumGetNumericStep();
			}
			return array;
		}

		// Token: 0x04000099 RID: 153
		internal static readonly IReadOnlyList<object> EmptyForecastResult = new object[3];

		// Token: 0x0400009A RID: 154
		private readonly ForecastContext _forecastContext;

		// Token: 0x0400009B RID: 155
		private readonly DataPreprocessResult _trainData;

		// Token: 0x0400009C RID: 156
		private readonly IDataRowFactory _dataRowFactory;
	}
}
