using System;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EDC RID: 3804
	[Flags]
	public enum TitleAboveMode
	{
		// Token: 0x04002DE6 RID: 11750
		Invalid = 0,
		// Token: 0x04002DE7 RID: 11751
		AllowGaps = 1,
		// Token: 0x04002DE8 RID: 11752
		MustBeMerged = 2,
		// Token: 0x04002DE9 RID: 11753
		AtMostOneCell = 4,
		// Token: 0x04002DEA RID: 11754
		AtMostTwoCells = 8,
		// Token: 0x04002DEB RID: 11755
		IncludeTopTableRow = 16,
		// Token: 0x04002DEC RID: 11756
		RequireAligned = 32
	}
}
