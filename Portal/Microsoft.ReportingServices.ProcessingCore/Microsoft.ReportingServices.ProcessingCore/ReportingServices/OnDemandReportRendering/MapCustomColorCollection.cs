using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000181 RID: 385
	public sealed class MapCustomColorCollection : MapObjectCollectionBase<MapCustomColor>
	{
		// Token: 0x06000FF4 RID: 4084 RVA: 0x000447F9 File Offset: 0x000429F9
		internal MapCustomColorCollection(MapCustomColorRule customColorRule, Map map)
		{
			this.m_customColorRule = customColorRule;
			this.m_map = map;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0004480F File Offset: 0x00042A0F
		protected override MapCustomColor CreateMapObject(int index)
		{
			return new MapCustomColor(this.m_customColorRule.MapCustomColorRuleDef.MapCustomColors[index], this.m_map);
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00044832 File Offset: 0x00042A32
		public override int Count
		{
			get
			{
				return this.m_customColorRule.MapCustomColorRuleDef.MapCustomColors.Count;
			}
		}

		// Token: 0x0400076C RID: 1900
		private Map m_map;

		// Token: 0x0400076D RID: 1901
		private MapCustomColorRule m_customColorRule;
	}
}
