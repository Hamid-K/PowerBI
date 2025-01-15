using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000248 RID: 584
	[ImmutableObject(true)]
	public sealed class ResolvedQueryOrExpression : ResolvedQueryBinaryExpression
	{
		// Token: 0x060011A7 RID: 4519 RVA: 0x0001FA38 File Offset: 0x0001DC38
		internal ResolvedQueryOrExpression(ResolvedQueryExpression left, ResolvedQueryExpression right)
			: base(left, right)
		{
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0001FA42 File Offset: 0x0001DC42
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0001FA4B File Offset: 0x0001DC4B
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0001FA54 File Offset: 0x0001DC54
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryOrExpression);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0001FA63 File Offset: 0x0001DC63
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
