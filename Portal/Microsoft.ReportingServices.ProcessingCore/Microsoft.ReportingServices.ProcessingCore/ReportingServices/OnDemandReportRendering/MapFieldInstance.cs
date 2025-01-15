using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001EA RID: 490
	public sealed class MapFieldInstance : BaseInstance
	{
		// Token: 0x06001291 RID: 4753 RVA: 0x0004BCD3 File Offset: 0x00049ED3
		internal MapFieldInstance(MapField defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0004BCED File Offset: 0x00049EED
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x040008DD RID: 2269
		private MapField m_defObject;
	}
}
