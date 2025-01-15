using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200023F RID: 575
	public sealed class ChartTitleCollection : ChartObjectCollectionBase<ChartTitle, ChartTitleInstance>
	{
		// Token: 0x06001622 RID: 5666 RVA: 0x0005940F File Offset: 0x0005760F
		internal ChartTitleCollection(Chart chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x00059420 File Offset: 0x00057620
		protected override ChartTitle CreateChartObject(int index)
		{
			if (this.m_chart.IsOldSnapshot)
			{
				return new ChartTitle(this.m_chart.RenderChartDef.Title, this.m_chart.ChartInstanceInfo.Title, this.m_chart);
			}
			return new ChartTitle(this.m_chart.ChartDef.Titles[index], this.m_chart);
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x00059487 File Offset: 0x00057687
		public override int Count
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return 1;
				}
				if (this.m_chart.ChartDef.Titles != null)
				{
					return this.m_chart.ChartDef.Titles.Count;
				}
				return 0;
			}
		}

		// Token: 0x04000A9E RID: 2718
		private Chart m_chart;
	}
}
