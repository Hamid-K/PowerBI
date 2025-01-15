using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000237 RID: 567
	[ImmutableObject(true)]
	public sealed class ResolvedQueryFilteredEvalExpression : ResolvedQueryExpression
	{
		// Token: 0x06001134 RID: 4404 RVA: 0x0001F510 File Offset: 0x0001D710
		internal ResolvedQueryFilteredEvalExpression(ResolvedQueryExpression expression, IReadOnlyList<ResolvedQueryFilter> filters)
		{
			this._expression = expression;
			this._filters = filters;
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0001F526 File Offset: 0x0001D726
		public ResolvedQueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0001F52E File Offset: 0x0001D72E
		public IReadOnlyList<ResolvedQueryFilter> Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0001F536 File Offset: 0x0001D736
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0001F53F File Offset: 0x0001D73F
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0001F548 File Offset: 0x0001D748
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryFilteredEvalExpression);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0001F557 File Offset: 0x0001D757
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x04000778 RID: 1912
		private readonly ResolvedQueryExpression _expression;

		// Token: 0x04000779 RID: 1913
		private readonly IReadOnlyList<ResolvedQueryFilter> _filters;
	}
}
