using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200018B RID: 395
	public sealed class MapTitleCollection : MapObjectCollectionBase<MapTitle>
	{
		// Token: 0x06001015 RID: 4117 RVA: 0x00044B35 File Offset: 0x00042D35
		internal MapTitleCollection(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00044B44 File Offset: 0x00042D44
		protected override MapTitle CreateMapObject(int index)
		{
			return new MapTitle(this.m_map.MapDef.MapTitles[index], this.m_map);
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00044B67 File Offset: 0x00042D67
		public override int Count
		{
			get
			{
				return this.m_map.MapDef.MapTitles.Count;
			}
		}

		// Token: 0x0400077D RID: 1917
		private Map m_map;
	}
}
