using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200026C RID: 620
	public sealed class ChartLegendColumnHeaderInstance : BaseInstance
	{
		// Token: 0x06001809 RID: 6153 RVA: 0x000626D9 File Offset: 0x000608D9
		internal ChartLegendColumnHeaderInstance(ChartLegendColumnHeader chartLegendColumnHeaderDef)
			: base(chartLegendColumnHeaderDef.ChartDef)
		{
			this.m_chartLegendColumnHeaderDef = chartLegendColumnHeaderDef;
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x000626EE File Offset: 0x000608EE
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartLegendColumnHeaderDef, this.m_chartLegendColumnHeaderDef.ChartDef, this.m_chartLegendColumnHeaderDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x0006272C File Offset: 0x0006092C
		public string Value
		{
			get
			{
				if (this.m_value == null && !this.m_chartLegendColumnHeaderDef.ChartDef.IsOldSnapshot)
				{
					this.m_value = this.m_chartLegendColumnHeaderDef.ChartLegendColumnHeaderDef.EvaluateValue(this.ReportScopeInstance, this.m_chartLegendColumnHeaderDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_value;
			}
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0006278A File Offset: 0x0006098A
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_value = null;
		}

		// Token: 0x04000C1D RID: 3101
		private ChartLegendColumnHeader m_chartLegendColumnHeaderDef;

		// Token: 0x04000C1E RID: 3102
		private StyleInstance m_style;

		// Token: 0x04000C1F RID: 3103
		private string m_value;
	}
}
