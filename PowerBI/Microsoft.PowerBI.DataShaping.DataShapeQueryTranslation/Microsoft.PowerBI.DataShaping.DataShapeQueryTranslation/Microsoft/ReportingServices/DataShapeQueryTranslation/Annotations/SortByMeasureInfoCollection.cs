using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000247 RID: 583
	internal sealed class SortByMeasureInfoCollection : IEnumerable<KeyValuePair<SortKey, SortByMeasureInfo>>, IEnumerable
	{
		// Token: 0x060013E1 RID: 5089 RVA: 0x0004D325 File Offset: 0x0004B525
		internal SortByMeasureInfoCollection(bool isAtMeasureScope)
		{
			this.m_isAtMeasureScope = isAtMeasureScope;
			this.m_sortByMeasureInfos = new List<KeyValuePair<SortKey, SortByMeasureInfo>>();
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0004D33F File Offset: 0x0004B53F
		public bool IsAtMeasureScope
		{
			get
			{
				return this.m_isAtMeasureScope;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0004D347 File Offset: 0x0004B547
		public int Count
		{
			get
			{
				return this.m_sortByMeasureInfos.Count;
			}
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0004D354 File Offset: 0x0004B554
		public void Add(SortKey sortKey, SortByMeasureInfo sortByMeasureInfo)
		{
			this.m_sortByMeasureInfos.Add(new KeyValuePair<SortKey, SortByMeasureInfo>(sortKey, sortByMeasureInfo));
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0004D368 File Offset: 0x0004B568
		public bool ContainsKey(SortKey sortKey)
		{
			return this.m_sortByMeasureInfos.Any((KeyValuePair<SortKey, SortByMeasureInfo> i) => i.Key == sortKey);
		}

		// Token: 0x17000372 RID: 882
		public SortByMeasureInfo this[SortKey key]
		{
			get
			{
				return this.m_sortByMeasureInfos.Where((KeyValuePair<SortKey, SortByMeasureInfo> i) => i.Key == key).Single<KeyValuePair<SortKey, SortByMeasureInfo>>().Value;
			}
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0004D3DA File Offset: 0x0004B5DA
		public IEnumerator<KeyValuePair<SortKey, SortByMeasureInfo>> GetEnumerator()
		{
			return this.m_sortByMeasureInfos.GetEnumerator();
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0004D3EC File Offset: 0x0004B5EC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_sortByMeasureInfos.GetEnumerator();
		}

		// Token: 0x040008C6 RID: 2246
		private readonly bool m_isAtMeasureScope;

		// Token: 0x040008C7 RID: 2247
		private readonly List<KeyValuePair<SortKey, SortByMeasureInfo>> m_sortByMeasureInfos;
	}
}
