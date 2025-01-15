using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000256 RID: 598
	public sealed class ResolvedQueryStartsWithExpression : ResolvedQueryBinaryExpression
	{
		// Token: 0x06001204 RID: 4612 RVA: 0x0001FE86 File Offset: 0x0001E086
		internal ResolvedQueryStartsWithExpression(ResolvedQueryExpression left, ResolvedQueryExpression right)
			: base(left, right)
		{
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0001FE90 File Offset: 0x0001E090
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0001FE99 File Offset: 0x0001E099
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0001FEA2 File Offset: 0x0001E0A2
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryStartsWithExpression);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0001FEB1 File Offset: 0x0001E0B1
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
