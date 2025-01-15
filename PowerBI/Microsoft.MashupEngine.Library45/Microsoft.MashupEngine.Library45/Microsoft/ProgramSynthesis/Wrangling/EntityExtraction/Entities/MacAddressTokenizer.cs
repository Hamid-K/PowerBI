using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001EE RID: 494
	public class MacAddressTokenizer : RegexBasedTokenizer
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002052C File Offset: 0x0001E72C
		[JsonConstructor]
		public MacAddressTokenizer()
			: base(OverlapStrategy.None, (TokenPatternMatch m) => Seq.Of<MacAddressToken>(new MacAddressToken[]
			{
				new MacAddressToken(m.Source, m.Start, m.End)
			}), new TokenPattern[]
			{
				new TokenPattern(MacAddressTokenizer.PairMacAddressPatternString, "^|[^\\d\\w\\.\\-\\:]", "$|[^\\d\\w\\.\\-\\:]", false, false, Array.Empty<RegexOptions>()),
				new TokenPattern(MacAddressTokenizer.QuadMacAddressPatternString, "^|[^\\d\\w\\.\\-\\:]", "$|[^\\d\\w\\.\\-\\:]", false, false, Array.Empty<RegexOptions>())
			})
		{
		}

		// Token: 0x04000551 RID: 1361
		private const string PairComponentPatternString = "[A-Fa-f0-9]{2}";

		// Token: 0x04000552 RID: 1362
		private const string QuadComponentPatternString = "[A-Fa-f0-9]{4}";

		// Token: 0x04000553 RID: 1363
		private const string SeparatorPatternString = "(?<Separator>[\\.\\:\\-])";

		// Token: 0x04000554 RID: 1364
		private const string LeftContextPatternString = "^|[^\\d\\w\\.\\-\\:]";

		// Token: 0x04000555 RID: 1365
		private const string RightContextPatternString = "$|[^\\d\\w\\.\\-\\:]";

		// Token: 0x04000556 RID: 1366
		private static readonly string PairMacAddressPatternString = FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}{2}(?:\\k<Separator>{3}){{4}}", new object[] { "[A-Fa-f0-9]{2}", "(?<Separator>[\\.\\:\\-])", "[A-Fa-f0-9]{2}", "[A-Fa-f0-9]{2}" }));

		// Token: 0x04000557 RID: 1367
		private static readonly string QuadMacAddressPatternString = FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}{2}\\k<Separator>{3}", new object[] { "[A-Fa-f0-9]{4}", "(?<Separator>[\\.\\:\\-])", "[A-Fa-f0-9]{4}", "[A-Fa-f0-9]{4}" }));
	}
}
