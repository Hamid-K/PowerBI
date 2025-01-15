using System;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x0200016A RID: 362
	public enum SearchOptions
	{
		// Token: 0x040006AD RID: 1709
		None,
		// Token: 0x040006AE RID: 1710
		SkipPunctuation,
		// Token: 0x040006AF RID: 1711
		IncludeExactMatches,
		// Token: 0x040006B0 RID: 1712
		IncludePrefixMatches = 4,
		// Token: 0x040006B1 RID: 1713
		IncludePartialMatches = 8,
		// Token: 0x040006B2 RID: 1714
		SkipPartialMatchesWithoutContentWord = 16
	}
}
