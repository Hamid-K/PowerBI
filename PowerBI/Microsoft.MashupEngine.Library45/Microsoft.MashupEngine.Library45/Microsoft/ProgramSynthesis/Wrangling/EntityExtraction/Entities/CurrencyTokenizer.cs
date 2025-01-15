using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001BF RID: 447
	public class CurrencyTokenizer : RegexBasedTokenizer
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
		[JsonConstructor]
		public CurrencyTokenizer()
		{
			OverlapStrategy overlapStrategy = OverlapStrategy.Subsumption;
			RegexBasedTokenizer.TokenFactoryDelegate tokenFactoryDelegate;
			if ((tokenFactoryDelegate = CurrencyTokenizer.<>O.<0>__ProcessMatch) == null)
			{
				tokenFactoryDelegate = (CurrencyTokenizer.<>O.<0>__ProcessMatch = new RegexBasedTokenizer.TokenFactoryDelegate(CurrencyTokenizer.ProcessMatch));
			}
			base..ctor(overlapStrategy, tokenFactoryDelegate, Array.Empty<TokenPattern>());
			base.Initialize(NumericTokenizer.AllSeparatedPatternStrings.SelectMany(delegate(string p)
			{
				string text = FormattableString.Invariant(FormattableStringFactory.Create("{0}\\s*{1}", new object[]
				{
					CurrencyTokenizer.CurrencyPattern,
					p
				}));
				string text2 = FormattableString.Invariant(FormattableStringFactory.Create("{0}\\s*{1}", new object[]
				{
					p,
					CurrencyTokenizer.CurrencyPattern
				}));
				return new TokenPattern[]
				{
					new TokenPattern(text, "(?:^|[^\\p{Sc}])", "(?:$|[^\\d])", false, false, Array.Empty<RegexOptions>()),
					new TokenPattern(text2, "(?:^|[^\\d])", "(?:$|[^\\p{Sc}])", false, false, Array.Empty<RegexOptions>())
				};
			}));
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0001CB53 File Offset: 0x0001AD53
		private static IEnumerable<EntityToken> ProcessMatch(TokenPatternMatch m)
		{
			double num;
			if (!NumericTokenizer.TryParseNumberMatch(m.FullMatch, out num))
			{
				yield break;
			}
			string value = m.FullMatch.Groups["CurrencySymbol"].Value;
			yield return new CurrencyToken(m.Source, m.Start, m.End, num, value);
			yield break;
		}

		// Token: 0x040004AC RID: 1196
		private const string PrefixCurrencyLeftContextPattern = "(?:^|[^\\p{Sc}])";

		// Token: 0x040004AD RID: 1197
		private const string PrefixCurrencyRightContextPattern = "(?:$|[^\\d])";

		// Token: 0x040004AE RID: 1198
		private const string SuffixCurrencyLeftContextPattern = "(?:^|[^\\d])";

		// Token: 0x040004AF RID: 1199
		private const string SuffixCurrencyRightContextPattern = "(?:$|[^\\p{Sc}])";

		// Token: 0x040004B0 RID: 1200
		private static readonly string CurrencyPattern = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>\\p{{Sc}})", new object[] { "CurrencySymbol" }));

		// Token: 0x040004B1 RID: 1201
		private const string CurrencyGroupName = "CurrencySymbol";

		// Token: 0x020001C0 RID: 448
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040004B2 RID: 1202
			public static RegexBasedTokenizer.TokenFactoryDelegate <0>__ProcessMatch;
		}
	}
}
