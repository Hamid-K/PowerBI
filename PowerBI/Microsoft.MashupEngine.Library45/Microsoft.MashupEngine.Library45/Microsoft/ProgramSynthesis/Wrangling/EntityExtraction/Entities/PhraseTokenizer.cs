using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x02000201 RID: 513
	internal class PhraseTokenizer : RegexBasedTokenizer
	{
		// Token: 0x06000B11 RID: 2833 RVA: 0x00021E08 File Offset: 0x00020008
		public PhraseTokenizer()
			: base(OverlapStrategy.None, (TokenPatternMatch m) => Seq.Of<PhraseToken>(new PhraseToken[]
			{
				new PhraseToken(m.Source, m.Start, m.End)
			}), new TokenPattern[]
			{
				new TokenPattern(PhraseTokenizer.PhrasePattern, null, null, false, false, Array.Empty<RegexOptions>())
			})
		{
		}

		// Token: 0x040005B2 RID: 1458
		private static readonly string PhrasePattern = "\\p{L}+(?:(?:(?:\\s?\\p{P}\\s?)|\\s)\\p{L}+)*";
	}
}
