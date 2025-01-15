using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001E3 RID: 483
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapSpatialElement : MapObjectCollectionItem
	{
		// Token: 0x06001267 RID: 4711 RVA: 0x0004B418 File Offset: 0x00049618
		internal MapSpatialElement(MapSpatialElement defObject, MapVectorLayer mapVectorLayer, Map map)
		{
			this.m_defObject = defObject;
			this.m_mapVectorLayer = mapVectorLayer;
			this.m_map = map;
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x0004B435 File Offset: 0x00049635
		public string VectorData
		{
			get
			{
				return this.m_defObject.VectorData;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x0004B442 File Offset: 0x00049642
		public MapFieldCollection MapFields
		{
			get
			{
				if (this.m_mapFields == null && this.m_defObject.MapFields != null)
				{
					this.m_mapFields = new MapFieldCollection(this, this.m_map);
				}
				return this.m_mapFields;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0004B471 File Offset: 0x00049671
		internal IReportScope ReportScope
		{
			get
			{
				return this.m_mapVectorLayer.ReportScope;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0004B47E File Offset: 0x0004967E
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0004B486 File Offset: 0x00049686
		internal MapSpatialElement MapSpatialElementDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x0004B48E File Offset: 0x0004968E
		internal MapSpatialElementInstance Instance
		{
			get
			{
				return this.GetInstance();
			}
		}

		// Token: 0x0600126E RID: 4718
		internal abstract MapSpatialElementInstance GetInstance();

		// Token: 0x0600126F RID: 4719 RVA: 0x0004B496 File Offset: 0x00049696
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapFields != null)
			{
				this.m_mapFields.SetNewContext();
			}
		}

		// Token: 0x040008BF RID: 2239
		protected Map m_map;

		// Token: 0x040008C0 RID: 2240
		protected MapVectorLayer m_mapVectorLayer;

		// Token: 0x040008C1 RID: 2241
		private MapSpatialElement m_defObject;

		// Token: 0x040008C2 RID: 2242
		private MapFieldCollection m_mapFields;
	}
}
