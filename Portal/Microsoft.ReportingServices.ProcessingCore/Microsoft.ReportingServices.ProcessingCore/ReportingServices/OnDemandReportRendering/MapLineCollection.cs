using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000183 RID: 387
	public sealed class MapLineCollection : MapObjectCollectionBase<MapLine>, ISpatialElementCollection
	{
		// Token: 0x06000FFA RID: 4090 RVA: 0x00044899 File Offset: 0x00042A99
		internal MapLineCollection(MapLineLayer mapLineLayer, Map map)
		{
			this.m_mapLineLayer = mapLineLayer;
			this.m_map = map;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x000448AF File Offset: 0x00042AAF
		protected override MapLine CreateMapObject(int index)
		{
			return new MapLine(this.m_mapLineLayer.MapLineLayerDef.MapLines[index], this.m_mapLineLayer, this.m_map);
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x000448D8 File Offset: 0x00042AD8
		public override int Count
		{
			get
			{
				return this.m_mapLineLayer.MapLineLayerDef.MapLines.Count;
			}
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x000448EF File Offset: 0x00042AEF
		MapSpatialElement ISpatialElementCollection.GetItem(int index)
		{
			return this[index];
		}

		// Token: 0x04000770 RID: 1904
		private Map m_map;

		// Token: 0x04000771 RID: 1905
		private MapLineLayer m_mapLineLayer;
	}
}
