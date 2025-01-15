using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200017F RID: 383
	public sealed class MapBucketCollection : MapObjectCollectionBase<MapBucket>
	{
		// Token: 0x06000FEE RID: 4078 RVA: 0x00044759 File Offset: 0x00042959
		internal MapBucketCollection(MapAppearanceRule mapApperanceRule, Map map)
		{
			this.m_mapApperanceRule = mapApperanceRule;
			this.m_map = map;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0004476F File Offset: 0x0004296F
		protected override MapBucket CreateMapObject(int index)
		{
			return new MapBucket(this.m_mapApperanceRule.MapAppearanceRuleDef.MapBuckets[index], this.m_map);
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x00044792 File Offset: 0x00042992
		public override int Count
		{
			get
			{
				return this.m_mapApperanceRule.MapAppearanceRuleDef.MapBuckets.Count;
			}
		}

		// Token: 0x04000768 RID: 1896
		private Map m_map;

		// Token: 0x04000769 RID: 1897
		private MapAppearanceRule m_mapApperanceRule;
	}
}
