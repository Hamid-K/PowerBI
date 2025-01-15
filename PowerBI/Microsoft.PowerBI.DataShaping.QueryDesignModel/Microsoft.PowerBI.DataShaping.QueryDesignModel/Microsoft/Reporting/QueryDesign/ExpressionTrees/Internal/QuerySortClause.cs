using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001B7 RID: 439
	internal sealed class QuerySortClause : IEquatable<QuerySortClause>
	{
		// Token: 0x06001614 RID: 5652 RVA: 0x0003D44A File Offset: 0x0003B64A
		internal QuerySortClause(SortDirection direction, QueryExpression expression)
		{
			this.Direction = direction;
			this.Expression = expression;
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x0003D460 File Offset: 0x0003B660
		public QueryExpression Expression { get; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x0003D468 File Offset: 0x0003B668
		public SortDirection Direction { get; }

		// Token: 0x06001617 RID: 5655 RVA: 0x0003D470 File Offset: 0x0003B670
		public sealed override bool Equals(object obj)
		{
			return this.Equals(obj as QuerySortClause);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0003D480 File Offset: 0x0003B680
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Expression.GetHashCode(), this.Direction.GetHashCode());
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0003D4B1 File Offset: 0x0003B6B1
		public bool Equals(QuerySortClause other)
		{
			return this == other || (other != null && this.Expression.Equals(other.Expression) && this.Direction == other.Direction);
		}
	}
}
