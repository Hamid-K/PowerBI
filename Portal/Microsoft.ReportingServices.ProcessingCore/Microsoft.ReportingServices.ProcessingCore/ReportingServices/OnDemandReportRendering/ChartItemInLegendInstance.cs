using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000267 RID: 615
	public sealed class ChartItemInLegendInstance : BaseInstance
	{
		// Token: 0x060017ED RID: 6125 RVA: 0x00061F24 File Offset: 0x00060124
		internal ChartItemInLegendInstance(ChartItemInLegend chartItemInLegendDef)
			: base(chartItemInLegendDef.ReportScope)
		{
			this.m_chartItemInLegendDef = chartItemInLegendDef;
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x00061F3C File Offset: 0x0006013C
		public string LegendText
		{
			get
			{
				if (this.m_legendText == null && !this.m_chartItemInLegendDef.ChartDef.IsOldSnapshot)
				{
					this.m_legendText = this.m_chartItemInLegendDef.ChartItemInLegendDef.EvaluateLegendText(this.ReportScopeInstance, this.m_chartItemInLegendDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_legendText;
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x00061F9C File Offset: 0x0006019C
		public string ToolTip
		{
			get
			{
				if (this.m_toolTip == null)
				{
					this.m_toolTip = this.m_chartItemInLegendDef.ChartItemInLegendDef.EvaluateToolTip(this.ReportScopeInstance, this.m_chartItemInLegendDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00061FE8 File Offset: 0x000601E8
		public bool Hidden
		{
			get
			{
				if (this.m_hidden == null)
				{
					this.m_hidden = new bool?(this.m_chartItemInLegendDef.ChartItemInLegendDef.EvaluateHidden(this.ReportScopeInstance, this.m_chartItemInLegendDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_hidden.Value;
			}
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00062043 File Offset: 0x00060243
		protected override void ResetInstanceCache()
		{
			this.m_legendText = null;
			this.m_toolTip = null;
			this.m_hidden = null;
		}

		// Token: 0x04000C06 RID: 3078
		private ChartItemInLegend m_chartItemInLegendDef;

		// Token: 0x04000C07 RID: 3079
		private string m_legendText;

		// Token: 0x04000C08 RID: 3080
		private string m_toolTip;

		// Token: 0x04000C09 RID: 3081
		private bool? m_hidden;
	}
}
