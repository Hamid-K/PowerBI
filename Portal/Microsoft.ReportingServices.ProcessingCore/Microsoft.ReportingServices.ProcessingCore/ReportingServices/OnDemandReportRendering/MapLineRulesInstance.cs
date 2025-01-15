using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001D2 RID: 466
	public sealed class MapLineRulesInstance : BaseInstance
	{
		// Token: 0x06001204 RID: 4612 RVA: 0x0004A58C File Offset: 0x0004878C
		internal MapLineRulesInstance(MapLineRules defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0004A5A6 File Offset: 0x000487A6
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x04000890 RID: 2192
		private MapLineRules m_defObject;
	}
}
