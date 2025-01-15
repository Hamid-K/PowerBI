using System;
using System.IO;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200019D RID: 413
	public sealed class MapInstance : ReportItemInstance, IDynamicImageInstance
	{
		// Token: 0x060010A6 RID: 4262 RVA: 0x00046C5F File Offset: 0x00044E5F
		internal MapInstance(Map reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00046C80 File Offset: 0x00044E80
		public MapAntiAliasing AntiAliasing
		{
			get
			{
				if (this.m_antiAliasing == null)
				{
					this.m_antiAliasing = new MapAntiAliasing?(this.MapDef.MapDef.EvaluateAntiAliasing(this.ReportScopeInstance, this.MapDef.RenderingContext.OdpContext));
				}
				return this.m_antiAliasing.Value;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x00046CD8 File Offset: 0x00044ED8
		public MapTextAntiAliasingQuality TextAntiAliasingQuality
		{
			get
			{
				if (this.m_textAntiAliasingQuality == null)
				{
					this.m_textAntiAliasingQuality = new MapTextAntiAliasingQuality?(this.MapDef.MapDef.EvaluateTextAntiAliasingQuality(this.ReportScopeInstance, this.MapDef.RenderingContext.OdpContext));
				}
				return this.m_textAntiAliasingQuality.Value;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x00046D30 File Offset: 0x00044F30
		public double ShadowIntensity
		{
			get
			{
				if (this.m_shadowIntensity == null)
				{
					this.m_shadowIntensity = new double?(this.MapDef.MapDef.EvaluateShadowIntensity(this.ReportScopeInstance, this.MapDef.RenderingContext.OdpContext));
				}
				return this.m_shadowIntensity.Value;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x00046D88 File Offset: 0x00044F88
		public string TileLanguage
		{
			get
			{
				if (!this.m_tileLanguageEvaluated)
				{
					this.m_tileLanguage = this.MapDef.MapDef.EvaluateTileLanguage(this.ReportScopeInstance, this.MapDef.RenderingContext.OdpContext);
					this.m_tileLanguageEvaluated = true;
				}
				return this.m_tileLanguage;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x00046DD8 File Offset: 0x00044FD8
		public string PageName
		{
			get
			{
				if (!this.m_pageNameEvaluated)
				{
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_pageName = null;
					}
					else
					{
						this.m_pageNameEvaluated = true;
						Map mapDef = this.MapDef.MapDef;
						ExpressionInfo pageName = mapDef.PageName;
						if (pageName != null)
						{
							if (pageName.IsExpression)
							{
								this.m_pageName = mapDef.EvaluatePageName(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext);
							}
							else
							{
								this.m_pageName = pageName.StringValue;
							}
						}
					}
				}
				return this.m_pageName;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x00046E5E File Offset: 0x0004505E
		private Map MapDef
		{
			get
			{
				return (Map)this.m_reportElementDef;
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00046E6B File Offset: 0x0004506B
		public void SetDpi(int xDpi, int yDpi)
		{
			this.m_dpiX = (float)xDpi;
			this.m_dpiY = (float)yDpi;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00046E7D File Offset: 0x0004507D
		public void SetSize(double width, double height)
		{
			this.m_widthOverride = new double?(width);
			this.m_heightOverride = new double?(height);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00046E98 File Offset: 0x00045098
		public Stream GetImage()
		{
			bool flag;
			return this.GetImage(DynamicImageInstance.ImageType.PNG, out flag);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00046EB0 File Offset: 0x000450B0
		public Stream GetImage(DynamicImageInstance.ImageType type)
		{
			bool flag;
			return this.GetImage(type, out flag);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00046EC6 File Offset: 0x000450C6
		public Stream GetImage(out ActionInfoWithDynamicImageMapCollection actionImageMaps)
		{
			return this.GetImage(DynamicImageInstance.ImageType.PNG, out actionImageMaps);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00046ED0 File Offset: 0x000450D0
		public Stream GetImage(DynamicImageInstance.ImageType type, out ActionInfoWithDynamicImageMapCollection actionImageMaps)
		{
			Stream stream2;
			try
			{
				Stream stream;
				this.GetImage(type, out actionImageMaps, out stream);
				stream2 = stream;
			}
			catch (Exception ex)
			{
				actionImageMaps = null;
				stream2 = DynamicImageInstance.CreateExceptionImage(ex, this.WidthInPixels, this.HeightInPixels, this.m_dpiX, this.m_dpiY);
			}
			return stream2;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00046F20 File Offset: 0x00045120
		private Stream GetImage(DynamicImageInstance.ImageType type, out bool hasImageMap)
		{
			ActionInfoWithDynamicImageMapCollection actionInfoWithDynamicImageMapCollection;
			Stream image = this.GetImage(type, out actionInfoWithDynamicImageMapCollection);
			hasImageMap = actionInfoWithDynamicImageMapCollection != null;
			return image;
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00046F3C File Offset: 0x0004513C
		private int WidthInPixels
		{
			get
			{
				return MappingHelper.ToIntPixels(((ReportItem)this.m_reportElementDef).Width, this.m_dpiX);
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x00046F59 File Offset: 0x00045159
		private int HeightInPixels
		{
			get
			{
				return MappingHelper.ToIntPixels(((ReportItem)this.m_reportElementDef).Height, this.m_dpiX);
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00046F78 File Offset: 0x00045178
		private void GetImage(DynamicImageInstance.ImageType type, out ActionInfoWithDynamicImageMapCollection actionImageMaps, out Stream image)
		{
			using (IMapMapper mapMapper = MapMapperFactory.CreateMapMapperInstance((Map)this.m_reportElementDef, base.GetDefaultFontFamily()))
			{
				mapMapper.DpiX = this.m_dpiX;
				mapMapper.DpiY = this.m_dpiY;
				mapMapper.WidthOverride = this.m_widthOverride;
				mapMapper.HeightOverride = this.m_heightOverride;
				mapMapper.RenderMap();
				image = mapMapper.GetImage(type);
				actionImageMaps = mapMapper.GetImageMaps();
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00047000 File Offset: 0x00045200
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_antiAliasing = null;
			this.m_textAntiAliasingQuality = null;
			this.m_shadowIntensity = null;
			this.m_tileLanguage = null;
			this.m_tileLanguageEvaluated = false;
			this.m_pageNameEvaluated = false;
			this.m_pageName = null;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00047054 File Offset: 0x00045254
		public Stream GetCoreXml()
		{
			Stream coreXml;
			using (IMapMapper mapMapper = MapMapperFactory.CreateMapMapperInstance((Map)this.m_reportElementDef, base.GetDefaultFontFamily()))
			{
				mapMapper.DpiX = this.m_dpiX;
				mapMapper.DpiY = this.m_dpiY;
				mapMapper.WidthOverride = this.m_widthOverride;
				mapMapper.HeightOverride = this.m_heightOverride;
				mapMapper.RenderMap();
				coreXml = mapMapper.GetCoreXml();
			}
			return coreXml;
		}

		// Token: 0x040007E2 RID: 2018
		private float m_dpiX = 96f;

		// Token: 0x040007E3 RID: 2019
		private float m_dpiY = 96f;

		// Token: 0x040007E4 RID: 2020
		private double? m_widthOverride;

		// Token: 0x040007E5 RID: 2021
		private double? m_heightOverride;

		// Token: 0x040007E6 RID: 2022
		private MapAntiAliasing? m_antiAliasing;

		// Token: 0x040007E7 RID: 2023
		private MapTextAntiAliasingQuality? m_textAntiAliasingQuality;

		// Token: 0x040007E8 RID: 2024
		private double? m_shadowIntensity;

		// Token: 0x040007E9 RID: 2025
		private string m_tileLanguage;

		// Token: 0x040007EA RID: 2026
		private bool m_tileLanguageEvaluated;

		// Token: 0x040007EB RID: 2027
		private bool m_pageNameEvaluated;

		// Token: 0x040007EC RID: 2028
		private string m_pageName;
	}
}
