using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000218 RID: 536
	[ImmutableObject(true)]
	public sealed class ResolvedQueryContainsExpression : ResolvedQueryBinaryExpression
	{
		// Token: 0x06000F81 RID: 3969 RVA: 0x0001D9F2 File Offset: 0x0001BBF2
		internal ResolvedQueryContainsExpression(ResolvedQueryExpression left, ResolvedQueryExpression right)
			: base(left, right)
		{
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0001D9FC File Offset: 0x0001BBFC
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0001DA05 File Offset: 0x0001BC05
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0001DA0E File Offset: 0x0001BC0E
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryContainsExpression);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0001DA1D File Offset: 0x0001BC1D
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
