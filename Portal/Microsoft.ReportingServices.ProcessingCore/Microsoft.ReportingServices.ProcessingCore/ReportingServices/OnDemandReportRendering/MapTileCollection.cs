using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000187 RID: 391
	public sealed class MapTileCollection : MapObjectCollectionBase<MapTile>
	{
		// Token: 0x06001008 RID: 4104 RVA: 0x000449B6 File Offset: 0x00042BB6
		internal MapTileCollection(MapTileLayer mapTileLayer, Map map)
		{
			this.m_mapTileLayer = mapTileLayer;
			this.m_map = map;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x000449CC File Offset: 0x00042BCC
		protected override MapTile CreateMapObject(int index)
		{
			return new MapTile(this.m_mapTileLayer.MapTileLayerDef.MapTiles[index], this.m_map);
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x000449EF File Offset: 0x00042BEF
		public override int Count
		{
			get
			{
				return this.m_mapTileLayer.MapTileLayerDef.MapTiles.Count;
			}
		}

		// Token: 0x04000776 RID: 1910
		private Map m_map;

		// Token: 0x04000777 RID: 1911
		private MapTileLayer m_mapTileLayer;
	}
}
