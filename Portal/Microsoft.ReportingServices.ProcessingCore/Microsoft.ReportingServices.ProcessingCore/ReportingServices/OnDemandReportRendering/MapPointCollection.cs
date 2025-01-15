using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000186 RID: 390
	public sealed class MapPointCollection : MapObjectCollectionBase<MapPoint>, ISpatialElementCollection
	{
		// Token: 0x06001004 RID: 4100 RVA: 0x00044957 File Offset: 0x00042B57
		internal MapPointCollection(MapPointLayer mapPointLayer, Map map)
		{
			this.m_mapPointLayer = mapPointLayer;
			this.m_map = map;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0004496D File Offset: 0x00042B6D
		protected override MapPoint CreateMapObject(int index)
		{
			return new MapPoint(this.m_mapPointLayer.MapPointLayerDef.MapPoints[index], this.m_mapPointLayer, this.m_map);
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x00044996 File Offset: 0x00042B96
		public override int Count
		{
			get
			{
				return this.m_mapPointLayer.MapPointLayerDef.MapPoints.Count;
			}
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x000449AD File Offset: 0x00042BAD
		MapSpatialElement ISpatialElementCollection.GetItem(int index)
		{
			return this[index];
		}

		// Token: 0x04000774 RID: 1908
		private Map m_map;

		// Token: 0x04000775 RID: 1909
		private MapPointLayer m_mapPointLayer;
	}
}
