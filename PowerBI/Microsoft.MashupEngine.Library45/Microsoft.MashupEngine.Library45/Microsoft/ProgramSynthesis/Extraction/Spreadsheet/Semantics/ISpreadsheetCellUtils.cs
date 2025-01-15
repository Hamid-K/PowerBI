using System;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EBB RID: 3771
	public static class ISpreadsheetCellUtils
	{
		// Token: 0x060066BB RID: 26299 RVA: 0x0014F050 File Offset: 0x0014D250
		public static bool IsHidden(this ISpreadsheetCell cell)
		{
			return cell.RowHidden || cell.ColumnHidden;
		}
	}
}
