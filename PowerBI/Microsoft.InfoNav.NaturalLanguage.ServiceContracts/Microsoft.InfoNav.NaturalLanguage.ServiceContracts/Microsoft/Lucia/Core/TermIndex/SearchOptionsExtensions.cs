using System;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x0200016B RID: 363
	public static class SearchOptionsExtensions
	{
		// Token: 0x06000713 RID: 1811 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		public static bool SkipPunctuation(this SearchOptions options)
		{
			return SearchOptionsExtensions.HasFlag(options, SearchOptions.SkipPunctuation);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000C2A9 File Offset: 0x0000A4A9
		public static bool IncludeExactMatches(this SearchOptions options)
		{
			return SearchOptionsExtensions.HasFlag(options, SearchOptions.IncludeExactMatches);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0000C2B2 File Offset: 0x0000A4B2
		public static bool IncludePrefixMatches(this SearchOptions options)
		{
			return SearchOptionsExtensions.HasFlag(options, SearchOptions.IncludePrefixMatches);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0000C2BB File Offset: 0x0000A4BB
		public static bool IncludePartialMatches(this SearchOptions options)
		{
			return SearchOptionsExtensions.HasFlag(options, SearchOptions.IncludePartialMatches);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0000C2C4 File Offset: 0x0000A4C4
		public static bool SkipPartialMatchesWithoutContentWord(this SearchOptions options)
		{
			return SearchOptionsExtensions.HasFlag(options, SearchOptions.SkipPartialMatchesWithoutContentWord);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0000C2CE File Offset: 0x0000A4CE
		private static bool HasFlag(SearchOptions options, SearchOptions flag)
		{
			return (options & flag) == flag;
		}
	}
}
