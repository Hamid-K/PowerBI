using System;
using Microsoft.Reporting.Map.WebForms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000166 RID: 358
	internal class PolygonLayerMapper : VectorLayerMapper
	{
		// Token: 0x06000ECF RID: 3791 RVA: 0x000405FE File Offset: 0x0003E7FE
		internal PolygonLayerMapper(MapPolygonLayer mapPolygonLayer, MapControl coreMap, MapMapper mapMapper)
			: base(mapPolygonLayer, coreMap, mapMapper)
		{
			this.m_polygonTemplateMapper = new PolygonTemplateMapper(this.m_mapMapper, this, this.MapPolygonLayer);
			this.m_pointTemplateMapper = base.CreatePointTemplateMapper();
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0004062D File Offset: 0x0003E82D
		protected override ISpatialElementCollection SpatialElementCollection
		{
			get
			{
				return this.MapPolygonLayer.MapPolygons;
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0004063A File Offset: 0x0003E83A
		protected override CoreSpatialElementManager GetSpatialElementManager()
		{
			if (this.m_shapeManager == null)
			{
				this.m_shapeManager = new CoreShapeManager(this.m_coreMap, this.m_mapVectorLayer);
			}
			return this.m_shapeManager;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00040661 File Offset: 0x0003E861
		internal bool HasColorRule(Shape shape)
		{
			return this.HasColorRule() && this.m_polygonColorRuleMapper.HasDataValue(shape);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0004067C File Offset: 0x0003E87C
		private bool HasColorRule()
		{
			MapPolygonRules mapPolygonRules = this.MapPolygonLayer.MapPolygonRules;
			MapColorRule mapColorRule;
			if (mapPolygonRules != null)
			{
				mapColorRule = mapPolygonRules.MapColorRule;
			}
			else
			{
				mapColorRule = null;
			}
			return mapColorRule != null;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x000406A8 File Offset: 0x0003E8A8
		protected override void CreateRules()
		{
			MapPolygonRules mapPolygonRules = this.MapPolygonLayer.MapPolygonRules;
			if (mapPolygonRules != null && mapPolygonRules.MapColorRule != null)
			{
				this.m_polygonColorRuleMapper = new ColorRuleMapper(mapPolygonRules.MapColorRule, this, this.GetSpatialElementManager());
				this.m_polygonColorRuleMapper.CreatePolygonRule();
			}
			MapPointRules mapCenterPointRules = this.MapPolygonLayer.MapCenterPointRules;
			if (mapCenterPointRules != null)
			{
				base.CreatePointRules(mapCenterPointRules);
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00040708 File Offset: 0x0003E908
		protected override void RenderRules()
		{
			MapPolygonRules mapPolygonRules = this.MapPolygonLayer.MapPolygonRules;
			if (mapPolygonRules != null && mapPolygonRules.MapColorRule != null)
			{
				this.m_polygonColorRuleMapper.RenderPolygonRule(this.m_polygonTemplateMapper);
			}
			MapPointRules mapCenterPointRules = this.MapPolygonLayer.MapCenterPointRules;
			if (mapCenterPointRules != null)
			{
				base.RenderPointRules(mapCenterPointRules);
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00040754 File Offset: 0x0003E954
		protected override void RenderSpatialElement(SpatialElementInfo spatialElementInfo, bool hasScope)
		{
			base.InitializeSpatialElement(spatialElementInfo.CoreSpatialElement);
			if (hasScope)
			{
				this.RenderPolygonRulesField((Shape)spatialElementInfo.CoreSpatialElement);
			}
			this.RenderPolygonTemplate((MapPolygon)spatialElementInfo.MapSpatialElement, (Shape)spatialElementInfo.CoreSpatialElement, hasScope);
			this.RenderPolygonCenterPoint(spatialElementInfo, hasScope);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x000407A6 File Offset: 0x0003E9A6
		internal override MapPointRules GetMapPointRules()
		{
			return this.MapPolygonLayer.MapCenterPointRules;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000407B3 File Offset: 0x0003E9B3
		internal override MapPointTemplate GetMapPointTemplate()
		{
			return this.MapPolygonLayer.MapCenterPointTemplate;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x000407C0 File Offset: 0x0003E9C0
		private bool HasCenterPointRule()
		{
			return base.HasPointColorRule() || base.HasPointSizeRule() || base.HasMarkerRule();
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x000407DA File Offset: 0x0003E9DA
		private bool HasCenterPointTemplate(MapPolygon mapPolygon, MapPointTemplate pointTemplate, bool hasScope)
		{
			if (mapPolygon == null || !PointTemplateMapper.PolygonUseCustomTemplate(mapPolygon, hasScope))
			{
				return pointTemplate != null;
			}
			return mapPolygon.MapCenterPointTemplate != null;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x000407F8 File Offset: 0x0003E9F8
		private void RenderPolygonCenterPoint(SpatialElementInfo spatialElementInfo, bool hasScope)
		{
			if (this.HasCenterPointRule() || this.HasCenterPointTemplate((MapPolygon)spatialElementInfo.MapSpatialElement, this.MapPolygonLayer.MapCenterPointTemplate, hasScope))
			{
				Symbol symbol = (Symbol)base.GetSymbolManager().CreateSpatialElement();
				symbol.Layer = spatialElementInfo.CoreSpatialElement.Layer;
				symbol.Category = spatialElementInfo.CoreSpatialElement.Category;
				symbol.ParentShape = spatialElementInfo.CoreSpatialElement.Name;
				this.CopyFieldsToPoint((Shape)spatialElementInfo.CoreSpatialElement, symbol);
				base.GetSymbolManager().AddSpatialElement(symbol);
				base.RenderPoint(spatialElementInfo.MapSpatialElement, symbol, hasScope);
			}
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0004089C File Offset: 0x0003EA9C
		private void CopyFieldsToPoint(Shape shape, Symbol symbol)
		{
			foreach (object obj in shape.fields.Keys)
			{
				string text = (string)obj;
				this.CopyFieldToPoint(shape, symbol, text);
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x000408FC File Offset: 0x0003EAFC
		private void CopyFieldToPoint(Shape shape, Symbol symbol, string fieldName)
		{
			if (this.m_coreMap.SymbolFields.GetByName(fieldName) == null)
			{
				Field field = new Field();
				field.Name = fieldName;
				field.Type = ((Field)this.m_coreMap.ShapeFields.GetByName(fieldName)).Type;
				this.m_coreMap.SymbolFields.Add(field);
			}
			symbol[fieldName] = shape[fieldName];
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0004096A File Offset: 0x0003EB6A
		private void RenderPolygonRulesField(Shape shape)
		{
			if (this.m_polygonColorRuleMapper != null)
			{
				this.m_polygonColorRuleMapper.SetRuleFieldValue(shape);
			}
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00040980 File Offset: 0x0003EB80
		private void RenderPolygonTemplate(MapPolygon mapPolygon, Shape coreShape, bool hasScope)
		{
			this.m_polygonTemplateMapper.Render(mapPolygon, coreShape, hasScope);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00040990 File Offset: 0x0003EB90
		protected override void RenderSymbolTemplate(MapSpatialElement mapSpatialElement, Symbol coreSymbol, bool hasScope)
		{
			this.m_pointTemplateMapper.RenderPolygonCenterPoint((MapPolygon)mapSpatialElement, coreSymbol, hasScope);
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x000409A5 File Offset: 0x0003EBA5
		private MapPolygonLayer MapPolygonLayer
		{
			get
			{
				return (MapPolygonLayer)this.m_mapVectorLayer;
			}
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x000409B2 File Offset: 0x0003EBB2
		internal override bool IsValidSpatialElement(ISpatialElement spatialElement)
		{
			return spatialElement is Shape;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x000409BD File Offset: 0x0003EBBD
		internal override void OnSpatialElementAdded(ISpatialElement spatialElement)
		{
			this.m_mapMapper.Simplify((Shape)spatialElement);
		}

		// Token: 0x04000712 RID: 1810
		private CoreShapeManager m_shapeManager;

		// Token: 0x04000713 RID: 1811
		private ColorRuleMapper m_polygonColorRuleMapper;

		// Token: 0x04000714 RID: 1812
		private PolygonTemplateMapper m_polygonTemplateMapper;
	}
}
