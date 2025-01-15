using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000244 RID: 580
	[ImmutableObject(true)]
	public sealed class ResolvedQueryNativeMeasureExpression : ResolvedQueryExpression
	{
		// Token: 0x0600118D RID: 4493 RVA: 0x0001F90F File Offset: 0x0001DB0F
		internal ResolvedQueryNativeMeasureExpression(string language, string expression)
		{
			this.Language = language;
			this.Expression = expression;
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x0001F925 File Offset: 0x0001DB25
		public string Language { get; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0001F92D File Offset: 0x0001DB2D
		public string Expression { get; }

		// Token: 0x06001190 RID: 4496 RVA: 0x0001F935 File Offset: 0x0001DB35
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0001F93E File Offset: 0x0001DB3E
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0001F947 File Offset: 0x0001DB47
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryNativeMeasureExpression);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0001F956 File Offset: 0x0001DB56
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
