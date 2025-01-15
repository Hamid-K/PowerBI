using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.FormatParsing.Dates
{
	// Token: 0x02000794 RID: 1940
	public class DateTimeSpacerFormatParser : SpacerFormatParser<DateTimeFormat, DateTimeFormatMatch, PartialDateTime, StringRegion>
	{
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x00075D51 File Offset: 0x00073F51
		private static Regex WhiteSpaceRegex { get; } = new Regex("\\s", RegexOptions.Compiled);

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600299B RID: 10651 RVA: 0x00075D58 File Offset: 0x00073F58
		private static Regex SpacesWithCommaOrSymbols { get; } = new Regex("^[\\p{Pc}\\p{Po}\\p{S}]*,?\\s+[\\p{Pc}\\p{Po}\\p{S}]*$", RegexOptions.Compiled);

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x00075D5F File Offset: 0x00073F5F
		private static Regex LettersAndOrSpaces { get; } = new Regex("^\\p{L}*\\s*$", RegexOptions.Compiled);

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600299D RID: 10653 RVA: 0x00075D66 File Offset: 0x00073F66
		private static Regex Number { get; } = new Regex("\\p{N}", RegexOptions.Compiled);

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x00075D6D File Offset: 0x00073F6D
		private static Regex ForbiddenSeparator { get; } = new Regex("^,$|\"", RegexOptions.Compiled);

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x0600299F RID: 10655 RVA: 0x00075D74 File Offset: 0x00073F74
		private static Regex ForbiddenSeparatorSubString { get; } = new Regex(string.Join("|", StringDateTimeFormatPart.DateValueStrings.Select(new Func<string, string>(Regex.Escape))), RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060029A0 RID: 10656 RVA: 0x00075D7B File Offset: 0x00073F7B
		public bool AllowComma { get; }

		// Token: 0x060029A1 RID: 10657 RVA: 0x00075D84 File Offset: 0x00073F84
		private bool IsMatchingSuffixOfAbbreviation(FormatMatchState<DateTimeFormatMatch, PartialDateTime, DateTimeFormat, StringRegion> state)
		{
			if (!state.CumulativeParse.HasValue)
			{
				return false;
			}
			DateTimeFormatMatch value = state.CumulativeParse.Value;
			StringRegion unparsedSuffix = state.UnparsedSuffix;
			StringDateTimeFormatPart stringDateTimeFormatPart = this.LastNonEmptyPart(value.DateTimeFormat.AllFormatParts) as StringDateTimeFormatPart;
			if (stringDateTimeFormatPart != null && stringDateTimeFormatPart.MatchedPart.HasValue && stringDateTimeFormatPart.AbbreviationOf != null && stringDateTimeFormatPart.MaximumLength == stringDateTimeFormatPart.MinimumLength)
			{
				string value2 = unparsedSuffix.Value;
				if (value2.Length >= stringDateTimeFormatPart.AbbreviationOf.MinimumLength - stringDateTimeFormatPart.MaximumLength)
				{
					string text = stringDateTimeFormatPart.AbbreviationOf.ToString(value.PartialDateTime);
					if (text.Length > stringDateTimeFormatPart.MaximumLength)
					{
						string text2 = text.Substring(stringDateTimeFormatPart.MaximumLength);
						if (value2.StartsWith(text2, StringComparison.Ordinal))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x00075E60 File Offset: 0x00074060
		private bool CheckSpacer(string spacer)
		{
			return !DateTimeSpacerFormatParser.ForbiddenSeparator.IsMatch(spacer) && !DateTimeSpacerFormatParser.ForbiddenSeparatorSubString.IsMatch(spacer) && !DateTimeSpacerFormatParser.Number.IsMatch(spacer);
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x00075E8C File Offset: 0x0007408C
		private DateTimeFormat FormatFrom(StringRegion region)
		{
			return new DateTimeFormat(new DateTimeFormatPart[]
			{
				new SpacerDateTimeFormatPart(region)
			});
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x00075EA2 File Offset: 0x000740A2
		private DateTimeFormatPart LastNonEmptyPart(IEnumerable<DateTimeFormatPart> parts)
		{
			return parts.LastOrDefault(delegate(DateTimeFormatPart part)
			{
				SpacerDateTimeFormatPart spacerDateTimeFormatPart = part as SpacerDateTimeFormatPart;
				return (spacerDateTimeFormatPart == null || !spacerDateTimeFormatPart.IsEmpty) && !(part is EmptyDateTimeFormatPart);
			});
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x00075ECC File Offset: 0x000740CC
		public override Optional<DateTimeFormatMatch> CreatePartialParse(FormatMatchState<DateTimeFormatMatch, PartialDateTime, DateTimeFormat, StringRegion> stateBeforeDelimiter, int delimiterLength)
		{
			StringRegion unparsedSuffix = stateBeforeDelimiter.UnparsedSuffix;
			if (this.IsMatchingSuffixOfAbbreviation(stateBeforeDelimiter))
			{
				return Optional<DateTimeFormatMatch>.Nothing;
			}
			if (delimiterLength > 5)
			{
				return Optional<DateTimeFormatMatch>.Nothing;
			}
			if (!stateBeforeDelimiter.CumulativeParse.HasValue)
			{
				return Optional<DateTimeFormatMatch>.Nothing;
			}
			DateTimeFormatMatch value = stateBeforeDelimiter.CumulativeParse.Value;
			DateTimeFormatPart dateTimeFormatPart = this.LastNonEmptyPart(value.DateTimeFormat.AllFormatParts);
			StringRegion stringRegion = unparsedSuffix.AbsoluteSlice(unparsedSuffix.Start, (uint)((ulong)unparsedSuffix.Start + (ulong)((long)delimiterLength)));
			string value2 = stringRegion.Value;
			int num = (int)(stringRegion.Start + (uint)delimiterLength);
			bool flag = num < stringRegion.Source.Length && char.IsDigit(stringRegion.Source[num]);
			if (value2 == "," && this.AllowComma)
			{
				return new DateTimeFormatMatch(stringRegion, this.FormatFrom(stringRegion), PartialDateTime.Empty).Some<DateTimeFormatMatch>();
			}
			if (!this.CheckSpacer(value2))
			{
				return Optional<DateTimeFormatMatch>.Nothing;
			}
			if (dateTimeFormatPart == null)
			{
				if (delimiterLength == 0)
				{
					return new DateTimeFormatMatch(stringRegion, this.FormatFrom(stringRegion), PartialDateTime.Empty).Some<DateTimeFormatMatch>();
				}
				return Optional<DateTimeFormatMatch>.Nothing;
			}
			else
			{
				if (dateTimeFormatPart is SpacerDateTimeFormatPart && delimiterLength > 0)
				{
					return Optional<DateTimeFormatMatch>.Nothing;
				}
				if (delimiterLength > 0 && (value.DateTimeFormat.IsNumeric && value.DateTimeFormat.HasNonDelimitedNumbers() && dateTimeFormatPart is NumericDateTimeFormatPart && flag))
				{
					return Optional<DateTimeFormatMatch>.Nothing;
				}
				if (delimiterLength == 0)
				{
					if (value.DateTimeFormat.FormatParts.Count((DateTimeFormatPart p) => p is NumericDateTimeFormatPart) > 1 && !value.DateTimeFormat.HasNonDelimitedNumbers() && dateTimeFormatPart is NumericDateTimeFormatPart && flag)
					{
						return Optional<DateTimeFormatMatch>.Nothing;
					}
					if (dateTimeFormatPart is NumericDateTimeFormatPart && flag && dateTimeFormatPart.MinimumLength != dateTimeFormatPart.MaximumLength && !dateTimeFormatPart.AllowsLeadingZeros())
					{
						return Optional<DateTimeFormatMatch>.Nothing;
					}
				}
				return new DateTimeFormatMatch(stringRegion, this.FormatFrom(stringRegion), PartialDateTime.Empty).Some<DateTimeFormatMatch>();
			}
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x000760C8 File Offset: 0x000742C8
		internal override FormatParser<DateTimeFormat, DateTimeFormatMatch, PartialDateTime, StringRegion> Clone(int? matchStoreIndex, Predicate<DeltaFormatMatchState<DateTimeFormatMatch, PartialDateTime, DateTimeFormat, StringRegion>> filterPredicate, IEnumerable<FormatParser<DateTimeFormat, DateTimeFormatMatch, PartialDateTime, StringRegion>.DirectionalConstraint> directionalConstraints, IEnumerable<FormatParser<DateTimeFormat, DateTimeFormatMatch, PartialDateTime, StringRegion>.GroupConstraint> groupConstraints, int minRepetitions, int maxRepetitions)
		{
			return new DateTimeSpacerFormatParser(this.AllowComma, filterPredicate, directionalConstraints, groupConstraints, minRepetitions, maxRepetitions, matchStoreIndex);
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x000760E0 File Offset: 0x000742E0
		public DateTimeSpacerFormatParser(bool allowComma = false, Predicate<DeltaFormatMatchState<DateTimeFormatMatch, PartialDateTime, DateTimeFormat, StringRegion>> filterPredicate = null, IEnumerable<FormatParser<DateTimeFormat, DateTimeFormatMatch, PartialDateTime, StringRegion>.DirectionalConstraint> directionalConstraints = null, IEnumerable<FormatParser<DateTimeFormat, DateTimeFormatMatch, PartialDateTime, StringRegion>.GroupConstraint> groupConstraints = null, int minRepetitions = 1, int maxRepetitions = 1, int? matchStoreIndex = null)
		{
			Func<StringRegion, DateTimeFormatMatch> func;
			if ((func = DateTimeSpacerFormatParser.<>O.<0>__Empty) == null)
			{
				func = (DateTimeSpacerFormatParser.<>O.<0>__Empty = new Func<StringRegion, DateTimeFormatMatch>(DateTimeFormatMatch.Empty));
			}
			base..ctor(func, 5, filterPredicate, directionalConstraints, groupConstraints, minRepetitions, maxRepetitions, matchStoreIndex);
			this.AllowComma = allowComma;
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x00076120 File Offset: 0x00074320
		public static bool SpacerCompatibilityChecker(DateTimeFormatMatch dependentMatch, DateTimeFormatMatch independentMatch)
		{
			if (independentMatch.DateTimeFormat.AllFormatParts.Single<DateTimeFormatPart>() is EmptyDateTimeFormatPart || dependentMatch.DateTimeFormat.AllFormatParts.Single<DateTimeFormatPart>() is EmptyDateTimeFormatPart)
			{
				return true;
			}
			string value = dependentMatch.Region.Value;
			string value2 = independentMatch.Region.Value;
			return value.Equals(value2) || string.IsNullOrWhiteSpace(value) || (DateTimeSpacerFormatParser.SpacesWithCommaOrSymbols.IsMatch(value) && DateTimeSpacerFormatParser.SpacesWithCommaOrSymbols.IsMatch(value2)) || (DateTimeSpacerFormatParser.LettersAndOrSpaces.IsMatch(value) && DateTimeSpacerFormatParser.LettersAndOrSpaces.IsMatch(value2));
		}

		// Token: 0x0400143C RID: 5180
		private const bool AllowNumericSpacers = false;

		// Token: 0x04001444 RID: 5188
		private const int MaximumSpacerLength = 5;

		// Token: 0x02000795 RID: 1941
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001445 RID: 5189
			public static Func<StringRegion, DateTimeFormatMatch> <0>__Empty;
		}
	}
}
