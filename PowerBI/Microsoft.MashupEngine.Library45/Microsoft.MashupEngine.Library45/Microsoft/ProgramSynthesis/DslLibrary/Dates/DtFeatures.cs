using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000855 RID: 2133
	public class DtFeatures
	{
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x00084D4D File Offset: 0x00082F4D
		public DateTimeFormat DtFormat { get; }

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x00084D55 File Offset: 0x00082F55
		public double ConstantLength { get; }

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06002E5C RID: 11868 RVA: 0x00084D5D File Offset: 0x00082F5D
		public double DigitConstantLength { get; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x00084D65 File Offset: 0x00082F65
		public double HasOneDecimalPoint { get; }

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x00084D6D File Offset: 0x00082F6D
		public double SeparatorCount { get; }

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x00084D75 File Offset: 0x00082F75
		public double SeparatorKindMatches { get; }

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002E60 RID: 11872 RVA: 0x00084D7D File Offset: 0x00082F7D
		public double SeparatorKindMisMatches { get; }

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x00084D85 File Offset: 0x00082F85
		public double UnlikelySeparatorCount { get; }

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002E62 RID: 11874 RVA: 0x00084D8D File Offset: 0x00082F8D
		public double HasNonDelimitedNumbers { get; }

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x00084D95 File Offset: 0x00082F95
		public double IsNumeric { get; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002E64 RID: 11876 RVA: 0x00084D9D File Offset: 0x00082F9D
		public double IsMatchingCommonDatePartsOrders { get; }

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x00084DA5 File Offset: 0x00082FA5
		public double DatePartOrderCount { get; }

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002E66 RID: 11878 RVA: 0x00084DAD File Offset: 0x00082FAD
		public double MinDateInversions { get; }

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x00084DB5 File Offset: 0x00082FB5
		public double IsMatchingCommonTimePartsOrders { get; }

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002E68 RID: 11880 RVA: 0x00084DBD File Offset: 0x00082FBD
		public double TimePartOrderCount { get; }

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x00084DC5 File Offset: 0x00082FC5
		public double MinTimeInversions { get; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002E6A RID: 11882 RVA: 0x00084DCD File Offset: 0x00082FCD
		public double TimeBeforeDate { get; }

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002E6B RID: 11883 RVA: 0x00084DD5 File Offset: 0x00082FD5
		public double PeriodWithFullHour { get; }

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002E6C RID: 11884 RVA: 0x00084DDD File Offset: 0x00082FDD
		public double VariableLengthCount { get; }

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x00084DE5 File Offset: 0x00082FE5
		public double VariableLengthPenalty { get; }

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x00084DED File Offset: 0x00082FED
		public double MatchedPartsCount { get; }

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x00084DF5 File Offset: 0x00082FF5
		public double TimeAndDateShareSeparator { get; }

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002E70 RID: 11888 RVA: 0x00084DFD File Offset: 0x00082FFD
		public double BetweenTimeDateSeparatorReused { get; }

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x00084E05 File Offset: 0x00083005
		public double HasDayOfWeekInMonth { get; }

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x00084E0D File Offset: 0x0008300D
		public double HasUnorderedTimeFormatParts { get; }

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x00084E15 File Offset: 0x00083015
		public double HasUnlikelyConstants { get; }

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x00084E1D File Offset: 0x0008301D
		public double UnlikelyFormatPartsCount { get; }

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x00084E25 File Offset: 0x00083025
		public double UnlikelyFormatCombinationsCount { get; }

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x00084E2D File Offset: 0x0008302D
		public double HasUnorderedDateFormatParts { get; }

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002E77 RID: 11895 RVA: 0x00084E35 File Offset: 0x00083035
		public double HasPartialDate { get; }

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x00084E3D File Offset: 0x0008303D
		public double PeriodWithoutHour { get; }

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002E79 RID: 11897 RVA: 0x00084E45 File Offset: 0x00083045
		public double ConstantAtStartLength { get; }

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x00084E4D File Offset: 0x0008304D
		public double ConstantAtEndLength { get; }

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x00084E55 File Offset: 0x00083055
		public double RepeatPartCount { get; }

		// Token: 0x06002E7C RID: 11900 RVA: 0x00084E60 File Offset: 0x00083060
		private bool IsUnlikelyConstant(string s)
		{
			if (DtFeatures._unlikelyConstants == null)
			{
				DtFeatures._unlikelyConstants = (from x in new IEnumerable<string>[]
					{
						(DateTimeFormatPart.Create("MMM", null) as StringDateTimeFormatPart).AllValues,
						(DateTimeFormatPart.Create("MMMM", null) as StringDateTimeFormatPart).AllValues,
						(DateTimeFormatPart.Create("ddd", null) as StringDateTimeFormatPart).AllValues,
						(DateTimeFormatPart.Create("dddd", null) as StringDateTimeFormatPart).AllValues,
						(DateTimeFormatPart.Create("t", null) as StringDateTimeFormatPart).AllValues,
						(DateTimeFormatPart.Create("tt", null) as StringDateTimeFormatPart).AllValues
					}.SelectMany((IEnumerable<string> x) => x)
					select new Regex("\\b" + x + "\\b", RegexOptions.Compiled)).ToList<Regex>();
			}
			return DtFeatures._unlikelyConstants.Any((Regex sk) => sk.IsMatch(s));
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x00084F82 File Offset: 0x00083182
		public double Score
		{
			get
			{
				if (this._score == null)
				{
					this._score = new double?(this.GetScore());
				}
				return this._score.Value;
			}
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x00084FB0 File Offset: 0x000831B0
		public DtFeatures(DateTimeFormat dtFormat, bool completionFeatures = false, bool avoidImperialDateTimeFormat = false)
		{
			this.DtFormat = dtFormat;
			this.ConstantLength = (double)dtFormat.FormatParts.OfType<ConstantDateTimeFormatPart>().Sum((ConstantDateTimeFormatPart c) => c.ConstantString.Length);
			this.DigitConstantLength = (double)dtFormat.FormatParts.OfType<ConstantDateTimeFormatPart>().Sum(delegate(ConstantDateTimeFormatPart c)
			{
				IEnumerable<char> constantString2 = c.ConstantString;
				Func<char, bool> func;
				if ((func = DtFeatures.<>O.<0>__IsDigit) == null)
				{
					func = (DtFeatures.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
				}
				return constantString2.Count(func);
			});
			this.HasOneDecimalPoint = dtFormat.FormatParts.OfType<ConstantDateTimeFormatPart>().Count((ConstantDateTimeFormatPart c) => c.ConstantString == ".") == 1;
			this.HasDayOfWeekInMonth = dtFormat.MatchedParts.Contains(DateTimePart.DayOfWeekInMonth) > false;
			this.SeparatorCount = 0.0;
			this.UnlikelySeparatorCount = 0.0;
			this.SeparatorKindMatches = 0.0;
			this.SeparatorKindMisMatches = 0.0;
			Dictionary<DtFeatures.DateTimeSeparatorKind, List<string>> dictionary = Enum.GetValues(typeof(DtFeatures.DateTimeSeparatorKind)).Cast<DtFeatures.DateTimeSeparatorKind>().ToDictionary((DtFeatures.DateTimeSeparatorKind k) => k, (DtFeatures.DateTimeSeparatorKind _) => new List<string>());
			for (int i = 1; i < dtFormat.FormatParts.Count - 1; i++)
			{
				ConstantDateTimeFormatPart constantDateTimeFormatPart = dtFormat.FormatParts[i] as ConstantDateTimeFormatPart;
				if (constantDateTimeFormatPart != null)
				{
					double num = this.SeparatorCount;
					this.SeparatorCount = num + 1.0;
					Optional<DateTimePart> matchedPart = dtFormat.FormatParts[i - 1].MatchedPart;
					Optional<DateTimePart> matchedPart2 = dtFormat.FormatParts[i + 1].MatchedPart;
					string constantString = constantDateTimeFormatPart.ConstantString;
					if (matchedPart.HasValue && matchedPart2.HasValue)
					{
						DtFeatures.DateTimeSeparatorKind expectedDateTimeSeparatorKind = DtFeatures.GetExpectedDateTimeSeparatorKind(matchedPart.Value, matchedPart2.Value);
						dictionary[expectedDateTimeSeparatorKind].Add(constantString);
						bool? flag = DtFeatures.IsCommonDateTimeSeparator(constantString, expectedDateTimeSeparatorKind);
						if (flag == null)
						{
							if (constantString == "+")
							{
								num = this.UnlikelySeparatorCount;
								this.UnlikelySeparatorCount = num + 1.0;
							}
						}
						else if (flag.Value)
						{
							num = this.SeparatorKindMatches;
							this.SeparatorKindMatches = num + 1.0;
						}
						else
						{
							num = this.SeparatorKindMisMatches;
							this.SeparatorKindMisMatches = num + 1.0;
						}
					}
				}
			}
			this.TimeAndDateShareSeparator = 0.0;
			this.BetweenTimeDateSeparatorReused = 0.0;
			if (this.SeparatorCount != 0.0 && dtFormat.MatchedParts.Intersect(DateTimePartSet.DateParts).Any() && dtFormat.MatchedParts.Intersect(DateTimePartSet.TimeParts).Any())
			{
				if (dictionary[DtFeatures.DateTimeSeparatorKind.Time].Intersect(dictionary[DtFeatures.DateTimeSeparatorKind.Date]).Any<string>())
				{
					this.TimeAndDateShareSeparator = 1.0;
				}
				string betweenTimeAndDate = dictionary[DtFeatures.DateTimeSeparatorKind.BetweenTimeDate].FirstOrDefault<string>();
				if (!string.IsNullOrWhiteSpace(betweenTimeAndDate) && dictionary.Any((KeyValuePair<DtFeatures.DateTimeSeparatorKind, List<string>> kv) => kv.Key != DtFeatures.DateTimeSeparatorKind.BetweenTimeDate && kv.Value.Contains(betweenTimeAndDate)))
				{
					this.BetweenTimeDateSeparatorReused = 1.0;
				}
			}
			this.HasNonDelimitedNumbers = dtFormat.HasNonDelimitedNumbers() > false;
			bool flag2 = dtFormat.FormatParts.IsNumericIncludingAtEnd();
			this.IsNumeric = flag2 > false;
			IReadOnlyList<DateTimePart> partOrder = dtFormat.FormatParts.SelectMany((DateTimeFormatPart fp) => fp.MatchedPart.AsEnumerable<DateTimePart>()).ToList<DateTimePart>();
			IReadOnlyList<DateTimePart> readOnlyList = partOrder.Where((DateTimePart p) => p.GetKind() == DateTimePartKind.Date).ToList<DateTimePart>();
			this.DatePartOrderCount = (double)readOnlyList.Count;
			IReadOnlyList<DtFeatures.DateTimePartOrder> readOnlyList2 = DtFeatures.GetMatchingCommonOrders(DtFeatures.CommonDatePartOrders, readOnlyList, avoidImperialDateTimeFormat, dictionary[DtFeatures.DateTimeSeparatorKind.Date]).ToList<DtFeatures.DateTimePartOrder>();
			if (readOnlyList2.Any<DtFeatures.DateTimePartOrder>())
			{
				this.IsMatchingCommonDatePartsOrders = 1.0;
				this.MinDateInversions = (double)readOnlyList2.Min((DtFeatures.DateTimePartOrder commonOrder) => commonOrder.CountInversions(partOrder));
			}
			else
			{
				this.IsMatchingCommonDatePartsOrders = 0.0;
				this.MinDateInversions = this.DatePartOrderCount;
			}
			IReadOnlyList<DateTimePart> readOnlyList3 = partOrder.Where((DateTimePart p) => p.GetKind() == DateTimePartKind.Time).ToList<DateTimePart>();
			this.TimePartOrderCount = (double)readOnlyList3.Count;
			IReadOnlyList<DtFeatures.DateTimePartOrder> readOnlyList4 = DtFeatures.GetMatchingCommonOrders(DtFeatures.CommonTimePartOrders, readOnlyList3, false, null).ToList<DtFeatures.DateTimePartOrder>();
			if (readOnlyList4.Any<DtFeatures.DateTimePartOrder>())
			{
				this.IsMatchingCommonTimePartsOrders = 1.0;
				this.MinTimeInversions = (double)readOnlyList4.Min((DtFeatures.DateTimePartOrder commonOrder) => commonOrder.CountInversions(partOrder));
			}
			else
			{
				this.IsMatchingCommonTimePartsOrders = 0.0;
				this.MinTimeInversions = this.TimePartOrderCount;
			}
			this.TimeBeforeDate = (double)((partOrder.First<DateTimePart>().GetKind() == DateTimePartKind.Time && partOrder.Last<DateTimePart>().GetKind() == DateTimePartKind.Date) ? 1 : 0);
			this.PeriodWithFullHour = dtFormat.MatchedParts.Contains(DateTimePart.Hour, DateTimePart.Period) > false;
			this.VariableLengthCount = 0.0;
			this.VariableLengthPenalty = 0.0;
			if (dtFormat.HasNonDelimitedNumbers())
			{
				this.VariableLengthPenalty += 100.0;
			}
			for (int j = 0; j < dtFormat.FormatParts.Count; j++)
			{
				DateTimeFormatPart dateTimeFormatPart = dtFormat.FormatParts[j];
				if (dateTimeFormatPart.MatchedPart.HasValue)
				{
					DateTimeFormatPart dateTimeFormatPart2 = ((j == 0) ? null : dtFormat.FormatParts[j - 1]);
					bool flag3 = dateTimeFormatPart.IsNumeric && dateTimeFormatPart.MinimumLength != dateTimeFormatPart.MaximumLength;
					if (flag3 && dateTimeFormatPart.MatchedPart.Value == DateTimePart.Year)
					{
						double num = this.VariableLengthCount;
						this.VariableLengthCount = num + 1.0;
						num = this.VariableLengthPenalty;
						this.VariableLengthPenalty = num + 1.0;
					}
					else
					{
						bool flag4 = dateTimeFormatPart2 != null && dateTimeFormatPart2.IsNumeric;
						bool flag5 = dateTimeFormatPart2 is ConstantDateTimeFormatPart;
						ConstantDateTimeFormatPart constantDateTimeFormatPart2 = dateTimeFormatPart2 as ConstantDateTimeFormatPart;
						char? c2 = ((constantDateTimeFormatPart2 != null) ? constantDateTimeFormatPart2.ConstantString.MaybeLastChar().OrElseNull<char>() : null);
						if (dateTimeFormatPart2 == null || (flag5 && ('/' == c2.Value || char.IsWhiteSpace(c2.Value))))
						{
							if (flag3 && flag2 && dtFormat.FormatParts.Count > 1)
							{
								double num = this.VariableLengthCount;
								this.VariableLengthCount = num + 1.0;
								this.VariableLengthPenalty += 5.0;
							}
							else if (!flag3 && (!flag2 || dtFormat.FormatParts.Count == 1))
							{
								double num = this.VariableLengthCount;
								this.VariableLengthCount = num + 1.0;
								num = this.VariableLengthPenalty;
								this.VariableLengthPenalty = num + 1.0;
							}
						}
						else if (flag4 && flag3)
						{
							double num = this.VariableLengthCount;
							this.VariableLengthCount = num + 1.0;
							this.VariableLengthPenalty += 5.0;
						}
						else if (flag3)
						{
							double num = this.VariableLengthCount;
							this.VariableLengthCount = num + 1.0;
							num = this.VariableLengthPenalty;
							this.VariableLengthPenalty = num + 1.0;
						}
					}
				}
			}
			this.MatchedPartsCount = dtFormat.MatchedParts.Count();
			if (completionFeatures)
			{
				this.HasUnlikelyConstants = (from cp in dtFormat.AllFormatParts.OfType<ConstantDateTimeFormatPart>()
					select cp.ConstantString).Any(new Func<string, bool>(this.IsUnlikelyConstant)) > false;
				this.UnlikelyFormatPartsCount = (double)dtFormat.AllFormatParts.Count((DateTimeFormatPart x) => DtFeatures.UnlikelyFormatParts.Contains(x.BaseFormatString));
				Func<string, bool> <>9__23;
				this.UnlikelyFormatCombinationsCount = (double)DtFeatures.UnlikelyFormatCombinations.Count(delegate(IReadOnlyList<string> combination)
				{
					Func<string, bool> func2;
					if ((func2 = <>9__23) == null)
					{
						func2 = (<>9__23 = (string pt) => dtFormat.AllFormatParts.Any((DateTimeFormatPart dtfp) => pt == dtfp.BaseFormatString));
					}
					return combination.All(func2);
				});
				IReadOnlyList<int> readOnlyList5 = (from x in dtFormat.AllFormatParts.Where((DateTimeFormatPart dtfp) => dtfp.MatchedPart.HasValue && dtfp.MatchedPart.Value.GetKind() == DateTimePartKind.Time).Select(delegate(DateTimeFormatPart dtfp)
					{
						int num3;
						switch (dtfp.MatchedPart.Value)
						{
						case DateTimePart.Hour:
							num3 = 1;
							break;
						case DateTimePart.Minute:
							num3 = 2;
							break;
						case DateTimePart.Second:
							num3 = 3;
							break;
						case DateTimePart.Millisecond:
							num3 = 4;
							break;
						case DateTimePart.HourInPeriod:
							num3 = 1;
							break;
						case DateTimePart.Period:
							num3 = 5;
							break;
						default:
							num3 = -1;
							break;
						}
						return num3;
					})
					where x != -1
					select x).ToList<int>();
				this.HasUnorderedTimeFormatParts = !readOnlyList5.IsSorted<int>();
				IReadOnlyList<int> readOnlyList6 = (from x in dtFormat.AllFormatParts.Where((DateTimeFormatPart dtfp) => dtfp.MatchedPart.HasValue && dtfp.MatchedPart.Value.GetKind() == DateTimePartKind.Date).Select(delegate(DateTimeFormatPart dtfp)
					{
						int num4;
						switch (dtfp.MatchedPart.Value)
						{
						case DateTimePart.Year:
							num4 = 0;
							break;
						case DateTimePart.Month:
							num4 = 1;
							break;
						case DateTimePart.Day:
							num4 = 1;
							break;
						default:
							num4 = -1;
							break;
						}
						return num4;
					})
					where x != -1
					select x).ToList<int>();
				this.HasUnorderedDateFormatParts = (double)((readOnlyList6.IsSorted<int>() || readOnlyList6.Reverse<int>().IsSorted<int>()) ? 0 : 1);
				DateTimePartSet matchedParts = dtFormat.MatchedParts;
				bool flag6 = matchedParts.Intersect(DateTimePartSet.TimeParts).Any();
				bool flag7 = matchedParts.Contains(DateTimePart.Hour) || (matchedParts.Contains(DateTimePart.HourInPeriod) && matchedParts.Contains(DateTimePart.Period));
				bool flag8 = matchedParts.Intersect(DateTimePartSet.DateParts).Any();
				bool flag9 = matchedParts.CanExplainFullDate() || (matchedParts.Contains(DateTimePart.Day) && matchedParts.Contains(DateTimePart.Month));
				this.HasPartialDate = (double)((flag8 && (!flag9 || (flag6 && (!flag7 || !matchedParts.Contains(DateTimePart.Minute))))) ? 1 : 0);
				this.PeriodWithoutHour = (double)((matchedParts.Contains(DateTimePart.Period) && !matchedParts.Contains(DateTimePart.HourInPeriod) && !matchedParts.Contains(DateTimePart.Hour)) ? 1 : 0);
				ConstantDateTimeFormatPart constantDateTimeFormatPart3 = dtFormat.FormatParts[0] as ConstantDateTimeFormatPart;
				this.ConstantAtStartLength = (double)((constantDateTimeFormatPart3 != null) ? constantDateTimeFormatPart3.ConstantString.Length : 0);
				double num2;
				if (dtFormat.FormatParts.Count > 1)
				{
					ConstantDateTimeFormatPart constantDateTimeFormatPart4 = dtFormat.FormatParts.Last<DateTimeFormatPart>() as ConstantDateTimeFormatPart;
					if (constantDateTimeFormatPart4 != null)
					{
						num2 = (double)constantDateTimeFormatPart4.ConstantString.Length;
						goto IL_0BB1;
					}
				}
				num2 = (double)0;
				IL_0BB1:
				this.ConstantAtEndLength = num2;
				this.RepeatPartCount = (double)(from p in dtFormat.FormatParts.SelectMany((DateTimeFormatPart fp) => fp.MatchedPart)
					group p by p into g
					where g.Count<DateTimePart>() > 1
					select g).Count<IGrouping<DateTimePart, DateTimePart>>();
			}
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x00085BF8 File Offset: 0x00083DF8
		public virtual double GetScore()
		{
			return -15.0 + (this.ConstantLength + this.DigitConstantLength * 10.0 + this.HasOneDecimalPoint * 50.0) * -2.0 + this.SeparatorKindMatches * 3.0 + this.SeparatorKindMisMatches * -3.0 + this.UnlikelySeparatorCount * -200.0 + this.TimeAndDateShareSeparator * -3.0 + this.BetweenTimeDateSeparatorReused * -100.0 + this.HasNonDelimitedNumbers * -10.0 + this.MinDateInversions * -1.0 + this.MinTimeInversions * -3.0 + this.TimeBeforeDate * -5.0 + this.PeriodWithFullHour * -0.5 + this.VariableLengthCount * -1.5 + this.MatchedPartsCount * 5.0 + this.HasDayOfWeekInMonth * -2.0;
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x00085D20 File Offset: 0x00083F20
		public virtual double GetCompletionScore()
		{
			return -15.0 + (this.ConstantLength + this.DigitConstantLength * 50.0 + this.HasOneDecimalPoint * 50.0) * -2.0 + this.SeparatorKindMatches * 3.0 + this.SeparatorKindMisMatches * -3.0 + this.UnlikelySeparatorCount * -200.0 + this.TimeAndDateShareSeparator * -3.0 + this.BetweenTimeDateSeparatorReused * -100.0 + this.HasNonDelimitedNumbers * -10.0 + this.MinDateInversions * -1.0 + this.MinTimeInversions * -3.0 + this.TimeBeforeDate * -5.0 + this.PeriodWithFullHour * -0.5 + this.VariableLengthPenalty * -1.5 + this.MatchedPartsCount * 5.0 + this.HasUnlikelyConstants * -100.0 + this.UnlikelyFormatPartsCount * -20.0 + this.UnlikelyFormatCombinationsCount * -10.0 + this.HasUnorderedTimeFormatParts * -100.0 + this.HasUnorderedDateFormatParts * -100.0 + this.HasPartialDate * -20.0 + this.PeriodWithoutHour * -100.0 + this.ConstantAtStartLength * 0.001 + this.ConstantAtEndLength * 0.01 + this.RepeatPartCount * -20.0;
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x00085EE0 File Offset: 0x000840E0
		public static bool? IsCommonDateTimeSeparator(string constant, DtFeatures.DateTimeSeparatorKind partsKind)
		{
			IReadOnlyList<DtFeatures.DateTimeSeparatorKind> readOnlyList = (from kv in DtFeatures.PreferredDateTimeSeparators
				where kv.Value.Contains(constant)
				select kv.Key).ToList<DtFeatures.DateTimeSeparatorKind>();
			if (!readOnlyList.Any<DtFeatures.DateTimeSeparatorKind>())
			{
				return null;
			}
			return new bool?(readOnlyList.Contains(partsKind));
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x00085F58 File Offset: 0x00084158
		public static bool? IsCommonDateTimeSeparator(string constant, Optional<DateTimePart> leftMatchedPart, Optional<DateTimePart> rightMatchedPart)
		{
			if (!leftMatchedPart.HasValue || !rightMatchedPart.HasValue)
			{
				return null;
			}
			DtFeatures.DateTimeSeparatorKind expectedDateTimeSeparatorKind = DtFeatures.GetExpectedDateTimeSeparatorKind(leftMatchedPart.Value, rightMatchedPart.Value);
			return DtFeatures.IsCommonDateTimeSeparator(constant, expectedDateTimeSeparatorKind);
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x00085F9C File Offset: 0x0008419C
		private static IEnumerable<DtFeatures.DateTimePartOrder> GetMatchingCommonOrders(IReadOnlyList<DtFeatures.DateTimePartOrder> commonDateTimePartOrders, IReadOnlyList<DateTimePart> partOrder, bool avoidImperialDateTimeFormat = false, IReadOnlyList<string> separators = null)
		{
			bool compareSeparators = separators != null && commonDateTimePartOrders.Any((DtFeatures.DateTimePartOrder commonOrder) => commonOrder.IsCommonSeparators(separators));
			return from referenceOrder in commonDateTimePartOrders
				where !avoidImperialDateTimeFormat || !referenceOrder.IsImperial()
				where referenceOrder.ContainsAllParts(partOrder) && (!compareSeparators || referenceOrder.IsCommonSeparators(separators))
				select referenceOrder;
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x0008600C File Offset: 0x0008420C
		private static DtFeatures.DateTimeSeparatorKind GetExpectedDateTimeSeparatorKind(DateTimePart leftMatchedPart, DateTimePart rightMatchedPart)
		{
			DateTimePartKind kind = leftMatchedPart.GetKind();
			DateTimePartKind kind2 = rightMatchedPart.GetKind();
			if (kind != kind2)
			{
				if ((kind == DateTimePartKind.Date || kind == DateTimePartKind.Time) && (kind2 == DateTimePartKind.Date || kind2 == DateTimePartKind.Time))
				{
					return DtFeatures.DateTimeSeparatorKind.BetweenTimeDate;
				}
			}
			else
			{
				if (kind == DateTimePartKind.Date)
				{
					return DtFeatures.DateTimeSeparatorKind.Date;
				}
				if (kind == DateTimePartKind.Time)
				{
					if (leftMatchedPart == DateTimePart.Period || rightMatchedPart == DateTimePart.Period)
					{
						return DtFeatures.DateTimeSeparatorKind.BetweenTimePeriod;
					}
					if (leftMatchedPart != DateTimePart.Millisecond && rightMatchedPart != DateTimePart.Millisecond)
					{
						return DtFeatures.DateTimeSeparatorKind.Time;
					}
					return DtFeatures.DateTimeSeparatorKind.BetweenSecondMillisecond;
				}
			}
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unexpected DateTimePartKinds: left={0} ({1}), right={2} ({3})", new object[] { leftMatchedPart, kind, rightMatchedPart, kind2 })));
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x0008609C File Offset: 0x0008429C
		// Note: this type is marked as 'beforefieldinit'.
		static DtFeatures()
		{
			DtFeatures.DateTimePartOrder[] array = new DtFeatures.DateTimePartOrder[4];
			array[0] = new DtFeatures.DateTimePartOrder(new DateTimePart[]
			{
				DateTimePart.DayOfWeek,
				DateTimePart.Year,
				DateTimePart.Month,
				DateTimePart.Day
			}, new string[][]
			{
				new string[] { ".", "." },
				new string[] { "-", "-" }
			});
			array[1] = new DtFeatures.DateTimePartOrder(new DateTimePart[]
			{
				DateTimePart.DayOfWeek,
				DateTimePart.Month,
				DateTimePart.Day,
				DateTimePart.Year
			}, new string[][] { new string[] { "/", "/" } });
			array[2] = new DtFeatures.DateTimePartOrder(new DateTimePart[]
			{
				DateTimePart.DayOfWeek,
				DateTimePart.Day,
				DateTimePart.Month,
				DateTimePart.Year
			}, new string[][]
			{
				new string[] { ".", "." },
				new string[] { "/", "/" }
			});
			int num = 3;
			DateTimePart[] array2 = new DateTimePart[2];
			array2[0] = DateTimePart.Quarter;
			array[num] = new DtFeatures.DateTimePartOrder(array2, null);
			DtFeatures.CommonDatePartOrders = array;
			DtFeatures.CommonTimePartOrders = new DtFeatures.DateTimePartOrder[]
			{
				new DtFeatures.DateTimePartOrder(new DateTimePart[]
				{
					DateTimePart.Hour,
					DateTimePart.Minute,
					DateTimePart.Second,
					DateTimePart.Millisecond,
					DateTimePart.Period
				}, null),
				new DtFeatures.DateTimePartOrder(new DateTimePart[]
				{
					DateTimePart.HourInPeriod,
					DateTimePart.Minute,
					DateTimePart.Second,
					DateTimePart.Millisecond,
					DateTimePart.Period
				}, null)
			};
			Dictionary<DtFeatures.DateTimeSeparatorKind, string[]> dictionary = new Dictionary<DtFeatures.DateTimeSeparatorKind, string[]>();
			dictionary[DtFeatures.DateTimeSeparatorKind.Date] = new string[] { "/", "-", " ", ", ", "." };
			dictionary[DtFeatures.DateTimeSeparatorKind.Time] = new string[] { ":" };
			dictionary[DtFeatures.DateTimeSeparatorKind.BetweenTimeDate] = new string[] { " ", "T" };
			dictionary[DtFeatures.DateTimeSeparatorKind.BetweenTimePeriod] = new string[] { " " };
			dictionary[DtFeatures.DateTimeSeparatorKind.BetweenSecondMillisecond] = NumberOptions.DecimalMarkOptions.Select((char c) => c.ToString()).ToArray<string>();
			DtFeatures.PreferredDateTimeSeparators = dictionary;
		}

		// Token: 0x04001693 RID: 5779
		private static IReadOnlyList<Regex> _unlikelyConstants = null;

		// Token: 0x04001694 RID: 5780
		private static readonly IReadOnlyList<string> UnlikelyFormatParts = new string[] { "YY", "YYYY", "y", "m", "s", "yyyyy", "q", "i" };

		// Token: 0x04001695 RID: 5781
		private static readonly IReadOnlyList<IReadOnlyList<string>> UnlikelyFormatCombinations = new string[][]
		{
			new string[] { "H", "t" },
			new string[] { "HH", "t" },
			new string[] { "H", "tt" },
			new string[] { "HH", "tt" }
		};

		// Token: 0x04001696 RID: 5782
		private double? _score;

		// Token: 0x04001697 RID: 5783
		private static readonly DtFeatures.DateTimePartOrder[] CommonDatePartOrders;

		// Token: 0x04001698 RID: 5784
		private static readonly DtFeatures.DateTimePartOrder[] CommonTimePartOrders;

		// Token: 0x04001699 RID: 5785
		private static readonly Dictionary<DtFeatures.DateTimeSeparatorKind, string[]> PreferredDateTimeSeparators;

		// Token: 0x02000856 RID: 2134
		public enum DateTimeSeparatorKind
		{
			// Token: 0x0400169B RID: 5787
			Time,
			// Token: 0x0400169C RID: 5788
			Date,
			// Token: 0x0400169D RID: 5789
			BetweenTimeDate,
			// Token: 0x0400169E RID: 5790
			BetweenTimePeriod,
			// Token: 0x0400169F RID: 5791
			BetweenSecondMillisecond
		}

		// Token: 0x02000857 RID: 2135
		private class DateTimePartOrder
		{
			// Token: 0x06002E86 RID: 11910 RVA: 0x0008634F File Offset: 0x0008454F
			public DateTimePartOrder(IEnumerable<DateTimePart> order, IReadOnlyList<string[]> commonSeparators = null)
			{
				this.Order = (order as DateTimePart[]) ?? order.ToArray<DateTimePart>();
				this.CommonSeparators = commonSeparators;
			}

			// Token: 0x17000829 RID: 2089
			// (get) Token: 0x06002E87 RID: 11911 RVA: 0x00086374 File Offset: 0x00084574
			private DateTimePart[] Order { get; }

			// Token: 0x1700082A RID: 2090
			// (get) Token: 0x06002E88 RID: 11912 RVA: 0x0008637C File Offset: 0x0008457C
			private IReadOnlyList<IReadOnlyList<string>> CommonSeparators { get; }

			// Token: 0x06002E89 RID: 11913 RVA: 0x00086384 File Offset: 0x00084584
			public int CountInversions(IReadOnlyList<DateTimePart> other)
			{
				return DtFeatures.DateTimePartOrder.CountInversions<DateTimePart>(this.Order, other);
			}

			// Token: 0x06002E8A RID: 11914 RVA: 0x00086394 File Offset: 0x00084594
			private static int CountInversions<T>(T[] reference, IReadOnlyList<T> list)
			{
				int num = 0;
				for (int i = 0; i < list.Count; i++)
				{
					for (int j = i + 1; j < list.Count; j++)
					{
						int num2 = Array.IndexOf<T>(reference, list[i]);
						if (num2 != -1)
						{
							int num3 = Array.IndexOf<T>(reference, list[j]);
							if (num3 != -1 && num2 > num3)
							{
								num++;
							}
						}
					}
				}
				return num;
			}

			// Token: 0x06002E8B RID: 11915 RVA: 0x000863F7 File Offset: 0x000845F7
			public bool ContainsAllParts(IEnumerable<DateTimePart> other)
			{
				return !other.Except(this.Order).Any<DateTimePart>();
			}

			// Token: 0x06002E8C RID: 11916 RVA: 0x00086410 File Offset: 0x00084610
			public bool IsImperial()
			{
				bool flag = this._isImperial.GetValueOrDefault();
				if (this._isImperial == null)
				{
					flag = this.Order.FirstOrDefault((DateTimePart p) => p == DateTimePart.Month || p == DateTimePart.Day) == DateTimePart.Day;
					this._isImperial = new bool?(flag);
					return flag;
				}
				return flag;
			}

			// Token: 0x06002E8D RID: 11917 RVA: 0x00086474 File Offset: 0x00084674
			public bool IsCommonSeparators(IReadOnlyList<string> separators)
			{
				IReadOnlyList<IReadOnlyList<string>> commonSeparators = this.CommonSeparators;
				return commonSeparators != null && commonSeparators.Any((IReadOnlyList<string> common) => common.SequenceEqual(separators));
			}

			// Token: 0x040016A2 RID: 5794
			private bool? _isImperial;
		}

		// Token: 0x0200085A RID: 2138
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040016A6 RID: 5798
			public static Func<char, bool> <0>__IsDigit;
		}
	}
}
