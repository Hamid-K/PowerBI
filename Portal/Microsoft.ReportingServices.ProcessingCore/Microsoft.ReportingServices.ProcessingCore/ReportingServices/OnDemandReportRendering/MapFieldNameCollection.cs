using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000188 RID: 392
	public sealed class MapFieldNameCollection : MapObjectCollectionBase<MapFieldName>
	{
		// Token: 0x0600100B RID: 4107 RVA: 0x00044A06 File Offset: 0x00042C06
		internal MapFieldNameCollection(List<MapFieldName> mapFieldNames, Map map)
		{
			this.m_mapFieldNames = mapFieldNames;
			this.m_map = map;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00044A1C File Offset: 0x00042C1C
		protected override MapFieldName CreateMapObject(int index)
		{
			return new MapFieldName(this.m_mapFieldNames[index], this.m_map);
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00044A35 File Offset: 0x00042C35
		public override int Count
		{
			get
			{
				return this.m_mapFieldNames.Count;
			}
		}

		// Token: 0x04000778 RID: 1912
		private Map m_map;

		// Token: 0x04000779 RID: 1913
		private List<MapFieldName> m_mapFieldNames;
	}
}
