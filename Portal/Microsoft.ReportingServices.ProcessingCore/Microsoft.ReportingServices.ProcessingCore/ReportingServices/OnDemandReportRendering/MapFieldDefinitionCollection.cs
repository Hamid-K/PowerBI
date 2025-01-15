using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000189 RID: 393
	public sealed class MapFieldDefinitionCollection : MapObjectCollectionBase<MapFieldDefinition>
	{
		// Token: 0x0600100E RID: 4110 RVA: 0x00044A42 File Offset: 0x00042C42
		internal MapFieldDefinitionCollection(MapVectorLayer mapVectorLayer, Map map)
		{
			this.m_mapVectorLayer = mapVectorLayer;
			this.m_map = map;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00044A58 File Offset: 0x00042C58
		protected override MapFieldDefinition CreateMapObject(int index)
		{
			return new MapFieldDefinition(this.m_mapVectorLayer.MapVectorLayerDef.MapFieldDefinitions[index], this.m_map);
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x00044A7B File Offset: 0x00042C7B
		public override int Count
		{
			get
			{
				return this.m_mapVectorLayer.MapVectorLayerDef.MapFieldDefinitions.Count;
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00044A94 File Offset: 0x00042C94
		internal MapFieldDefinition GetFieldDefinition(string name)
		{
			foreach (MapFieldDefinition mapFieldDefinition in this)
			{
				if (string.CompareOrdinal(name, mapFieldDefinition.Name) == 0)
				{
					return mapFieldDefinition;
				}
			}
			return null;
		}

		// Token: 0x0400077A RID: 1914
		private Map m_map;

		// Token: 0x0400077B RID: 1915
		private MapVectorLayer m_mapVectorLayer;
	}
}
