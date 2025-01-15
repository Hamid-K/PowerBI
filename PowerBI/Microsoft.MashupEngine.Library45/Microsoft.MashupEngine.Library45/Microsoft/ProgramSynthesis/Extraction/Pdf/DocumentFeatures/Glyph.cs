using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CB6 RID: 3254
	[NullableContext(2)]
	[Nullable(new byte[] { 0, 0, 1, 1, 2, 2 })]
	public class Glyph : Tuple<Bounds<PixelUnit>, string, FontCharacteristics, TransformationMatrix>, IRotatedPixelBounded, IApparentPixelBounded
	{
		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x060053BC RID: 21436 RVA: 0x0010803D File Offset: 0x0010623D
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> StablePixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x060053BD RID: 21437 RVA: 0x00108045 File Offset: 0x00106245
		[Nullable(1)]
		public string Text
		{
			[NullableContext(1)]
			get
			{
				return base.Item2;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x060053BE RID: 21438 RVA: 0x0010804D File Offset: 0x0010624D
		public FontCharacteristics Font
		{
			get
			{
				return base.Item3;
			}
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x060053BF RID: 21439 RVA: 0x00108055 File Offset: 0x00106255
		public TransformationMatrix TransformationMatrix
		{
			get
			{
				return base.Item4;
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x060053C0 RID: 21440 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public bool MayBeOverlay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x060053C1 RID: 21441 RVA: 0x0010805D File Offset: 0x0010625D
		internal virtual bool IsLetterOrNumber
		{
			get
			{
				IEnumerable<char> text = this.Text;
				Func<char, bool> func;
				if ((func = Glyph.<>O.<0>__IsLetterOrDigit) == null)
				{
					func = (Glyph.<>O.<0>__IsLetterOrDigit = new Func<char, bool>(char.IsLetterOrDigit));
				}
				return text.Any(func);
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x060053C2 RID: 21442 RVA: 0x00108085 File Offset: 0x00106285
		// (set) Token: 0x060053C3 RID: 21443 RVA: 0x0010808D File Offset: 0x0010628D
		internal AlignmentDotCollection.AlignmentDotRow AlignmentDotRow { get; set; }

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x060053C4 RID: 21444 RVA: 0x00108096 File Offset: 0x00106296
		internal bool IsAlignmentDot
		{
			get
			{
				return this.AlignmentDotRow != null;
			}
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x060053C5 RID: 21445 RVA: 0x001080A1 File Offset: 0x001062A1
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> ApparentPixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x060053C6 RID: 21446 RVA: 0x001080A9 File Offset: 0x001062A9
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> ApparentPixelBoundsWithoutRotation
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x060053C7 RID: 21447 RVA: 0x001080B4 File Offset: 0x001062B4
		public float? RotationAngle
		{
			get
			{
				TransformationMatrix transformationMatrix = this.TransformationMatrix;
				if (transformationMatrix == null)
				{
					return null;
				}
				return new float?(transformationMatrix.RotationAngle);
			}
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x060053C8 RID: 21448 RVA: 0x001080DF File Offset: 0x001062DF
		public bool IsRotated
		{
			get
			{
				TransformationMatrix transformationMatrix = this.TransformationMatrix;
				return transformationMatrix != null && transformationMatrix.IsRotated;
			}
		}

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x060053C9 RID: 21449 RVA: 0x001080F2 File Offset: 0x001062F2
		public bool IsRotatedByRightAngle
		{
			get
			{
				TransformationMatrix transformationMatrix = this.TransformationMatrix;
				return transformationMatrix != null && transformationMatrix.IsRotatedByRightAngle;
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x060053CA RID: 21450 RVA: 0x00108105 File Offset: 0x00106305
		public double? TextRunHeight { get; }

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x060053CB RID: 21451 RVA: 0x0010810D File Offset: 0x0010630D
		public BidiUnicodeCategory BidiCategory { get; }

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x060053CC RID: 21452 RVA: 0x00108115 File Offset: 0x00106315
		public int RenderingOrder { get; }

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x060053CD RID: 21453 RVA: 0x00108120 File Offset: 0x00106320
		public Direction? BaseLineEdge
		{
			get
			{
				TransformationMatrix transformationMatrix = this.TransformationMatrix;
				if (transformationMatrix == null)
				{
					return null;
				}
				return transformationMatrix.DownDirection;
			}
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x00108148 File Offset: 0x00106348
		public Glyph([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, [Nullable(1)] string text, FontCharacteristics font, TransformationMatrix transformationMatrix, int renderingOrder, double? textRunHeight = null, BidiUnicodeCategory bidiCategory = BidiUnicodeCategory.Unknown)
			: base(bounds, text, font, transformationMatrix)
		{
			this.RenderingOrder = renderingOrder;
			this.TextRunHeight = textRunHeight;
			this.BidiCategory = bidiCategory;
			if (textRunHeight == null || transformationMatrix == null)
			{
				this.ApparentPixelBoundsWithoutRotation = bounds;
				this.ApparentPixelBounds = bounds;
				return;
			}
			Bounds<PixelUnit> bounds2 = (this.IsRotated ? bounds.Unrotate(transformationMatrix, textRunHeight.Value, false) : bounds);
			int max = transformationMatrix.WithoutRotation.GlyphApparentVerticalRange.Max;
			if (max > bounds2.Top)
			{
				this.ApparentPixelBoundsWithoutRotation = bounds2.With(Direction.Down, max);
			}
			int min = transformationMatrix.WithoutRotation.GlyphApparentVerticalRange.Min;
			if (MathUtils.WithinTolerance(min, bounds2.Top, 5))
			{
				int num = min - (max - bounds2.Bottom);
				if (num < this.ApparentPixelBoundsWithoutRotation.Bottom)
				{
					this.ApparentPixelBoundsWithoutRotation = this.ApparentPixelBoundsWithoutRotation.With(Direction.Up, num);
				}
			}
			this.ApparentPixelBounds = (this.IsRotated ? transformationMatrix.JustRotation.TransformBoundingBox(this.ApparentPixelBoundsWithoutRotation) : this.ApparentPixelBoundsWithoutRotation);
			if (!this.StablePixelBounds.Overlaps(this.ApparentPixelBounds))
			{
				this.ApparentPixelBounds = this.StablePixelBounds;
			}
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x00108288 File Offset: 0x00106488
		[NullableContext(1)]
		public override string ToString()
		{
			string text = "{{\"{0}\", X:{1}, Y:{2}, Size:{3}}}";
			object[] array = new object[4];
			array[0] = this.Text;
			array[1] = this.ApparentPixelBounds.Left;
			array[2] = this.ApparentPixelBounds.Top;
			int num = 3;
			FontCharacteristics font = this.Font;
			array[num] = ((font != null) ? font.FontSize.ToString(CultureInfo.InvariantCulture) : null) ?? "N/A";
			return string.Format(text, array);
		}

		// Token: 0x060053D0 RID: 21456 RVA: 0x00108305 File Offset: 0x00106505
		[NullableContext(1)]
		public string MinimalToString()
		{
			return this.Text;
		}

		// Token: 0x040025C2 RID: 9666
		private const int BoundsTopEpsilon = 5;

		// Token: 0x02000CB7 RID: 3255
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040025C3 RID: 9667
			[Nullable(0)]
			public static Func<char, bool> <0>__IsLetterOrDigit;
		}
	}
}
