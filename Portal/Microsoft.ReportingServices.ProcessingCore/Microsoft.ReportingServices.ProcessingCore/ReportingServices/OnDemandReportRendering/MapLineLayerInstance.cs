using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001B6 RID: 438
	public sealed class MapLineLayerInstance : MapVectorLayerInstance
	{
		// Token: 0x06001158 RID: 4440 RVA: 0x00048842 File Offset: 0x00046A42
		internal MapLineLayerInstance(MapLineLayer defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00048852 File Offset: 0x00046A52
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
		}

		// Token: 0x04000833 RID: 2099
		private MapLineLayer m_defObject;
	}
}
