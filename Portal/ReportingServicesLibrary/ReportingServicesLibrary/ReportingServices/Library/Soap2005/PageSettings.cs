using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x02000305 RID: 773
	public class PageSettings
	{
		// Token: 0x06001B10 RID: 6928 RVA: 0x0006DD92 File Offset: 0x0006BF92
		public PageSettings()
		{
			this.PaperSize = new ReportPaperSize();
			this.Margins = new ReportMargins();
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0006DDB0 File Offset: 0x0006BFB0
		internal void FromProcessingPageProperties(PageProperties pageProperties)
		{
			this.PaperSize.Height = pageProperties.PageHeight;
			this.PaperSize.Width = pageProperties.PageWidth;
			this.Margins.Bottom = pageProperties.BottomMargin;
			this.Margins.Left = pageProperties.LeftMargin;
			this.Margins.Right = pageProperties.RightMargin;
			this.Margins.Top = pageProperties.TopMargin;
		}

		// Token: 0x04000A65 RID: 2661
		public ReportPaperSize PaperSize;

		// Token: 0x04000A66 RID: 2662
		public ReportMargins Margins;
	}
}
