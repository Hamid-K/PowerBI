using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001C5 RID: 453
	public class DashedNumbersTokenizer : RegexBasedTokenizer
	{
		// Token: 0x060009E8 RID: 2536 RVA: 0x0001CE44 File Offset: 0x0001B044
		[JsonConstructor]
		public DashedNumbersTokenizer()
			: base(OverlapStrategy.None, null, new TokenPattern[]
			{
				new TokenPattern(DashedNumbersTokenizer.BaseDashedPattern, "^|[^\\p{Pd}]", "$|[^\\p{Pd}]", false, false, Array.Empty<RegexOptions>())
			})
		{
			this.Initialize();
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0001CF28 File Offset: 0x0001B128
		private IEnumerable<EntityToken> ProcessMatch(TokenPatternMatch m)
		{
			string matchedValue = m.Value;
			int offset = m.Start;
			yield return new DashedNumbersToken(m.Source, m.Start, m.End);
			IEnumerable<TokenPatternMatch> enumerable = this._ccTokenPattern.Matches(matchedValue);
			foreach (TokenPatternMatch tokenPatternMatch in enumerable)
			{
				yield return new CreditCardNumberToken(m.Source, offset + tokenPatternMatch.Start, offset + tokenPatternMatch.End);
			}
			IEnumerator<TokenPatternMatch> enumerator = null;
			IEnumerable<TokenPatternMatch> enumerable2 = this._maskedCcTokenPattern.Matches(matchedValue);
			foreach (TokenPatternMatch tokenPatternMatch2 in enumerable2)
			{
				yield return new MaskedCreditCardNumberToken(m.Source, offset + tokenPatternMatch2.Start, offset + tokenPatternMatch2.End);
			}
			enumerator = null;
			IEnumerable<TokenPatternMatch> enumerable3 = this._ssnTokenPattern.Matches(matchedValue);
			foreach (TokenPatternMatch tokenPatternMatch3 in enumerable3)
			{
				yield return new SocialSecurityNumberToken(m.Source, offset + tokenPatternMatch3.Start, offset + tokenPatternMatch3.End);
			}
			enumerator = null;
			IEnumerable<TokenPatternMatch> enumerable4 = this._maskedSsnTokenPattern.Matches(matchedValue);
			foreach (TokenPatternMatch tokenPatternMatch4 in enumerable4)
			{
				yield return new MaskedSocialSecurityNumberToken(m.Source, offset + tokenPatternMatch4.Start, offset + tokenPatternMatch4.End);
			}
			enumerator = null;
			IEnumerable<TokenPatternMatch> enumerable5 = this._guidTokenPattern.Matches(matchedValue);
			foreach (TokenPatternMatch tokenPatternMatch5 in enumerable5)
			{
				yield return new GuidToken(m.Source, offset + tokenPatternMatch5.Start, offset + tokenPatternMatch5.End);
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0001CF3F File Offset: 0x0001B13F
		private void Initialize()
		{
			base.TokenFactory = new RegexBasedTokenizer.TokenFactoryDelegate(this.ProcessMatch);
		}

		// Token: 0x040004BD RID: 1213
		private const string AlphaNumericPattern = "[a-zA-Z0-9]";

		// Token: 0x040004BE RID: 1214
		private const string LeftContextPattern = "^|[^\\p{Pd}]";

		// Token: 0x040004BF RID: 1215
		private const string RightContextPattern = "$|[^\\p{Pd}]";

		// Token: 0x040004C0 RID: 1216
		private const string CreditCardNumberPattern = "\\d{4}(?:\\p{Pd}\\d{4}){3}";

		// Token: 0x040004C1 RID: 1217
		private const string SocialSecurityNumberPattern = "\\d{3}\\p{Pd}\\d{2}\\p{Pd}\\d{4}";

		// Token: 0x040004C2 RID: 1218
		private const string MaskedSocialSecurityNumberPattern = "\\p{L}{3}\\p{Pd}\\p{L}{2}\\p{Pd}\\d{4}";

		// Token: 0x040004C3 RID: 1219
		private const string HexDigitPattern = "[A-Fa-f0-9]";

		// Token: 0x040004C4 RID: 1220
		private static readonly string BaseDashedPattern = FormattableString.Invariant(FormattableStringFactory.Create("(?:{0})+(?:\\p{{Pd}}(?:{1}+))+", new object[] { "[a-zA-Z0-9]", "[a-zA-Z0-9]" }));

		// Token: 0x040004C5 RID: 1221
		private static readonly string MaskedCreditCardNumberPattern = "\\p{L}{4}(?:\\p{Pd}\\p{L}{4}){2}(?:\\p{Pd}\\d{4})";

		// Token: 0x040004C6 RID: 1222
		private static readonly string GuidPattern = FormattableString.Invariant(FormattableStringFactory.Create("{0}{{8}}\\p{{Pd}}{1}{{4}}\\p{{Pd}}{2}{{4}}\\p{{Pd}}{3}{{4}}\\p{{Pd}}{4}{{12}}", new object[] { "[A-Fa-f0-9]", "[A-Fa-f0-9]", "[A-Fa-f0-9]", "[A-Fa-f0-9]", "[A-Fa-f0-9]" }));

		// Token: 0x040004C7 RID: 1223
		private readonly TokenPattern _ccTokenPattern = new TokenPattern("\\d{4}(?:\\p{Pd}\\d{4}){3}", "^|[^\\p{Pd}]", "$|[^\\p{Pd}]", false, false, Array.Empty<RegexOptions>());

		// Token: 0x040004C8 RID: 1224
		private readonly TokenPattern _guidTokenPattern = new TokenPattern(DashedNumbersTokenizer.GuidPattern, "^|[^\\p{Pd}]", "$|[^\\p{Pd}]", false, false, Array.Empty<RegexOptions>());

		// Token: 0x040004C9 RID: 1225
		private readonly TokenPattern _maskedCcTokenPattern = new TokenPattern(DashedNumbersTokenizer.MaskedCreditCardNumberPattern, "^|[^\\p{Pd}]", "$|[^\\p{Pd}]", false, false, Array.Empty<RegexOptions>());

		// Token: 0x040004CA RID: 1226
		private readonly TokenPattern _maskedSsnTokenPattern = new TokenPattern("\\p{L}{3}\\p{Pd}\\p{L}{2}\\p{Pd}\\d{4}", "^|[^\\p{Pd}]", "$|[^\\p{Pd}]", false, false, Array.Empty<RegexOptions>());

		// Token: 0x040004CB RID: 1227
		private readonly TokenPattern _ssnTokenPattern = new TokenPattern("\\d{3}\\p{Pd}\\d{2}\\p{Pd}\\d{4}", "^|[^\\p{Pd}]", "$|[^\\p{Pd}]", false, false, Array.Empty<RegexOptions>());
	}
}
