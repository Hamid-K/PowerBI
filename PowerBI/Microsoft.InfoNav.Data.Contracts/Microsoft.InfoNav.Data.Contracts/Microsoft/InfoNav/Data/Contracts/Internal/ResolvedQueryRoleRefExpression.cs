using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200024F RID: 591
	[ImmutableObject(true)]
	public class ResolvedQueryRoleRefExpression : ResolvedQueryExpression
	{
		// Token: 0x060011D1 RID: 4561 RVA: 0x0001FC2F File Offset: 0x0001DE2F
		internal ResolvedQueryRoleRefExpression(string role)
		{
			this.Role = role;
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x0001FC3E File Offset: 0x0001DE3E
		public string Role { get; }

		// Token: 0x060011D3 RID: 4563 RVA: 0x0001FC46 File Offset: 0x0001DE46
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0001FC4F File Offset: 0x0001DE4F
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0001FC58 File Offset: 0x0001DE58
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryRoleRefExpression);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0001FC67 File Offset: 0x0001DE67
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
