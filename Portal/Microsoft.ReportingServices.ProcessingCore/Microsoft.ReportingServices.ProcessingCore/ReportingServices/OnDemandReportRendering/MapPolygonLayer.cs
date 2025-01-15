using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001AA RID: 426
	public sealed class MapPolygonLayer : MapVectorLayer
	{
		// Token: 0x06001104 RID: 4356 RVA: 0x00047ADD File Offset: 0x00045CDD
		internal MapPolygonLayer(MapPolygonLayer defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x00047AE7 File Offset: 0x00045CE7
		public MapPolygonTemplate MapPolygonTemplate
		{
			get
			{
				if (this.m_mapPolygonTemplate == null && this.MapPolygonLayerDef.MapPolygonTemplate != null)
				{
					this.m_mapPolygonTemplate = new MapPolygonTemplate(this.MapPolygonLayerDef.MapPolygonTemplate, this, this.m_map);
				}
				return this.m_mapPolygonTemplate;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x00047B21 File Offset: 0x00045D21
		public MapPolygonRules MapPolygonRules
		{
			get
			{
				if (this.m_mapPolygonRules == null && this.MapPolygonLayerDef.MapPolygonRules != null)
				{
					this.m_mapPolygonRules = new MapPolygonRules(this.MapPolygonLayerDef.MapPolygonRules, this, this.m_map);
				}
				return this.m_mapPolygonRules;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x00047B5C File Offset: 0x00045D5C
		public MapPointTemplate MapCenterPointTemplate
		{
			get
			{
				if (this.m_mapCenterPointTemplate == null)
				{
					MapPointTemplate mapCenterPointTemplate = this.MapPolygonLayerDef.MapCenterPointTemplate;
					if (mapCenterPointTemplate != null && mapCenterPointTemplate is MapMarkerTemplate)
					{
						this.m_mapCenterPointTemplate = new MapMarkerTemplate((MapMarkerTemplate)mapCenterPointTemplate, this, this.m_map);
					}
				}
				return this.m_mapCenterPointTemplate;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x00047BA6 File Offset: 0x00045DA6
		public MapPointRules MapCenterPointRules
		{
			get
			{
				if (this.m_mapcenterPointRules == null && this.MapPolygonLayerDef.MapCenterPointRules != null)
				{
					this.m_mapcenterPointRules = new MapPointRules(this.MapPolygonLayerDef.MapCenterPointRules, this, this.m_map);
				}
				return this.m_mapcenterPointRules;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x00047BE0 File Offset: 0x00045DE0
		public MapPolygonCollection MapPolygons
		{
			get
			{
				if (this.m_mapPolygons == null && this.MapPolygonLayerDef.MapPolygons != null)
				{
					this.m_mapPolygons = new MapPolygonCollection(this, this.m_map);
				}
				return this.m_mapPolygons;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x00047C0F File Offset: 0x00045E0F
		internal MapPolygonLayer MapPolygonLayerDef
		{
			get
			{
				return (MapPolygonLayer)base.MapLayerDef;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x00047C1C File Offset: 0x00045E1C
		public new MapPolygonLayerInstance Instance
		{
			get
			{
				return (MapPolygonLayerInstance)this.GetInstance();
			}
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00047C29 File Offset: 0x00045E29
		internal override MapLayerInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapPolygonLayerInstance(this);
			}
			return (MapVectorLayerInstance)this.m_instance;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00047C60 File Offset: 0x00045E60
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapPolygonTemplate != null)
			{
				this.m_mapPolygonTemplate.SetNewContext();
			}
			if (this.m_mapPolygonRules != null)
			{
				this.m_mapPolygonRules.SetNewContext();
			}
			if (this.m_mapCenterPointTemplate != null)
			{
				this.m_mapCenterPointTemplate.SetNewContext();
			}
			if (this.m_mapcenterPointRules != null)
			{
				this.m_mapcenterPointRules.SetNewContext();
			}
			if (this.m_mapPolygons != null)
			{
				this.m_mapPolygons.SetNewContext();
			}
		}

		// Token: 0x0400080E RID: 2062
		private MapPolygonTemplate m_mapPolygonTemplate;

		// Token: 0x0400080F RID: 2063
		private MapPolygonRules m_mapPolygonRules;

		// Token: 0x04000810 RID: 2064
		private MapPointTemplate m_mapCenterPointTemplate;

		// Token: 0x04000811 RID: 2065
		private MapPointRules m_mapcenterPointRules;

		// Token: 0x04000812 RID: 2066
		private MapPolygonCollection m_mapPolygons;
	}
}
