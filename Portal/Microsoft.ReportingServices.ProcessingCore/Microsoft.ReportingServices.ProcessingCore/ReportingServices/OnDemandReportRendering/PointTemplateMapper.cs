using System;
using Microsoft.Reporting.Map.WebForms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200016F RID: 367
	internal abstract class PointTemplateMapper : SpatialElementTemplateMapper
	{
		// Token: 0x06000F68 RID: 3944 RVA: 0x00042F49 File Offset: 0x00041149
		internal PointTemplateMapper(MapMapper mapMapper, VectorLayerMapper vectorLayerMapper, MapVectorLayer mapVectorLayer)
			: base(mapMapper, mapVectorLayer)
		{
			this.m_vectorLayerMapper = vectorLayerMapper;
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x00042F5A File Offset: 0x0004115A
		private MapPointLayer MapPointLayer
		{
			get
			{
				return (MapPointLayer)this.m_mapVectorLayer;
			}
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00042F68 File Offset: 0x00041168
		internal void Render(MapPoint mapPoint, Symbol coreSymbol, bool hasScope)
		{
			bool flag = PointTemplateMapper.UseCustomTemplate(mapPoint, hasScope);
			MapPointTemplate mapPointTemplate;
			if (flag)
			{
				mapPointTemplate = mapPoint.MapPointTemplate;
			}
			else
			{
				mapPointTemplate = this.MapPointLayer.MapPointTemplate;
			}
			this.RenderPointTemplate(mapPointTemplate, coreSymbol, flag, !flag && this.m_vectorLayerMapper.HasPointColorRule(coreSymbol) && hasScope, !flag && this.m_vectorLayerMapper.HasPointSizeRule(coreSymbol) && hasScope, !flag && this.m_vectorLayerMapper.HasMarkerRule(coreSymbol) && hasScope, hasScope);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00042FDC File Offset: 0x000411DC
		internal void RenderPolygonCenterPoint(MapPolygon mapPolygon, Symbol coreSymbol, bool hasScope)
		{
			bool flag = PointTemplateMapper.PolygonUseCustomTemplate(mapPolygon, hasScope);
			MapPointTemplate mapPointTemplate;
			if (flag)
			{
				mapPointTemplate = mapPolygon.MapCenterPointTemplate;
			}
			else
			{
				mapPointTemplate = this.m_vectorLayerMapper.GetMapPointTemplate();
			}
			this.RenderPointTemplate(mapPointTemplate, coreSymbol, flag, !flag && this.m_vectorLayerMapper.HasPointColorRule(coreSymbol) && hasScope, !flag && this.m_vectorLayerMapper.HasPointSizeRule(coreSymbol) && hasScope, !flag && this.m_vectorLayerMapper.HasMarkerRule(coreSymbol) && hasScope, hasScope);
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00043050 File Offset: 0x00041250
		protected virtual void RenderPointTemplate(MapPointTemplate mapPointTemplate, Symbol coreSymbol, bool customTemplate, bool ignoreBackgroundColor, bool ignoreSize, bool ignoreMarker, bool hasScope)
		{
			if (mapPointTemplate == null)
			{
				base.RenderStyle(null, null, coreSymbol, ignoreBackgroundColor, hasScope);
				coreSymbol.BorderStyle = base.GetBorderStyle(null, null, hasScope);
				return;
			}
			base.RenderSpatialElementTemplate(mapPointTemplate, coreSymbol, ignoreBackgroundColor, hasScope);
			Style style = mapPointTemplate.Style;
			StyleInstance style2 = mapPointTemplate.Instance.Style;
			coreSymbol.BorderStyle = base.GetBorderStyle(style, style2, hasScope);
			if (!ignoreSize)
			{
				int size = this.GetSize(mapPointTemplate, hasScope);
				coreSymbol.Width = (coreSymbol.Height = (float)size);
			}
			ReportEnumProperty<MapPointLabelPlacement> labelPlacement = mapPointTemplate.LabelPlacement;
			TextAlignment textAlignment = 3;
			if (labelPlacement != null)
			{
				if (!labelPlacement.IsExpression)
				{
					textAlignment = this.GetTextAlignment(labelPlacement.Value);
				}
				else if (hasScope)
				{
					textAlignment = this.GetTextAlignment(mapPointTemplate.Instance.LabelPlacement);
				}
			}
			coreSymbol.TextAlignment = textAlignment;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00043110 File Offset: 0x00041310
		internal int GetSize(MapPointTemplate mapPointTemplate, bool hasScope)
		{
			ReportSizeProperty size = mapPointTemplate.Size;
			int num;
			if (size != null)
			{
				if (!size.IsExpression)
				{
					num = MappingHelper.ToIntPixels(size.Value, this.m_mapMapper.DpiX);
				}
				else if (hasScope)
				{
					num = MappingHelper.ToIntPixels(mapPointTemplate.Instance.Size, this.m_mapMapper.DpiX);
				}
				else
				{
					num = PointTemplateMapper.GetDefaultSymbolSize(this.m_mapMapper.DpiX);
				}
			}
			else
			{
				num = PointTemplateMapper.GetDefaultSymbolSize(this.m_mapMapper.DpiX);
			}
			return num;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0004318E File Offset: 0x0004138E
		internal static int GetDefaultSymbolSize(float dpi)
		{
			return MappingHelper.ToIntPixels(PointTemplateMapper.m_defaultSymbolSize, dpi);
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0004319C File Offset: 0x0004139C
		private static bool UseCustomTemplate(MapPoint mapPoint, bool hasScope)
		{
			if (mapPoint == null)
			{
				return false;
			}
			bool flag = false;
			ReportBoolProperty useCustomPointTemplate = mapPoint.UseCustomPointTemplate;
			if (useCustomPointTemplate != null)
			{
				if (!useCustomPointTemplate.IsExpression)
				{
					flag = useCustomPointTemplate.Value;
				}
				else if (hasScope)
				{
					flag = mapPoint.Instance.UseCustomPointTemplate;
				}
			}
			return flag;
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x000431DC File Offset: 0x000413DC
		internal static bool PolygonUseCustomTemplate(MapPolygon mapPolygon, bool hasScope)
		{
			if (mapPolygon == null)
			{
				return false;
			}
			bool flag = false;
			ReportBoolProperty useCustomCenterPointTemplate = mapPolygon.UseCustomCenterPointTemplate;
			if (useCustomCenterPointTemplate != null)
			{
				if (!useCustomCenterPointTemplate.IsExpression)
				{
					flag = useCustomCenterPointTemplate.Value;
				}
				else if (hasScope)
				{
					flag = mapPolygon.Instance.UseCustomCenterPointTemplate;
				}
			}
			return flag;
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0004321B File Offset: 0x0004141B
		private TextAlignment GetTextAlignment(MapPointLabelPlacement placement)
		{
			switch (placement)
			{
			case MapPointLabelPlacement.Top:
				return 2;
			case MapPointLabelPlacement.Left:
				return 0;
			case MapPointLabelPlacement.Right:
				return 1;
			case MapPointLabelPlacement.Center:
				return 4;
			default:
				return 3;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x00043240 File Offset: 0x00041440
		protected override MapSpatialElementTemplate DefaultTemplate
		{
			get
			{
				if (this.m_mapVectorLayer is MapPolygonLayer)
				{
					return ((MapPolygonLayer)this.m_mapVectorLayer).MapCenterPointTemplate;
				}
				return this.MapPointLayer.MapPointTemplate;
			}
		}

		// Token: 0x0400072E RID: 1838
		private VectorLayerMapper m_vectorLayerMapper;

		// Token: 0x0400072F RID: 1839
		private static string m_defaultSymbolSizeString = "5.25pt";

		// Token: 0x04000730 RID: 1840
		private static ReportSize m_defaultSymbolSize = new ReportSize(PointTemplateMapper.m_defaultSymbolSizeString);
	}
}
