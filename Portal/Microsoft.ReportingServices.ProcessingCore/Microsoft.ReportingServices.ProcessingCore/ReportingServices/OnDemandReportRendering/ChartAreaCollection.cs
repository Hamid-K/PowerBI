using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000233 RID: 563
	public sealed class ChartAreaCollection : ChartObjectCollectionBase<ChartArea, ChartAreaInstance>
	{
		// Token: 0x060015A7 RID: 5543 RVA: 0x000574D3 File Offset: 0x000556D3
		internal ChartAreaCollection(Chart chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x000574E2 File Offset: 0x000556E2
		protected override ChartArea CreateChartObject(int index)
		{
			if (this.m_chart.IsOldSnapshot)
			{
				return new ChartArea(this.m_chart);
			}
			return new ChartArea(this.m_chart.ChartDef.ChartAreas[index], this.m_chart);
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x0005751E File Offset: 0x0005571E
		public override int Count
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return 1;
				}
				if (this.m_chart.ChartDef.ChartAreas != null)
				{
					return this.m_chart.ChartDef.ChartAreas.Count;
				}
				return 0;
			}
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00057558 File Offset: 0x00055758
		internal ChartArea GetByName(string areaName)
		{
			for (int i = 0; i < this.Count; i++)
			{
				ChartArea chartArea = this.m_chart.ChartDef.ChartAreas[i];
				if (string.CompareOrdinal(areaName, chartArea.ChartAreaName) == 0)
				{
					return base[i];
				}
			}
			return null;
		}

		// Token: 0x04000A45 RID: 2629
		private Chart m_chart;
	}
}
