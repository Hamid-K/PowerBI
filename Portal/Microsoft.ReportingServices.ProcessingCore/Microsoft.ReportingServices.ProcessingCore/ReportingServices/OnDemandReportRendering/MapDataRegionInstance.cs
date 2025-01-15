using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200019E RID: 414
	public sealed class MapDataRegionInstance : DataRegionInstance
	{
		// Token: 0x060010B9 RID: 4281 RVA: 0x000470D4 File Offset: 0x000452D4
		internal MapDataRegionInstance(MapDataRegion reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x000470DD File Offset: 0x000452DD
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}
	}
}
