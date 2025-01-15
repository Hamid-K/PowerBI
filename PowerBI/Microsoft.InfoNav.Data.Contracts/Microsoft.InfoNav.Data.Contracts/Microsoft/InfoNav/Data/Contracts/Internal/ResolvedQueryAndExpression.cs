using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200020E RID: 526
	[ImmutableObject(true)]
	public sealed class ResolvedQueryAndExpression : ResolvedQueryBinaryExpression
	{
		// Token: 0x06000F44 RID: 3908 RVA: 0x0001D722 File Offset: 0x0001B922
		internal ResolvedQueryAndExpression(ResolvedQueryExpression left, ResolvedQueryExpression right)
			: base(left, right)
		{
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0001D72C File Offset: 0x0001B92C
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0001D735 File Offset: 0x0001B935
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0001D73E File Offset: 0x0001B93E
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryAndExpression);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0001D74D File Offset: 0x0001B94D
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
