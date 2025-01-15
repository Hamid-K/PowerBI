using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001CA RID: 458
	public sealed class MapPointRules
	{
		// Token: 0x060011D9 RID: 4569 RVA: 0x00049C69 File Offset: 0x00047E69
		internal MapPointRules(MapPointRules defObject, MapVectorLayer mapVectorLayer, Map map)
		{
			this.m_defObject = defObject;
			this.m_mapVectorLayer = mapVectorLayer;
			this.m_map = map;
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x00049C86 File Offset: 0x00047E86
		public MapSizeRule MapSizeRule
		{
			get
			{
				if (this.m_mapSizeRule == null && this.m_defObject.MapSizeRule != null)
				{
					this.m_mapSizeRule = new MapSizeRule(this.m_defObject.MapSizeRule, this.m_mapVectorLayer, this.m_map);
				}
				return this.m_mapSizeRule;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00049CC8 File Offset: 0x00047EC8
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
							this.m_mapColorRule = new MapColorRangeRule((MapColorRangeRule)this.m_defObject.MapColorRule, this.m_mapVectorLayer, this.m_map);
						}
						else if (mapColorRule is MapColorPaletteRule)
						{
							this.m_mapColorRule = new MapColorPaletteRule((MapColorPaletteRule)this.m_defObject.MapColorRule, this.m_mapVectorLayer, this.m_map);
						}
						else if (mapColorRule is MapCustomColorRule)
						{
							this.m_mapColorRule = new MapCustomColorRule((MapCustomColorRule)this.m_defObject.MapColorRule, this.m_mapVectorLayer, this.m_map);
						}
					}
				}
				return this.m_mapColorRule;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x00049D89 File Offset: 0x00047F89
		public MapMarkerRule MapMarkerRule
		{
			get
			{
				if (this.m_mapMarkerRule == null && this.m_defObject.MapMarkerRule != null)
				{
					this.m_mapMarkerRule = new MapMarkerRule(this.m_defObject.MapMarkerRule, this.m_mapVectorLayer, this.m_map);
				}
				return this.m_mapMarkerRule;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00049DC8 File Offset: 0x00047FC8
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x00049DD0 File Offset: 0x00047FD0
		internal MapPointRules MapMarkerRulesDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x00049DD8 File Offset: 0x00047FD8
		public MapPointRulesInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapPointRulesInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00049E08 File Offset: 0x00048008
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
			if (this.m_mapMarkerRule != null)
			{
				this.m_mapMarkerRule.SetNewContext();
			}
		}

		// Token: 0x04000872 RID: 2162
		private Map m_map;

		// Token: 0x04000873 RID: 2163
		private MapVectorLayer m_mapVectorLayer;

		// Token: 0x04000874 RID: 2164
		private MapPointRules m_defObject;

		// Token: 0x04000875 RID: 2165
		private MapPointRulesInstance m_instance;

		// Token: 0x04000876 RID: 2166
		private MapSizeRule m_mapSizeRule;

		// Token: 0x04000877 RID: 2167
		private MapColorRule m_mapColorRule;

		// Token: 0x04000878 RID: 2168
		private MapMarkerRule m_mapMarkerRule;
	}
}
