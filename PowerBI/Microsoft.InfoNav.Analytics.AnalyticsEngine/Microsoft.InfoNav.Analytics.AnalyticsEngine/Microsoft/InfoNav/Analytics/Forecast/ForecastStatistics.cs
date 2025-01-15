using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000032 RID: 50
	internal sealed class ForecastStatistics
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x0000558F File Offset: 0x0000378F
		internal ForecastStatistics()
		{
			this.InvalidSeasonality = false;
			this._handledProcessErrorsByForecasterType = new List<KeyValuePair<string, List<string>>>();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000055A9 File Offset: 0x000037A9
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000055B1 File Offset: 0x000037B1
		internal int? IgnoreLast { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000055BA File Offset: 0x000037BA
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000055C2 File Offset: 0x000037C2
		internal string IgnoreLastUnit { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000055CB File Offset: 0x000037CB
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000055D3 File Offset: 0x000037D3
		internal int? ActualLengthOfTrainingData { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000055DC File Offset: 0x000037DC
		// (set) Token: 0x060000CA RID: 202 RVA: 0x000055E4 File Offset: 0x000037E4
		internal int? SizeAfterCorrection { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000055ED File Offset: 0x000037ED
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000055F5 File Offset: 0x000037F5
		internal Tuple<int, string> SizeAfterCorrectionRevisedAndReason { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000055FE File Offset: 0x000037FE
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00005606 File Offset: 0x00003806
		internal int? ForecastLength { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000560F File Offset: 0x0000380F
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00005617 File Offset: 0x00003817
		internal int? ForecastLengthRevised { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005620 File Offset: 0x00003820
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00005628 File Offset: 0x00003828
		internal string ForecastAlgorithm { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005631 File Offset: 0x00003831
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00005639 File Offset: 0x00003839
		internal int? Delay { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005642 File Offset: 0x00003842
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000564A File Offset: 0x0000384A
		internal bool InvalidSeasonality { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005653 File Offset: 0x00003853
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000565B File Offset: 0x0000385B
		internal TransformException Exception { get; set; }

		// Token: 0x060000D9 RID: 217 RVA: 0x00005664 File Offset: 0x00003864
		internal void AddHandledError(string error, string forecasterName = null)
		{
			if (string.IsNullOrEmpty(forecasterName))
			{
				forecasterName = "Other";
			}
			KeyValuePair<string, List<string>> keyValuePair = this._handledProcessErrorsByForecasterType.Find((KeyValuePair<string, List<string>> item) => string.CompareOrdinal(item.Key, forecasterName) == 0);
			List<string> list = keyValuePair.Value;
			if (keyValuePair.Equals(default(KeyValuePair<string, List<string>>)))
			{
				list = new List<string>();
				this._handledProcessErrorsByForecasterType.Add(new KeyValuePair<string, List<string>>(forecasterName, list));
			}
			list.Add(error);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000056F8 File Offset: 0x000038F8
		internal void AddHandledError(TransformException e, string forecasterName = null)
		{
			string exceptionText = ForecastStatistics.GetExceptionText(e);
			this.AddHandledError(exceptionText, forecasterName);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005714 File Offset: 0x00003914
		internal IReadOnlyList<DataTransformMessage> GetWarnings()
		{
			List<DataTransformMessage> list = new List<DataTransformMessage>();
			if (this.InvalidSeasonality)
			{
				list.Add(new DataTransformMessage(ForecastErrorType.InvalidSeasonality.ToErrorCode(), DataTransformMessageSeverity.Warning, "Invalid Seasonality"));
			}
			return list;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005748 File Offset: 0x00003948
		internal void FireTelemetryEvent(ITelemetryService telemetryService)
		{
			List<string> list = new List<string>();
			if (this.IgnoreLast != null)
			{
				list.Add(ForecastStatistics.FormatPropDetailValue("IgnoreLast", this.IgnoreLast));
			}
			if (this.IgnoreLastUnit != null)
			{
				list.Add(ForecastStatistics.FormatPropDetailValue("IgnoreLastUnit", this.IgnoreLastUnit));
			}
			if (this.ActualLengthOfTrainingData != null)
			{
				list.Add(ForecastStatistics.FormatPropDetailValue("TrainingDataActualLen", this.ActualLengthOfTrainingData));
			}
			if (this.SizeAfterCorrection != null)
			{
				list.Add(ForecastStatistics.FormatPropDetailValue("SizeAfterCorrection", this.SizeAfterCorrection));
			}
			if (this.SizeAfterCorrectionRevisedAndReason != null)
			{
				list.Add(ForecastStatistics.FormatPropDetailValue(StringUtil.FormatInvariant("SizeAfterCorrectionRevised because {0}", this.SizeAfterCorrectionRevisedAndReason.Item2), this.SizeAfterCorrectionRevisedAndReason.Item1));
			}
			if (this.ForecastLength != null)
			{
				list.Add(ForecastStatistics.FormatPropDetailValue("ForecastLen", this.ForecastLength));
			}
			if (this.ForecastLengthRevised != null)
			{
				list.Add(ForecastStatistics.FormatPropDetailValue("ForecastLenRevised", this.ForecastLengthRevised));
			}
			if (this.ForecastAlgorithm != null)
			{
				list.Add(ForecastStatistics.FormatPropDetailValue("Algorithm", this.ForecastAlgorithm));
			}
			if (this.Delay != null)
			{
				list.Add(ForecastStatistics.FormatPropDetailValue("Delay", this.Delay));
			}
			foreach (KeyValuePair<string, List<string>> keyValuePair in this._handledProcessErrorsByForecasterType)
			{
				string text = StringUtil.FormatInvariant("{0}Err", keyValuePair.Key);
				string text2 = string.Join("|", keyValuePair.Value);
				list.Add(ForecastStatistics.FormatPropDetailValue(text, text2));
			}
			if (this.Exception != null)
			{
				list.Add(ForecastStatistics.FormatPropDetailValue("Exception", ForecastStatistics.GetExceptionText(this.Exception)));
			}
			string text3 = "ForecastProcess";
			object[] array = list.ToArray();
			telemetryService.FireEvent(text3, array);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005980 File Offset: 0x00003B80
		private static string FormatPropDetailValue(string propertyName, object value)
		{
			return string.Join(":", new string[]
			{
				propertyName,
				value.ToString()
			});
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000059A0 File Offset: 0x00003BA0
		private static string GetExceptionText(TransformException e)
		{
			string text = (string.IsNullOrEmpty(e.ErrorCode) ? e.Message : e.ErrorCode);
			if (string.IsNullOrEmpty(text))
			{
				text = "NA";
			}
			return text;
		}

		// Token: 0x040000F8 RID: 248
		internal const string InfiniteOutputsErrorMessage = "infinite outputs";

		// Token: 0x040000F9 RID: 249
		internal const string NonNumberOutputsErrorMessage = "non-number outputs";

		// Token: 0x040000FA RID: 250
		private const string IgnoreLastProp = "IgnoreLast";

		// Token: 0x040000FB RID: 251
		private const string IgnoreLastUnitProp = "IgnoreLastUnit";

		// Token: 0x040000FC RID: 252
		private const string ActualLengthOfTrainingDataProp = "TrainingDataActualLen";

		// Token: 0x040000FD RID: 253
		private const string SizeAfterCorrectionProp = "SizeAfterCorrection";

		// Token: 0x040000FE RID: 254
		private const string SizeAfterCorrectionRevisedPropFormat = "SizeAfterCorrectionRevised because {0}";

		// Token: 0x040000FF RID: 255
		private const string ForecastLengthProp = "ForecastLen";

		// Token: 0x04000100 RID: 256
		private const string ForecastLengthRevisedProp = "ForecastLenRevised";

		// Token: 0x04000101 RID: 257
		private const string ExceptionProp = "Exception";

		// Token: 0x04000102 RID: 258
		private const string ForecasterErrorPropFormat = "{0}Err";

		// Token: 0x04000103 RID: 259
		private const string Other = "Other";

		// Token: 0x04000104 RID: 260
		private const string NA = "NA";

		// Token: 0x04000105 RID: 261
		private const string Algorithm = "Algorithm";

		// Token: 0x04000106 RID: 262
		private const string DelayProp = "Delay";

		// Token: 0x04000107 RID: 263
		private readonly List<KeyValuePair<string, List<string>>> _handledProcessErrorsByForecasterType;
	}
}
