using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001EB RID: 491
	public sealed class MapLineInstance : MapSpatialElementInstance
	{
		// Token: 0x06001293 RID: 4755 RVA: 0x0004BCEF File Offset: 0x00049EEF
		internal MapLineInstance(MapLine defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x0004BD00 File Offset: 0x00049F00
		public bool UseCustomLineTemplate
		{
			get
			{
				if (this.m_useCustomLineTemplate == null)
				{
					this.m_useCustomLineTemplate = new bool?(((MapLine)this.m_defObject.MapSpatialElementDef).EvaluateUseCustomLineTemplate(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_useCustomLineTemplate.Value;
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0004BD60 File Offset: 0x00049F60
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_useCustomLineTemplate = null;
		}

		// Token: 0x040008DE RID: 2270
		private MapLine m_defObject;

		// Token: 0x040008DF RID: 2271
		private bool? m_useCustomLineTemplate;
	}
}
