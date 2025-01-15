using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003BC RID: 956
	public class PlotArea
	{
		// Token: 0x06001ED3 RID: 7891 RVA: 0x0007DC44 File Offset: 0x0007BE44
		public PlotArea()
		{
			this.Style = null;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0007DC53 File Offset: 0x0007BE53
		public PlotArea(Style style)
		{
			this.Style = style;
		}

		// Token: 0x04000D62 RID: 3426
		public Style Style;
	}
}
