using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000243 RID: 579
	[ImmutableObject(true)]
	public sealed class ResolvedQueryNativeFormatExpression : ResolvedQueryUnaryExpression
	{
		// Token: 0x06001187 RID: 4487 RVA: 0x0001F8CD File Offset: 0x0001DACD
		internal ResolvedQueryNativeFormatExpression(ResolvedQueryExpression expression, string formatString)
			: base(expression)
		{
			this.FormatString = formatString;
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0001F8DD File Offset: 0x0001DADD
		public string FormatString { get; }

		// Token: 0x06001189 RID: 4489 RVA: 0x0001F8E5 File Offset: 0x0001DAE5
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0001F8EE File Offset: 0x0001DAEE
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0001F8F7 File Offset: 0x0001DAF7
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryNativeFormatExpression);
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0001F906 File Offset: 0x0001DB06
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
