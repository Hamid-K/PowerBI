using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001D8 RID: 472
	public class EmailTokenizer : RegexBasedTokenizer
	{
		// Token: 0x06000A4F RID: 2639 RVA: 0x0001F98C File Offset: 0x0001DB8C
		[JsonConstructor]
		public EmailTokenizer()
			: base(OverlapStrategy.None, (TokenPatternMatch m) => Seq.Of<EmailToken>(new EmailToken[]
			{
				new EmailToken(m.Source, m.Start, m.End, m.FullMatch.Groups["UserName"].Value, m.FullMatch.Groups["DomainName"].Value)
			}), new TokenPattern[]
			{
				new TokenPattern(EmailTokenizer.EmailPattern, "(?:^|[^\\p{L}\\d\\._])", "(?:$|[^\\p{L}\\d\\._])", false, false, Array.Empty<RegexOptions>())
			})
		{
		}

		// Token: 0x04000524 RID: 1316
		private const string LeftContextPattern = "(?:^|[^\\p{L}\\d\\._])";

		// Token: 0x04000525 RID: 1317
		private const string RightContextPattern = "(?:$|[^\\p{L}\\d\\._])";

		// Token: 0x04000526 RID: 1318
		private const string UserNameGroup = "UserName";

		// Token: 0x04000527 RID: 1319
		private const string DomainNameGroup = "DomainName";

		// Token: 0x04000528 RID: 1320
		private static readonly string EmailPattern = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>[\\p{{L}}\\d_\\.]+)@(?<{1}>[\\p{{L}}\\d][\\p{{L}}\\d\\-]*(\\.[\\p{{L}}\\d\\-]+)+)", new object[] { "UserName", "DomainName" }));
	}
}
