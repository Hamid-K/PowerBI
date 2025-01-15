using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EB7 RID: 3767
	public interface ISpreadsheet
	{
		// Token: 0x17001225 RID: 4645
		ISpreadsheetCell this[int x, int y] { get; }

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x0600669D RID: 26269
		Vector<TableUnit> Size { get; }

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x0600669E RID: 26270
		BorderCollection Borders { get; }

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x0600669F RID: 26271
		Vector<TableUnit> FreezePaneSize { get; }

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x060066A0 RID: 26272
		IReadOnlyList<Bounds<TableUnit>> MergedCells { get; }

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x060066A1 RID: 26273
		IReadOnlyList<DefinedRange> DefinedRanges { get; }

		// Token: 0x060066A2 RID: 26274
		RectangularArray<ISpreadsheetCell> PrunedCells(bool pruneNullStringCells, bool pruneHiddenCells, bool pruneMergedCellProxies);

		// Token: 0x060066A3 RID: 26275
		SpreadsheetArea Trim(Bounds<TableUnit> span, bool pruneHidden, bool pruneMergedCellProxies, bool trimInteriorSlices);

		// Token: 0x060066A4 RID: 26276
		IEnumerable<int> MatchingRows(Bounds<TableUnit> span, StyleFilter styleFilter, SplitMode splitMode);
	}
}
