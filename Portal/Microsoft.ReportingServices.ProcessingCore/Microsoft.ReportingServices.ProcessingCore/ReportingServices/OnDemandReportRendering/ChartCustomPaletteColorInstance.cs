using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000255 RID: 597
	public sealed class ChartCustomPaletteColorInstance : BaseInstance
	{
		// Token: 0x0600173F RID: 5951 RVA: 0x0005E5C2 File Offset: 0x0005C7C2
		internal ChartCustomPaletteColorInstance(ChartCustomPaletteColor chartCustomPaletteColorDef)
			: base(chartCustomPaletteColorDef.ChartDef)
		{
			this.m_chartCustomPaletteColorDef = chartCustomPaletteColorDef;
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x0005E5D8 File Offset: 0x0005C7D8
		public ReportColor Color
		{
			get
			{
				if (!this.m_colorEvaluated)
				{
					this.m_colorEvaluated = true;
					if (!this.m_chartCustomPaletteColorDef.ChartDef.IsOldSnapshot)
					{
						this.m_color = new ReportColor(this.m_chartCustomPaletteColorDef.ChartCustomPaletteColorDef.EvaluateColor(this.ReportScopeInstance, this.m_chartCustomPaletteColorDef.ChartDef.RenderingContext.OdpContext), true);
					}
				}
				return this.m_color;
			}
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0005E643 File Offset: 0x0005C843
		protected override void ResetInstanceCache()
		{
			this.m_colorEvaluated = false;
		}

		// Token: 0x04000B79 RID: 2937
		private ChartCustomPaletteColor m_chartCustomPaletteColorDef;

		// Token: 0x04000B7A RID: 2938
		private bool m_colorEvaluated;

		// Token: 0x04000B7B RID: 2939
		private ReportColor m_color;
	}
}
