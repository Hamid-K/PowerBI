using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001C1 RID: 449
	internal sealed class TopNPerLevelLevelRow
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0003DD0A File Offset: 0x0003BF0A
		public QueryExpression LevelId { get; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0003DD12 File Offset: 0x0003BF12
		public QueryExpression SubtotalName { get; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x0003DD1A File Offset: 0x0003BF1A
		public IReadOnlyList<QuerySortClause> SortByItems { get; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0003DD22 File Offset: 0x0003BF22
		public IReadOnlyList<QueryExpression> ValueColumns { get; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0003DD2A File Offset: 0x0003BF2A
		public IReadOnlyList<QueryExpression> WindowValueColumns { get; }

		// Token: 0x06001654 RID: 5716 RVA: 0x0003DD32 File Offset: 0x0003BF32
		internal TopNPerLevelLevelRow(QueryExpression levelId, QueryExpression subtotalName, IReadOnlyList<QuerySortClause> sortByItems, IReadOnlyList<QueryExpression> valueColumns, IReadOnlyList<QueryExpression> windowValueColumns)
		{
			this.LevelId = levelId;
			this.SubtotalName = subtotalName;
			this.SortByItems = sortByItems;
			this.ValueColumns = valueColumns;
			this.WindowValueColumns = windowValueColumns;
		}
	}
}
