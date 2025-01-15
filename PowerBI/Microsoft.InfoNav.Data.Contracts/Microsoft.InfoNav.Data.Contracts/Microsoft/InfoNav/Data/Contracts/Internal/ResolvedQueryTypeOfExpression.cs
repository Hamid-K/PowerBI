using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000262 RID: 610
	[ImmutableObject(true)]
	public sealed class ResolvedQueryTypeOfExpression : ResolvedQueryUnaryExpression
	{
		// Token: 0x0600124F RID: 4687 RVA: 0x0002027F File Offset: 0x0001E47F
		internal ResolvedQueryTypeOfExpression(ResolvedQueryExpression expression)
			: base(expression)
		{
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00020288 File Offset: 0x0001E488
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), base.GetHashCode());
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x000202A0 File Offset: 0x0001E4A0
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x000202A9 File Offset: 0x0001E4A9
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x000202B2 File Offset: 0x0001E4B2
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryTypeOfExpression);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x000202C1 File Offset: 0x0001E4C1
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
