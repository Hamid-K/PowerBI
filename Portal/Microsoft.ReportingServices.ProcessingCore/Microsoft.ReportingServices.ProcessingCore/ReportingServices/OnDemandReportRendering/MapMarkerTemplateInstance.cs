using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001E8 RID: 488
	public sealed class MapMarkerTemplateInstance : MapPointTemplateInstance
	{
		// Token: 0x0600128B RID: 4747 RVA: 0x0004BBD6 File Offset: 0x00049DD6
		internal MapMarkerTemplateInstance(MapMarkerTemplate defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0004BBE6 File Offset: 0x00049DE6
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}

		// Token: 0x040008D9 RID: 2265
		private MapMarkerTemplate m_defObject;
	}
}
