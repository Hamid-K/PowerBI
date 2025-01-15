using System;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EB8 RID: 3768
	public interface ISpreadsheetCell
	{
		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x060066A5 RID: 26277
		string AsString { get; }

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x060066A6 RID: 26278
		bool IsError { get; }

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x060066A7 RID: 26279
		string Formula { get; }

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x060066A8 RID: 26280
		string FormulaSharedId { get; }

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x060066A9 RID: 26281
		Bounds<TableUnit> Span { get; }

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x060066AA RID: 26282
		bool RowHidden { get; }

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x060066AB RID: 26283
		bool ColumnHidden { get; }

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x060066AC RID: 26284
		ICellStyleInfo StyleInfo { get; }
	}
}
