using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001FC RID: 508
	public class PhoneNumberTokenizer : RegexBasedTokenizer
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x00021970 File Offset: 0x0001FB70
		[JsonConstructor]
		public PhoneNumberTokenizer()
		{
			OverlapStrategy overlapStrategy = OverlapStrategy.Subsumption;
			RegexBasedTokenizer.TokenFactoryDelegate tokenFactoryDelegate;
			if ((tokenFactoryDelegate = PhoneNumberTokenizer.<>O.<0>__ProcessMatch) == null)
			{
				tokenFactoryDelegate = (PhoneNumberTokenizer.<>O.<0>__ProcessMatch = new RegexBasedTokenizer.TokenFactoryDelegate(PhoneNumberTokenizer.ProcessMatch));
			}
			base..ctor(overlapStrategy, tokenFactoryDelegate, new TokenPattern[]
			{
				new TokenPattern(PhoneNumberTokenizer.PhoneNumberPattern1, "(?:^|[^\\d\\-\\.])", "(?:$|[^\\d\\-\\.])", false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(PhoneNumberTokenizer.PhoneNumberPattern1WithCc, "(?:^|[^\\d\\-\\.\\+])", "(?:$|[^\\d\\-\\.])", false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(PhoneNumberTokenizer.PhoneNumberPattern2, "(?:^|[^\\d\\-\\.])", "(?:$|[^\\d\\-\\.])", false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(PhoneNumberTokenizer.PhoneNumberPattern2WithCc, "(?:^|[^\\d\\-\\.\\+])", "(?:$|[^\\d\\-\\.])", false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(PhoneNumberTokenizer.PhoneNumberPattern3, "(?:^|[^\\d\\-\\.])", "(?:$|[^\\d\\-\\.])", false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(PhoneNumberTokenizer.PhoneNumberPattern4, "(?:^|[^\\d\\-\\.])", "(?:$|[^\\d\\-\\.])", false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(PhoneNumberTokenizer.PhoneNumberPattern4WithCc, "(?:^|[^\\d\\-\\.\\+])", "(?:$|[^\\d\\-\\.])", false, false, Array.Empty<RegexOptions>())
			});
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00021A77 File Offset: 0x0001FC77
		private static IEnumerable<EntityToken> ProcessMatch(TokenPatternMatch m)
		{
			string value = m.ContentGroup.Value;
			IEnumerable<char> enumerable = value;
			Func<char, bool> func;
			if ((func = PhoneNumberTokenizer.<>O.<1>__IsDigit) == null)
			{
				func = (PhoneNumberTokenizer.<>O.<1>__IsDigit = new Func<char, bool>(char.IsDigit));
			}
			int num = enumerable.Count(func);
			int num2 = value.Count((char c) => c == '.');
			List<Match> list = PhoneNumberTokenizer.DateSeparatorRegex.NonCachingMatches(value).ToList<Match>();
			int count = list.Count;
			int num3 = list.Select((Match m1) => m1.Value).Distinct<string>().Count<string>();
			int num4 = value.Length - num;
			if (num2 == 3 && num4 == num2)
			{
				yield break;
			}
			if (num2 == 1 && count == 1 && num4 == count)
			{
				yield break;
			}
			if (num3 > 1 && num4 == count)
			{
				yield break;
			}
			if (value.StartsWith("+", StringComparison.Ordinal))
			{
				if (num >= 11 && num <= 13)
				{
					yield return new PhoneNumberToken(m.Source, m.Start, m.End);
				}
				yield break;
			}
			if (num != 7 && num != 10)
			{
				yield break;
			}
			yield return new PhoneNumberToken(m.Source, m.Start, m.End);
			yield break;
		}

		// Token: 0x04000597 RID: 1431
		private const int MinDigits = 7;

		// Token: 0x04000598 RID: 1432
		private const int MaxDigits = 10;

		// Token: 0x04000599 RID: 1433
		private const int MinDigitsWithCountryCode = 11;

		// Token: 0x0400059A RID: 1434
		private const int MaxDigitsWithCountryCode = 13;

		// Token: 0x0400059B RID: 1435
		public const string LeftContext = "(?:^|[^\\d\\-\\.])";

		// Token: 0x0400059C RID: 1436
		public const string LeftContextWithCc = "(?:^|[^\\d\\-\\.\\+])";

		// Token: 0x0400059D RID: 1437
		public const string RightContext = "(?:$|[^\\d\\-\\.])";

		// Token: 0x0400059E RID: 1438
		public const string Separator = "[\\-\\.\\s]";

		// Token: 0x0400059F RID: 1439
		private const string CountryCode = "(?:\\+\\d{1,3})";

		// Token: 0x040005A0 RID: 1440
		private static readonly string PhoneNumberPattern1 = FormattableString.Invariant(FormattableStringFactory.Create("\\(\\d{{3}}\\){0}?\\d{{3}}{1}?\\d{{4}}", new object[] { "[\\-\\.\\s]", "[\\-\\.\\s]" }));

		// Token: 0x040005A1 RID: 1441
		private static readonly string PhoneNumberPattern1WithCc = FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}?\\(\\d{{3}}\\){2}?\\d{{3}}{3}?\\d{{4}}", new object[] { "(?:\\+\\d{1,3})", "[\\-\\.\\s]", "[\\-\\.\\s]", "[\\-\\.\\s]" }));

		// Token: 0x040005A2 RID: 1442
		private static readonly string PhoneNumberPattern2 = FormattableString.Invariant(FormattableStringFactory.Create("\\d{{3}}{0}?\\d{{3}}{1}?\\d{{4}}", new object[] { "[\\-\\.\\s]", "[\\-\\.\\s]" }));

		// Token: 0x040005A3 RID: 1443
		private static readonly string PhoneNumberPattern2WithCc = FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}?\\d{{3}}{2}?\\d{{3}}{3}?\\d{{4}}", new object[] { "(?:\\+\\d{1,3})", "[\\-\\.\\s]", "[\\-\\.\\s]", "[\\-\\.\\s]" }));

		// Token: 0x040005A4 RID: 1444
		private static readonly string PhoneNumberPattern3 = FormattableString.Invariant(FormattableStringFactory.Create("\\d{{3}}{0}?\\d{{4}}", new object[] { "[\\-\\.\\s]" }));

		// Token: 0x040005A5 RID: 1445
		private static readonly string PhoneNumberPattern4 = FormattableString.Invariant(FormattableStringFactory.Create("\\d+(?:\\d+{0}?)*\\d+", new object[] { "[\\-\\.\\s]" }));

		// Token: 0x040005A6 RID: 1446
		private static readonly string PhoneNumberPattern4WithCc = FormattableString.Invariant(FormattableStringFactory.Create("\\+\\d{{1,3}}(?:\\d+{0}?)*\\d+", new object[] { "[\\-\\.\\s]" }));

		// Token: 0x040005A7 RID: 1447
		private static readonly Regex DateSeparatorRegex = new Regex("[\\-\\.\\s\\p{Pd}]", RegexOptions.Compiled);

		// Token: 0x020001FD RID: 509
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040005A8 RID: 1448
			public static RegexBasedTokenizer.TokenFactoryDelegate <0>__ProcessMatch;

			// Token: 0x040005A9 RID: 1449
			public static Func<char, bool> <1>__IsDigit;
		}
	}
}
