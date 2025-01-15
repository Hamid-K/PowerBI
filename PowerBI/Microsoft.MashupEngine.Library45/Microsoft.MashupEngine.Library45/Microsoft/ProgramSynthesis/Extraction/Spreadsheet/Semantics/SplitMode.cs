using System;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EDB RID: 3803
	[Flags]
	public enum SplitMode
	{
		// Token: 0x04002DDC RID: 11740
		Any = 0,
		// Token: 0x04002DDD RID: 11741
		FirstCellOnly = 1,
		// Token: 0x04002DDE RID: 11742
		RestOnly = 2,
		// Token: 0x04002DDF RID: 11743
		AllCells = 3,
		// Token: 0x04002DE0 RID: 11744
		Only = 4,
		// Token: 0x04002DE1 RID: 11745
		CellSelectionMask = 7,
		// Token: 0x04002DE2 RID: 11746
		RequirePrecedingBlank = 8,
		// Token: 0x04002DE3 RID: 11747
		RequirePrecedingAtMostOne = 16,
		// Token: 0x04002DE4 RID: 11748
		RequirePrecedingMask = 24
	}
}
