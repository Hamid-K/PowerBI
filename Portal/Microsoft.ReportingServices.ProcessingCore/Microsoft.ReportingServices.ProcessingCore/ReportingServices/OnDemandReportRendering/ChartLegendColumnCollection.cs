using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000235 RID: 565
	public sealed class ChartLegendColumnCollection : ChartObjectCollectionBase<ChartLegendColumn, ChartLegendColumnInstance>
	{
		// Token: 0x060015AE RID: 5550 RVA: 0x00057669 File Offset: 0x00055869
		internal ChartLegendColumnCollection(ChartLegend legend, Chart chart)
		{
			this.m_legend = legend;
			this.m_chart = chart;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0005767F File Offset: 0x0005587F
		protected override ChartLegendColumn CreateChartObject(int index)
		{
			if (this.m_chart.IsOldSnapshot)
			{
				return null;
			}
			return new ChartLegendColumn(this.m_legend.ChartLegendDef.LegendColumns[index], this.m_chart);
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x000576B1 File Offset: 0x000558B1
		public override int Count
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return 0;
				}
				return this.m_legend.ChartLegendDef.LegendColumns.Count;
			}
		}

		// Token: 0x04000A47 RID: 2631
		private Chart m_chart;

		// Token: 0x04000A48 RID: 2632
		private ChartLegend m_legend;
	}
}
