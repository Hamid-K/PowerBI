using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000261 RID: 609
	[ImmutableObject(true)]
	public sealed class ResolvedQueryTransformTableColumnExpression : ResolvedQueryExpression
	{
		// Token: 0x06001248 RID: 4680 RVA: 0x0002022F File Offset: 0x0001E42F
		internal ResolvedQueryTransformTableColumnExpression(ResolvedQueryTransformTable table, ResolvedQueryTransformTableColumn column)
		{
			this._table = table;
			this._column = column;
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x00020245 File Offset: 0x0001E445
		public ResolvedQueryTransformTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0002024D File Offset: 0x0001E44D
		public ResolvedQueryTransformTableColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00020255 File Offset: 0x0001E455
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0002025E File Offset: 0x0001E45E
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00020267 File Offset: 0x0001E467
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryTransformTableColumnExpression);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00020276 File Offset: 0x0001E476
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x040007BF RID: 1983
		private readonly ResolvedQueryTransformTable _table;

		// Token: 0x040007C0 RID: 1984
		private readonly ResolvedQueryTransformTableColumn _column;
	}
}
