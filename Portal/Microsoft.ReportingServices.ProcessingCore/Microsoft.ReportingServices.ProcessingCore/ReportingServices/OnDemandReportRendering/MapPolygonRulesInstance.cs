using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001D3 RID: 467
	public sealed class MapPolygonRulesInstance : BaseInstance
	{
		// Token: 0x06001206 RID: 4614 RVA: 0x0004A5A8 File Offset: 0x000487A8
		internal MapPolygonRulesInstance(MapPolygonRules defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0004A5C2 File Offset: 0x000487C2
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x04000891 RID: 2193
		private MapPolygonRules m_defObject;
	}
}
