using System;
using Microsoft.Reporting.Map.WebForms;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000170 RID: 368
	internal class SymbolMarkerTemplateMapper : PointTemplateMapper
	{
		// Token: 0x06000F74 RID: 3956 RVA: 0x00043286 File Offset: 0x00041486
		internal SymbolMarkerTemplateMapper(MapMapper mapMapper, VectorLayerMapper vectorLayerMapper, MapVectorLayer mapVectorLayer)
			: base(mapMapper, vectorLayerMapper, mapVectorLayer)
		{
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00043294 File Offset: 0x00041494
		protected override void RenderPointTemplate(MapPointTemplate mapPointTemplate, Symbol coreSymbol, bool customTemplate, bool ignoreBackgoundColor, bool ignoreSize, bool ignoreMarker, bool hasScope)
		{
			base.RenderPointTemplate(mapPointTemplate, coreSymbol, customTemplate, ignoreBackgoundColor, ignoreSize, ignoreMarker, hasScope);
			if (!ignoreMarker)
			{
				MapMarker mapMarker = ((MapMarkerTemplate)mapPointTemplate).MapMarker;
				MapMarkerStyle markerStyle = MapMapper.GetMarkerStyle(mapMarker, hasScope);
				if (markerStyle != MapMarkerStyle.Image)
				{
					coreSymbol.MarkerStyle = MapMapper.GetMarkerStyle(markerStyle);
					return;
				}
				MapMarkerImage mapMarkerImage = mapMarker.MapMarkerImage;
				if (mapMarkerImage == null)
				{
					throw new RenderingObjectModelException(RPResWrapper.rsMapLayerMissingProperty(RPRes.rsObjectTypeMap, this.m_mapVectorLayer.MapDef.Name, this.m_mapVectorLayer.Name, "MapMarkerImage"));
				}
				string text;
				if (this.CanShareMarkerImage(mapMarkerImage, customTemplate))
				{
					if (this.sharedImageName == null)
					{
						this.sharedImageName = this.m_mapMapper.AddImage(mapMarkerImage);
					}
					text = this.sharedImageName;
				}
				else
				{
					text = this.m_mapMapper.AddImage(mapMarkerImage);
				}
				coreSymbol.Image = text;
				coreSymbol.ImageResizeMode = this.m_mapMapper.GetImageResizeMode(mapMarkerImage);
				coreSymbol.ImageTransColor = this.m_mapMapper.GetImageTransColor(mapMarkerImage);
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0004337F File Offset: 0x0004157F
		private bool CanShareMarkerImage(MapMarkerImage mapMarkerImage, bool customTemplate)
		{
			return !mapMarkerImage.MIMEType.IsExpression && !mapMarkerImage.Value.IsExpression && !customTemplate;
		}

		// Token: 0x04000731 RID: 1841
		private string sharedImageName;
	}
}
