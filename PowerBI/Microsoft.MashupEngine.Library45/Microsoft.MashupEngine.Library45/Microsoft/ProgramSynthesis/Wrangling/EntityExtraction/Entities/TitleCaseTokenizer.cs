using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x02000212 RID: 530
	internal class TitleCaseTokenizer : RegexBasedTokenizer
	{
		// Token: 0x06000B69 RID: 2921 RVA: 0x00022E50 File Offset: 0x00021050
		public TitleCaseTokenizer()
			: base(OverlapStrategy.None, (TokenPatternMatch m) => Seq.Of<TitleCaseToken>(new TitleCaseToken[]
			{
				new TitleCaseToken(m.Source, m.Start, m.End)
			}), new TokenPattern[]
			{
				new TokenPattern(TitleCaseTokenizer.TitleCasePattern, null, null, false, false, Array.Empty<RegexOptions>())
			})
		{
		}

		// Token: 0x040005ED RID: 1517
		private static readonly string TitleCasePattern = "(?:\\p{Lu}\\p{Ll}+)+";
	}
}
