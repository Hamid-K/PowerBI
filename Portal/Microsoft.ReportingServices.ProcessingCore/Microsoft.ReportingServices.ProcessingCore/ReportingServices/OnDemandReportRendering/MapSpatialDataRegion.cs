using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001AC RID: 428
	public sealed class MapSpatialDataRegion : MapSpatialData
	{
		// Token: 0x06001114 RID: 4372 RVA: 0x00047D39 File Offset: 0x00045F39
		internal MapSpatialDataRegion(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x00047D43 File Offset: 0x00045F43
		public ReportVariantProperty VectorData
		{
			get
			{
				if (this.m_vectorData == null && this.MapSpatialDataRegionDef.VectorData != null)
				{
					this.m_vectorData = new ReportVariantProperty(this.MapSpatialDataRegionDef.VectorData);
				}
				return this.m_vectorData;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00047D76 File Offset: 0x00045F76
		internal IReportScope ReportScope
		{
			get
			{
				return this.m_mapVectorLayer.ReportScope;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00047D83 File Offset: 0x00045F83
		internal MapSpatialDataRegion MapSpatialDataRegionDef
		{
			get
			{
				return (MapSpatialDataRegion)base.MapSpatialDataDef;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00047D90 File Offset: 0x00045F90
		public new MapSpatialDataRegionInstance Instance
		{
			get
			{
				return (MapSpatialDataRegionInstance)this.GetInstance();
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00047D9D File Offset: 0x00045F9D
		internal override MapSpatialDataInstance GetInstance()
		{
			if (this.m_map.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new MapSpatialDataRegionInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00047DCD File Offset: 0x00045FCD
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000817 RID: 2071
		private ReportVariantProperty m_vectorData;
	}
}
