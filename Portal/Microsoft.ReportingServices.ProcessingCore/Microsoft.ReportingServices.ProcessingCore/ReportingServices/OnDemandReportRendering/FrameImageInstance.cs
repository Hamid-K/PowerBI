using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000E8 RID: 232
	public sealed class FrameImageInstance : BaseGaugeImageInstance
	{
		// Token: 0x06000AEF RID: 2799 RVA: 0x000312B5 File Offset: 0x0002F4B5
		internal FrameImageInstance(FrameImage defObject)
			: base(defObject)
		{
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x000312C0 File Offset: 0x0002F4C0
		public ReportColor HueColor
		{
			get
			{
				if (this.m_hueColor == null)
				{
					this.m_hueColor = new ReportColor(((FrameImage)this.m_defObject.BaseGaugeImageDef).EvaluateHueColor(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext), true);
				}
				return this.m_hueColor;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00031318 File Offset: 0x0002F518
		public double Transparency
		{
			get
			{
				if (this.m_transparency == null)
				{
					this.m_transparency = new double?(((FrameImage)this.m_defObject.BaseGaugeImageDef).EvaluateTransparency(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_transparency.Value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0003137C File Offset: 0x0002F57C
		public bool ClipImage
		{
			get
			{
				if (this.m_clipImage == null)
				{
					this.m_clipImage = new bool?(((FrameImage)this.m_defObject.BaseGaugeImageDef).EvaluateClipImage(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_clipImage.Value;
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000313DC File Offset: 0x0002F5DC
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_hueColor = null;
			this.m_transparency = null;
			this.m_clipImage = null;
		}

		// Token: 0x0400049D RID: 1181
		private ReportColor m_hueColor;

		// Token: 0x0400049E RID: 1182
		private double? m_transparency;

		// Token: 0x0400049F RID: 1183
		private bool? m_clipImage;
	}
}
