using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200025D RID: 605
	[ImmutableObject(true)]
	public sealed class ResolvedQueryTransformOutputRoleRefExpression : ResolvedQueryExpression
	{
		// Token: 0x0600122E RID: 4654 RVA: 0x00020088 File Offset: 0x0001E288
		internal ResolvedQueryTransformOutputRoleRefExpression(string role)
		{
			this._role = role;
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x00020097 File Offset: 0x0001E297
		public string Role
		{
			get
			{
				return this._role;
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0002009F File Offset: 0x0001E29F
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000200A8 File Offset: 0x0001E2A8
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x000200B1 File Offset: 0x0001E2B1
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryTransformOutputRoleRefExpression);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x000200C0 File Offset: 0x0001E2C0
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x040007B7 RID: 1975
		private readonly string _role;
	}
}
