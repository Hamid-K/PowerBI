using System;
using Microsoft.Reporting.Map.WebForms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000171 RID: 369
	internal class LineTemplateMapper : SpatialElementTemplateMapper
	{
		// Token: 0x06000F77 RID: 3959 RVA: 0x000433A1 File Offset: 0x000415A1
		internal LineTemplateMapper(MapMapper mapMapper, LineLayerMapper lineLayerMapper, MapLineLayer mapLineLayer)
			: base(mapMapper, mapLineLayer)
		{
			this.m_lineLayerMapper = lineLayerMapper;
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x000433B4 File Offset: 0x000415B4
		internal void Render(MapLine mapLine, Path corePath, bool hasScope)
		{
			bool flag = this.UseCustomTemplate(mapLine, hasScope);
			MapLineTemplate mapLineTemplate;
			if (flag)
			{
				mapLineTemplate = mapLine.MapLineTemplate;
			}
			else
			{
				mapLineTemplate = this.MapLineLayer.MapLineTemplate;
			}
			this.RenderLineTemplate(mapLineTemplate, corePath, !flag && this.m_lineLayerMapper.HasColorRule(corePath) && hasScope, !flag && this.m_lineLayerMapper.HasSizeRule(corePath) && hasScope, hasScope);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00043414 File Offset: 0x00041614
		protected virtual void RenderLineTemplate(MapLineTemplate mapLineTemplate, Path corePath, bool ignoreBackgroundColor, bool ignoreSize, bool hasScope)
		{
			if (mapLineTemplate == null)
			{
				base.RenderStyle(null, null, corePath, ignoreBackgroundColor, hasScope);
				return;
			}
			base.RenderSpatialElementTemplate(mapLineTemplate, corePath, ignoreBackgroundColor, hasScope);
			Style style = mapLineTemplate.Style;
			StyleInstance style2 = mapLineTemplate.Instance.Style;
			corePath.LineStyle = base.GetBorderStyle(style, style2, hasScope);
			if (!ignoreSize)
			{
				int size = this.GetSize(mapLineTemplate, hasScope);
				corePath.Width = (float)size;
			}
			ReportEnumProperty<MapLineLabelPlacement> labelPlacement = mapLineTemplate.LabelPlacement;
			PathLabelPosition pathLabelPosition = 0;
			if (labelPlacement != null)
			{
				if (!labelPlacement.IsExpression)
				{
					pathLabelPosition = this.GetLabelPosition(labelPlacement.Value);
				}
				else if (hasScope)
				{
					pathLabelPosition = this.GetLabelPosition(mapLineTemplate.Instance.LabelPlacement);
				}
			}
			corePath.LabelPosition = pathLabelPosition;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x000434B8 File Offset: 0x000416B8
		internal int GetSize(MapLineTemplate mapLineTemplate, bool hasScope)
		{
			ReportSizeProperty width = mapLineTemplate.Width;
			int num;
			if (width != null)
			{
				if (!width.IsExpression)
				{
					num = MappingHelper.ToIntPixels(width.Value, this.m_mapMapper.DpiX);
				}
				else if (hasScope)
				{
					num = MappingHelper.ToIntPixels(mapLineTemplate.Instance.Width, this.m_mapMapper.DpiX);
				}
				else
				{
					num = LineTemplateMapper.GetDefaultSize(this.m_mapMapper.DpiX);
				}
			}
			else
			{
				num = LineTemplateMapper.GetDefaultSize(this.m_mapMapper.DpiX);
			}
			return num;
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00043536 File Offset: 0x00041736
		internal static int GetDefaultSize(float dpi)
		{
			return MappingHelper.ToIntPixels(LineTemplateMapper.m_defaultLineWidth, dpi);
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00043543 File Offset: 0x00041743
		private MapLineLayer MapLineLayer
		{
			get
			{
				return (MapLineLayer)this.m_mapVectorLayer;
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00043550 File Offset: 0x00041750
		private bool UseCustomTemplate(MapLine mapLine, bool hasScope)
		{
			if (mapLine == null)
			{
				return false;
			}
			bool flag = false;
			ReportBoolProperty useCustomLineTemplate = mapLine.UseCustomLineTemplate;
			if (useCustomLineTemplate != null)
			{
				if (!useCustomLineTemplate.IsExpression)
				{
					flag = useCustomLineTemplate.Value;
				}
				else if (hasScope)
				{
					flag = mapLine.Instance.UseCustomLineTemplate;
				}
			}
			return flag;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0004358F File Offset: 0x0004178F
		private PathLabelPosition GetLabelPosition(MapLineLabelPlacement placement)
		{
			if (placement == MapLineLabelPlacement.Center)
			{
				return 2;
			}
			if (placement == MapLineLabelPlacement.Below)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0004359E File Offset: 0x0004179E
		protected override MapSpatialElementTemplate DefaultTemplate
		{
			get
			{
				return this.MapLineLayer.MapLineTemplate;
			}
		}

		// Token: 0x04000732 RID: 1842
		private LineLayerMapper m_lineLayerMapper;

		// Token: 0x04000733 RID: 1843
		private static string m_defaultLineSizeString = "3.75pt";

		// Token: 0x04000734 RID: 1844
		private static ReportSize m_defaultLineWidth = new ReportSize(LineTemplateMapper.m_defaultLineSizeString);
	}
}
