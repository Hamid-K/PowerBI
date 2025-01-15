using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001EE RID: 494
	public sealed class MapPointInstance : MapSpatialElementInstance
	{
		// Token: 0x0600129C RID: 4764 RVA: 0x0004BE7B File Offset: 0x0004A07B
		internal MapPointInstance(MapPoint defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x0004BE8C File Offset: 0x0004A08C
		public bool UseCustomPointTemplate
		{
			get
			{
				if (this.m_useCustomPointTemplate == null)
				{
					this.m_useCustomPointTemplate = new bool?(((MapPoint)this.m_defObject.MapSpatialElementDef).EvaluateUseCustomPointTemplate(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_useCustomPointTemplate.Value;
			}
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0004BEEC File Offset: 0x0004A0EC
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_useCustomPointTemplate = null;
		}

		// Token: 0x040008E4 RID: 2276
		private MapPoint m_defObject;

		// Token: 0x040008E5 RID: 2277
		private bool? m_useCustomPointTemplate;
	}
}
