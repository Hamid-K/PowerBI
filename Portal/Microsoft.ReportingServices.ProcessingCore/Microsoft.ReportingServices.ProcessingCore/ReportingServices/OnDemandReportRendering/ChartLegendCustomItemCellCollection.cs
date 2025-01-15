using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200024A RID: 586
	public sealed class ChartLegendCustomItemCellCollection : ChartObjectCollectionBase<ChartLegendCustomItemCell, ChartLegendCustomItemCellInstance>
	{
		// Token: 0x0600169C RID: 5788 RVA: 0x0005AFF9 File Offset: 0x000591F9
		internal ChartLegendCustomItemCellCollection(ChartLegendCustomItem legendCustomItem, Chart chart)
		{
			this.m_legendCustomItem = legendCustomItem;
			this.m_chart = chart;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0005B00F File Offset: 0x0005920F
		protected override ChartLegendCustomItemCell CreateChartObject(int index)
		{
			if (this.m_chart.IsOldSnapshot)
			{
				return null;
			}
			return new ChartLegendCustomItemCell(this.m_legendCustomItem.ChartLegendCustomItemDef.LegendCustomItemCells[index], this.m_chart);
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x0005B041 File Offset: 0x00059241
		public override int Count
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return 0;
				}
				return this.m_legendCustomItem.ChartLegendCustomItemDef.LegendCustomItemCells.Count;
			}
		}

		// Token: 0x04000AF5 RID: 2805
		private Chart m_chart;

		// Token: 0x04000AF6 RID: 2806
		private ChartLegendCustomItem m_legendCustomItem;
	}
}
