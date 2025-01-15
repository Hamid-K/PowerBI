using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x0200003F RID: 63
	public abstract class ChartExprHost : DataRegionExprHost
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000029F8 File Offset: 0x00000BF8
		internal IList<ChartDataPointExprHost> ChartDataPointHostsRemotable
		{
			get
			{
				if (this.m_chartDataPointHostsRemotable == null && this.ChartDataPointHosts != null)
				{
					this.m_chartDataPointHostsRemotable = new RemoteArrayWrapper<ChartDataPointExprHost>(this.ChartDataPointHosts);
				}
				return this.m_chartDataPointHostsRemotable;
			}
		}

		// Token: 0x04000055 RID: 85
		public MultiChartExprHost MultiChartHost;

		// Token: 0x04000056 RID: 86
		public ChartDynamicGroupExprHost RowGroupingsHost;

		// Token: 0x04000057 RID: 87
		public IndexedExprHost StaticRowLabelsHost;

		// Token: 0x04000058 RID: 88
		public ChartDynamicGroupExprHost ColumnGroupingsHost;

		// Token: 0x04000059 RID: 89
		public IndexedExprHost StaticColumnLabelsHost;

		// Token: 0x0400005A RID: 90
		protected ChartDataPointExprHost[] ChartDataPointHosts;

		// Token: 0x0400005B RID: 91
		[CLSCompliant(false)]
		protected IList<ChartDataPointExprHost> m_chartDataPointHostsRemotable;

		// Token: 0x0400005C RID: 92
		public ChartTitleExprHost TitleHost;

		// Token: 0x0400005D RID: 93
		public AxisExprHost CategoryAxisHost;

		// Token: 0x0400005E RID: 94
		public AxisExprHost ValueAxisHost;

		// Token: 0x0400005F RID: 95
		public StyleExprHost LegendHost;

		// Token: 0x04000060 RID: 96
		public StyleExprHost PlotAreaHost;
	}
}
