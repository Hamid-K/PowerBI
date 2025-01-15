using System;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x0200016C RID: 364
	public sealed class SearchSettings
	{
		// Token: 0x06000719 RID: 1817 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		public SearchSettings(SearchOptions options, int? maxResultsPerQuery = null, int? maxPrefixTermExpansion = null)
		{
			this.Options = options;
			this.MaxResultsPerQuery = maxResultsPerQuery ?? 10;
			this.MaxPrefixTermExpansion = maxPrefixTermExpansion ?? 25;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0000C32A File Offset: 0x0000A52A
		public SearchOptions Options { get; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0000C332 File Offset: 0x0000A532
		public int MaxResultsPerQuery { get; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0000C33A File Offset: 0x0000A53A
		public int MaxPrefixTermExpansion { get; }
	}
}
