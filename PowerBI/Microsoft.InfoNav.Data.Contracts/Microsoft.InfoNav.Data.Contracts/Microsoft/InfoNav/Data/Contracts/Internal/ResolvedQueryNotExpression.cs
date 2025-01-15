using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000246 RID: 582
	[ImmutableObject(true)]
	public sealed class ResolvedQueryNotExpression : ResolvedQueryUnaryExpression
	{
		// Token: 0x0600119B RID: 4507 RVA: 0x0001F9AF File Offset: 0x0001DBAF
		internal ResolvedQueryNotExpression(ResolvedQueryExpression expression)
			: base(expression)
		{
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0001F9B8 File Offset: 0x0001DBB8
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetType().GetHashCode(), base.GetHashCode());
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0001F9D0 File Offset: 0x0001DBD0
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0001F9D9 File Offset: 0x0001DBD9
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0001F9E2 File Offset: 0x0001DBE2
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryNotExpression);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0001F9F1 File Offset: 0x0001DBF1
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
