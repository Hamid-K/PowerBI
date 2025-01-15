using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000241 RID: 577
	[ImmutableObject(true)]
	public sealed class ResolvedQueryMemberExpression : ResolvedQueryUnaryExpression
	{
		// Token: 0x0600117B RID: 4475 RVA: 0x0001F849 File Offset: 0x0001DA49
		internal ResolvedQueryMemberExpression(ResolvedQueryExpression expression, string member)
			: base(expression)
		{
			this._member = member;
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0001F859 File Offset: 0x0001DA59
		public string Member
		{
			get
			{
				return this._member;
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0001F861 File Offset: 0x0001DA61
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0001F86A File Offset: 0x0001DA6A
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0001F873 File Offset: 0x0001DA73
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryMemberExpression);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0001F882 File Offset: 0x0001DA82
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x0400078A RID: 1930
		private readonly string _member;
	}
}
