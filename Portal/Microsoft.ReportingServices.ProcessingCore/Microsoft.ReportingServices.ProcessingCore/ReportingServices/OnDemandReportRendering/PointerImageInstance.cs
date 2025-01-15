using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000EB RID: 235
	public sealed class PointerImageInstance : BaseGaugeImageInstance
	{
		// Token: 0x06000AFB RID: 2811 RVA: 0x00031540 File Offset: 0x0002F740
		internal PointerImageInstance(PointerImage defObject)
			: base(defObject)
		{
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0003154C File Offset: 0x0002F74C
		public ReportColor HueColor
		{
			get
			{
				if (this.m_hueColor == null)
				{
					this.m_hueColor = new ReportColor(((PointerImage)this.m_defObject.BaseGaugeImageDef).EvaluateHueColor(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext), true);
				}
				return this.m_hueColor;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x000315A4 File Offset: 0x0002F7A4
		public double Transparency
		{
			get
			{
				if (this.m_transparency == null)
				{
					this.m_transparency = new double?(((PointerImage)this.m_defObject.BaseGaugeImageDef).EvaluateTransparency(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_transparency.Value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00031608 File Offset: 0x0002F808
		public ReportSize OffsetX
		{
			get
			{
				if (this.m_offsetX == null)
				{
					this.m_offsetX = new ReportSize(((PointerImage)this.m_defObject.BaseGaugeImageDef).EvaluateOffsetX(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_offsetX;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00031660 File Offset: 0x0002F860
		public ReportSize OffsetY
		{
			get
			{
				if (this.m_offsetY == null)
				{
					this.m_offsetY = new ReportSize(((PointerImage)this.m_defObject.BaseGaugeImageDef).EvaluateOffsetY(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_offsetY;
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000316B6 File Offset: 0x0002F8B6
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_hueColor = null;
			this.m_transparency = null;
			this.m_offsetX = null;
			this.m_offsetY = null;
		}

		// Token: 0x040004A3 RID: 1187
		private ReportColor m_hueColor;

		// Token: 0x040004A4 RID: 1188
		private double? m_transparency;

		// Token: 0x040004A5 RID: 1189
		private ReportSize m_offsetX;

		// Token: 0x040004A6 RID: 1190
		private ReportSize m_offsetY;
	}
}
