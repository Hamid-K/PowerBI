using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001B8 RID: 440
	public sealed class MapPolygonLayerInstance : MapVectorLayerInstance
	{
		// Token: 0x06001161 RID: 4449 RVA: 0x000489C2 File Offset: 0x00046BC2
		internal MapPolygonLayerInstance(MapPolygonLayer defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000489D2 File Offset: 0x00046BD2
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}

		// Token: 0x04000838 RID: 2104
		private MapPolygonLayer m_defObject;
	}
}
