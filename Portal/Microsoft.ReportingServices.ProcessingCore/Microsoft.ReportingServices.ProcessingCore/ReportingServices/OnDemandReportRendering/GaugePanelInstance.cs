using System;
using System.IO;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000111 RID: 273
	public sealed class GaugePanelInstance : DynamicImageInstance, IDynamicImageInstance
	{
		// Token: 0x06000C1C RID: 3100 RVA: 0x00034D64 File Offset: 0x00032F64
		internal GaugePanelInstance(GaugePanel reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00034D70 File Offset: 0x00032F70
		public GaugeAntiAliasings AntiAliasing
		{
			get
			{
				if (this.m_antiAliasing == null)
				{
					this.m_antiAliasing = new GaugeAntiAliasings?(((GaugePanel)this.m_reportElementDef.ReportItemDef).EvaluateAntiAliasing(this, this.m_reportElementDef.RenderingContext.OdpContext));
				}
				return this.m_antiAliasing.Value;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00034DC8 File Offset: 0x00032FC8
		public bool AutoLayout
		{
			get
			{
				if (this.m_autoLayout == null)
				{
					this.m_autoLayout = new bool?(((GaugePanel)this.m_reportElementDef.ReportItemDef).EvaluateAutoLayout(this, this.m_reportElementDef.RenderingContext.OdpContext));
				}
				return this.m_autoLayout.Value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00034E20 File Offset: 0x00033020
		public double ShadowIntensity
		{
			get
			{
				if (this.m_shadowIntensity == null)
				{
					this.m_shadowIntensity = new double?(((GaugePanel)this.m_reportElementDef.ReportItemDef).EvaluateShadowIntensity(this, this.m_reportElementDef.RenderingContext.OdpContext));
				}
				return this.m_shadowIntensity.Value;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00034E78 File Offset: 0x00033078
		public TextAntiAliasingQualities TextAntiAliasingQuality
		{
			get
			{
				if (this.m_textAntiAliasingQuality == null)
				{
					this.m_textAntiAliasingQuality = new TextAntiAliasingQualities?(((GaugePanel)this.m_reportElementDef.ReportItemDef).EvaluateTextAntiAliasingQuality(this, this.m_reportElementDef.RenderingContext.OdpContext));
				}
				return this.m_textAntiAliasingQuality.Value;
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00034ED0 File Offset: 0x000330D0
		protected override void GetImage(DynamicImageInstance.ImageType type, out ActionInfoWithDynamicImageMapCollection actionImageMaps, out Stream image)
		{
			using (IGaugeMapper gaugeMapper = GaugeMapperFactory.CreateGaugeMapperInstance((GaugePanel)this.m_reportElementDef, base.GetDefaultFontFamily()))
			{
				gaugeMapper.DpiX = this.m_dpiX;
				gaugeMapper.DpiY = this.m_dpiY;
				gaugeMapper.WidthOverride = this.m_widthOverride;
				gaugeMapper.HeightOverride = this.m_heightOverride;
				gaugeMapper.RenderGaugePanel();
				image = gaugeMapper.GetImage(type);
				actionImageMaps = gaugeMapper.GetImageMaps();
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00034F58 File Offset: 0x00033158
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_antiAliasing = null;
			this.m_autoLayout = null;
			this.m_shadowIntensity = null;
			this.m_textAntiAliasingQuality = null;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00034F90 File Offset: 0x00033190
		public Stream GetCoreXml()
		{
			Stream stream = null;
			using (IGaugeMapper gaugeMapper = GaugeMapperFactory.CreateGaugeMapperInstance((GaugePanel)this.m_reportElementDef, base.GetDefaultFontFamily()))
			{
				gaugeMapper.DpiX = this.m_dpiX;
				gaugeMapper.DpiY = this.m_dpiY;
				gaugeMapper.WidthOverride = this.m_widthOverride;
				gaugeMapper.HeightOverride = this.m_heightOverride;
				gaugeMapper.RenderGaugePanel();
				stream = gaugeMapper.GetCoreXml();
			}
			return stream;
		}

		// Token: 0x0400053F RID: 1343
		private GaugeAntiAliasings? m_antiAliasing;

		// Token: 0x04000540 RID: 1344
		private bool? m_autoLayout;

		// Token: 0x04000541 RID: 1345
		private double? m_shadowIntensity;

		// Token: 0x04000542 RID: 1346
		private TextAntiAliasingQualities? m_textAntiAliasingQuality;
	}
}
