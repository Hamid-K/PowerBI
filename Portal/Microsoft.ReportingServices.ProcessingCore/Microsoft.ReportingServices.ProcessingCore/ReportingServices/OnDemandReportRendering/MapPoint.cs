using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001E4 RID: 484
	public sealed class MapPoint : MapSpatialElement
	{
		// Token: 0x06001270 RID: 4720 RVA: 0x0004B4BE File Offset: 0x000496BE
		internal MapPoint(MapPoint defObject, MapPointLayer mapPointLayer, Map map)
			: base(defObject, mapPointLayer, map)
		{
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x0004B4C9 File Offset: 0x000496C9
		public ReportBoolProperty UseCustomPointTemplate
		{
			get
			{
				if (this.m_useCustomPointTemplate == null && this.MapPointDef.UseCustomPointTemplate != null)
				{
					this.m_useCustomPointTemplate = new ReportBoolProperty(this.MapPointDef.UseCustomPointTemplate);
				}
				return this.m_useCustomPointTemplate;
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0004B4FC File Offset: 0x000496FC
		public MapPointTemplate MapPointTemplate
		{
			get
			{
				if (this.m_mapPointTemplate == null)
				{
					MapPointTemplate mapPointTemplate = this.MapPointDef.MapPointTemplate;
					if (mapPointTemplate != null && mapPointTemplate is MapMarkerTemplate)
					{
						this.m_mapPointTemplate = new MapMarkerTemplate((MapMarkerTemplate)mapPointTemplate, this.m_mapVectorLayer, this.m_map);
					}
				}
				return this.m_mapPointTemplate;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x0004B54B File Offset: 0x0004974B
		internal MapPoint MapPointDef
		{
			get
			{
				return (MapPoint)base.MapSpatialElementDef;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x0004B558 File Offset: 0x00049758
		public new MapPointInstance Instance
		{
			get
			{
				return (MapPointInstance)this.GetInstance();
			}
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0004B565 File Offset: 0x00049765
		internal override MapSpatialElementInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapPointInstance(this);
			}
			return (MapSpatialElementInstance)this.m_instance;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0004B59A File Offset: 0x0004979A
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapPointTemplate != null)
			{
				this.m_mapPointTemplate.SetNewContext();
			}
		}

		// Token: 0x040008C3 RID: 2243
		private ReportBoolProperty m_useCustomPointTemplate;

		// Token: 0x040008C4 RID: 2244
		private MapPointTemplate m_mapPointTemplate;
	}
}
