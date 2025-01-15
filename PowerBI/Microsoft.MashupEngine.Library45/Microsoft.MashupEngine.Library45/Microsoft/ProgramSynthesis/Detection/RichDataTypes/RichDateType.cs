using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.FormatParsing;
using Microsoft.ProgramSynthesis.FormatParsing.Dates;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000A8C RID: 2700
	public class RichDateType : RichDataType<SyntacticDateType>
	{
		// Token: 0x06004359 RID: 17241 RVA: 0x000D2B5F File Offset: 0x000D0D5F
		public RichDateType()
			: base(DataKind.DateTime)
		{
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x0600435A RID: 17242 RVA: 0x000D2B68 File Offset: 0x000D0D68
		public override DataKind Kind
		{
			get
			{
				List<DataKind> list = (from t in base.TypeClusters.SelectMany((SyntacticTypeOptionSet<SyntacticDateType> t) => t)
					select t.Kind).Distinct<DataKind>().ToList<DataKind>();
				if (list.All((DataKind k) => k == DataKind.Time))
				{
					return DataKind.Time;
				}
				if (list.All((DataKind k) => k == DataKind.Date))
				{
					return DataKind.Date;
				}
				return DataKind.DateTime;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x0600435B RID: 17243 RVA: 0x000D2C21 File Offset: 0x000D0E21
		private IEnumerable<SyntacticDateType> AllSyntacticTypes
		{
			get
			{
				return base.TypeClusters.SelectMany((SyntacticTypeOptionSet<SyntacticDateType> t) => t);
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x0600435C RID: 17244 RVA: 0x000D2C4D File Offset: 0x000D0E4D
		public bool HasDate
		{
			get
			{
				return this.AllSyntacticTypes.Any((SyntacticDateType t) => t.HasDate);
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x0600435D RID: 17245 RVA: 0x000D2C79 File Offset: 0x000D0E79
		public bool HasTime
		{
			get
			{
				return this.AllSyntacticTypes.Any((SyntacticDateType t) => t.HasTime);
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x0600435E RID: 17246 RVA: 0x000D2CA5 File Offset: 0x000D0EA5
		public bool HasDateTime
		{
			get
			{
				return this.HasDate && this.HasTime;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x0600435F RID: 17247 RVA: 0x000D2CB7 File Offset: 0x000D0EB7
		public bool HasSeconds
		{
			get
			{
				return this.AllSyntacticTypes.Any((SyntacticDateType t) => t.HasSeconds);
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06004360 RID: 17248 RVA: 0x000D2CE3 File Offset: 0x000D0EE3
		public bool HasMilliSeconds
		{
			get
			{
				return this.AllSyntacticTypes.Any((SyntacticDateType t) => t.HasMilliseconds);
			}
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x000D2D10 File Offset: 0x000D0F10
		public override Optional<object> MaybeCastAsType(string value)
		{
			return this.AllSyntacticTypes.FirstValue((SyntacticDateType type) => type.MaybeCastAsType(value)).Cast<object>();
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x000D2D4C File Offset: 0x000D0F4C
		public override bool Equals(RichDataType<SyntacticDateType> other)
		{
			if (other == this)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			RichDateType richDateType = other as RichDateType;
			if (richDateType == null)
			{
				return false;
			}
			return richDateType.TypeClusters.SelectMany((SyntacticTypeOptionSet<SyntacticDateType> t) => t).ConvertToHashSet<SyntacticDateType>().SetEquals(base.TypeClusters.SelectMany((SyntacticTypeOptionSet<SyntacticDateType> t) => t));
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x000D2DD0 File Offset: 0x000D0FD0
		private static bool ConflictsWithNumericFormat(string sample)
		{
			string text;
			string text2;
			sample = sample.UnquoteStringIfQuoted(out text, out text2);
			List<char> list = sample.Where((char c) => !char.IsDigit(c)).ToList<char>();
			if (list.Count != 1)
			{
				return false;
			}
			char c2 = list.Single<char>();
			return RichNumericType.AllNumericDelimiters.Contains(c2.ToString()) && sample.Split(new char[] { c2 })[1].Length == 1;
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06004364 RID: 17252 RVA: 0x000D2E58 File Offset: 0x000D1058
		private static HashSet<string> AllDateTimeNames
		{
			get
			{
				return RichDateType.AllDateTimeNamesLazy.Value;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06004365 RID: 17253 RVA: 0x000D2E64 File Offset: 0x000D1064
		private static Dictionary<string, string> NormalizingSubstitution
		{
			get
			{
				return RichDateType.NormalizingSubstitutionLazy.Value;
			}
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x000D2E70 File Offset: 0x000D1070
		protected override SyntacticTypeOptionSet<SyntacticDateType> ProcessSample(string sample)
		{
			if (RichDateType.ConflictsWithNumericFormat(sample))
			{
				base.EarlyFailure = true;
				return null;
			}
			Dictionary<string, string> substitutions = new Dictionary<string, string>();
			string sample2 = sample;
			string text;
			string text2;
			sample = sample.UnquoteStringIfQuoted(out text, out text2);
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				substitutions[text] = string.Empty;
				substitutions[text2] = string.Empty;
			}
			if (RichDateType.NormalizingSubstitution.Keys.Any(new Func<string, bool>(sample.Contains)))
			{
				sample = RichDateType.NormalizingSubstitution.Aggregate(sample, (string acc, KeyValuePair<string, string> s) => acc.Replace(s.Key, s.Value));
				foreach (KeyValuePair<string, string> keyValuePair in RichDateType.NormalizingSubstitution)
				{
					substitutions[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			FormatParser<DateTimeFormat, DateTimeFormatMatch, PartialDateTime, StringRegion> fullDateParser = RichDateType.FullDateParser;
			StringRegion stringRegion = new StringRegion(sample, Token.Tokens);
			List<DateTimeFormatMatch> list = (from m in fullDateParser.Parse(stringRegion, null).Distinct((DateTimeFormatMatch fm) => fm.DateTimeFormat)
				where (ulong)m.ParsedRegion.Length == (ulong)((long)m.ParsedRegion.Source.Length)
				select m into fm
				where fm.DateTimeFormat.Parse(sample).HasValue
				select fm).ToList<DateTimeFormatMatch>();
			if (list.Any<DateTimeFormatMatch>())
			{
				return SyntacticTypeOptionSet.From<SyntacticDateType>(list.Select((DateTimeFormatMatch f) => new SyntacticDateType(f.DateTimeFormat, substitutions)));
			}
			IEnumerable<SyntacticDateType> enumerable = this.ProcessNaValue(sample2);
			if (enumerable != null)
			{
				return SyntacticTypeOptionSet.From<SyntacticDateType>(enumerable);
			}
			return null;
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x000D3060 File Offset: 0x000D1260
		private IEnumerable<SyntacticDateType> ProcessNaValue(string sample)
		{
			if (!RichDateType.DigitsRegex.IsMatch(sample))
			{
				return new SyntacticDateType(sample).Yield<SyntacticDateType>();
			}
			return null;
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x000D307C File Offset: 0x000D127C
		private static bool HasPosix(SyntacticDateType typ)
		{
			DateTimeFormat format = typ.Format;
			return !string.IsNullOrEmpty((format != null) ? format.PosixParsingFormatString : null);
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x000D3098 File Offset: 0x000D1298
		protected override void FinishImpl(long numSamples)
		{
			base.SuccessOnFinish = base.AcceptanceCount > 0 && (double)base.NaValueCount < (double)numSamples * 0.1 && base.RejectionCount <= 0;
			if (base.SuccessOnFinish)
			{
				using (List<SyntacticTypeOptionSet<SyntacticDateType>>.Enumerator enumerator = base.TypeClusters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SyntacticTypeOptionSet<SyntacticDateType> typeCluster = enumerator.Current;
						typeCluster.Remove((SyntacticDateType typ) => !RichDateType.HasPosix(typ) && typ.Format != null && typeCluster.Options.Any((SyntacticDateType other) => RichDateType.HasPosix(other) && other.Format.MatchedParts.Equals(typ.Format.MatchedParts)));
					}
					return;
				}
			}
			base.TypeClusters.Clear();
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x0600436A RID: 17258 RVA: 0x000D314C File Offset: 0x000D134C
		private static DateTimeFormatParserBuilder FullDateParserBuilder
		{
			get
			{
				return RichDateType.FullDateParserBuilderLazy.Value;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x0600436B RID: 17259 RVA: 0x000D3158 File Offset: 0x000D1358
		private static FormatParser<DateTimeFormat, DateTimeFormatMatch, PartialDateTime, StringRegion> FullDateParser
		{
			get
			{
				return RichDateType.FullDateParserLazy.Value;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x000D3164 File Offset: 0x000D1364
		public override long MinRequiredSamplesForSuccess
		{
			get
			{
				return (long)((double)base.NaValueCount / 0.1);
			}
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x000D3178 File Offset: 0x000D1378
		private static DateTimeFormatParserBuilder ConstructFullDateParserBuilder()
		{
			DateTimeFormat dateTimeFormat = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("yy", null);
			DateTimeFormat dateTimeFormat2 = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("yyyy", null);
			DateTimeFormatParserBuilder dateTimeFormatParserBuilder = new DateTimeFormatParserBuilder();
			dateTimeFormatParserBuilder.AppendUnion(new DateTimeFormat[] { dateTimeFormat, dateTimeFormat2 }, null, null, false, 1, 1);
			DateTimeFormat dateTimeFormat3 = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("YY", null);
			DateTimeFormat dateTimeFormat4 = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("YYYY", null);
			new DateTimeFormatParserBuilder().AppendUnion(new DateTimeFormat[] { dateTimeFormat3, dateTimeFormat4 }, null, null, false, 1, 1);
			DateTimeFormat dateTimeFormat5 = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("M", DateTimeFormatPart.AllowLeadingZerosFormatAttributes);
			DateTimeFormat dateTimeFormat6 = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("MMM", null);
			DateTimeFormat dateTimeFormat7 = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("MMMM", null);
			DateTimeFormatParserBuilder dateTimeFormatParserBuilder2 = new DateTimeFormatParserBuilder();
			dateTimeFormatParserBuilder2.AppendUnion(new DateTimeFormat[] { dateTimeFormat5, dateTimeFormat7, dateTimeFormat6 }, null, null, false, 1, 1);
			DateTimeFormat dateTimeFormat8 = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("d", DateTimeFormatPart.AllowLeadingZerosFormatAttributes);
			DateTimeFormat dateTimeFormat9 = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("H", DateTimeFormatPart.AllowLeadingZerosAndDisallow24HourFormatAttributes);
			DateTimeFormat dateTimeFormat10 = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("H", DateTimeFormatPart.AllowLeadingZerosFormatAttributes);
			DateTimeFormat dateTimeFormat11 = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("h", DateTimeFormatPart.AllowLeadingZerosFormatAttributes);
			DateTimeFormatParserBuilder dateTimeFormatParserBuilder3 = new DateTimeFormatParserBuilder();
			dateTimeFormatParserBuilder3.AppendUnion(new DateTimeFormat[] { dateTimeFormat9, dateTimeFormat11, dateTimeFormat10 }, null, null, false, 1, 1);
			DateTimeFormat dateTimeFormat12 = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("m", DateTimeFormatPart.AllowLeadingZerosFormatAttributes);
			DateTimeFormatParserBuilder dateTimeFormatParserBuilder4 = new DateTimeFormatParserBuilder();
			FormatParserBuilder<DateTimeFormat, DateTimeFormatMatch, PartialDateTime, StringRegion, DateTimeSpacerFormatParser> formatParserBuilder = dateTimeFormatParserBuilder4;
			DateTimeFormat[] array = new DateTimeFormat[4];
			array[0] = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("tt", null);
			int num = 1;
			string text = "tt";
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["casing"] = "lower";
			array[num] = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1(text, new FormatAttributes(dictionary));
			array[2] = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("t", null);
			int num2 = 3;
			string text2 = "t";
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			dictionary2["casing"] = "lower";
			array[num2] = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1(text2, new FormatAttributes(dictionary2));
			formatParserBuilder.AppendUnion(array, null, null, false, 1, 1);
			DateTimeFormat dateTimeFormat13 = RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("s", DateTimeFormatPart.AllowLeadingZerosFormatAttributes);
			DateTimeFormatParserBuilder dateTimeFormatParserBuilder5 = new DateTimeFormatParserBuilder();
			dateTimeFormatParserBuilder5.AppendUnion(new DateTimeFormat[]
			{
				RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("f", null),
				RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("ff", null),
				RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("fff", null)
			}, null, null, false, 1, 1);
			DateTimeFormatParserBuilder dateTimeFormatParserBuilder6 = new DateTimeFormatParserBuilder();
			dateTimeFormatParserBuilder6.AppendUnion(new DateTimeFormat[]
			{
				RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("ddd", null),
				RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("dddd", null)
			}, null, null, false, 1, 1);
			new DateTimeFormatParserBuilder().AppendUnion(new DateTimeFormat[]
			{
				RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("MMM", null),
				RichDateType.<ConstructFullDateParserBuilder>g__CreateAtomic|37_1("MMMM", null)
			}, null, null, false, 1, 1);
			DateTimeFormatParserBuilder dateTimeFormatParserBuilder7 = new DateTimeFormatParserBuilder();
			dateTimeFormatParserBuilder7.Append(dateTimeFormatParserBuilder6, "day_of_week_1", null, false, 0, 1);
			dateTimeFormatParserBuilder7.AppendSpacer("day_of_week_1_delimiter", new DateTimeSpacerFormatParser(false, null, null, null, 1, 1, null), null);
			dateTimeFormatParserBuilder7.AssertDirectionalConstraint("day_of_week_1_delimiter", delegate(DateTimeFormatMatch delimiterMatch, IReadOnlyList<Optional<DateTimeFormatMatch>> otherMatches)
			{
				Optional<DateTimeFormatMatch> optional = otherMatches[0];
				if (!optional.HasValue || optional.Value.Region.Length == 0U)
				{
					return delimiterMatch.Region.Length == 0U;
				}
				return delimiterMatch.Region.Length > 0U;
			}, new string[] { "day_of_week_1" });
			dateTimeFormatParserBuilder7.Append(dateTimeFormatParserBuilder, null, null, true, 1, 1);
			dateTimeFormatParserBuilder7.AppendSpacer("date_delimiter_1", new DateTimeSpacerFormatParser(false, null, null, null, 1, 1, null), null);
			dateTimeFormatParserBuilder7.Append(dateTimeFormatParserBuilder2, null, null, true, 1, 1);
			dateTimeFormatParserBuilder7.AppendSpacer("date_delimiter_2", new DateTimeSpacerFormatParser(false, null, null, null, 1, 1, null), null);
			dateTimeFormatParserBuilder7.Append(dateTimeFormat8, null, null, true, 1, 1);
			dateTimeFormatParserBuilder7.AssertGroupConstraint((int index, DateTimeFormatMatch match, IReadOnlyList<Optional<DateTimeFormatMatch>> formatMatches) => DateTimeSpacerFormatParser.SpacerCompatibilityChecker(match, formatMatches[1 - index].Value), new string[] { "date_delimiter_1", "date_delimiter_2" });
			dateTimeFormatParserBuilder7.AppendSpacer("day_of_week_2_delimiter", new DateTimeSpacerFormatParser(false, null, null, null, 1, 1, null), null);
			dateTimeFormatParserBuilder7.Append(dateTimeFormatParserBuilder6, "day_of_week_2", null, false, 0, 1);
			dateTimeFormatParserBuilder7.AssertDirectionalConstraint("day_of_week_2", delegate(DateTimeFormatMatch delimiterMatch, IReadOnlyList<Optional<DateTimeFormatMatch>> otherMatches)
			{
				Optional<DateTimeFormatMatch> optional2 = otherMatches[0];
				return !optional2.HasValue || optional2.Value.Region.Length <= 0U;
			}, new string[] { "day_of_week_1" });
			DateTimeFormatParserBuilder dateTimeFormatParserBuilder8 = new DateTimeFormatParserBuilder();
			dateTimeFormatParserBuilder8.Append(dateTimeFormat13, null, null, false, 1, 1);
			dateTimeFormatParserBuilder8.AppendSpacer("seconds_milliseconds_delimiter", new DateTimeSpacerFormatParser(true, null, null, null, 1, 1, null), null);
			dateTimeFormatParserBuilder8.Append(dateTimeFormatParserBuilder5, null, null, false, 0, 1);
			DateTimeFormatParserBuilder dateTimeFormatParserBuilder9 = new DateTimeFormatParserBuilder();
			dateTimeFormatParserBuilder9.Append(dateTimeFormatParserBuilder3, null, null, false, 1, 1);
			dateTimeFormatParserBuilder9.AppendSpacer("time_delimiter_1", new DateTimeSpacerFormatParser(false, null, null, null, 1, 1, null), null);
			dateTimeFormatParserBuilder9.Append(dateTimeFormat12, null, null, false, 1, 1);
			dateTimeFormatParserBuilder9.AppendSpacer("time_delimiter_2", new DateTimeSpacerFormatParser(false, null, null, null, 1, 1, null), null);
			dateTimeFormatParserBuilder9.Append(dateTimeFormatParserBuilder8, "seconds_milliseconds_builder", null, false, 0, 1);
			dateTimeFormatParserBuilder9.AppendSpacer("time_delimiter_before_period", new DateTimeSpacerFormatParser(false, null, null, null, 1, 1, null), null);
			dateTimeFormatParserBuilder9.Append(dateTimeFormatParserBuilder4, null, null, false, 0, 1);
			dateTimeFormatParserBuilder9.AssertGroupConstraint((int index, DateTimeFormatMatch match, IReadOnlyList<Optional<DateTimeFormatMatch>> otherMatches) => DateTimeSpacerFormatParser.SpacerCompatibilityChecker(match, otherMatches[1 - index].Value), new string[] { "time_delimiter_1", "time_delimiter_2" });
			DateTimeFormatParserBuilder dateTimeFormatParserBuilder10 = new DateTimeFormatParserBuilder();
			dateTimeFormatParserBuilder10.Append(dateTimeFormatParserBuilder7, "date_builder", null, true, 0, 1);
			dateTimeFormatParserBuilder10.AppendSpacer("date_time_delimiter", new DateTimeSpacerFormatParser(false, null, null, null, 1, 1, null), null);
			dateTimeFormatParserBuilder10.Append(dateTimeFormatParserBuilder9, "time_builder", null, true, 0, 1);
			dateTimeFormatParserBuilder10.AssertGroupConstraint((int index, DateTimeFormatMatch match, IReadOnlyList<Optional<DateTimeFormatMatch>> otherMatches) => otherMatches[1 - index].Value.Region.Length != 0U || match.Region.Length > 0U, new string[] { "date_builder", "time_builder" });
			dateTimeFormatParserBuilder10.AssertGroupConstraint(delegate(int index, DateTimeFormatMatch match, IReadOnlyList<Optional<DateTimeFormatMatch>> otherMatches)
			{
				IEnumerable<DateTimeFormatMatch> enumerable = from idx in Enumerable.Range(0, index)
					select otherMatches[idx].OrElseDefault<DateTimeFormatMatch>();
				enumerable = enumerable.AppendItem(match);
				enumerable = enumerable.Concat(from idx in Enumerable.Range(index + 1, 4 - index - 1)
					select otherMatches[idx].OrElseDefault<DateTimeFormatMatch>()).Memoize<DateTimeFormatMatch>();
				return RichDateType.<ConstructFullDateParserBuilder>g__DistinctDelimiters|37_7(enumerable.Take(2).ToList<DateTimeFormatMatch>(), enumerable.Skip(2).ToList<DateTimeFormatMatch>());
			}, new string[] { "time_builder.time_delimiter_1", "time_builder.time_delimiter_2", "date_builder.date_delimiter_1", "date_builder.date_delimiter_2" });
			DateTimeFormatParserBuilder dateTimeFormatParserBuilder11 = new DateTimeFormatParserBuilder();
			dateTimeFormatParserBuilder11.Append(dateTimeFormatParserBuilder10, null, new Predicate<DeltaFormatMatchState<DateTimeFormatMatch, PartialDateTime, DateTimeFormat, StringRegion>>(RichDateType.<ConstructFullDateParserBuilder>g__NoNumericPaddingFilterPredicate|37_0), false, 1, 1);
			return dateTimeFormatParserBuilder11;
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x000D37A8 File Offset: 0x000D19A8
		public void FilterOutVarianceCheckFailures(IEnumerable<string> samples)
		{
			List<SyntacticTypeOptionSet<SyntacticDateType>> list = new List<SyntacticTypeOptionSet<SyntacticDateType>>();
			List<SyntacticTypeOptionSet<SyntacticDateType>> list2 = new List<SyntacticTypeOptionSet<SyntacticDateType>>();
			Func<SyntacticDateType, bool> <>9__1;
			foreach (SyntacticTypeOptionSet<SyntacticDateType> syntacticTypeOptionSet in base.TypeClusters)
			{
				IEnumerable<SyntacticDateType> enumerable = syntacticTypeOptionSet.Where((SyntacticDateType t) => t.HasDate);
				if (enumerable.Any<SyntacticDateType>() && enumerable.Skip(1).Any<SyntacticDateType>())
				{
					IEnumerable<SyntacticDateType> enumerable2 = enumerable;
					Func<SyntacticDateType, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (SyntacticDateType t) => t.IsConsistentWithVariance(samples));
					}
					enumerable = enumerable2.Where(func);
					if (enumerable.Any<SyntacticDateType>())
					{
						IEnumerable<SyntacticDateType> enumerable3 = syntacticTypeOptionSet.Where((SyntacticDateType t) => !t.HasDate);
						list.Add(syntacticTypeOptionSet);
						list2.Add(SyntacticTypeOptionSet.From<SyntacticDateType>(enumerable.Concat(enumerable3)));
					}
				}
			}
			foreach (SyntacticTypeOptionSet<SyntacticDateType> syntacticTypeOptionSet2 in list)
			{
				base.TypeClusters.Remove(syntacticTypeOptionSet2);
			}
			foreach (SyntacticTypeOptionSet<SyntacticDateType> syntacticTypeOptionSet3 in list2)
			{
				base.TypeClusters.Add(syntacticTypeOptionSet3);
			}
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x000D39DC File Offset: 0x000D1BDC
		[CompilerGenerated]
		internal static bool <ConstructFullDateParserBuilder>g__NoNumericPaddingFilterPredicate|37_0(DeltaFormatMatchState<DateTimeFormatMatch, PartialDateTime, DateTimeFormat, StringRegion> delta)
		{
			if (!delta.CumulativeParse.HasValue || delta.CumulativeParse.Value.ParsedRegion.Length == 0U)
			{
				return false;
			}
			DateTimeFormatMatch value = delta.CumulativeParse.Value;
			List<DateTimeFormatPart> list = value.DateTimeFormat.FormatParts.Where((DateTimeFormatPart p) => p.MinimumLength > 0).ToList<DateTimeFormatPart>();
			return (list.Count < 1 || !(list.First<DateTimeFormatPart>() is NumericDateTimeFormatPart) || value.Region.Start <= 0U || !char.IsDigit(value.Region.Source[(int)(value.Region.Start - 1U)])) && (list.Count <= 1 || !(list.Last<DateTimeFormatPart>() is NumericDateTimeFormatPart) || (ulong)value.Region.End >= (ulong)((long)value.Region.Source.Length) || !char.IsDigit(value.Region.Source[(int)value.Region.End]));
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x000D3AFD File Offset: 0x000D1CFD
		[CompilerGenerated]
		internal static DateTimeFormat <ConstructFullDateParserBuilder>g__CreateAtomic|37_1(string formatString, FormatAttributes attributes = null)
		{
			return new DateTimeFormat(new DateTimeFormatPart[] { DateTimeFormatPart.Create(formatString, attributes) });
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x000D3B14 File Offset: 0x000D1D14
		[CompilerGenerated]
		internal static bool <ConstructFullDateParserBuilder>g__DistinctDelimiters|37_7(IReadOnlyList<DateTimeFormatMatch> timeDelimiters, IReadOnlyList<DateTimeFormatMatch> dateDelimiters)
		{
			foreach (DateTimeFormatMatch dateTimeFormatMatch in dateDelimiters.Where((DateTimeFormatMatch m) => m != null))
			{
				foreach (DateTimeFormatMatch dateTimeFormatMatch2 in timeDelimiters.Where((DateTimeFormatMatch m) => m != null))
				{
					if ((!string.IsNullOrWhiteSpace(dateTimeFormatMatch2.Region.Value) || !string.IsNullOrWhiteSpace(dateTimeFormatMatch.Region.Value)) && dateTimeFormatMatch2.Region.Value == dateTimeFormatMatch.Region.Value)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04001E64 RID: 7780
		private static readonly Lazy<HashSet<string>> AllDateTimeNamesLazy = new Lazy<HashSet<string>>(() => new string[][]
		{
			CultureInfo.InvariantCulture.DateTimeFormat.AbbreviatedDayNames,
			CultureInfo.InvariantCulture.DateTimeFormat.AbbreviatedMonthNames,
			CultureInfo.InvariantCulture.DateTimeFormat.DayNames,
			CultureInfo.InvariantCulture.DateTimeFormat.MonthNames
		}.SelectMany((string[] s) => s).ConvertToHashSet<string>());

		// Token: 0x04001E65 RID: 7781
		private static readonly Lazy<Dictionary<string, string>> NormalizingSubstitutionLazy = new Lazy<Dictionary<string, string>>(() => (from kvp in RichDateType.AllDateTimeNames.SelectMany((string s) => new KeyValuePair<string, string>[]
			{
				KVP.Create<string, string>(s.ToUpper(), s),
				KVP.Create<string, string>(s.ToLower(), s)
			})
			where kvp.Key != kvp.Value
			select kvp).ToDictionary<string, string>());

		// Token: 0x04001E66 RID: 7782
		private static readonly Regex DigitsRegex = new Regex("\\d", RegexOptions.Compiled);

		// Token: 0x04001E67 RID: 7783
		private static readonly Lazy<DateTimeFormatParserBuilder> FullDateParserBuilderLazy = new Lazy<DateTimeFormatParserBuilder>(new Func<DateTimeFormatParserBuilder>(RichDateType.ConstructFullDateParserBuilder), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04001E68 RID: 7784
		private static readonly Lazy<FormatParser<DateTimeFormat, DateTimeFormatMatch, PartialDateTime, StringRegion>> FullDateParserLazy = new Lazy<FormatParser<DateTimeFormat, DateTimeFormatMatch, PartialDateTime, StringRegion>>(() => RichDateType.FullDateParserBuilder.Build(), LazyThreadSafetyMode.ExecutionAndPublication);
	}
}
