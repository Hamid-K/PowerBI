using System;
using System.Drawing;
using Microsoft.Reporting.Map.WebForms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000168 RID: 360
	internal class LineLayerMapper : VectorLayerMapper
	{
		// Token: 0x06000EF0 RID: 3824 RVA: 0x00040ADF File Offset: 0x0003ECDF
		internal LineLayerMapper(MapLineLayer mapLineLayer, MapControl coreMap, MapMapper mapMapper)
			: base(mapLineLayer, coreMap, mapMapper)
		{
			if (mapLineLayer.MapLineTemplate != null)
			{
				this.m_lineTemplateMapper = new LineTemplateMapper(this.m_mapMapper, this, this.MapLineLayer);
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00040B0A File Offset: 0x0003ED0A
		protected override ISpatialElementCollection SpatialElementCollection
		{
			get
			{
				return this.MapLineLayer.MapLines;
			}
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00040B17 File Offset: 0x0003ED17
		protected override CoreSpatialElementManager GetSpatialElementManager()
		{
			if (this.m_pathManager == null)
			{
				this.m_pathManager = new CorePathManager(this.m_coreMap, this.m_mapVectorLayer);
			}
			return this.m_pathManager;
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00040B3E File Offset: 0x0003ED3E
		internal bool HasColorRule(Path path)
		{
			return this.HasColorRule() && this.m_lineColorRuleMapper.HasDataValue(path);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00040B58 File Offset: 0x0003ED58
		private bool HasColorRule()
		{
			MapLineRules mapLineRules = this.MapLineLayer.MapLineRules;
			MapColorRule mapColorRule;
			if (mapLineRules != null)
			{
				mapColorRule = mapLineRules.MapColorRule;
			}
			else
			{
				mapColorRule = null;
			}
			return mapColorRule != null;
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00040B83 File Offset: 0x0003ED83
		internal bool HasSizeRule(Path path)
		{
			return this.HasSizeRule() && this.m_lineSizeRuleMapper.HasDataValue(path);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00040B9C File Offset: 0x0003ED9C
		private bool HasSizeRule()
		{
			MapLineRules mapLineRules = this.MapLineLayer.MapLineRules;
			MapSizeRule mapSizeRule;
			if (mapLineRules != null)
			{
				mapSizeRule = mapLineRules.MapSizeRule;
			}
			else
			{
				mapSizeRule = null;
			}
			return mapSizeRule != null;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00040BC8 File Offset: 0x0003EDC8
		protected override void CreateRules()
		{
			MapLineRules mapLineRules = this.MapLineLayer.MapLineRules;
			if (mapLineRules != null)
			{
				if (mapLineRules.MapColorRule != null)
				{
					this.m_lineColorRuleMapper = new ColorRuleMapper(mapLineRules.MapColorRule, this, this.GetSpatialElementManager());
					this.m_lineColorRuleMapper.CreatePathRule();
				}
				if (mapLineRules.MapSizeRule != null)
				{
					this.m_lineSizeRuleMapper = new SizeRuleMapper(mapLineRules.MapSizeRule, this, this.GetSpatialElementManager());
					this.m_lineSizeRuleMapper.CreatePathRule();
				}
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00040C3C File Offset: 0x0003EE3C
		protected override void RenderRules()
		{
			MapLineRules mapLineRules = this.MapLineLayer.MapLineRules;
			if (mapLineRules != null)
			{
				if (mapLineRules.MapColorRule != null)
				{
					this.m_lineColorRuleMapper.RenderLineRule(this.m_lineTemplateMapper, this.GetLegendSize());
				}
				if (mapLineRules.MapSizeRule != null)
				{
					this.m_lineSizeRuleMapper.RenderLineRule(this.m_lineTemplateMapper, this.GetLegendColor());
				}
			}
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00040C96 File Offset: 0x0003EE96
		private Color? GetLegendColor()
		{
			return new Color?(this.m_lineTemplateMapper.GetBackgroundColor(false));
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00040CA9 File Offset: 0x0003EEA9
		private int? GetLegendSize()
		{
			if (this.m_lineTemplateMapper == null)
			{
				return new int?(LineTemplateMapper.GetDefaultSize(this.m_mapMapper.DpiX));
			}
			return new int?(this.m_lineTemplateMapper.GetSize(this.MapLineLayer.MapLineTemplate, false));
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00040CE5 File Offset: 0x0003EEE5
		protected override void RenderSpatialElement(SpatialElementInfo spatialElementInfo, bool hasScope)
		{
			base.InitializeSpatialElement(spatialElementInfo.CoreSpatialElement);
			if (hasScope)
			{
				this.RenderLineRuleFields((Path)spatialElementInfo.CoreSpatialElement);
			}
			this.RenderLineTemplate((MapLine)spatialElementInfo.MapSpatialElement, (Path)spatialElementInfo.CoreSpatialElement, hasScope);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00040D24 File Offset: 0x0003EF24
		protected void RenderLineRuleFields(Path corePath)
		{
			if (this.m_lineColorRuleMapper != null)
			{
				this.m_lineColorRuleMapper.SetRuleFieldValue(corePath);
			}
			if (this.m_lineSizeRuleMapper != null)
			{
				this.m_lineSizeRuleMapper.SetRuleFieldValue(corePath);
			}
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00040D4E File Offset: 0x0003EF4E
		private void RenderLineTemplate(MapLine mapLine, Path path, bool hasScope)
		{
			this.m_lineTemplateMapper.Render(mapLine, path, hasScope);
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00040D5E File Offset: 0x0003EF5E
		private MapLineLayer MapLineLayer
		{
			get
			{
				return (MapLineLayer)this.m_mapVectorLayer;
			}
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00040D6B File Offset: 0x0003EF6B
		internal override bool IsValidSpatialElement(ISpatialElement spatialElement)
		{
			return spatialElement is Path;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00040D76 File Offset: 0x0003EF76
		internal override void OnSpatialElementAdded(ISpatialElement spatialElement)
		{
			this.m_mapMapper.Simplify((Path)spatialElement);
		}

		// Token: 0x04000716 RID: 1814
		private CorePathManager m_pathManager;

		// Token: 0x04000717 RID: 1815
		private ColorRuleMapper m_lineColorRuleMapper;

		// Token: 0x04000718 RID: 1816
		private SizeRuleMapper m_lineSizeRuleMapper;

		// Token: 0x04000719 RID: 1817
		private LineTemplateMapper m_lineTemplateMapper;
	}
}
