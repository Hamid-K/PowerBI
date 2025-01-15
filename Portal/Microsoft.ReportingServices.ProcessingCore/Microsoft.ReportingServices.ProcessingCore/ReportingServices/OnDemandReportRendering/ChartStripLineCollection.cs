using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200024D RID: 589
	public sealed class ChartStripLineCollection : ChartObjectCollectionBase<ChartStripLine, ChartStripLineInstance>
	{
		// Token: 0x060016C3 RID: 5827 RVA: 0x0005BA19 File Offset: 0x00059C19
		internal ChartStripLineCollection(ChartAxis axis, Chart chart)
		{
			this.m_axis = axis;
			this.m_chart = chart;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0005BA2F File Offset: 0x00059C2F
		protected override ChartStripLine CreateChartObject(int index)
		{
			if (this.m_chart.IsOldSnapshot)
			{
				return null;
			}
			return new ChartStripLine(this.m_axis.AxisDef.StripLines[index], this.m_chart);
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x0005BA61 File Offset: 0x00059C61
		public override int Count
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return 0;
				}
				return this.m_axis.AxisDef.StripLines.Count;
			}
		}

		// Token: 0x04000B14 RID: 2836
		private Chart m_chart;

		// Token: 0x04000B15 RID: 2837
		private ChartAxis m_axis;
	}
}
