using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CB2 RID: 3250
	[NullableContext(1)]
	[Nullable(0)]
	public class DependencyGraph
	{
		// Token: 0x06005357 RID: 21335 RVA: 0x001074D0 File Offset: 0x001056D0
		[NullableContext(2)]
		public DependencyGraph(PageData pageData = null, PdfAnalyzerOptions options = null)
		{
			if (pageData == null)
			{
				this.Set(0);
				this.Set(Bounds<PixelUnit>.Zero);
				this.Set(new IReadOnlyList<Glyph>[0]);
				this.Set(new GraphicalPath[0]);
				this.Set(new Image[0]);
			}
			else
			{
				this.Set(pageData.PageIndex);
				this.Set(pageData.PageBounds);
				this.Set(pageData.Glyphs);
				this.Set(pageData.Paths);
				this.Set(pageData.Images);
			}
			this._analyzerOptions = options ?? new PdfAnalyzerOptions(null, null, null, null, false, true, true, true, null, null);
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x001075A6 File Offset: 0x001057A6
		public DependencyGraph Set(int pageIndex)
		{
			this._pageIndex = new int?(pageIndex);
			return this;
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x001075B5 File Offset: 0x001057B5
		public DependencyGraph Set([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds)
		{
			this._pageBounds = new Bounds<PixelUnit>?(pageBounds);
			return this;
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x001075C4 File Offset: 0x001057C4
		public DependencyGraph Set(IReadOnlyList<IReadOnlyList<Glyph>> glyphs)
		{
			this._glyphs = glyphs;
			return this;
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x001075CE File Offset: 0x001057CE
		public DependencyGraph Set(IReadOnlyList<GraphicalPath> paths)
		{
			this._paths = paths;
			return this;
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x001075D8 File Offset: 0x001057D8
		public DependencyGraph Set(IReadOnlyList<Image> images)
		{
			this._images = images;
			return this;
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x001075E2 File Offset: 0x001057E2
		internal DependencyGraph Set(PathCollection pathCollection)
		{
			this._pathCollection = pathCollection;
			return this;
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x001075EC File Offset: 0x001057EC
		internal DependencyGraph Set(ImageCollection imageCollection)
		{
			this._imageCollection = imageCollection;
			return this;
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x001075F6 File Offset: 0x001057F6
		public DependencyGraph Set(PdfAnalyzerOptions analyzerOptions)
		{
			this._analyzerOptions = analyzerOptions;
			return this;
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x00107600 File Offset: 0x00105800
		public DependencyGraph Set(SeparatorCollection separators)
		{
			this._separators = separators;
			return this;
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x0010760A File Offset: 0x0010580A
		internal DependencyGraph Set(QuadTree<SeparatorGrid, PixelUnit> separatorGrids)
		{
			this._separatorGrids = separatorGrids;
			return this;
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x00107614 File Offset: 0x00105814
		public DependencyGraph Set(FloatKeyReadOnlyDictionary<IReadOnlyList<IWord>> words)
		{
			this._words = words;
			return this;
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x0010761E File Offset: 0x0010581E
		internal DependencyGraph Set(IReadOnlyList<LineGroup> lines)
		{
			this._lines = lines;
			return this;
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x00107628 File Offset: 0x00105828
		public DependencyGraph Set(QuadTree<ICell, PixelUnit> cells)
		{
			this._cells = cells;
			return this;
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x00107632 File Offset: 0x00105832
		public DependencyGraph Set(AxisAlignedList<Alignment<ICell>> alignments)
		{
			this._alignments = alignments;
			return this;
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x0010763C File Offset: 0x0010583C
		public DependencyGraph Set(AxisAlignedList<IBoundedList<ICell>> boundedLists)
		{
			this._boundedLists = boundedLists;
			return this;
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x00107646 File Offset: 0x00105846
		public DependencyGraph Set(AxisAlignedList<ContiguousList<ICell>> contiguousLists)
		{
			this._contiguousLists = contiguousLists;
			return this;
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x00107650 File Offset: 0x00105850
		internal DependencyGraph Set(NonConflictingRegionCollection<ICell> nonConflictingRegions)
		{
			this._nonConflictingRegions = nonConflictingRegions;
			return this;
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x0010765A File Offset: 0x0010585A
		internal DependencyGraph Set(IReadOnlyList<Grid<ICell>> grids)
		{
			this._grids = grids;
			return this;
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x00107664 File Offset: 0x00105864
		public DependencyGraph Set(IProsePdfTable<ICell> fullTable)
		{
			this._fullTable = fullTable;
			return this;
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x0010766E File Offset: 0x0010586E
		internal DependencyGraph Set(ConflictCollection<ICell> conflicts)
		{
			this._conflicts = conflicts;
			return this;
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x00107678 File Offset: 0x00105878
		public DependencyGraph SetSimpleTables(IReadOnlyList<IProsePdfTable<ICell>> simpleTables)
		{
			this._simpleTables = simpleTables;
			return this;
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x00107682 File Offset: 0x00105882
		public int GetPageIndex()
		{
			if (this._pageIndex == null)
			{
				throw new InvalidOperationException("PageIndex required.");
			}
			return this._pageIndex.Value;
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x001076A7 File Offset: 0x001058A7
		[return: Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> GetPageBounds()
		{
			if (this._pageBounds == null)
			{
				throw new InvalidOperationException("PageBounds required.");
			}
			return this._pageBounds.Value;
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x001076CC File Offset: 0x001058CC
		public IReadOnlyList<IReadOnlyList<Glyph>> GetGlyphs()
		{
			if (this._glyphs == null)
			{
				throw new InvalidOperationException("Glyphs required.");
			}
			return this._glyphs;
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x001076E7 File Offset: 0x001058E7
		public IReadOnlyList<GraphicalPath> GetPaths()
		{
			if (this._paths == null)
			{
				throw new InvalidOperationException("Paths required.");
			}
			return this._paths;
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x00107702 File Offset: 0x00105902
		public IReadOnlyList<Image> GetImages()
		{
			if (this._images == null)
			{
				throw new InvalidOperationException("Images required.");
			}
			return this._images;
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x0010771D File Offset: 0x0010591D
		public PdfAnalyzerOptions GetAnalyzerOptions()
		{
			if (this._analyzerOptions == null)
			{
				throw new InvalidOperationException("Analyzer options required.");
			}
			return this._analyzerOptions;
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x00107738 File Offset: 0x00105938
		public AlignmentDotCollection BuildAlignmentDotCollection()
		{
			if (this._alignmentDots == null)
			{
				this._alignmentDots = AlignmentDotCollection.Build(this.GetPageBounds(), this.GetGlyphs(), this.BuildPageStatistics(), this.BuildSeparatorCollection());
			}
			return this._alignmentDots;
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x0010776B File Offset: 0x0010596B
		public PageStatistics BuildPageStatistics()
		{
			if (this._pageStatistics == null)
			{
				this._pageStatistics = new PageStatistics(this.GetGlyphs());
			}
			return this._pageStatistics;
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x0010778C File Offset: 0x0010598C
		internal PathCollection BuildPathCollection()
		{
			if (this._pathCollection == null)
			{
				this._pathCollection = new PathCollection(this.GetPageBounds(), this.GetPaths());
			}
			return this._pathCollection;
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x001077B3 File Offset: 0x001059B3
		internal ImageCollection BuildImageCollection()
		{
			if (this._imageCollection == null)
			{
				this._imageCollection = ImageCollection.Build(this.GetPageBounds(), this.GetGlyphs(), this.GetImages());
			}
			return this._imageCollection;
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x001077E0 File Offset: 0x001059E0
		public SeparatorCollection BuildSeparatorCollection()
		{
			if (this._separators == null)
			{
				this._separators = SeparatorCollection.Build(this.GetAnalyzerOptions(), this.GetPageBounds(), this.GetGlyphs(), this.BuildPathCollection(), this.BuildImageCollection());
			}
			return this._separators;
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x00107819 File Offset: 0x00105A19
		public QuadTree<SeparatorGrid, PixelUnit> BuildSeparatorGrids()
		{
			if (this._separatorGrids == null)
			{
				this._separatorGrids = SeparatorGridBuilder.Build(this.BuildSeparatorCollection());
			}
			return this._separatorGrids;
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x0010783C File Offset: 0x00105A3C
		public FloatKeyReadOnlyDictionary<IReadOnlyList<IWord>> BuildWords()
		{
			if (this._words == null)
			{
				this._words = WordBuilder.Build(this.GetAnalyzerOptions(), this.GetPageBounds(), this.GetGlyphs(), this.BuildAlignmentDotCollection(), this.BuildPageStatistics(), this.BuildImageCollection(), this.BuildSeparatorCollection());
			}
			return this._words;
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x0010788C File Offset: 0x00105A8C
		internal IReadOnlyList<LineGroup> BuildLineGroups()
		{
			if (this._lines == null)
			{
				this._lines = LineBuilder.BuildLineGroups(this.BuildWords());
			}
			return this._lines;
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x001078AD File Offset: 0x00105AAD
		public IReadOnlyList<PartialTextRun> BuildPartialTextRuns()
		{
			if (this._partialTextRuns == null)
			{
				this._partialTextRuns = PartialTextRunBuilder.Build(this.BuildPageStatistics(), this.BuildSeparatorCollection(), this.BuildLineGroups());
			}
			return this._partialTextRuns;
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x001078DA File Offset: 0x00105ADA
		public QuadTree<ITextRun, PixelUnit> BuildTextRuns()
		{
			if (this._textRuns == null)
			{
				this._textRuns = TextRunBuilder.Build(this.GetPageBounds(), this.BuildAlignmentDotCollection(), this.BuildPartialTextRuns());
			}
			return this._textRuns;
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x00107907 File Offset: 0x00105B07
		public AxisAlignedList<Alignment<ITextRun>> BuildTextRunAlignments()
		{
			if (this._textRunAlignments == null)
			{
				this._textRunAlignments = AlignmentBuilder<ITextRun>.Build(this.BuildTextRuns());
			}
			return this._textRunAlignments;
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x00107928 File Offset: 0x00105B28
		public AxisAlignedList<ContiguousList<ITextRun>> BuildTextRunContiguousLists()
		{
			if (this._textRunContiguousLists == null)
			{
				this._textRunContiguousLists = ContiguousListBuilder<ITextRun>.Build(this.BuildTextRuns(), this.BuildTextRunAlignments());
			}
			return this._textRunContiguousLists;
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x0010794F File Offset: 0x00105B4F
		public AxisAlignedList<IBoundedList<ITextRun>> BuildTextRunBoundedLists()
		{
			if (this._textRunBoundedLists == null)
			{
				this._textRunBoundedLists = BoundedListBuilder.Build<ITextRun>(this.GetAnalyzerOptions(), this.BuildSeparatorCollection(), this.BuildTextRuns(), this.BuildTextRunAlignments(), this.BuildTextRunContiguousLists());
			}
			return this._textRunBoundedLists;
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x00107988 File Offset: 0x00105B88
		public IProsePdfTable<ITextRun> BuildTextRunFullTable()
		{
			if (this._textRunFullTable == null)
			{
				this._textRunFullTable = PdfTableBuilder.BuildFullPageTable<ITextRun>(this.GetPageIndex(), this.BuildTextRuns(), this.BuildTextRunBoundedLists(), this.GetAnalyzerOptions());
			}
			return this._textRunFullTable;
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x001079BB File Offset: 0x00105BBB
		internal ConflictCollection<ITextRun> BuildTextRunConflicts()
		{
			if (this._textRunConflicts == null)
			{
				this._textRunConflicts = ConflictCollection<ITextRun>.Build(this.GetPageBounds(), this.BuildTextRuns(), this.BuildTextRunContiguousLists(), new Axis?(Axis.Vertical));
			}
			return this._textRunConflicts;
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x001079EE File Offset: 0x00105BEE
		internal NonConflictingRegionCollection<ITextRun> BuildTextRunNonConflictingRegions()
		{
			if (this._textRunNonConflictingRegions == null)
			{
				this._textRunNonConflictingRegions = NonConflictingRegionCollection<ITextRun>.Build(this.GetPageBounds(), this.BuildTextRunConflicts());
			}
			return this._textRunNonConflictingRegions;
		}

		// Token: 0x06005383 RID: 21379 RVA: 0x00107A15 File Offset: 0x00105C15
		internal IReadOnlyList<Grid<ITextRun>> BuildTextRunGrids()
		{
			if (this._textRunGrids == null)
			{
				this._textRunGrids = GridBuilder<ITextRun>.RecognizeGrids(this.GetAnalyzerOptions(), this.BuildSeparatorCollection(), this.BuildTextRunNonConflictingRegions(), this.BuildTextRuns(), this.BuildTextRunFullTable());
			}
			return this._textRunGrids;
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x00107A50 File Offset: 0x00105C50
		public IReadOnlyList<IProsePdfTable<ITextRun>> BuildTextRunSimpleTables()
		{
			if (this._textRunSimpleTables == null)
			{
				this._textRunSimpleTables = PdfTableBuilder.BuildSimpleTables<ITextRun>(this.GetAnalyzerOptions(), this.BuildTextRuns(), this.BuildSeparatorCollection(), this.BuildSeparatorGrids(), this.BuildAlignmentDotCollection(), this.BuildTextRunFullTable(), this.BuildTextRunGrids(), new Axis?(Axis.Vertical));
			}
			return this._textRunSimpleTables;
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x00107AA8 File Offset: 0x00105CA8
		private static QuadTree<TCell, PixelUnit> ComputeTCellAdjustedBySimpleTables<TCell>(IReadOnlyList<IProsePdfTable<TCell>> simpleTables, QuadTree<TCell, PixelUnit> cells) where TCell : class, IWordAmalgamation<TCell>
		{
			return PdfTable<TCell>.SplitInfo.ApplySplits(simpleTables.OfType<PdfTable<TCell>>().Collect((PdfTable<TCell> table) => table.SplitInfos).SelectMany((IReadOnlyList<PdfTable<TCell>.SplitInfo> infos) => infos), cells);
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x00107B09 File Offset: 0x00105D09
		public QuadTree<ITextRun, PixelUnit> BuildTextRunsAdjustedBySimpleTables()
		{
			if (this._textRunsAdjustedBySimpleTables == null)
			{
				this._textRunsAdjustedBySimpleTables = DependencyGraph.ComputeTCellAdjustedBySimpleTables<ITextRun>(this.BuildTextRunSimpleTables(), this.BuildTextRuns());
			}
			return this._textRunsAdjustedBySimpleTables;
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x00107B30 File Offset: 0x00105D30
		internal IReadOnlyList<TextRunTable> BuildTextRunTables()
		{
			if (this._textRunTables == null)
			{
				this._textRunTables = TextRunTable.BuildCollection(this.GetAnalyzerOptions(), this.BuildPageStatistics(), this.BuildSeparatorCollection(), this.BuildSeparatorGrids(), this.BuildAlignmentDotCollection(), this.BuildTextRunsAdjustedBySimpleTables(), this.BuildTextRunContiguousLists(), this.BuildTextRunSimpleTables());
			}
			return this._textRunTables;
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x00107B88 File Offset: 0x00105D88
		public QuadTree<ICell, PixelUnit> BuildInternalCells()
		{
			if (this._cells == null)
			{
				this._cells = CellBuilder.Build(this.GetAnalyzerOptions(), this.GetPageBounds(), this.BuildSeparatorCollection(), this.BuildSeparatorGrids(), this.BuildAlignmentDotCollection(), this.BuildTextRunsAdjustedBySimpleTables(), this.BuildTextRunTables());
			}
			return this._cells;
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x00107BD8 File Offset: 0x00105DD8
		public AxisAlignedList<Alignment<ICell>> BuildAlignments()
		{
			if (this._alignments == null)
			{
				this._alignments = AlignmentBuilder<ICell>.Build(this.BuildInternalCells());
			}
			return this._alignments;
		}

		// Token: 0x0600538A RID: 21386 RVA: 0x00107BF9 File Offset: 0x00105DF9
		public AxisAlignedList<IBoundedList<ICell>> BuildBoundedLists()
		{
			if (this._boundedLists == null)
			{
				this._boundedLists = BoundedListBuilder.Build<ICell>(this.GetAnalyzerOptions(), this.BuildSeparatorCollection(), this.BuildInternalCells(), this.BuildAlignments(), this.BuildContiguousLists());
			}
			return this._boundedLists;
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x00107C32 File Offset: 0x00105E32
		public AxisAlignedList<ContiguousList<ICell>> BuildContiguousLists()
		{
			if (this._contiguousLists == null)
			{
				this._contiguousLists = ContiguousListBuilder<ICell>.Build(this.BuildInternalCells(), this.BuildAlignments());
			}
			return this._contiguousLists;
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x00107C59 File Offset: 0x00105E59
		public IProsePdfTable<ICell> BuildFullTable()
		{
			if (this._fullTable == null)
			{
				this._fullTable = PdfTableBuilder.BuildFullPageTable<ICell>(this.GetPageIndex(), this.BuildInternalCells(), this.BuildBoundedLists(), this.GetAnalyzerOptions());
			}
			return this._fullTable;
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x00107C8C File Offset: 0x00105E8C
		internal ConflictCollection<ICell> BuildConflicts()
		{
			if (this._conflicts == null)
			{
				this._conflicts = ConflictCollection<ICell>.Build(this.GetPageBounds(), this.BuildInternalCells(), this.BuildContiguousLists(), null);
			}
			return this._conflicts;
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x00107CCD File Offset: 0x00105ECD
		internal NonConflictingRegionCollection<ICell> BuildNonConflictingRegions()
		{
			if (this._nonConflictingRegions == null)
			{
				this._nonConflictingRegions = NonConflictingRegionCollection<ICell>.Build(this.GetPageBounds(), this.BuildConflicts());
			}
			return this._nonConflictingRegions;
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x00107CF4 File Offset: 0x00105EF4
		internal IReadOnlyList<Grid<ICell>> BuildGrids()
		{
			if (this._grids == null)
			{
				this._grids = GridBuilder<ICell>.RecognizeGrids(this.GetAnalyzerOptions(), this.BuildSeparatorCollection(), this.BuildNonConflictingRegions(), this.BuildInternalCells(), this.BuildFullTable());
			}
			return this._grids;
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x00107D30 File Offset: 0x00105F30
		public IReadOnlyList<IProsePdfTable<ICell>> BuildSimpleTables()
		{
			if (this._simpleTables == null)
			{
				this._simpleTables = PdfTableBuilder.BuildSimpleTables<ICell>(this.GetAnalyzerOptions(), this.BuildInternalCells(), this.BuildSeparatorCollection(), this.BuildSeparatorGrids(), this.BuildAlignmentDotCollection(), this.BuildFullTable(), this.BuildGrids(), null);
			}
			return this._simpleTables;
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x00107D89 File Offset: 0x00105F89
		public QuadTree<ICell, PixelUnit> BuildCells()
		{
			if (this._cellsAdjustedBySimpleTables == null)
			{
				this._cellsAdjustedBySimpleTables = DependencyGraph.ComputeTCellAdjustedBySimpleTables<ICell>(this.BuildSimpleTables(), this.BuildInternalCells());
			}
			return this._cellsAdjustedBySimpleTables;
		}

		// Token: 0x0400258D RID: 9613
		private int? _pageIndex;

		// Token: 0x0400258E RID: 9614
		[Nullable(new byte[] { 0, 1 })]
		private Bounds<PixelUnit>? _pageBounds;

		// Token: 0x0400258F RID: 9615
		[Nullable(new byte[] { 2, 1, 1 })]
		private IReadOnlyList<IReadOnlyList<Glyph>> _glyphs;

		// Token: 0x04002590 RID: 9616
		[Nullable(2)]
		private AlignmentDotCollection _alignmentDots;

		// Token: 0x04002591 RID: 9617
		[Nullable(2)]
		private PageStatistics _pageStatistics;

		// Token: 0x04002592 RID: 9618
		[Nullable(new byte[] { 2, 1 })]
		private IReadOnlyList<GraphicalPath> _paths;

		// Token: 0x04002593 RID: 9619
		[Nullable(2)]
		private PathCollection _pathCollection;

		// Token: 0x04002594 RID: 9620
		[Nullable(new byte[] { 2, 1 })]
		private IReadOnlyList<Image> _images;

		// Token: 0x04002595 RID: 9621
		[Nullable(2)]
		private PdfAnalyzerOptions _analyzerOptions;

		// Token: 0x04002596 RID: 9622
		[Nullable(2)]
		private ImageCollection _imageCollection;

		// Token: 0x04002597 RID: 9623
		[Nullable(2)]
		private SeparatorCollection _separators;

		// Token: 0x04002598 RID: 9624
		[Nullable(new byte[] { 2, 1, 1 })]
		private QuadTree<SeparatorGrid, PixelUnit> _separatorGrids;

		// Token: 0x04002599 RID: 9625
		[Nullable(new byte[] { 2, 1, 1 })]
		private FloatKeyReadOnlyDictionary<IReadOnlyList<IWord>> _words;

		// Token: 0x0400259A RID: 9626
		[Nullable(new byte[] { 2, 1 })]
		private IReadOnlyList<LineGroup> _lines;

		// Token: 0x0400259B RID: 9627
		[Nullable(new byte[] { 2, 1 })]
		private IReadOnlyList<PartialTextRun> _partialTextRuns;

		// Token: 0x0400259C RID: 9628
		[Nullable(new byte[] { 2, 1, 1 })]
		private QuadTree<ITextRun, PixelUnit> _textRuns;

		// Token: 0x0400259D RID: 9629
		[Nullable(new byte[] { 2, 1, 1 })]
		private QuadTree<ITextRun, PixelUnit> _textRunsAdjustedBySimpleTables;

		// Token: 0x0400259E RID: 9630
		[Nullable(new byte[] { 2, 1, 1 })]
		private AxisAlignedList<Alignment<ITextRun>> _textRunAlignments;

		// Token: 0x0400259F RID: 9631
		[Nullable(new byte[] { 2, 1, 1 })]
		private AxisAlignedList<ContiguousList<ITextRun>> _textRunContiguousLists;

		// Token: 0x040025A0 RID: 9632
		[Nullable(new byte[] { 2, 1 })]
		private ConflictCollection<ITextRun> _textRunConflicts;

		// Token: 0x040025A1 RID: 9633
		[Nullable(new byte[] { 2, 1 })]
		private NonConflictingRegionCollection<ITextRun> _textRunNonConflictingRegions;

		// Token: 0x040025A2 RID: 9634
		[Nullable(new byte[] { 2, 1, 1 })]
		private AxisAlignedList<IBoundedList<ITextRun>> _textRunBoundedLists;

		// Token: 0x040025A3 RID: 9635
		[Nullable(new byte[] { 2, 1, 1 })]
		private IReadOnlyList<Grid<ITextRun>> _textRunGrids;

		// Token: 0x040025A4 RID: 9636
		[Nullable(new byte[] { 2, 1 })]
		private IProsePdfTable<ITextRun> _textRunFullTable;

		// Token: 0x040025A5 RID: 9637
		[Nullable(new byte[] { 2, 1, 1 })]
		private IReadOnlyList<IProsePdfTable<ITextRun>> _textRunSimpleTables;

		// Token: 0x040025A6 RID: 9638
		[Nullable(new byte[] { 2, 1 })]
		private IReadOnlyList<TextRunTable> _textRunTables;

		// Token: 0x040025A7 RID: 9639
		[Nullable(new byte[] { 2, 1, 1 })]
		private QuadTree<ICell, PixelUnit> _cells;

		// Token: 0x040025A8 RID: 9640
		[Nullable(new byte[] { 2, 1, 1 })]
		private QuadTree<ICell, PixelUnit> _cellsAdjustedBySimpleTables;

		// Token: 0x040025A9 RID: 9641
		[Nullable(new byte[] { 2, 1, 1 })]
		private AxisAlignedList<Alignment<ICell>> _alignments;

		// Token: 0x040025AA RID: 9642
		[Nullable(new byte[] { 2, 1, 1 })]
		private AxisAlignedList<IBoundedList<ICell>> _boundedLists;

		// Token: 0x040025AB RID: 9643
		[Nullable(new byte[] { 2, 1, 1 })]
		private AxisAlignedList<ContiguousList<ICell>> _contiguousLists;

		// Token: 0x040025AC RID: 9644
		[Nullable(new byte[] { 2, 1 })]
		private ConflictCollection<ICell> _conflicts;

		// Token: 0x040025AD RID: 9645
		[Nullable(new byte[] { 2, 1 })]
		private NonConflictingRegionCollection<ICell> _nonConflictingRegions;

		// Token: 0x040025AE RID: 9646
		[Nullable(new byte[] { 2, 1, 1 })]
		private IReadOnlyList<Grid<ICell>> _grids;

		// Token: 0x040025AF RID: 9647
		[Nullable(new byte[] { 2, 1 })]
		private IProsePdfTable<ICell> _fullTable;

		// Token: 0x040025B0 RID: 9648
		[Nullable(new byte[] { 2, 1, 1 })]
		private IReadOnlyList<IProsePdfTable<ICell>> _simpleTables;
	}
}
