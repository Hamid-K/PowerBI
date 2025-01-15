using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000185 RID: 389
	public sealed class MapPolygonCollection : MapObjectCollectionBase<MapPolygon>, ISpatialElementCollection
	{
		// Token: 0x06001000 RID: 4096 RVA: 0x000448F8 File Offset: 0x00042AF8
		internal MapPolygonCollection(MapPolygonLayer mapPolygonLayer, Map map)
		{
			this.m_mapPolygonLayer = mapPolygonLayer;
			this.m_map = map;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0004490E File Offset: 0x00042B0E
		protected override MapPolygon CreateMapObject(int index)
		{
			return new MapPolygon(this.m_mapPolygonLayer.MapPolygonLayerDef.MapPolygons[index], this.m_mapPolygonLayer, this.m_map);
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x00044937 File Offset: 0x00042B37
		public override int Count
		{
			get
			{
				return this.m_mapPolygonLayer.MapPolygonLayerDef.MapPolygons.Count;
			}
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0004494E File Offset: 0x00042B4E
		MapSpatialElement ISpatialElementCollection.GetItem(int index)
		{
			return this[index];
		}

		// Token: 0x04000772 RID: 1906
		private Map m_map;

		// Token: 0x04000773 RID: 1907
		private MapPolygonLayer m_mapPolygonLayer;
	}
}
