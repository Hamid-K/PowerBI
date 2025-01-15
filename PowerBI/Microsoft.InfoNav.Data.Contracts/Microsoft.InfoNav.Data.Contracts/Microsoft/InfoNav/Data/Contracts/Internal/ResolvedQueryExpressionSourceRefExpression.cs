using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000231 RID: 561
	[ImmutableObject(true)]
	public sealed class ResolvedQueryExpressionSourceRefExpression : ResolvedQueryExpression
	{
		// Token: 0x0600105C RID: 4188 RVA: 0x0001EF54 File Offset: 0x0001D154
		internal ResolvedQueryExpressionSourceRefExpression(string sourceName, ResolvedQueryExpression expression)
		{
			this.SourceName = sourceName;
			this.Expression = expression;
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x0001EF6A File Offset: 0x0001D16A
		public string SourceName { get; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0001EF72 File Offset: 0x0001D172
		public ResolvedQueryExpression Expression { get; }

		// Token: 0x0600105F RID: 4191 RVA: 0x0001EF7A File Offset: 0x0001D17A
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0001EF83 File Offset: 0x0001D183
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0001EF8C File Offset: 0x0001D18C
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryExpressionSourceRefExpression);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0001EF9B File Offset: 0x0001D19B
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
