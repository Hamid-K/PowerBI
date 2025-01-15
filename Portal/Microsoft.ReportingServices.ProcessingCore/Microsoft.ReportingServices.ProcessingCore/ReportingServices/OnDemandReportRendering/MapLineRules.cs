using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001C4 RID: 452
	public sealed class MapLineRules
	{
		// Token: 0x060011A1 RID: 4513 RVA: 0x00049347 File Offset: 0x00047547
		internal MapLineRules(MapLineRules defObject, MapLineLayer mapLineLayer, Map map)
		{
			this.m_defObject = defObject;
			this.m_mapLineLayer = mapLineLayer;
			this.m_map = map;
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00049364 File Offset: 0x00047564
		public MapSizeRule MapSizeRule
		{
			get
			{
				if (this.m_mapSizeRule == null && this.m_defObject.MapSizeRule != null)
				{
					this.m_mapSizeRule = new MapSizeRule(this.m_defObject.MapSizeRule, this.m_mapLineLayer, this.m_map);
				}
				return this.m_mapSizeRule;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x000493A4 File Offset: 0x000475A4
		public MapColorRule MapColorRule
		{
			get
			{
				if (this.m_mapColorRule == null)
				{
					MapColorRule mapColorRule = this.m_defObject.MapColorRule;
					if (mapColorRule != null)
					{
						if (mapColorRule is MapColorRangeRule)
						{
							this.m_mapColorRule = new MapColorRangeRule((MapColorRangeRule)this.m_defObject.MapColorRule, this.m_mapLineLayer, this.m_map);
						}
						else if (mapColorRule is MapColorPaletteRule)
						{
							this.m_mapColorRule = new MapColorPaletteRule((MapColorPaletteRule)this.m_defObject.MapColorRule, this.m_mapLineLayer, this.m_map);
						}
						else if (mapColorRule is MapCustomColorRule)
						{
							this.m_mapColorRule = new MapCustomColorRule((MapCustomColorRule)this.m_defObject.MapColorRule, this.m_mapLineLayer, this.m_map);
						}
					}
				}
				return this.m_mapColorRule;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x00049465 File Offset: 0x00047665
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x0004946D File Offset: 0x0004766D
		internal MapLineRules MapLineRulesDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00049475 File Offset: 0x00047675
		public MapLineRulesInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapLineRulesInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000494A5 File Offset: 0x000476A5
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapSizeRule != null)
			{
				this.m_mapSizeRule.SetNewContext();
			}
			if (this.m_mapColorRule != null)
			{
				this.m_mapColorRule.SetNewContext();
			}
		}

		// Token: 0x04000858 RID: 2136
		private Map m_map;

		// Token: 0x04000859 RID: 2137
		private MapLineLayer m_mapLineLayer;

		// Token: 0x0400085A RID: 2138
		private MapLineRules m_defObject;

		// Token: 0x0400085B RID: 2139
		private MapLineRulesInstance m_instance;

		// Token: 0x0400085C RID: 2140
		private MapSizeRule m_mapSizeRule;

		// Token: 0x0400085D RID: 2141
		private MapColorRule m_mapColorRule;
	}
}
