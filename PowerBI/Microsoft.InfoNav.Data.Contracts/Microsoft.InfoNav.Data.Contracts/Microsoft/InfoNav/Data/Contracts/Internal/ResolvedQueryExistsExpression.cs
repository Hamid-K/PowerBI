using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200022D RID: 557
	[ImmutableObject(true)]
	public sealed class ResolvedQueryExistsExpression : ResolvedQueryUnaryExpression
	{
		// Token: 0x06001012 RID: 4114 RVA: 0x0001E5C5 File Offset: 0x0001C7C5
		internal ResolvedQueryExistsExpression(ResolvedQueryExpression expression)
			: base(expression)
		{
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0001E5CE File Offset: 0x0001C7CE
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0001E5D7 File Offset: 0x0001C7D7
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0001E5E0 File Offset: 0x0001C7E0
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryExistsExpression);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0001E5EF File Offset: 0x0001C7EF
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
