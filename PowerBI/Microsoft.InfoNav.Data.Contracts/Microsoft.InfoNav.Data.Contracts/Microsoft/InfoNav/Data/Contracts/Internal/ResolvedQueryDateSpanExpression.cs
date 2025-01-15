using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200021A RID: 538
	[ImmutableObject(true)]
	public sealed class ResolvedQueryDateSpanExpression : ResolvedQueryUnaryExpression
	{
		// Token: 0x06000F8D RID: 3981 RVA: 0x0001DA77 File Offset: 0x0001BC77
		internal ResolvedQueryDateSpanExpression(TimeUnit timeUnit, ResolvedQueryExpression expression)
			: base(expression)
		{
			this._timeUnit = timeUnit;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0001DA87 File Offset: 0x0001BC87
		public TimeUnit TimeUnit
		{
			get
			{
				return this._timeUnit;
			}
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0001DA8F File Offset: 0x0001BC8F
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0001DA98 File Offset: 0x0001BC98
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0001DAA1 File Offset: 0x0001BCA1
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryDateSpanExpression);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0001DAB0 File Offset: 0x0001BCB0
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x04000730 RID: 1840
		private readonly TimeUnit _timeUnit;
	}
}
