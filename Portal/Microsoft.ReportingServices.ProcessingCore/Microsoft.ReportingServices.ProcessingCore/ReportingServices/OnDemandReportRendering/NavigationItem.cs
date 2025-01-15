using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000218 RID: 536
	public sealed class NavigationItem
	{
		// Token: 0x0600145B RID: 5211 RVA: 0x00053888 File Offset: 0x00051A88
		internal NavigationItem(NavigationItem navigationItem)
		{
			this.m_navigationItem = navigationItem;
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00053897 File Offset: 0x00051A97
		public string ReportItemReference
		{
			get
			{
				return this.m_navigationItem.ReportItemReference;
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x000538A4 File Offset: 0x00051AA4
		public ReportItem ReportItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040009A2 RID: 2466
		private readonly NavigationItem m_navigationItem;
	}
}
