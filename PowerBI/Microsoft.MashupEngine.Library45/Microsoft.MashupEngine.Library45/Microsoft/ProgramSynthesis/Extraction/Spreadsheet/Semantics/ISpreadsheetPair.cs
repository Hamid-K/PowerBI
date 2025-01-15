using System;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EB6 RID: 3766
	public interface ISpreadsheetPair
	{
		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x0600669A RID: 26266
		ISpreadsheet WithFormatting { get; }

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x0600669B RID: 26267
		ISpreadsheet WithoutFormatting { get; }
	}
}
