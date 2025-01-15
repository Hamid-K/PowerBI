using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000DA0 RID: 3488
	[NullableContext(1)]
	[Nullable(0)]
	internal class Word : IWord, IEquatable<IWord>, IRotatedPixelBounded, IApparentPixelBounded
	{
		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x060058D1 RID: 22737 RVA: 0x0011A367 File Offset: 0x00118567
		public string Content { get; }

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x060058D2 RID: 22738 RVA: 0x0011A36F File Offset: 0x0011856F
		public string ContentRtl { get; }

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x060058D3 RID: 22739 RVA: 0x0011A377 File Offset: 0x00118577
		[Nullable(2)]
		public FontCharacteristics Font
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x060058D4 RID: 22740 RVA: 0x0011A37F File Offset: 0x0011857F
		public IReadOnlyList<Glyph> Children { get; }

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x060058D5 RID: 22741 RVA: 0x0011A387 File Offset: 0x00118587
		public bool? HasSpaceBefore { get; }

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x060058D6 RID: 22742 RVA: 0x0011A38F File Offset: 0x0011858F
		[Nullable(2)]
		public AlignmentDotCollection.AlignmentDotRow BeforeAlignmentDotRow
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x060058D7 RID: 22743 RVA: 0x0011A397 File Offset: 0x00118597
		// (set) Token: 0x060058D8 RID: 22744 RVA: 0x0011A39F File Offset: 0x0011859F
		[Nullable(2)]
		public AlignmentDotCollection.AlignmentDotRow AfterAlignmentDotRow
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x060058D9 RID: 22745 RVA: 0x0011A3A8 File Offset: 0x001185A8
		public bool IsWhitespace { get; }

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x060058DA RID: 22746 RVA: 0x0011A3B0 File Offset: 0x001185B0
		public bool IsSymbol { get; }

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x060058DB RID: 22747 RVA: 0x0011A3B8 File Offset: 0x001185B8
		public bool IsImage { get; }

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x060058DC RID: 22748 RVA: 0x0011A3C0 File Offset: 0x001185C0
		[Nullable(new byte[] { 0, 1 })]
		public Range<PixelUnit> BasicVerticalBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.ApparentPixelBoundsWithoutRotation.Vertical;
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x060058DD RID: 22749 RVA: 0x0011A3DC File Offset: 0x001185DC
		public int BaseLine
		{
			get
			{
				return this.BasicVerticalBounds.Max;
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x060058DE RID: 22750 RVA: 0x0011A3F7 File Offset: 0x001185F7
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> StablePixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x060058DF RID: 22751 RVA: 0x0011A3FF File Offset: 0x001185FF
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> ApparentPixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x060058E0 RID: 22752 RVA: 0x0011A407 File Offset: 0x00118607
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> ApparentPixelBoundsWithoutRotation
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x060058E1 RID: 22753 RVA: 0x0011A410 File Offset: 0x00118610
		public float? RotationAngle
		{
			get
			{
				Glyph glyph = this.Children.FirstOrDefault<Glyph>();
				if (glyph == null)
				{
					return null;
				}
				return glyph.RotationAngle;
			}
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x060058E2 RID: 22754 RVA: 0x0011A43B File Offset: 0x0011863B
		public bool IsRotated
		{
			get
			{
				Glyph glyph = this.Children.FirstOrDefault<Glyph>();
				return glyph != null && glyph.IsRotated;
			}
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x060058E3 RID: 22755 RVA: 0x0011A453 File Offset: 0x00118653
		public bool IsRotatedByRightAngle
		{
			get
			{
				Glyph glyph = this.Children.FirstOrDefault<Glyph>();
				return glyph != null && glyph.IsRotatedByRightAngle;
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x060058E4 RID: 22756 RVA: 0x0011A46C File Offset: 0x0011866C
		public bool IsRotatedByEvenRightAngle
		{
			get
			{
				Glyph glyph = this.Children.FirstOrDefault<Glyph>();
				bool? flag;
				if (glyph == null)
				{
					flag = null;
				}
				else
				{
					TransformationMatrix transformationMatrix = glyph.TransformationMatrix;
					flag = ((transformationMatrix != null) ? new bool?(transformationMatrix.IsRotatedByEvenRightAngle) : null);
				}
				bool? flag2 = flag;
				return flag2.GetValueOrDefault();
			}
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x060058E5 RID: 22757 RVA: 0x0011A4B9 File Offset: 0x001186B9
		// (set) Token: 0x060058E6 RID: 22758 RVA: 0x0011A4C1 File Offset: 0x001186C1
		public bool IsSuperscriptOrSubscript { get; set; }

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x060058E7 RID: 22759 RVA: 0x0011A4CA File Offset: 0x001186CA
		// (set) Token: 0x060058E8 RID: 22760 RVA: 0x0011A4D2 File Offset: 0x001186D2
		public bool IsBackground { get; set; }

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x060058E9 RID: 22761 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public bool MayBeOverlay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x060058EA RID: 22762 RVA: 0x0011A4DB File Offset: 0x001186DB
		public TextDirection TextDirection { get; }

		// Token: 0x060058EB RID: 22763 RVA: 0x0011A4E4 File Offset: 0x001186E4
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static Optional<Word> MaybeCreate([Nullable(1)] IReadOnlyList<Glyph> children, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds, bool? hasSpaceBefore, TextDirection textDirection, FontCharacteristics font = null, AlignmentDotCollection.AlignmentDotRow beforeAlignmentDotRow = null, AlignmentDotCollection.AlignmentDotRow afterAlignmentDotRow = null)
		{
			if (children == null || children.Count == 0)
			{
				throw new ArgumentException("Children must not be null or empty.", "children");
			}
			IReadOnlyList<Glyph> readOnlyList = children.Where((Glyph child) => child.ApparentPixelBounds.Overlaps(pageBounds)).ToList<Glyph>();
			if (readOnlyList.IsEmpty<Glyph>())
			{
				return Optional<Word>.Nothing;
			}
			Bounds<PixelUnit> value = Bounds<PixelUnit>.Join(readOnlyList.Select((Glyph g) => g.ApparentPixelBounds)).Intersect(pageBounds).Value;
			Bounds<PixelUnit> value2 = Bounds<PixelUnit>.Join(readOnlyList.Select((Glyph g) => g.StablePixelBounds)).Intersect(pageBounds).Value;
			return new Word(readOnlyList, value, value2, hasSpaceBefore, textDirection, font, beforeAlignmentDotRow, afterAlignmentDotRow).Some<Word>();
		}

		// Token: 0x060058EC RID: 22764 RVA: 0x0011A5E0 File Offset: 0x001187E0
		[NullableContext(2)]
		private Word([Nullable(1)] IReadOnlyList<Glyph> children, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> apparentPixelBounds, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> stablePixelBounds, bool? hasSpaceBefore, TextDirection textDirection, FontCharacteristics font = null, AlignmentDotCollection.AlignmentDotRow beforeAlignmentDotRow = null, AlignmentDotCollection.AlignmentDotRow afterAlignmentDotRow = null)
		{
			this.Children = children;
			this.StablePixelBounds = stablePixelBounds;
			this.ApparentPixelBounds = apparentPixelBounds;
			Bounds<PixelUnit> bounds;
			if (!this.IsRotated)
			{
				bounds = this.ApparentPixelBounds;
			}
			else
			{
				bounds = Bounds<PixelUnit>.Join(this.Children.Select((Glyph g) => g.ApparentPixelBoundsWithoutRotation));
			}
			this.ApparentPixelBoundsWithoutRotation = bounds;
			this.Content = string.Concat(children.Select((Glyph child) => child.Text));
			string text;
			if (textDirection != TextDirection.LeftToRight && !children[0].BidiCategory.IsNumberCategory())
			{
				text = string.Concat(from child in children.Reverse<Glyph>()
					select child.Text);
			}
			else
			{
				text = this.Content;
			}
			this.ContentRtl = text;
			this.IsWhitespace = string.IsNullOrWhiteSpace(this.Content);
			this.Font = font;
			this.HasSpaceBefore = hasSpaceBefore;
			this.TextDirection = textDirection;
			this.BeforeAlignmentDotRow = beforeAlignmentDotRow;
			this.AfterAlignmentDotRow = afterAlignmentDotRow;
			this.IsSymbol = children.All((Glyph g) => !g.IsLetterOrNumber);
			this.IsImage = children.OfType<ImageGlyph>().Any<ImageGlyph>();
		}

		// Token: 0x060058ED RID: 22765 RVA: 0x0011A747 File Offset: 0x00118947
		public double AverageGlyphWidth()
		{
			return this.Children.Average((Glyph glyph) => glyph.ApparentPixelBoundsWithoutRotation.Width());
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x0011A774 File Offset: 0x00118974
		[return: Nullable(new byte[] { 0, 0, 2, 2 })]
		public Optional<Record<IWord, IWord>> SplitOnBoundary(Axis axis, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> boundary)
		{
			if (this.Children.Count <= 1)
			{
				if (boundary.Min >= this.ApparentPixelBounds[axis].Min)
				{
					return new Record<IWord, IWord>(this, null).Some<Record<IWord, IWord>>();
				}
				return new Record<IWord, IWord>(null, this).Some<Record<IWord, IWord>>();
			}
			else
			{
				if (boundary.Min < this.ApparentPixelBounds[axis].Min)
				{
					return new Record<IWord, IWord>(null, this).Some<Record<IWord, IWord>>();
				}
				if (boundary.Max > this.ApparentPixelBounds[axis].Max)
				{
					return new Record<IWord, IWord>(this, null).Some<Record<IWord, IWord>>();
				}
				if (this.IsRotated && !this.IsRotatedByRightAngle)
				{
					return Optional<Record<IWord, IWord>>.Nothing;
				}
				if (axis == Axis.Vertical && this.IsRotatedByRightAngle && this.IsRotatedByEvenRightAngle)
				{
					return Optional<Record<IWord, IWord>>.Nothing;
				}
				Func<Range<PixelUnit>, Optional<Range<PixelUnit>>> <>9__7;
				IReadOnlyList<Range<PixelUnit>> readOnlyList = (from pair in this.Children.Windowed<Glyph>()
					group pair by pair.Item1.ApparentPixelBounds[axis].Distance(pair.Item2.ApparentPixelBounds[axis])).ArgMax((IGrouping<int, Record<Glyph, Glyph>> g) => g.Key).Select2(delegate(Glyph a, Glyph b)
				{
					Optional<Range<PixelUnit>> optional = a.ApparentPixelBounds[axis].BetweenExclusive(b.ApparentPixelBounds[axis]);
					Func<Range<PixelUnit>, Optional<Range<PixelUnit>>> func;
					if ((func = <>9__7) == null)
					{
						func = (<>9__7 = (Range<PixelUnit> between) => between.Intersect(boundary));
					}
					return optional.SelectMany(func);
				}).SelectValues<Range<PixelUnit>>()
					.ToList<Range<PixelUnit>>();
				if (!readOnlyList.Any<Range<PixelUnit>>())
				{
					return Optional<Record<IWord, IWord>>.Nothing;
				}
				Range<PixelUnit> adjustedBoundary = readOnlyList.ArgMax((Range<PixelUnit> r) => r.Size());
				IList<Glyph> before;
				IList<Glyph> after;
				this.Children.PartitionByPredicate((Glyph glyph) => glyph.ApparentPixelBounds[axis].IsAfter(adjustedBoundary, false), out after, out before);
				return new Record<IWord, IWord>(before.Any<Glyph>().Then(delegate
				{
					IReadOnlyList<Glyph> readOnlyList2 = before.ToList<Glyph>();
					Bounds<PixelUnit> stablePixelBounds = this.StablePixelBounds;
					FontCharacteristics font = this.Font;
					bool? hasSpaceBefore = this.HasSpaceBefore;
					TextDirection textDirection;
					if (this.TextDirection.IsStrong())
					{
						if (before.Any((Glyph g) => g.BidiCategory.IsStrong()))
						{
							textDirection = this.TextDirection;
							goto IL_0079;
						}
					}
					textDirection = TextDirection.Neutral;
					IL_0079:
					return Word.MaybeCreate(readOnlyList2, stablePixelBounds, hasSpaceBefore, textDirection, font, this.BeforeAlignmentDotRow, null);
				}).OrElseDefault<Optional<Word>>()
					.OrElseDefault<Word>(), after.Any<Glyph>().Then(delegate
				{
					IReadOnlyList<Glyph> readOnlyList3 = after.ToList<Glyph>();
					Bounds<PixelUnit> stablePixelBounds2 = this.StablePixelBounds;
					FontCharacteristics font2 = this.Font;
					bool? flag = new bool?(true);
					TextDirection textDirection2;
					if (this.TextDirection.IsStrong())
					{
						if (after.Any((Glyph g) => g.BidiCategory.IsStrong()))
						{
							textDirection2 = this.TextDirection;
							goto IL_0074;
						}
					}
					textDirection2 = TextDirection.Neutral;
					IL_0074:
					return Word.MaybeCreate(readOnlyList3, stablePixelBounds2, flag, textDirection2, font2, null, this.AfterAlignmentDotRow);
				}).OrElseDefault<Optional<Word>>()
					.OrElseDefault<Word>()).Some<Record<IWord, IWord>>();
			}
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x0011A99C File Offset: 0x00118B9C
		public override string ToString()
		{
			return string.Format("{{ Content: {0}, Bounds: {1} }}", this.Content.ToLiteral(null), this.ApparentPixelBounds);
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x0011A9BF File Offset: 0x00118BBF
		public string MinimalToString()
		{
			return this.Content.ToLiteral(null);
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x0011A9D0 File Offset: 0x00118BD0
		public bool Equals(IWord other)
		{
			return other != null && object.Equals(this.ApparentPixelBounds, other.ApparentPixelBounds) && string.Equals(this.Content, other.Content) && object.Equals(this.Font, other.Font);
		}

		// Token: 0x060058F2 RID: 22770 RVA: 0x0011AA23 File Offset: 0x00118C23
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((IWord)obj)));
		}

		// Token: 0x060058F3 RID: 22771 RVA: 0x0011AA54 File Offset: 0x00118C54
		public override int GetHashCode()
		{
			return (((this.ApparentPixelBounds.GetHashCode() * 397) ^ this.Content.GetHashCode()) * 397) ^ ((this.Font != null) ? this.Font.GetHashCode() : 0);
		}
	}
}
