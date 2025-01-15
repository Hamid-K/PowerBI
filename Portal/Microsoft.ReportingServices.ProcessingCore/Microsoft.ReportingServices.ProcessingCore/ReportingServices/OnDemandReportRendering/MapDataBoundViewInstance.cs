using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001FA RID: 506
	public sealed class MapDataBoundViewInstance : MapViewInstance
	{
		// Token: 0x060012FC RID: 4860 RVA: 0x0004D265 File Offset: 0x0004B465
		internal MapDataBoundViewInstance(MapDataBoundView defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0004D275 File Offset: 0x0004B475
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}

		// Token: 0x04000924 RID: 2340
		private MapDataBoundView m_defObject;
	}
}
