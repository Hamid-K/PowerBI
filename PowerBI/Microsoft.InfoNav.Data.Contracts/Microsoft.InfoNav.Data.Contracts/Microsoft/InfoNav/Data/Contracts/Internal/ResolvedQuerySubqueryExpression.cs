using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000257 RID: 599
	[ImmutableObject(true)]
	public class ResolvedQuerySubqueryExpression : ResolvedQueryExpression
	{
		// Token: 0x06001209 RID: 4617 RVA: 0x0001FEBA File Offset: 0x0001E0BA
		internal ResolvedQuerySubqueryExpression(ResolvedQueryDefinition subquery)
		{
			this.Subquery = subquery;
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0001FEC9 File Offset: 0x0001E0C9
		public ResolvedQueryDefinition Subquery { get; }

		// Token: 0x0600120B RID: 4619 RVA: 0x0001FED1 File Offset: 0x0001E0D1
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0001FEDA File Offset: 0x0001E0DA
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0001FEE3 File Offset: 0x0001E0E3
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQuerySubqueryExpression);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0001FEF2 File Offset: 0x0001E0F2
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
