using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001AD RID: 429
	public sealed class MapSpatialDataRegionInstance : MapSpatialDataInstance
	{
		// Token: 0x0600111B RID: 4379 RVA: 0x00047DE8 File Offset: 0x00045FE8
		internal MapSpatialDataRegionInstance(MapSpatialDataRegion defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00047DF8 File Offset: 0x00045FF8
		public object VectorData
		{
			get
			{
				if (this.m_vectorData == null)
				{
					this.m_vectorData = ((MapSpatialDataRegion)this.m_defObject.MapSpatialDataDef).EvaluateVectorData(this.m_defObject.ReportScope.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext).Value;
				}
				return this.m_vectorData;
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00047E58 File Offset: 0x00046058
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_vectorData = null;
		}

		// Token: 0x04000818 RID: 2072
		private MapSpatialDataRegion m_defObject;

		// Token: 0x04000819 RID: 2073
		private object m_vectorData;
	}
}
