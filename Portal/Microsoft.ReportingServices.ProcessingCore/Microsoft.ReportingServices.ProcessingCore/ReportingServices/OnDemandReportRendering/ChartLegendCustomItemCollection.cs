using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000237 RID: 567
	public sealed class ChartLegendCustomItemCollection : ChartObjectCollectionBase<ChartLegendCustomItem, ChartLegendCustomItemInstance>
	{
		// Token: 0x060015C2 RID: 5570 RVA: 0x00057B50 File Offset: 0x00055D50
		internal ChartLegendCustomItemCollection(ChartLegend legend, Chart chart)
		{
			this.m_legend = legend;
			this.m_chart = chart;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00057B66 File Offset: 0x00055D66
		protected override ChartLegendCustomItem CreateChartObject(int index)
		{
			if (this.m_chart.IsOldSnapshot)
			{
				return null;
			}
			return new ChartLegendCustomItem(this.m_legend.ChartLegendDef.LegendCustomItems[index], this.m_chart);
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00057B98 File Offset: 0x00055D98
		public override int Count
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return 0;
				}
				return this.m_legend.ChartLegendDef.LegendCustomItems.Count;
			}
		}

		// Token: 0x04000A55 RID: 2645
		private Chart m_chart;

		// Token: 0x04000A56 RID: 2646
		private ChartLegend m_legend;
	}
}
