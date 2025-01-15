using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001D8 RID: 472
	public sealed class MapPointRulesInstance : BaseInstance
	{
		// Token: 0x06001219 RID: 4633 RVA: 0x0004A8E9 File Offset: 0x00048AE9
		internal MapPointRulesInstance(MapPointRules defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0004A903 File Offset: 0x00048B03
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x0400089D RID: 2205
		private MapPointRules m_defObject;
	}
}
