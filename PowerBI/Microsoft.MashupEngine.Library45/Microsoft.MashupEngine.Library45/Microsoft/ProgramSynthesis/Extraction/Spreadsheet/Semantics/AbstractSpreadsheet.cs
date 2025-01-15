using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Learning;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EC1 RID: 3777
	public abstract class AbstractSpreadsheet : ISpreadsheet
	{
		// Token: 0x17001249 RID: 4681
		public abstract ISpreadsheetCell this[int x, int y] { get; }

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x060066DB RID: 26331
		public abstract Vector<TableUnit> Size { get; }

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x060066DC RID: 26332
		public abstract Vector<TableUnit> FreezePaneSize { get; }

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x060066DD RID: 26333
		public abstract IReadOnlyList<DefinedRange> DefinedRanges { get; }

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x060066DE RID: 26334 RVA: 0x00002188 File Offset: 0x00000388
		public virtual ISpreadsheet WithoutFormatting
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x060066DF RID: 26335 RVA: 0x0014F40E File Offset: 0x0014D60E
		public BorderCollection Borders
		{
			get
			{
				return this._bordersLazy.Value;
			}
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x060066E0 RID: 26336 RVA: 0x0014F41B File Offset: 0x0014D61B
		public IReadOnlyList<Bounds<TableUnit>> MergedCells
		{
			get
			{
				return this._mergedCellsLazy.Value;
			}
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x060066E1 RID: 26337 RVA: 0x0014F428 File Offset: 0x0014D628
		internal SpreadsheetPatterns Patterns
		{
			get
			{
				return this._patternsLazy.Value;
			}
		}

		// Token: 0x060066E2 RID: 26338 RVA: 0x0014F438 File Offset: 0x0014D638
		protected AbstractSpreadsheet()
		{
			this._bordersLazy = new Lazy<BorderCollection>(() => new BorderCollection(this));
			this._patternsLazy = new Lazy<SpreadsheetPatterns>(() => new SpreadsheetPatterns(this));
			this._mergedCellsLazy = new Lazy<IReadOnlyList<Bounds<TableUnit>>>(() => (from cell in this.EnumerateCells(Axis.Horizontal, false, false, true)
				select cell.Span into span
				where span.Area() > 1
				select span).ToList<Bounds<TableUnit>>());
		}

		// Token: 0x060066E3 RID: 26339 RVA: 0x0014F4BC File Offset: 0x0014D6BC
		public RectangularArray<ISpreadsheetCell> PrunedCells(bool pruneNullStringCells, bool pruneHiddenCells, bool pruneMergedCellProxies)
		{
			AbstractSpreadsheet.PruneFlags pruneFlags = (pruneNullStringCells ? AbstractSpreadsheet.PruneFlags.Null : ((AbstractSpreadsheet.PruneFlags)0)) | (pruneHiddenCells ? AbstractSpreadsheet.PruneFlags.Hidden : ((AbstractSpreadsheet.PruneFlags)0)) | (pruneMergedCellProxies ? AbstractSpreadsheet.PruneFlags.MergedProxies : ((AbstractSpreadsheet.PruneFlags)0));
			return this._prunedCellsCache.GetOrAdd(pruneFlags, delegate(AbstractSpreadsheet.PruneFlags _)
			{
				RectangularArray<ISpreadsheetCell> rectangularArray = new RectangularArray<ISpreadsheetCell>(this.Size);
				for (int i = 0; i < rectangularArray.Width; i++)
				{
					for (int j = 0; j < rectangularArray.Height; j++)
					{
						ISpreadsheetCell spreadsheetCell = this[i, j];
						if (spreadsheetCell != null && (!pruneNullStringCells || !string.IsNullOrWhiteSpace(spreadsheetCell.AsString)) && (!pruneMergedCellProxies || !(spreadsheetCell is IMergedSpreadsheetCellProxy)) && (!pruneHiddenCells || !spreadsheetCell.IsHidden()))
						{
							rectangularArray[i, j] = spreadsheetCell;
						}
					}
				}
				return rectangularArray;
			});
		}

		// Token: 0x060066E4 RID: 26340 RVA: 0x0014F52C File Offset: 0x0014D72C
		public SpreadsheetArea Trim(Bounds<TableUnit> span, bool pruneHidden, bool pruneMergedCellProxies, bool trimInteriorSlices)
		{
			Axis axis;
			AxisAligned<Ranges<TableUnit>[]> nonEmptyRangesCache = this._nonEmptyRangesCache.GetOrAdd(Record.Create<bool, bool>(pruneHidden, pruneMergedCellProxies), delegate(Record<bool, bool> _)
			{
				RectangularArray<ISpreadsheetCell> prunedCells = this.PrunedCells(true, pruneHidden, pruneMergedCellProxies);
				return new AxisAligned<Ranges<TableUnit>[]>((Axis axis) => (from i in Enumerable.Range(0, this.Size[axis.Perpendicular()])
					select new Ranges<TableUnit>(Enumerable.Range(0, this.Size[axis]).Collect(delegate(int j)
					{
						ISpreadsheetCell spreadsheetCell = ((axis == Axis.Vertical) ? prunedCells[i, j] : prunedCells[j, i]);
						if (!string.IsNullOrWhiteSpace((spreadsheetCell != null) ? spreadsheetCell.AsString : null))
						{
							return new int?(j);
						}
						return null;
					}), false)).ToArray<Ranges<TableUnit>>());
			});
			Dictionary<Direction, int> dictionary = new Dictionary<Direction, int>();
			foreach (Direction direction in DirectionUtilities.Directions)
			{
				axis = direction.AlignedAxis();
				Range<TableUnit> range = span[axis];
				Range<TableUnit> untrimmedPerpendicular = span[axis.Perpendicular()];
				IEnumerable<int> enumerable = ((direction.Derivative() == Derivative.Decreasing) ? range.AsEnumerable : range.AsReverseEnumerable);
				Ranges<TableUnit>[] nonEmptyRanges = nonEmptyRangesCache[axis.Perpendicular()];
				int? num = enumerable.MaybeFirst((int i) => nonEmptyRanges[i].Overlaps(untrimmedPerpendicular)).OrElseNull<int>();
				if (num == null)
				{
					return null;
				}
				int valueOrDefault = num.GetValueOrDefault();
				dictionary[direction] = valueOrDefault;
			}
			Bounds<TableUnit> resSpan = new Bounds<TableUnit>(dictionary);
			if (trimInteriorSlices)
			{
				Ranges<TableUnit> ranges = new Ranges<TableUnit>(resSpan.Horizontal.AsEnumerable.Where((int col) => nonEmptyRangesCache.Vertical[col].Overlaps(resSpan.Vertical)), true);
				Ranges<TableUnit> ranges2 = new Ranges<TableUnit>(resSpan.Vertical.AsEnumerable.Where((int col) => nonEmptyRangesCache.Horizontal[col].Overlaps(resSpan.Horizontal)), true);
				return new SpreadsheetArea(this, resSpan, ranges, ranges2, null);
			}
			return new SpreadsheetArea(this, resSpan, null, null, null);
		}

		// Token: 0x060066E5 RID: 26341 RVA: 0x0014F6F4 File Offset: 0x0014D8F4
		private bool[][] AllMatchingRows(StyleFilter styleFilter, Bounds<TableUnit>? intersection)
		{
			bool[][] array = new bool[this.Size.Y][];
			RectangularArray<ISpreadsheetCell> rectangularArray = this.PrunedCells(true, false, true);
			array[0] = new bool[5];
			for (int i = 1; i < array.Length; i++)
			{
				bool[] array2 = new bool[5];
				bool[] array3 = ((intersection != null) ? (rectangularArray.Row(i, intersection.Value) ?? Enumerable.Empty<ISpreadsheetCell>()) : rectangularArray.Row(i)).Collect<ISpreadsheetCell>().Select(new Func<ISpreadsheetCell, bool>(styleFilter.Matches)).ToArray<bool>();
				bool flag = array3.Length == 0;
				bool flag2 = array3.Length == 1;
				bool flag3 = array3.Length >= 2;
				bool flag4 = !flag && array3[0];
				bool flag5;
				if (!flag)
				{
					flag5 = array3.Skip(1).Any((bool b) => b);
				}
				else
				{
					flag5 = false;
				}
				bool flag6 = flag5;
				bool flag7;
				if (flag3)
				{
					flag7 = array3.Skip(1).All((bool b) => b);
				}
				else
				{
					flag7 = false;
				}
				bool flag8 = flag7;
				array2[0] = flag4 || flag6;
				array2[1] = flag4;
				array2[2] = flag8;
				array2[3] = flag4 && flag8;
				array2[4] = flag2 && array2[0];
				array[i] = array2;
			}
			return array;
		}

		// Token: 0x060066E6 RID: 26342 RVA: 0x0014F844 File Offset: 0x0014DA44
		private bool[] AllMatchingRows(SplitMode splitMode, Bounds<TableUnit>? intersection)
		{
			bool[] array = new bool[this.Size.Y];
			RectangularArray<ISpreadsheetCell> rectangularArray = this.PrunedCells(true, false, false);
			array[0] = false;
			for (int i = 1; i < array.Length; i++)
			{
				int num = ((intersection != null) ? (rectangularArray.Row(i - 1, intersection.Value) ?? Enumerable.Empty<ISpreadsheetCell>()) : rectangularArray.Row(i - 1)).Collect(delegate(ISpreadsheetCell cell)
				{
					IMergedSpreadsheetCellProxy mergedSpreadsheetCellProxy = cell as IMergedSpreadsheetCellProxy;
					if (mergedSpreadsheetCellProxy == null)
					{
						return cell;
					}
					return mergedSpreadsheetCellProxy.OriginalCell;
				}).Distinct<ISpreadsheetCell>().Take(2)
					.Count<ISpreadsheetCell>();
				array[i] = (((splitMode & SplitMode.RequirePrecedingBlank) != SplitMode.Any) ? (num == 0) : ((splitMode & SplitMode.RequirePrecedingAtMostOne) == SplitMode.Any || num <= 1));
			}
			return array;
		}

		// Token: 0x060066E7 RID: 26343 RVA: 0x0014F908 File Offset: 0x0014DB08
		public IEnumerable<int> MatchingRows(Bounds<TableUnit> span, StyleFilter styleFilter, SplitMode splitMode)
		{
			AbstractSpreadsheet.<>c__DisplayClass29_0 CS$<>8__locals1 = new AbstractSpreadsheet.<>c__DisplayClass29_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.intersection = ((span.Width() == this.Size.X) ? null : new Bounds<TableUnit>?(span.Extend(Direction.Up, 1)));
			Ranges<TableUnit> ranges = ((CS$<>8__locals1.intersection != null) ? CS$<>8__locals1.<MatchingRows>g__ComputeForStyleFilter|0(styleFilter) : this._styleFilterCache.GetOrAdd(styleFilter, new Func<StyleFilter, Ranges<TableUnit>[]>(CS$<>8__locals1.<MatchingRows>g__ComputeForStyleFilter|0)))[(int)(splitMode & SplitMode.CellSelectionMask)];
			if ((splitMode & SplitMode.RequirePrecedingMask) != SplitMode.Any)
			{
				SplitMode splitMode2 = splitMode & SplitMode.RequirePrecedingMask;
				Ranges<TableUnit> ranges2 = ((CS$<>8__locals1.intersection != null) ? CS$<>8__locals1.<MatchingRows>g__ComputeForSplitMode|3(splitMode2) : this._splitModeCache.GetOrAdd(splitMode2, new Func<SplitMode, Ranges<TableUnit>>(CS$<>8__locals1.<MatchingRows>g__ComputeForSplitMode|3)));
				ranges = ranges.Intersect(ranges2);
			}
			ranges = ranges.Intersect(span.Vertical.Expand(-1, Derivative.Decreasing));
			return ranges.SelectMany((Range<TableUnit> range) => range.AsEnumerable);
		}

		// Token: 0x04002D6A RID: 11626
		private readonly Lazy<BorderCollection> _bordersLazy;

		// Token: 0x04002D6B RID: 11627
		private readonly Lazy<SpreadsheetPatterns> _patternsLazy;

		// Token: 0x04002D6C RID: 11628
		private readonly Lazy<IReadOnlyList<Bounds<TableUnit>>> _mergedCellsLazy;

		// Token: 0x04002D6D RID: 11629
		private readonly ConcurrentDictionary<AbstractSpreadsheet.PruneFlags, RectangularArray<ISpreadsheetCell>> _prunedCellsCache = new ConcurrentDictionary<AbstractSpreadsheet.PruneFlags, RectangularArray<ISpreadsheetCell>>();

		// Token: 0x04002D6E RID: 11630
		private readonly ConcurrentDictionary<Record<bool, bool>, AxisAligned<Ranges<TableUnit>[]>> _nonEmptyRangesCache = new ConcurrentDictionary<Record<bool, bool>, AxisAligned<Ranges<TableUnit>[]>>();

		// Token: 0x04002D6F RID: 11631
		private readonly ConcurrentDictionary<StyleFilter, Ranges<TableUnit>[]> _styleFilterCache = new ConcurrentDictionary<StyleFilter, Ranges<TableUnit>[]>();

		// Token: 0x04002D70 RID: 11632
		private readonly ConcurrentDictionary<SplitMode, Ranges<TableUnit>> _splitModeCache = new ConcurrentDictionary<SplitMode, Ranges<TableUnit>>();

		// Token: 0x02000EC2 RID: 3778
		[Flags]
		private enum PruneFlags
		{
			// Token: 0x04002D72 RID: 11634
			Null = 1,
			// Token: 0x04002D73 RID: 11635
			Hidden = 2,
			// Token: 0x04002D74 RID: 11636
			MergedProxies = 4,
			// Token: 0x04002D75 RID: 11637
			Count = 8
		}
	}
}
