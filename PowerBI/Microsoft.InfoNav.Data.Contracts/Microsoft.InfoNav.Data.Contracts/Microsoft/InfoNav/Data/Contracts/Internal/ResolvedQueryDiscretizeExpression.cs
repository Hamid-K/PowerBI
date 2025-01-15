using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200022B RID: 555
	[ImmutableObject(true)]
	public sealed class ResolvedQueryDiscretizeExpression : ResolvedQueryUnaryExpression
	{
		// Token: 0x06001007 RID: 4103 RVA: 0x0001E54F File Offset: 0x0001C74F
		internal ResolvedQueryDiscretizeExpression(ResolvedQueryExpression expression, int count)
			: base(expression)
		{
			this._count = count;
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0001E55F File Offset: 0x0001C75F
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0001E567 File Offset: 0x0001C767
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0001E570 File Offset: 0x0001C770
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0001E579 File Offset: 0x0001C779
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryDiscretizeExpression);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0001E588 File Offset: 0x0001C788
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x04000772 RID: 1906
		private readonly int _count;
	}
}
