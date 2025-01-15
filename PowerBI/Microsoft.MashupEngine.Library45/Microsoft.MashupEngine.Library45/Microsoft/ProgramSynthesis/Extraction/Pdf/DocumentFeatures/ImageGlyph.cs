using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CCD RID: 3277
	internal class ImageGlyph : Glyph
	{
		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x0600543D RID: 21565 RVA: 0x0000FA11 File Offset: 0x0000DC11
		internal override bool IsLetterOrNumber
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x001095B8 File Offset: 0x001077B8
		[NullableContext(1)]
		public ImageGlyph([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds, [Nullable(2)] TransformationMatrix transformation, int renderingOrder, PdfAnalyzerOptions options)
			: base(bounds, options.ImageString, null, null, renderingOrder, null, BidiUnicodeCategory.Unknown)
		{
		}
	}
}
