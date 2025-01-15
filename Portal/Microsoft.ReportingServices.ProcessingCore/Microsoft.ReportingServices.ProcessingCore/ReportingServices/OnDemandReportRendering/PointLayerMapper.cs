using System;
using Microsoft.Reporting.Map.WebForms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000167 RID: 359
	internal class PointLayerMapper : VectorLayerMapper
	{
		// Token: 0x06000EE4 RID: 3812 RVA: 0x000409D0 File Offset: 0x0003EBD0
		internal PointLayerMapper(MapPointLayer mapPointLayer, MapControl coreMap, MapMapper mapMapper)
			: base(mapPointLayer, coreMap, mapMapper)
		{
			if (mapPointLayer.MapPointTemplate != null)
			{
				this.m_pointTemplateMapper = base.CreatePointTemplateMapper();
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x000409EF File Offset: 0x0003EBEF
		protected override ISpatialElementCollection SpatialElementCollection
		{
			get
			{
				return this.MapPointLayer.MapPoints;
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000409FC File Offset: 0x0003EBFC
		protected override CoreSpatialElementManager GetSpatialElementManager()
		{
			if (this.m_symbolManager == null)
			{
				this.m_symbolManager = new CoreSymbolManager(this.m_coreMap, this.m_mapVectorLayer);
			}
			return this.m_symbolManager;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00040A24 File Offset: 0x0003EC24
		protected override void CreateRules()
		{
			MapPointRules mapPointRules = this.MapPointLayer.MapPointRules;
			if (mapPointRules != null)
			{
				base.CreatePointRules(mapPointRules);
			}
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00040A48 File Offset: 0x0003EC48
		protected override void RenderRules()
		{
			MapPointRules mapPointRules = this.MapPointLayer.MapPointRules;
			if (mapPointRules != null)
			{
				base.RenderPointRules(mapPointRules);
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00040A6B File Offset: 0x0003EC6B
		protected override void RenderSpatialElement(SpatialElementInfo spatialElementInfo, bool hasScope)
		{
			base.InitializeSpatialElement(spatialElementInfo.CoreSpatialElement);
			base.RenderPoint((MapPoint)spatialElementInfo.MapSpatialElement, (Symbol)spatialElementInfo.CoreSpatialElement, hasScope);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00040A96 File Offset: 0x0003EC96
		protected override void RenderSymbolTemplate(MapSpatialElement mapSpatialElement, Symbol coreSymbol, bool hasScope)
		{
			this.m_pointTemplateMapper.Render((MapPoint)mapSpatialElement, coreSymbol, hasScope);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x00040AAB File Offset: 0x0003ECAB
		internal override MapPointRules GetMapPointRules()
		{
			return this.MapPointLayer.MapPointRules;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00040AB8 File Offset: 0x0003ECB8
		internal override MapPointTemplate GetMapPointTemplate()
		{
			return this.MapPointLayer.MapPointTemplate;
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00040AC5 File Offset: 0x0003ECC5
		private MapPointLayer MapPointLayer
		{
			get
			{
				return (MapPointLayer)this.m_mapVectorLayer;
			}
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00040AD2 File Offset: 0x0003ECD2
		internal override bool IsValidSpatialElement(ISpatialElement spatialElement)
		{
			return spatialElement is Symbol;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00040ADD File Offset: 0x0003ECDD
		internal override void OnSpatialElementAdded(ISpatialElement spatialElement)
		{
		}

		// Token: 0x04000715 RID: 1813
		private CoreSymbolManager m_symbolManager;
	}
}
