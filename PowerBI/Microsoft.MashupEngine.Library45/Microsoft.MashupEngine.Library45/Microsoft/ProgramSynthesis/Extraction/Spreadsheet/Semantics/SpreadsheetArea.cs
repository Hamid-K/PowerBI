using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000ED0 RID: 3792
	public class SpreadsheetArea : IEquatable<SpreadsheetArea>
	{
		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x0600672B RID: 26411 RVA: 0x001503FD File Offset: 0x0014E5FD
		public ISpreadsheet Spreadsheet { get; }

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x0600672C RID: 26412 RVA: 0x00150405 File Offset: 0x0014E605
		public Bounds<TableUnit> Span { get; }

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x0600672D RID: 26413 RVA: 0x0015040D File Offset: 0x0014E60D
		public AxisAligned<Ranges<TableUnit>> IncludedSlices { get; }

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x0600672E RID: 26414 RVA: 0x00150415 File Offset: 0x0014E615
		public Ranges<TableUnit> IncludedColumns
		{
			get
			{
				AxisAligned<Ranges<TableUnit>> includedSlices = this.IncludedSlices;
				if (includedSlices == null)
				{
					return null;
				}
				return includedSlices.Horizontal;
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x0600672F RID: 26415 RVA: 0x00150428 File Offset: 0x0014E628
		public Ranges<TableUnit> IncludedRows
		{
			get
			{
				AxisAligned<Ranges<TableUnit>> includedSlices = this.IncludedSlices;
				if (includedSlices == null)
				{
					return null;
				}
				return includedSlices.Vertical;
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06006730 RID: 26416 RVA: 0x0015043B File Offset: 0x0014E63B
		public string Name { get; }

		// Token: 0x06006731 RID: 26417 RVA: 0x00150444 File Offset: 0x0014E644
		public IEnumerable<int> IncludedSlicesEnumerable(Axis axis)
		{
			AxisAligned<Ranges<TableUnit>> includedSlices = this.IncludedSlices;
			IEnumerable<int> enumerable;
			if (includedSlices == null)
			{
				enumerable = null;
			}
			else
			{
				Ranges<TableUnit> ranges = includedSlices[axis];
				if (ranges == null)
				{
					enumerable = null;
				}
				else
				{
					enumerable = ranges.SelectMany((Range<TableUnit> r) => r.AsEnumerable);
				}
			}
			return enumerable ?? this.Span[axis].AsEnumerable;
		}

		// Token: 0x06006732 RID: 26418 RVA: 0x001504AC File Offset: 0x0014E6AC
		public IEnumerable<int> IncludedSlicesReverseEnumerable(Axis axis)
		{
			AxisAligned<Ranges<TableUnit>> includedSlices = this.IncludedSlices;
			IEnumerable<int> enumerable;
			if (includedSlices == null)
			{
				enumerable = null;
			}
			else
			{
				Ranges<TableUnit> ranges = includedSlices[axis];
				if (ranges == null)
				{
					enumerable = null;
				}
				else
				{
					enumerable = ranges.Reverse<Range<TableUnit>>().SelectMany((Range<TableUnit> r) => r.AsReverseEnumerable);
				}
			}
			return enumerable ?? this.Span[axis].AsReverseEnumerable;
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06006733 RID: 26419 RVA: 0x00150517 File Offset: 0x0014E717
		public IEnumerable<int> IncludedColumnsEnumerable
		{
			get
			{
				return this.IncludedSlicesEnumerable(Axis.Horizontal);
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06006734 RID: 26420 RVA: 0x00150520 File Offset: 0x0014E720
		public IEnumerable<int> IncludedRowsEnumerable
		{
			get
			{
				return this.IncludedSlicesEnumerable(Axis.Vertical);
			}
		}

		// Token: 0x06006735 RID: 26421 RVA: 0x0015052C File Offset: 0x0014E72C
		internal SpreadsheetArea(ISpreadsheet spreadsheet, Bounds<TableUnit> span, Ranges<TableUnit> includedColumns = null, Ranges<TableUnit> includedRows = null, string name = null)
		{
			this.Spreadsheet = spreadsheet;
			this.Span = span;
			this.Name = name;
			if (includedColumns != null)
			{
				Range<TableUnit>? range = includedColumns.JoinOfRanges;
				if (range != null && !range.GetValueOrDefault().Equals(span.Horizontal))
				{
					throw new ArgumentException("Included columns must be within area's span.", "includedColumns");
				}
				includedColumns = (includedColumns.Contains(span.Horizontal) ? null : includedColumns);
			}
			if (includedRows != null)
			{
				Range<TableUnit>? range = includedRows.JoinOfRanges;
				if (range != null && !range.GetValueOrDefault().Equals(span.Vertical))
				{
					throw new ArgumentException("Included rows must be within area's span.", "includedRows");
				}
				includedRows = (includedRows.Contains(span.Vertical) ? null : includedRows);
			}
			this.IncludedSlices = ((includedColumns == null && includedRows == null) ? null : new AxisAligned<Ranges<TableUnit>>(includedColumns, includedRows));
		}

		// Token: 0x06006736 RID: 26422 RVA: 0x0015061D File Offset: 0x0014E81D
		internal SpreadsheetArea WithName(string name)
		{
			return new SpreadsheetArea(this.Spreadsheet, this.Span, this.IncludedColumns, this.IncludedRows, name);
		}

		// Token: 0x06006737 RID: 26423 RVA: 0x0015063D File Offset: 0x0014E83D
		internal SpreadsheetArea WithSpreadsheet(ISpreadsheet spreadsheet)
		{
			return new SpreadsheetArea(spreadsheet, this.Span, this.IncludedColumns, this.IncludedRows, this.Name);
		}

		// Token: 0x06006738 RID: 26424 RVA: 0x00150660 File Offset: 0x0014E860
		internal SpreadsheetArea TrimToSpan(Bounds<TableUnit> trimmedSpan)
		{
			foreach (Axis axis in AxisUtilities.Axes)
			{
				AxisAligned<Ranges<TableUnit>> includedSlices = this.IncludedSlices;
				Ranges<TableUnit> ranges = ((includedSlices != null) ? includedSlices[axis] : null);
				if (ranges != null)
				{
					Range<TableUnit>? joinOfRanges = ranges.Intersect(trimmedSpan[axis]).JoinOfRanges;
					if (joinOfRanges == null)
					{
						return null;
					}
					if (joinOfRanges.Value != trimmedSpan[axis])
					{
						trimmedSpan = trimmedSpan.With(axis, joinOfRanges.Value);
					}
				}
			}
			ISpreadsheet spreadsheet = this.Spreadsheet;
			Bounds<TableUnit> bounds = trimmedSpan;
			Ranges<TableUnit> includedColumns = this.IncludedColumns;
			Ranges<TableUnit> ranges2 = ((includedColumns != null) ? includedColumns.Intersect(trimmedSpan.Horizontal) : null);
			Ranges<TableUnit> includedRows = this.IncludedRows;
			return new SpreadsheetArea(spreadsheet, bounds, ranges2, (includedRows != null) ? includedRows.Intersect(trimmedSpan.Vertical) : null, null);
		}

		// Token: 0x06006739 RID: 26425 RVA: 0x00150748 File Offset: 0x0014E948
		public static Optional<SpreadsheetArea> MaybeCreateSpreadsheetArea(ISpreadsheet spreadsheet, Bounds<TableUnit> span, Ranges<TableUnit> includedColumns = null, Ranges<TableUnit> includedRows = null)
		{
			return (span.Left >= 0 && span.Top >= 0 && span.Right < spreadsheet.Size.X && span.Bottom < spreadsheet.Size.Y).Then(() => new SpreadsheetArea(spreadsheet, span, includedColumns, includedRows, null));
		}

		// Token: 0x0600673A RID: 26426 RVA: 0x001507E4 File Offset: 0x0014E9E4
		public IEnumerable<ISpreadsheetCell> EnumerateCells(Axis majorAxis = Axis.Horizontal, bool pruneNullStringCells = true, bool pruneHiddenCells = true, bool pruneMergedCellProxies = true, Derivative derivative = Derivative.Increasing, int? slice = null)
		{
			SpreadsheetArea.<>c__DisplayClass27_0 CS$<>8__locals1 = new SpreadsheetArea.<>c__DisplayClass27_0();
			CS$<>8__locals1.slice = slice;
			CS$<>8__locals1.majorAxis = majorAxis;
			CS$<>8__locals1.derivative = derivative;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.prunedCells = ((AbstractSpreadsheet)this.Spreadsheet).PrunedCells(pruneNullStringCells, pruneHiddenCells, pruneMergedCellProxies);
			AxisAligned<IEnumerable<int>> axisAligned = new AxisAligned<IEnumerable<int>>(delegate(Axis axis)
			{
				if (CS$<>8__locals1.slice != null || CS$<>8__locals1.majorAxis != axis.Perpendicular() || CS$<>8__locals1.derivative != Derivative.Decreasing)
				{
					return CS$<>8__locals1.<>4__this.IncludedSlicesEnumerable(axis);
				}
				return CS$<>8__locals1.<>4__this.IncludedSlicesReverseEnumerable(axis);
			});
			CS$<>8__locals1.columns = axisAligned.Horizontal;
			CS$<>8__locals1.rows = axisAligned.Vertical;
			return CS$<>8__locals1.<EnumerateCells>g__Generator|1().Collect<ISpreadsheetCell>();
		}

		// Token: 0x0600673B RID: 26427 RVA: 0x00150864 File Offset: 0x0014EA64
		public IEnumerable<ISpreadsheetCell> EnumerateCellsRowMajor(bool pruneNullStringCells = true, bool pruneHiddenCells = true, bool pruneMergedCellProxies = true, Derivative derivative = Derivative.Increasing)
		{
			return this.EnumerateCells(Axis.Horizontal, pruneNullStringCells, pruneHiddenCells, pruneMergedCellProxies, derivative, null);
		}

		// Token: 0x0600673C RID: 26428 RVA: 0x00150888 File Offset: 0x0014EA88
		public IEnumerable<ISpreadsheetCell> EnumerateCellsColumnMajor(bool pruneNullStringCells = true, bool pruneHiddenCells = true, bool pruneMergedCellProxies = true, Derivative derivative = Derivative.Increasing)
		{
			return this.EnumerateCells(Axis.Vertical, pruneNullStringCells, pruneHiddenCells, pruneMergedCellProxies, derivative, null);
		}

		// Token: 0x0600673D RID: 26429 RVA: 0x001508AC File Offset: 0x0014EAAC
		public IEnumerable<ISpreadsheetCell> EnumerateRow(int rowIdx, bool pruneNullStringCells = true, bool pruneHiddenCells = true, bool pruneMergedCellProxies = true)
		{
			Axis axis = Axis.Horizontal;
			int? num = new int?(rowIdx);
			return this.EnumerateCells(axis, pruneNullStringCells, pruneHiddenCells, pruneMergedCellProxies, Derivative.Increasing, num);
		}

		// Token: 0x0600673E RID: 26430 RVA: 0x001508D0 File Offset: 0x0014EAD0
		public IEnumerable<ISpreadsheetCell> EnumerateColumn(int colIdx, bool pruneNullStringCells = true, bool pruneHiddenCells = true, bool pruneMergedCellProxies = true)
		{
			Axis axis = Axis.Vertical;
			int? num = new int?(colIdx);
			return this.EnumerateCells(axis, pruneNullStringCells, pruneHiddenCells, pruneMergedCellProxies, Derivative.Increasing, num);
		}

		// Token: 0x0600673F RID: 26431 RVA: 0x001508F4 File Offset: 0x0014EAF4
		public IEnumerable<Record<int, IEnumerable<ISpreadsheetCell>>> EnumerateSlices(Axis majorAxis = Axis.Vertical, bool pruneNullStringCells = true, bool pruneHiddenCells = true, bool pruneMergedCellProxies = true, Derivative derivative = Derivative.Increasing, bool coverExcludedSlices = false)
		{
			bool flag = derivative == Derivative.Decreasing;
			Axis axis = majorAxis.Perpendicular();
			Func<int, bool> excludedSliceCheck;
			IEnumerable<int> enumerable;
			if (coverExcludedSlices)
			{
				SpreadsheetArea.<>c__DisplayClass32_1 CS$<>8__locals2 = new SpreadsheetArea.<>c__DisplayClass32_1();
				enumerable = (flag ? this.Span[axis].AsReverseEnumerable : this.Span[axis].AsEnumerable);
				SpreadsheetArea.<>c__DisplayClass32_1 CS$<>8__locals3 = CS$<>8__locals2;
				AxisAligned<Ranges<TableUnit>> includedSlices = this.IncludedSlices;
				CS$<>8__locals3.includedSlices = ((includedSlices != null) ? includedSlices[axis] : null);
				if (CS$<>8__locals2.includedSlices == null)
				{
					excludedSliceCheck = (int _) => false;
				}
				else
				{
					excludedSliceCheck = (int idx) => !CS$<>8__locals2.includedSlices.Contains(idx);
				}
			}
			else
			{
				enumerable = this.IncludedSlicesEnumerable(axis);
				if (flag)
				{
					enumerable = enumerable.Reverse<int>();
				}
				excludedSliceCheck = (int _) => false;
			}
			return enumerable.Select((int slice) => Record.Create<int, IEnumerable<ISpreadsheetCell>>(slice, excludedSliceCheck(slice) ? null : this.EnumerateCells(majorAxis, pruneNullStringCells, pruneHiddenCells, pruneMergedCellProxies, Derivative.Increasing, new int?(slice))));
		}

		// Token: 0x06006740 RID: 26432 RVA: 0x00150A2B File Offset: 0x0014EC2B
		public IEnumerable<Record<int, IEnumerable<ISpreadsheetCell>>> EnumerateRows(bool pruneNullStringCells = true, bool pruneHiddenCells = true, bool pruneMergedCellProxies = true, Derivative derivative = Derivative.Increasing)
		{
			return this.EnumerateSlices(Axis.Horizontal, pruneNullStringCells, pruneHiddenCells, pruneMergedCellProxies, derivative, false);
		}

		// Token: 0x06006741 RID: 26433 RVA: 0x00150A3A File Offset: 0x0014EC3A
		public IEnumerable<Record<int, IEnumerable<ISpreadsheetCell>>> EnumerateColumns(bool pruneNullStringCells = true, bool pruneHiddenCells = true, bool pruneMergedCellProxies = true, Derivative derivative = Derivative.Increasing)
		{
			return this.EnumerateSlices(Axis.Vertical, pruneNullStringCells, pruneHiddenCells, pruneMergedCellProxies, derivative, false);
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x00150A4C File Offset: 0x0014EC4C
		public SpreadsheetArea Trim(bool pruneHidden, bool pruneMergedCellProxies, bool trimInteriorSlices)
		{
			SpreadsheetArea trimmedArea = this.Spreadsheet.Trim(this.Span, pruneHidden, pruneMergedCellProxies, trimInteriorSlices);
			if (trimmedArea == null || this.IncludedSlices == null)
			{
				return trimmedArea;
			}
			AxisAligned<Ranges<TableUnit>> trimmedIncludedSlices = this.IncludedSlices.Select(delegate(Axis axis, Ranges<TableUnit> included)
			{
				AxisAligned<Ranges<TableUnit>> includedSlices = trimmedArea.IncludedSlices;
				Ranges<TableUnit> ranges = ((includedSlices != null) ? includedSlices[axis] : null);
				if (included == null)
				{
					return ranges;
				}
				if (ranges == null)
				{
					return included.Intersect(trimmedArea.Span[axis]);
				}
				return included.Intersect(ranges);
			});
			if (trimmedIncludedSlices.Any((Ranges<TableUnit> included) => included != null && included.Count == 0))
			{
				return null;
			}
			Optional<Bounds<TableUnit>> optional = trimmedArea.Span.Intersect(new Bounds<TableUnit>(delegate(Axis axis)
			{
				Ranges<TableUnit> ranges2 = trimmedIncludedSlices[axis];
				Range<TableUnit>? range = ((ranges2 != null) ? ranges2.JoinOfRanges : null);
				if (range == null)
				{
					return trimmedArea.Span[axis];
				}
				return range.GetValueOrDefault();
			}));
			if (!optional.HasValue)
			{
				return null;
			}
			return new SpreadsheetArea(this.Spreadsheet, optional.Value, trimmedIncludedSlices.Horizontal, trimmedIncludedSlices.Vertical, this.Name);
		}

		// Token: 0x06006743 RID: 26435 RVA: 0x00150B40 File Offset: 0x0014ED40
		public IEnumerable<Border> BordersOrderedByDirection(Direction dir, bool excludeFarEdge = false)
		{
			Axis axis = dir.AlignedAxis().Perpendicular();
			Range<TableUnit> borderPosSpan = this.Span[axis.Perpendicular()].Expand(1, Derivative.Increasing);
			if (excludeFarEdge)
			{
				borderPosSpan = borderPosSpan.Expand(-1, dir.Derivative());
			}
			IEnumerable<KeyValuePair<int, IReadOnlyList<Border>>> enumerable = this.Spreadsheet.Borders.Borders[axis];
			if (dir.Derivative() == Derivative.Decreasing)
			{
				enumerable = enumerable.Reverse<KeyValuePair<int, IReadOnlyList<Border>>>();
			}
			Range<TableUnit> borderSpan = this.Span[axis].Expand(1, Derivative.Increasing);
			Func<Border, bool> <>9__2;
			return enumerable.Where2((int pos, IReadOnlyList<Border> borders) => borderPosSpan.Contains(pos)).SelectMany2(delegate(int pos, IReadOnlyList<Border> borders)
			{
				Func<Border, bool> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (Border border) => (from overlap in borderSpan.Intersect(border.Line.Range)
						select overlap.Size() > 1).OrElse(false));
				}
				return borders.Where(func);
			});
		}

		// Token: 0x06006744 RID: 26436 RVA: 0x00150C09 File Offset: 0x0014EE09
		public IEnumerable<Border> BordersOfAxis(Axis axis)
		{
			return this.BordersOrderedByDirection(axis.Perpendicular().IncreasingDirection(), false);
		}

		// Token: 0x06006745 RID: 26437 RVA: 0x00150C1D File Offset: 0x0014EE1D
		public bool ContainsEmptySlice(Axis axis)
		{
			return this.EnumerateSlices(axis, true, false, true, Derivative.Increasing, true).Any2(delegate(int idx, IEnumerable<ISpreadsheetCell> slice)
			{
				if (slice != null)
				{
					return slice.AllOrElseCompute((ISpreadsheetCell cell) => string.IsNullOrWhiteSpace(cell.AsString), () => true);
				}
				return true;
			});
		}

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06006746 RID: 26438 RVA: 0x00150C4F File Offset: 0x0014EE4F
		public bool ContainsEmptyColumn
		{
			get
			{
				return this.ContainsEmptySlice(Axis.Vertical);
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06006747 RID: 26439 RVA: 0x00150C58 File Offset: 0x0014EE58
		public bool ContainsEmptyRow
		{
			get
			{
				return this.ContainsEmptySlice(Axis.Horizontal);
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06006748 RID: 26440 RVA: 0x00150C64 File Offset: 0x0014EE64
		public RectangularArray<ISpreadsheetCell> Section
		{
			get
			{
				List<int> list = this.IncludedColumnsEnumerable.ToList<int>();
				List<int> list2 = this.IncludedRowsEnumerable.ToList<int>();
				RectangularArray<ISpreadsheetCell> rectangularArray = new RectangularArray<ISpreadsheetCell>(list.Count, list2.Count);
				foreach (Record<int, int> record in list.Enumerate<int>())
				{
					int num;
					int num2;
					record.Deconstruct(out num, out num2);
					int num3 = num;
					int num4 = num2;
					foreach (Record<int, int> record2 in list2.Enumerate<int>())
					{
						record2.Deconstruct(out num2, out num);
						int num5 = num2;
						int num6 = num;
						rectangularArray[num3, num5] = this.Spreadsheet[num4, num6];
					}
				}
				return rectangularArray;
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06006749 RID: 26441 RVA: 0x00150D4C File Offset: 0x0014EF4C
		public RectangularArray<string> SectionAsStrings
		{
			get
			{
				return this.Section.Select<string>(delegate(ISpreadsheetCell cell)
				{
					if (cell is IMergedSpreadsheetCellProxy)
					{
						return null;
					}
					if (cell == null)
					{
						return null;
					}
					return cell.AsString;
				});
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x0600674A RID: 26442 RVA: 0x00150D88 File Offset: 0x0014EF88
		public RectangularArray<string> SectionAsStringsWithShadowed
		{
			get
			{
				return this.Section.Select<string>(delegate(ISpreadsheetCell cell)
				{
					IMergedSpreadsheetCellProxy mergedSpreadsheetCellProxy = cell as IMergedSpreadsheetCellProxy;
					if (mergedSpreadsheetCellProxy == null)
					{
						if (cell == null)
						{
							return null;
						}
						return cell.AsString;
					}
					else
					{
						ISpreadsheetCell shadowedCell = mergedSpreadsheetCellProxy.ShadowedCell;
						if (shadowedCell == null)
						{
							return null;
						}
						return shadowedCell.AsString;
					}
				});
			}
		}

		// Token: 0x0600674B RID: 26443 RVA: 0x00150DC4 File Offset: 0x0014EFC4
		public override string ToString()
		{
			return this.SectionAsStrings.ToString();
		}

		// Token: 0x0600674C RID: 26444 RVA: 0x00150DE8 File Offset: 0x0014EFE8
		public bool Equals(SpreadsheetArea other)
		{
			return other != null && (this == other || (this.Spreadsheet == other.Spreadsheet && this.Span.Equals(other.Span) && object.Equals(this.IncludedColumns, other.IncludedColumns) && object.Equals(this.IncludedRows, other.IncludedRows)));
		}

		// Token: 0x0600674D RID: 26445 RVA: 0x00150E4A File Offset: 0x0014F04A
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((SpreadsheetArea)obj)));
		}

		// Token: 0x0600674E RID: 26446 RVA: 0x00150E78 File Offset: 0x0014F078
		public override int GetHashCode()
		{
			int num = (RuntimeHelpers.GetHashCode(this.Spreadsheet) * 397) ^ this.Span.GetHashCode();
			Ranges<TableUnit> includedColumns = this.IncludedColumns;
			int num2 = num ^ ((includedColumns != null) ? includedColumns.GetHashCode() : 0);
			Ranges<TableUnit> includedRows = this.IncludedRows;
			return num2 ^ ((includedRows != null) ? includedRows.GetHashCode() : 0);
		}

		// Token: 0x0600674F RID: 26447 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(SpreadsheetArea left, SpreadsheetArea right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06006750 RID: 26448 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(SpreadsheetArea left, SpreadsheetArea right)
		{
			return !object.Equals(left, right);
		}
	}
}
