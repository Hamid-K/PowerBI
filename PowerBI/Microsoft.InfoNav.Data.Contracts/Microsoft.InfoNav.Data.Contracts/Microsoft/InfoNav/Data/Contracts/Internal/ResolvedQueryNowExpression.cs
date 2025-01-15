using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000247 RID: 583
	[ImmutableObject(true)]
	public sealed class ResolvedQueryNowExpression : ResolvedQueryExpression
	{
		// Token: 0x060011A1 RID: 4513 RVA: 0x0001F9FA File Offset: 0x0001DBFA
		private ResolvedQueryNowExpression()
		{
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0001FA02 File Offset: 0x0001DC02
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0001FA0B File Offset: 0x0001DC0B
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0001FA14 File Offset: 0x0001DC14
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryNowExpression);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0001FA23 File Offset: 0x0001DC23
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x04000791 RID: 1937
		internal static readonly ResolvedQueryNowExpression Instance = new ResolvedQueryNowExpression();
	}
}
