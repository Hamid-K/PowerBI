using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001B5 RID: 437
	internal class AlphaNumericTokenizer : RegexBasedTokenizer
	{
		// Token: 0x060009AB RID: 2475 RVA: 0x0001C674 File Offset: 0x0001A874
		public AlphaNumericTokenizer()
			: base(OverlapStrategy.None, (TokenPatternMatch m) => Seq.Of<AlphaNumericToken>(new AlphaNumericToken[]
			{
				new AlphaNumericToken(m.Source, m.Start, m.End)
			}), new TokenPattern[]
			{
				new TokenPattern("(?:\\p{L}|\\d)+", null, null, false, false, Array.Empty<RegexOptions>())
			})
		{
		}

		// Token: 0x04000498 RID: 1176
		private const string AlphaNumericPattern = "(?:\\p{L}|\\d)+";
	}
}
