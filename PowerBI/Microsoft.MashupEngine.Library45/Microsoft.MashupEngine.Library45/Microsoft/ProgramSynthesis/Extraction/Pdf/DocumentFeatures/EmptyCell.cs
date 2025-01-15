using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CB4 RID: 3252
	[NullableContext(1)]
	[Nullable(0)]
	internal class EmptyCell : ICell, IWordAmalgamation<ICell>, IApparentPixelBounded, IPixelBounded, IBounded<PixelUnit>
	{
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06005396 RID: 21398 RVA: 0x00107DC4 File Offset: 0x00105FC4
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> StablePixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.PixelBounds;
			}
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06005397 RID: 21399 RVA: 0x00107DC4 File Offset: 0x00105FC4
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> ApparentPixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.PixelBounds;
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06005398 RID: 21400 RVA: 0x00107DC4 File Offset: 0x00105FC4
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.PixelBounds;
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06005399 RID: 21401 RVA: 0x00107DCC File Offset: 0x00105FCC
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> PixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x0600539A RID: 21402 RVA: 0x0008E3C3 File Offset: 0x0008C5C3
		public string Content
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x0600539B RID: 21403 RVA: 0x00107DC4 File Offset: 0x00105FC4
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> ScriptsInclusiveBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.PixelBounds;
			}
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x00107DD4 File Offset: 0x00105FD4
		internal EmptyCell([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds)
		{
			this.PixelBounds = bounds;
			this.Alignments = new AxisAlignedSet<Alignment<ICell>>();
			this.ContiguousLists = new AxisAlignedSet<ContiguousList<ICell>>();
			this.BoundedLists = new AxisAlignedSet<IBoundedList<ICell>>();
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x0600539D RID: 21405 RVA: 0x00107E04 File Offset: 0x00106004
		public LogicalGlyphOrdering LogicalGlyphOrdering
		{
			get
			{
				return LogicalGlyphOrdering.Empty;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x0600539E RID: 21406 RVA: 0x00107E0B File Offset: 0x0010600B
		public IReadOnlyList<IWord> Children
		{
			get
			{
				return EmptyCell.EmptyWordArray;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x0600539F RID: 21407 RVA: 0x00107E12 File Offset: 0x00106012
		public IReadOnlyList<ITextRun> TextRuns
		{
			get
			{
				return EmptyCell.EmptyTextRunArray;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x060053A0 RID: 21408 RVA: 0x00002188 File Offset: 0x00000388
		[Nullable(2)]
		public FontCharacteristics Font
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x060053A1 RID: 21409 RVA: 0x00107E19 File Offset: 0x00106019
		public AxisAlignedSet<Alignment<ICell>> Alignments { get; }

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x060053A2 RID: 21410 RVA: 0x00107E21 File Offset: 0x00106021
		public AxisAlignedSet<ContiguousList<ICell>> ContiguousLists { get; }

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x060053A3 RID: 21411 RVA: 0x00107E29 File Offset: 0x00106029
		public AxisAlignedSet<IBoundedList<ICell>> BoundedLists { get; }

		// Token: 0x060053A4 RID: 21412 RVA: 0x00107E31 File Offset: 0x00106031
		public void AddToAlignment(Alignment<ICell> alignment)
		{
			this.Alignments[alignment.Axis].Add(alignment);
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x00107E4B File Offset: 0x0010604B
		public void AddToContiguousList(ContiguousList<ICell> list)
		{
			this.ContiguousLists[list.BaseAlignment.Axis].Add(list);
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x00107E6A File Offset: 0x0010606A
		public void AddToBoundedList(IBoundedList<ICell> list)
		{
			this.BoundedLists[list.Axis].Add(list);
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x00107E84 File Offset: 0x00106084
		public string MinimalToString()
		{
			return "\"\"";
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x0000E945 File Offset: 0x0000CB45
		public ICell CombineWithOverlappingCellInTable(ICell cell)
		{
			return cell;
		}

		// Token: 0x060053A9 RID: 21417 RVA: 0x00107E8B File Offset: 0x0010608B
		[return: Nullable(new byte[] { 0, 0, 2, 2 })]
		public Optional<Record<ICell, ICell>> SplitOnBoundary(Axis axis, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> boundary)
		{
			return Optional<Record<ICell, ICell>>.Nothing;
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x060053AA RID: 21418 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool IsBoundsFromBorders
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x060053AB RID: 21419 RVA: 0x00002188 File Offset: 0x00000388
		[Nullable(2)]
		public AlignmentDotCollection.AlignmentDotRow BeforeAlignmentDotRow
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x060053AC RID: 21420 RVA: 0x00002188 File Offset: 0x00000388
		[Nullable(2)]
		public AlignmentDotCollection.AlignmentDotRow AfterAlignmentDotRow
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x0000E945 File Offset: 0x0000CB45
		public ICell Join(ICell other, bool newLine, bool includeSpace = true)
		{
			return other;
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x00107E92 File Offset: 0x00106092
		public ICell WithBounds([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, bool fromBorders)
		{
			return new EmptyCell(bounds);
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x00107E9C File Offset: 0x0010609C
		protected bool Equals(EmptyCell other)
		{
			return this.PixelBounds.Equals(other.PixelBounds);
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x00107EBD File Offset: 0x001060BD
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((EmptyCell)obj)));
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x00107EEC File Offset: 0x001060EC
		public override int GetHashCode()
		{
			return this.PixelBounds.GetHashCode();
		}

		// Token: 0x040025B5 RID: 9653
		private static readonly IWord[] EmptyWordArray = new IWord[0];

		// Token: 0x040025B6 RID: 9654
		private static readonly ITextRun[] EmptyTextRunArray = new ITextRun[0];
	}
}
