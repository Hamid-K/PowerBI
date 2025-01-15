using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000260 RID: 608
	public sealed class ChartLegendCustomItemInstance : BaseInstance
	{
		// Token: 0x060017A8 RID: 6056 RVA: 0x00060528 File Offset: 0x0005E728
		internal ChartLegendCustomItemInstance(ChartLegendCustomItem chartLegendCustomItemDef)
			: base(chartLegendCustomItemDef.ChartDef)
		{
			this.m_chartLegendCustomItemDef = chartLegendCustomItemDef;
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x0006053D File Offset: 0x0005E73D
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartLegendCustomItemDef, this.m_chartLegendCustomItemDef.ChartDef, this.m_chartLegendCustomItemDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0006057C File Offset: 0x0005E77C
		public ChartSeparators Separator
		{
			get
			{
				if (this.m_separator == null && !this.m_chartLegendCustomItemDef.ChartDef.IsOldSnapshot)
				{
					this.m_separator = new ChartSeparators?(this.m_chartLegendCustomItemDef.ChartLegendCustomItemDef.EvaluateSeparator(this.ReportScopeInstance, this.m_chartLegendCustomItemDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_separator.Value;
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x000605EC File Offset: 0x0005E7EC
		public ReportColor SeparatorColor
		{
			get
			{
				if (this.m_separatorColor == null && !this.m_chartLegendCustomItemDef.ChartDef.IsOldSnapshot)
				{
					this.m_separatorColor = new ReportColor(this.m_chartLegendCustomItemDef.ChartLegendCustomItemDef.EvaluateSeparatorColor(this.ReportScopeInstance, this.m_chartLegendCustomItemDef.ChartDef.RenderingContext.OdpContext), true);
				}
				return this.m_separatorColor;
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x00060650 File Offset: 0x0005E850
		public string ToolTip
		{
			get
			{
				if (this.m_toolTip == null && !this.m_chartLegendCustomItemDef.ChartDef.IsOldSnapshot)
				{
					this.m_toolTip = this.m_chartLegendCustomItemDef.ChartLegendCustomItemDef.EvaluateToolTip(this.ReportScopeInstance, this.m_chartLegendCustomItemDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x000606AE File Offset: 0x0005E8AE
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_separator = null;
			this.m_separatorColor = null;
			this.m_toolTip = null;
		}

		// Token: 0x04000BC6 RID: 3014
		private ChartLegendCustomItem m_chartLegendCustomItemDef;

		// Token: 0x04000BC7 RID: 3015
		private StyleInstance m_style;

		// Token: 0x04000BC8 RID: 3016
		private ChartSeparators? m_separator;

		// Token: 0x04000BC9 RID: 3017
		private ReportColor m_separatorColor;

		// Token: 0x04000BCA RID: 3018
		private string m_toolTip;
	}
}
