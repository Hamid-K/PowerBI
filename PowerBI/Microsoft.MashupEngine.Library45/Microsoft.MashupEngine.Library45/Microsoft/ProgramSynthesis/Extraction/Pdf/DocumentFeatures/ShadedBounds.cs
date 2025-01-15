using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D73 RID: 3443
	public class ShadedBounds : IPixelBounded, IBounded<PixelUnit>
	{
		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x060057C9 RID: 22473 RVA: 0x00117070 File Offset: 0x00115270
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.PixelBounds;
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x060057CA RID: 22474 RVA: 0x00117078 File Offset: 0x00115278
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> PixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x060057CB RID: 22475 RVA: 0x00117080 File Offset: 0x00115280
		public Color ShadingColor { get; }

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x060057CC RID: 22476 RVA: 0x00117088 File Offset: 0x00115288
		[Nullable(new byte[] { 0, 1 })]
		public Range<IndexUnit> RenderingOrders
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x00117090 File Offset: 0x00115290
		public ShadedBounds([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pixelBounds, Color shadingColor, [Nullable(new byte[] { 0, 1 })] Range<IndexUnit> renderingOrders)
		{
			this.PixelBounds = pixelBounds;
			this.ShadingColor = shadingColor;
			this.RenderingOrders = renderingOrders;
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x001170AD File Offset: 0x001152AD
		[NullableContext(1)]
		public override string ToString()
		{
			return string.Format("ShadedBounds({0}, {1})", this.PixelBounds, this.ShadingColor);
		}
	}
}
