using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Utils.Logging;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000C76 RID: 3190
	[NullableContext(1)]
	[Nullable(0)]
	internal static class CellBuilder
	{
		// Token: 0x0600521F RID: 21023 RVA: 0x00102B60 File Offset: 0x00100D60
		private static void CombineCellsByForcedSeparatorGrids(QuadTree<ICell, PixelUnit> cells, QuadTree<SeparatorGrid, PixelUnit> separatorGrids)
		{
			List<ICell> list = new List<ICell>();
			foreach (SeparatorGrid separatorGrid in separatorGrids)
			{
				foreach (SeparatorGrid.SeparatorGridCell separatorGridCell in separatorGrid.CellsEnumerable)
				{
					IReadOnlyList<ICell> readOnlyList;
					ICell cell = separatorGridCell.AsICell(cells, out readOnlyList);
					if (cell == null)
					{
						list.Add(new EmptyCell(separatorGridCell.PixelBounds));
					}
					else
					{
						cells.RemoveRange(readOnlyList);
						list.Add(cell);
					}
				}
			}
			cells.AddRange(list);
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x00102C14 File Offset: 0x00100E14
		private static bool IsSeparatorGridTooSmall(SeparatorGrid grid)
		{
			if (grid.HasEdgeSeparators.All((Direction dir, bool hasSeparatorsOnEdge) => hasSeparatorsOnEdge || dir == Direction.Down))
			{
				if (grid.Cells.Height <= 1 || grid.Cells.Width <= 1)
				{
					return true;
				}
			}
			else if (grid.HasEdgeSeparators.Up && grid.HasEdgeSeparators.Down)
			{
				if (grid.Cells.Height <= 1 || grid.Cells.Width <= 2)
				{
					return true;
				}
			}
			else if (grid.Cells.Height <= 2 || grid.Cells.Width <= 2)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x00102CD4 File Offset: 0x00100ED4
		private static void CombineCellsByHeuristicSeparatorGrids(QuadTree<ICell, PixelUnit> cells, SeparatorCollection separators, QuadTree<SeparatorGrid, PixelUnit> separatorGrids, QuadTree<ITextRun, PixelUnit> textRuns, PdfAnalyzerOptions options)
		{
			using (IEnumerator<SeparatorGrid> enumerator = separatorGrids.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SeparatorGrid grid = enumerator.Current;
					if (!CellBuilder.IsSeparatorGridTooSmall(grid) && !separatorGrids.OverlappingElements(grid.PixelBounds).Any((SeparatorGrid other) => other != grid && !other.Contains(grid)))
					{
						if (options.Version >= PdfAnalyzerVersion.V1_1)
						{
							grid.InitializeCellsByTextRun(textRuns);
						}
						CellBuilder.SeparatorGridMergedCellCollection.Create(grid, separators, cells, options).MergeCells(cells);
					}
				}
			}
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x00102D7C File Offset: 0x00100F7C
		private static void CombineCellsBySeparatorGrids(QuadTree<ICell, PixelUnit> cells, SeparatorCollection separators, QuadTree<SeparatorGrid, PixelUnit> separatorGrids, QuadTree<ITextRun, PixelUnit> textRuns, PdfAnalyzerOptions options, bool forceSeparatorGrid)
		{
			if (forceSeparatorGrid)
			{
				CellBuilder.CombineCellsByForcedSeparatorGrids(cells, separatorGrids);
				return;
			}
			CellBuilder.CombineCellsByHeuristicSeparatorGrids(cells, separators, separatorGrids, textRuns, options);
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x00102D98 File Offset: 0x00100F98
		private static void CombineCellsBySeparators(QuadTree<ICell, PixelUnit> cells, SeparatorCollection separators, AlignmentDotCollection alignmentDotCollection, PdfAnalyzerOptions options)
		{
			QuadTree<AlignmentDotCollection.AlignmentDotColumn, PixelUnit> dotColumns = alignmentDotCollection.DotColumns;
			using (IEnumerator<Bounds<PixelUnit>> enumerator = separators.EnumerateSeparatorPairs(Axis.Horizontal).Select2((Separator topSep, Separator bottomSep) => from bounds in topSep.MaybeBetween(bottomSep)
				select new Bounds<PixelUnit>[]
				{
					bounds.With(Axis.Horizontal, topSep.Line.Range.Join(bottomSep.Line.Range)),
					bounds
				}).SelectValues<Bounds<PixelUnit>[]>()
				.SelectMany((Bounds<PixelUnit>[] b) => b)
				.Distinct<Bounds<PixelUnit>>()
				.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Bounds<PixelUnit> between = enumerator.Current;
					IReadOnlyList<ICell> readOnlyList = (from cell in cells.OverlappingElements(between)
						where cell.Children.All((IWord word) => !word.IsRotated)
						select cell).ToList<ICell>();
					if (readOnlyList.Count >= 2)
					{
						Bounds<PixelUnit> betweenExpanded = between.Extend(Axis.Horizontal, 2);
						if (!readOnlyList.Any((ICell cell) => !betweenExpanded.Contains(cell.ApparentPixelBounds)) && !dotColumns.OverlappingElements(between).Any<AlignmentDotCollection.AlignmentDotColumn>())
						{
							Optional<CellBuilder.SeparatorsMergedCellRow> optional = from row in CellBuilder.SeparatorsMergedCellRow.Build(readOnlyList, options)
								where row.IsValidRow(cells.Bounds, between)
								select row;
							if (optional.HasValue)
							{
								foreach (IGrouping<FontCharacteristics, CellBuilder.SeparatorsMergedCell> grouping in from @group in optional.Value.MergedCells
									where @group.Cells.Count > 1
									group @group by @group.Font)
								{
									int lineWrapDistanceMax = grouping.Select((CellBuilder.SeparatorsMergedCell g) => g.MaxLineSpacing).Min() + 2;
									IEnumerable<CellBuilder.SeparatorsMergedCell> enumerable = grouping;
									Func<CellBuilder.SeparatorsMergedCell, bool> func;
									Func<CellBuilder.SeparatorsMergedCell, bool> <>9__10;
									if ((func = <>9__10) == null)
									{
										func = (<>9__10 = (CellBuilder.SeparatorsMergedCell group) => group.MinLineSpacing <= lineWrapDistanceMax);
									}
									foreach (CellBuilder.SeparatorsMergedCell separatorsMergedCell in enumerable.Where(func))
									{
										cells.MergeCell(separatorsMergedCell);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x0010307C File Offset: 0x0010127C
		private static ICell BuildCellFromTextRun(ITextRun textRun)
		{
			Bounds<PixelUnit> bounds = textRun.Bounds;
			Bounds<PixelUnit> scriptsInclusiveBounds = textRun.ScriptsInclusiveBounds;
			string text = ((textRun.LogicalGlyphOrdering.DirectLines[0].TextDirection == TextDirection.RightToLeft) ? ("\u202b" + textRun.ContentRtl + "\u202c") : textRun.Content);
			IReadOnlyList<IWord> children = textRun.Children;
			FontCharacteristics font = textRun.Font;
			return new Cell(bounds, scriptsInclusiveBounds, text, children, new ITextRun[] { textRun }, textRun.LogicalGlyphOrdering, font, false);
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x001030F0 File Offset: 0x001012F0
		internal static IEnumerable<IReadOnlyList<TCell>> SortCellGroup<TCell>(IEnumerable<TCell> cells) where TCell : class, IWordAmalgamation<TCell>
		{
			return from g in cells.SplitRuns((TCell cell) => cell.Children[0].ApparentPixelBoundsWithoutRotation.Vertical, (Range<PixelUnit> a, Range<PixelUnit> b) => (from intersection in a.Intersect(b)
					select intersection.Size() > Math.Min(a.Size(), b.Size()) / 2).OrElse(false), (Range<PixelUnit> a, Range<PixelUnit> b) => a.Join(b))
				orderby g.Key.Center()
				select g into line
				select line.OrderBy((TCell cell) => cell.Children[0].ApparentPixelBoundsWithoutRotation.Left).ToList<TCell>();
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x001031A8 File Offset: 0x001013A8
		private static ICell BuildCellFromSortedTextRunGroup(IEnumerable<IReadOnlyList<ITextRun>> textRuns)
		{
			IEnumerable<ITextRun> enumerable = textRuns.SelectMany((IReadOnlyList<ITextRun> tr) => tr);
			Func<ITextRun, ICell> func;
			if ((func = CellBuilder.<>O.<0>__BuildCellFromTextRun) == null)
			{
				func = (CellBuilder.<>O.<0>__BuildCellFromTextRun = new Func<ITextRun, ICell>(CellBuilder.BuildCellFromTextRun));
			}
			return CellBuilder.<BuildCellFromSortedTextRunGroup>g__BuildCellFromSortedCellGroup|11_0(enumerable.Select(func));
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x00103200 File Offset: 0x00101400
		internal static ICell BuildCellFromUnsortedTextRunGroup(IEnumerable<ITextRun> textRuns)
		{
			List<IReadOnlyList<ITextRun>> list = CellBuilder.SortCellGroup<ITextRun>(textRuns).ToList<IReadOnlyList<ITextRun>>();
			if (list.SelectMany((IReadOnlyList<ITextRun> line) => line).SelectMany((ITextRun tr) => tr.Children).Any((IWord word) => word.TextDirection == TextDirection.RightToLeft))
			{
				List<string> list2 = new List<string>(list.Count);
				List<LogicalGlyphOrderingLine> list3 = new List<LogicalGlyphOrderingLine>();
				for (int i = 0; i < list.Count; i++)
				{
					IReadOnlyList<ITextRun> readOnlyList = list[i];
					int num = readOnlyList.SelectMany((ITextRun tr) => tr.Children).Count((IWord word) => word.TextDirection == TextDirection.RightToLeft);
					if (num != 0)
					{
						int num2 = readOnlyList.SelectMany((ITextRun tr) => tr.Children).Count((IWord word) => word.TextDirection == TextDirection.LeftToRight);
						TextDirection textDirection = ((num > num2) ? TextDirection.RightToLeft : TextDirection.LeftToRight);
						if (textDirection == TextDirection.RightToLeft)
						{
							readOnlyList = (list[i] = readOnlyList.Reverse<ITextRun>().ToList<ITextRun>());
							list3.Add(new LogicalGlyphOrderingLine(textDirection, readOnlyList.SelectMany((ITextRun tr) => tr.LogicalGlyphOrderingLineRtl.Elements).ToList<LogicalGlyphOrderingElement>()));
							list2.Add("\u202b" + string.Join(" ", readOnlyList.Select((ITextRun tr) => tr.ContentRtl)) + "\u202c\u200f");
						}
						else
						{
							list3.Add(new LogicalGlyphOrderingLine(textDirection, readOnlyList.SelectMany((ITextRun tr) => tr.LogicalGlyphOrderingLine.Elements).ToList<LogicalGlyphOrderingElement>()));
							list2.Add("\u202a" + string.Join(" ", readOnlyList.Select((ITextRun tr) => tr.Content)) + "\u202c\u200e");
						}
					}
				}
				LogicalGlyphOrdering logicalGlyphOrdering = new LogicalGlyphOrdering(list3);
				Bounds<PixelUnit> bounds = Bounds<PixelUnit>.Join(from tr in list.SelectMany((IReadOnlyList<ITextRun> tr) => tr)
					select tr.ApparentPixelBounds);
				Bounds<PixelUnit> bounds2 = Bounds<PixelUnit>.Join(from tr in list.SelectMany((IReadOnlyList<ITextRun> tr) => tr)
					select tr.ScriptsInclusiveBounds);
				string text = string.Join("\n", list2);
				IReadOnlyList<IWord> readOnlyList2 = logicalGlyphOrdering.AllWords.ToList<IWord>();
				FontCharacteristics font = list[0][0].Font;
				return new Cell(bounds, bounds2, text, readOnlyList2, list.SelectMany((IReadOnlyList<ITextRun> tr) => tr).ToList<ITextRun>(), logicalGlyphOrdering, font, false);
			}
			return CellBuilder.BuildCellFromSortedTextRunGroup(list);
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x0010358F File Offset: 0x0010178F
		internal static ICell BuildCellFromUnsortedCellGroup(IEnumerable<ICell> cells)
		{
			return CellBuilder.BuildCellFromUnsortedTextRunGroup(cells.SelectMany((ICell cell) => cell.TextRuns));
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x001035BC File Offset: 0x001017BC
		public static QuadTree<ICell, PixelUnit> Build(PdfAnalyzerOptions options, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds, SeparatorCollection separators, QuadTree<SeparatorGrid, PixelUnit> separatorGrids, AlignmentDotCollection alignmentDotCollection, QuadTree<ITextRun, PixelUnit> textRuns, IReadOnlyList<TextRunTable> textRunTables)
		{
			IStopwatchWrapper stopwatchWrapper = Logger.Instance.InfoTiming("Recognize Cells", "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\CellBuilder.cs", 100);
			bool forceSeparatorGrids = options.ForceSeparatorGrids;
			QuadTree<ICell, PixelUnit> quadTree = new QuadTree<ICell, PixelUnit>(pageBounds);
			if (textRuns.IsEmpty())
			{
				Logger.Instance.Debug("No text runs for cell recognition.", null, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\CellBuilder.cs", 106);
				return quadTree;
			}
			HashSet<ITextRun> hashSet = new HashSet<ITextRun>(IdentityEquality.Comparer);
			foreach (TextRunTable.TextRunCell textRunCell in textRunTables.SelectMany((TextRunTable table) => table.Rows).SelectMany((TextRunTable.TextRunRow row) => row.Cells))
			{
				IReadOnlyList<ITextRun> textRuns2 = textRunCell.TextRuns;
				if (!hashSet.Overlaps(textRuns2))
				{
					hashSet.AddRange(textRuns2);
					quadTree.Add(textRunCell.AsICell);
				}
			}
			QuadTree<ICell, PixelUnit> quadTree2 = quadTree;
			IEnumerable<ITextRun> enumerable = textRuns.Except(hashSet);
			Func<ITextRun, ICell> func;
			if ((func = CellBuilder.<>O.<0>__BuildCellFromTextRun) == null)
			{
				func = (CellBuilder.<>O.<0>__BuildCellFromTextRun = new Func<ITextRun, ICell>(CellBuilder.BuildCellFromTextRun));
			}
			quadTree2.AddRange(enumerable.Select(func));
			CellBuilder.CombineCellsBySeparators(quadTree, separators, alignmentDotCollection, options);
			CellBuilder.CombineCellsBySeparatorGrids(quadTree, separators, separatorGrids, textRuns, options, forceSeparatorGrids);
			stopwatchWrapper.Stop();
			Logger.Instance.Debug("Cells", quadTree.ToList<ICell>(), "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\CellBuilder.cs", 125);
			return quadTree;
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x00103730 File Offset: 0x00101930
		[CompilerGenerated]
		internal static ICell <BuildCellFromSortedTextRunGroup>g__BuildCellFromSortedCellGroup|11_0(IEnumerable<ICell> cells)
		{
			return cells.Aggregate(delegate(ICell a, ICell b)
			{
				if (Range<PixelUnit>.Join(a.Children.Select((IWord word) => word.ApparentPixelBoundsWithoutRotation.Vertical)).Overlaps(Range<PixelUnit>.Join(b.Children.Select((IWord word) => word.ApparentPixelBoundsWithoutRotation.Vertical))))
				{
					return a.Join(b, false, true);
				}
				if (!SpecialCharacters.EndsWithHyphen(a.Content))
				{
					return a.Join(b, true, true);
				}
				return a.Join(b, true, false);
			});
		}

		// Token: 0x02000C77 RID: 3191
		[Nullable(new byte[] { 0, 1 })]
		private class SeparatorGridMergedCell : MergedCell<ICell>
		{
			// Token: 0x17000EBC RID: 3772
			// (get) Token: 0x0600522B RID: 21035 RVA: 0x00103757 File Offset: 0x00101957
			public SeparatorGrid SeparatorGrid { get; }

			// Token: 0x17000EBD RID: 3773
			// (get) Token: 0x0600522C RID: 21036 RVA: 0x0010375F File Offset: 0x0010195F
			public SeparatorGrid.SeparatorGridCell GridCell { get; }

			// Token: 0x17000EBE RID: 3774
			// (get) Token: 0x0600522D RID: 21037 RVA: 0x00103767 File Offset: 0x00101967
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit> CellBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x17000EBF RID: 3775
			// (get) Token: 0x0600522E RID: 21038 RVA: 0x0010376F File Offset: 0x0010196F
			// (set) Token: 0x0600522F RID: 21039 RVA: 0x00103777 File Offset: 0x00101977
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit>? Bounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
				[param: Nullable(new byte[] { 0, 1 })]
				set;
			}

			// Token: 0x17000EC0 RID: 3776
			// (get) Token: 0x06005230 RID: 21040 RVA: 0x00103780 File Offset: 0x00101980
			public override ICell AsICell
			{
				get
				{
					if (!base.Cells.Any<ICell>())
					{
						return new EmptyCell(this.GridCell.PixelBounds);
					}
					ICell cell = base.AsICell;
					if (this.Bounds != null)
					{
						cell = cell.WithBounds(this.Bounds.Value, true);
					}
					return cell;
				}
			}

			// Token: 0x06005231 RID: 21041 RVA: 0x001037DC File Offset: 0x001019DC
			private SeparatorGridMergedCell(IEnumerable<ICell> cells, SeparatorGrid.SeparatorGridCell gridCell, SeparatorGrid separatorGrid, PdfAnalyzerOptions options)
				: base(CellBuilder.SortCellGroup<ICell>(cells).SelectMany((IReadOnlyList<ICell> c) => c).ToList<ICell>(), options)
			{
				this.SeparatorGrid = separatorGrid;
				this.GridCell = gridCell;
				Bounds<PixelUnit> bounds;
				if (!base.Cells.Any<ICell>())
				{
					bounds = gridCell.PixelBounds;
				}
				else
				{
					bounds = Bounds<PixelUnit>.Join(base.Cells.Select((ICell cell) => cell.ApparentPixelBounds));
				}
				this.CellBounds = bounds;
			}

			// Token: 0x06005232 RID: 21042 RVA: 0x00103878 File Offset: 0x00101A78
			public bool IsValidCell(SeparatorCollection separators, PdfAnalyzerOptions options, QuadTree<ICell, PixelUnit> allCells)
			{
				if (base.Cells.Any((ICell cell) => cell.HasAlignmentDotRow<ICell>()))
				{
					return false;
				}
				Bounds<PixelUnit>? bounds2 = this.Bounds;
				if (bounds2 != null)
				{
					Bounds<PixelUnit> bounds = bounds2.GetValueOrDefault();
					Func<IWord, bool> <>9__7;
					if (base.Cells.Any(delegate(ICell cell)
					{
						IEnumerable<IWord> children = cell.Children;
						Func<IWord, bool> func;
						if ((func = <>9__7) == null)
						{
							func = (<>9__7 = (IWord word) => !word.IsSuperscriptOrSubscript && !bounds.Overlaps(word.ApparentPixelBounds));
						}
						return children.Any(func);
					}))
					{
						return false;
					}
				}
				if (base.Cells.Count == 1)
				{
					return true;
				}
				if (this.IsLikelyMultipleColumns(separators))
				{
					return false;
				}
				if (base.Cells.Select((ICell cell) => cell.TextRuns.SelectMany((ITextRun tr) => tr.ContiguousLists.Vertical).ConvertToHashSet<ContiguousList<ITextRun>>()).UnorderedPairs(false).Any2((HashSet<ContiguousList<ITextRun>> a, HashSet<ContiguousList<ITextRun>> b) => a.Overlaps(b)))
				{
					if (options.Version >= PdfAnalyzerVersion.V1_1 && this.SeparatorGrid.CellsByTextRun != null)
					{
						int num;
						int num2;
						base.Cells.Select((ICell cell) => cell.ApparentPixelBounds.Vertical).Windowed((Range<PixelUnit> a, Range<PixelUnit> b) => a.Distance(b)).Extrema(null)
							.Deconstruct(out num, out num2);
						int num3 = num;
						int num4 = num2;
						int maxRowHeight = base.Cells.Max((ICell cell) => cell.ApparentPixelBounds.Height());
						if (num4 > num3 + 4 && num4 > maxRowHeight && (this.IsContentPossiblyMultipleRows || num4 > maxRowHeight * 3))
						{
							return false;
						}
						if (num4 < maxRowHeight)
						{
							if (this.GridCell.PixelBounds.Vertical.Subtract(base.Cells.Select((ICell cell) => cell.ApparentPixelBounds.Vertical)).All((Range<PixelUnit> verticalGap) => verticalGap.Size() < maxRowHeight))
							{
								HashSet<ITextRun> allTextRuns = base.Cells.SelectMany((ICell cell) => cell.TextRuns).ConvertToHashSet<ITextRun>();
								Func<SeparatorGrid.SeparatorGridCell, bool> <>9__20;
								Func<ITextRun, bool> <>9__19;
								Func<ContiguousList<ITextRun>, bool> <>9__17;
								if (base.Cells.All(delegate(ICell cell)
								{
									IEnumerable<ContiguousList<ITextRun>> enumerable = cell.TextRuns.SelectMany((ITextRun tr) => tr.ContiguousLists.Horizontal);
									Func<ContiguousList<ITextRun>, bool> func2;
									if ((func2 = <>9__17) == null)
									{
										func2 = (<>9__17 = delegate(ContiguousList<ITextRun> list)
										{
											HashSet<ITextRun> hashSet = allCells.OverlappingElements(list.PixelBounds.With(Axis.Horizontal, this.SeparatorGrid.PixelBounds.Horizontal)).SelectMany((ICell overlappingCell) => overlappingCell.TextRuns).ConvertToHashSet<ITextRun>();
											bool flag = list.Cells.IsSupersetOf(hashSet);
											hashSet.ExceptWith(allTextRuns);
											bool flag2 = hashSet.Any<ITextRun>();
											if (flag2 && hashSet.Except(this.SeparatorGrid.CellsByTextRun.Keys).Any<ITextRun>())
											{
												return false;
											}
											bool flag3;
											if (flag2)
											{
												IEnumerable<ITextRun> enumerable2 = hashSet;
												Func<ITextRun, bool> func3;
												if ((func3 = <>9__19) == null)
												{
													func3 = (<>9__19 = delegate(ITextRun tr)
													{
														IEnumerable<SeparatorGrid.SeparatorGridCell> enumerable3 = this.SeparatorGrid.CellsByTextRun[tr];
														Func<SeparatorGrid.SeparatorGridCell, bool> func4;
														if ((func4 = <>9__20) == null)
														{
															func4 = (<>9__20 = (SeparatorGrid.SeparatorGridCell otherGridCell) => this.GridCell.Span.Vertical != otherGridCell.Span.Vertical && this.GridCell.Span.Vertical.Contains(otherGridCell.Span.Vertical));
														}
														return enumerable3.Any(func4);
													});
												}
												flag3 = enumerable2.Any(func3);
											}
											else
											{
												flag3 = false;
											}
											bool flag4 = flag3;
											return flag && flag2 && flag4;
										});
									}
									return enumerable.Any(func2);
								}))
								{
									return false;
								}
							}
						}
					}
					if (this.IsContentLikelyMultipleRows)
					{
						if (base.Cells.All((ICell cell) => cell.TextRuns.Any((ITextRun tr) => tr.ContiguousLists.Horizontal.Any<ContiguousList<ITextRun>>())))
						{
							if (base.Cells.Select((ICell cell) => cell.TextRuns.Max((ITextRun tr) => tr.ContiguousLists.Horizontal.MaybeMax((ContiguousList<ITextRun> list) => list.Count).OrElse(0))).ExtremaWithinOrElse(2, () => false))
							{
								return false;
							}
						}
					}
				}
				return true;
			}

			// Token: 0x06005233 RID: 21043 RVA: 0x00103BA8 File Offset: 0x00101DA8
			private bool IsLikelyMultipleColumns(SeparatorCollection separators)
			{
				if (base.Cells.All((ICell cell) => cell.TextRuns.All((ITextRun tr) => tr.IsRotated)))
				{
					return false;
				}
				IReadOnlyList<CellBuilder.SeparatorGridMergedCell.CellColumnInfo> readOnlyList = (from cell in base.Cells.Where(new Func<ICell, bool>(CellBuilder.SeparatorGridMergedCell.<IsLikelyMultipleColumns>g__IsNotBulletPointOrHyphen|17_1))
					select new CellBuilder.SeparatorGridMergedCell.CellColumnInfo(cell)).ToList<CellBuilder.SeparatorGridMergedCell.CellColumnInfo>();
				if (readOnlyList.Count <= 1)
				{
					return false;
				}
				IEnumerable<Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo>> enumerable = readOnlyList.UnorderedPairs(false);
				Func<Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo>, bool> func;
				if ((func = CellBuilder.SeparatorGridMergedCell.<>O.<0>__HasConflictingColumns) == null)
				{
					func = (CellBuilder.SeparatorGridMergedCell.<>O.<0>__HasConflictingColumns = new Func<Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo>, bool>(CellBuilder.SeparatorGridMergedCell.CellColumnInfo.HasConflictingColumns));
				}
				if (enumerable.Any(func))
				{
					return false;
				}
				double num = (double)base.Cells.SelectMany((ICell cell) => cell.Children).Max((IWord word) => word.Children.Max((Glyph glyph) => glyph.ApparentPixelBoundsWithoutRotation.Width()));
				Func<Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo>, bool> hasDistantTextRuns = CellBuilder.SeparatorGridMergedCell.CellColumnInfo.HasDistantTextRunsFunc(separators, num);
				Func<Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo>, bool> hasVeryDistantTextRuns = CellBuilder.SeparatorGridMergedCell.CellColumnInfo.HasDistantTextRunsFunc(separators, 4.0 * num);
				IEnumerable<Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo>> enumerable2 = readOnlyList.UnorderedPairs(false);
				Func<Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo>, bool> func2;
				if ((func2 = CellBuilder.SeparatorGridMergedCell.<>O.<1>__MightBeSeparateColumns) == null)
				{
					func2 = (CellBuilder.SeparatorGridMergedCell.<>O.<1>__MightBeSeparateColumns = new Func<Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo>, bool>(CellBuilder.SeparatorGridMergedCell.CellColumnInfo.MightBeSeparateColumns));
				}
				return enumerable2.Where(func2).Any((Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo> r) => (r.Item1.IsAlignedWithAnyOtherCells && r.Item2.IsAlignedWithAnyOtherCells && hasDistantTextRuns(r)) || hasVeryDistantTextRuns(r));
			}

			// Token: 0x06005234 RID: 21044 RVA: 0x00103D0C File Offset: 0x00101F0C
			private static bool AreNotSeparateColumns([Nullable(new byte[] { 0, 1, 1 })] Record<CellBuilder.SeparatorGridMergedCell, CellBuilder.SeparatorGridMergedCell> pair)
			{
				CellBuilder.SeparatorGridMergedCell separatorGridMergedCell;
				CellBuilder.SeparatorGridMergedCell separatorGridMergedCell2;
				pair.Deconstruct(out separatorGridMergedCell, out separatorGridMergedCell2);
				CellBuilder.SeparatorGridMergedCell separatorGridMergedCell3 = separatorGridMergedCell;
				CellBuilder.SeparatorGridMergedCell separatorGridMergedCell4 = separatorGridMergedCell2;
				return separatorGridMergedCell3.CellBounds.Horizontal.Overlaps(separatorGridMergedCell4.CellBounds.Horizontal) || separatorGridMergedCell3.CellBounds.Overlaps(separatorGridMergedCell4.CellBounds);
			}

			// Token: 0x06005235 RID: 21045 RVA: 0x00103D68 File Offset: 0x00101F68
			internal static IReadOnlyList<CellBuilder.SeparatorGridMergedCell> Build(SeparatorGrid.SeparatorGridCell gridCell, QuadTree<ICell, PixelUnit> cells, SeparatorGrid grid, HashSet<ICell> cellsSeen, PdfAnalyzerOptions options)
			{
				IReadOnlyList<ICell> readOnlyList = cells.OverlappingElements(gridCell.PixelBounds).Except(cellsSeen).ToList<ICell>();
				if (readOnlyList.Count == 0)
				{
					return new CellBuilder.SeparatorGridMergedCell[]
					{
						new CellBuilder.SeparatorGridMergedCell(Enumerable.Empty<ICell>(), gridCell, grid, options)
					};
				}
				IEnumerable<FontCharacteristics> commonFonts = readOnlyList.GetCommonFonts(true);
				IReadOnlyList<CellBuilder.SeparatorGridMergedCell> readOnlyList3;
				if (commonFonts == null || !commonFonts.Any<FontCharacteristics>())
				{
					IReadOnlyList<CellBuilder.SeparatorGridMergedCell> readOnlyList2 = (from cell in readOnlyList
						group cell by cell.Font into g
						select new CellBuilder.SeparatorGridMergedCell(g, gridCell, grid, options)).ToList<CellBuilder.SeparatorGridMergedCell>();
					readOnlyList3 = readOnlyList2;
				}
				else
				{
					IReadOnlyList<CellBuilder.SeparatorGridMergedCell> readOnlyList2 = new CellBuilder.SeparatorGridMergedCell[]
					{
						new CellBuilder.SeparatorGridMergedCell(readOnlyList, gridCell, grid, options)
					};
					readOnlyList3 = readOnlyList2;
				}
				IReadOnlyList<CellBuilder.SeparatorGridMergedCell> readOnlyList4 = readOnlyList3;
				IEnumerable<Record<CellBuilder.SeparatorGridMergedCell, CellBuilder.SeparatorGridMergedCell>> enumerable = readOnlyList4.UnorderedPairs(false);
				Func<Record<CellBuilder.SeparatorGridMergedCell, CellBuilder.SeparatorGridMergedCell>, bool> func;
				if ((func = CellBuilder.SeparatorGridMergedCell.<>O.<2>__AreNotSeparateColumns) == null)
				{
					func = (CellBuilder.SeparatorGridMergedCell.<>O.<2>__AreNotSeparateColumns = new Func<Record<CellBuilder.SeparatorGridMergedCell, CellBuilder.SeparatorGridMergedCell>, bool>(CellBuilder.SeparatorGridMergedCell.AreNotSeparateColumns));
				}
				if (enumerable.Any(func))
				{
					readOnlyList4 = new CellBuilder.SeparatorGridMergedCell[]
					{
						new CellBuilder.SeparatorGridMergedCell(readOnlyList, gridCell, grid, options)
					};
				}
				if (readOnlyList4.Count == 1)
				{
					readOnlyList4[0].Bounds = new Bounds<PixelUnit>?(gridCell.PixelBounds);
				}
				else
				{
					using (IEnumerator<CellBuilder.SeparatorGridMergedCell> enumerator = readOnlyList4.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CellBuilder.SeparatorGridMergedCell cell = enumerator.Current;
							Bounds<PixelUnit> bounds = cell.CellBounds;
							bool flag = false;
							foreach (Direction direction in DirectionUtilities.Directions)
							{
								Bounds<PixelUnit> newBounds = bounds.With(direction, gridCell.PixelBounds[direction]);
								if (!readOnlyList4.Any((CellBuilder.SeparatorGridMergedCell other) => other != cell && other.CellBounds.Overlaps(newBounds)))
								{
									bounds = newBounds;
									flag = true;
								}
							}
							if (flag)
							{
								cell.Bounds = new Bounds<PixelUnit>?(bounds);
							}
						}
					}
				}
				return readOnlyList4;
			}

			// Token: 0x06005236 RID: 21046 RVA: 0x00103FD0 File Offset: 0x001021D0
			[CompilerGenerated]
			internal static bool <IsLikelyMultipleColumns>g__IsNotBulletPointOrHyphen|17_1(ICell cell)
			{
				return !SpecialCharacters.IsBulletPointOrHyphen(cell.Content);
			}

			// Token: 0x02000C78 RID: 3192
			[Nullable(0)]
			private class CellColumnInfo
			{
				// Token: 0x17000EC1 RID: 3777
				// (get) Token: 0x06005237 RID: 21047 RVA: 0x00103FE0 File Offset: 0x001021E0
				private ICell Cell { get; }

				// Token: 0x17000EC2 RID: 3778
				// (get) Token: 0x06005238 RID: 21048 RVA: 0x00103FE8 File Offset: 0x001021E8
				private HashSet<ContiguousList<ITextRun>> ContiguousLists { get; }

				// Token: 0x17000EC3 RID: 3779
				// (get) Token: 0x06005239 RID: 21049 RVA: 0x00103FF0 File Offset: 0x001021F0
				[Nullable(new byte[] { 0, 1 })]
				private Bounds<PixelUnit> ListBounds
				{
					[return: Nullable(new byte[] { 0, 1 })]
					get;
				}

				// Token: 0x17000EC4 RID: 3780
				// (get) Token: 0x0600523A RID: 21050 RVA: 0x00103FF8 File Offset: 0x001021F8
				private Ranges<PixelUnit> Ranges { get; }

				// Token: 0x0600523B RID: 21051 RVA: 0x00104000 File Offset: 0x00102200
				public CellColumnInfo(ICell cell)
				{
					this.Cell = cell;
					this.ContiguousLists = cell.TextRuns.Select((ITextRun tr) => tr.ContiguousLists.Vertical).Intersect<ContiguousList<ITextRun>>().ConvertToHashSet<ContiguousList<ITextRun>>();
					this.ListBounds = Bounds<PixelUnit>.MaybeJoin(this.ContiguousLists.Select((ContiguousList<ITextRun> list) => list.PixelBounds)).OrElse(Bounds<PixelUnit>.Zero);
					this.Ranges = new Ranges<PixelUnit>(this.ContiguousLists.Select((ContiguousList<ITextRun> cl) => cl.PixelBounds.Horizontal).AppendItem(cell.PixelBounds.Horizontal));
				}

				// Token: 0x17000EC5 RID: 3781
				// (get) Token: 0x0600523C RID: 21052 RVA: 0x001040DB File Offset: 0x001022DB
				public bool IsAlignedWithAnyOtherCells
				{
					get
					{
						return this.ContiguousLists.Any<ContiguousList<ITextRun>>();
					}
				}

				// Token: 0x0600523D RID: 21053 RVA: 0x001040E8 File Offset: 0x001022E8
				public static bool HasConflictingColumns([Nullable(new byte[] { 0, 1, 1 })] Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo> pair)
				{
					CellBuilder.SeparatorGridMergedCell.CellColumnInfo cellColumnInfo;
					CellBuilder.SeparatorGridMergedCell.CellColumnInfo cellColumnInfo2;
					pair.Deconstruct(out cellColumnInfo, out cellColumnInfo2);
					CellBuilder.SeparatorGridMergedCell.CellColumnInfo cellColumnInfo3 = cellColumnInfo;
					CellBuilder.SeparatorGridMergedCell.CellColumnInfo cellColumnInfo4 = cellColumnInfo2;
					return !cellColumnInfo3.ContiguousLists.Overlaps(cellColumnInfo4.ContiguousLists) && cellColumnInfo3.Ranges.Overlaps(cellColumnInfo4.Ranges);
				}

				// Token: 0x0600523E RID: 21054 RVA: 0x0010412C File Offset: 0x0010232C
				public static bool MightBeSeparateColumns([Nullable(new byte[] { 0, 1, 1 })] Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo> pair)
				{
					CellBuilder.SeparatorGridMergedCell.CellColumnInfo cellColumnInfo;
					CellBuilder.SeparatorGridMergedCell.CellColumnInfo cellColumnInfo2;
					pair.Deconstruct(out cellColumnInfo, out cellColumnInfo2);
					CellBuilder.SeparatorGridMergedCell.CellColumnInfo cellColumnInfo3 = cellColumnInfo;
					CellBuilder.SeparatorGridMergedCell.CellColumnInfo cellColumnInfo4 = cellColumnInfo2;
					return !cellColumnInfo3.ContiguousLists.Overlaps(cellColumnInfo4.ContiguousLists);
				}

				// Token: 0x0600523F RID: 21055 RVA: 0x00104159 File Offset: 0x00102359
				[return: Nullable(new byte[] { 1, 0, 1, 1 })]
				public static Func<Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo>, bool> HasDistantTextRunsFunc(SeparatorCollection separators, double columnSeparationLimit)
				{
					CellBuilder.SeparatorGridMergedCell.CellColumnInfo.<>c__DisplayClass17_0 CS$<>8__locals1 = new CellBuilder.SeparatorGridMergedCell.CellColumnInfo.<>c__DisplayClass17_0();
					CS$<>8__locals1.columnSeparationLimit = columnSeparationLimit;
					CS$<>8__locals1.separators = separators;
					return new Func<Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo>, bool>(CS$<>8__locals1.<HasDistantTextRunsFunc>g__HasDistantTextRuns|0);
				}
			}

			// Token: 0x02000C7B RID: 3195
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x040024B6 RID: 9398
				[Nullable(0)]
				public static Func<Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo>, bool> <0>__HasConflictingColumns;

				// Token: 0x040024B7 RID: 9399
				[Nullable(0)]
				public static Func<Record<CellBuilder.SeparatorGridMergedCell.CellColumnInfo, CellBuilder.SeparatorGridMergedCell.CellColumnInfo>, bool> <1>__MightBeSeparateColumns;

				// Token: 0x040024B8 RID: 9400
				[Nullable(0)]
				public static Func<Record<CellBuilder.SeparatorGridMergedCell, CellBuilder.SeparatorGridMergedCell>, bool> <2>__AreNotSeparateColumns;
			}
		}

		// Token: 0x02000C84 RID: 3204
		[Nullable(0)]
		private class SeparatorGridMergedCellCollection
		{
			// Token: 0x17000EC6 RID: 3782
			// (get) Token: 0x06005275 RID: 21109 RVA: 0x001047C9 File Offset: 0x001029C9
			public IReadOnlyList<CellBuilder.SeparatorGridMergedCell> GridCells { get; }

			// Token: 0x17000EC7 RID: 3783
			// (get) Token: 0x06005276 RID: 21110 RVA: 0x001047D1 File Offset: 0x001029D1
			[Nullable(new byte[] { 1, 0, 1 })]
			public AxisAlignedSet<Range<TableUnit>> IgnoredRanges
			{
				[return: Nullable(new byte[] { 1, 0, 1 })]
				get;
			}

			// Token: 0x17000EC8 RID: 3784
			// (get) Token: 0x06005277 RID: 21111 RVA: 0x001047D9 File Offset: 0x001029D9
			[Nullable(new byte[] { 1, 0, 1, 1, 1 })]
			public IReadOnlyDictionary<Range<TableUnit>, Ranges<PixelUnit>> MultiCellColumns
			{
				[return: Nullable(new byte[] { 1, 0, 1, 1, 1 })]
				get;
			}

			// Token: 0x06005278 RID: 21112 RVA: 0x001047E1 File Offset: 0x001029E1
			private SeparatorGridMergedCellCollection(IReadOnlyList<CellBuilder.SeparatorGridMergedCell> gridCells, [Nullable(new byte[] { 1, 0, 1 })] AxisAlignedSet<Range<TableUnit>> ignoredRanges, [Nullable(new byte[] { 1, 0, 1, 1, 1 })] IReadOnlyDictionary<Range<TableUnit>, Ranges<PixelUnit>> multiCellColumns)
			{
				this.GridCells = gridCells;
				this.IgnoredRanges = ignoredRanges;
				this.MultiCellColumns = multiCellColumns;
			}

			// Token: 0x06005279 RID: 21113 RVA: 0x00104800 File Offset: 0x00102A00
			internal static CellBuilder.SeparatorGridMergedCellCollection Create(SeparatorGrid grid, SeparatorCollection separators, QuadTree<ICell, PixelUnit> cells, PdfAnalyzerOptions options)
			{
				AxisAlignedSet<Range<TableUnit>> axisAlignedSet = new AxisAlignedSet<Range<TableUnit>>();
				Dictionary<Range<TableUnit>, Ranges<PixelUnit>> dictionary = new Dictionary<Range<TableUnit>, Ranges<PixelUnit>>();
				List<CellBuilder.SeparatorGridMergedCell> list = new List<CellBuilder.SeparatorGridMergedCell>();
				HashSet<SeparatorGrid.SeparatorGridCell> hashSet = new HashSet<SeparatorGrid.SeparatorGridCell>();
				HashSet<ICell> hashSet2 = new HashSet<ICell>();
				bool flag = false;
				foreach (IEnumerable<SeparatorGrid.SeparatorGridCell> enumerable in grid.Cells.Rows())
				{
					List<SeparatorGrid.SeparatorGridCell> list2 = enumerable.Collect<SeparatorGrid.SeparatorGridCell>().Distinct<SeparatorGrid.SeparatorGridCell>().ToList<SeparatorGrid.SeparatorGridCell>();
					flag |= list2.Count > 1;
					if (flag)
					{
						foreach (SeparatorGrid.SeparatorGridCell separatorGridCell in list2)
						{
							if (hashSet.Add(separatorGridCell))
							{
								IReadOnlyList<CellBuilder.SeparatorGridMergedCell> readOnlyList = CellBuilder.SeparatorGridMergedCell.Build(separatorGridCell, cells, grid, hashSet2, options);
								if (readOnlyList.Count > 1)
								{
									dictionary[separatorGridCell.Span.Horizontal] = dictionary.MaybeGet(separatorGridCell.Span.Horizontal).OrElse(Ranges<PixelUnit>.Empty).Join(readOnlyList.Select((CellBuilder.SeparatorGridMergedCell cell) => cell.CellBounds.Horizontal));
								}
								foreach (CellBuilder.SeparatorGridMergedCell separatorGridMergedCell in readOnlyList)
								{
									if (separatorGridMergedCell.IsValidCell(separators, options, cells))
									{
										list.Add(separatorGridMergedCell);
										hashSet2.AddRange(separatorGridMergedCell.Cells);
									}
									else
									{
										foreach (Axis axis in AxisUtilities.Axes)
										{
											axisAlignedSet[axis].Add(separatorGridCell.Span[axis]);
										}
									}
								}
							}
						}
					}
				}
				return new CellBuilder.SeparatorGridMergedCellCollection(list, axisAlignedSet, CellBuilder.SeparatorGridMergedCellCollection.NormalizeSubColumnWidths(dictionary, list));
			}

			// Token: 0x0600527A RID: 21114 RVA: 0x00104A60 File Offset: 0x00102C60
			[return: Nullable(new byte[] { 1, 0, 1, 1, 1 })]
			private static IReadOnlyDictionary<Range<TableUnit>, Ranges<PixelUnit>> NormalizeSubColumnWidths([Nullable(new byte[] { 1, 0, 1, 1, 1 })] IReadOnlyDictionary<Range<TableUnit>, Ranges<PixelUnit>> splitCellRanges, IReadOnlyList<CellBuilder.SeparatorGridMergedCell> gridCells)
			{
				return splitCellRanges.ToDictionary((KeyValuePair<Range<TableUnit>, Ranges<PixelUnit>> kv) => kv.Key, delegate(KeyValuePair<Range<TableUnit>, Ranges<PixelUnit>> kv)
				{
					Range<TableUnit> range3;
					Ranges<PixelUnit> ranges;
					kv.Deconstruct(out range3, out ranges);
					Range<TableUnit> columnSpan = range3;
					Ranges<PixelUnit> ranges2 = ranges;
					Range<PixelUnit>? joinOfRanges = ranges2.JoinOfRanges;
					if (joinOfRanges == null)
					{
						return ranges2;
					}
					Range<PixelUnit> range2 = joinOfRanges.Value;
					IEnumerable<CellBuilder.SeparatorGridMergedCell> gridCells2 = gridCells;
					Func<CellBuilder.SeparatorGridMergedCell, bool> <>9__2;
					Func<CellBuilder.SeparatorGridMergedCell, bool> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (CellBuilder.SeparatorGridMergedCell cell) => cell.GridCell.Span.Horizontal.Equals(columnSpan));
					}
					using (IEnumerator<CellBuilder.SeparatorGridMergedCell> enumerator = gridCells2.Where(func).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CellBuilder.SeparatorGridMergedCell cellInColumn = enumerator.Current;
							if (ranges2.Where((Range<PixelUnit> range) => cellInColumn.CellBounds.Horizontal.Overlaps(range)).MaybeOnly<Range<PixelUnit>>().HasValue)
							{
								ranges2 = ranges2.Join(cellInColumn.CellBounds.Horizontal);
								if (cellInColumn.Bounds != null)
								{
									range2 = range2.Join(cellInColumn.Bounds.Value.Horizontal);
								}
							}
						}
					}
					range2 = range2.Join(joinOfRanges.Value);
					ranges2 = ranges2.Join(new Range<PixelUnit>[]
					{
						new Range<PixelUnit>(range2.Min, ranges2.First<Range<PixelUnit>>().Max),
						new Range<PixelUnit>(ranges2.Last<Range<PixelUnit>>().Min, range2.Max)
					});
					return ranges2;
				});
			}

			// Token: 0x17000EC9 RID: 3785
			// (get) Token: 0x0600527B RID: 21115 RVA: 0x00104AAB File Offset: 0x00102CAB
			private IEnumerable<CellBuilder.SeparatorGridMergedCell> CellsToMerge
			{
				get
				{
					return this.GridCells.Where((CellBuilder.SeparatorGridMergedCell mergedCell) => !this.IgnoredRanges.Any((Axis axis, HashSet<Range<TableUnit>> ranges) => ranges.Contains(mergedCell.GridCell.Span[axis])));
				}
			}

			// Token: 0x0600527C RID: 21116 RVA: 0x00104AC4 File Offset: 0x00102CC4
			private CellBuilder.SeparatorGridMergedCell FixSubColumnBounds(CellBuilder.SeparatorGridMergedCell mergedCell)
			{
				Ranges<PixelUnit> ranges;
				if (this.MultiCellColumns.TryGetValue(mergedCell.GridCell.Span.Horizontal, out ranges))
				{
					Optional<Range<PixelUnit>> optional = ranges.Where((Range<PixelUnit> range) => mergedCell.CellBounds.Horizontal.Overlaps(range)).MaybeOnly<Range<PixelUnit>>();
					if (optional.HasValue)
					{
						mergedCell.Bounds = new Bounds<PixelUnit>?((mergedCell.Bounds ?? mergedCell.CellBounds).With(Axis.Horizontal, optional.Value));
					}
				}
				return mergedCell;
			}

			// Token: 0x0600527D RID: 21117 RVA: 0x00104B78 File Offset: 0x00102D78
			internal void MergeCells(QuadTree<ICell, PixelUnit> cells)
			{
				foreach (CellBuilder.SeparatorGridMergedCell separatorGridMergedCell in this.CellsToMerge)
				{
					cells.MergeCell(this.FixSubColumnBounds(separatorGridMergedCell));
				}
			}
		}

		// Token: 0x02000C8B RID: 3211
		[NullableContext(0)]
		[Nullable(new byte[] { 0, 1 })]
		private class SeparatorsMergedCell : MergedCell<ICell>, IApparentPixelBounded, IPixelBounded, IBounded<PixelUnit>
		{
			// Token: 0x0600528D RID: 21133 RVA: 0x00104E7C File Offset: 0x0010307C
			[NullableContext(1)]
			private SeparatorsMergedCell(IReadOnlyList<ICell> cells, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> horizontal, PdfAnalyzerOptions options)
				: base(cells, options)
			{
				this._horizontal = horizontal;
				this.ApparentPixelBounds = base.Cells.Select((ICell cell) => cell.ApparentPixelBounds).Aggregate((Bounds<PixelUnit> x, Bounds<PixelUnit> y) => x.Join(y));
			}

			// Token: 0x17000ECA RID: 3786
			// (get) Token: 0x0600528E RID: 21134 RVA: 0x00104EEC File Offset: 0x001030EC
			[Nullable(2)]
			public FontCharacteristics Font
			{
				[NullableContext(2)]
				get
				{
					return base.Cells[0].Font;
				}
			}

			// Token: 0x17000ECB RID: 3787
			// (get) Token: 0x0600528F RID: 21135 RVA: 0x00104EFF File Offset: 0x001030FF
			public int MinLineSpacing
			{
				get
				{
					return base.Cells.Windowed((ICell a, ICell b) => a.ScriptsInclusiveBounds.Vertical.Distance(b.ScriptsInclusiveBounds.Vertical)).Min();
				}
			}

			// Token: 0x17000ECC RID: 3788
			// (get) Token: 0x06005290 RID: 21136 RVA: 0x00104F30 File Offset: 0x00103130
			public int MaxLineSpacing
			{
				get
				{
					return base.Cells.Windowed((ICell a, ICell b) => a.ApparentPixelBounds.Vertical.Distance(b.ApparentPixelBounds.Vertical)).Max();
				}
			}

			// Token: 0x17000ECD RID: 3789
			// (get) Token: 0x06005291 RID: 21137 RVA: 0x00104F61 File Offset: 0x00103161
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit> ApparentPixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x17000ECE RID: 3790
			// (get) Token: 0x06005292 RID: 21138 RVA: 0x00104F6C File Offset: 0x0010316C
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit> StablePixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return base.Cells.Select((ICell cell) => cell.StablePixelBounds).Aggregate((Bounds<PixelUnit> x, Bounds<PixelUnit> y) => x.Join(y));
				}
			}

			// Token: 0x17000ECF RID: 3791
			// (get) Token: 0x06005293 RID: 21139 RVA: 0x00104FC7 File Offset: 0x001031C7
			[Nullable(new byte[] { 0, 1 })]
			Bounds<PixelUnit> IPixelBounded.PixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.ApparentPixelBounds;
				}
			}

			// Token: 0x17000ED0 RID: 3792
			// (get) Token: 0x06005294 RID: 21140 RVA: 0x00104FC7 File Offset: 0x001031C7
			[Nullable(new byte[] { 0, 1 })]
			Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.ApparentPixelBounds;
				}
			}

			// Token: 0x17000ED1 RID: 3793
			// (get) Token: 0x06005295 RID: 21141 RVA: 0x00104FD0 File Offset: 0x001031D0
			private bool IsHorizontallyAligned
			{
				get
				{
					Dictionary<Justification, Predicate<Range<PixelUnit>>> dictionary = new Dictionary<Justification, Predicate<Range<PixelUnit>>>();
					dictionary[Justification.Before] = ([Nullable(new byte[] { 0, 1 })] Range<PixelUnit> cellHorizontal) => MathUtils.WithinTolerance(cellHorizontal.Min, this._horizontal.Min, 2);
					dictionary[Justification.After] = ([Nullable(new byte[] { 0, 1 })] Range<PixelUnit> cellHorizontal) => MathUtils.WithinTolerance(cellHorizontal.Max, this._horizontal.Max, 2);
					dictionary[Justification.Center] = ([Nullable(new byte[] { 0, 1 })] Range<PixelUnit> cellHorizontal) => MathUtils.WithinTolerance(cellHorizontal.Center(), this._horizontal.Center(), 4.0);
					Dictionary<Justification, Predicate<Range<PixelUnit>>> dictionary2 = dictionary;
					foreach (ICell cell in base.Cells)
					{
						Range<PixelUnit> horizontal = cell.ApparentPixelBounds.Horizontal;
						if (!(horizontal == this._horizontal))
						{
							foreach (KeyValuePair<Justification, Predicate<Range<PixelUnit>>> keyValuePair in dictionary2.ToList<KeyValuePair<Justification, Predicate<Range<PixelUnit>>>>())
							{
								Justification justification;
								Predicate<Range<PixelUnit>> predicate;
								keyValuePair.Deconstruct(out justification, out predicate);
								Justification justification2 = justification;
								if (!predicate(horizontal))
								{
									dictionary2.Remove(justification2);
								}
							}
							if (!dictionary2.Any<KeyValuePair<Justification, Predicate<Range<PixelUnit>>>>())
							{
								return false;
							}
						}
					}
					return true;
				}
			}

			// Token: 0x17000ED2 RID: 3794
			// (get) Token: 0x06005296 RID: 21142 RVA: 0x001050E0 File Offset: 0x001032E0
			private bool IsLineSpacingValid
			{
				get
				{
					CellBuilder.SeparatorsMergedCell.<>c__DisplayClass20_0 CS$<>8__locals1;
					CS$<>8__locals1.<>4__this = this;
					CS$<>8__locals1.minCellHeight = base.Cells.Min((ICell c) => c.ApparentPixelBounds.Height());
					if (!this.<get_IsLineSpacingValid>g__IsLineSpacingValidForBounds|20_2((ICell c) => c.ApparentPixelBounds, ref CS$<>8__locals1))
					{
						return this.<get_IsLineSpacingValid>g__IsLineSpacingValidForBounds|20_2((ICell c) => c.ScriptsInclusiveBounds, ref CS$<>8__locals1);
					}
					return true;
				}
			}

			// Token: 0x17000ED3 RID: 3795
			// (get) Token: 0x06005297 RID: 21143 RVA: 0x00105178 File Offset: 0x00103378
			internal override bool IsContentLikelyMultipleRows
			{
				get
				{
					if (base.Cells.Count <= 1)
					{
						return false;
					}
					return !base.Cells.Any(delegate(ICell c)
					{
						IEnumerable<char> content = c.Content;
						Func<char, bool> func;
						if ((func = CellBuilder.SeparatorsMergedCell.<>O.<0>__IsLetter) == null)
						{
							func = (CellBuilder.SeparatorsMergedCell.<>O.<0>__IsLetter = new Func<char, bool>(char.IsLetter));
						}
						return content.Any(func);
					});
				}
			}

			// Token: 0x17000ED4 RID: 3796
			// (get) Token: 0x06005298 RID: 21144 RVA: 0x001051B8 File Offset: 0x001033B8
			internal bool IsValidCell
			{
				get
				{
					if (base.Cells.Any(delegate(ICell c)
					{
						Cell cell2 = c as Cell;
						return cell2 != null && cell2.BuiltFromMultipleTextRuns;
					}))
					{
						return false;
					}
					if (base.Cells.Count <= 1)
					{
						return true;
					}
					if (!base.Cells.Select((ICell cell) => cell.ApparentPixelBounds.Vertical).AreDisjoint<PixelUnit>())
					{
						return false;
					}
					if (!this.IsHorizontallyAligned)
					{
						return false;
					}
					IEnumerable<FontCharacteristics> commonFonts = base.Cells.GetCommonFonts(false);
					return commonFonts != null && commonFonts.Any<FontCharacteristics>() && !this.IsContentLikelyMultipleRows && this.IsLineSpacingValid;
				}
			}

			// Token: 0x06005299 RID: 21145 RVA: 0x00105271 File Offset: 0x00103471
			[NullableContext(1)]
			[return: Nullable(new byte[] { 0, 1 })]
			internal static Optional<CellBuilder.SeparatorsMergedCell> Build(IEnumerable<ICell> cells, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> horizontal, PdfAnalyzerOptions options)
			{
				return new CellBuilder.SeparatorsMergedCell(cells.OrderBy((ICell cell) => cell.ApparentPixelBounds.Top).ToList<ICell>(), horizontal, options).Some<CellBuilder.SeparatorsMergedCell>();
			}

			// Token: 0x0600529D RID: 21149 RVA: 0x00105310 File Offset: 0x00103510
			[NullableContext(1)]
			[CompilerGenerated]
			private bool <get_IsLineSpacingValid>g__IsLineSpacingValid|20_1(IEnumerable<int> lineSpacings, ref CellBuilder.SeparatorsMergedCell.<>c__DisplayClass20_0 A_2)
			{
				int num;
				int num2;
				lineSpacings.Extrema(null).Deconstruct(out num, out num2);
				int num3 = num;
				int num4 = num2;
				return num4 - num3 <= 2 && num4 <= A_2.minCellHeight;
			}

			// Token: 0x0600529E RID: 21150 RVA: 0x00105345 File Offset: 0x00103545
			[CompilerGenerated]
			private bool <get_IsLineSpacingValid>g__IsLineSpacingValidForBounds|20_2([Nullable(new byte[] { 1, 1, 0, 1 })] Func<ICell, Bounds<PixelUnit>> boundsFunc, ref CellBuilder.SeparatorsMergedCell.<>c__DisplayClass20_0 A_2)
			{
				return this.<get_IsLineSpacingValid>g__IsLineSpacingValid|20_1(base.Cells.Select(boundsFunc).Windowed((Bounds<PixelUnit> a, Bounds<PixelUnit> b) => a.Vertical.Distance(b.Vertical)), ref A_2);
			}

			// Token: 0x040024F3 RID: 9459
			[Nullable(new byte[] { 0, 1 })]
			private readonly Range<PixelUnit> _horizontal;

			// Token: 0x02000C8C RID: 3212
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x040024F5 RID: 9461
				public static Func<char, bool> <0>__IsLetter;
			}
		}

		// Token: 0x02000C8F RID: 3215
		[Nullable(new byte[] { 0, 1, 1 })]
		private class SeparatorsMergedCellRow : MergedCellRow<ICell, CellBuilder.SeparatorsMergedCell>
		{
			// Token: 0x060052AF RID: 21167 RVA: 0x001054C3 File Offset: 0x001036C3
			private SeparatorsMergedCellRow(IReadOnlyList<CellBuilder.SeparatorsMergedCell> mergedCells)
				: base(mergedCells)
			{
			}

			// Token: 0x060052B0 RID: 21168 RVA: 0x001054CC File Offset: 0x001036CC
			[return: Nullable(new byte[] { 0, 1 })]
			internal static Optional<CellBuilder.SeparatorsMergedCellRow> Build(IEnumerable<ICell> baseCells, PdfAnalyzerOptions options)
			{
				return from row in (from cellGroup in baseCells.OrderBy((ICell cell) => cell.ApparentPixelBounds.Left).SplitRuns((ICell cell) => cell.ApparentPixelBounds.Horizontal, (Range<PixelUnit> a, Range<PixelUnit> b) => a.Overlaps(b), (Range<PixelUnit> a, Range<PixelUnit> b) => a.Join(b))
						select from mergedCell in CellBuilder.SeparatorsMergedCell.Build(cellGroup, cellGroup.Key, options)
							where mergedCell.IsValidCell
							select mergedCell).WholeReadOnlyListOfValues<CellBuilder.SeparatorsMergedCell>()
					select new CellBuilder.SeparatorsMergedCellRow(row);
			}

			// Token: 0x060052B1 RID: 21169 RVA: 0x001055A8 File Offset: 0x001037A8
			public bool IsValidRow([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> rowBounds)
			{
				if (this.MergedCells.Count > 1)
				{
					if (!this.MergedCells.Select((CellBuilder.SeparatorsMergedCell group) => group.Cells.Count).Distinct<int>().HasAtLeast(2))
					{
						return false;
					}
				}
				else if (this.MergedCells.Single<CellBuilder.SeparatorsMergedCell>().Cells.All((ICell cell) => cell.TextRuns.Single<ITextRun>().ContiguousLists.Horizontal.Any<ContiguousList<ITextRun>>()))
				{
					return false;
				}
				Optional<QuadTree<CellBuilder.SeparatorsMergedCell, PixelUnit>> optional = this.MergedCells.ToQuadTreeWithoutOverlaps(pageBounds);
				if (!optional.HasValue)
				{
					return false;
				}
				QuadTree<CellBuilder.SeparatorsMergedCell, PixelUnit> value = optional.Value;
				if (!value.Select((CellBuilder.SeparatorsMergedCell bvp) => bvp.ApparentPixelBounds.Top).ExtremaWithin(2))
				{
					if (!value.Select((CellBuilder.SeparatorsMergedCell bvp) => bvp.ApparentPixelBounds.Bottom).ExtremaWithin(2))
					{
						if (!value.Select((CellBuilder.SeparatorsMergedCell bvp) => bvp.ApparentPixelBounds.Vertical.Center()).ExtremaWithin(4.0))
						{
							return false;
						}
					}
				}
				int num = this.MergedCells.Max((CellBuilder.SeparatorsMergedCell cs) => cs.Cells.Max((ICell c) => c.ApparentPixelBounds.Height()));
				int num2 = 2 * num;
				return value.OverlappingElements(rowBounds.Edges[Direction.Up].Bounds.Extend(Direction.Down, num2)).Any<CellBuilder.SeparatorsMergedCell>() && value.OverlappingElements(rowBounds.Edges[Direction.Down].Bounds.Extend(Direction.Up, num2)).Any<CellBuilder.SeparatorsMergedCell>();
			}
		}

		// Token: 0x02000C92 RID: 3218
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002516 RID: 9494
			[Nullable(0)]
			public static Func<ITextRun, ICell> <0>__BuildCellFromTextRun;
		}
	}
}
