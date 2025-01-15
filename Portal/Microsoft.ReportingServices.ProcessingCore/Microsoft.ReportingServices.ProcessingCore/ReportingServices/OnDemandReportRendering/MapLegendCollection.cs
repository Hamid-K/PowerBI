using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200018A RID: 394
	public sealed class MapLegendCollection : MapObjectCollectionBase<MapLegend>
	{
		// Token: 0x06001012 RID: 4114 RVA: 0x00044AEC File Offset: 0x00042CEC
		internal MapLegendCollection(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00044AFB File Offset: 0x00042CFB
		protected override MapLegend CreateMapObject(int index)
		{
			return new MapLegend(this.m_map.MapDef.MapLegends[index], this.m_map);
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00044B1E File Offset: 0x00042D1E
		public override int Count
		{
			get
			{
				return this.m_map.MapDef.MapLegends.Count;
			}
		}

		// Token: 0x0400077C RID: 1916
		private Map m_map;
	}
}
