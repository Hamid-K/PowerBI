using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200023B RID: 571
	public sealed class ChartDerivedSeriesCollection : ChartObjectCollectionBase<ChartDerivedSeries, BaseInstance>
	{
		// Token: 0x060015F9 RID: 5625 RVA: 0x00058BBA File Offset: 0x00056DBA
		internal ChartDerivedSeriesCollection(Chart chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00058BC9 File Offset: 0x00056DC9
		protected override ChartDerivedSeries CreateChartObject(int index)
		{
			if (this.m_chart.IsOldSnapshot)
			{
				return null;
			}
			return new ChartDerivedSeries(this.m_chart.ChartDef.DerivedSeriesCollection[index], this.m_chart);
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x00058BFB File Offset: 0x00056DFB
		public override int Count
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return 0;
				}
				return this.m_chart.ChartDef.DerivedSeriesCollection.Count;
			}
		}

		// Token: 0x04000A80 RID: 2688
		private Chart m_chart;
	}
}
