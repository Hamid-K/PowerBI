using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D79 RID: 3449
	[NullableContext(1)]
	[Nullable(0)]
	internal class TextRun : ITextRun, IWordAmalgamation<ITextRun>, IApparentPixelBounded, IPixelBounded, IBounded<PixelUnit>
	{
		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x060057E5 RID: 22501 RVA: 0x00117326 File Offset: 0x00115526
		public string Content { get; }

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x060057E6 RID: 22502 RVA: 0x0011732E File Offset: 0x0011552E
		public string ContentRtl { get; }

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x060057E7 RID: 22503 RVA: 0x00117336 File Offset: 0x00115536
		public IReadOnlyList<IWord> Children { get; }

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x060057E8 RID: 22504 RVA: 0x0011733E File Offset: 0x0011553E
		public LogicalGlyphOrdering LogicalGlyphOrdering { get; }

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x060057E9 RID: 22505 RVA: 0x00117346 File Offset: 0x00115546
		public LogicalGlyphOrderingLine LogicalGlyphOrderingLine { get; }

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x060057EA RID: 22506 RVA: 0x0011734E File Offset: 0x0011554E
		public LogicalGlyphOrderingLine LogicalGlyphOrderingLineRtl { get; }

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x060057EB RID: 22507 RVA: 0x00117356 File Offset: 0x00115556
		public IReadOnlyList<ITextRun> TextRuns
		{
			get
			{
				return new TextRun[] { this };
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x060057EC RID: 22508 RVA: 0x00117362 File Offset: 0x00115562
		[Nullable(2)]
		public FontCharacteristics Font
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x060057ED RID: 22509 RVA: 0x0011736A File Offset: 0x0011556A
		public AxisAlignedSet<Alignment<ITextRun>> Alignments { get; }

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x060057EE RID: 22510 RVA: 0x00117372 File Offset: 0x00115572
		public AxisAlignedSet<ContiguousList<ITextRun>> ContiguousLists { get; }

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x060057EF RID: 22511 RVA: 0x0011737A File Offset: 0x0011557A
		public AxisAlignedSet<IBoundedList<ITextRun>> BoundedLists { get; }

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x060057F0 RID: 22512 RVA: 0x00117382 File Offset: 0x00115582
		public bool IsRotated { get; }

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x060057F1 RID: 22513 RVA: 0x0011738A File Offset: 0x0011558A
		public bool IsRotatedByRightAngle { get; }

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x060057F2 RID: 22514 RVA: 0x00117392 File Offset: 0x00115592
		[Nullable(2)]
		public AlignmentDotCollection.AlignmentDotRow BeforeAlignmentDotRow
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x060057F3 RID: 22515 RVA: 0x0011739A File Offset: 0x0011559A
		[Nullable(2)]
		public AlignmentDotCollection.AlignmentDotRow AfterAlignmentDotRow
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x060057F4 RID: 22516 RVA: 0x001173A4 File Offset: 0x001155A4
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

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x060057F5 RID: 22517 RVA: 0x00117403 File Offset: 0x00115603
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> ApparentPixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x060057F6 RID: 22518 RVA: 0x0011740B File Offset: 0x0011560B
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> ScriptsInclusiveBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x060057F7 RID: 22519 RVA: 0x00117413 File Offset: 0x00115613
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IPixelBounded.PixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.ApparentPixelBounds;
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x060057F8 RID: 22520 RVA: 0x00117413 File Offset: 0x00115613
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.ApparentPixelBounds;
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x060057F9 RID: 22521 RVA: 0x0011741B File Offset: 0x0011561B
		public IReadOnlyList<PartialTextRun> PartialTextRuns { get; }

		// Token: 0x060057FA RID: 22522 RVA: 0x00117424 File Offset: 0x00115624
		private TextRun([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> scriptsInclusiveBounds, string content, LogicalGlyphOrderingLine children, IReadOnlyList<PartialTextRun> partialTextRuns, string contentRtl, LogicalGlyphOrderingLine childrenRtl, [Nullable(2)] FontCharacteristics font)
		{
			this.ApparentPixelBounds = bounds;
			this.ScriptsInclusiveBounds = scriptsInclusiveBounds;
			this.Content = content;
			this.Children = children.AllWords.Where((IWord word) => !word.IsWhitespace).ToList<IWord>();
			this.ContentRtl = contentRtl;
			LogicalGlyphOrderingLine[] array = new LogicalGlyphOrderingLine[1];
			array[0] = ((children.AllWords.Count((IWord word) => word.TextDirection == TextDirection.RightToLeft) > children.AllWords.Count((IWord word) => word.TextDirection == TextDirection.LeftToRight)) ? childrenRtl : children);
			this.LogicalGlyphOrdering = new LogicalGlyphOrdering(array);
			this.LogicalGlyphOrderingLine = children;
			this.LogicalGlyphOrderingLineRtl = childrenRtl;
			this.PartialTextRuns = partialTextRuns;
			this.Font = font;
			Glyph glyph = children.AllWords.First<IWord>().Children[0];
			this.IsRotated = glyph.IsRotated;
			this.IsRotatedByRightAngle = glyph.IsRotatedByRightAngle;
			this.Alignments = new AxisAlignedSet<Alignment<ITextRun>>();
			this.ContiguousLists = new AxisAlignedSet<ContiguousList<ITextRun>>();
			this.BoundedLists = new AxisAlignedSet<IBoundedList<ITextRun>>();
			IWord word3 = this.Children.FirstOrDefault<IWord>();
			this.BeforeAlignmentDotRow = ((word3 != null) ? word3.BeforeAlignmentDotRow : null);
			IWord word2 = this.Children.LastOrDefault<IWord>();
			this.AfterAlignmentDotRow = ((word2 != null) ? word2.AfterAlignmentDotRow : null);
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x001175A8 File Offset: 0x001157A8
		public TextRun([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> scriptsInclusiveBounds, string content, LogicalGlyphOrderingLine children, PartialTextRun partialTextRun, string contentRtl, LogicalGlyphOrderingLine childrenRtl, [Nullable(2)] FontCharacteristics font)
			: this(bounds, scriptsInclusiveBounds, content, children, new PartialTextRun[] { partialTextRun }, contentRtl, childrenRtl, font)
		{
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x001175D1 File Offset: 0x001157D1
		public void AddToAlignment(Alignment<ITextRun> alignment)
		{
			this.Alignments[alignment.Axis].Add(alignment);
		}

		// Token: 0x060057FD RID: 22525 RVA: 0x001175EB File Offset: 0x001157EB
		public void AddToContiguousList(ContiguousList<ITextRun> list)
		{
			this.ContiguousLists[list.BaseAlignment.Axis].Add(list);
		}

		// Token: 0x060057FE RID: 22526 RVA: 0x0011760A File Offset: 0x0011580A
		public void AddToBoundedList(IBoundedList<ITextRun> list)
		{
			this.BoundedLists[list.Axis].Add(list);
		}

		// Token: 0x060057FF RID: 22527 RVA: 0x00117624 File Offset: 0x00115824
		ITextRun IWordAmalgamation<ITextRun>.CombineWithOverlappingCellInTable(ITextRun other)
		{
			Bounds<PixelUnit> bounds = this.ApparentPixelBounds.Join(other.ApparentPixelBounds);
			Bounds<PixelUnit> bounds2 = this.ScriptsInclusiveBounds.Join(other.ScriptsInclusiveBounds);
			string text = this.Content + " " + other.Content;
			LogicalGlyphOrderingLine logicalGlyphOrderingLine = this.LogicalGlyphOrderingLine.Join(other.LogicalGlyphOrderingLine);
			string text2 = other.ContentRtl + " " + this.ContentRtl;
			LogicalGlyphOrderingLine logicalGlyphOrderingLine2 = other.LogicalGlyphOrderingLineRtl.Join(this.LogicalGlyphOrderingLineRtl);
			Bounds<PixelUnit> bounds3 = bounds2;
			string text3 = text;
			LogicalGlyphOrderingLine logicalGlyphOrderingLine3 = logicalGlyphOrderingLine;
			FontCharacteristics font = this.Font;
			return new TextRun(bounds, bounds3, text3, logicalGlyphOrderingLine3, this.PartialTextRuns.Concat(other.PartialTextRuns).ToList<PartialTextRun>(), text2, logicalGlyphOrderingLine2, font);
		}

		// Token: 0x06005800 RID: 22528 RVA: 0x001176DC File Offset: 0x001158DC
		[return: Nullable(new byte[] { 0, 0, 2, 2 })]
		Optional<Record<ITextRun, ITextRun>> IWordAmalgamation<ITextRun>.SplitOnBoundary(Axis axis, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> boundary)
		{
			if (boundary.Max > this.ApparentPixelBounds[axis.IncreasingDirection()])
			{
				return new Record<ITextRun, ITextRun>(this, null).Some<Record<ITextRun, ITextRun>>();
			}
			if (boundary.Min < this.ApparentPixelBounds[axis.DecreasingDirection()])
			{
				return new Record<ITextRun, ITextRun>(null, this).Some<Record<ITextRun, ITextRun>>();
			}
			Optional<IEnumerable<Record<PartialTextRun, PartialTextRun>>> optional = this.PartialTextRuns.Select((PartialTextRun partialTextRun) => partialTextRun.SplitOnBoundary(axis, boundary)).WholeSequenceOfValues<Record<PartialTextRun, PartialTextRun>>();
			if (!optional.HasValue)
			{
				return Optional<Record<ITextRun, ITextRun>>.Nothing;
			}
			IReadOnlyList<PartialTextRun> readOnlyList;
			IReadOnlyList<PartialTextRun> readOnlyList2;
			optional.Value.UnzipToLists<PartialTextRun, PartialTextRun>().Deconstruct(out readOnlyList, out readOnlyList2);
			IEnumerable<PartialTextRun> enumerable = readOnlyList;
			IReadOnlyList<PartialTextRun> readOnlyList3 = readOnlyList2;
			return Record.Create<ITextRun, ITextRun>(TextRun.<Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures.IWordAmalgamation<Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures.ITextRun>.SplitOnBoundary>g__FromPartialTextRuns|68_1(enumerable), TextRun.<Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures.IWordAmalgamation<Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures.ITextRun>.SplitOnBoundary>g__FromPartialTextRuns|68_1(readOnlyList3)).Some<Record<ITextRun, ITextRun>>();
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06005801 RID: 22529 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public bool IsBoundsFromBorders
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005802 RID: 22530 RVA: 0x001177BE File Offset: 0x001159BE
		public string MinimalToString()
		{
			return this.Content.ToLiteral(null);
		}

		// Token: 0x06005803 RID: 22531 RVA: 0x001177CC File Offset: 0x001159CC
		public override string ToString()
		{
			return string.Format("{{ Content: {0}, bounds: {1} }}", this.Content.ToLiteral(null), this.ApparentPixelBounds);
		}

		// Token: 0x06005804 RID: 22532 RVA: 0x001177F0 File Offset: 0x001159F0
		public bool Equals(ITextRun cell)
		{
			return this.ApparentPixelBounds.Equals(cell.ApparentPixelBounds) && string.Equals(this.Content, cell.Content) && this.Children.SequenceEqual(cell.Children) && object.Equals(this.Font, cell.Font);
		}

		// Token: 0x06005805 RID: 22533 RVA: 0x0011784C File Offset: 0x00115A4C
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((ITextRun)obj)));
		}

		// Token: 0x06005806 RID: 22534 RVA: 0x0011787C File Offset: 0x00115A7C
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

		// Token: 0x06005807 RID: 22535 RVA: 0x0011790C File Offset: 0x00115B0C
		[NullableContext(2)]
		[CompilerGenerated]
		internal static ITextRun <Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures.IWordAmalgamation<Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures.ITextRun>.SplitOnBoundary>g__FromPartialTextRuns|68_1([Nullable(new byte[] { 1, 2 })] IEnumerable<PartialTextRun> partials)
		{
			return partials.Collect<PartialTextRun>().Collect((PartialTextRun partial) => partial.AsTextRun).Aggregate(null, delegate(ITextRun a, ITextRun b)
			{
				if (a != null)
				{
					return a.CombineWithOverlappingCellInTable(b);
				}
				return b;
			});
		}

		// Token: 0x04002899 RID: 10393
		private int _hashcode;

		// Token: 0x0400289A RID: 10394
		private bool _hashcodeInitialized;

		// Token: 0x0400289B RID: 10395
		[Nullable(new byte[] { 0, 1 })]
		private Bounds<PixelUnit>? _stablePixelBounds;
	}
}
