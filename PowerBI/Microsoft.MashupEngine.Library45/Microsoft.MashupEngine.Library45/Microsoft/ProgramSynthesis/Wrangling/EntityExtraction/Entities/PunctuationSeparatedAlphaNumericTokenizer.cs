using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x02000204 RID: 516
	internal class PunctuationSeparatedAlphaNumericTokenizer : RegexBasedTokenizer
	{
		// Token: 0x06000B1B RID: 2843 RVA: 0x00021F60 File Offset: 0x00020160
		public PunctuationSeparatedAlphaNumericTokenizer()
			: base(OverlapStrategy.None, (TokenPatternMatch m) => Seq.Of<PunctuationSeparatedAlphaNumericToken>(new PunctuationSeparatedAlphaNumericToken[]
			{
				new PunctuationSeparatedAlphaNumericToken(m.Source, m.Start, m.End)
			}), new TokenPattern[]
			{
				new TokenPattern(PunctuationSeparatedAlphaNumericTokenizer.DashedAlphaNumericPattern, null, null, false, false, Array.Empty<RegexOptions>())
			})
		{
		}

		// Token: 0x040005B6 RID: 1462
		private static readonly string DashedAlphaNumericPattern = "(?:\\p{L}|\\d)+(?:[\\p{P}](?:\\p{L}|\\d)+)+";
	}
}
