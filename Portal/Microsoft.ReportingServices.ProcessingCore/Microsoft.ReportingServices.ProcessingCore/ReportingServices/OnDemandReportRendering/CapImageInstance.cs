using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000E9 RID: 233
	public sealed class CapImageInstance : BaseGaugeImageInstance
	{
		// Token: 0x06000AF4 RID: 2804 RVA: 0x00031403 File Offset: 0x0002F603
		internal CapImageInstance(CapImage defObject)
			: base(defObject)
		{
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0003140C File Offset: 0x0002F60C
		public ReportColor HueColor
		{
			get
			{
				if (this.m_hueColor == null)
				{
					this.m_hueColor = new ReportColor(((CapImage)this.m_defObject.BaseGaugeImageDef).EvaluateHueColor(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext), true);
				}
				return this.m_hueColor;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00031464 File Offset: 0x0002F664
		public ReportSize OffsetX
		{
			get
			{
				if (this.m_offsetX == null)
				{
					this.m_offsetX = new ReportSize(((CapImage)this.m_defObject.BaseGaugeImageDef).EvaluateOffsetX(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_offsetX;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x000314BC File Offset: 0x0002F6BC
		public ReportSize OffsetY
		{
			get
			{
				if (this.m_offsetY == null)
				{
					this.m_offsetY = new ReportSize(((CapImage)this.m_defObject.BaseGaugeImageDef).EvaluateOffsetY(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_offsetY;
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00031512 File Offset: 0x0002F712
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_hueColor = null;
			this.m_offsetX = null;
			this.m_offsetY = null;
		}

		// Token: 0x040004A0 RID: 1184
		private ReportColor m_hueColor;

		// Token: 0x040004A1 RID: 1185
		private ReportSize m_offsetX;

		// Token: 0x040004A2 RID: 1186
		private ReportSize m_offsetY;
	}
}
