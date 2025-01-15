using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000219 RID: 537
	public sealed class Coverflow : Navigation
	{
		// Token: 0x0600145E RID: 5214 RVA: 0x000538A7 File Offset: 0x00051AA7
		internal Coverflow(BandLayoutOptions bandLayout)
			: base(bandLayout)
		{
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x000538B0 File Offset: 0x00051AB0
		public NavigationItem NavigationItem
		{
			get
			{
				if (this.m_navigationItem == null && this.RIFCoverflow.NavigationItem != null)
				{
					this.m_navigationItem = new NavigationItem(this.RIFCoverflow.NavigationItem);
				}
				return this.m_navigationItem;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x000538E3 File Offset: 0x00051AE3
		public Slider Slider
		{
			get
			{
				if (this.m_slider == null && this.RIFCoverflow.Slider != null)
				{
					this.m_slider = new Slider(this.RIFCoverflow.Slider);
				}
				return this.m_slider;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x00053916 File Offset: 0x00051B16
		private Coverflow RIFCoverflow
		{
			get
			{
				return this.m_navigation as Coverflow;
			}
		}

		// Token: 0x040009A3 RID: 2467
		private NavigationItem m_navigationItem;

		// Token: 0x040009A4 RID: 2468
		private Slider m_slider;
	}
}
