using System;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EB9 RID: 3769
	public interface IMergedSpreadsheetCellProxy : ISpreadsheetCell
	{
		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x060066AD RID: 26285
		ISpreadsheetCell OriginalCell { get; }

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x060066AE RID: 26286
		ISpreadsheetCell ShadowedCell { get; }
	}
}
