using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200026B RID: 619
	public sealed class ChartLegendTitleInstance : BaseInstance
	{
		// Token: 0x06001804 RID: 6148 RVA: 0x00062590 File Offset: 0x00060790
		internal ChartLegendTitleInstance(ChartLegendTitle chartLegendTitleDef)
			: base(chartLegendTitleDef.ChartDef)
		{
			this.m_chartLegendTitleDef = chartLegendTitleDef;
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x000625A5 File Offset: 0x000607A5
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartLegendTitleDef, this.m_chartLegendTitleDef.ChartDef, this.m_chartLegendTitleDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x000625E4 File Offset: 0x000607E4
		public string Caption
		{
			get
			{
				if (this.m_caption == null && !this.m_chartLegendTitleDef.ChartDef.IsOldSnapshot)
				{
					this.m_caption = this.m_chartLegendTitleDef.ChartLegendTitleDef.EvaluateCaption(this.ReportScopeInstance, this.m_chartLegendTitleDef.ChartDef.RenderingContext.OdpContext);
				}
				return this.m_caption;
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x00062644 File Offset: 0x00060844
		public ChartSeparators TitleSeparator
		{
			get
			{
				if (this.m_titleSeparator == null && !this.m_chartLegendTitleDef.ChartDef.IsOldSnapshot)
				{
					this.m_titleSeparator = new ChartSeparators?(this.m_chartLegendTitleDef.ChartLegendTitleDef.EvaluateTitleSeparator(this.ReportScopeInstance, this.m_chartLegendTitleDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_titleSeparator.Value;
			}
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x000626B1 File Offset: 0x000608B1
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_caption = null;
			this.m_titleSeparator = null;
		}

		// Token: 0x04000C19 RID: 3097
		private ChartLegendTitle m_chartLegendTitleDef;

		// Token: 0x04000C1A RID: 3098
		private StyleInstance m_style;

		// Token: 0x04000C1B RID: 3099
		private string m_caption;

		// Token: 0x04000C1C RID: 3100
		private ChartSeparators? m_titleSeparator;
	}
}
