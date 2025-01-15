using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200018C RID: 396
	public sealed class MapLayerCollection : MapObjectCollectionBase<MapLayer>
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x00044B7E File Offset: 0x00042D7E
		internal MapLayerCollection(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00044B90 File Offset: 0x00042D90
		protected override MapLayer CreateMapObject(int index)
		{
			MapLayer mapLayer = this.m_map.MapDef.MapLayers[index];
			if (mapLayer is MapTileLayer)
			{
				return new MapTileLayer((MapTileLayer)mapLayer, this.m_map);
			}
			if (mapLayer is MapPolygonLayer)
			{
				return new MapPolygonLayer((MapPolygonLayer)mapLayer, this.m_map);
			}
			if (mapLayer is MapPointLayer)
			{
				return new MapPointLayer((MapPointLayer)mapLayer, this.m_map);
			}
			if (mapLayer is MapLineLayer)
			{
				return new MapLineLayer((MapLineLayer)mapLayer, this.m_map);
			}
			return null;
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00044C1D File Offset: 0x00042E1D
		public override int Count
		{
			get
			{
				return this.m_map.MapDef.MapLayers.Count;
			}
		}

		// Token: 0x0400077E RID: 1918
		private Map m_map;
	}
}
