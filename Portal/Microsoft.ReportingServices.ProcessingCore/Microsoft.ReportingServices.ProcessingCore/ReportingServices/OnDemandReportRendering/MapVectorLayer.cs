using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001B2 RID: 434
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapVectorLayer : MapLayer
	{
		// Token: 0x06001141 RID: 4417 RVA: 0x000483E4 File Offset: 0x000465E4
		internal MapVectorLayer(MapVectorLayer defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x000483EE File Offset: 0x000465EE
		public string DataElementName
		{
			get
			{
				return this.MapVectorLayerDef.DataElementName;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x000483FB File Offset: 0x000465FB
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.MapVectorLayerDef.DataElementOutput;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x00048408 File Offset: 0x00046608
		public string MapDataRegionName
		{
			get
			{
				return this.MapVectorLayerDef.MapDataRegionName;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x00048415 File Offset: 0x00046615
		public MapBindingFieldPairCollection MapBindingFieldPairs
		{
			get
			{
				if (this.m_mapBindingFieldPairs == null && this.MapVectorLayerDef.MapBindingFieldPairs != null)
				{
					this.m_mapBindingFieldPairs = new MapBindingFieldPairCollection(this, this.m_map);
				}
				return this.m_mapBindingFieldPairs;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00048444 File Offset: 0x00046644
		public MapFieldDefinitionCollection MapFieldDefinitions
		{
			get
			{
				if (this.m_mapFieldDefinitions == null && this.MapVectorLayerDef.MapFieldDefinitions != null)
				{
					this.m_mapFieldDefinitions = new MapFieldDefinitionCollection(this, this.m_map);
				}
				return this.m_mapFieldDefinitions;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x00048474 File Offset: 0x00046674
		public MapSpatialData MapSpatialData
		{
			get
			{
				if (this.m_mapSpatialData == null)
				{
					MapSpatialData mapSpatialData = this.MapVectorLayerDef.MapSpatialData;
					if (mapSpatialData != null)
					{
						if (mapSpatialData is MapShapefile)
						{
							this.m_mapSpatialData = new MapShapefile(this, this.m_map);
						}
						else if (mapSpatialData is MapSpatialDataSet)
						{
							this.m_mapSpatialData = new MapSpatialDataSet(this, this.m_map);
						}
						else if (mapSpatialData is MapSpatialDataRegion)
						{
							this.m_mapSpatialData = new MapSpatialDataRegion(this, this.m_map);
						}
					}
				}
				return this.m_mapSpatialData;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x000484F0 File Offset: 0x000466F0
		internal IReportScope ReportScope
		{
			get
			{
				if (this.m_reportScope == null)
				{
					if (this.MapDataRegionName != null)
					{
						this.m_reportScope = this.m_map.MapDataRegions[this.MapDataRegionName].InnerMostMapMember;
					}
					else
					{
						this.m_reportScope = this.m_map.ReportScope;
					}
				}
				return this.m_reportScope;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00048547 File Offset: 0x00046747
		public MapDataRegion MapDataRegion
		{
			get
			{
				if (this.MapDataRegionName != null && this.m_mapDataRegion == null)
				{
					this.m_mapDataRegion = base.MapDef.MapDataRegions[this.MapDataRegionName];
				}
				return this.m_mapDataRegion;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0004857B File Offset: 0x0004677B
		internal MapVectorLayer MapVectorLayerDef
		{
			get
			{
				return (MapVectorLayer)base.MapLayerDef;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00048588 File Offset: 0x00046788
		internal new MapVectorLayerInstance Instance
		{
			get
			{
				return (MapVectorLayerInstance)this.GetInstance();
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00048598 File Offset: 0x00046798
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapBindingFieldPairs != null)
			{
				this.m_mapBindingFieldPairs.SetNewContext();
			}
			if (this.m_mapFieldDefinitions != null)
			{
				this.m_mapFieldDefinitions.SetNewContext();
			}
			if (this.m_mapSpatialData != null)
			{
				this.m_mapSpatialData.SetNewContext();
			}
		}

		// Token: 0x04000826 RID: 2086
		private IReportScope m_reportScope;

		// Token: 0x04000827 RID: 2087
		private MapBindingFieldPairCollection m_mapBindingFieldPairs;

		// Token: 0x04000828 RID: 2088
		private MapFieldDefinitionCollection m_mapFieldDefinitions;

		// Token: 0x04000829 RID: 2089
		private MapSpatialData m_mapSpatialData;

		// Token: 0x0400082A RID: 2090
		private MapDataRegion m_mapDataRegion;
	}
}
