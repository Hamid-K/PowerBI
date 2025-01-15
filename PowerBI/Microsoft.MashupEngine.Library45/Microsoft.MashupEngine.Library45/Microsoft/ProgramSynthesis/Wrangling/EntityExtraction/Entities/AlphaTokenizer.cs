using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001B8 RID: 440
	internal class AlphaTokenizer : RegexBasedTokenizer
	{
		// Token: 0x060009B3 RID: 2483 RVA: 0x0001C70C File Offset: 0x0001A90C
		public AlphaTokenizer()
			: base(OverlapStrategy.None, (TokenPatternMatch m) => Seq.Of<AlphaToken>(new AlphaToken[]
			{
				new AlphaToken(m.Source, m.Start, m.End)
			}), new TokenPattern[]
			{
				new TokenPattern("\\p{L}+", null, null, false, false, Array.Empty<RegexOptions>())
			})
		{
		}

		// Token: 0x0400049B RID: 1179
		private const string AlphaPattern = "\\p{L}+";
	}
}
