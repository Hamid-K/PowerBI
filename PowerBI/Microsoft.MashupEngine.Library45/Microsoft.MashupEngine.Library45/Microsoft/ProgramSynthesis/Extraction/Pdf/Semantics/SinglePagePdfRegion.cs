using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;
using Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics
{
	// Token: 0x02000C10 RID: 3088
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class SinglePagePdfRegion : PdfRegion
	{
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06004FD0 RID: 20432 RVA: 0x000FB50A File Offset: 0x000F970A
		internal DependencyGraph PageData { get; }

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06004FD1 RID: 20433 RVA: 0x000FB512 File Offset: 0x000F9712
		public int PageIndex
		{
			get
			{
				return this.PageData.GetPageIndex();
			}
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x000FB51F File Offset: 0x000F971F
		internal SinglePagePdfRegion(ILoadedPdf pdf, DependencyGraph pageData)
			: base(pdf)
		{
			this.PageData = pageData;
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x000FB52F File Offset: 0x000F972F
		public bool SamePageAs(SinglePagePdfRegion other)
		{
			return base.SamePdfAs(other) && this.PageIndex == other.PageIndex;
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x000FB54A File Offset: 0x000F974A
		internal BoundsOnPdfPage GetBoundsOnSamePage([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds)
		{
			return new BoundsOnPdfPage(base.Pdf, this.PageData, bounds);
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x000FB560 File Offset: 0x000F9760
		public override bool Equals(PdfRegion other)
		{
			SinglePagePdfRegion singlePagePdfRegion = other as SinglePagePdfRegion;
			return singlePagePdfRegion != null && this.SamePageAs(singlePagePdfRegion);
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x000FB580 File Offset: 0x000F9780
		public override int GetHashCode()
		{
			return (23 * base.GetHashCode()) ^ this.PageIndex.GetHashCode();
		}
	}
}
