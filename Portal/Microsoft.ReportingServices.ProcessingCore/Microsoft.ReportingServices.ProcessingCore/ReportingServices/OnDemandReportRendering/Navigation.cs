using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000217 RID: 535
	public abstract class Navigation
	{
		// Token: 0x0600145A RID: 5210 RVA: 0x00053874 File Offset: 0x00051A74
		internal Navigation(BandLayoutOptions bandLayout)
		{
			this.m_navigation = bandLayout.Navigation;
		}

		// Token: 0x040009A1 RID: 2465
		internal readonly Navigation m_navigation;
	}
}
