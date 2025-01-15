using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x02000215 RID: 533
	public class UrlTokenizer : RegexBasedTokenizer
	{
		// Token: 0x06000B72 RID: 2930 RVA: 0x00022EF4 File Offset: 0x000210F4
		[JsonConstructor]
		public UrlTokenizer()
			: base(OverlapStrategy.None, (TokenPatternMatch m) => Seq.Of<UrlToken>(new UrlToken[]
			{
				new UrlToken(m.Source, m.Start, m.End)
			}), new TokenPattern[]
			{
				new TokenPattern(UrlTokenizer.UrlPattern, null, null, false, false, new RegexOptions[] { RegexOptions.IgnoreCase })
			})
		{
		}

		// Token: 0x040005F0 RID: 1520
		private static readonly string UrlPattern = "(?:(?:https?|ftp)://)(?:\\S+(?::\\S*)?@)?(:?(?:[1-9]\\d?|1\\d\\d|2[01]\\d|22[0-3])(?:\\.(?:1?\\d{1,2}|2[0-4]\\d|25[0-5])){2}(?:\\.(?:[1-9]\\d?|1\\d\\d|2[0-4]\\d|25[0-4]))|(?:(?:[a-z\\u00a1-\\uffff0-9]-*)*[a-z\\u00a1-\\uffff0-9]+)(?:\\.(?:[a-z\\u00a1-\\uffff0-9]-*)*[a-z\\u00a1-\\uffff0-9]+)*(?:\\.(?:[a-z\\u00a1-\\uffff]{2,}))\\.?)(?::\\d{2,5})?(?:[/?#]\\S*)?";
	}
}
