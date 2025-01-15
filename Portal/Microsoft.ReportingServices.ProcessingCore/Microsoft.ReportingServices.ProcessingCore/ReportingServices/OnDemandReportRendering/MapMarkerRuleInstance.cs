using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001D7 RID: 471
	public sealed class MapMarkerRuleInstance : MapAppearanceRuleInstance
	{
		// Token: 0x06001217 RID: 4631 RVA: 0x0004A8D1 File Offset: 0x00048AD1
		internal MapMarkerRuleInstance(MapMarkerRule defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0004A8E1 File Offset: 0x00048AE1
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}

		// Token: 0x0400089C RID: 2204
		private MapMarkerRule m_defObject;
	}
}
