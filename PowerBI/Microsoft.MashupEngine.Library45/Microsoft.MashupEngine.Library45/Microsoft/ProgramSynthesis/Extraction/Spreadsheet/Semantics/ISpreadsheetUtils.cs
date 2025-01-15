using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EBC RID: 3772
	public static class ISpreadsheetUtils
	{
		// Token: 0x060066BC RID: 26300 RVA: 0x0014F064 File Offset: 0x0014D264
		public static IEnumerable<ISpreadsheetCell> EnumerateCells(this ISpreadsheet spreadsheet, Axis majorAxis = Axis.Horizontal, bool pruneNullStringCells = true, bool pruneHiddenCells = true, bool pruneMergedCellProxies = true)
		{
			ISpreadsheetUtils.<>c__DisplayClass0_0 CS$<>8__locals1 = new ISpreadsheetUtils.<>c__DisplayClass0_0();
			CS$<>8__locals1.spreadsheet = spreadsheet;
			CS$<>8__locals1.prunedCells = CS$<>8__locals1.spreadsheet.PrunedCells(pruneNullStringCells, pruneHiddenCells, pruneMergedCellProxies);
			if (majorAxis != Axis.Horizontal)
			{
				return CS$<>8__locals1.<EnumerateCells>g__EnumerateCellsColumnMajor|1();
			}
			return CS$<>8__locals1.<EnumerateCells>g__EnumerateCellsRowMajor|0();
		}

		// Token: 0x060066BD RID: 26301 RVA: 0x0014F0A3 File Offset: 0x0014D2A3
		public static IEnumerable<ISpreadsheetCell> EnumerateCellsRowMajor(this ISpreadsheet spreadsheet, bool pruneNullStringCells = true, bool pruneHiddenCells = true, bool pruneMergedCellProxies = true)
		{
			return spreadsheet.EnumerateCells(Axis.Horizontal, pruneNullStringCells, pruneHiddenCells, pruneMergedCellProxies);
		}

		// Token: 0x060066BE RID: 26302 RVA: 0x0014F0AF File Offset: 0x0014D2AF
		public static IEnumerable<ISpreadsheetCell> EnumerateCellsColumnMajor(this ISpreadsheet spreadsheet, bool pruneNullStringCells = true, bool pruneHiddenCells = true, bool pruneMergedCellProxies = true)
		{
			return spreadsheet.EnumerateCells(Axis.Vertical, pruneNullStringCells, pruneHiddenCells, pruneMergedCellProxies);
		}

		// Token: 0x060066BF RID: 26303 RVA: 0x0014F0BC File Offset: 0x0014D2BC
		internal static RectangularArray<T> Select<T>(this ISpreadsheet spreadsheet, Func<ISpreadsheetCell, T> func)
		{
			RectangularArray<T> rectangularArray = new RectangularArray<T>(spreadsheet.Size.X, spreadsheet.Size.Y);
			for (int i = 0; i < rectangularArray.Width; i++)
			{
				for (int j = 0; j < rectangularArray.Height; j++)
				{
					rectangularArray[i, j] = func(spreadsheet[i, j]);
				}
			}
			return rectangularArray;
		}
	}
}
