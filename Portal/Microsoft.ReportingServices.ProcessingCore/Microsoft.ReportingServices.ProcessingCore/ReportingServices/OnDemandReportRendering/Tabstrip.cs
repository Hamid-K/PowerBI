using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200021A RID: 538
	public sealed class Tabstrip : Navigation
	{
		// Token: 0x06001462 RID: 5218 RVA: 0x00053923 File Offset: 0x00051B23
		internal Tabstrip(BandLayoutOptions bandLayout)
			: base(bandLayout)
		{
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0005392C File Offset: 0x00051B2C
		public NavigationItem NavigationItem
		{
			get
			{
				if (this.m_navigationItem == null && this.RIFTabstrip.NavigationItem != null)
				{
					this.m_navigationItem = new NavigationItem(this.RIFTabstrip.NavigationItem);
				}
				return this.m_navigationItem;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0005395F File Offset: 0x00051B5F
		public Slider Slider
		{
			get
			{
				if (this.m_slider == null && this.RIFTabstrip.Slider != null)
				{
					this.m_slider = new Slider(this.RIFTabstrip.Slider);
				}
				return this.m_slider;
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x00053992 File Offset: 0x00051B92
		private Tabstrip RIFTabstrip
		{
			get
			{
				return this.m_navigation as Tabstrip;
			}
		}

		// Token: 0x040009A5 RID: 2469
		private NavigationItem m_navigationItem;

		// Token: 0x040009A6 RID: 2470
		private Slider m_slider;
	}
}
