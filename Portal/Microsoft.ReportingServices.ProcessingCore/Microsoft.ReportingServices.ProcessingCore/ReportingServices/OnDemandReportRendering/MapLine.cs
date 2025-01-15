using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001E1 RID: 481
	public sealed class MapLine : MapSpatialElement
	{
		// Token: 0x06001257 RID: 4695 RVA: 0x0004B15C File Offset: 0x0004935C
		internal MapLine(MapLine defObject, MapLineLayer mapVectorLayer, Map map)
			: base(defObject, mapVectorLayer, map)
		{
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x0004B167 File Offset: 0x00049367
		public ReportBoolProperty UseCustomLineTemplate
		{
			get
			{
				if (this.m_useCustomLineTemplate == null && this.MapLineDef.UseCustomLineTemplate != null)
				{
					this.m_useCustomLineTemplate = new ReportBoolProperty(this.MapLineDef.UseCustomLineTemplate);
				}
				return this.m_useCustomLineTemplate;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0004B19C File Offset: 0x0004939C
		public MapLineTemplate MapLineTemplate
		{
			get
			{
				if (this.m_mapLineTemplate == null && this.MapLineDef.MapLineTemplate != null)
				{
					this.m_mapLineTemplate = new MapLineTemplate(this.MapLineDef.MapLineTemplate, (MapLineLayer)this.m_mapVectorLayer, this.m_map);
				}
				return this.m_mapLineTemplate;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x0004B1EB File Offset: 0x000493EB
		internal MapLine MapLineDef
		{
			get
			{
				return (MapLine)base.MapSpatialElementDef;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0004B1F8 File Offset: 0x000493F8
		public new MapLineInstance Instance
		{
			get
			{
				return (MapLineInstance)this.GetInstance();
			}
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0004B205 File Offset: 0x00049405
		internal override MapSpatialElementInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapLineInstance(this);
			}
			return (MapSpatialElementInstance)this.m_instance;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0004B23A File Offset: 0x0004943A
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
		}

		// Token: 0x040008B9 RID: 2233
		private ReportBoolProperty m_useCustomLineTemplate;

		// Token: 0x040008BA RID: 2234
		private MapLineTemplate m_mapLineTemplate;
	}
}
