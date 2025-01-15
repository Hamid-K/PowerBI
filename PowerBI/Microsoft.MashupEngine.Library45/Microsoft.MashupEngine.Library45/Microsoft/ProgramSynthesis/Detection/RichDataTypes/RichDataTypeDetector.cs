using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000A84 RID: 2692
	public class RichDataTypeDetector
	{
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600430D RID: 17165 RVA: 0x000D1AE4 File Offset: 0x000CFCE4
		public IReadOnlyList<IRichDataType> CandidateTypes
		{
			get
			{
				return this._candidateTypes;
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x0600430E RID: 17166 RVA: 0x000D1AEC File Offset: 0x000CFCEC
		public int RejectionThreshold { get; }

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x0600430F RID: 17167 RVA: 0x000D1AF4 File Offset: 0x000CFCF4
		public double MaxRejectionFraction { get; }

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06004310 RID: 17168 RVA: 0x000D1AFC File Offset: 0x000CFCFC
		// (set) Token: 0x06004311 RID: 17169 RVA: 0x000D1B04 File Offset: 0x000CFD04
		public double MeanInputLength { get; private set; }

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06004312 RID: 17170 RVA: 0x000D1B0D File Offset: 0x000CFD0D
		// (set) Token: 0x06004313 RID: 17171 RVA: 0x000D1B15 File Offset: 0x000CFD15
		public double InputLengthStdDev { get; private set; }

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06004314 RID: 17172 RVA: 0x000D1B1E File Offset: 0x000CFD1E
		// (set) Token: 0x06004315 RID: 17173 RVA: 0x000D1B26 File Offset: 0x000CFD26
		public double MaxInputLength { get; private set; }

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x000D1B2F File Offset: 0x000CFD2F
		// (set) Token: 0x06004317 RID: 17175 RVA: 0x000D1B37 File Offset: 0x000CFD37
		public double MinInputLength { get; private set; } = double.MaxValue;

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06004318 RID: 17176 RVA: 0x000D1B40 File Offset: 0x000CFD40
		// (set) Token: 0x06004319 RID: 17177 RVA: 0x000D1B48 File Offset: 0x000CFD48
		public double NullValueCount { get; private set; }

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x000D1B51 File Offset: 0x000CFD51
		// (set) Token: 0x0600431B RID: 17179 RVA: 0x000D1B59 File Offset: 0x000CFD59
		public double EmptyStringCount { get; private set; }

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x000D1B62 File Offset: 0x000CFD62
		// (set) Token: 0x0600431D RID: 17181 RVA: 0x000D1B6A File Offset: 0x000CFD6A
		public double NormalizedStringCount { get; private set; }

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x0600431E RID: 17182 RVA: 0x000D1B73 File Offset: 0x000CFD73
		// (set) Token: 0x0600431F RID: 17183 RVA: 0x000D1B7B File Offset: 0x000CFD7B
		public double NumSamplesProcessed { get; private set; }

		// Token: 0x06004320 RID: 17184 RVA: 0x000D1B84 File Offset: 0x000CFD84
		public RichDataTypeDetector(IEnumerable<IRichDataType> candidates, int rejectionThreshold = 1, double maxRejectionFraction = 0.001)
		{
			this._candidateTypes = candidates.ToList<IRichDataType>();
			this.RejectionThreshold = rejectionThreshold;
			this.MaxRejectionFraction = maxRejectionFraction;
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x000D1BB8 File Offset: 0x000CFDB8
		public IEnumerable<IRichDataType> DetectAll(IEnumerable<string> samples, long? trueDatasetSize = null, ILogger logger = null, Guid? correlationId = null)
		{
			if (this._usedOnce)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot call {0} more than once on the same instance of {1}.", new object[] { "DetectAll", "RichDataTypeDetector" })));
			}
			this._usedOnce = true;
			IEnumerable<IRichDataType> enumerable;
			try
			{
				enumerable = this._DetectAll(samples, trueDatasetSize, logger, correlationId);
			}
			catch (Exception ex)
			{
				if (logger != null)
				{
					logger.TrackException(ex);
				}
				throw;
			}
			return enumerable;
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x000D1C2C File Offset: 0x000CFE2C
		private IEnumerable<IRichDataType> _DetectAll(IEnumerable<string> samples, long? trueDatasetSize = null, ILogger logger = null, Guid? correlationId = null)
		{
			bool flag = true;
			long num = 0L;
			Stopwatch stopwatch = Stopwatch.StartNew();
			Predicate<IRichDataType> <>9__5;
			foreach (string text in samples)
			{
				num += 1L;
				string text2 = ((text == null) ? text : text.NormalizeAndTrim(NormalizationForm.FormC));
				if (text2 != text)
				{
					double num2 = this.NormalizedStringCount + 1.0;
					this.NormalizedStringCount = num2;
				}
				if (string.IsNullOrEmpty(text2))
				{
					if (text2 == null)
					{
						double num2 = this.NullValueCount + 1.0;
						this.NullValueCount = num2;
					}
					else
					{
						double num2 = this.EmptyStringCount + 1.0;
						this.EmptyStringCount = num2;
					}
				}
				else
				{
					if (text.Length > 256)
					{
						IRichDataType richDataType = this._candidateTypes.FirstOrDefault((IRichDataType t) => t is RichStringType);
						return ((richDataType != null) ? richDataType.Yield<IRichDataType>() : null) ?? Enumerable.Empty<IRichDataType>();
					}
					this.MeanInputLength += (double)text.Length;
					this.InputLengthStdDev += (double)(text.Length * text.Length);
					this.MaxInputLength = Math.Max(this.MaxInputLength, (double)text.Length);
					this.MinInputLength = Math.Min(this.MinInputLength, (double)text.Length);
					flag = false;
					foreach (IRichDataType richDataType2 in this._candidateTypes)
					{
						richDataType2.AddSample(text2);
					}
					List<IRichDataType> candidateTypes = this._candidateTypes;
					Predicate<IRichDataType> predicate;
					if ((predicate = <>9__5) == null)
					{
						predicate = (<>9__5 = delegate(IRichDataType c)
						{
							if (c.EarlyFailure || c.RejectionCount >= this.RejectionThreshold)
							{
								return true;
							}
							if (trueDatasetSize != null)
							{
								long minRequiredSamplesForSuccess = c.MinRequiredSamplesForSuccess;
								long? trueDatasetSize2 = trueDatasetSize;
								return (minRequiredSamplesForSuccess > trueDatasetSize2.GetValueOrDefault()) & (trueDatasetSize2 != null);
							}
							return false;
						});
					}
					candidateTypes.RemoveAll(predicate);
				}
			}
			foreach (IRichDataType richDataType3 in this._candidateTypes)
			{
				richDataType3.Finish(trueDatasetSize ?? num);
			}
			stopwatch.Stop();
			this.NumSamplesProcessed = (double)num;
			this.MeanInputLength = ((num > 0L) ? (this.MeanInputLength / (double)num) : 0.0);
			this.InputLengthStdDev = ((num > 0L) ? (this.InputLengthStdDev / (double)num - this.MeanInputLength * this.MeanInputLength) : 0.0);
			this.InputLengthStdDev = Math.Sqrt(this.InputLengthStdDev);
			List<IRichDataType> list = (flag ? Enumerable.Empty<IRichDataType>().ToList<IRichDataType>() : this._candidateTypes.Where((IRichDataType c) => c.RejectionCount < this.RejectionThreshold && c.SuccessOnFinish && (double)c.RejectionCount / (double)c.SampleCount < this.MaxRejectionFraction).ToList<IRichDataType>());
			if (this.EmptyStringCount > 0.0)
			{
				list.ForEach(delegate(IRichDataType t)
				{
					t.EmptyStringsExpectedInData = true;
				});
			}
			if (this.NormalizedStringCount > 0.0)
			{
				list.ForEach(delegate(IRichDataType t)
				{
					t.NormalizableStringsExpectedInData = true;
				});
			}
			if (this.NullValueCount > 0.0)
			{
				list.ForEach(delegate(IRichDataType t)
				{
					t.NullsExpectedInData = true;
				});
			}
			this.LogLearnEvent(logger, correlationId, list, stopwatch.Elapsed);
			return list;
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x000D201C File Offset: 0x000D021C
		private void LogLearnEvent(ILogger logger, Guid? correlationId, IReadOnlyList<IRichDataType> result, TimeSpan elapsedTime)
		{
			if (logger == null)
			{
				return;
			}
			if (!result.Any<IRichDataType>())
			{
				return;
			}
			Guid guid = correlationId ?? Guid.Empty;
			string text = "LearnTopK";
			IReadOnlyCollection<KeyValuePair<string, double>> readOnlyCollection = new KeyValuePair<string, double>[]
			{
				KVP.Create<string, double>("LearnTime", elapsedTime.TotalMilliseconds),
				KVP.Create<string, double>("MeanInputLength", this.MeanInputLength),
				KVP.Create<string, double>("StdDevInputLength", this.InputLengthStdDev),
				KVP.Create<string, double>("NumInputs", this.NumSamplesProcessed),
				KVP.Create<string, double>("MaxInputLength", this.MaxInputLength),
				KVP.Create<string, double>("MinInputLength", this.MinInputLength),
				KVP.Create<string, double>("NumNullInputs", this.NullValueCount),
				KVP.Create<string, double>("NumEmptyInputs", this.EmptyStringCount)
			};
			KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[3];
			array[0] = KVP.Create<string, string>("SessionType", "Detection.Datatype.RichDataTypeDetection");
			array[1] = KVP.Create<string, string>("Id", guid.ToString());
			int num = 2;
			string text2 = "DetectedType";
			IRichDataType richDataType = result.FirstOrDefault<IRichDataType>();
			array[num] = KVP.Create<string, string>(text2, ((richDataType != null) ? richDataType.Kind.ToString() : null) ?? "null");
			logger.TrackEvent(text, readOnlyCollection, array, new KeyValuePair<string, string>[0]);
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x000D2194 File Offset: 0x000D0394
		private static IRichDataType ChooseBetween(RichDateType dateType, RichNumericType numericType)
		{
			if (dateType.HasDate)
			{
				return dateType;
			}
			if (numericType.MaxValue.HasValue && numericType.MinValue.HasValue && numericType.MaxValue.Value - numericType.MinValue.Value < 60L)
			{
				return numericType;
			}
			return dateType;
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x000D21FC File Offset: 0x000D03FC
		public IRichDataType Detect(IEnumerable<string> samples, IRichDataType defaultType = null, long? trueDatasetSize = null, ILogger logger = null, Guid? correlationId = null)
		{
			IRichDataType richDataType = this._Detect(samples, defaultType, trueDatasetSize, logger, correlationId);
			RichDateType richDateType = richDataType as RichDateType;
			if (richDateType != null)
			{
				richDateType.FixOneInterpretation();
			}
			else
			{
				RichNumericType richNumericType = richDataType as RichNumericType;
				if (richNumericType != null)
				{
					richNumericType.FixOneInterpretation();
				}
			}
			return richDataType;
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x000D223C File Offset: 0x000D043C
		private IRichDataType _Detect(IEnumerable<string> samples, IRichDataType defaultType = null, long? trueDatasetSize = null, ILogger logger = null, Guid? correlationId = null)
		{
			List<IRichDataType> list = (from t in this.DetectAll(samples, trueDatasetSize, logger, correlationId)
				orderby t.NaValueCount
				select t).ToList<IRichDataType>();
			if (!list.Any<IRichDataType>())
			{
				return defaultType;
			}
			if (list.Count == 1)
			{
				return list.Single<IRichDataType>();
			}
			list.OfType<RichStringType>().FirstOrDefault<RichStringType>();
			RichCategoricalType richCategoricalType = list.OfType<RichCategoricalType>().FirstOrDefault<RichCategoricalType>();
			RichNumericType richNumericType = list.OfType<RichNumericType>().FirstOrDefault<RichNumericType>();
			RichDateType richDateType = list.OfType<RichDateType>().FirstOrDefault<RichDateType>();
			RichBooleanType richBooleanType = list.OfType<RichBooleanType>().FirstOrDefault<RichBooleanType>();
			if (richBooleanType != null)
			{
				return richBooleanType;
			}
			if (richDateType != null)
			{
				richDateType.FilterOutVarianceCheckFailures(samples);
				if (richNumericType == null)
				{
					return richDateType;
				}
				return RichDataTypeDetector.ChooseBetween(richDateType, richNumericType);
			}
			else
			{
				if (richNumericType == null)
				{
					return richCategoricalType;
				}
				if (richCategoricalType != null && richNumericType.IsNaturalNumber)
				{
					return richCategoricalType;
				}
				return richNumericType;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06004327 RID: 17191 RVA: 0x000D2307 File Offset: 0x000D0507
		public static RichDataTypeDetector Default
		{
			get
			{
				return RichDataTypeDetector.BuildDefaultInstance();
			}
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x000D230E File Offset: 0x000D050E
		private static RichDataTypeDetector BuildDefaultInstance()
		{
			return new RichDataTypeDetector(new IRichDataType[]
			{
				new RichBooleanType(),
				new RichDateType(),
				new RichNumericType(),
				new RichStringType()
			}, 1, 0.001);
		}

		// Token: 0x04001E37 RID: 7735
		private readonly List<IRichDataType> _candidateTypes;

		// Token: 0x04001E3A RID: 7738
		public const int DefaultRejectionThreshold = 1;

		// Token: 0x04001E3B RID: 7739
		public const double DefaultMaxRejectionFraction = 0.001;

		// Token: 0x04001E44 RID: 7748
		private bool _usedOnce;

		// Token: 0x04001E45 RID: 7749
		private const int MaxSampleStringLength = 256;

		// Token: 0x04001E46 RID: 7750
		private const string SessionTypeForLogging = "Detection.Datatype.RichDataTypeDetection";
	}
}
