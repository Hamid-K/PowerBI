using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CCC RID: 3276
	[NullableContext(1)]
	[Nullable(0)]
	public class Image : IPixelBounded, IBounded<PixelUnit>
	{
		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06005435 RID: 21557 RVA: 0x001094A2 File Offset: 0x001076A2
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.PixelBounds;
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06005436 RID: 21558 RVA: 0x001094AA File Offset: 0x001076AA
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> PixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06005437 RID: 21559 RVA: 0x001094B2 File Offset: 0x001076B2
		public int RenderingOrder { get; }

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06005438 RID: 21560 RVA: 0x001094BA File Offset: 0x001076BA
		[Nullable(2)]
		public TransformationMatrix Transformation
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x001094C2 File Offset: 0x001076C2
		internal ImageGlyph AsGlyph(PdfAnalyzerOptions options)
		{
			return new ImageGlyph(this.PixelBounds, this.Transformation, this.RenderingOrder, options);
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x0600543A RID: 21562 RVA: 0x001094DC File Offset: 0x001076DC
		internal Separator AsSeparator
		{
			get
			{
				Axis axis = ((this.PixelBounds.Width() >= this.PixelBounds.Height()) ? Axis.Horizontal : Axis.Vertical);
				return new Separator(new AxisAlignedLine<PixelUnit>(axis, this.PixelBounds[axis], (int)this.PixelBounds[axis.Perpendicular()].Center()), null, null, this.PixelBounds[axis.Perpendicular()].Size());
			}
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x00109571 File Offset: 0x00107771
		[NullableContext(2)]
		public Image([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, int renderingOrder, TransformationMatrix transformation)
		{
			this.PixelBounds = bounds;
			this.RenderingOrder = renderingOrder;
			this.Transformation = transformation;
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x0010958E File Offset: 0x0010778E
		public override string ToString()
		{
			return string.Format("Image({0}, RenderingOrder={1}, Transformation={2})", this.PixelBounds, this.RenderingOrder, this.Transformation);
		}
	}
}
