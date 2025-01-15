using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000EC RID: 236
	public sealed class IndicatorImageInstance : BaseGaugeImageInstance
	{
		// Token: 0x06000B01 RID: 2817 RVA: 0x000316DF File Offset: 0x0002F8DF
		internal IndicatorImageInstance(IndicatorImage defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x000316F0 File Offset: 0x0002F8F0
		public ReportColor HueColor
		{
			get
			{
				if (this.m_hueColor == null)
				{
					this.m_hueColor = new ReportColor(((IndicatorImage)this.m_defObject.BaseGaugeImageDef).EvaluateHueColor(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_hueColor;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00031748 File Offset: 0x0002F948
		public double Transparency
		{
			get
			{
				if (this.m_transparency == null)
				{
					this.m_transparency = new double?(((IndicatorImage)this.m_defObject.BaseGaugeImageDef).EvaluateTransparency(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_transparency.Value;
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000317A9 File Offset: 0x0002F9A9
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_hueColor = null;
			this.m_transparency = null;
		}

		// Token: 0x040004A7 RID: 1191
		private ReportColor m_hueColor;

		// Token: 0x040004A8 RID: 1192
		private double? m_transparency;
	}
}
