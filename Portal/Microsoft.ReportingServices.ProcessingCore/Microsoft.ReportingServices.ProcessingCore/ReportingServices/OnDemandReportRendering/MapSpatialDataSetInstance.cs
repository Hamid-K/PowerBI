using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001BA RID: 442
	public sealed class MapSpatialDataSetInstance : MapSpatialDataInstance
	{
		// Token: 0x06001165 RID: 4453 RVA: 0x000489F6 File Offset: 0x00046BF6
		internal MapSpatialDataSetInstance(MapSpatialDataSet defObject)
			: base(defObject)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00048A08 File Offset: 0x00046C08
		public string DataSetName
		{
			get
			{
				if (this.m_dataSetName == null)
				{
					this.m_dataSetName = ((MapSpatialDataSet)this.m_defObject.MapSpatialDataDef).EvaluateDataSetName(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
				}
				return this.m_dataSetName;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00048A5C File Offset: 0x00046C5C
		public string SpatialField
		{
			get
			{
				if (this.m_spatialField == null)
				{
					this.m_spatialField = ((MapSpatialDataSet)this.m_defObject.MapSpatialDataDef).EvaluateSpatialField(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
				}
				return this.m_spatialField;
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00048AAD File Offset: 0x00046CAD
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_dataSetName = null;
			this.m_spatialField = null;
		}

		// Token: 0x0400083A RID: 2106
		private MapSpatialDataSet m_defObject;

		// Token: 0x0400083B RID: 2107
		private string m_dataSetName;

		// Token: 0x0400083C RID: 2108
		private string m_spatialField;
	}
}
