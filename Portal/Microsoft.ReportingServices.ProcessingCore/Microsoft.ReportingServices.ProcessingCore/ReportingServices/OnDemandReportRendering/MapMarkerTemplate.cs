using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001DE RID: 478
	public sealed class MapMarkerTemplate : MapPointTemplate
	{
		// Token: 0x06001244 RID: 4676 RVA: 0x0004AF3A File Offset: 0x0004913A
		internal MapMarkerTemplate(MapMarkerTemplate defObject, MapVectorLayer mapVectorLayer, Map map)
			: base(defObject, mapVectorLayer, map)
		{
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x0004AF45 File Offset: 0x00049145
		public MapMarker MapMarker
		{
			get
			{
				if (this.m_mapMarker == null && this.MapMarkerTemplateDef.MapMarker != null)
				{
					this.m_mapMarker = new MapMarker(this.MapMarkerTemplateDef.MapMarker, this.m_map);
				}
				return this.m_mapMarker;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0004AF7E File Offset: 0x0004917E
		internal MapMarkerTemplate MapMarkerTemplateDef
		{
			get
			{
				return (MapMarkerTemplate)base.MapSpatialElementTemplateDef;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x0004AF8B File Offset: 0x0004918B
		public new MapMarkerTemplateInstance Instance
		{
			get
			{
				return (MapMarkerTemplateInstance)this.GetInstance();
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0004AF98 File Offset: 0x00049198
		internal override MapSpatialElementTemplateInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapMarkerTemplateInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0004AFC8 File Offset: 0x000491C8
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapMarker != null)
			{
				this.m_mapMarker.SetNewContext();
			}
		}

		// Token: 0x040008B4 RID: 2228
		private MapMarker m_mapMarker;
	}
}
