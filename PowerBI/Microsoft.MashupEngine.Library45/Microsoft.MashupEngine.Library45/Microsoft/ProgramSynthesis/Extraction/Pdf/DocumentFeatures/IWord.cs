using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D9F RID: 3487
	[NullableContext(1)]
	public interface IWord : IEquatable<IWord>, IRotatedPixelBounded, IApparentPixelBounded
	{
		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x060058BB RID: 22715
		string Content { get; }

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x060058BC RID: 22716
		string ContentRtl { get; }

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x060058BD RID: 22717
		[Nullable(2)]
		FontCharacteristics Font
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x060058BE RID: 22718
		IReadOnlyList<Glyph> Children { get; }

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x060058BF RID: 22719
		bool? HasSpaceBefore { get; }

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x060058C0 RID: 22720
		bool IsWhitespace { get; }

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x060058C1 RID: 22721
		bool IsSymbol { get; }

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x060058C2 RID: 22722
		bool IsImage { get; }

		// Token: 0x060058C3 RID: 22723
		double AverageGlyphWidth();

		// Token: 0x060058C4 RID: 22724
		string MinimalToString();

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x060058C5 RID: 22725
		[Nullable(new byte[] { 0, 1 })]
		Range<PixelUnit> BasicVerticalBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x060058C6 RID: 22726
		int BaseLine { get; }

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x060058C7 RID: 22727
		bool MayBeOverlay { get; }

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x060058C8 RID: 22728
		// (set) Token: 0x060058C9 RID: 22729
		bool IsSuperscriptOrSubscript { get; set; }

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x060058CA RID: 22730
		// (set) Token: 0x060058CB RID: 22731
		bool IsBackground { get; set; }

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x060058CC RID: 22732
		[Nullable(2)]
		AlignmentDotCollection.AlignmentDotRow BeforeAlignmentDotRow
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x060058CD RID: 22733
		// (set) Token: 0x060058CE RID: 22734
		[Nullable(2)]
		AlignmentDotCollection.AlignmentDotRow AfterAlignmentDotRow
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x060058CF RID: 22735
		TextDirection TextDirection { get; }

		// Token: 0x060058D0 RID: 22736
		[return: Nullable(new byte[] { 0, 0, 2, 2 })]
		Optional<Record<IWord, IWord>> SplitOnBoundary(Axis axis, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> boundary);
	}
}
