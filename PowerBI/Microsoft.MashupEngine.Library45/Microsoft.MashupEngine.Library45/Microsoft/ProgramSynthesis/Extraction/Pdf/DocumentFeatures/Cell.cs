using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000C73 RID: 3187
	[NullableContext(1)]
	[Nullable(0)]
	internal class Cell : ICell, IWordAmalgamation<ICell>, IApparentPixelBounded, IPixelBounded, IBounded<PixelUnit>
	{
		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x060051FB RID: 20987 RVA: 0x0010257C File Offset: 0x0010077C
		public string Content { get; }

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x060051FC RID: 20988 RVA: 0x00102584 File Offset: 0x00100784
		public LogicalGlyphOrdering LogicalGlyphOrdering { get; }

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x060051FD RID: 20989 RVA: 0x0010258C File Offset: 0x0010078C
		public IReadOnlyList<IWord> Children { get; }

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x060051FE RID: 20990 RVA: 0x00102594 File Offset: 0x00100794
		[Nullable(2)]
		public FontCharacteristics Font
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x060051FF RID: 20991 RVA: 0x0010259C File Offset: 0x0010079C
		public bool BuiltFromMultipleTextRuns
		{
			get
			{
				return this.TextRuns.Count > 1;
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06005200 RID: 20992 RVA: 0x001025AC File Offset: 0x001007AC
		public IReadOnlyList<ITextRun> TextRuns { get; }

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06005201 RID: 20993 RVA: 0x001025B4 File Offset: 0x001007B4
		public AxisAlignedSet<Alignment<ICell>> Alignments { get; }

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06005202 RID: 20994 RVA: 0x001025BC File Offset: 0x001007BC
		public AxisAlignedSet<IBoundedList<ICell>> BoundedLists { get; }

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06005203 RID: 20995 RVA: 0x001025C4 File Offset: 0x001007C4
		public AxisAlignedSet<ContiguousList<ICell>> ContiguousLists { get; }

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06005204 RID: 20996 RVA: 0x001025CC File Offset: 0x001007CC
		[Nullable(2)]
		public AlignmentDotCollection.AlignmentDotRow BeforeAlignmentDotRow
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06005205 RID: 20997 RVA: 0x001025D4 File Offset: 0x001007D4
		[Nullable(2)]
		public AlignmentDotCollection.AlignmentDotRow AfterAlignmentDotRow
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06005206 RID: 20998 RVA: 0x001025DC File Offset: 0x001007DC
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> StablePixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this._stablePixelBounds == null)
				{
					this._stablePixelBounds = new Bounds<PixelUnit>?(Bounds<PixelUnit>.Join(this.Children.Select((IWord w) => w.StablePixelBounds)));
				}
				return this._stablePixelBounds.Value;
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06005207 RID: 20999 RVA: 0x0010263B File Offset: 0x0010083B
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> ApparentPixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06005208 RID: 21000 RVA: 0x00102643 File Offset: 0x00100843
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> ScriptsInclusiveBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06005209 RID: 21001 RVA: 0x0010264B File Offset: 0x0010084B
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IPixelBounded.PixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.ApparentPixelBounds;
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x0600520A RID: 21002 RVA: 0x0010264B File Offset: 0x0010084B
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.ApparentPixelBounds;
			}
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x0600520B RID: 21003 RVA: 0x00102653 File Offset: 0x00100853
		public bool IsBoundsFromBorders { get; }

		// Token: 0x0600520C RID: 21004 RVA: 0x0010265C File Offset: 0x0010085C
		public Cell([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> apparentPixelBounds, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> scriptsInclusiveBounds, string content, IReadOnlyList<IWord> children, IReadOnlyList<ITextRun> textRuns, LogicalGlyphOrdering logicalGlyphOrdering, [Nullable(2)] FontCharacteristics font = null, bool isBoundsFromBorders = false)
		{
			this.ApparentPixelBounds = apparentPixelBounds;
			this.ScriptsInclusiveBounds = scriptsInclusiveBounds;
			this.Content = content;
			this.Children = children;
			this.Font = font;
			this.TextRuns = textRuns;
			this.LogicalGlyphOrdering = logicalGlyphOrdering;
			this.IsBoundsFromBorders = isBoundsFromBorders;
			this.Alignments = new AxisAlignedSet<Alignment<ICell>>();
			this.ContiguousLists = new AxisAlignedSet<ContiguousList<ICell>>();
			this.BoundedLists = new AxisAlignedSet<IBoundedList<ICell>>();
			IWord word = this.Children.FirstOrDefault<IWord>();
			this.BeforeAlignmentDotRow = ((word != null) ? word.BeforeAlignmentDotRow : null);
			IWord word2 = this.Children.LastOrDefault<IWord>();
			this.AfterAlignmentDotRow = ((word2 != null) ? word2.AfterAlignmentDotRow : null);
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x00102707 File Offset: 0x00100907
		public void AddToAlignment(Alignment<ICell> alignment)
		{
			this.Alignments[alignment.Axis].Add(alignment);
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x00102721 File Offset: 0x00100921
		public void AddToContiguousList(ContiguousList<ICell> list)
		{
			this.ContiguousLists[list.BaseAlignment.Axis].Add(list);
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x00102740 File Offset: 0x00100940
		public void AddToBoundedList(IBoundedList<ICell> list)
		{
			this.BoundedLists[list.Axis].Add(list);
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x0010275C File Offset: 0x0010095C
		public ICell Join(ICell other, bool newLine, bool includeSpace = true)
		{
			Bounds<PixelUnit> bounds = this.ApparentPixelBounds.Join(other.ApparentPixelBounds);
			Bounds<PixelUnit> bounds2 = this.ScriptsInclusiveBounds.Join(other.ScriptsInclusiveBounds);
			string text = (includeSpace ? (this.Content + (newLine ? "\n" : " ") + other.Content) : (this.Content + other.Content));
			IReadOnlyList<IWord> readOnlyList = this.Children.Concat(other.Children).ToList<IWord>();
			return new Cell(bounds, bounds2, text, readOnlyList, this.TextRuns.Concat(other.TextRuns).ToList<ITextRun>(), newLine ? new LogicalGlyphOrdering(this.LogicalGlyphOrdering.DirectLines.Concat(other.LogicalGlyphOrdering.DirectLines).ToList<LogicalGlyphOrderingLine>()) : new LogicalGlyphOrdering(this.LogicalGlyphOrdering.DirectLines.Take(this.LogicalGlyphOrdering.DirectLines.Count - 1).AppendItem(this.LogicalGlyphOrdering.DirectLines.Last<LogicalGlyphOrderingLine>().Join(other.LogicalGlyphOrdering.DirectLines[0])).Concat(other.LogicalGlyphOrdering.DirectLines.Skip(1))
				.ToList<LogicalGlyphOrderingLine>()), this.Font, false);
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x0010289C File Offset: 0x00100A9C
		public ICell WithBounds([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, bool fromBorders)
		{
			return new Cell(bounds, this.ScriptsInclusiveBounds, this.Content, this.Children, this.TextRuns, this.LogicalGlyphOrdering, this.Font, fromBorders);
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x001028C9 File Offset: 0x00100AC9
		ICell IWordAmalgamation<ICell>.CombineWithOverlappingCellInTable(ICell cell)
		{
			return CellBuilder.BuildCellFromUnsortedTextRunGroup(this.TextRuns.Concat(cell.TextRuns));
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x001028E4 File Offset: 0x00100AE4
		[return: Nullable(new byte[] { 0, 0, 2, 2 })]
		Optional<Record<ICell, ICell>> IWordAmalgamation<ICell>.SplitOnBoundary(Axis axis, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> boundary)
		{
			if (boundary.Max > this.ApparentPixelBounds[axis.IncreasingDirection()])
			{
				return new Record<ICell, ICell>(this, null).Some<Record<ICell, ICell>>();
			}
			if (boundary.Min < this.ApparentPixelBounds[axis.DecreasingDirection()])
			{
				return new Record<ICell, ICell>(null, this).Some<Record<ICell, ICell>>();
			}
			Optional<IEnumerable<Record<ITextRun, ITextRun>>> optional = this.TextRuns.Select((ITextRun textRun) => textRun.SplitOnBoundary(axis, boundary)).WholeSequenceOfValues<Record<ITextRun, ITextRun>>();
			if (!optional.HasValue)
			{
				return Optional<Record<ICell, ICell>>.Nothing;
			}
			IReadOnlyList<ITextRun> readOnlyList;
			IReadOnlyList<ITextRun> readOnlyList2;
			optional.Value.UnzipToLists<ITextRun, ITextRun>().Deconstruct(out readOnlyList, out readOnlyList2);
			IReadOnlyList<ITextRun> readOnlyList3 = readOnlyList;
			IReadOnlyList<ITextRun> readOnlyList4 = readOnlyList2;
			return Record.Create<ICell, ICell>(Cell.<Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures.IWordAmalgamation<Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures.ICell>.SplitOnBoundary>g__FromTextRuns|55_1(readOnlyList3), Cell.<Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures.IWordAmalgamation<Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures.ICell>.SplitOnBoundary>g__FromTextRuns|55_1(readOnlyList4)).Some<Record<ICell, ICell>>();
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x001029C6 File Offset: 0x00100BC6
		public override string ToString()
		{
			return string.Format("{{ Content: {0}, bounds: {1} }}", this.Content.ToLiteral(null), this.ApparentPixelBounds);
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x001029E9 File Offset: 0x00100BE9
		public string MinimalToString()
		{
			return this.Content.ToLiteral(null);
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x001029F8 File Offset: 0x00100BF8
		public bool Equals(ICell cell)
		{
			return this.ApparentPixelBounds.Equals(cell.ApparentPixelBounds) && string.Equals(this.Content, cell.Content) && this.Children.SequenceEqual(cell.Children) && object.Equals(this.Font, cell.Font);
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x00102A54 File Offset: 0x00100C54
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((ICell)obj)));
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x00102A84 File Offset: 0x00100C84
		public override int GetHashCode()
		{
			if (!this._hashcodeInitialized)
			{
				int num = 547763164;
				num = num * -1521134295 + this.ApparentPixelBounds.GetHashCode();
				num = num * -1521134295 + this.Content.GetHashCode();
				num = num * -1521134295 + this.Children.OrderDependentHashCode<IWord>();
				int num2 = num * -1521134295;
				FontCharacteristics font = this.Font;
				num = num2 + ((font != null) ? font.GetHashCode() : 0);
				this._hashcode = num;
				this._hashcodeInitialized = true;
			}
			return this._hashcode;
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x00102B13 File Offset: 0x00100D13
		[NullableContext(2)]
		[CompilerGenerated]
		internal static ICell <Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures.IWordAmalgamation<Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures.ICell>.SplitOnBoundary>g__FromTextRuns|55_1(IReadOnlyList<ITextRun> textRuns)
		{
			if (textRuns == null || !textRuns.Collect<ITextRun>().Any<ITextRun>())
			{
				return null;
			}
			return CellBuilder.BuildCellFromUnsortedTextRunGroup(textRuns.Collect<ITextRun>().ToList<ITextRun>());
		}

		// Token: 0x0400249E RID: 9374
		[Nullable(new byte[] { 0, 1 })]
		private Bounds<PixelUnit>? _stablePixelBounds;

		// Token: 0x040024A2 RID: 9378
		private bool _hashcodeInitialized;

		// Token: 0x040024A3 RID: 9379
		private int _hashcode;
	}
}
