using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000252 RID: 594
	[ImmutableObject(true)]
	public sealed class ResolvedQuerySortClause : IEquatable<ResolvedQuerySortClause>
	{
		// Token: 0x060011E5 RID: 4581 RVA: 0x0001FD1E File Offset: 0x0001DF1E
		internal ResolvedQuerySortClause(ResolvedQueryExpression expression, QuerySortDirection direction)
		{
			this.Expression = expression;
			this.Direction = direction;
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0001FD34 File Offset: 0x0001DF34
		public ResolvedQueryExpression Expression { get; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0001FD3C File Offset: 0x0001DF3C
		public QuerySortDirection Direction { get; }

		// Token: 0x060011E8 RID: 4584 RVA: 0x0001FD44 File Offset: 0x0001DF44
		public bool Equals(ResolvedQuerySortClause other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0001FD52 File Offset: 0x0001DF52
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0001FD5F File Offset: 0x0001DF5F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQuerySortClause);
		}
	}
}
