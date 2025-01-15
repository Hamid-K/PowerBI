using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200023E RID: 574
	[ImmutableObject(true)]
	public sealed class ResolvedQueryLiteralExpression : ResolvedQueryExpression
	{
		// Token: 0x06001168 RID: 4456 RVA: 0x0001F77C File Offset: 0x0001D97C
		internal ResolvedQueryLiteralExpression(PrimitiveValue value)
		{
			this._value = value;
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0001F78B File Offset: 0x0001D98B
		public PrimitiveValue Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0001F793 File Offset: 0x0001D993
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0001F79C File Offset: 0x0001D99C
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0001F7A5 File Offset: 0x0001D9A5
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryLiteralExpression);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x04000787 RID: 1927
		private readonly PrimitiveValue _value;
	}
}
