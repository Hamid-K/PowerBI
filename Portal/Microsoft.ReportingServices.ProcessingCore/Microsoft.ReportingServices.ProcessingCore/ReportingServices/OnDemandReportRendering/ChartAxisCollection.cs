using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000251 RID: 593
	public sealed class ChartAxisCollection : ChartObjectCollectionBase<ChartAxis, ChartAxisInstance>
	{
		// Token: 0x06001715 RID: 5909 RVA: 0x0005D486 File Offset: 0x0005B686
		internal ChartAxisCollection(ChartArea chartArea, Chart chart, bool isCategory)
		{
			this.m_chartArea = chartArea;
			this.m_chart = chart;
			this.m_isCategory = isCategory;
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0005D4A4 File Offset: 0x0005B6A4
		protected override ChartAxis CreateChartObject(int index)
		{
			if (this.m_chart.IsOldSnapshot)
			{
				if (!this.m_isCategory)
				{
					return new ChartAxis(this.m_chart.RenderChartDef.ValueAxis, this.m_chart.ChartInstanceInfo.ValueAxis, this.m_chart, this.m_isCategory);
				}
				return new ChartAxis(this.m_chart.RenderChartDef.CategoryAxis, this.m_chart.ChartInstanceInfo.CategoryAxis, this.m_chart, this.m_isCategory);
			}
			else
			{
				if (!this.m_isCategory)
				{
					return new ChartAxis(this.m_chartArea.ChartAreaDef.ValueAxes[index], this.m_chart);
				}
				return new ChartAxis(this.m_chartArea.ChartAreaDef.CategoryAxes[index], this.m_chart);
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x0005D578 File Offset: 0x0005B778
		public override int Count
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return 1;
				}
				if (!this.m_isCategory)
				{
					return this.m_chartArea.ChartAreaDef.ValueAxes.Count;
				}
				return this.m_chartArea.ChartAreaDef.CategoryAxes.Count;
			}
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0005D5C8 File Offset: 0x0005B7C8
		internal ChartAxis GetByName(string axisName)
		{
			for (int i = 0; i < this.Count; i++)
			{
				ChartAxis chartAxis;
				if (!this.m_isCategory)
				{
					chartAxis = this.m_chartArea.ChartAreaDef.ValueAxes[i];
				}
				else
				{
					chartAxis = this.m_chartArea.ChartAreaDef.CategoryAxes[i];
				}
				if (string.CompareOrdinal(axisName, chartAxis.AxisName) == 0)
				{
					return base[i];
				}
			}
			return null;
		}

		// Token: 0x04000B5B RID: 2907
		private Chart m_chart;

		// Token: 0x04000B5C RID: 2908
		private ChartArea m_chartArea;

		// Token: 0x04000B5D RID: 2909
		private bool m_isCategory;
	}
}
