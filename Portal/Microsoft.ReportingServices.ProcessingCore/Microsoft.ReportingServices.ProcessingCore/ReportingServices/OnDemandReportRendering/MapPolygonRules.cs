using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001C5 RID: 453
	public sealed class MapPolygonRules
	{
		// Token: 0x060011A8 RID: 4520 RVA: 0x000494E0 File Offset: 0x000476E0
		internal MapPolygonRules(MapPolygonRules defObject, MapPolygonLayer mapPolygonLayer, Map map)
		{
			this.m_defObject = defObject;
			this.m_mapPolygonLayer = mapPolygonLayer;
			this.m_map = map;
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x00049500 File Offset: 0x00047700
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
							this.m_mapColorRule = new MapColorRangeRule((MapColorRangeRule)this.m_defObject.MapColorRule, this.m_mapPolygonLayer, this.m_map);
						}
						else if (mapColorRule is MapColorPaletteRule)
						{
							this.m_mapColorRule = new MapColorPaletteRule((MapColorPaletteRule)this.m_defObject.MapColorRule, this.m_mapPolygonLayer, this.m_map);
						}
						else if (mapColorRule is MapCustomColorRule)
						{
							this.m_mapColorRule = new MapCustomColorRule((MapCustomColorRule)this.m_defObject.MapColorRule, this.m_mapPolygonLayer, this.m_map);
						}
					}
				}
				return this.m_mapColorRule;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x000495C1 File Offset: 0x000477C1
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x000495C9 File Offset: 0x000477C9
		internal MapPolygonRules MapPolygonRulesDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x000495D1 File Offset: 0x000477D1
		public MapPolygonRulesInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapPolygonRulesInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00049601 File Offset: 0x00047801
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapColorRule != null)
			{
				this.m_mapColorRule.SetNewContext();
			}
		}

		// Token: 0x0400085E RID: 2142
		private Map m_map;

		// Token: 0x0400085F RID: 2143
		private MapPolygonLayer m_mapPolygonLayer;

		// Token: 0x04000860 RID: 2144
		private MapPolygonRules m_defObject;

		// Token: 0x04000861 RID: 2145
		private MapPolygonRulesInstance m_instance;

		// Token: 0x04000862 RID: 2146
		private MapColorRule m_mapColorRule;
	}
}
