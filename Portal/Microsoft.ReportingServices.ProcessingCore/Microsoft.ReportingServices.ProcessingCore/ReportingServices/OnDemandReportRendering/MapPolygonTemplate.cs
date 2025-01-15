using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001DC RID: 476
	public sealed class MapPolygonTemplate : MapSpatialElementTemplate
	{
		// Token: 0x06001227 RID: 4647 RVA: 0x0004AA9E File Offset: 0x00048C9E
		internal MapPolygonTemplate(MapPolygonTemplate defObject, MapPolygonLayer shapeLayer, Map map)
			: base(defObject, shapeLayer, map)
		{
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x0004AAA9 File Offset: 0x00048CA9
		public ReportDoubleProperty ScaleFactor
		{
			get
			{
				if (this.m_scaleFactor == null && this.MapPolygonTemplateDef.ScaleFactor != null)
				{
					this.m_scaleFactor = new ReportDoubleProperty(this.MapPolygonTemplateDef.ScaleFactor);
				}
				return this.m_scaleFactor;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x0004AADC File Offset: 0x00048CDC
		public ReportDoubleProperty CenterPointOffsetX
		{
			get
			{
				if (this.m_centerPointOffsetX == null && this.MapPolygonTemplateDef.CenterPointOffsetX != null)
				{
					this.m_centerPointOffsetX = new ReportDoubleProperty(this.MapPolygonTemplateDef.CenterPointOffsetX);
				}
				return this.m_centerPointOffsetX;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0004AB0F File Offset: 0x00048D0F
		public ReportDoubleProperty CenterPointOffsetY
		{
			get
			{
				if (this.m_centerPointOffsetY == null && this.MapPolygonTemplateDef.CenterPointOffsetY != null)
				{
					this.m_centerPointOffsetY = new ReportDoubleProperty(this.MapPolygonTemplateDef.CenterPointOffsetY);
				}
				return this.m_centerPointOffsetY;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x0004AB44 File Offset: 0x00048D44
		public ReportEnumProperty<MapAutoBool> ShowLabel
		{
			get
			{
				if (this.m_showLabel == null && this.MapPolygonTemplateDef.ShowLabel != null)
				{
					this.m_showLabel = new ReportEnumProperty<MapAutoBool>(this.MapPolygonTemplateDef.ShowLabel.IsExpression, this.MapPolygonTemplateDef.ShowLabel.OriginalText, EnumTranslator.TranslateMapAutoBool(this.MapPolygonTemplateDef.ShowLabel.StringValue, null));
				}
				return this.m_showLabel;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0004ABB0 File Offset: 0x00048DB0
		public ReportEnumProperty<MapPolygonLabelPlacement> LabelPlacement
		{
			get
			{
				if (this.m_labelPlacement == null && this.MapPolygonTemplateDef.LabelPlacement != null)
				{
					this.m_labelPlacement = new ReportEnumProperty<MapPolygonLabelPlacement>(this.MapPolygonTemplateDef.LabelPlacement.IsExpression, this.MapPolygonTemplateDef.LabelPlacement.OriginalText, EnumTranslator.TranslateMapPolygonLabelPlacement(this.MapPolygonTemplateDef.LabelPlacement.StringValue, null));
				}
				return this.m_labelPlacement;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x0004AC19 File Offset: 0x00048E19
		internal MapPolygonTemplate MapPolygonTemplateDef
		{
			get
			{
				return (MapPolygonTemplate)base.MapSpatialElementTemplateDef;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0004AC26 File Offset: 0x00048E26
		public new MapPolygonTemplateInstance Instance
		{
			get
			{
				return (MapPolygonTemplateInstance)this.GetInstance();
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0004AC33 File Offset: 0x00048E33
		internal override MapSpatialElementTemplateInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapPolygonTemplateInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0004AC63 File Offset: 0x00048E63
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x040008A3 RID: 2211
		private ReportDoubleProperty m_scaleFactor;

		// Token: 0x040008A4 RID: 2212
		private ReportDoubleProperty m_centerPointOffsetX;

		// Token: 0x040008A5 RID: 2213
		private ReportDoubleProperty m_centerPointOffsetY;

		// Token: 0x040008A6 RID: 2214
		private ReportEnumProperty<MapAutoBool> m_showLabel;

		// Token: 0x040008A7 RID: 2215
		private ReportEnumProperty<MapPolygonLabelPlacement> m_labelPlacement;
	}
}
