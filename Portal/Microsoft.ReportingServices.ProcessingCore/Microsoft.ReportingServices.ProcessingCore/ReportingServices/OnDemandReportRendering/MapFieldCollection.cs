using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000182 RID: 386
	public sealed class MapFieldCollection : MapObjectCollectionBase<MapField>
	{
		// Token: 0x06000FF7 RID: 4087 RVA: 0x00044849 File Offset: 0x00042A49
		internal MapFieldCollection(MapSpatialElement mapSpatialElement, Map map)
		{
			this.m_mapSpatialElement = mapSpatialElement;
			this.m_map = map;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0004485F File Offset: 0x00042A5F
		protected override MapField CreateMapObject(int index)
		{
			return new MapField(this.m_mapSpatialElement.MapSpatialElementDef.MapFields[index], this.m_map);
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00044882 File Offset: 0x00042A82
		public override int Count
		{
			get
			{
				return this.m_mapSpatialElement.MapSpatialElementDef.MapFields.Count;
			}
		}

		// Token: 0x0400076E RID: 1902
		private Map m_map;

		// Token: 0x0400076F RID: 1903
		private MapSpatialElement m_mapSpatialElement;
	}
}
