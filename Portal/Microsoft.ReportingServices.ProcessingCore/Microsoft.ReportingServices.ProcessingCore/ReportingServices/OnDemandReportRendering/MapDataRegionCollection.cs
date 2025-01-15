using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000173 RID: 371
	public sealed class MapDataRegionCollection : MapObjectCollectionBase<MapDataRegion>
	{
		// Token: 0x06000F9B RID: 3995 RVA: 0x00043B26 File Offset: 0x00041D26
		internal MapDataRegionCollection(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00043B35 File Offset: 0x00041D35
		protected override MapDataRegion CreateMapObject(int index)
		{
			return new MapDataRegion(this.m_map, index, this.m_map.MapDef.MapDataRegions[index], this.m_map.RenderingContext);
		}

		// Token: 0x1700084E RID: 2126
		public MapDataRegion this[string name]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					MapDataRegion mapDataRegion = this.m_map.MapDef.MapDataRegions[i];
					if (string.CompareOrdinal(name, mapDataRegion.Name) == 0)
					{
						return this[i];
					}
				}
				throw new RenderingObjectModelException(ProcessingErrorCode.rsNotInCollection, new object[] { name });
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x00043BC3 File Offset: 0x00041DC3
		public override int Count
		{
			get
			{
				if (this.m_map.MapDef.MapDataRegions != null)
				{
					return this.m_map.MapDef.MapDataRegions.Count;
				}
				return 0;
			}
		}

		// Token: 0x04000744 RID: 1860
		private Map m_map;
	}
}
