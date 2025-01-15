using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000234 RID: 564
	public sealed class ChartLegendCollection : ChartObjectCollectionBase<ChartLegend, ChartLegendInstance>
	{
		// Token: 0x060015AB RID: 5547 RVA: 0x000575A4 File Offset: 0x000557A4
		internal ChartLegendCollection(Chart chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000575B4 File Offset: 0x000557B4
		protected override ChartLegend CreateChartObject(int index)
		{
			if (!this.m_chart.IsOldSnapshot)
			{
				return new ChartLegend(this.m_chart.ChartDef.Legends[index], this.m_chart);
			}
			if (this.m_chart.RenderChartDef.Legend != null)
			{
				return new ChartLegend(this.m_chart.RenderChartDef.Legend, this.m_chart.ChartInstanceInfo.LegendStyleAttributeValues, this.m_chart);
			}
			return null;
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0005762F File Offset: 0x0005582F
		public override int Count
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return 1;
				}
				if (this.m_chart.ChartDef.Legends != null)
				{
					return this.m_chart.ChartDef.Legends.Count;
				}
				return 0;
			}
		}

		// Token: 0x04000A46 RID: 2630
		private Chart m_chart;
	}
}
