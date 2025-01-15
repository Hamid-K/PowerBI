using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200020D RID: 525
	[ImmutableObject(true)]
	public sealed class ResolvedQueryAggregationExpression : ResolvedQueryUnaryExpression
	{
		// Token: 0x06000F3E RID: 3902 RVA: 0x0001D6E0 File Offset: 0x0001B8E0
		internal ResolvedQueryAggregationExpression(ResolvedQueryExpression expression, QueryAggregateFunction function)
			: base(expression)
		{
			this._function = function;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x0001D6F0 File Offset: 0x0001B8F0
		public QueryAggregateFunction Function
		{
			get
			{
				return this._function;
			}
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0001D6F8 File Offset: 0x0001B8F8
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0001D701 File Offset: 0x0001B901
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0001D70A File Offset: 0x0001B90A
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryAggregationExpression);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0001D719 File Offset: 0x0001B919
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x0400071C RID: 1820
		private readonly QueryAggregateFunction _function;
	}
}
