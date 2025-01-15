using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001BC RID: 444
	public sealed class MapTileInstance : BaseInstance
	{
		// Token: 0x0600116B RID: 4459 RVA: 0x00048ADB File Offset: 0x00046CDB
		internal MapTileInstance(MapTile defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00048AF5 File Offset: 0x00046CF5
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x0400083E RID: 2110
		private MapTile m_defObject;
	}
}
