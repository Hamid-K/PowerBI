using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Utils.Logging;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D2A RID: 3370
	[NullableContext(1)]
	[Nullable(0)]
	internal static class PdfTableBuilder
	{
		// Token: 0x06005658 RID: 22104 RVA: 0x00111058 File Offset: 0x0010F258
		public static IProsePdfTable<TCell> BuildFullPageTable<TCell>(int pageIndex, QuadTree<TCell, PixelUnit> cells, AxisAlignedList<IBoundedList<TCell>> lists, PdfAnalyzerOptions options) where TCell : class, IWordAmalgamation<TCell>
		{
			PdfTableBuilder.<>c__DisplayClass0_0<TCell> CS$<>8__locals1 = new PdfTableBuilder.<>c__DisplayClass0_0<TCell>();
			CS$<>8__locals1.lists = lists;
			IStopwatchWrapper stopwatchWrapper = Logger.Instance.InfoTiming("Build Full Page Table", "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\PdfTableBuilder.cs", 16);
			CS$<>8__locals1.listsWithoutOverlaps = new AxisAligned<List<Record<IBoundedList<TCell>, HashSet<TCell>, int>>>((Axis axis) => CS$<>8__locals1.lists[axis].Select((IBoundedList<TCell> l, int idx) => Record.Create<IBoundedList<TCell>, HashSet<TCell>, int>(l, new HashSet<TCell>(l.Cells), idx)).ToList<Record<IBoundedList<TCell>, HashSet<TCell>, int>>());
			bool flag = options.Version >= PdfAnalyzerVersion.V1_2;
			RectangularArray<TCell> rectangularArray;
			bool flag2;
			do
			{
				rectangularArray = new RectangularArray<TCell>(CS$<>8__locals1.lists.Vertical.Count, CS$<>8__locals1.lists.Horizontal.Count);
				flag2 = false;
				foreach (Axis axis2 in AxisUtilities.Axes)
				{
					CS$<>8__locals1.listsWithoutOverlaps[axis2].Sort(delegate([Nullable(new byte[] { 0, 1, 1, 1, 1 })] Record<IBoundedList<TCell>, HashSet<TCell>, int> a, [Nullable(new byte[] { 0, 1, 1, 1, 1 })] Record<IBoundedList<TCell>, HashSet<TCell>, int> b)
					{
						int num = b.Item2.Count - a.Item2.Count;
						if (num != 0)
						{
							return num;
						}
						return a.Item3 - b.Item3;
					});
				}
				using (IEnumerator<TCell> enumerator2 = cells.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						PdfTableBuilder.<>c__DisplayClass0_1<TCell> CS$<>8__locals2 = new PdfTableBuilder.<>c__DisplayClass0_1<TCell>();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.cell = enumerator2.Current;
						PdfTableBuilder.<>c__DisplayClass0_2<TCell> CS$<>8__locals3 = new PdfTableBuilder.<>c__DisplayClass0_2<TCell>();
						CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
						CS$<>8__locals3.bestLists = CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.listsWithoutOverlaps.Select(delegate(List<Record<IBoundedList<TCell>, HashSet<TCell>, int>> l)
						{
							Func<Record<IBoundedList<TCell>, HashSet<TCell>, int>, bool> func;
							if ((func = CS$<>8__locals3.CS$<>8__locals2.<>9__6) == null)
							{
								func = (CS$<>8__locals3.CS$<>8__locals2.<>9__6 = (Record<IBoundedList<TCell>, HashSet<TCell>, int> r) => r.Item2.Contains(CS$<>8__locals3.CS$<>8__locals2.cell));
							}
							return l.First(func);
						});
						Vector<TableUnit> vector = new Vector<TableUnit>((Axis axis) => CS$<>8__locals3.bestLists[axis.Perpendicular()].Item3);
						CS$<>8__locals3.existingElement = rectangularArray[vector];
						if (CS$<>8__locals3.existingElement != null)
						{
							if (flag && !CS$<>8__locals3.existingElement.Overlaps(CS$<>8__locals3.CS$<>8__locals2.cell))
							{
								if (CS$<>8__locals3.existingElement.Overlaps(CS$<>8__locals3.CS$<>8__locals2.cell, Axis.Horizontal))
								{
									if (CS$<>8__locals3.<BuildFullPageTable>g__TryRemove|5(Axis.Horizontal))
									{
										flag2 = true;
										break;
									}
								}
								else if (CS$<>8__locals3.<BuildFullPageTable>g__TryRemove|5(Axis.Vertical))
								{
									flag2 = true;
									break;
								}
							}
							rectangularArray[vector] = CS$<>8__locals3.existingElement.CombineWithOverlappingCellInTable(CS$<>8__locals3.CS$<>8__locals2.cell);
						}
						else
						{
							rectangularArray[vector] = CS$<>8__locals3.CS$<>8__locals2.cell;
						}
					}
				}
			}
			while (flag2);
			IProsePdfTable<TCell> prosePdfTable = PdfTable<TCell>.CreateFullPageTable(rectangularArray.Collapse(null), pageIndex);
			stopwatchWrapper.Stop();
			Logger.Instance.Debug(string.Format("Full Page Table: \n{0}", prosePdfTable), null, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\PdfTableBuilder.cs", 95);
			return prosePdfTable;
		}

		// Token: 0x06005659 RID: 22105 RVA: 0x00111308 File Offset: 0x0010F508
		[return: Nullable(new byte[] { 0, 1 })]
		private static Bounds<PixelUnit> ExpandToOverlappingSeparators([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, int textRunHeight, SeparatorCollection separators)
		{
			IReadOnlyList<Bounds<PixelUnit>> readOnlyList = separators.Separators.Select(delegate(Axis axis, QuadTree<Separator, PixelUnit> tree)
			{
				Bounds<PixelUnit> bounds3 = ((axis == Axis.Horizontal) ? bounds.Extend(Axis.Vertical, textRunHeight) : bounds);
				return Bounds<PixelUnit>.MaybeJoin(from sep in tree.OverlappingElements(bounds3)
					where sep.StrokingColor != null
					select sep.PixelBounds);
			}).AsEnumerable.SelectValues<Bounds<PixelUnit>>().ToList<Bounds<PixelUnit>>();
			if (readOnlyList.Count == 0)
			{
				return bounds;
			}
			Bounds<PixelUnit> bounds2 = Bounds<PixelUnit>.Join(readOnlyList.AppendItem(bounds));
			if (bounds2 == bounds)
			{
				return bounds;
			}
			return PdfTableBuilder.ExpandToOverlappingSeparators(bounds2, textRunHeight, separators);
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x00111394 File Offset: 0x0010F594
		[return: Nullable(new byte[] { 0, 1 })]
		private static Bounds<PixelUnit>? ExpandGridAlongSeparators<TCell>(Grid<TCell> grid, SeparatorCollection separators, QuadTree<SeparatorGrid, PixelUnit> separatorGrids) where TCell : class, IWordAmalgamation<TCell>
		{
			int num = grid.Cells.Max(delegate(TCell c)
			{
				if (c.Children.Any<IWord>())
				{
					return c.Children.Max((IWord word) => word.ApparentPixelBoundsWithoutRotation.Height());
				}
				return 0;
			});
			Bounds<PixelUnit> bounds = PdfTableBuilder.ExpandToOverlappingSeparators(grid.PixelBounds, num, separators);
			if (separatorGrids.OverlappingElements(bounds).Any((SeparatorGrid sg) => !sg.Overlaps(grid.PixelBounds)))
			{
				return null;
			}
			return new Bounds<PixelUnit>?(bounds);
		}

		// Token: 0x0600565B RID: 22107 RVA: 0x0011141C File Offset: 0x0010F61C
		[return: Nullable(new byte[] { 0, 1 })]
		private static Bounds<PixelUnit>? MaybeExpandGridUsingHorizontalSeparators<TCell>(PdfAnalyzerOptions options, Grid<TCell> grid, AxisAligned<bool> gridContainsBorderLines, SeparatorCollection separators) where TCell : class, IWordAmalgamation<TCell>
		{
			if (!gridContainsBorderLines[Axis.Horizontal] || gridContainsBorderLines[Axis.Vertical])
			{
				return null;
			}
			IEnumerable<PdfTableBuilder.StrokedSeparator> enumerable = from r in separators.Separators.Horizontal.OverlappingElements(grid.PixelBounds).Collect((Separator sep) => PdfTableBuilder.StrokedSeparator.TryCreate(sep, grid.PixelBounds.Horizontal))
				where (double)r.Width > 0.8 * (double)grid.PixelBounds.Width()
				orderby r.Width descending
				select r;
			Func<PdfTableBuilder.StrokedSeparator, int> func = (PdfTableBuilder.StrokedSeparator r) => r.Width;
			Func<int, int, bool> func2 = (int a, int b) => MathUtils.WithinTolerance(a, b, 5);
			Func<int, int, int> func3;
			if ((func3 = PdfTableBuilder.<>O.<0>__Max) == null)
			{
				func3 = (PdfTableBuilder.<>O.<0>__Max = new Func<int, int, int>(Math.Max));
			}
			IGrouping<int, PdfTableBuilder.StrokedSeparator> grouping = enumerable.SplitRuns(func, func2, func3).FirstOrDefault<IGrouping<int, PdfTableBuilder.StrokedSeparator>>();
			if (grouping == null)
			{
				return null;
			}
			IEnumerable<Record<Color, Color>> enumerable2 = grouping.Select((PdfTableBuilder.StrokedSeparator sep) => sep.StrokingColor).Windowed<Color>();
			Func<Color, Color, bool> func4;
			if ((func4 = PdfTableBuilder.<>O.<1>__ColorEquals) == null)
			{
				func4 = (PdfTableBuilder.<>O.<1>__ColorEquals = new Func<Color, Color, bool>(ColorUtils.ColorEquals));
			}
			if (enumerable2.All2(func4))
			{
				if (grouping.Select((PdfTableBuilder.StrokedSeparator sep) => sep.Separator.Line.Range.Min).ExtremaWithin(5))
				{
					if (grouping.Select((PdfTableBuilder.StrokedSeparator sep) => sep.Separator.LineWidth).ExtremaWithin(1))
					{
						Color strokingColor = grouping.First<PdfTableBuilder.StrokedSeparator>().StrokingColor;
						Range<PixelUnit> range = Range<PixelUnit>.Join(grouping.Select((PdfTableBuilder.StrokedSeparator sep) => sep.Separator.Line.Range));
						Range<PixelUnit> lineWidthRange = Range<PixelUnit>.Create(grouping.Select((PdfTableBuilder.StrokedSeparator sep) => sep.Separator.LineWidth).Extrema(null)).Expand(1);
						List<Separator> list = separators.Separators.Horizontal.Where((Separator sep) => sep.StrokingColor != null && sep.StrokingColor.Value.ColorEquals(strokingColor) && lineWidthRange.Contains(sep.LineWidth) && RangeUtils.WithinToleranceOnBothSides<PixelUnit>(sep.Line.Range, range, 5)).ToList<Separator>();
						Func<Separator, Optional<SeparatorGroup>> groupForSeparator;
						if (options.Version >= PdfAnalyzerVersion.V1_2)
						{
							groupForSeparator = (Separator sep) => (from g in separators.Groups.ElementsThatContain(sep.PixelBounds)
								where g.Separators.Contains(sep)
								select g).MaybeOnly<SeparatorGroup>();
						}
						else
						{
							groupForSeparator = (Separator _) => default(Optional<SeparatorGroup>);
						}
						Optional<int> optional = from sep in list.Where((Separator sep) => sep.Line.Position <= grid.PixelBounds.Top).MaybeArgMax((Separator sep) => sep.Line.Position)
							select (from g in groupForSeparator(sep)
								select g.PixelBounds.Top).OrElse(sep.Line.Position);
						Optional<int> optional2 = from sep in list.Where((Separator sep) => sep.Line.Position >= grid.PixelBounds.Bottom).MaybeArgMin((Separator sep) => sep.Line.Position)
							select (from g in groupForSeparator(sep)
								select g.PixelBounds.Bottom).OrElse(sep.Line.Position);
						if (!optional.Select((int pos) => pos < grid.PixelBounds.Top).OrElse(false) && !optional2.Select((int pos) => pos > grid.PixelBounds.Bottom).OrElse(false))
						{
							return null;
						}
						Bounds<PixelUnit> bounds = grid.PixelBounds.With(Axis.Horizontal, grid.PixelBounds.Horizontal.Join(range));
						if (optional.HasValue)
						{
							bounds = bounds.With(Direction.Up, optional.Value);
						}
						if (optional2.HasValue)
						{
							bounds = bounds.With(Direction.Down, optional2.Value);
						}
						if (separators.Separators.Vertical.OverlappingElements(bounds).Any((Separator sep) => sep.StrokingColor != null))
						{
							return null;
						}
						return new Bounds<PixelUnit>?(bounds);
					}
				}
			}
			return null;
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x00111858 File Offset: 0x0010FA58
		[return: Nullable(new byte[] { 0, 1 })]
		private static Bounds<PixelUnit>? ExpandGridUsingSeparators<TCell>(PdfAnalyzerOptions options, Grid<TCell> grid, SeparatorCollection separators, QuadTree<SeparatorGrid, PixelUnit> separatorGrids) where TCell : class, IWordAmalgamation<TCell>
		{
			AxisAligned<bool> axisAligned = separators.Separators.Select((QuadTree<Separator, PixelUnit> tree) => tree.OverlappingElements(grid.PixelBounds).Any((Separator sep) => sep.StrokingColor != null));
			bool flag = separatorGrids.ElementsThatContain(grid.PixelBounds).Any<SeparatorGrid>();
			if (!axisAligned.Any((bool b) => b) || flag)
			{
				return null;
			}
			Bounds<PixelUnit>? bounds = PdfTableBuilder.MaybeExpandGridUsingHorizontalSeparators<TCell>(options, grid, axisAligned, separators);
			if (bounds == null)
			{
				return PdfTableBuilder.ExpandGridAlongSeparators<TCell>(grid, separators, separatorGrids);
			}
			return bounds;
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x00111900 File Offset: 0x0010FB00
		[return: Nullable(new byte[] { 0, 1 })]
		private static Bounds<TableUnit>? NormalizeExpandedGridToTableBounds<TCell>([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit>? expandedBounds, Grid<TCell> grid, SeparatorCollection separators, QuadTree<TCell, PixelUnit> cells, IProsePdfTable<TCell> fullTable, PdfAnalyzerOptions options, Axis? checkForNullsAxis) where TCell : class, IWordAmalgamation<TCell>
		{
			if (expandedBounds == null)
			{
				return new Bounds<TableUnit>?(grid.TableBounds);
			}
			bool includeOverlappingCells = options.Version >= PdfAnalyzerVersion.V1_2;
			Bounds<TableUnit>? bounds = fullTable.FindTableBounds(includeOverlappingCells ? cells.OverlappingElements(expandedBounds.Value) : cells.ContainedElements(expandedBounds.Value));
			if (bounds != null)
			{
				Bounds<PixelUnit>? bounds2 = expandedBounds;
				Bounds<PixelUnit> pixelBounds = grid.PixelBounds;
				if ((bounds2 == null || (bounds2 != null && bounds2.GetValueOrDefault() != pixelBounds)) && bounds.Value != grid.TableBounds)
				{
					IProsePdfTable<TCell> prosePdfTable = fullTable.CollapsedSection(bounds.Value, separators, options, true, (options.Version >= PdfAnalyzerVersion.V1_2) ? options.CombineCompatibleAlongAxis : null);
					using (IEnumerator<Direction> enumerator = DirectionUtilities.Directions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Direction direction = enumerator.Current;
							if (bounds.Value[direction] != grid.TableBounds[direction] && bounds.Value[direction.AlignedAxis()].Size() > 1 && (checkForNullsAxis == null || direction.AlignedAxis() == checkForNullsAxis.Value))
							{
								Func<ContiguousList<TCell>, bool> <>9__5;
								if (!fullTable.Section(bounds.Value.Edges[direction].Bounds).Table.ToEnumerableNonNull<TCell>().MaybeOnly<TCell>().Select(delegate(TCell cell)
								{
									if (!includeOverlappingCells || !cell.PixelBounds.Overlaps(grid.PixelBounds, direction.AlignedAxis()))
									{
										IEnumerable<ContiguousList<TCell>> enumerable = cell.ContiguousLists[direction.AlignedAxis()];
										Func<ContiguousList<TCell>, bool> func;
										if ((func = <>9__5) == null)
										{
											func = (<>9__5 = (ContiguousList<TCell> l) => !l.PixelBounds.Overlaps(grid.PixelBounds, direction.AlignedAxis()));
										}
										return enumerable.All(func);
									}
									return false;
								})
									.OrElse(false))
								{
									if (!(from slice in prosePdfTable.Table.Slices(direction.AlignedAxis())
										select slice.ToList<TCell>() into sliceList
										select Record.Create<List<TCell>, TCell>(sliceList, (direction.Derivative() == Derivative.Decreasing) ? sliceList.First<TCell>() : sliceList.Last<TCell>())).Where2((List<TCell> _, TCell cell) => cell != null).Any((Record<List<TCell>, TCell> t) => !t.Item1.Where((TCell c) => c != null).HasAtLeast(2)))
									{
										continue;
									}
								}
								bounds = new Bounds<TableUnit>?(bounds.Value.Extend(direction, -1));
							}
						}
					}
				}
			}
			return bounds;
		}

		// Token: 0x0600565E RID: 22110 RVA: 0x00111BFC File Offset: 0x0010FDFC
		[return: Nullable(new byte[] { 0, 1 })]
		private static Bounds<TableUnit>? NormalizedExpandedGridUsingSeparators<TCell>(Grid<TCell> grid, SeparatorCollection separators, QuadTree<SeparatorGrid, PixelUnit> separatorGrids, QuadTree<TCell, PixelUnit> cells, IProsePdfTable<TCell> fullTable, PdfAnalyzerOptions options, Axis? checkForNullsAxis) where TCell : class, IWordAmalgamation<TCell>
		{
			return PdfTableBuilder.NormalizeExpandedGridToTableBounds<TCell>(PdfTableBuilder.ExpandGridUsingSeparators<TCell>(options, grid, separators, separatorGrids), grid, separators, cells, fullTable, options, checkForNullsAxis);
		}

		// Token: 0x0600565F RID: 22111 RVA: 0x00111C18 File Offset: 0x0010FE18
		public static IReadOnlyList<IProsePdfTable<TCell>> BuildSimpleTables<TCell>(PdfAnalyzerOptions options, QuadTree<TCell, PixelUnit> cells, SeparatorCollection separators, QuadTree<SeparatorGrid, PixelUnit> separatorGrids, AlignmentDotCollection alignmentDotCollection, IProsePdfTable<TCell> fullTable, IReadOnlyList<Grid<TCell>> grids, Axis? checkForNullsAxis = null) where TCell : class, IWordAmalgamation<TCell>
		{
			bool forceSeparatorGrids = options.ForceSeparatorGrids;
			IReadOnlyList<Record<SeparatorGrid, Bounds<TableUnit>>> separatorGridsTableBounds = (from sepGrid in separatorGrids
				let tableBounds = fullTable.FindTableBounds(cells.ContainedElements(sepGrid.PixelBounds))
				where tableBounds != null
				select Record.Create<SeparatorGrid, Bounds<TableUnit>>(sepGrid, tableBounds.Value)).ToList<Record<SeparatorGrid, Bounds<TableUnit>>>();
			MultiValueDictionary<AlignmentDotCollection.AlignmentDotRow, TCell> cellsByAlignmentDotRow = cells.GroupByNonNull((TCell cell) => cell.AfterAlignmentDotRow).ToMultiValueDictionary(null);
			IEnumerable<Bounds<TableUnit>> enumerable = grids.OrderByDescending((Grid<TCell> grid) => grid.PixelBounds.Area()).Collect((Grid<TCell> grid) => PdfTableBuilder.NormalizedExpandedGridUsingSeparators<TCell>(grid, separators, separatorGrids, cells, fullTable, options, checkForNullsAxis));
			IEnumerable<Bounds<TableUnit>> enumerable3;
			if (!forceSeparatorGrids)
			{
				IEnumerable<Bounds<TableUnit>> enumerable2 = separatorGridsTableBounds.Select((Record<SeparatorGrid, Bounds<TableUnit>> r) => r.Item2);
				enumerable3 = enumerable2;
			}
			else
			{
				enumerable3 = Enumerable.Empty<Bounds<TableUnit>>();
			}
			List<PdfTableBuilder.SimpleTableSeed> list = (from bounds in enumerable.Concat(enumerable3).Concat(alignmentDotCollection.DotColumns.Collect((AlignmentDotCollection.AlignmentDotColumn dotColumn) => fullTable.FindTableBounds(cells.OverlappingElements(dotColumn.ApparentPixelBounds).Concat(cellsByAlignmentDotRow.Where((KeyValuePair<AlignmentDotCollection.AlignmentDotRow, IReadOnlyCollection<TCell>> kv) => dotColumn.DotRows.Contains(kv.Key)).SelectMany((KeyValuePair<AlignmentDotCollection.AlignmentDotRow, IReadOnlyCollection<TCell>> kv) => kv.Value))))).Distinct<Bounds<TableUnit>>()
				select new PdfTableBuilder.SimpleTableSeed(bounds, separatorGridsTableBounds.Any2(delegate(SeparatorGrid sepGrid, Bounds<TableUnit> gridBounds)
				{
					if (bounds.Equals(gridBounds))
					{
						return sepGrid.HasEdgeSeparators.All((bool b) => b);
					}
					return false;
				})) into seed
				orderby seed.IsFromBorderedSeparatorGrid
				select seed).ToList<PdfTableBuilder.SimpleTableSeed>();
			bool flag = true;
			bool flag2;
			do
			{
				flag2 = false;
				bool flag3 = false;
				for (int i = 0; i < list.Count; i++)
				{
					PdfTableBuilder.SimpleTableSeed currentSeed = list[i];
					IReadOnlyList<Record<int, PdfTableBuilder.SimpleTableSeed>> readOnlyList = (from seed in list.Enumerate<PdfTableBuilder.SimpleTableSeed>().Skip(i + 1)
						where seed.Item2.Overlaps(currentSeed)
						select seed).ToList<Record<int, PdfTableBuilder.SimpleTableSeed>>();
					if (readOnlyList.Any<Record<int, PdfTableBuilder.SimpleTableSeed>>())
					{
						flag2 = true;
						IReadOnlyList<PdfTableBuilder.SimpleTableSeed> readOnlyList2 = readOnlyList.Select((Record<int, PdfTableBuilder.SimpleTableSeed> b) => b.Item2).AppendItem(currentSeed).ToList<PdfTableBuilder.SimpleTableSeed>();
						PdfTableBuilder.SimpleTableSeed joinedSeeds = PdfTableBuilder.SimpleTableSeed.Join(readOnlyList2);
						IReadOnlyList<PdfTableBuilder.SimpleTableSeed> readOnlyList3 = (from seed in list.Skip(i + 1).SkipWhile((PdfTableBuilder.SimpleTableSeed seed) => !seed.IsFromBorderedSeparatorGrid).Except(readOnlyList2)
							where seed.Overlaps(joinedSeeds)
							select seed).ToList<PdfTableBuilder.SimpleTableSeed>();
						if (readOnlyList2.Concat(readOnlyList3).Any((PdfTableBuilder.SimpleTableSeed seed) => seed.IsFromBorderedSeparatorGrid))
						{
							if (readOnlyList2.Any((PdfTableBuilder.SimpleTableSeed seed) => !seed.IsFromBorderedSeparatorGrid))
							{
								if (PdfTableBuilder.SimpleTableSeed.Join(from seed in readOnlyList2.Concat(readOnlyList3)
									where seed.IsFromBorderedSeparatorGrid
									select seed).Contains(joinedSeeds))
								{
									if (!flag)
									{
										foreach (int num in readOnlyList.PrependItem(Record.Create<int, PdfTableBuilder.SimpleTableSeed>(i, currentSeed)).Where2((int _, PdfTableBuilder.SimpleTableSeed seed) => !seed.IsFromBorderedSeparatorGrid).Select2((int idx, PdfTableBuilder.SimpleTableSeed _) => idx)
											.Reverse<int>())
										{
											list.RemoveAt(num);
										}
										flag3 = true;
										goto IL_0455;
									}
									goto IL_0455;
								}
							}
						}
						list[i] = joinedSeeds;
						foreach (int num2 in readOnlyList.Select2((int idx, PdfTableBuilder.SimpleTableSeed _) => idx).Reverse<int>())
						{
							list.RemoveAt(num2);
						}
						flag3 = true;
					}
					IL_0455:;
				}
				flag = flag3;
			}
			while (flag2);
			if (forceSeparatorGrids)
			{
				list.RemoveAll((PdfTableBuilder.SimpleTableSeed seed) => separatorGridsTableBounds.Any2((SeparatorGrid _, Bounds<TableUnit> gridBounds) => seed.Overlaps(gridBounds)));
				list.AddRange(separatorGridsTableBounds.Select2((SeparatorGrid sepGrid, Bounds<TableUnit> gridBounds) => new PdfTableBuilder.SimpleTableSeed(gridBounds, true)));
			}
			list.Sort(CompareByIBoundedReadOrder<TableUnit>.Instance);
			return list.Select(delegate(PdfTableBuilder.SimpleTableSeed seed)
			{
				IProsePdfTable<TCell> fullTable2 = fullTable;
				Bounds<TableUnit> tableBounds = seed.TableBounds;
				SeparatorCollection separators2 = separators;
				PdfAnalyzerOptions options2 = options;
				bool flag4 = !forceSeparatorGrids || !seed.IsFromBorderedSeparatorGrid;
				PdfAnalyzerOptions options3 = options;
				return fullTable2.CollapsedSection(tableBounds, separators2, options2, flag4, (options3 != null) ? options3.CombineCompatibleAlongAxis : null);
			}).ToList<IProsePdfTable<TCell>>();
		}

		// Token: 0x02000D2B RID: 3371
		[Nullable(0)]
		private class StrokedSeparator
		{
			// Token: 0x17000FB0 RID: 4016
			// (get) Token: 0x06005660 RID: 22112 RVA: 0x00112120 File Offset: 0x00110320
			public Separator Separator { get; }

			// Token: 0x17000FB1 RID: 4017
			// (get) Token: 0x06005661 RID: 22113 RVA: 0x00112128 File Offset: 0x00110328
			public int Width { get; }

			// Token: 0x17000FB2 RID: 4018
			// (get) Token: 0x06005662 RID: 22114 RVA: 0x00112130 File Offset: 0x00110330
			public Color StrokingColor { get; }

			// Token: 0x06005663 RID: 22115 RVA: 0x00112138 File Offset: 0x00110338
			public StrokedSeparator(Separator separator, int width, Color strokingColor)
			{
				this.Separator = separator;
				this.Width = width;
				this.StrokingColor = strokingColor;
			}

			// Token: 0x06005664 RID: 22116 RVA: 0x00112158 File Offset: 0x00110358
			[return: Nullable(2)]
			public static PdfTableBuilder.StrokedSeparator TryCreate(Separator sep, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> gridHorizontal)
			{
				Color? strokingColor = sep.StrokingColor;
				if (strokingColor == null)
				{
					return null;
				}
				return new PdfTableBuilder.StrokedSeparator(sep, sep.Line.Range.Intersect(gridHorizontal).Value.Size(), strokingColor.Value);
			}
		}

		// Token: 0x02000D2C RID: 3372
		[NullableContext(0)]
		private class SimpleTableSeed : ITableBounded, IBounded<TableUnit>
		{
			// Token: 0x17000FB3 RID: 4019
			// (get) Token: 0x06005665 RID: 22117 RVA: 0x001121AB File Offset: 0x001103AB
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<TableUnit> TableBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x17000FB4 RID: 4020
			// (get) Token: 0x06005666 RID: 22118 RVA: 0x001121B3 File Offset: 0x001103B3
			[Nullable(new byte[] { 0, 1 })]
			Bounds<TableUnit> IBounded<TableUnit>.Bounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.TableBounds;
				}
			}

			// Token: 0x17000FB5 RID: 4021
			// (get) Token: 0x06005667 RID: 22119 RVA: 0x001121BB File Offset: 0x001103BB
			public bool IsFromBorderedSeparatorGrid { get; }

			// Token: 0x06005668 RID: 22120 RVA: 0x001121C3 File Offset: 0x001103C3
			public SimpleTableSeed([Nullable(new byte[] { 0, 1 })] Bounds<TableUnit> tableBounds, bool isFromBorderedSeparatorGrid)
			{
				this.TableBounds = tableBounds;
				this.IsFromBorderedSeparatorGrid = isFromBorderedSeparatorGrid;
			}

			// Token: 0x06005669 RID: 22121 RVA: 0x001121D9 File Offset: 0x001103D9
			[NullableContext(1)]
			public static PdfTableBuilder.SimpleTableSeed Join(IEnumerable<PdfTableBuilder.SimpleTableSeed> xs)
			{
				return new PdfTableBuilder.SimpleTableSeed(Bounds<TableUnit>.Join(xs.Select((PdfTableBuilder.SimpleTableSeed x) => x.TableBounds)), false);
			}

			// Token: 0x0600566A RID: 22122 RVA: 0x0011220B File Offset: 0x0011040B
			[NullableContext(1)]
			public override string ToString()
			{
				return string.Format("SimpleTableSeed({0}, IsFromSeparatorGrid={1})", this.TableBounds, this.IsFromBorderedSeparatorGrid);
			}
		}

		// Token: 0x02000D2E RID: 3374
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002767 RID: 10087
			[Nullable(0)]
			public static Func<int, int, int> <0>__Max;

			// Token: 0x04002768 RID: 10088
			[Nullable(0)]
			public static Func<Color, Color, bool> <1>__ColorEquals;
		}
	}
}
