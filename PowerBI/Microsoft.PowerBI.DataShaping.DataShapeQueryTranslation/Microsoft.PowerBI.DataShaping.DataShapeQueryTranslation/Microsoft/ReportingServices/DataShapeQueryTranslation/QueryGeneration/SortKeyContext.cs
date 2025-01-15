using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000095 RID: 149
	internal sealed class SortKeyContext
	{
		// Token: 0x060006FF RID: 1791 RVA: 0x0001A5AE File Offset: 0x000187AE
		internal SortKeyContext(SortKey sortKey, QueryExpressionContext queryExpressionContext, bool isReused, bool isAddAsDetail, string name)
		{
			this.m_sortKey = sortKey;
			this.m_queryExpressionContext = queryExpressionContext;
			this.m_isReused = isReused;
			this.m_isAddAsDetail = isAddAsDetail;
			this.m_name = name;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001A5DB File Offset: 0x000187DB
		internal SortKey SortKey
		{
			get
			{
				return this.m_sortKey;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001A5E3 File Offset: 0x000187E3
		internal QueryExpressionContext QueryExpressionContext
		{
			get
			{
				return this.m_queryExpressionContext;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001A5EB File Offset: 0x000187EB
		internal bool IsReused
		{
			get
			{
				return this.m_isReused;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001A5F3 File Offset: 0x000187F3
		internal bool IsAddAsDetail
		{
			get
			{
				return this.m_isAddAsDetail;
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001A5FC File Offset: 0x000187FC
		internal bool IsSameSorting(KeyValuePair<SortKey, QueryExpressionContext> dsqSort, string name)
		{
			if (dsqSort.Value.CalculateAsMeasure)
			{
				return this.m_name == name && this.SortKey.SortDirection.Value == dsqSort.Key.SortDirection.Value;
			}
			return this.SortKey.Value.OriginalNode.Equals(dsqSort.Key.Value.OriginalNode) && this.SortKey.SortDirection.Value == dsqSort.Key.SortDirection.Value;
		}

		// Token: 0x04000364 RID: 868
		private readonly SortKey m_sortKey;

		// Token: 0x04000365 RID: 869
		private readonly QueryExpressionContext m_queryExpressionContext;

		// Token: 0x04000366 RID: 870
		private readonly bool m_isReused;

		// Token: 0x04000367 RID: 871
		private readonly string m_name;

		// Token: 0x04000368 RID: 872
		private readonly bool m_isAddAsDetail;
	}
}
