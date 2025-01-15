using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D80 RID: 3456
	[NullableContext(1)]
	[Nullable(0)]
	internal class TextRunTable : IPixelBounded, IBounded<PixelUnit>
	{
		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06005822 RID: 22562 RVA: 0x00117FA0 File Offset: 0x001161A0
		public IReadOnlyList<TextRunTable.TextRunRow> Rows { get; }

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06005823 RID: 22563 RVA: 0x00117FA8 File Offset: 0x001161A8
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> PixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return Bounds<PixelUnit>.Join(this.Rows.Select((TextRunTable.TextRunRow row) => row.ApparentPixelBounds));
			}
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06005824 RID: 22564 RVA: 0x00117FD9 File Offset: 0x001161D9
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.PixelBounds;
			}
		}

		// Token: 0x06005825 RID: 22565 RVA: 0x00117FE1 File Offset: 0x001161E1
		public TextRunTable(IReadOnlyList<TextRunTable.TextRunRow> rows)
		{
			this.Rows = rows;
		}

		// Token: 0x06005826 RID: 22566 RVA: 0x00117FF0 File Offset: 0x001161F0
		public static IReadOnlyList<TextRunTable> BuildCollection(PdfAnalyzerOptions options, PageStatistics pageStatistics, SeparatorCollection separators, QuadTree<SeparatorGrid, PixelUnit> separatorGrids, AlignmentDotCollection alignmentDotCollection, QuadTree<ITextRun, PixelUnit> textRuns, AxisAlignedList<ContiguousList<ITextRun>> textRunContiguousLists, IReadOnlyList<IProsePdfTable<ITextRun>> simpleTables)
		{
			TextRunTable.<>c__DisplayClass9_0 CS$<>8__locals1 = new TextRunTable.<>c__DisplayClass9_0();
			CS$<>8__locals1.options = options;
			CS$<>8__locals1.pageStatistics = pageStatistics;
			CS$<>8__locals1.separators = separators;
			CS$<>8__locals1.textRuns = textRuns;
			CS$<>8__locals1.lineWrapDistanceOptions = TextRunTable.ComputeLineWrapDistanceOptions(textRunContiguousLists, CS$<>8__locals1.separators);
			CS$<>8__locals1.res = simpleTables.SelectMany((IProsePdfTable<ITextRun> table) => table.CalculateApparentPixelBounds()).Collect(new Func<Bounds<PixelUnit>, TextRunTable>(CS$<>8__locals1.<BuildCollection>g__BuildForBounds|0)).ToList<TextRunTable>();
			CS$<>8__locals1.res.AddRange((from grid in separatorGrids
				select grid.PixelBounds into gridBounds
				where !CS$<>8__locals1.res.Any((TextRunTable table) => gridBounds.Overlaps(table.PixelBounds))
				select gridBounds).Collect(new Func<Bounds<PixelUnit>, TextRunTable>(CS$<>8__locals1.<BuildCollection>g__BuildForBounds|0)));
			return CS$<>8__locals1.res;
		}

		// Token: 0x06005827 RID: 22567 RVA: 0x001180D0 File Offset: 0x001162D0
		[return: Nullable(new byte[] { 1, 1, 0, 1 })]
		private static IReadOnlyList<FloatKeyReadOnlyDictionary<Range<PixelUnit>>> ComputeLineWrapDistanceOptions(AxisAlignedList<ContiguousList<ITextRun>> textRunContiguousLists, SeparatorCollection separators)
		{
			FloatKeyReadOnlyDictionary<IReadOnlyList<Range<PixelUnit>>> floatKeyReadOnlyDictionary = FloatKeyReadOnlyDictionary<IReadOnlyList<int>>.Create<KeyValuePair<float, int>>(from pair in textRunContiguousLists.Vertical.SelectMany((ContiguousList<ITextRun> l) => l.Cells.OrderBy((ITextRun c) => c.ApparentPixelBounds.Top).Windowed<ITextRun>())
				let a = pair.Item1
				let b = pair.Item2
				where a.Font != null && b.Font != null && MathUtils.WithinTolerance((double)a.Font.FontSize, (double)b.Font.FontSize, PageStatistics.FontSizeEpsilon)
				where !separators.AnySeparates(Axis.Horizontal, a, b)
				let d = a.ApparentPixelBounds.Vertical.Distance(b.ApparentPixelBounds.Vertical)
				where d > 0
				where (double)d < 0.7 * (double)Math.Min(a.ApparentPixelBounds.Height(), b.ApparentPixelBounds.Height())
				select KVP.Create<float, int>(a.Font.FontSize, d), (KeyValuePair<float, int> kv) => kv.Key, ([Nullable(new byte[] { 1, 0 })] IEnumerable<KeyValuePair<float, int>> values) => (from v in values.Select((KeyValuePair<float, int> kv) => kv.Value).Distinct<int>()
				orderby v
				select v).ToList<int>()).Select<IReadOnlyList<Range<PixelUnit>>>(new Func<IReadOnlyList<int>, IReadOnlyList<Range<PixelUnit>>>(TextRunTable.<ComputeLineWrapDistanceOptions>g__ClumpDistances|10_11));
			List<FloatKeyReadOnlyDictionary<Range<PixelUnit>>> list = new List<FloatKeyReadOnlyDictionary<Range<PixelUnit>>>();
			using (IEnumerator<double> enumerator = (from ratio in floatKeyReadOnlyDictionary.SelectMany2((float fontSize, IReadOnlyList<Range<PixelUnit>> clumps) => clumps.Select((Range<PixelUnit> clump) => Math.Sqrt((double)(clump.Min * clump.Max)) / (double)fontSize)).Distinct<double>()
				orderby ratio
				select ratio).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TextRunTable.<>c__DisplayClass10_2 CS$<>8__locals2 = new TextRunTable.<>c__DisplayClass10_2();
					CS$<>8__locals2.ratio = enumerator.Current;
					FloatKeyReadOnlyDictionary<Range<PixelUnit>> dict = floatKeyReadOnlyDictionary.SelectOptionalValues<Range<PixelUnit>>((float fontSize, [Nullable(new byte[] { 1, 0, 1 })] IReadOnlyList<Range<PixelUnit>> clumps) => clumps.Where((Range<PixelUnit> clump) => MathUtils.WithinTolerance(CS$<>8__locals2.ratio, Math.Sqrt((double)(clump.Min * clump.Max)) / (double)fontSize, 0.2)).MaybeFirst<Range<PixelUnit>>());
					if (!list.Any((FloatKeyReadOnlyDictionary<Range<PixelUnit>> existing) => existing.ReadOnlyDictionaryEquals(dict, null)))
					{
						list.Add(dict);
					}
				}
			}
			return list;
		}

		// Token: 0x06005828 RID: 22568 RVA: 0x0011835C File Offset: 0x0011655C
		[return: Nullable(2)]
		private static TextRunTable Build([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> simpleTableBounds, PdfAnalyzerOptions options, PageStatistics pageStatistics, SeparatorCollection separators, QuadTree<ITextRun, PixelUnit> textRuns, [Nullable(new byte[] { 1, 1, 0, 1 })] IReadOnlyList<FloatKeyReadOnlyDictionary<Range<PixelUnit>>> lineWrapDistanceOptions)
		{
			IReadOnlyList<IGrouping<int, ITextRun>> textRunsInTable = (from t in textRuns.OverlappingElements(simpleTableBounds)
				group t by t.ApparentPixelBounds.Bottom into g
				orderby g.Key
				select g).ToList<IGrouping<int, ITextRun>>();
			if ((from textRun in textRunsInTable.SelectMany((IGrouping<int, ITextRun> g) => g)
				select textRun.Font).Distinct<FontCharacteristics>().All(new Func<FontCharacteristics, bool>(pageStatistics.IsFixedWidth)))
			{
				return null;
			}
			IReadOnlyList<int> horizontalSeparatorPositions = (from sep in separators.Separators.Horizontal.OverlappingElements(simpleTableBounds)
				select sep.Line.Position into pos
				orderby pos
				select pos).ToList<int>();
			return lineWrapDistanceOptions.Collect((FloatKeyReadOnlyDictionary<Range<PixelUnit>> lineWrapDistanceRange) => TextRunTable.Build(textRunsInTable, lineWrapDistanceRange, horizontalSeparatorPositions, options)).FirstOrDefault<TextRunTable>();
		}

		// Token: 0x06005829 RID: 22569 RVA: 0x001184B8 File Offset: 0x001166B8
		[return: Nullable(2)]
		private static TextRunTable Build(IReadOnlyList<IGrouping<int, ITextRun>> textRunsInTable, [Nullable(new byte[] { 1, 0, 1 })] FloatKeyReadOnlyDictionary<Range<PixelUnit>> lineWrapDistanceRange, IReadOnlyList<int> horizontalSeparatorPositions, PdfAnalyzerOptions options)
		{
			TextRunTable.<>c__DisplayClass12_0 CS$<>8__locals1;
			CS$<>8__locals1.options = options;
			CS$<>8__locals1.lineWrapDistanceRange = lineWrapDistanceRange;
			CS$<>8__locals1.finalizedRows = new List<TextRunTable.TextRunRow>();
			CS$<>8__locals1.possibleRows = new List<TextRunTable.TextRunRow>();
			int num = 0;
			using (IEnumerator<IGrouping<int, ITextRun>> enumerator = textRunsInTable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IGrouping<int, ITextRun> grouping = enumerator.Current;
					TextRunTable.<>c__DisplayClass12_2 CS$<>8__locals2;
					CS$<>8__locals2.textRunsByBottom = grouping;
					int key = CS$<>8__locals2.textRunsByBottom.Key;
					TextRunTable.<>c__DisplayClass12_3 CS$<>8__locals3;
					CS$<>8__locals3.endedRowDueToSeparator = false;
					while (num < horizontalSeparatorPositions.Count && horizontalSeparatorPositions[num] < key)
					{
						while (CS$<>8__locals1.possibleRows.Any<TextRunTable.TextRunRow>())
						{
							if (!TextRunTable.<Build>g__FinalizeRow|12_0(true, ref CS$<>8__locals1))
							{
								return null;
							}
						}
						CS$<>8__locals3.endedRowDueToSeparator = true;
						num++;
					}
					if (!TextRunTable.<Build>g__TryAddTextRuns|12_4(ref CS$<>8__locals1, ref CS$<>8__locals2, ref CS$<>8__locals3))
					{
						return null;
					}
				}
				goto IL_00C8;
			}
			IL_00BC:
			if (!TextRunTable.<Build>g__FinalizeRow|12_0(true, ref CS$<>8__locals1))
			{
				return null;
			}
			IL_00C8:
			if (CS$<>8__locals1.possibleRows.Any<TextRunTable.TextRunRow>())
			{
				goto IL_00BC;
			}
			if (CS$<>8__locals1.finalizedRows.Any((TextRunTable.TextRunRow row) => row.Cells.Any((TextRunTable.TextRunCell cell) => cell.TextRuns.Count > 1)) && CS$<>8__locals1.finalizedRows.Count > 1)
			{
				return new TextRunTable(CS$<>8__locals1.finalizedRows);
			}
			return null;
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x001185F4 File Offset: 0x001167F4
		[CompilerGenerated]
		[return: Nullable(new byte[] { 1, 0, 1 })]
		internal static IReadOnlyList<Range<PixelUnit>> <ComputeLineWrapDistanceOptions>g__ClumpDistances|10_11(IReadOnlyList<int> distances)
		{
			IEnumerable<Record<int, int>> enumerable = distances.AdjacentClumps(2, new int?(4));
			Func<Record<int, int>, Range<PixelUnit>> func;
			if ((func = TextRunTable.<>O.<0>__Create) == null)
			{
				func = (TextRunTable.<>O.<0>__Create = new Func<Record<int, int>, Range<PixelUnit>>(Range<PixelUnit>.Create));
			}
			return enumerable.Select(func).ToList<Range<PixelUnit>>();
		}

		// Token: 0x0600582B RID: 22571 RVA: 0x00118628 File Offset: 0x00116828
		[CompilerGenerated]
		internal static bool <Build>g__FinalizeRow|12_0(bool foundSeparator, ref TextRunTable.<>c__DisplayClass12_0 A_1)
		{
			if (A_1.possibleRows.Count == 0)
			{
				return true;
			}
			TextRunTable.TextRunRow previousRow = A_1.finalizedRows.LastOrDefault<TextRunTable.TextRunRow>();
			TextRunTable.TextRunRow newRow = ((foundSeparator && A_1.possibleRows.Last<TextRunTable.TextRunRow>().IsValidRow(previousRow, true)) ? A_1.possibleRows.Last<TextRunTable.TextRunRow>() : A_1.possibleRows.Reverse<TextRunTable.TextRunRow>().FirstOrDefault((TextRunTable.TextRunRow row) => row.IsValidRow(previousRow, false)));
			if (newRow == null)
			{
				return false;
			}
			A_1.finalizedRows.Add(newRow);
			A_1.possibleRows = A_1.possibleRows.Collect((TextRunTable.TextRunRow row) => row.FilterToBelow(newRow.Bottom)).ToList<TextRunTable.TextRunRow>();
			return true;
		}

		// Token: 0x0600582C RID: 22572 RVA: 0x001186E4 File Offset: 0x001168E4
		[CompilerGenerated]
		internal static bool <Build>g__TryAddTextRuns|12_4(ref TextRunTable.<>c__DisplayClass12_0 A_0, ref TextRunTable.<>c__DisplayClass12_2 A_1, ref TextRunTable.<>c__DisplayClass12_3 A_2)
		{
			if (!A_0.possibleRows.Any<TextRunTable.TextRunRow>())
			{
				A_0.possibleRows.Add(new TextRunTable.TextRunRow(A_1.textRunsByBottom, A_2.endedRowDueToSeparator || !A_0.finalizedRows.Any<TextRunTable.TextRunRow>(), A_0.options));
				return true;
			}
			Optional<TextRunTable.TextRunRow> optional = A_0.possibleRows.Last<TextRunTable.TextRunRow>().With(A_1.textRunsByBottom, A_0.lineWrapDistanceRange);
			if (optional.HasValue)
			{
				A_0.possibleRows.Add(optional.Value);
				return true;
			}
			return TextRunTable.<Build>g__FinalizeRow|12_0(false, ref A_0) && TextRunTable.<Build>g__TryAddTextRuns|12_4(ref A_0, ref A_1, ref A_2);
		}

		// Token: 0x040028BE RID: 10430
		private const double MaxLineWrapDistanceFraction = 0.7;

		// Token: 0x02000D81 RID: 3457
		[Nullable(new byte[] { 0, 1 })]
		internal class TextRunCell : MergedCell<ITextRun>, IApparentPixelBounded
		{
			// Token: 0x17000FFE RID: 4094
			// (get) Token: 0x0600582D RID: 22573 RVA: 0x00118782 File Offset: 0x00116982
			public IReadOnlyList<ITextRun> TextRuns
			{
				get
				{
					return base.Cells;
				}
			}

			// Token: 0x17000FFF RID: 4095
			// (get) Token: 0x0600582E RID: 22574 RVA: 0x0011878A File Offset: 0x0011698A
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit> StablePixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return Bounds<PixelUnit>.Join(this.TextRuns.Select((ITextRun t) => t.StablePixelBounds));
				}
			}

			// Token: 0x17001000 RID: 4096
			// (get) Token: 0x0600582F RID: 22575 RVA: 0x001187BB File Offset: 0x001169BB
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit> ApparentPixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x06005830 RID: 22576 RVA: 0x001187C4 File Offset: 0x001169C4
			public TextRunCell(ITextRun initialTextRun, PdfAnalyzerOptions options)
				: base(new ITextRun[] { initialTextRun }, options)
			{
				this._contiguousLists = initialTextRun.ContiguousLists.Vertical.ToList<ContiguousList<ITextRun>>();
				this._alignments = ((initialTextRun.BeforeAlignmentDotRow != null) ? initialTextRun.Alignments.Vertical.ToList<Alignment<ITextRun>>() : null);
				this.ApparentPixelBounds = initialTextRun.ApparentPixelBounds;
			}

			// Token: 0x06005831 RID: 22577 RVA: 0x00118828 File Offset: 0x00116A28
			private TextRunCell(IReadOnlyList<ITextRun> textRuns, PdfAnalyzerOptions options, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyList<ContiguousList<ITextRun>> contiguousLists = null, [Nullable(new byte[] { 2, 1, 1 })] IReadOnlyList<Alignment<ITextRun>> alignments = null, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit>? apparentPixelBounds = null)
				: base(textRuns, options)
			{
				IReadOnlyList<ContiguousList<ITextRun>> readOnlyList = contiguousLists;
				if (contiguousLists == null)
				{
					readOnlyList = this.TextRuns.Select((ITextRun t) => t.ContiguousLists.Vertical).Intersect<ContiguousList<ITextRun>>().ToList<ContiguousList<ITextRun>>();
				}
				this._contiguousLists = readOnlyList;
				this._alignments = alignments;
				Bounds<PixelUnit>? bounds = apparentPixelBounds;
				Bounds<PixelUnit> bounds2;
				if (bounds == null)
				{
					bounds2 = Bounds<PixelUnit>.Join(this.TextRuns.Select((ITextRun t) => t.ApparentPixelBounds));
				}
				else
				{
					bounds2 = bounds.GetValueOrDefault();
				}
				this.ApparentPixelBounds = bounds2;
			}

			// Token: 0x17001001 RID: 4097
			// (get) Token: 0x06005832 RID: 22578 RVA: 0x001188CE File Offset: 0x00116ACE
			internal bool MayBeCentered
			{
				get
				{
					return this._contiguousLists.Any((ContiguousList<ITextRun> list) => list.BaseAlignment.Justification == Justification.Center);
				}
			}

			// Token: 0x17001002 RID: 4098
			// (get) Token: 0x06005833 RID: 22579 RVA: 0x001188FC File Offset: 0x00116AFC
			internal int MinColumnWidth
			{
				get
				{
					return this._contiguousLists.MaybeMin((ContiguousList<ITextRun> list) => list.PixelBounds.Width()).OrElse(this.ApparentPixelBounds.Width());
				}
			}

			// Token: 0x17001003 RID: 4099
			// (get) Token: 0x06005834 RID: 22580 RVA: 0x00118948 File Offset: 0x00116B48
			internal bool CouldBeWrappedToCellWidth
			{
				get
				{
					if (this.TextRuns.Count <= 1)
					{
						return true;
					}
					if (this.MayBeCentered)
					{
						return true;
					}
					if (this.TextRuns.Any((ITextRun tr) => tr.HasAlignmentDotRow<ITextRun>()))
					{
						return true;
					}
					int minColumnWidth = this.MinColumnWidth;
					using (IEnumerator<Record<ITextRun, ITextRun>> enumerator = this.TextRuns.Windowed<ITextRun>().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ITextRun textRun;
							ITextRun textRun2;
							enumerator.Current.Deconstruct(out textRun, out textRun2);
							ITextRun textRun3 = textRun;
							ITextRun belowTextRun = textRun2;
							int maxGlyphWidth = (int)Math.Ceiling(textRun3.Children.Concat(belowTextRun.Children).Max((IWord word) => word.AverageGlyphWidth()));
							int num = (from word in belowTextRun.Children.Skip(1).MaybeFirst((IWord word) => word.HasSpaceBefore ?? true)
								select word.ApparentPixelBounds.Left).OrElseCompute(() => belowTextRun.ApparentPixelBounds.Right + maxGlyphWidth) - belowTextRun.ApparentPixelBounds.Left + 1 + maxGlyphWidth;
							int num2 = textRun3.ApparentPixelBounds.Width();
							if (minColumnWidth - num2 > num)
							{
								return false;
							}
						}
					}
					return true;
				}
			}

			// Token: 0x17001004 RID: 4100
			// (get) Token: 0x06005835 RID: 22581 RVA: 0x00118B00 File Offset: 0x00116D00
			private bool DoesContentLikelyHaveRowHeader
			{
				get
				{
					return this.TextRuns.Where((ITextRun cell) => SpecialCharacters.EndsWithColon(cell.Content)).HasAtLeast(2);
				}
			}

			// Token: 0x17001005 RID: 4101
			// (get) Token: 0x06005836 RID: 22582 RVA: 0x00118B32 File Offset: 0x00116D32
			[Nullable(new byte[] { 0, 1 })]
			internal override MergedCell<ITextRun>.CellIsMultipleRowsConfidence IsContentMultipleRowsConfidence
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					if (this.DoesContentLikelyHaveRowHeader)
					{
						return MergedCell<ITextRun>.CellIsMultipleRowsConfidence.LikelyMultipleRows;
					}
					return base.IsContentMultipleRowsConfidence;
				}
			}

			// Token: 0x06005837 RID: 22583 RVA: 0x00118B44 File Offset: 0x00116D44
			[return: Nullable(new byte[] { 0, 1 })]
			public Optional<TextRunTable.TextRunCell> With(ITextRun textRun, [Nullable(new byte[] { 1, 0, 1 })] FloatKeyReadOnlyDictionary<Range<PixelUnit>> lineWrapDistanceRange)
			{
				if (!textRun.ApparentPixelBounds.Horizontal.Overlaps(this.ApparentPixelBounds.Horizontal))
				{
					return Optional<TextRunTable.TextRunCell>.Nothing;
				}
				if (textRun.BeforeAlignmentDotRow != null)
				{
					return Optional<TextRunTable.TextRunCell>.Nothing;
				}
				ITextRun textRun2 = this.TextRuns.Last<ITextRun>();
				ITextRun textRun3 = this.TextRuns.First<ITextRun>();
				if (textRun2.AfterAlignmentDotRow != null)
				{
					return Optional<TextRunTable.TextRunCell>.Nothing;
				}
				if (textRun2.Font == null || textRun.Font == null || !MathUtils.WithinTolerance((double)textRun2.Font.FontSize, (double)textRun.Font.FontSize, PageStatistics.FontSizeEpsilon))
				{
					return Optional<TextRunTable.TextRunCell>.Nothing;
				}
				int num = textRun2.ApparentPixelBounds.Vertical.Distance(textRun.ApparentPixelBounds.Vertical);
				Range<PixelUnit> range;
				if ((!lineWrapDistanceRange.TryGetValue(textRun.Font.FontSize, out range) || !range.Contains(num)) && (textRun.AfterAlignmentDotRow == null || !range.Expand(2).Contains(num)))
				{
					return Optional<TextRunTable.TextRunCell>.Nothing;
				}
				IReadOnlyList<ContiguousList<ITextRun>> readOnlyList = this._contiguousLists.Intersect(textRun.ContiguousLists.Vertical).ToList<ContiguousList<ITextRun>>();
				IReadOnlyList<Alignment<ITextRun>> alignments = this._alignments;
				IReadOnlyList<Alignment<ITextRun>> readOnlyList2 = ((alignments != null) ? alignments.Intersect(textRun.Alignments.Vertical).ToList<Alignment<ITextRun>>() : null);
				if (textRun3.BeforeAlignmentDotRow != null && readOnlyList2 != null)
				{
					if (!readOnlyList2.Any((Alignment<ITextRun> alignment) => alignment.Justification == Justification.After || alignment.Justification == Justification.Unknown))
					{
						return Optional<TextRunTable.TextRunCell>.Nothing;
					}
				}
				if (readOnlyList.Count == 1 && textRun3.BeforeAlignmentDotRow == null)
				{
					Func<ITextRun, bool> <>9__2;
					Func<ITextRun, bool> <>9__3;
					Optional<ITextRun> optional = textRun.BoundedLists.Vertical.SelectMany(delegate(IBoundedList<ITextRun> list)
					{
						IEnumerable<ITextRun> cells = list.Cells;
						Func<ITextRun, bool> func;
						if ((func = <>9__2) == null)
						{
							func = (<>9__2 = (ITextRun run) => run != textRun);
						}
						Optional<ITextRun> optional2 = cells.SkipWhile(func).Skip(1).MaybeFirst<ITextRun>();
						Func<ITextRun, bool> func2;
						if ((func2 = <>9__3) == null)
						{
							func2 = (<>9__3 = (ITextRun run) => range.Expand(1).Contains(run.ApparentPixelBounds.Vertical.Distance(textRun.ApparentPixelBounds.Vertical)));
						}
						return optional2.Where(func2);
					}).Distinct<ITextRun>().MaybeOnly<ITextRun>();
					if (optional.HasValue && !readOnlyList[0].Cells.Contains(optional.Value))
					{
						return Optional<TextRunTable.TextRunCell>.Nothing;
					}
				}
				return new TextRunTable.TextRunCell(this.TextRuns.AppendItem(textRun).ToList<ITextRun>(), base.Options, readOnlyList, readOnlyList2, new Bounds<PixelUnit>?(this.ApparentPixelBounds.Join(textRun.ApparentPixelBounds))).Some<TextRunTable.TextRunCell>();
			}

			// Token: 0x06005838 RID: 22584 RVA: 0x00118DCC File Offset: 0x00116FCC
			[NullableContext(2)]
			public TextRunTable.TextRunCell FilterToBelow(int bottom)
			{
				IReadOnlyList<ITextRun> readOnlyList = this.TextRuns.Where((ITextRun t) => t.ApparentPixelBounds.Bottom > bottom).ToList<ITextRun>();
				if (!readOnlyList.Any<ITextRun>())
				{
					return null;
				}
				return new TextRunTable.TextRunCell(readOnlyList, base.Options, null, null, null);
			}

			// Token: 0x040028C0 RID: 10432
			private readonly IReadOnlyList<ContiguousList<ITextRun>> _contiguousLists;

			// Token: 0x040028C1 RID: 10433
			[Nullable(new byte[] { 2, 1, 1 })]
			private readonly IReadOnlyList<Alignment<ITextRun>> _alignments;
		}

		// Token: 0x02000D86 RID: 3462
		[Nullable(new byte[] { 0, 1, 1 })]
		internal class TextRunRow : MergedCellRow<ITextRun, TextRunTable.TextRunCell>, IApparentPixelBounded
		{
			// Token: 0x17001006 RID: 4102
			// (get) Token: 0x0600584E RID: 22606 RVA: 0x00118FD7 File Offset: 0x001171D7
			public IReadOnlyList<TextRunTable.TextRunCell> Cells
			{
				get
				{
					return this.MergedCells;
				}
			}

			// Token: 0x17001007 RID: 4103
			// (get) Token: 0x0600584F RID: 22607 RVA: 0x00118FDF File Offset: 0x001171DF
			public bool SeparatorAbove { get; }

			// Token: 0x17001008 RID: 4104
			// (get) Token: 0x06005850 RID: 22608 RVA: 0x00118FE7 File Offset: 0x001171E7
			[Nullable(new byte[] { 0, 1 })]
			public Range<PixelUnit> VerticalRange
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x17001009 RID: 4105
			// (get) Token: 0x06005851 RID: 22609 RVA: 0x00118FF0 File Offset: 0x001171F0
			public int Bottom
			{
				get
				{
					return this.VerticalRange.Max;
				}
			}

			// Token: 0x1700100A RID: 4106
			// (get) Token: 0x06005852 RID: 22610 RVA: 0x0011900B File Offset: 0x0011720B
			private PdfAnalyzerOptions Options { get; }

			// Token: 0x1700100B RID: 4107
			// (get) Token: 0x06005853 RID: 22611 RVA: 0x00119013 File Offset: 0x00117213
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit> ApparentPixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return Bounds<PixelUnit>.Join(this.Cells.Select((TextRunTable.TextRunCell t) => t.ApparentPixelBounds));
				}
			}

			// Token: 0x1700100C RID: 4108
			// (get) Token: 0x06005854 RID: 22612 RVA: 0x00119044 File Offset: 0x00117244
			[Nullable(new byte[] { 0, 1 })]
			Bounds<PixelUnit> IApparentPixelBounded.StablePixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return Bounds<PixelUnit>.Join(this.Cells.Select((TextRunTable.TextRunCell t) => t.StablePixelBounds));
				}
			}

			// Token: 0x06005855 RID: 22613 RVA: 0x00119078 File Offset: 0x00117278
			public TextRunRow(IGrouping<int, ITextRun> initialTextRuns, bool separatorAbove, PdfAnalyzerOptions options)
				: this(initialTextRuns.Select((ITextRun textRun) => new TextRunTable.TextRunCell(textRun, options)).ToList<TextRunTable.TextRunCell>(), initialTextRuns.Key, separatorAbove, options)
			{
			}

			// Token: 0x06005856 RID: 22614 RVA: 0x001190BC File Offset: 0x001172BC
			private TextRunRow(IReadOnlyList<TextRunTable.TextRunCell> cells, int bottom, bool separatorAbove, PdfAnalyzerOptions options)
				: base(cells)
			{
				this.SeparatorAbove = separatorAbove;
				this.VerticalRange = new Range<PixelUnit>(this.Cells.Min((TextRunTable.TextRunCell cell) => cell.ApparentPixelBounds.Top), bottom);
				this.Options = options;
			}

			// Token: 0x1700100D RID: 4109
			// (get) Token: 0x06005857 RID: 22615 RVA: 0x00119118 File Offset: 0x00117318
			private bool HasCellsNotAtTopOrBottom
			{
				get
				{
					if (this.VerticalRange.Size() >= 3)
					{
						Range<PixelUnit> rangeInside = this.VerticalRange.Expand(-1);
						return this.Cells.Any((TextRunTable.TextRunCell cell) => rangeInside.Contains(cell.ApparentPixelBounds.Vertical));
					}
					return false;
				}
			}

			// Token: 0x06005858 RID: 22616 RVA: 0x0011916C File Offset: 0x0011736C
			private bool AreCellsVerticallyAligned()
			{
				int rowCurrentBottom = this.VerticalRange.Max;
				int rowCurrentTop = this.VerticalRange.Min;
				bool flag = this.Cells.Any((TextRunTable.TextRunCell cell) => cell.TextRuns.Last<ITextRun>().AfterAlignmentDotRow != null || cell.TextRuns.First<ITextRun>().BeforeAlignmentDotRow != null);
				double? center;
				if (!flag)
				{
					IReadOnlyList<TextRunTable.TextRunCell> readOnlyList = this.Cells.Where((TextRunTable.TextRunCell c) => c.ApparentPixelBounds.Bottom < rowCurrentBottom && c.ApparentPixelBounds.Top > rowCurrentTop).ToList<TextRunTable.TextRunCell>();
					center = new double?((from c in readOnlyList.Any<TextRunTable.TextRunCell>() ? readOnlyList : this.Cells
						group c by c.TextRuns.Count).ArgMin((IGrouping<int, TextRunTable.TextRunCell> g) => g.Key).Average((TextRunTable.TextRunCell c) => c.ApparentPixelBounds.Vertical.Center()));
				}
				else
				{
					center = null;
				}
				IReadOnlyDictionary<TextRunTable.TextRunCell, Justification?> readOnlyDictionary = this.Cells.ToDictionary((TextRunTable.TextRunCell c) => c, delegate(TextRunTable.TextRunCell c)
				{
					bool flag3;
					if (center != null)
					{
						double valueOrDefault = center.GetValueOrDefault();
						flag3 = MathUtils.WithinTolerance(valueOrDefault, c.ApparentPixelBounds.Vertical.Center(), 4.0);
					}
					else
					{
						flag3 = false;
					}
					bool flag4 = flag3;
					bool flag5 = MathUtils.WithinTolerance(c.ApparentPixelBounds.Top, rowCurrentTop, 2);
					bool flag6 = MathUtils.WithinTolerance(c.ApparentPixelBounds.Bottom, rowCurrentBottom, 2);
					if (flag4 && !flag5 && !flag6)
					{
						return new Justification?(Justification.Center);
					}
					if (flag4)
					{
						return new Justification?(Justification.Unknown);
					}
					if (flag5 && flag6)
					{
						return new Justification?(Justification.BeforeAndAfter);
					}
					if (flag5)
					{
						return new Justification?(Justification.Before);
					}
					if (flag6)
					{
						return new Justification?(Justification.After);
					}
					return null;
				});
				HashSet<Justification?> hashSet = (from kv in readOnlyDictionary.Where(delegate(KeyValuePair<TextRunTable.TextRunCell, Justification?> kv)
					{
						Justification? justification3 = kv.Value;
						Justification justification2 = Justification.Before;
						if (!((justification3.GetValueOrDefault() == justification2) & (justification3 != null)) || kv.Key.TextRuns.Last<ITextRun>().AfterAlignmentDotRow == null)
						{
							justification3 = kv.Value;
							justification2 = Justification.After;
							return !((justification3.GetValueOrDefault() == justification2) & (justification3 != null)) || kv.Key.TextRuns.First<ITextRun>().BeforeAlignmentDotRow == null;
						}
						return false;
					})
					select kv.Value).ConvertToHashSet<Justification?>();
				if (hashSet.Contains(null))
				{
					return false;
				}
				bool flag2 = hashSet.Contains(new Justification?(Justification.Center));
				if (flag2 && center != null && !MathUtils.WithinTolerance(this.VerticalRange.Center(), center.Value, 5.0))
				{
					return false;
				}
				if (!flag && hashSet.IsSupersetOf(new Justification?[]
				{
					new Justification?(Justification.After),
					new Justification?(Justification.Before)
				}))
				{
					return false;
				}
				if (flag2 && hashSet.Contains(new Justification?(Justification.BeforeAndAfter)))
				{
					if (!readOnlyDictionary.Where2(delegate(TextRunTable.TextRunCell cell, Justification? justification)
					{
						Justification? justification4 = justification;
						Justification justification5 = Justification.BeforeAndAfter;
						return (justification4.GetValueOrDefault() == justification5) & (justification4 != null);
					}).All((KeyValuePair<TextRunTable.TextRunCell, Justification?> kv) => kv.Key.TextRuns.Any((ITextRun tr) => tr.IsRotated)))
					{
						return false;
					}
				}
				if (!flag)
				{
					if (readOnlyDictionary.Values.Count(delegate(Justification? j)
					{
						Justification? justification6 = j;
						Justification justification7 = Justification.Before;
						return (justification6.GetValueOrDefault() == justification7) & (justification6 != null);
					}) == this.Cells.Count - 1)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x1700100E RID: 4110
			// (get) Token: 0x06005859 RID: 22617 RVA: 0x00119440 File Offset: 0x00117640
			private bool CouldBeWrappedToCellWidths
			{
				get
				{
					if (!this.Cells.OrderBy((TextRunTable.TextRunCell cell) => cell.ApparentPixelBounds.Left).Skip(1).Any((TextRunTable.TextRunCell cell) => cell.TextRuns.Count > 1))
					{
						return true;
					}
					return this.Cells.All((TextRunTable.TextRunCell cell) => cell.CouldBeWrappedToCellWidth);
				}
			}

			// Token: 0x0600585A RID: 22618 RVA: 0x001194D0 File Offset: 0x001176D0
			public bool IsValidRow(TextRunTable.TextRunRow previousRow, bool separatorBelow)
			{
				TextRunTable.TextRunCell textRunCell = this.Cells.OnlyOrDefault<TextRunTable.TextRunCell>();
				if (textRunCell != null && textRunCell.TextRuns.Count > 1 && previousRow != null)
				{
					if (previousRow.VerticalRange.Distance(this.VerticalRange) < this.Cells[0].TextRuns.Max((ITextRun textRun) => textRun.ApparentPixelBounds.Height()))
					{
						return false;
					}
				}
				if (previousRow != null && this.VerticalRange.Overlaps(previousRow.VerticalRange) && !this.HasCellsNotAtTopOrBottom && !previousRow.HasCellsNotAtTopOrBottom && !previousRow.Cells.All((TextRunTable.TextRunCell previousCell) => this.Cells.Any((TextRunTable.TextRunCell cell) => cell.ApparentPixelBounds.Overlaps(previousCell.ApparentPixelBounds))) && !this.Cells.All((TextRunTable.TextRunCell cell) => previousRow.Cells.Any((TextRunTable.TextRunCell previousCell) => cell.ApparentPixelBounds.Overlaps(previousCell.ApparentPixelBounds))))
				{
					return false;
				}
				if (this.Cells.Count == 1)
				{
					return true;
				}
				if (!this.CouldBeWrappedToCellWidths)
				{
					return false;
				}
				if (!separatorBelow || !this.SeparatorAbove)
				{
					IReadOnlyList<TextRunTable.TextRunCell> readOnlyList = this.Cells.SkipWhile((TextRunTable.TextRunCell cell) => cell.TextRuns.Count == 1).TakeWhile((TextRunTable.TextRunCell cell) => cell.TextRuns.Count > 1).ToList<TextRunTable.TextRunCell>();
					if (readOnlyList.Count > 1)
					{
						if (previousRow != null || readOnlyList.Count != this.Cells.Count)
						{
							if (previousRow == null)
							{
								goto IL_0258;
							}
							if (!this.Cells.Except(readOnlyList).All((TextRunTable.TextRunCell cell) => cell.TextRuns.Count == 1))
							{
								goto IL_0258;
							}
						}
						HashSet<ContiguousList<ITextRun>> hashSet = null;
						bool flag = true;
						foreach (IEnumerable<ContiguousList<ITextRun>> enumerable in readOnlyList.Select((TextRunTable.TextRunCell cell) => cell.TextRuns.SelectMany((ITextRun t) => t.ContiguousLists.Horizontal)))
						{
							if (hashSet == null)
							{
								hashSet = enumerable.ConvertToHashSet<ContiguousList<ITextRun>>();
							}
							else if (!hashSet.SetEquals(enumerable))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							return false;
						}
					}
				}
				IL_0258:
				return !this.Cells.Any((TextRunTable.TextRunCell cell) => cell.IsContentLikelyMultipleRows) && this.AreCellsVerticallyAligned();
			}

			// Token: 0x0600585B RID: 22619 RVA: 0x0011977C File Offset: 0x0011797C
			[return: Nullable(new byte[] { 0, 1 })]
			public Optional<TextRunTable.TextRunRow> With(IGrouping<int, ITextRun> newTextRuns, [Nullable(new byte[] { 1, 0, 1 })] FloatKeyReadOnlyDictionary<Range<PixelUnit>> lineWrapDistanceRange)
			{
				List<TextRunTable.TextRunCell> newCells = this.Cells.ToList<TextRunTable.TextRunCell>();
				foreach (ITextRun textRun in newTextRuns)
				{
					bool flag = false;
					bool flag2 = false;
					int j;
					int i;
					for (i = 0; i < newCells.Count; i = j + 1)
					{
						if (textRun.ApparentPixelBounds.Horizontal.Overlaps(newCells[i].ApparentPixelBounds.Horizontal))
						{
							flag2 = true;
							Optional<TextRunTable.TextRunCell> optional = newCells[i].With(textRun, lineWrapDistanceRange);
							if (optional.HasValue)
							{
								if (newCells[i].TextRuns[0].BeforeAlignmentDotRow != null)
								{
									TextRunTable.TextRunCell textRunCell = this.Cells.OnlyOrDefault((TextRunTable.TextRunCell cell) => cell.TextRuns.Last<ITextRun>().AfterAlignmentDotRow == newCells[i].TextRuns[0].BeforeAlignmentDotRow);
									int? num = ((textRunCell != null) ? new int?(textRunCell.ApparentPixelBounds.Left) : null);
									if (num != null)
									{
										int valueOrDefault = num.GetValueOrDefault();
										if (MathUtils.WithinTolerance(valueOrDefault, textRun.ApparentPixelBounds.Left, 2))
										{
											return Optional<TextRunTable.TextRunRow>.Nothing;
										}
									}
								}
								flag = true;
								newCells[i] = optional.Value;
								break;
							}
							if (textRun.BeforeAlignmentDotRow != null && newCells[i].TextRuns.Last<ITextRun>().AfterAlignmentDotRow == textRun.BeforeAlignmentDotRow)
							{
								flag2 = false;
							}
						}
						j = i;
					}
					if (!flag2)
					{
						newCells.Add(new TextRunTable.TextRunCell(textRun, this.Options));
					}
					else if (!flag)
					{
						return Optional<TextRunTable.TextRunRow>.Nothing;
					}
				}
				return new TextRunTable.TextRunRow(newCells.OrderBy((TextRunTable.TextRunCell cell) => cell.ApparentPixelBounds.Left).ToList<TextRunTable.TextRunCell>(), newTextRuns.Key, this.SeparatorAbove, this.Options).Some<TextRunTable.TextRunRow>();
			}

			// Token: 0x0600585C RID: 22620 RVA: 0x00119A20 File Offset: 0x00117C20
			[NullableContext(2)]
			public TextRunTable.TextRunRow FilterToBelow(int bottom)
			{
				if (bottom > this.Bottom)
				{
					return null;
				}
				IReadOnlyList<TextRunTable.TextRunCell> readOnlyList = this.Cells.Collect((TextRunTable.TextRunCell cell) => cell.FilterToBelow(bottom)).ToList<TextRunTable.TextRunCell>();
				if (!readOnlyList.Any<TextRunTable.TextRunCell>())
				{
					return null;
				}
				return new TextRunTable.TextRunRow(readOnlyList, this.Bottom, false, this.Options);
			}
		}

		// Token: 0x02000D91 RID: 3473
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002900 RID: 10496
			[Nullable(0)]
			public static Func<Record<int, int>, Range<PixelUnit>> <0>__Create;
		}
	}
}
