using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000217 RID: 535
	[ImmutableObject(true)]
	public sealed class ResolvedQueryComparisonExpression : ResolvedQueryBinaryExpression
	{
		// Token: 0x06000F7B RID: 3963 RVA: 0x0001D9AF File Offset: 0x0001BBAF
		internal ResolvedQueryComparisonExpression(ResolvedQueryExpression left, ResolvedQueryExpression right, QueryComparisonKind comparisonKind)
			: base(left, right)
		{
			this._comparisonKind = comparisonKind;
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
		public QueryComparisonKind ComparisonKind
		{
			get
			{
				return this._comparisonKind;
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0001D9C8 File Offset: 0x0001BBC8
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0001D9D1 File Offset: 0x0001BBD1
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0001D9DA File Offset: 0x0001BBDA
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryComparisonExpression);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0001D9E9 File Offset: 0x0001BBE9
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x0400072D RID: 1837
		private readonly QueryComparisonKind _comparisonKind;
	}
}
