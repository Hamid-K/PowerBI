using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001CC RID: 460
	public sealed class MapCustomColorRule : MapColorRule
	{
		// Token: 0x060011E7 RID: 4583 RVA: 0x00049F61 File Offset: 0x00048161
		internal MapCustomColorRule(MapCustomColorRule defObject, MapVectorLayer mapVectorLayer, Map map)
			: base(defObject, mapVectorLayer, map)
		{
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00049F6C File Offset: 0x0004816C
		public MapCustomColorCollection MapCustomColors
		{
			get
			{
				if (this.m_mapCustomColors == null && this.MapCustomColorRuleDef.MapCustomColors != null)
				{
					this.m_mapCustomColors = new MapCustomColorCollection(this, this.m_map);
				}
				return this.m_mapCustomColors;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00049F9B File Offset: 0x0004819B
		internal MapCustomColorRule MapCustomColorRuleDef
		{
			get
			{
				return (MapCustomColorRule)base.MapAppearanceRuleDef;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x00049FA8 File Offset: 0x000481A8
		public new MapCustomColorRuleInstance Instance
		{
			get
			{
				return (MapCustomColorRuleInstance)this.GetInstance();
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00049FB5 File Offset: 0x000481B5
		internal override MapAppearanceRuleInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapCustomColorRuleInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00049FE5 File Offset: 0x000481E5
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapCustomColors != null)
			{
				this.m_mapCustomColors.SetNewContext();
			}
		}

		// Token: 0x0400087C RID: 2172
		private MapCustomColorCollection m_mapCustomColors;
	}
}
