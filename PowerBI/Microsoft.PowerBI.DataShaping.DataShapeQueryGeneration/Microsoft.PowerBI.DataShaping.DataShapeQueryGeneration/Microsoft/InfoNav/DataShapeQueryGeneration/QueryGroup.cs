using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000A4 RID: 164
	internal sealed class QueryGroup
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x000177B4 File Offset: 0x000159B4
		internal QueryGroup(IReadOnlyList<QueryGroupKey> keys, IReadOnlyList<DsqSortKey> sortKeys, QueryDetailGroupIdentity detailGroupIdentity, SubtotalType subtotal, QueryGroupBindingHints bindingHints, bool suppressSortByMeasureRollup, bool isSubtotalContextOnly)
		{
			this.Keys = keys;
			this.SortKeys = sortKeys;
			this.DetailGroupIdentity = detailGroupIdentity;
			this.Subtotal = subtotal;
			this.BindingHints = bindingHints;
			this.SuppressSortByMeasureRollup = suppressSortByMeasureRollup;
			this.IsSubtotalContextOnly = isSubtotalContextOnly;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x000177F1 File Offset: 0x000159F1
		internal IReadOnlyList<QueryGroupKey> Keys { get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x000177F9 File Offset: 0x000159F9
		internal IReadOnlyList<DsqSortKey> SortKeys { get; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00017801 File Offset: 0x00015A01
		internal QueryDetailGroupIdentity DetailGroupIdentity { get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00017809 File Offset: 0x00015A09
		internal SubtotalType Subtotal { get; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00017811 File Offset: 0x00015A11
		public bool SuppressSortByMeasureRollup { get; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00017819 File Offset: 0x00015A19
		public QueryGroupBindingHints BindingHints { get; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00017821 File Offset: 0x00015A21
		public bool IsSubtotalContextOnly { get; }

		// Token: 0x0600060E RID: 1550 RVA: 0x00017829 File Offset: 0x00015A29
		internal QueryGroup CloneWithOverrides(IReadOnlyList<QueryGroupKey> keys = null, IReadOnlyList<DsqSortKey> sortKeys = null, IDictionary<ExpressionNode, IList<int>> modelSortToSourceSelectsMap = null)
		{
			return new QueryGroup(keys ?? this.Keys, sortKeys ?? this.SortKeys, this.DetailGroupIdentity, this.Subtotal, this.BindingHints, this.SuppressSortByMeasureRollup, this.IsSubtotalContextOnly);
		}
	}
}
