using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000869 RID: 2153
	internal class Heuristics
	{
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002ED6 RID: 11990 RVA: 0x00086B43 File Offset: 0x00084D43
		internal static Heuristics AllowMostFormats { get; } = new Heuristics(HeuristicsMode.AllowMostFormats);

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x00086B4A File Offset: 0x00084D4A
		internal static Heuristics FromAutocomplete { get; } = new Heuristics(HeuristicsMode.FromAutocomplete);

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x00086B51 File Offset: 0x00084D51
		internal static Heuristics FromAutocompleteAllowingDatetimeConstants { get; } = new Heuristics(HeuristicsMode.FromAutocompleteAllowingDatetimeConstants);

		// Token: 0x06002ED9 RID: 11993 RVA: 0x00086B58 File Offset: 0x00084D58
		public static Heuristics For(HeuristicsMode mode)
		{
			Heuristics heuristics;
			switch (mode)
			{
			case HeuristicsMode.AllowMostFormats:
				heuristics = Heuristics.AllowMostFormats;
				break;
			case HeuristicsMode.FromAutocomplete:
				heuristics = Heuristics.FromAutocomplete;
				break;
			case HeuristicsMode.FromAutocompleteAllowingDatetimeConstants:
				heuristics = Heuristics.FromAutocompleteAllowingDatetimeConstants;
				break;
			default:
				throw new NotImplementedException("Unknown HeuristicsMode: " + mode.ToString());
			}
			return heuristics;
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x00086BAF File Offset: 0x00084DAF
		private HeuristicsMode Mode { get; }

		// Token: 0x06002EDB RID: 11995 RVA: 0x00086BB8 File Offset: 0x00084DB8
		private Heuristics(HeuristicsMode mode)
		{
			this.Mode = mode;
			HeuristicsMode mode2 = this.Mode;
			uint? num;
			if (mode2 != HeuristicsMode.FromAutocomplete)
			{
				if (mode2 != HeuristicsMode.FromAutocompleteAllowingDatetimeConstants)
				{
					num = new uint?(5U);
				}
				else
				{
					num = null;
				}
			}
			else
			{
				num = null;
			}
			this.MaxConstantLength = num;
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002EDC RID: 11996 RVA: 0x00086C09 File Offset: 0x00084E09
		internal uint? MaxConstantLength { get; }

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002EDD RID: 11997 RVA: 0x00086C11 File Offset: 0x00084E11
		internal bool AllowAnyNonNumericEdgeConstants
		{
			get
			{
				return this.Mode == HeuristicsMode.FromAutocompleteAllowingDatetimeConstants;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002EDE RID: 11998 RVA: 0x0000A5FD File Offset: 0x000087FD
		internal bool MustMatchDistinctParts
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x00086C1C File Offset: 0x00084E1C
		internal bool AllowMismatchedDayOfWeek
		{
			get
			{
				return this.Mode == HeuristicsMode.FromAutocomplete || this.Mode == HeuristicsMode.FromAutocompleteAllowingDatetimeConstants;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002EE0 RID: 12000 RVA: 0x00086C1C File Offset: 0x00084E1C
		internal bool AllowIntermingledDateAndTimeParts
		{
			get
			{
				return this.Mode == HeuristicsMode.FromAutocomplete || this.Mode == HeuristicsMode.FromAutocompleteAllowingDatetimeConstants;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002EE1 RID: 12001 RVA: 0x00086C1C File Offset: 0x00084E1C
		internal bool OnlyCheckPrefixHeuristics
		{
			get
			{
				return this.Mode == HeuristicsMode.FromAutocomplete || this.Mode == HeuristicsMode.FromAutocompleteAllowingDatetimeConstants;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x00086C1C File Offset: 0x00084E1C
		internal bool AllowAnySeparatorWithWhitespace
		{
			get
			{
				return this.Mode == HeuristicsMode.FromAutocomplete || this.Mode == HeuristicsMode.FromAutocompleteAllowingDatetimeConstants;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002EE3 RID: 12003 RVA: 0x00086C11 File Offset: 0x00084E11
		internal bool AllowConstantDigits
		{
			get
			{
				return this.Mode == HeuristicsMode.FromAutocompleteAllowingDatetimeConstants;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x00086C11 File Offset: 0x00084E11
		internal bool AllowConstantDateValueStrings
		{
			get
			{
				return this.Mode == HeuristicsMode.FromAutocompleteAllowingDatetimeConstants;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002EE5 RID: 12005 RVA: 0x00086C1C File Offset: 0x00084E1C
		internal bool StrongOrdinalDayPreference
		{
			get
			{
				return this.Mode == HeuristicsMode.FromAutocomplete || this.Mode == HeuristicsMode.FromAutocompleteAllowingDatetimeConstants;
			}
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x00086C34 File Offset: 0x00084E34
		internal bool IsReasonableDateTimeMatchPrefix(DateTimeFormatMatch match)
		{
			return this.IsReasonableDateTimeMatchPrefix(match.DateTimeFormat.FormatParts.SelectMany((DateTimeFormatPart fp) => fp.MatchedPart.AsEnumerable<DateTimePart>()).ToImmutableList<DateTimePart>(), match.DateTimeFormat.MatchedParts);
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x00086C88 File Offset: 0x00084E88
		private bool IsReasonableDateTimeMatchPrefix(IImmutableList<DateTimePart> matchedParts, DateTimePartSet matchedPartsMask)
		{
			if (matchedParts.Count <= 1)
			{
				return true;
			}
			bool flag = matchedPartsMask.Contains(DateTimePart.Second);
			if (matchedPartsMask.Contains(DateTimePart.Millisecond) && !flag && matchedParts.Last<DateTimePart>() != DateTimePart.Second)
			{
				return false;
			}
			bool flag2 = matchedPartsMask.Contains(DateTimePart.Hour);
			bool flag3 = matchedPartsMask.Contains(DateTimePart.HourInPeriod);
			if (flag2 && flag3)
			{
				return false;
			}
			bool flag4 = matchedPartsMask.Contains(DateTimePart.Minute);
			bool flag5 = matchedPartsMask.Contains(DateTimePart.Period);
			bool flag6 = flag2 || (flag3 && flag5);
			bool flag7 = matchedPartsMask.Intersect(DateTimePartSet.TimeParts).Any();
			bool flag8 = flag4 && flag6;
			bool flag9 = matchedPartsMask.Contains(DateTimePart.Year);
			bool flag10 = matchedPartsMask.Contains(DateTimePart.Month);
			bool flag11 = matchedPartsMask.Contains(DateTimePart.Quarter);
			bool flag12 = matchedPartsMask.Contains(DateTimePart.Day);
			bool flag13 = matchedPartsMask.Contains(DateTimePart.DayOfWeek);
			bool flag14 = matchedPartsMask.Contains(DateTimePart.DayOfWeekInMonth);
			bool flag15 = matchedPartsMask.Contains(DateTimePart.DayOfYear);
			bool flag16 = matchedPartsMask.Contains(DateTimePart.WeekYear);
			bool flag17 = matchedPartsMask.Contains(DateTimePart.WeekOfYear);
			if ((flag16 || flag17) && (flag9 || flag10 || flag12 || flag14))
			{
				return false;
			}
			if (flag11 && (flag12 || flag13 || flag10 || flag15 || flag14))
			{
				return false;
			}
			if (flag15 && (flag12 || flag13 || flag10 || flag11 || flag14))
			{
				return false;
			}
			bool flag18 = matchedPartsMask.Intersect(DateTimePartSet.DateParts).Any();
			bool flag19 = matchedPartsMask.CanExplainFullDate() || (flag12 && flag10);
			if (flag7 && !flag8 && flag18 && !flag19 && !this.AllowIntermingledDateAndTimeParts)
			{
				return false;
			}
			if (matchedParts.Count >= 3)
			{
				DateTimePartKind kind = matchedParts[0].GetKind();
				if (kind == matchedParts.Last<DateTimePart>().GetKind() && kind != matchedParts[matchedParts.Count - 2].GetKind())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x00086E38 File Offset: 0x00085038
		private static bool IsReasonableDateTimeMatch(DateTimePartSet matchedPartsMask)
		{
			if (!matchedPartsMask.Any())
			{
				return false;
			}
			DateTimePart? dateTimePart = matchedPartsMask.OnlyOrDefault();
			if (dateTimePart != null)
			{
				DateTimePart value = dateTimePart.Value;
				return Heuristics.AllowedOnlyParts.Contains(value);
			}
			bool flag = matchedPartsMask.Contains(DateTimePart.Millisecond);
			bool flag2 = matchedPartsMask.Contains(DateTimePart.Second);
			if (flag && !flag2)
			{
				return false;
			}
			bool flag3 = matchedPartsMask.Contains(DateTimePart.Hour);
			bool flag4 = matchedPartsMask.Contains(DateTimePart.HourInPeriod);
			if (flag3 && flag4)
			{
				return false;
			}
			bool flag5 = matchedPartsMask.Contains(DateTimePart.Minute);
			bool flag6 = matchedPartsMask.Contains(DateTimePart.Period);
			bool flag7 = flag3 || (flag4 && flag6);
			bool flag8 = matchedPartsMask.Intersect(DateTimePartSet.TimeParts).Any();
			if (flag2 && !flag5)
			{
				return false;
			}
			if (flag5 && !flag7)
			{
				return false;
			}
			bool flag9 = matchedPartsMask.Contains(DateTimePart.Year);
			bool flag10 = matchedPartsMask.Contains(DateTimePart.Month);
			bool flag11 = matchedPartsMask.Contains(DateTimePart.Quarter);
			bool flag12 = matchedPartsMask.Contains(DateTimePart.Day);
			bool flag13 = matchedPartsMask.Contains(DateTimePart.DayOfWeek);
			bool flag14 = matchedPartsMask.Contains(DateTimePart.DayOfYear);
			bool flag15 = matchedPartsMask.Contains(DateTimePart.DayOfWeekInMonth);
			bool flag16 = matchedPartsMask.Contains(DateTimePart.WeekYear);
			bool flag17 = matchedPartsMask.Contains(DateTimePart.WeekOfYear);
			if ((flag16 || flag17) && (flag9 || flag10 || flag12 || flag15))
			{
				return false;
			}
			if (flag11 && (flag12 || flag13 || flag10 || flag14 || flag15 || !flag9))
			{
				return false;
			}
			if (flag14 && (flag12 || flag13 || flag10 || flag11 || flag15 || !flag9))
			{
				return false;
			}
			if (flag15 && !flag13)
			{
				return false;
			}
			if (flag12 && !flag10)
			{
				return false;
			}
			bool flag18 = matchedPartsMask.Intersect(DateTimePartSet.DateParts).Any();
			bool flag19 = matchedPartsMask.CanExplainFullDate() || (flag12 && flag10);
			return !flag8 || !flag18 || (flag19 && (flag7 && flag5));
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x00086FF0 File Offset: 0x000851F0
		internal bool IsReasonableDateTimeMatch(DateTimeFormatMatch match)
		{
			if (this.OnlyCheckPrefixHeuristics)
			{
				return this.IsReasonableDateTimeMatchPrefix(match);
			}
			bool flag = match.DateTimeFormat.FormatParts.IsNumericIncludingAtEnd();
			if (!flag && match.DateTimeFormat.HasNonDelimitedNumbers())
			{
				return false;
			}
			if (!flag && match.DateTimeFormat.FormatParts.Count >= 3)
			{
				if (match.Region.Start > 0U)
				{
					ConstantDateTimeFormatPart constantDateTimeFormatPart = match.DateTimeFormat.FormatParts.OfType<ConstantDateTimeFormatPart>().FirstOrDefault<ConstantDateTimeFormatPart>();
					string text = ((constantDateTimeFormatPart != null) ? constantDateTimeFormatPart.ConstantString : null);
					if (!string.IsNullOrWhiteSpace(text) && text.Trim() == text && (ulong)match.Region.Start >= (ulong)((long)text.Length) && match.Region.Source.IndexOf(text, (int)(match.Region.Start - (uint)text.Length), text.Length, StringComparison.Ordinal) != -1)
					{
						return false;
					}
				}
				if ((ulong)match.Region.End < (ulong)((long)match.Region.Source.Length))
				{
					ConstantDateTimeFormatPart constantDateTimeFormatPart2 = match.DateTimeFormat.FormatParts.OfType<ConstantDateTimeFormatPart>().LastOrDefault<ConstantDateTimeFormatPart>();
					string text2 = ((constantDateTimeFormatPart2 != null) ? constantDateTimeFormatPart2.ConstantString : null);
					if (!string.IsNullOrWhiteSpace(text2) && text2.Trim() == text2 && (ulong)match.Region.End + (ulong)((long)text2.Length) <= (ulong)((long)match.Region.Source.Length) && match.Region.Source.IndexOf(text2, (int)match.Region.End, text2.Length, StringComparison.Ordinal) != -1)
					{
						return false;
					}
				}
			}
			return Heuristics.IsReasonableDateTimeMatch(match.DateTimeFormat.MatchedParts) && match.PartialDateTime != null;
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x000871A0 File Offset: 0x000853A0
		internal DateTimeFormatMatch CombineWithIfReasonable(DateTimeFormatMatch thisMatch, DateTimeFormatMatch other)
		{
			bool allowConstantDigits = this.AllowConstantDigits;
			bool mustMatchDistinctParts = this.MustMatchDistinctParts;
			if (other.Region.Source != thisMatch.Region.Source)
			{
				return null;
			}
			if (thisMatch.Region.IntersectNonEmpty(other.Region))
			{
				return null;
			}
			if (mustMatchDistinctParts && thisMatch.DateTimeFormat.MatchedParts.Intersect(other.DateTimeFormat.MatchedParts).Any())
			{
				return null;
			}
			DateTimePartSet dateTimePartSet = thisMatch.DateTimeFormat.MatchedParts.Union(other.DateTimeFormat.MatchedParts);
			if (dateTimePartSet.Contains(DateTimePart.HourInPeriod, DateTimePart.Hour))
			{
				return null;
			}
			DateTimeFormatMatch dateTimeFormatMatch = ((thisMatch.Region.Start < other.Region.Start) ? thisMatch : other);
			DateTimeFormatMatch dateTimeFormatMatch2 = ((dateTimeFormatMatch == thisMatch) ? other : thisMatch);
			StringRegion stringRegion = thisMatch.Region.WholeRegion.Slice(dateTimeFormatMatch.Region.End, dateTimeFormatMatch2.Region.Start);
			if (stringRegion.Length > 0U && dateTimeFormatMatch.DateTimeFormat.FormatParts.Count > 1 && dateTimeFormatMatch.DateTimeFormat.IsNumeric)
			{
				return null;
			}
			bool flag = stringRegion.Length == 0U && thisMatch.DateTimeFormat.IsNumeric && other.DateTimeFormat.IsNumeric;
			if (!flag && stringRegion.Length == 0U && dateTimeFormatMatch.DateTimeFormat.FormatParts.Count == 1)
			{
				Optional<char> optional = dateTimeFormatMatch.Region.MaybeLastChar();
				Func<char, bool> func;
				if ((func = Heuristics.<>O.<0>__IsDigit) == null)
				{
					func = (Heuristics.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
				}
				if (optional.Select(func).OrElse(false))
				{
					Optional<char> optional2 = dateTimeFormatMatch.Region.MaybeNextChar();
					Func<char, bool> func2;
					if ((func2 = Heuristics.<>O.<0>__IsDigit) == null)
					{
						func2 = (Heuristics.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
					}
					if (optional2.Select(func2).OrElse(false))
					{
						return null;
					}
				}
			}
			if (!flag)
			{
				Optional<char> optional3 = dateTimeFormatMatch2.Region.MaybeLastChar();
				Func<char, bool> func3;
				if ((func3 = Heuristics.<>O.<0>__IsDigit) == null)
				{
					func3 = (Heuristics.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
				}
				if (optional3.Select(func3).OrElse(false))
				{
					Optional<char> optional4 = dateTimeFormatMatch2.Region.MaybeNextChar();
					Func<char, bool> func4;
					if ((func4 = Heuristics.<>O.<0>__IsDigit) == null)
					{
						func4 = (Heuristics.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
					}
					if (optional4.Select(func4).OrElse(false))
					{
						return null;
					}
				}
			}
			if (flag && dateTimePartSet.Contains(DateTimePart.Year, DateTimePart.DayOfYear) && other.DateTimeFormat.MatchedParts.Intersect(new DateTimePartSet(new DateTimePart[]
			{
				DateTimePart.Year,
				DateTimePart.DayOfYear
			})).Any())
			{
				IReadOnlyList<DateTimeFormatPart> readOnlyList = dateTimeFormatMatch.DateTimeFormat.FormatParts.Concat(dateTimeFormatMatch2.DateTimeFormat.FormatParts).ToList<DateTimeFormatPart>();
				if (readOnlyList.Any((DateTimeFormatPart p) => p.MatchedPart.HasValue && p.MatchedPart.Value == DateTimePart.Year && p.MaximumLength == 2))
				{
					if (readOnlyList.Any((DateTimeFormatPart p) => p.MatchedPart.HasValue && p.MatchedPart.Value == DateTimePart.DayOfYear && p.MinimumLength != p.MaximumLength))
					{
						return null;
					}
				}
			}
			string constantBetweenStr = stringRegion.Value;
			ConstantDateTimeFormatPart constantDateTimeFormatPart = dateTimeFormatMatch.DateTimeFormat.FormatParts.Last<DateTimeFormatPart>() as ConstantDateTimeFormatPart;
			ConstantDateTimeFormatPart constantDateTimeFormatPart2 = dateTimeFormatMatch2.DateTimeFormat.FormatParts.First<DateTimeFormatPart>() as ConstantDateTimeFormatPart;
			if (!this.AllowConstantDateValueStrings && ((constantDateTimeFormatPart != null && Heuristics.ForbiddenSeparatorSubString.IsMatch(constantDateTimeFormatPart.ConstantString)) || (constantDateTimeFormatPart2 != null && Heuristics.ForbiddenSeparatorSubString.IsMatch(constantDateTimeFormatPart2.ConstantString))))
			{
				return null;
			}
			if (!allowConstantDigits)
			{
				if (Heuristics.Number.IsMatch(constantBetweenStr) || (constantDateTimeFormatPart != null && Heuristics.Number.IsMatch(constantDateTimeFormatPart.ConstantString)) || (constantDateTimeFormatPart2 != null && Heuristics.Number.IsMatch(constantDateTimeFormatPart2.ConstantString)))
				{
					return null;
				}
			}
			else if (Heuristics.Number.IsMatch(constantBetweenStr) && ((from previousChar in dateTimeFormatMatch.ParsedRegion.MaybeLastChar()
				select char.IsDigit(previousChar) && char.IsDigit(constantBetweenStr[0])).OrElseDefault<bool>() || (from nextChar in dateTimeFormatMatch2.ParsedRegion.MaybeFirstChar()
				select char.IsDigit(nextChar) && char.IsDigit(constantBetweenStr[constantBetweenStr.Length - 1])).OrElseDefault<bool>()))
			{
				return null;
			}
			if ((Heuristics.ForbiddenSeparator.IsMatch(constantBetweenStr) || (!this.AllowConstantDateValueStrings && Heuristics.ForbiddenSeparatorSubString.IsMatch(constantBetweenStr))) && (!(constantBetweenStr == ",") || !(dateTimeFormatMatch.DateTimeFormat.FormatParts.Last<DateTimeFormatPart>().MatchedPart == DateTimePart.Second.Some<DateTimePart>()) || !(dateTimeFormatMatch2.DateTimeFormat.FormatParts[0].MatchedPart == DateTimePart.Millisecond.Some<DateTimePart>())))
			{
				return null;
			}
			if (dateTimeFormatMatch.DateTimeFormat.FormatParts.Count == 1 && constantBetweenStr.Length > 0 && (ulong)dateTimeFormatMatch.Region.Start >= (ulong)((long)constantBetweenStr.Length) && dateTimeFormatMatch.Region.Source.IndexOf(constantBetweenStr, dateTimeFormatMatch.Region.Source.Length - constantBetweenStr.Length, constantBetweenStr.Length, StringComparison.Ordinal) != -1)
			{
				if (this.AllowAnySeparatorWithWhitespace)
				{
					IEnumerable<char> constantBetweenStr3 = constantBetweenStr;
					Func<char, bool> func5;
					if ((func5 = Heuristics.<>O.<1>__IsWhiteSpace) == null)
					{
						func5 = (Heuristics.<>O.<1>__IsWhiteSpace = new Func<char, bool>(char.IsWhiteSpace));
					}
					if (constantBetweenStr3.Any(func5))
					{
						goto IL_0557;
					}
				}
				return null;
			}
			IL_0557:
			Optional<DateTimePart> matchedPart = dateTimeFormatMatch2.DateTimeFormat.FormatParts[0].MatchedPart;
			Optional<DateTimePart> matchedPart2 = dateTimeFormatMatch.DateTimeFormat.FormatParts.Last<DateTimeFormatPart>().MatchedPart;
			if (matchedPart.HasValue && matchedPart.Value == DateTimePart.Millisecond)
			{
				if (!matchedPart2.HasValue || matchedPart2.Value != DateTimePart.Second)
				{
					return null;
				}
				if ((constantBetweenStr.Length != 1 || !NumberOptions.DecimalMarkOptions.Contains(constantBetweenStr[0])) && !string.IsNullOrEmpty(constantBetweenStr) && dateTimeFormatMatch.DateTimeFormat.IsNumeric)
				{
					return null;
				}
			}
			if (matchedPart2.HasValue && matchedPart2.Value == DateTimePart.Millisecond && !dateTimeFormatMatch.DateTimeFormat.MatchedParts.Contains(DateTimePart.Second))
			{
				if (matchedPart != DateTimePart.Second.Some<DateTimePart>())
				{
					return null;
				}
				if (!string.IsNullOrEmpty(constantBetweenStr) && dateTimeFormatMatch.DateTimeFormat.IsNumeric && dateTimeFormatMatch2.DateTimeFormat.IsNumeric)
				{
					return null;
				}
			}
			if (dateTimeFormatMatch.DateTimeFormat.FormatParts.OfType<ConstantDateTimeFormatPart>().Any<ConstantDateTimeFormatPart>())
			{
				string constantString = dateTimeFormatMatch.DateTimeFormat.FormatParts.OfType<ConstantDateTimeFormatPart>().Last<ConstantDateTimeFormatPart>().ConstantString;
				if (!string.IsNullOrWhiteSpace(constantBetweenStr))
				{
					List<DateTimePart> list = (from fp in dateTimeFormatMatch.DateTimeFormat.FormatParts
						select fp.MatchedPart into p
						where p.HasValue
						select p.Value).ToList<DateTimePart>();
					DateTimePartKind kind = list.Last<DateTimePart>().GetKind();
					DateTimePart? dateTimePart;
					DateTimePartKind? dateTimePartKind = ((list.DropLast(1).MaybeLast<DateTimePart>().OrElseNull<DateTimePart>() != null) ? new DateTimePartKind?(dateTimePart.GetValueOrDefault().GetKind()) : null);
					DateTimePartKind kind2 = dateTimeFormatMatch2.DateTimeFormat.FormatParts.First<DateTimeFormatPart>().MatchedPart.Value.GetKind();
					if ((kind != kind2 || (dateTimePartKind != null && dateTimePartKind.Value != kind)) && constantString == constantBetweenStr)
					{
						return null;
					}
				}
				if (this.AllowAnySeparatorWithWhitespace)
				{
					IEnumerable<char> constantBetweenStr2 = constantBetweenStr;
					Func<char, bool> func6;
					if ((func6 = Heuristics.<>O.<1>__IsWhiteSpace) == null)
					{
						func6 = (Heuristics.<>O.<1>__IsWhiteSpace = new Func<char, bool>(char.IsWhiteSpace));
					}
					if (constantBetweenStr2.Any(func6))
					{
						goto IL_0921;
					}
				}
				DateTimePartSet dateTimePartSet2 = new DateTimePartSet(new DateTimePart[]
				{
					DateTimePart.Year,
					DateTimePart.Month,
					DateTimePart.Day
				});
				DateTimePartSet dateTimePartSet3 = new DateTimePartSet(new DateTimePart[]
				{
					DateTimePart.Hour,
					DateTimePart.Minute,
					DateTimePart.Second
				});
				DateTimePartSet[] array = new DateTimePartSet[] { dateTimePartSet3, dateTimePartSet2 };
				for (int i = 0; i < array.Length; i++)
				{
					DateTimePartSet mask = array[i];
					if (mask.Intersect(dateTimeFormatMatch.DateTimeFormat.MatchedParts).Any() && mask.Intersect(dateTimeFormatMatch2.DateTimeFormat.MatchedParts).Any() && dateTimeFormatMatch.DateTimeFormat.FormatParts.TakeLast(3).All((DateTimeFormatPart p) => !p.MatchedPart.HasValue || mask.Contains(p.MatchedPart.Value)) && !(constantString == constantBetweenStr) && (!Heuristics.SpacesWithCommaOrSymbols.IsMatch(constantString) || !Heuristics.SpacesWithCommaOrSymbols.IsMatch(constantBetweenStr)) && (!Heuristics.LettersAndOrSpaces.IsMatch(constantString) || !Heuristics.LettersAndOrSpaces.IsMatch(constantBetweenStr)))
					{
						return null;
					}
				}
			}
			IL_0921:
			StringDateTimeFormatPart stringDateTimeFormatPart = dateTimeFormatMatch.DateTimeFormat.FormatParts.Last<DateTimeFormatPart>() as StringDateTimeFormatPart;
			if (stringDateTimeFormatPart != null && stringDateTimeFormatPart.MatchedPart.HasValue && stringDateTimeFormatPart.AbbreviationOf != null && stringDateTimeFormatPart.MinimumLength == stringDateTimeFormatPart.MaximumLength)
			{
				string text = constantBetweenStr;
				ConstantDateTimeFormatPart constantDateTimeFormatPart3 = other.DateTimeFormat.FormatParts[0] as ConstantDateTimeFormatPart;
				if (constantDateTimeFormatPart3 != null)
				{
					text += constantDateTimeFormatPart3.ConstantString;
				}
				if (text.Length >= stringDateTimeFormatPart.AbbreviationOf.MinimumLength - stringDateTimeFormatPart.MaximumLength)
				{
					string text2 = stringDateTimeFormatPart.AbbreviationOf.ToString(dateTimeFormatMatch.PartialDateTime);
					if (text2.Length > stringDateTimeFormatPart.MaximumLength)
					{
						string text3 = text2.Substring(stringDateTimeFormatPart.MaximumLength);
						if (text.StartsWith(text3, StringComparison.Ordinal))
						{
							return null;
						}
					}
				}
			}
			Optional<PartialDateTime> combinedPartialDateTime = thisMatch.PartialDateTime.CombineWith(other.PartialDateTime);
			if (!combinedPartialDateTime.HasValue)
			{
				if (!this.AllowMismatchedDayOfWeek || (!thisMatch.PartialDateTime.DayOfWeek.HasValue && !other.PartialDateTime.DayOfWeek.HasValue))
				{
					return null;
				}
				combinedPartialDateTime = thisMatch.PartialDateTime.Without(DateTimePart.DayOfWeek).CombineWith(other.PartialDateTime.Without(DateTimePart.DayOfWeek));
				if (!combinedPartialDateTime.HasValue)
				{
					return null;
				}
			}
			StringRegion match = thisMatch.Region.WholeRegion.Slice(dateTimeFormatMatch.Region.Start, dateTimeFormatMatch2.Region.End);
			IEnumerable<DateTimeFormatPart> enumerable = dateTimeFormatMatch.DateTimeFormat.FormatParts.Concat(Seq.Of<ConstantDateTimeFormatPart>(new ConstantDateTimeFormatPart[]
			{
				new ConstantDateTimeFormatPart(stringRegion)
			})).Concat(dateTimeFormatMatch2.DateTimeFormat.FormatParts);
			return (from format in this.CreateDateTimeFormatIfReasonable(enumerable)
				select from dt in combinedPartialDateTime
					select new DateTimeFormatMatch(match, format, dt)).OrElseDefault<Optional<DateTimeFormatMatch>>().OrElseDefault<DateTimeFormatMatch>();
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x00087CD0 File Offset: 0x00085ED0
		internal Optional<DateTimeFormat> CreateDateTimeFormatIfReasonable(IEnumerable<DateTimeFormatPart> formatParts)
		{
			List<DateTimeFormatPart> list = formatParts.Where((DateTimeFormatPart fp) => fp.MinimumLength > 0).ToList<DateTimeFormatPart>();
			uint? maxConstantLength2 = this.MaxConstantLength;
			if (maxConstantLength2 != null)
			{
				uint maxConstantLength = maxConstantLength2.GetValueOrDefault();
				if (list.OfType<ConstantDateTimeFormatPart>().Any((ConstantDateTimeFormatPart constant) => (long)constant.ConstantString.Length > (long)((ulong)maxConstantLength)))
				{
					return Optional<DateTimeFormat>.Nothing;
				}
			}
			bool flag = list.IsNumericIncludingAtEnd();
			if (!flag && list.HasNonDelimitedNumbers())
			{
				return Optional<DateTimeFormat>.Nothing;
			}
			if (flag && list.Count > 1)
			{
				List<bool> list2 = list.Select((DateTimeFormatPart fp) => fp.MinimumLength != fp.MaximumLength).ToList<bool>();
				if (list2.Skip(1).Take(list.Count - 2).Any((bool b) => b) || (list2.First<bool>() && list2.Last<bool>()))
				{
					return Optional<DateTimeFormat>.Nothing;
				}
				List<DateTimePart> list3 = list.Select((DateTimeFormatPart fp) => fp.MatchedPart.Value).ToList<DateTimePart>();
				if (list3.Contains(DateTimePart.Quarter) || list3.First<DateTimePart>() == DateTimePart.Millisecond)
				{
					return Optional<DateTimeFormat>.Nothing;
				}
				if (list3.ZipWith(list3.Skip(1)).Any(delegate(Record<DateTimePart, DateTimePart> t)
				{
					DateTimePart item = t.Item1;
					DateTimePart item2 = t.Item2;
					return (item == DateTimePart.Month && item2.GetKind() != DateTimePartKind.Date) || (item2 == DateTimePart.Month && item.GetKind() != DateTimePartKind.Date) || ((item == DateTimePart.Minute && item2.GetKind() != DateTimePartKind.Time) || (item2 == DateTimePart.Minute && item.GetKind() != DateTimePartKind.Time)) || (((item == DateTimePart.Hour || item == DateTimePart.HourInPeriod) && item2 == DateTimePart.Second) || (item == DateTimePart.Second && (item2 == DateTimePart.Hour || item2 == DateTimePart.HourInPeriod))) || (item2 == DateTimePart.Millisecond && item != DateTimePart.Second) || (item == DateTimePart.Period && item2.GetKind() == DateTimePartKind.Time);
				}))
				{
					return Optional<DateTimeFormat>.Nothing;
				}
				for (int i = 1; i < list3.Count - 1; i++)
				{
					if (list3[i - 1].GetKind() == list3[i + 1].GetKind() && list3[i].GetKind() != list3[i - 1].GetKind())
					{
						return Optional<DateTimeFormat>.Nothing;
					}
				}
			}
			return new DateTimeFormat(list).Some<DateTimeFormat>();
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x00087EE0 File Offset: 0x000860E0
		internal bool IsReasonablePartMatch(DateTimeFormatMatch match, bool ordinalDaySupported)
		{
			DateTimeFormatPart dateTimeFormatPart = match.DateTimeFormat.FormatParts[0];
			if (dateTimeFormatPart.MatchedPart == DateTimePart.Period.Some<DateTimePart>())
			{
				StringRegion region = match.Region;
				Optional<char> optional = region.MaybePreviousChar();
				if (optional.HasValue && char.IsLetter(optional.Value))
				{
					return false;
				}
				Optional<char> optional2 = region.MaybeNextChar();
				if (optional2.HasValue && char.IsLetterOrDigit(optional2.Value))
				{
					return false;
				}
			}
			else if (this.StrongOrdinalDayPreference && ordinalDaySupported && dateTimeFormatPart.IsNumeric)
			{
				DateTimeFormatPart dateTimeFormatPart2 = DateTimeFormatPart.Create("o", null);
				StringRegion stringRegion = match.ParsedRegion.WholeRegion.Slice(match.ParsedRegion.Start, match.ParsedRegion.WholeRegion.End);
				if (dateTimeFormatPart2.ParseNext(stringRegion).HasValue)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040016E2 RID: 5858
		private const uint DefaultMaxConstantLength = 5U;

		// Token: 0x040016E4 RID: 5860
		private static readonly DateTimePartSet AllowedOnlyParts = new DateTimePartSet(new DateTimePart[]
		{
			DateTimePart.DayOfWeek,
			DateTimePart.Month,
			DateTimePart.Day,
			DateTimePart.Year,
			DateTimePart.Hour,
			DateTimePart.Quarter,
			DateTimePart.DayOfYear,
			DateTimePart.WeekOfYear
		});

		// Token: 0x040016E5 RID: 5861
		private static readonly Regex SpacesWithCommaOrSymbols = new Regex("^[\\p{Pc}\\p{Po}\\p{S}]*,?\\s+[\\p{Pc}\\p{Po}\\p{S}]*$", RegexOptions.Compiled);

		// Token: 0x040016E6 RID: 5862
		private static readonly Regex LettersAndOrSpaces = new Regex("^\\p{L}*\\s*$", RegexOptions.Compiled);

		// Token: 0x040016E7 RID: 5863
		private static readonly Regex Whitespace = new Regex("\\s", RegexOptions.Compiled);

		// Token: 0x040016E8 RID: 5864
		private static readonly Regex Number = new Regex("\\p{N}", RegexOptions.Compiled);

		// Token: 0x040016E9 RID: 5865
		private static readonly Regex ForbiddenSeparator = new Regex("^,$|\"", RegexOptions.Compiled);

		// Token: 0x040016EA RID: 5866
		private static readonly Regex ForbiddenSeparatorSubString = new Regex(string.Join("|", StringDateTimeFormatPart.DateValueStrings.Select(new Func<string, string>(Regex.Escape))), RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x0200086A RID: 2154
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040016EB RID: 5867
			public static Func<char, bool> <0>__IsDigit;

			// Token: 0x040016EC RID: 5868
			public static Func<char, bool> <1>__IsWhiteSpace;
		}
	}
}
