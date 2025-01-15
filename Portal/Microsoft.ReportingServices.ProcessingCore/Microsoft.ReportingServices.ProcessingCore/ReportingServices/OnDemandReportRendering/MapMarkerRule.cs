using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001C9 RID: 457
	public sealed class MapMarkerRule : MapAppearanceRule
	{
		// Token: 0x060011D3 RID: 4563 RVA: 0x00049BB7 File Offset: 0x00047DB7
		internal MapMarkerRule(MapMarkerRule defObject, MapVectorLayer mapVectorLayer, Map map)
			: base(defObject, mapVectorLayer, map)
		{
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00049BC2 File Offset: 0x00047DC2
		public MapMarkerCollection MapMarkers
		{
			get
			{
				if (this.m_mapMarkers == null && this.MapMarkerRuleDef.MapMarkers != null)
				{
					this.m_mapMarkers = new MapMarkerCollection(this, this.m_map);
				}
				return this.m_mapMarkers;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00049BF1 File Offset: 0x00047DF1
		internal MapMarkerRule MapMarkerRuleDef
		{
			get
			{
				return (MapMarkerRule)base.MapAppearanceRuleDef;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00049BFE File Offset: 0x00047DFE
		public new MapMarkerRuleInstance Instance
		{
			get
			{
				return (MapMarkerRuleInstance)this.GetInstance();
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00049C0B File Offset: 0x00047E0B
		internal override MapAppearanceRuleInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapMarkerRuleInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00049C3B File Offset: 0x00047E3B
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapMarkers != null)
			{
				this.m_mapMarkers.SetNewContext();
			}
		}

		// Token: 0x04000871 RID: 2161
		private MapMarkerCollection m_mapMarkers;
	}
}
