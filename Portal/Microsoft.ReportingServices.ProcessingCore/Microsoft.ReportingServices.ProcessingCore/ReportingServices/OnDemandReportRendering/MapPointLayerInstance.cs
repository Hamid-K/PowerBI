using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001BB RID: 443
	public sealed class MapPointLayerInstance : MapVectorLayerInstance
	{
		// Token: 0x06001169 RID: 4457 RVA: 0x00048AC3 File Offset: 0x00046CC3
		internal MapPointLayerInstance(MapPointLayer defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00048AD3 File Offset: 0x00046CD3
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}

		// Token: 0x0400083D RID: 2109
		private MapPointLayer m_defObject;
	}
}
