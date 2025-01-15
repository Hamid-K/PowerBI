using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EDD RID: 3805
	public static class Semantics
	{
		// Token: 0x06006790 RID: 26512 RVA: 0x00151C0F File Offset: 0x0014FE0F
		public static ISpreadsheet WithFormatting(ISpreadsheetPair sheetPair)
		{
			return sheetPair.WithFormatting;
		}

		// Token: 0x06006791 RID: 26513 RVA: 0x00151C17 File Offset: 0x0014FE17
		public static SpreadsheetArea WholeSheet(ISpreadsheet spreadsheet)
		{
			return new SpreadsheetArea(spreadsheet, new Bounds<TableUnit>(0, spreadsheet.Size.X - 1, 0, spreadsheet.Size.Y - 1), null, null, null);
		}

		// Token: 0x06006792 RID: 26514 RVA: 0x00151C44 File Offset: 0x0014FE44
		public static SpreadsheetArea Area(ISpreadsheet spreadsheet, int left, int top, int right, int bottom)
		{
			return SpreadsheetArea.MaybeCreateSpreadsheetArea(spreadsheet, new Bounds<TableUnit>(left, right, top, bottom), null, null).OrElseDefault<SpreadsheetArea>();
		}

		// Token: 0x06006793 RID: 26515 RVA: 0x00151C6C File Offset: 0x0014FE6C
		public static SpreadsheetArea DefinedRange(ISpreadsheet spreadsheet, string name)
		{
			IReadOnlyList<DefinedRange> definedRanges = spreadsheet.DefinedRanges;
			DefinedRange definedRange = ((definedRanges != null) ? definedRanges.FirstOrDefault((DefinedRange dr) => dr.Name == name) : null);
			if (definedRange == null)
			{
				return null;
			}
			SpreadsheetArea spreadsheetArea = SpreadsheetArea.MaybeCreateSpreadsheetArea(spreadsheet, definedRange.Span, null, null).OrElseDefault<SpreadsheetArea>();
			if (spreadsheetArea == null)
			{
				return null;
			}
			return spreadsheetArea.WithName(definedRange.InternalName ? null : definedRange.Name);
		}

		// Token: 0x06006794 RID: 26516 RVA: 0x00151CDC File Offset: 0x0014FEDC
		public static SpreadsheetArea FreezePaneTight(ISpreadsheet spreadsheet)
		{
			if (spreadsheet.FreezePaneSize == null)
			{
				return null;
			}
			int num = Math.Max(0, spreadsheet.FreezePaneSize.X - 1);
			int num2 = Math.Max(0, spreadsheet.FreezePaneSize.Y - 1);
			return new SpreadsheetArea(spreadsheet, new Bounds<TableUnit>(num, spreadsheet.Size.X - 1, num2, spreadsheet.Size.Y - 1), null, null, null);
		}

		// Token: 0x06006795 RID: 26517 RVA: 0x00151D49 File Offset: 0x0014FF49
		public static SpreadsheetArea FreezePaneToBlanks(ISpreadsheet spreadsheet)
		{
			return Semantics.FreezePaneToBlanks(spreadsheet, 1);
		}

		// Token: 0x06006796 RID: 26518 RVA: 0x00151D52 File Offset: 0x0014FF52
		public static SpreadsheetArea FreezePaneToMultipleBlanks(ISpreadsheet spreadsheet)
		{
			return Semantics.FreezePaneToBlanks(spreadsheet, 2);
		}

		// Token: 0x06006797 RID: 26519 RVA: 0x00151D5C File Offset: 0x0014FF5C
		private static SpreadsheetArea FreezePaneToBlanks(ISpreadsheet spreadsheet, int minNumBlanks)
		{
			if (spreadsheet.FreezePaneSize == null)
			{
				return null;
			}
			SpreadsheetArea wholeSheet = Semantics.WholeSheet(spreadsheet);
			Vector<TableUnit> vector = new Vector<TableUnit>(delegate(Axis axis)
			{
				IEnumerable<Record<int, IEnumerable<ISpreadsheetCell>>> enumerable = wholeSheet.EnumerateSlices(axis.Perpendicular(), true, true, false, Derivative.Decreasing, false);
				int num = 0;
				Func<ISpreadsheetCell, bool> <>9__1;
				foreach (Record<int, IEnumerable<ISpreadsheetCell>> record in enumerable)
				{
					int num2;
					IEnumerable<ISpreadsheetCell> enumerable2;
					record.Deconstruct(out num2, out enumerable2);
					int num3 = num2;
					IEnumerable<ISpreadsheetCell> enumerable3 = enumerable2;
					if (num3 < spreadsheet.FreezePaneSize[axis] - 1)
					{
						IEnumerable<ISpreadsheetCell> enumerable4 = enumerable3;
						Func<ISpreadsheetCell, bool> func;
						if ((func = <>9__1) == null)
						{
							func = (<>9__1 = (ISpreadsheetCell cell) => cell.Span[axis.Perpendicular()].Max > spreadsheet.FreezePaneSize[axis.Perpendicular()]);
						}
						if (enumerable4.Any(func))
						{
							num = 0;
						}
						else if (++num >= minNumBlanks)
						{
							return num3 + num;
						}
					}
				}
				return 0;
			});
			if (vector.X == 0 && vector.Y == 0)
			{
				return null;
			}
			return new SpreadsheetArea(spreadsheet, new Bounds<TableUnit>(vector, wholeSheet.Span.Corner(Ordinal.BottomRight)), null, null, null);
		}

		// Token: 0x06006798 RID: 26520 RVA: 0x00151DEF File Offset: 0x0014FFEF
		public static SpreadsheetArea Trim(SpreadsheetArea area)
		{
			return area.Trim(false, true, true);
		}

		// Token: 0x06006799 RID: 26521 RVA: 0x00151DFA File Offset: 0x0014FFFA
		public static SpreadsheetArea TrimHidden(SpreadsheetArea area)
		{
			return area.Trim(true, true, true);
		}

		// Token: 0x0600679A RID: 26522 RVA: 0x00151E05 File Offset: 0x00150005
		public static SpreadsheetArea TrimHiddenKeepingMergedCellProxies(SpreadsheetArea area)
		{
			return area.Trim(true, false, false);
		}

		// Token: 0x0600679B RID: 26523 RVA: 0x00151E10 File Offset: 0x00150010
		private static SpreadsheetArea TrimEdgeSingleCellSlices(SpreadsheetArea area, Direction dir, bool unlessSingleError = false)
		{
			return Semantics.TrimEdgeFewCellSlices(area, dir, 1, unlessSingleError);
		}

		// Token: 0x0600679C RID: 26524 RVA: 0x00151E1C File Offset: 0x0015001C
		private static SpreadsheetArea TrimEdgeFewCellSlices(SpreadsheetArea area, Direction dir, int maxNonEmpty, bool unlessFewError = false)
		{
			Optional<int> optional = from r in area.EnumerateSlices(dir.AlignedAxis().Perpendicular(), true, false, true, dir.Opposite().Derivative(), false).SkipWhile(delegate(Record<int, IEnumerable<ISpreadsheetCell>> g)
				{
					if (g.Item2.HasAtLeast(maxNonEmpty + 1))
					{
						return false;
					}
					if (unlessFewError)
					{
						return !g.Item2.Any((ISpreadsheetCell cell) => cell.IsError);
					}
					return true;
				}).MaybeFirst<Record<int, IEnumerable<ISpreadsheetCell>>>()
				select r.Item1;
			if (!optional.HasValue)
			{
				return null;
			}
			return area.TrimToSpan(area.Span.With(dir, optional.Value));
		}

		// Token: 0x0600679D RID: 26525 RVA: 0x00151EBF File Offset: 0x001500BF
		public static SpreadsheetArea TrimTopSingleCellRows(SpreadsheetArea area)
		{
			return Semantics.TrimEdgeSingleCellSlices(area, Direction.Up, false);
		}

		// Token: 0x0600679E RID: 26526 RVA: 0x00151ECC File Offset: 0x001500CC
		public static SpreadsheetArea TrimTopSingleLeftCellRows(SpreadsheetArea area)
		{
			Func<ISpreadsheetCell, bool> <>9__2;
			return area.EnumerateRows(true, false, true, Derivative.Increasing).MaybeFirst(delegate(Record<int, IEnumerable<ISpreadsheetCell>> row)
			{
				IEnumerable<ISpreadsheetCell> enumerable = row.Item2.Take(2);
				Func<ISpreadsheetCell, bool> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (ISpreadsheetCell cell) => cell.Span.Left > area.Span.Left);
				}
				return enumerable.Any(func);
			}).Select2((int newTop, IEnumerable<ISpreadsheetCell> _) => area.TrimToSpan(area.Span.With(Direction.Up, newTop)))
				.OrElseDefault<SpreadsheetArea>();
		}

		// Token: 0x0600679F RID: 26527 RVA: 0x00151F1C File Offset: 0x0015011C
		public static SpreadsheetArea TrimRightSingleCellColumns(SpreadsheetArea area)
		{
			return Semantics.TrimEdgeSingleCellSlices(area, Direction.Right, false);
		}

		// Token: 0x060067A0 RID: 26528 RVA: 0x00151F26 File Offset: 0x00150126
		public static SpreadsheetArea TrimLeftSingleCellColumns(SpreadsheetArea area)
		{
			return Semantics.TrimEdgeSingleCellSlices(area, Direction.Left, false);
		}

		// Token: 0x060067A1 RID: 26529 RVA: 0x00151F30 File Offset: 0x00150130
		private static SpreadsheetArea _TrimTopMergedCellRows(SpreadsheetArea area, bool fullWidthOnly)
		{
			if (area.Span.Width() == 1)
			{
				return area;
			}
			int width = (fullWidthOnly ? area.Span.Width() : 2);
			Func<ISpreadsheetCell, bool> <>9__2;
			Optional<int> optional = area.EnumerateRows(true, true, true, Derivative.Increasing).SkipWhile(delegate(Record<int, IEnumerable<ISpreadsheetCell>> g)
			{
				IEnumerable<ISpreadsheetCell> item = g.Item2;
				Func<ISpreadsheetCell, bool> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (ISpreadsheetCell cell) => cell.Span.Width() >= width);
				}
				return item.All(func);
			}).MaybeFirst<Record<int, IEnumerable<ISpreadsheetCell>>>()
				.Select2((int idx, IEnumerable<ISpreadsheetCell> row) => idx);
			if (!optional.HasValue)
			{
				return null;
			}
			return area.TrimToSpan(area.Span.With(Direction.Up, optional.Value));
		}

		// Token: 0x060067A2 RID: 26530 RVA: 0x00151FDC File Offset: 0x001501DC
		public static SpreadsheetArea TrimTopFullWidthMergedCellRows(SpreadsheetArea area)
		{
			return Semantics._TrimTopMergedCellRows(area, true);
		}

		// Token: 0x060067A3 RID: 26531 RVA: 0x00151FE5 File Offset: 0x001501E5
		public static SpreadsheetArea TrimTopMergedCellRows(SpreadsheetArea area)
		{
			return Semantics._TrimTopMergedCellRows(area, false);
		}

		// Token: 0x060067A4 RID: 26532 RVA: 0x00151FEE File Offset: 0x001501EE
		public static SpreadsheetArea TrimBottomSingleCellRows(SpreadsheetArea area)
		{
			return Semantics.TrimEdgeSingleCellSlices(area, Direction.Down, false);
		}

		// Token: 0x060067A5 RID: 26533 RVA: 0x00151FF8 File Offset: 0x001501F8
		private static IEnumerable<Range<TableUnit>> EmptySliceIndexes(SpreadsheetArea area, Direction dir)
		{
			Semantics.<>c__DisplayClass21_0 CS$<>8__locals1 = new Semantics.<>c__DisplayClass21_0();
			Axis axis = dir.AlignedAxis();
			Axis axis2 = axis.Perpendicular();
			Semantics.<>c__DisplayClass21_0 CS$<>8__locals2 = CS$<>8__locals1;
			Func<ISpreadsheetCell, bool> func;
			if (axis != Axis.Horizontal)
			{
				func = (ISpreadsheetCell cell) => !cell.ColumnHidden;
			}
			else
			{
				func = (ISpreadsheetCell cell) => !cell.RowHidden;
			}
			CS$<>8__locals2.nonHiddenCellCheck = func;
			return area.EnumerateSlices(axis2, true, false, false, dir.Opposite().Derivative(), true).Where2((int idx, IEnumerable<ISpreadsheetCell> slice) => slice != null && !slice.Any(CS$<>8__locals1.nonHiddenCellCheck)).Select2((int idx, IEnumerable<ISpreadsheetCell> slice) => idx)
				.AsRanges<TableUnit>();
		}

		// Token: 0x060067A6 RID: 26534 RVA: 0x001520B4 File Offset: 0x001502B4
		private static IEnumerable<Range<TableUnit>> NonEmptySliceIndexes(SpreadsheetArea area, Axis axis)
		{
			IEnumerable<Range<TableUnit>> enumerable = Semantics.EmptySliceIndexes(area, axis.DecreasingDirection());
			return area.Span[axis].Subtract(enumerable);
		}

		// Token: 0x060067A7 RID: 26535 RVA: 0x001520E8 File Offset: 0x001502E8
		private static SpreadsheetArea TrimEdgeEmptySlices(SpreadsheetArea area, Direction dir, int k)
		{
			return Semantics.EmptySliceIndexes(area, (k < 0) ? dir.Opposite() : dir).MaybeElementAt((k < 0) ? (-k - 1) : k).Select(delegate(Range<TableUnit> emptyRange)
			{
				Optional<Bounds<TableUnit>> optional = area.Span.MaybeWith(dir, emptyRange.Expand(1)[dir.Opposite().Derivative()]);
				if (!optional.HasValue)
				{
					return null;
				}
				return area.TrimToSpan(optional.Value);
			})
				.OrElse(area);
		}

		// Token: 0x060067A8 RID: 26536 RVA: 0x00152157 File Offset: 0x00150357
		public static SpreadsheetArea TakeUntilEmptyRow(SpreadsheetArea area)
		{
			return Semantics.TrimEdgeEmptySlices(area, Direction.Down, -1);
		}

		// Token: 0x060067A9 RID: 26537 RVA: 0x00152161 File Offset: 0x00150361
		public static SpreadsheetArea TakeAfterEmptyRow(SpreadsheetArea area)
		{
			return Semantics.TrimEdgeEmptySlices(area, Direction.Up, 0);
		}

		// Token: 0x060067AA RID: 26538 RVA: 0x0015216B File Offset: 0x0015036B
		public static SpreadsheetArea TakeUntilEmptyColumn(SpreadsheetArea area)
		{
			return Semantics.TrimEdgeEmptySlices(area, Direction.Right, -1);
		}

		// Token: 0x060067AB RID: 26539 RVA: 0x00152178 File Offset: 0x00150378
		public static SpreadsheetArea[] BorderedAreas(SpreadsheetArea area)
		{
			return (from @group in area.Spreadsheet.Borders.BorderGroups
				select @group.Span into span
				where area.Span.Contains(span)
				select area.TrimToSpan(span)).ToArray<SpreadsheetArea>();
		}

		// Token: 0x060067AC RID: 26540 RVA: 0x001521F4 File Offset: 0x001503F4
		private static SpreadsheetArea[] SplitOnEmptySlices(SpreadsheetArea area, Axis axis)
		{
			return Semantics.NonEmptySliceIndexes(area, axis).Collect((Range<TableUnit> range) => area.TrimToSpan(area.Span.With(axis, range))).ToArray<SpreadsheetArea>();
		}

		// Token: 0x060067AD RID: 26541 RVA: 0x0015223C File Offset: 0x0015043C
		public static SpreadsheetArea[] SplitOnEmptyRows(SpreadsheetArea area)
		{
			return Semantics.SplitOnEmptySlices(area, Axis.Vertical);
		}

		// Token: 0x060067AE RID: 26542 RVA: 0x00152245 File Offset: 0x00150445
		public static SpreadsheetArea[] SplitOnMatchingRows(SpreadsheetArea area, StyleFilter styleFilter, SplitMode splitMode)
		{
			return Semantics.SplitOnMatchingRows(area, styleFilter, splitMode, false);
		}

		// Token: 0x060067AF RID: 26543 RVA: 0x00152250 File Offset: 0x00150450
		private static SpreadsheetArea[] SplitOnMatchingRows(SpreadsheetArea area, StyleFilter styleFilter, SplitMode splitMode, bool allowPartialWidth)
		{
			if (area.Span.Height() >= 2 && (allowPartialWidth || area.Span.Width() == area.Spreadsheet.Size.X))
			{
				return new Ranges<TableUnit>(area.Spreadsheet.MatchingRows(area.Span, styleFilter, splitMode), false).Select((Range<TableUnit> range) => range.Min).PrependItem(area.Span.Top).AppendItem(area.Span.Bottom + 1)
					.Windowed((int top, int nextTop) => area.TrimToSpan(area.Span.With(Axis.Vertical, new Range<TableUnit>(top, nextTop - 1))))
					.ToArray<SpreadsheetArea>();
			}
			return null;
		}

		// Token: 0x060067B0 RID: 26544 RVA: 0x00152344 File Offset: 0x00150544
		public static SpreadsheetArea[] SplitOnEmptyColumns(SpreadsheetArea area)
		{
			return Semantics.SplitOnEmptySlices(area, Axis.Horizontal);
		}

		// Token: 0x060067B1 RID: 26545 RVA: 0x00152350 File Offset: 0x00150550
		private static SpreadsheetArea TrimToFirstBorderInDirection(SpreadsheetArea area, Direction dir)
		{
			return (from firstBorder in area.BordersOrderedByDirection(dir.Opposite(), true).MaybeFirst<Border>()
				select area.TrimToSpan(area.Span.With(dir, firstBorder.Line.Position + ((dir.Derivative() == Derivative.Increasing) ? (-1) : 0)))).OrElseDefault<SpreadsheetArea>();
		}

		// Token: 0x060067B2 RID: 26546 RVA: 0x001523A3 File Offset: 0x001505A3
		public static SpreadsheetArea TrimBelowTopBorder(SpreadsheetArea area)
		{
			return Semantics.TrimToFirstBorderInDirection(area, Direction.Up);
		}

		// Token: 0x060067B3 RID: 26547 RVA: 0x001523AC File Offset: 0x001505AC
		public static SpreadsheetArea TrimAboveBottomBorder(SpreadsheetArea area)
		{
			return Semantics.TrimToFirstBorderInDirection(area, Direction.Down);
		}

		// Token: 0x060067B4 RID: 26548 RVA: 0x001523B5 File Offset: 0x001505B5
		public static ISpreadsheet WithoutFormatting(ISpreadsheetPair sheetPair)
		{
			return sheetPair.WithoutFormatting;
		}

		// Token: 0x060067B5 RID: 26549 RVA: 0x001523C0 File Offset: 0x001505C0
		private static SpreadsheetArea RemoveEmptySlices(SpreadsheetArea area, Axis axis)
		{
			Axis axis2 = axis.Perpendicular();
			SpreadsheetArea spreadsheetArea = area.Trim(false, true, true);
			if (spreadsheetArea == null)
			{
				return null;
			}
			Range<TableUnit> range = spreadsheetArea.Span[axis2];
			return new SpreadsheetArea(area.Spreadsheet, area.Span.With(axis2, range), (axis == Axis.Vertical) ? spreadsheetArea.IncludedColumns : area.IncludedColumns, (axis == Axis.Horizontal) ? spreadsheetArea.IncludedRows : area.IncludedRows, area.Name);
		}

		// Token: 0x060067B6 RID: 26550 RVA: 0x0015243D File Offset: 0x0015063D
		public static SpreadsheetArea RemoveEmptyRows(SpreadsheetArea area)
		{
			return Semantics.RemoveEmptySlices(area, Axis.Horizontal);
		}

		// Token: 0x060067B7 RID: 26551 RVA: 0x00152446 File Offset: 0x00150646
		public static SpreadsheetArea RemoveEmptyColumns(SpreadsheetArea area)
		{
			return Semantics.RemoveEmptySlices(area, Axis.Vertical);
		}

		// Token: 0x060067B8 RID: 26552 RVA: 0x0015244F File Offset: 0x0015064F
		public static SpreadsheetArea MTrimTopSingleCellRows(SpreadsheetArea area)
		{
			return Semantics.TrimEdgeSingleCellSlices(area, Direction.Up, true);
		}

		// Token: 0x060067B9 RID: 26553 RVA: 0x00152459 File Offset: 0x00150659
		public static SpreadsheetArea MTrimBottomSingleCellRows(SpreadsheetArea area)
		{
			return Semantics.TrimEdgeSingleCellSlices(area, Direction.Down, true);
		}

		// Token: 0x060067BA RID: 26554 RVA: 0x00152463 File Offset: 0x00150663
		public static SpreadsheetArea MTrimLeftSingleCellColumns(SpreadsheetArea area)
		{
			return Semantics.TrimEdgeSingleCellSlices(area, Direction.Left, true);
		}

		// Token: 0x060067BB RID: 26555 RVA: 0x0015246D File Offset: 0x0015066D
		public static SpreadsheetArea MTrimRightSingleCellColumns(SpreadsheetArea area)
		{
			return Semantics.TrimEdgeSingleCellSlices(area, Direction.Right, true);
		}

		// Token: 0x060067BC RID: 26556 RVA: 0x00152477 File Offset: 0x00150677
		public static SpreadsheetArea TrimTopDoubleCellRows(SpreadsheetArea area)
		{
			return Semantics.TrimEdgeFewCellSlices(area, Direction.Up, 2, true);
		}

		// Token: 0x060067BD RID: 26557 RVA: 0x00152482 File Offset: 0x00150682
		public static SpreadsheetArea TrimBottomDoubleCellRows(SpreadsheetArea area)
		{
			return Semantics.TrimEdgeFewCellSlices(area, Direction.Down, 2, true);
		}

		// Token: 0x060067BE RID: 26558 RVA: 0x00152490 File Offset: 0x00150690
		public static SpreadsheetArea KthAndNextMSection(SpreadsheetArea[] sections, int k)
		{
			if (k < 0 || k + 1 >= sections.Length)
			{
				return null;
			}
			SpreadsheetArea spreadsheetArea = sections[k];
			SpreadsheetArea spreadsheetArea2 = sections[k + 1];
			return new SpreadsheetArea(spreadsheetArea.Spreadsheet, spreadsheetArea.Span.Join(spreadsheetArea2.Span), new Ranges<TableUnit>(spreadsheetArea.IncludedColumnsEnumerable.Union(spreadsheetArea2.IncludedColumnsEnumerable), false), new Ranges<TableUnit>(spreadsheetArea.IncludedRowsEnumerable.Union(spreadsheetArea2.IncludedRowsEnumerable), false), null);
		}

		// Token: 0x060067BF RID: 26559 RVA: 0x00152504 File Offset: 0x00150704
		public static SpreadsheetArea TopLeftCell(SpreadsheetArea area)
		{
			Vector<TableUnit> vector = area.Span.Corner(Ordinal.TopLeft);
			return area.TrimToSpan(new Bounds<TableUnit>(vector, vector));
		}

		// Token: 0x060067C0 RID: 26560 RVA: 0x00152530 File Offset: 0x00150730
		public static SpreadsheetArea TopSameFontCells(SpreadsheetArea area)
		{
			Semantics.<>c__DisplayClass48_0 CS$<>8__locals1 = new Semantics.<>c__DisplayClass48_0();
			CS$<>8__locals1.area = area;
			Semantics.<>c__DisplayClass48_0 CS$<>8__locals2 = CS$<>8__locals1;
			ISpreadsheetCell spreadsheetCell = CS$<>8__locals1.area.EnumerateCells(Axis.Horizontal, true, true, true, Derivative.Increasing, null).FirstOrDefault<ISpreadsheetCell>();
			CS$<>8__locals2.firstStyle = ((spreadsheetCell != null) ? spreadsheetCell.StyleInfo : null);
			if (CS$<>8__locals1.firstStyle == null)
			{
				return null;
			}
			return (from span in Bounds<TableUnit>.MaybeJoin(CS$<>8__locals1.area.EnumerateRows(true, true, true, Derivative.Increasing).TakeWhile(delegate(Record<int, IEnumerable<ISpreadsheetCell>> row)
				{
					IEnumerable<ISpreadsheetCell> item = row.Item2;
					Func<ISpreadsheetCell, bool> func;
					if ((func = CS$<>8__locals1.<>9__3) == null)
					{
						func = (CS$<>8__locals1.<>9__3 = (ISpreadsheetCell cell) => FontInfoEqualityComparer.Instance.Equals(CS$<>8__locals1.firstStyle, cell.StyleInfo));
					}
					return item.All(func);
				}).SelectMany((Record<int, IEnumerable<ISpreadsheetCell>> row) => row.Item2.Select((ISpreadsheetCell cell) => cell.Span)))
				select CS$<>8__locals1.area.TrimToSpan(span)).OrElseDefault<SpreadsheetArea>();
		}

		// Token: 0x060067C1 RID: 26561 RVA: 0x001525E4 File Offset: 0x001507E4
		public static SpreadsheetArea BottomLeftSameFontCells(SpreadsheetArea area)
		{
			Semantics.<>c__DisplayClass49_0 CS$<>8__locals1 = new Semantics.<>c__DisplayClass49_0();
			CS$<>8__locals1.area = area;
			CS$<>8__locals1.area = Semantics.LeftmostColumn(CS$<>8__locals1.area);
			if (CS$<>8__locals1.area == null)
			{
				return null;
			}
			Semantics.<>c__DisplayClass49_0 CS$<>8__locals2 = CS$<>8__locals1;
			ISpreadsheetCell spreadsheetCell = CS$<>8__locals1.area.EnumerateCells(Axis.Horizontal, true, true, true, Derivative.Increasing, null).LastOrDefault<ISpreadsheetCell>();
			CS$<>8__locals2.bottomStyle = ((spreadsheetCell != null) ? spreadsheetCell.StyleInfo : null);
			if (CS$<>8__locals1.bottomStyle == null)
			{
				return null;
			}
			return (from span in Bounds<TableUnit>.MaybeJoin(CS$<>8__locals1.area.EnumerateRows(true, true, true, Derivative.Decreasing).TakeWhile(delegate(Record<int, IEnumerable<ISpreadsheetCell>> row)
				{
					IEnumerable<ISpreadsheetCell> item = row.Item2;
					Func<ISpreadsheetCell, bool> func;
					if ((func = CS$<>8__locals1.<>9__3) == null)
					{
						func = (CS$<>8__locals1.<>9__3 = (ISpreadsheetCell cell) => FontInfoEqualityComparer.Instance.Equals(CS$<>8__locals1.bottomStyle, cell.StyleInfo));
					}
					return item.All(func);
				}).SelectMany((Record<int, IEnumerable<ISpreadsheetCell>> row) => row.Item2.Select((ISpreadsheetCell cell) => cell.Span)))
				select CS$<>8__locals1.area.TrimToSpan(span)).OrElseDefault<SpreadsheetArea>();
		}

		// Token: 0x060067C2 RID: 26562 RVA: 0x001526B8 File Offset: 0x001508B8
		private static SpreadsheetArea LeftmostOrOneFurther(SpreadsheetArea area, bool pastEdge)
		{
			ISpreadsheetCell spreadsheetCell = area.EnumerateCells(Axis.Vertical, true, true, true, Derivative.Increasing, null).FirstOrDefault<ISpreadsheetCell>();
			int? num = ((spreadsheetCell != null) ? new int?(spreadsheetCell.Span.Left) : null);
			if (num == null)
			{
				return null;
			}
			if (pastEdge)
			{
				int? num2 = num;
				int num3 = 0;
				if (!((num2.GetValueOrDefault() > num3) & (num2 != null)))
				{
					return null;
				}
				num--;
			}
			SpreadsheetArea spreadsheetArea = new SpreadsheetArea(area.Spreadsheet, area.Span.With(Axis.Horizontal, Range<TableUnit>.CreateAround(num.Value, 0)), null, null, null);
			if (!pastEdge)
			{
				return Semantics.Trim(spreadsheetArea);
			}
			return spreadsheetArea;
		}

		// Token: 0x060067C3 RID: 26563 RVA: 0x00152788 File Offset: 0x00150988
		public static SpreadsheetArea LeftOf(SpreadsheetArea area)
		{
			return Semantics.LeftmostOrOneFurther(area, true);
		}

		// Token: 0x060067C4 RID: 26564 RVA: 0x00152791 File Offset: 0x00150991
		public static SpreadsheetArea LeftmostColumn(SpreadsheetArea area)
		{
			return Semantics.LeftmostOrOneFurther(area, false);
		}

		// Token: 0x060067C5 RID: 26565 RVA: 0x0015279A File Offset: 0x0015099A
		public static SpreadsheetArea FirstSplit(SpreadsheetArea[] splits)
		{
			return splits.FirstOrDefault<SpreadsheetArea>();
		}

		// Token: 0x060067C6 RID: 26566 RVA: 0x001527A4 File Offset: 0x001509A4
		public static SpreadsheetArea[] TitleSplitOnMatchingRows(SpreadsheetArea area, StyleFilter styleFilter, SplitMode splitMode)
		{
			return Semantics.SplitOnMatchingRows((area.Span.Left == 0) ? area : SpreadsheetArea.MaybeCreateSpreadsheetArea(area.Spreadsheet, area.Span.Extend(Direction.Left, 1), null, null).Value, styleFilter, splitMode, true);
		}

		// Token: 0x060067C7 RID: 26567 RVA: 0x001527F1 File Offset: 0x001509F1
		public static SpreadsheetArea TitleCellsAbove(SpreadsheetArea area, TitleAboveMode mode)
		{
			return Semantics.CellsAbove(area, mode, null);
		}

		// Token: 0x060067C8 RID: 26568 RVA: 0x001527FB File Offset: 0x001509FB
		public static SpreadsheetArea TitleCellsAboveMatching(SpreadsheetArea area, TitleAboveMode mode, StyleFilter styleFilter)
		{
			return Semantics.CellsAbove(area, mode, styleFilter);
		}

		// Token: 0x060067C9 RID: 26569 RVA: 0x00152808 File Offset: 0x00150A08
		private static SpreadsheetArea CellsAbove(SpreadsheetArea area, TitleAboveMode mode, StyleFilter styleFilter = null)
		{
			bool flag = (mode & TitleAboveMode.AllowGaps) > TitleAboveMode.Invalid;
			bool flag2 = (mode & TitleAboveMode.MustBeMerged) > TitleAboveMode.Invalid;
			bool flag3 = (mode & TitleAboveMode.IncludeTopTableRow) > TitleAboveMode.Invalid;
			bool flag4 = (mode & TitleAboveMode.RequireAligned) > TitleAboveMode.Invalid;
			int num;
			if ((mode & TitleAboveMode.AtMostTwoCells) == TitleAboveMode.Invalid)
			{
				if ((mode & TitleAboveMode.AtMostOneCell) == TitleAboveMode.Invalid)
				{
					throw new ArgumentException("Invalid TitleAboveMode: " + mode.ToString(), "mode");
				}
				num = 1;
			}
			else
			{
				num = 2;
			}
			int num2 = num;
			bool areaHasHiddenCells = area.EnumerateCells(Axis.Horizontal, true, false, true, Derivative.Increasing, null).Any((ISpreadsheetCell cell) => cell.IsHidden());
			RectangularArray<ISpreadsheetCell> rectangularArray = area.Spreadsheet.PrunedCells(true, !areaHasHiddenCells, true);
			Bounds<TableUnit> bounds = area.Span.With(Direction.Up, 0).With(Direction.Left, Math.Max(0, area.Span.Left - 1)).With(Direction.Right, Math.Min(area.Spreadsheet.Size.X - 1, area.Span.Right + 1));
			int? num3 = null;
			IReadOnlyList<Range<TableUnit>> readOnlyList = null;
			List<ISpreadsheetCell> list = new List<ISpreadsheetCell>();
			Func<ISpreadsheetCell, bool> <>9__3;
			Func<ISpreadsheetCell, bool> <>9__6;
			for (int i = area.Span.Top - ((!flag3) ? 1 : 0); i >= 0; i--)
			{
				IEnumerable<ISpreadsheetCell> enumerable = rectangularArray.Row(i, bounds).Collect<ISpreadsheetCell>();
				Func<ISpreadsheetCell, bool> func;
				if ((func = <>9__3) == null)
				{
					func = (<>9__3 = delegate(ISpreadsheetCell cell)
					{
						StyleFilter styleFilter2 = styleFilter;
						return styleFilter2 == null || styleFilter2.Matches(cell);
					});
				}
				IReadOnlyList<ISpreadsheetCell> readOnlyList2 = enumerable.Where(func).DistinctOn((ISpreadsheetCell cell) => cell.AsString.Trim(), null).Take(num2 + 1)
					.ToList<ISpreadsheetCell>();
				if (readOnlyList2.Count == 0)
				{
					if (!flag && num3 != null)
					{
						break;
					}
				}
				else
				{
					if (readOnlyList2.Count <= num2)
					{
						if (!readOnlyList2.Any((ISpreadsheetCell cell) => cell.Span.Width() == 1) || !flag2)
						{
							IEnumerable<ISpreadsheetCell> enumerable2 = readOnlyList2;
							Func<ISpreadsheetCell, bool> func2;
							if ((func2 = <>9__6) == null)
							{
								func2 = (<>9__6 = (ISpreadsheetCell cell) => cell.Span.Left > area.Span.Right + 1);
							}
							if (!enumerable2.Any(func2) || flag2)
							{
								IReadOnlyList<Range<TableUnit>> readOnlyList3 = readOnlyList2.Select((ISpreadsheetCell cell) => cell.Span.Horizontal).ToList<Range<TableUnit>>();
								if (num3 == null)
								{
									num3 = new int?(i);
									if (flag4)
									{
										readOnlyList = readOnlyList3;
									}
									list.AddRange(readOnlyList2);
									goto IL_02F9;
								}
								if (!flag4 || readOnlyList3.IsSubsequenceOf(readOnlyList))
								{
									list.AddRange(readOnlyList2);
									goto IL_02F9;
								}
								if (num2 > 1 && readOnlyList.IsSubsequenceOf(readOnlyList3))
								{
									readOnlyList = readOnlyList3;
									list.AddRange(readOnlyList2);
									goto IL_02F9;
								}
								break;
							}
						}
					}
					if (num3 != null)
					{
						break;
					}
				}
				IL_02F9:;
			}
			if (num3 == null)
			{
				return null;
			}
			return SpreadsheetArea.MaybeCreateSpreadsheetArea(area.Spreadsheet, Bounds<TableUnit>.Join(list.Select((ISpreadsheetCell cell) => cell.Span)), null, null).Select(delegate(SpreadsheetArea above)
			{
				if (!areaHasHiddenCells)
				{
					return Semantics.TrimHidden(above);
				}
				return Semantics.Trim(above);
			}).OrElseDefault<SpreadsheetArea>();
		}

		// Token: 0x060067CA RID: 26570 RVA: 0x00152B7C File Offset: 0x00150D7C
		public static SpreadsheetArea IncludeEmptyToLeft(SpreadsheetArea area)
		{
			if (area.Span.Left == 0)
			{
				return null;
			}
			bool flag = area.EnumerateCells(Axis.Horizontal, true, false, true, Derivative.Increasing, null).Any((ISpreadsheetCell cell) => cell.IsHidden());
			RectangularArray<ISpreadsheetCell> cells = area.Spreadsheet.PrunedCells(true, !flag, false);
			Bounds<TableUnit> intersection = area.Span.With(Axis.Horizontal, new Range<TableUnit>(0, area.Span.Left - 1));
			Optional<int> optional = intersection.Horizontal.AsReverseEnumerable.MaybeLast((int col) => !cells.Column(col, intersection).Collect<ISpreadsheetCell>().Any<ISpreadsheetCell>());
			if (!optional.HasValue)
			{
				return null;
			}
			ISpreadsheet spreadsheet = area.Spreadsheet;
			Bounds<TableUnit> bounds = area.Span.With(Direction.Left, optional.Value);
			Ranges<TableUnit> includedColumns = area.IncludedColumns;
			return new SpreadsheetArea(spreadsheet, bounds, (includedColumns != null) ? includedColumns.Join(new Range<TableUnit>(optional.Value, area.Span.Left - 1)) : null, area.IncludedRows, null);
		}
	}
}
