using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;
using Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics
{
	// Token: 0x02000C12 RID: 3090
	[NullableContext(1)]
	[Nullable(0)]
	public class BoundsOnPdfPage : SinglePagePdfRegion
	{
		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06004FD9 RID: 20441 RVA: 0x000FB5C6 File Offset: 0x000F97C6
		[Nullable(new byte[] { 0, 1 })]
		internal Bounds<PixelUnit> Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x000FB5CE File Offset: 0x000F97CE
		internal BoundsOnPdfPage(ILoadedPdf pdf, DependencyGraph pageData, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds)
			: base(pdf, pageData)
		{
			this.Bounds = bounds;
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x000FB5DF File Offset: 0x000F97DF
		public override string ToString()
		{
			return string.Format("BoundsOnPdfPage(PageIndex={0}, Bounds={1})", base.PageIndex, this.Bounds);
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x000FB604 File Offset: 0x000F9804
		public override bool Equals(PdfRegion other)
		{
			BoundsOnPdfPage boundsOnPdfPage = other as BoundsOnPdfPage;
			return boundsOnPdfPage != null && boundsOnPdfPage.Bounds.Equals(this.Bounds) && base.Equals(other);
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x000FB63C File Offset: 0x000F983C
		public override int GetHashCode()
		{
			return (19 * base.GetHashCode()) ^ this.Bounds.GetHashCode();
		}
	}
}
