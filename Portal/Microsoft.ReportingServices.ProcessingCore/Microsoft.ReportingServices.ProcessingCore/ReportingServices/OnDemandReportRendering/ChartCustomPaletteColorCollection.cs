using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000222 RID: 546
	public sealed class ChartCustomPaletteColorCollection : ChartObjectCollectionBase<ChartCustomPaletteColor, ChartCustomPaletteColorInstance>
	{
		// Token: 0x060014A3 RID: 5283 RVA: 0x000544DE File Offset: 0x000526DE
		internal ChartCustomPaletteColorCollection(Chart chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000544ED File Offset: 0x000526ED
		protected override ChartCustomPaletteColor CreateChartObject(int index)
		{
			if (this.m_chart.IsOldSnapshot)
			{
				return null;
			}
			return new ChartCustomPaletteColor(this.m_chart.ChartDef.CustomPaletteColors[index], this.m_chart);
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0005451F File Offset: 0x0005271F
		public override int Count
		{
			get
			{
				if (this.m_chart.IsOldSnapshot)
				{
					return 0;
				}
				if (this.m_chart.ChartDef.CustomPaletteColors != null)
				{
					return this.m_chart.ChartDef.CustomPaletteColors.Count;
				}
				return 0;
			}
		}

		// Token: 0x040009BD RID: 2493
		private Chart m_chart;
	}
}
