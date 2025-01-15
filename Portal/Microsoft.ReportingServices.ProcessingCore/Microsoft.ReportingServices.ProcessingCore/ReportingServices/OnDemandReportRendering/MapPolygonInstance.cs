using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001EC RID: 492
	public sealed class MapPolygonInstance : MapSpatialElementInstance
	{
		// Token: 0x06001296 RID: 4758 RVA: 0x0004BD74 File Offset: 0x00049F74
		internal MapPolygonInstance(MapPolygon defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x0004BD84 File Offset: 0x00049F84
		public bool UseCustomPolygonTemplate
		{
			get
			{
				if (this.m_useCustomPolygonTemplate == null)
				{
					this.m_useCustomPolygonTemplate = new bool?(((MapPolygon)this.m_defObject.MapSpatialElementDef).EvaluateUseCustomPolygonTemplate(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_useCustomPolygonTemplate.Value;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0004BDE4 File Offset: 0x00049FE4
		public bool UseCustomCenterPointTemplate
		{
			get
			{
				if (this.m_useCustomCenterPointTemplate == null)
				{
					this.m_useCustomCenterPointTemplate = new bool?(((MapPolygon)this.m_defObject.MapSpatialElementDef).EvaluateUseCustomCenterPointTemplate(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_useCustomCenterPointTemplate.Value;
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0004BE44 File Offset: 0x0004A044
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_useCustomPolygonTemplate = null;
			this.m_useCustomCenterPointTemplate = null;
		}

		// Token: 0x040008E0 RID: 2272
		private MapPolygon m_defObject;

		// Token: 0x040008E1 RID: 2273
		private bool? m_useCustomPolygonTemplate;

		// Token: 0x040008E2 RID: 2274
		private bool? m_useCustomCenterPointTemplate;
	}
}
