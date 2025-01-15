using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x0200020E RID: 526
	public class TimeTokenizer : RegexBasedTokenizer
	{
		// Token: 0x06000B59 RID: 2905 RVA: 0x000228E0 File Offset: 0x00020AE0
		[JsonConstructor]
		public TimeTokenizer()
		{
			OverlapStrategy overlapStrategy = OverlapStrategy.Subsumption;
			RegexBasedTokenizer.TokenFactoryDelegate tokenFactoryDelegate;
			if ((tokenFactoryDelegate = TimeTokenizer.<>O.<0>__ProcessMatch) == null)
			{
				tokenFactoryDelegate = (TimeTokenizer.<>O.<0>__ProcessMatch = new RegexBasedTokenizer.TokenFactoryDelegate(TimeTokenizer.ProcessMatch));
			}
			base..ctor(overlapStrategy, tokenFactoryDelegate, new TokenPattern[]
			{
				new TokenPattern(TimeTokenizer.HoursAndMinsPattern, "(?:^|[^\\-\\d\\.\\:])", null, false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(TimeTokenizer.HoursMinAndSecPattern, "(?:^|[^\\-\\d\\.\\:])", null, false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(TimeTokenizer.HoursMinSecMsPattern, "(?:^|[^\\-\\d\\.\\:])", null, false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(TimeTokenizer.MinSecMsPattern, "(?:^|[^\\-\\d\\.\\:])", null, false, false, Array.Empty<RegexOptions>())
			});
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002297D File Offset: 0x00020B7D
		private static IEnumerable<EntityToken> ProcessMatch(TokenPatternMatch m)
		{
			string text = m.FullMatch.Groups["Hours"].Value;
			string text2 = m.FullMatch.Groups["Minutes"].Value;
			string text3 = m.FullMatch.Groups["Seconds"].Value;
			string text4 = m.FullMatch.Groups["Milliseconds"].Value;
			string text5 = m.FullMatch.Groups["AmPm"].Value;
			text = (string.IsNullOrEmpty(text) ? "0" : text);
			text2 = (string.IsNullOrEmpty(text2) ? "0" : text2);
			text3 = (string.IsNullOrEmpty(text3) ? "0" : text3);
			text4 = (string.IsNullOrEmpty(text4) ? "0" : text4);
			text5 = (string.IsNullOrEmpty(text5) ? "" : ((char.ToUpperInvariant(text5[0]) == 'A') ? "AM" : "PM"));
			DateTime dateTime;
			if (DateTime.TryParse(FormattableString.Invariant(FormattableStringFactory.Create("{0}:{1}:{2}.{3} {4}", new object[] { text, text2, text3, text4, text5 })), out dateTime))
			{
				yield return new TimeToken(m.Source, m.Start, m.End, dateTime);
			}
			yield break;
		}

		// Token: 0x040005D9 RID: 1497
		private const string LeftContext = "(?:^|[^\\-\\d\\.\\:])";

		// Token: 0x040005DA RID: 1498
		private const string AmPm = "(?:(?<AmPm>[AaPp])\\.?(?:[Mm]\\.?)?)?";

		// Token: 0x040005DB RID: 1499
		private const string TwoDigits = "\\d{1,2}";

		// Token: 0x040005DC RID: 1500
		private const string WhiteSpace = "\\s*";

		// Token: 0x040005DD RID: 1501
		private const string HoursGroup = "Hours";

		// Token: 0x040005DE RID: 1502
		private const string MinutesGroup = "Minutes";

		// Token: 0x040005DF RID: 1503
		private const string SecondsGroup = "Seconds";

		// Token: 0x040005E0 RID: 1504
		private const string MilliSecondsGroup = "Milliseconds";

		// Token: 0x040005E1 RID: 1505
		private const string AmPmGroup = "AmPm";

		// Token: 0x040005E2 RID: 1506
		private static readonly string HoursAndMinsPattern = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1}){2}\\:{3}(?<{4}>{5}){6}{7}", new object[] { "Hours", "\\d{1,2}", "\\s*", "\\s*", "Minutes", "\\d{1,2}", "\\s*", "(?:(?<AmPm>[AaPp])\\.?(?:[Mm]\\.?)?)?" }));

		// Token: 0x040005E3 RID: 1507
		private static readonly string HoursMinAndSecPattern = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1}){2}\\:{3}(?<{4}>{5}){6}\\:{7}(?<{8}>{9}){10}{11}", new object[]
		{
			"Hours", "\\d{1,2}", "\\s*", "\\s*", "Minutes", "\\d{1,2}", "\\s*", "\\s*", "Seconds", "\\d{1,2}",
			"\\s*", "(?:(?<AmPm>[AaPp])\\.?(?:[Mm]\\.?)?)?"
		}));

		// Token: 0x040005E4 RID: 1508
		private static readonly string HoursMinSecMsPattern = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1}){2}\\:{3}(?<{4}>{5}){6}\\:{7}(?<{8}>{9})\\.(?<{10}>\\d+){11}{12}", new object[]
		{
			"Hours", "\\d{1,2}", "\\s*", "\\s*", "Minutes", "\\d{1,2}", "\\s*", "\\s*", "Seconds", "\\d{1,2}",
			"Milliseconds", "\\s*", "(?:(?<AmPm>[AaPp])\\.?(?:[Mm]\\.?)?)?"
		}));

		// Token: 0x040005E5 RID: 1509
		private static readonly string MinSecMsPattern = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1}){2}\\:{3}(?<{4}>{5})\\.(?<{6}>\\d+)", new object[] { "Minutes", "\\d{1,2}", "\\s*", "\\s*", "Seconds", "\\d{1,2}", "Milliseconds" }));

		// Token: 0x0200020F RID: 527
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040005E6 RID: 1510
			public static RegexBasedTokenizer.TokenFactoryDelegate <0>__ProcessMatch;
		}
	}
}
