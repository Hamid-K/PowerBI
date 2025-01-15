using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001AB RID: 427
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapSpatialData
	{
		// Token: 0x0600110E RID: 4366 RVA: 0x00047CE5 File Offset: 0x00045EE5
		internal MapSpatialData(MapVectorLayer mapVectorLayer, Map map)
		{
			this.m_defObject = mapVectorLayer.MapVectorLayerDef.MapSpatialData;
			this.m_mapVectorLayer = mapVectorLayer;
			this.m_map = map;
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x00047D0C File Offset: 0x00045F0C
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00047D14 File Offset: 0x00045F14
		internal MapSpatialData MapSpatialDataDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x00047D1C File Offset: 0x00045F1C
		internal MapSpatialDataInstance Instance
		{
			get
			{
				return this.GetInstance();
			}
		}

		// Token: 0x06001112 RID: 4370
		internal abstract MapSpatialDataInstance GetInstance();

		// Token: 0x06001113 RID: 4371 RVA: 0x00047D24 File Offset: 0x00045F24
		internal virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000813 RID: 2067
		protected Map m_map;

		// Token: 0x04000814 RID: 2068
		protected MapVectorLayer m_mapVectorLayer;

		// Token: 0x04000815 RID: 2069
		private MapSpatialData m_defObject;

		// Token: 0x04000816 RID: 2070
		protected MapSpatialDataInstance m_instance;
	}
}
