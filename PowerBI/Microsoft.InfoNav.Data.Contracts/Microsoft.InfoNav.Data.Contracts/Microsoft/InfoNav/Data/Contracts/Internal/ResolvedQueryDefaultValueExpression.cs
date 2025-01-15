using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200021B RID: 539
	[ImmutableObject(true)]
	public sealed class ResolvedQueryDefaultValueExpression : ResolvedQueryExpression
	{
		// Token: 0x06000F93 RID: 3987 RVA: 0x0001DAB9 File Offset: 0x0001BCB9
		private ResolvedQueryDefaultValueExpression()
		{
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0001DAC1 File Offset: 0x0001BCC1
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0001DACA File Offset: 0x0001BCCA
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0001DAD3 File Offset: 0x0001BCD3
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryDefaultValueExpression);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0001DAE2 File Offset: 0x0001BCE2
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x04000731 RID: 1841
		internal static readonly ResolvedQueryDefaultValueExpression Instance = new ResolvedQueryDefaultValueExpression();
	}
}
