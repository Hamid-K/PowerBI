using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200017E RID: 382
	public sealed class MapBindingFieldPairCollection : MapObjectCollectionBase<MapBindingFieldPair>
	{
		// Token: 0x06000FEA RID: 4074 RVA: 0x000446E9 File Offset: 0x000428E9
		internal MapBindingFieldPairCollection(MapVectorLayer mapVectorLayer, Map map)
		{
			this.m_mapBindingFieldCollectionDef = mapVectorLayer.MapVectorLayerDef.MapBindingFieldPairs;
			this.m_mapVectorLayer = mapVectorLayer;
			this.m_map = map;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00044710 File Offset: 0x00042910
		internal MapBindingFieldPairCollection(List<MapBindingFieldPair> mapBindingFieldCollectionDef, Map map)
		{
			this.m_mapBindingFieldCollectionDef = mapBindingFieldCollectionDef;
			this.m_mapVectorLayer = null;
			this.m_map = map;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0004472D File Offset: 0x0004292D
		protected override MapBindingFieldPair CreateMapObject(int index)
		{
			return new MapBindingFieldPair(this.m_mapBindingFieldCollectionDef[index], this.m_mapVectorLayer, this.m_map);
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0004474C File Offset: 0x0004294C
		public override int Count
		{
			get
			{
				return this.m_mapBindingFieldCollectionDef.Count;
			}
		}

		// Token: 0x04000765 RID: 1893
		private Map m_map;

		// Token: 0x04000766 RID: 1894
		private MapVectorLayer m_mapVectorLayer;

		// Token: 0x04000767 RID: 1895
		private List<MapBindingFieldPair> m_mapBindingFieldCollectionDef;
	}
}
