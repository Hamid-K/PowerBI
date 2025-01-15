using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000138 RID: 312
	public sealed class IndicatorStateInstance : BaseInstance
	{
		// Token: 0x06000D9E RID: 3486 RVA: 0x0003A227 File Offset: 0x00038427
		internal IndicatorStateInstance(IndicatorState defObject)
			: base(defObject.GaugePanelDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0003A244 File Offset: 0x00038444
		public ReportColor Color
		{
			get
			{
				if (this.m_color == null)
				{
					this.m_color = new ReportColor(this.m_defObject.IndicatorStateDef.EvaluateColor(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_color;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0003A298 File Offset: 0x00038498
		public double ScaleFactor
		{
			get
			{
				if (this.m_scaleFactor == null)
				{
					this.m_scaleFactor = new double?(this.m_defObject.IndicatorStateDef.EvaluateScaleFactor(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_scaleFactor.Value;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0003A2F4 File Offset: 0x000384F4
		public GaugeStateIndicatorStyles IndicatorStyle
		{
			get
			{
				if (this.m_indicatorStyle == null)
				{
					this.m_indicatorStyle = new GaugeStateIndicatorStyles?(this.m_defObject.IndicatorStateDef.EvaluateIndicatorStyle(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_indicatorStyle.Value;
			}
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003A34F File Offset: 0x0003854F
		protected override void ResetInstanceCache()
		{
			this.m_color = null;
			this.m_scaleFactor = null;
			this.m_indicatorStyle = null;
		}

		// Token: 0x04000641 RID: 1601
		private IndicatorState m_defObject;

		// Token: 0x04000642 RID: 1602
		private ReportColor m_color;

		// Token: 0x04000643 RID: 1603
		private double? m_scaleFactor;

		// Token: 0x04000644 RID: 1604
		private GaugeStateIndicatorStyles? m_indicatorStyle;
	}
}
