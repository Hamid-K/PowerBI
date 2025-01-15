using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001A8 RID: 424
	public sealed class MapLineLayer : MapVectorLayer
	{
		// Token: 0x060010F5 RID: 4341 RVA: 0x00047893 File Offset: 0x00045A93
		internal MapLineLayer(MapLineLayer defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0004789D File Offset: 0x00045A9D
		public MapLineTemplate MapLineTemplate
		{
			get
			{
				if (this.m_mapLineTemplate == null && this.MapLineLayerDef.MapLineTemplate != null)
				{
					this.m_mapLineTemplate = new MapLineTemplate(this.MapLineLayerDef.MapLineTemplate, this, this.m_map);
				}
				return this.m_mapLineTemplate;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x000478D7 File Offset: 0x00045AD7
		public MapLineRules MapLineRules
		{
			get
			{
				if (this.m_mapLineRules == null && this.MapLineLayerDef.MapLineRules != null)
				{
					this.m_mapLineRules = new MapLineRules(this.MapLineLayerDef.MapLineRules, this, this.m_map);
				}
				return this.m_mapLineRules;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00047911 File Offset: 0x00045B11
		public MapLineCollection MapLines
		{
			get
			{
				if (this.m_mapLines == null && this.MapLineLayerDef.MapLines != null)
				{
					this.m_mapLines = new MapLineCollection(this, this.m_map);
				}
				return this.m_mapLines;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00047940 File Offset: 0x00045B40
		internal MapLineLayer MapLineLayerDef
		{
			get
			{
				return (MapLineLayer)base.MapLayerDef;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0004794D File Offset: 0x00045B4D
		public new MapLineLayerInstance Instance
		{
			get
			{
				return (MapLineLayerInstance)this.GetInstance();
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0004795A File Offset: 0x00045B5A
		internal override MapLayerInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapLineLayerInstance(this);
			}
			return (MapVectorLayerInstance)this.m_instance;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00047990 File Offset: 0x00045B90
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapLineTemplate != null)
			{
				this.m_mapLineTemplate.SetNewContext();
			}
			if (this.m_mapLineRules != null)
			{
				this.m_mapLineRules.SetNewContext();
			}
			if (this.m_mapLines != null)
			{
				this.m_mapLines.SetNewContext();
			}
		}

		// Token: 0x04000809 RID: 2057
		private MapLineTemplate m_mapLineTemplate;

		// Token: 0x0400080A RID: 2058
		private MapLineRules m_mapLineRules;

		// Token: 0x0400080B RID: 2059
		private MapLineCollection m_mapLines;
	}
}
