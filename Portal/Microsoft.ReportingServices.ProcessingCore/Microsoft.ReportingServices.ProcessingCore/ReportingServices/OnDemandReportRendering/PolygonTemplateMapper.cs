using System;
using System.Drawing;
using Microsoft.Reporting.Map.WebForms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200016E RID: 366
	internal class PolygonTemplateMapper : SpatialElementTemplateMapper
	{
		// Token: 0x06000F61 RID: 3937 RVA: 0x00042CA7 File Offset: 0x00040EA7
		internal PolygonTemplateMapper(MapMapper mapMapper, PolygonLayerMapper polygonLayerMapper, MapPolygonLayer mapPolygonLayer)
			: base(mapMapper, mapPolygonLayer)
		{
			this.m_polygonLayerMapper = polygonLayerMapper;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00042CB8 File Offset: 0x00040EB8
		internal void Render(MapPolygon mapPolygon, Shape coreShape, bool hasScope)
		{
			bool flag = PolygonTemplateMapper.UseCustomTemplate(mapPolygon, hasScope);
			MapPolygonTemplate mapPolygonTemplate;
			if (flag)
			{
				mapPolygonTemplate = mapPolygon.MapPolygonTemplate;
			}
			else
			{
				mapPolygonTemplate = this.MapPolygonLayer.MapPolygonTemplate;
			}
			bool flag2 = !flag && this.m_polygonLayerMapper.HasColorRule(coreShape) && hasScope;
			if (mapPolygonTemplate == null)
			{
				base.RenderStyle(null, null, coreShape, flag2, hasScope);
				coreShape.BorderStyle = base.GetBorderStyle(null, null, hasScope);
				return;
			}
			base.RenderSpatialElementTemplate(mapPolygonTemplate, coreShape, flag2, hasScope);
			Style style = mapPolygonTemplate.Style;
			StyleInstance style2 = mapPolygonTemplate.Instance.Style;
			coreShape.BorderStyle = base.GetBorderStyle(style, style2, hasScope);
			ReportDoubleProperty scaleFactor = mapPolygonTemplate.ScaleFactor;
			if (scaleFactor != null)
			{
				if (!scaleFactor.IsExpression)
				{
					coreShape.ScaleFactor = scaleFactor.Value;
				}
				else if (hasScope)
				{
					coreShape.ScaleFactor = mapPolygonTemplate.Instance.ScaleFactor;
				}
			}
			ReportDoubleProperty reportDoubleProperty = mapPolygonTemplate.CenterPointOffsetX;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					coreShape.CentralPointOffset.X = reportDoubleProperty.Value;
				}
				else if (hasScope)
				{
					coreShape.CentralPointOffset.X = mapPolygonTemplate.Instance.CenterPointOffsetX;
				}
			}
			reportDoubleProperty = mapPolygonTemplate.CenterPointOffsetY;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					coreShape.CentralPointOffset.Y = reportDoubleProperty.Value;
				}
				else if (hasScope)
				{
					coreShape.CentralPointOffset.Y = mapPolygonTemplate.Instance.CenterPointOffsetY;
				}
			}
			ReportEnumProperty<MapAutoBool> showLabel = mapPolygonTemplate.ShowLabel;
			if (showLabel != null)
			{
				if (!showLabel.IsExpression)
				{
					coreShape.TextVisibility = this.GetTextVisibility(showLabel.Value);
				}
				else if (hasScope)
				{
					coreShape.TextVisibility = this.GetTextVisibility(mapPolygonTemplate.Instance.ShowLabel);
				}
			}
			ReportEnumProperty<MapPolygonLabelPlacement> labelPlacement = mapPolygonTemplate.LabelPlacement;
			if (labelPlacement != null)
			{
				if (!labelPlacement.IsExpression)
				{
					coreShape.TextAlignment = this.GetTextAlignment(labelPlacement.Value);
					return;
				}
				if (hasScope)
				{
					coreShape.TextAlignment = this.GetTextAlignment(mapPolygonTemplate.Instance.LabelPlacement);
				}
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00042E85 File Offset: 0x00041085
		private MapPolygonLayer MapPolygonLayer
		{
			get
			{
				return (MapPolygonLayer)this.m_mapVectorLayer;
			}
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00042E94 File Offset: 0x00041094
		private static bool UseCustomTemplate(MapPolygon mapPolygon, bool hasScope)
		{
			if (mapPolygon == null)
			{
				return false;
			}
			bool flag = false;
			ReportBoolProperty useCustomPolygonTemplate = mapPolygon.UseCustomPolygonTemplate;
			if (useCustomPolygonTemplate != null)
			{
				if (!useCustomPolygonTemplate.IsExpression)
				{
					flag = useCustomPolygonTemplate.Value;
				}
				else if (hasScope)
				{
					flag = mapPolygon.Instance.UseCustomPolygonTemplate;
				}
			}
			return flag;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00042ED4 File Offset: 0x000410D4
		private ContentAlignment GetTextAlignment(MapPolygonLabelPlacement placement)
		{
			switch (placement)
			{
			case MapPolygonLabelPlacement.MiddleLeft:
				return ContentAlignment.MiddleLeft;
			case MapPolygonLabelPlacement.MiddleRight:
				return ContentAlignment.MiddleRight;
			case MapPolygonLabelPlacement.TopCenter:
				return ContentAlignment.TopCenter;
			case MapPolygonLabelPlacement.TopLeft:
				return ContentAlignment.TopLeft;
			case MapPolygonLabelPlacement.TopRight:
				return ContentAlignment.TopRight;
			case MapPolygonLabelPlacement.BottomCenter:
				return ContentAlignment.BottomCenter;
			case MapPolygonLabelPlacement.BottomLeft:
				return ContentAlignment.BottomLeft;
			case MapPolygonLabelPlacement.BottomRight:
				return ContentAlignment.BottomRight;
			default:
				return ContentAlignment.MiddleCenter;
			}
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00042F2B File Offset: 0x0004112B
		private TextVisibility GetTextVisibility(MapAutoBool value)
		{
			if (value == MapAutoBool.True)
			{
				return 0;
			}
			if (value != MapAutoBool.False)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00042F3C File Offset: 0x0004113C
		protected override MapSpatialElementTemplate DefaultTemplate
		{
			get
			{
				return this.MapPolygonLayer.MapPolygonTemplate;
			}
		}

		// Token: 0x0400072D RID: 1837
		private PolygonLayerMapper m_polygonLayerMapper;
	}
}
