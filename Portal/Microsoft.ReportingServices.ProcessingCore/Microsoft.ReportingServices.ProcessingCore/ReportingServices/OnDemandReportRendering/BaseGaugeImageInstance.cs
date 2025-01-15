using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000E7 RID: 231
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class BaseGaugeImageInstance : BaseInstance
	{
		// Token: 0x06000AE8 RID: 2792 RVA: 0x00031171 File Offset: 0x0002F371
		internal BaseGaugeImageInstance(BaseGaugeImage defObject)
			: base(defObject.GaugePanelDef)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00031188 File Offset: 0x0002F388
		public Image.SourceType Source
		{
			get
			{
				if (this.m_source == null)
				{
					this.m_source = new Image.SourceType?(this.m_defObject.BaseGaugeImageDef.EvaluateSource(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_source.Value;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x000311E3 File Offset: 0x0002F3E3
		public string MIMEType
		{
			get
			{
				return this.ImageHandler.MIMEType;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x000311F0 File Offset: 0x0002F3F0
		public ReportColor TransparentColor
		{
			get
			{
				if (this.m_transparentColor == null)
				{
					this.m_transparentColor = new ReportColor(this.m_defObject.BaseGaugeImageDef.EvaluateTransparentColor(this.ReportScopeInstance, this.m_defObject.GaugePanelDef.RenderingContext.OdpContext));
				}
				return this.m_transparentColor;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00031241 File Offset: 0x0002F441
		public byte[] ImageData
		{
			get
			{
				return this.ImageHandler.ImageData;
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0003124E File Offset: 0x0002F44E
		protected override void ResetInstanceCache()
		{
			this.m_source = null;
			this.m_transparentColor = null;
			if (this.m_imageDataHandler != null)
			{
				this.m_imageDataHandler.ClearCache();
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00031276 File Offset: 0x0002F476
		private ImageDataHandler ImageHandler
		{
			get
			{
				if (this.m_imageDataHandler == null || this.Source != this.m_imageDataHandler.Source)
				{
					this.m_imageDataHandler = ImageDataHandlerFactory.Create(this.m_defObject.GaugePanelDef, this.m_defObject);
				}
				return this.m_imageDataHandler;
			}
		}

		// Token: 0x04000499 RID: 1177
		protected BaseGaugeImage m_defObject;

		// Token: 0x0400049A RID: 1178
		private Image.SourceType? m_source;

		// Token: 0x0400049B RID: 1179
		private ReportColor m_transparentColor;

		// Token: 0x0400049C RID: 1180
		private ImageDataHandler m_imageDataHandler;
	}
}
