using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001E2 RID: 482
	public sealed class MapPolygon : MapSpatialElement
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x0004B268 File Offset: 0x00049468
		internal MapPolygon(MapPolygon defObject, MapPolygonLayer mapPolygonLayer, Map map)
			: base(defObject, mapPolygonLayer, map)
		{
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x0004B273 File Offset: 0x00049473
		public ReportBoolProperty UseCustomPolygonTemplate
		{
			get
			{
				if (this.m_useCustomPolygonTemplate == null && this.MapPolygonDef.UseCustomPolygonTemplate != null)
				{
					this.m_useCustomPolygonTemplate = new ReportBoolProperty(this.MapPolygonDef.UseCustomPolygonTemplate);
				}
				return this.m_useCustomPolygonTemplate;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x0004B2A8 File Offset: 0x000494A8
		public MapPolygonTemplate MapPolygonTemplate
		{
			get
			{
				if (this.m_mapPolygonTemplate == null && this.MapPolygonDef.MapPolygonTemplate != null)
				{
					this.m_mapPolygonTemplate = new MapPolygonTemplate(this.MapPolygonDef.MapPolygonTemplate, (MapPolygonLayer)this.m_mapVectorLayer, this.m_map);
				}
				return this.m_mapPolygonTemplate;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x0004B2F7 File Offset: 0x000494F7
		public ReportBoolProperty UseCustomCenterPointTemplate
		{
			get
			{
				if (this.m_useCustomCenterPointTemplate == null && this.MapPolygonDef.UseCustomCenterPointTemplate != null)
				{
					this.m_useCustomCenterPointTemplate = new ReportBoolProperty(this.MapPolygonDef.UseCustomCenterPointTemplate);
				}
				return this.m_useCustomCenterPointTemplate;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0004B32C File Offset: 0x0004952C
		public MapPointTemplate MapCenterPointTemplate
		{
			get
			{
				if (this.m_mapCenterPointTemplate == null)
				{
					MapPointTemplate mapCenterPointTemplate = this.MapPolygonDef.MapCenterPointTemplate;
					if (mapCenterPointTemplate != null && mapCenterPointTemplate is MapMarkerTemplate)
					{
						this.m_mapCenterPointTemplate = new MapMarkerTemplate((MapMarkerTemplate)mapCenterPointTemplate, this.m_mapVectorLayer, this.m_map);
					}
				}
				return this.m_mapCenterPointTemplate;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x0004B37B File Offset: 0x0004957B
		internal MapPolygon MapPolygonDef
		{
			get
			{
				return (MapPolygon)base.MapSpatialElementDef;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0004B388 File Offset: 0x00049588
		public new MapPolygonInstance Instance
		{
			get
			{
				return (MapPolygonInstance)this.GetInstance();
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004B395 File Offset: 0x00049595
		internal override MapSpatialElementInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapPolygonInstance(this);
			}
			return (MapSpatialElementInstance)this.m_instance;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0004B3CC File Offset: 0x000495CC
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
			if (this.m_mapCenterPointTemplate != null)
			{
				this.m_mapCenterPointTemplate.SetNewContext();
			}
		}

		// Token: 0x040008BB RID: 2235
		private ReportBoolProperty m_useCustomPolygonTemplate;

		// Token: 0x040008BC RID: 2236
		private MapPolygonTemplate m_mapPolygonTemplate;

		// Token: 0x040008BD RID: 2237
		private ReportBoolProperty m_useCustomCenterPointTemplate;

		// Token: 0x040008BE RID: 2238
		private MapPointTemplate m_mapCenterPointTemplate;
	}
}
