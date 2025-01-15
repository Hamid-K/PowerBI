using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x02001232 RID: 4658
	public struct RegexProfile
	{
		// Token: 0x06008C5C RID: 35932 RVA: 0x001D6E1C File Offset: 0x001D501C
		internal RegexProfile(string regex, IReadOnlyList<string> regexesToExclude)
		{
			this.Regex = regex;
			this.RegexesToExclude = regexesToExclude;
		}

		// Token: 0x04003964 RID: 14692
		public readonly string Regex;

		// Token: 0x04003965 RID: 14693
		public readonly IReadOnlyList<string> RegexesToExclude;
	}
}
