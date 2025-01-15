using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001AF RID: 431
	public sealed class MapPointLayer : MapVectorLayer
	{
		// Token: 0x06001128 RID: 4392 RVA: 0x00048058 File Offset: 0x00046258
		internal MapPointLayer(MapPointLayer defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x00048064 File Offset: 0x00046264
		public MapPointTemplate MapPointTemplate
		{
			get
			{
				if (this.m_mapPointTemplate == null)
				{
					MapPointTemplate mapPointTemplate = this.MapPointLayerDef.MapPointTemplate;
					if (mapPointTemplate != null && mapPointTemplate is MapMarkerTemplate)
					{
						this.m_mapPointTemplate = new MapMarkerTemplate((MapMarkerTemplate)mapPointTemplate, this, this.m_map);
					}
				}
				return this.m_mapPointTemplate;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x000480AE File Offset: 0x000462AE
		public MapPointRules MapPointRules
		{
			get
			{
				if (this.m_mapPointRules == null && this.MapPointLayerDef.MapPointRules != null)
				{
					this.m_mapPointRules = new MapPointRules(this.MapPointLayerDef.MapPointRules, this, this.m_map);
				}
				return this.m_mapPointRules;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x000480E8 File Offset: 0x000462E8
		public MapPointCollection MapPoints
		{
			get
			{
				if (this.m_mapPoints == null && this.MapPointLayerDef.MapPoints != null)
				{
					this.m_mapPoints = new MapPointCollection(this, this.m_map);
				}
				return this.m_mapPoints;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x00048117 File Offset: 0x00046317
		internal MapPointLayer MapPointLayerDef
		{
			get
			{
				return (MapPointLayer)base.MapLayerDef;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x00048124 File Offset: 0x00046324
		public new MapPointLayerInstance Instance
		{
			get
			{
				return (MapPointLayerInstance)this.GetInstance();
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00048131 File Offset: 0x00046331
		internal override MapLayerInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapPointLayerInstance(this);
			}
			return (MapVectorLayerInstance)this.m_instance;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00048168 File Offset: 0x00046368
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
			if (this.m_mapPointRules != null)
			{
				this.m_mapPointRules.SetNewContext();
			}
			if (this.m_mapPoints != null)
			{
				this.m_mapPoints.SetNewContext();
			}
		}

		// Token: 0x0400081D RID: 2077
		private MapPointTemplate m_mapPointTemplate;

		// Token: 0x0400081E RID: 2078
		private MapPointRules m_mapPointRules;

		// Token: 0x0400081F RID: 2079
		private MapPointCollection m_mapPoints;
	}
}
