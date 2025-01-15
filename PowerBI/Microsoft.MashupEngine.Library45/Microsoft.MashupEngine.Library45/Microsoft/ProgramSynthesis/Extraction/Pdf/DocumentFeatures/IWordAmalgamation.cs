using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CD1 RID: 3281
	[NullableContext(1)]
	public interface IWordAmalgamation<T> : IApparentPixelBounded, IPixelBounded, IBounded<PixelUnit> where T : class, IWordAmalgamation<T>
	{
		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06005450 RID: 21584
		string Content { get; }

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06005451 RID: 21585
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> ScriptsInclusiveBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06005452 RID: 21586
		IReadOnlyList<IWord> Children { get; }

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06005453 RID: 21587
		LogicalGlyphOrdering LogicalGlyphOrdering { get; }

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06005454 RID: 21588
		IReadOnlyList<ITextRun> TextRuns { get; }

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06005455 RID: 21589
		[Nullable(2)]
		FontCharacteristics Font
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06005456 RID: 21590
		AxisAlignedSet<Alignment<T>> Alignments { get; }

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06005457 RID: 21591
		AxisAlignedSet<ContiguousList<T>> ContiguousLists { get; }

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06005458 RID: 21592
		AxisAlignedSet<IBoundedList<T>> BoundedLists { get; }

		// Token: 0x06005459 RID: 21593
		void AddToAlignment(Alignment<T> alignment);

		// Token: 0x0600545A RID: 21594
		void AddToContiguousList(ContiguousList<T> contiguousList);

		// Token: 0x0600545B RID: 21595
		void AddToBoundedList(IBoundedList<T> list);

		// Token: 0x0600545C RID: 21596
		string MinimalToString();

		// Token: 0x0600545D RID: 21597
		T CombineWithOverlappingCellInTable(T cell);

		// Token: 0x0600545E RID: 21598
		[return: Nullable(new byte[] { 0, 0, 2, 2 })]
		Optional<Record<T, T>> SplitOnBoundary(Axis axis, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> boundary);

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x0600545F RID: 21599
		bool IsBoundsFromBorders { get; }

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06005460 RID: 21600
		[Nullable(2)]
		AlignmentDotCollection.AlignmentDotRow BeforeAlignmentDotRow
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06005461 RID: 21601
		[Nullable(2)]
		AlignmentDotCollection.AlignmentDotRow AfterAlignmentDotRow
		{
			[NullableContext(2)]
			get;
		}
	}
}
