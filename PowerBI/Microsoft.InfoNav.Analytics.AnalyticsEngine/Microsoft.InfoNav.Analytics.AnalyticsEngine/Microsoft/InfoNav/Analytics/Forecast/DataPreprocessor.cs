using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000025 RID: 37
	internal sealed class DataPreprocessor
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00003CF4 File Offset: 0x00001EF4
		internal DataPreprocessor(ForecastStatistics forecastStatistics)
		{
			this._forecastStatistics = forecastStatistics;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003D04 File Offset: 0x00001F04
		internal List<IDataRow> ReadAndValidateDataRows(ForecastContext forecastContext, out List<double> xValues, out List<double> yValues, out double minValue, out double maxValue, out bool hasNullXValues)
		{
			hasNullXValues = false;
			ISchemaRow inputSchema = forecastContext.TransformContext.InputSchema;
			int xvalueColumnIndex = forecastContext.XValueColumnIndex;
			DataType dataType = inputSchema.GetColumn(xvalueColumnIndex).DataType;
			xValues = new List<double>();
			yValues = new List<double>();
			minValue = double.MaxValue;
			maxValue = double.MinValue;
			List<IDataRow> list = new List<IDataRow>();
			foreach (IDataRow dataRow in forecastContext.TransformContext.InputRows)
			{
				list.Add(dataRow);
				double? num = dataRow.CoerceIntoDouble(dataType, xvalueColumnIndex);
				double num2;
				if (num == null)
				{
					hasNullXValues = true;
				}
				else if (dataRow.TryGetAsDouble(forecastContext.YValueColumnIndex, out num2))
				{
					if (num2 > maxValue)
					{
						maxValue = num2;
					}
					if (num2 < minValue)
					{
						minValue = num2;
					}
					xValues.Add(num.Value);
					yValues.Add(num2);
				}
			}
			double? num3 = null;
			foreach (double num4 in xValues)
			{
				if (num3 != null)
				{
					double? num5 = num3;
					double num6 = num4;
					if ((num5.GetValueOrDefault() > num6) & (num5 != null))
					{
						throw new TransformException("x-values must be sorted ascending", ForecastErrorType.InputTimeStampsNotSorted.ToErrorCode(), ErrorSource.PowerBI);
					}
				}
				num3 = new double?(num4);
			}
			return list;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003E88 File Offset: 0x00002088
		internal DataPreprocessResult Preprocess(List<IDataRow> dataRows, List<double> xValues, List<double> yValues, double minValue, double maxValue, ForecastContext forecastContext)
		{
			ISchemaRow inputSchema = forecastContext.TransformContext.InputSchema;
			int xvalueColumnIndex = forecastContext.XValueColumnIndex;
			DataType dataType = inputSchema.GetColumn(xvalueColumnIndex).DataType;
			int num = xValues.Count - 1;
			int i = dataRows.Count - 1;
			this._forecastStatistics.IgnoreLast = new int?(forecastContext.IgnoreLast);
			this._forecastStatistics.IgnoreLastUnit = forecastContext.ParameterUnit.ToString();
			if (forecastContext.IgnoreLast > 0)
			{
				double num2;
				if (forecastContext.ParameterUnit == ForecastParameterUnit.Point)
				{
					if (num - forecastContext.IgnoreLast < 0)
					{
						string text = ForecastErrorType.DataIsTooSmall.ToErrorCode();
						throw new TransformException("The number of items to ignore must be less than the number of rows", text, ErrorSource.User);
					}
					num2 = xValues[num - forecastContext.IgnoreLast];
				}
				else
				{
					num2 = DataPreprocessor.ComputeIgnoreCutOffDate(xValues[num], forecastContext);
				}
				while (i >= 0)
				{
					double? num3 = dataRows[i].CoerceIntoDouble(dataType, xvalueColumnIndex);
					double num4 = num2;
					if (!((num3.GetValueOrDefault() > num4) & (num3 != null)))
					{
						IL_0115:
						while (num >= 0 && xValues[num] > num2)
						{
							num--;
						}
						goto IL_0119;
					}
					i--;
				}
				goto IL_0115;
			}
			IL_0119:
			int num5 = num + 1;
			if (num5 < 2)
			{
				throw ForecasterUtils.CreateNotEnoughDataException();
			}
			int num6 = (int)Math.Ceiling(1.4 * (double)num5);
			double[] array = new double[num6];
			double[] array2 = new double[num6];
			int num7;
			ForecastStepData forecastStepData;
			ForecastErrorType forecastErrorType = ForecastDataProcessor.Process(xValues, yValues, (uint)num5, array, array2, num6, out num7, out forecastStepData);
			this._forecastStatistics.ActualLengthOfTrainingData = new int?(num5);
			this._forecastStatistics.SizeAfterCorrection = new int?(num7);
			if (forecastErrorType != ForecastErrorType.NoError)
			{
				if (forecastErrorType != ForecastErrorType.AnchorSequenceNotFound || dataRows.Count <= 1500)
				{
					throw DataPreprocessor.CreateUserException(forecastErrorType);
				}
				xValues.CopyTo(array);
				yValues.CopyTo(array2);
				num7 = num5;
				this._forecastStatistics.SizeAfterCorrectionRevisedAndReason = new Tuple<int, string>(num5, StringUtil.FormatInvariant("{0} ignored for long enough data (>{1})", forecastErrorType.ToString(), 1500));
			}
			int num8;
			if (forecastContext.ParameterUnit == ForecastParameterUnit.Point)
			{
				num8 = forecastContext.ForecastLength;
				switch (dataType)
				{
				case DataType.DateTime:
				{
					TimeSpan timeSpan = DateTime.MaxValue - array[num7 - 1].ToDateTime();
					if (timeSpan.TotalDays <= forecastStepData.PxnumGetNumericStep() * (double)forecastContext.ForecastLength)
					{
						num8 = (int)(timeSpan.TotalDays / forecastStepData.PxnumGetNumericStep()) - 100;
					}
					break;
				}
				case DataType.Decimal:
				{
					double num9 = 7.922816251426434E+28 - array[num7 - 1];
					if (num9 <= forecastStepData.PxnumGetNumericStep() * (double)forecastContext.ForecastLength)
					{
						num8 = (int)(num9 / forecastStepData.PxnumGetNumericStep());
					}
					break;
				}
				case DataType.Double:
				{
					double num10 = double.MaxValue - array[num7 - 1];
					if (num10 <= forecastStepData.PxnumGetNumericStep() * (double)forecastContext.ForecastLength)
					{
						num8 = (int)(num10 / forecastStepData.PxnumGetNumericStep());
					}
					break;
				}
				case DataType.Int64:
				{
					double num11 = 9.223372036854776E+18 - array[num7 - 1];
					if (num11 <= forecastStepData.PxnumGetNumericStep() * (double)forecastContext.ForecastLength)
					{
						num8 = (int)(num11 / forecastStepData.PxnumGetNumericStep());
					}
					break;
				}
				default:
					throw new TransformException("unrecognized x-axis type");
				}
				if (num8 < 0)
				{
					num8 = 0;
				}
			}
			else
			{
				num8 = DataPreprocessor.ComputeForecastLength(array[num7 - 1], xValues[xValues.Count - 1], forecastContext, forecastStepData);
			}
			this._forecastStatistics.ForecastLength = new int?(num8);
			if (num8 > 5000)
			{
				num8 = 5000;
				this._forecastStatistics.ForecastLengthRevised = new int?(num8);
			}
			return new DataPreprocessResult(dataRows, i, num8, forecastStepData, minValue, maxValue, array, array2, num7);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000422C File Offset: 0x0000242C
		private static TransformException CreateUserException(ForecastErrorType result)
		{
			string text = result.ToErrorCode();
			if (result == ForecastErrorType.DataIsTooSmall)
			{
				return new TransformException("not enough data in inputs", text, ErrorSource.User);
			}
			if (result == ForecastErrorType.TooManyMissingValues || result == ForecastErrorType.NoDominantStepDetected)
			{
				return new TransformException("data is not evently spaced", text, ErrorSource.User);
			}
			if (result == ForecastErrorType.InputTimeStampsNotSorted)
			{
				return new TransformException("input is not sorted", text, ErrorSource.User);
			}
			if (result == ForecastErrorType.AnchorSequenceNotFound)
			{
				text = ForecastErrorType.NoDominantStepDetected.ToErrorCode();
				return new TransformException("anchor sequence not found", text, ErrorSource.User);
			}
			string text2 = ForecastErrorType.Unexpected.ToErrorCode();
			return new TransformException("data preprocessing failed with return value:" + text, text2, ErrorSource.PowerBI);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000042B0 File Offset: 0x000024B0
		private static double ComputeIgnoreCutOffDate(double lastXValue, ForecastContext forecastContext)
		{
			double num;
			try
			{
				num = lastXValue.ToDateTime().AddUnits(forecastContext.ParameterUnit, -forecastContext.IgnoreLast).ToDouble();
			}
			catch (ArgumentOutOfRangeException)
			{
				throw ForecasterUtils.CreateNotEnoughDataException();
			}
			return num;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000042F8 File Offset: 0x000024F8
		private static int ComputeForecastLength(double lastNotIgnoredXValue, double lastXValue, ForecastContext forecastContext, ForecastStepData stepData)
		{
			DateTime dateTime = lastXValue.ToDateTime();
			DateTime dateTime2 = lastNotIgnoredXValue.ToDateTime();
			DateTime dateTime3 = dateTime;
			try
			{
				dateTime3 = dateTime3.AddUnits(forecastContext.ParameterUnit, -forecastContext.IgnoreLast);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw ForecasterUtils.CreateNotEnoughDataException();
			}
			DateTime dateTime4;
			try
			{
				dateTime4 = dateTime3.AddUnits(forecastContext.ParameterUnit, forecastContext.ForecastLength);
			}
			catch (ArgumentOutOfRangeException)
			{
				dateTime4 = DateTime.MaxValue;
			}
			int num = (int)Math.Round((dateTime4 - dateTime2).TotalDays / stepData.PxnumGetNumericStep());
			if (num < 1)
			{
				return 1;
			}
			return num;
		}

		// Token: 0x0400009D RID: 157
		private const string AnchorSequenceNotFoundErrorIgnoredFormat = "{0} ignored for long enough data (>{1})";

		// Token: 0x0400009E RID: 158
		private readonly ForecastStatistics _forecastStatistics;
	}
}
