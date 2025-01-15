using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001DA RID: 474
	public sealed class MapCustomColorRuleInstance : MapColorRuleInstance
	{
		// Token: 0x0600121E RID: 4638 RVA: 0x0004A97A File Offset: 0x00048B7A
		internal MapCustomColorRuleInstance(MapCustomColorRule defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0004A98A File Offset: 0x00048B8A
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}

		// Token: 0x040008A0 RID: 2208
		private MapCustomColorRule m_defObject;
	}
}
