using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000180 RID: 384
	public sealed class MapMarkerCollection : MapObjectCollectionBase<MapMarker>
	{
		// Token: 0x06000FF1 RID: 4081 RVA: 0x000447A9 File Offset: 0x000429A9
		internal MapMarkerCollection(MapMarkerRule markerRule, Map map)
		{
			this.m_markerRule = markerRule;
			this.m_map = map;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x000447BF File Offset: 0x000429BF
		protected override MapMarker CreateMapObject(int index)
		{
			return new MapMarker(this.m_markerRule.MapMarkerRuleDef.MapMarkers[index], this.m_map);
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x000447E2 File Offset: 0x000429E2
		public override int Count
		{
			get
			{
				return this.m_markerRule.MapMarkerRuleDef.MapMarkers.Count;
			}
		}

		// Token: 0x0400076A RID: 1898
		private Map m_map;

		// Token: 0x0400076B RID: 1899
		private MapMarkerRule m_markerRule;
	}
}
