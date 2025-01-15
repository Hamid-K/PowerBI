using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200022C RID: 556
	public sealed class ResolvedQueryEndsWithExpression : ResolvedQueryBinaryExpression
	{
		// Token: 0x0600100D RID: 4109 RVA: 0x0001E591 File Offset: 0x0001C791
		internal ResolvedQueryEndsWithExpression(ResolvedQueryExpression left, ResolvedQueryExpression right)
			: base(left, right)
		{
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0001E59B File Offset: 0x0001C79B
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0001E5A4 File Offset: 0x0001C7A4
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0001E5AD File Offset: 0x0001C7AD
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryEndsWithExpression);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0001E5BC File Offset: 0x0001C7BC
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
