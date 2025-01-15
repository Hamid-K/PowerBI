using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000270 RID: 624
	public sealed class ChartBorderSkinInstance : BaseInstance
	{
		// Token: 0x06001834 RID: 6196 RVA: 0x0006374A File Offset: 0x0006194A
		internal ChartBorderSkinInstance(ChartBorderSkin chartBorderSkinDef)
			: base(chartBorderSkinDef.ChartDef)
		{
			this.m_chartBorderSkinDef = chartBorderSkinDef;
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0006375F File Offset: 0x0006195F
		public StyleInstance Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new StyleInstance(this.m_chartBorderSkinDef, this.m_chartBorderSkinDef.ChartDef, this.m_chartBorderSkinDef.ChartDef.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x0006379C File Offset: 0x0006199C
		public ChartBorderSkinType BorderSkinType
		{
			get
			{
				if (this.m_borderSkinType == null && !this.m_chartBorderSkinDef.ChartDef.IsOldSnapshot)
				{
					this.m_borderSkinType = new ChartBorderSkinType?(this.m_chartBorderSkinDef.ChartBorderSkinDef.EvaluateBorderSkinType(this.ReportScopeInstance, this.m_chartBorderSkinDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_borderSkinType.Value;
			}
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00063809 File Offset: 0x00061A09
		protected override void ResetInstanceCache()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.m_borderSkinType = null;
		}

		// Token: 0x04000C44 RID: 3140
		private ChartBorderSkin m_chartBorderSkinDef;

		// Token: 0x04000C45 RID: 3141
		private StyleInstance m_style;

		// Token: 0x04000C46 RID: 3142
		private ChartBorderSkinType? m_borderSkinType;
	}
}
