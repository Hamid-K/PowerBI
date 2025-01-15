using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001D5 RID: 469
	public sealed class MapMarkerImageInstance : BaseInstance
	{
		// Token: 0x0600120C RID: 4620 RVA: 0x0004A698 File Offset: 0x00048898
		internal MapMarkerImageInstance(MapMarkerImage defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0004A6B4 File Offset: 0x000488B4
		public Image.SourceType Source
		{
			get
			{
				if (this.m_source == null)
				{
					this.m_source = new Image.SourceType?(this.m_defObject.MapMarkerImageDef.EvaluateSource(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_source.Value;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0004A70F File Offset: 0x0004890F
		public byte[] ImageData
		{
			get
			{
				return this.ImageHandler.ImageData;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0004A71C File Offset: 0x0004891C
		public string MIMEType
		{
			get
			{
				return this.ImageHandler.MIMEType;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x0004A72C File Offset: 0x0004892C
		public ReportColor TransparentColor
		{
			get
			{
				if (this.m_transparentColor == null)
				{
					this.m_transparentColor = new ReportColor(this.m_defObject.MapMarkerImageDef.EvaluateTransparentColor(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_transparentColor;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0004A780 File Offset: 0x00048980
		public MapResizeMode ResizeMode
		{
			get
			{
				if (this.m_resizeMode == null)
				{
					this.m_resizeMode = new MapResizeMode?(this.m_defObject.MapMarkerImageDef.EvaluateResizeMode(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_resizeMode.Value;
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0004A7DB File Offset: 0x000489DB
		protected override void ResetInstanceCache()
		{
			this.m_source = null;
			this.m_transparentColor = null;
			this.m_resizeMode = null;
			if (this.m_imageDataHandler != null)
			{
				this.m_imageDataHandler.ClearCache();
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0004A80F File Offset: 0x00048A0F
		private ImageDataHandler ImageHandler
		{
			get
			{
				if (this.m_imageDataHandler == null || this.Source != this.m_imageDataHandler.Source)
				{
					this.m_imageDataHandler = ImageDataHandlerFactory.Create(this.m_defObject.MapDef, this.m_defObject);
				}
				return this.m_imageDataHandler;
			}
		}

		// Token: 0x04000895 RID: 2197
		private MapMarkerImage m_defObject;

		// Token: 0x04000896 RID: 2198
		private Image.SourceType? m_source;

		// Token: 0x04000897 RID: 2199
		private ReportColor m_transparentColor;

		// Token: 0x04000898 RID: 2200
		private MapResizeMode? m_resizeMode;

		// Token: 0x04000899 RID: 2201
		private ImageDataHandler m_imageDataHandler;
	}
}
