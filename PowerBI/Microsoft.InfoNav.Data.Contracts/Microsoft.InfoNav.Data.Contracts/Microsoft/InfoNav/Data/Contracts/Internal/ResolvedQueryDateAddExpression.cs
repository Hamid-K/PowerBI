using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000219 RID: 537
	[ImmutableObject(true)]
	public sealed class ResolvedQueryDateAddExpression : ResolvedQueryUnaryExpression
	{
		// Token: 0x06000F86 RID: 3974 RVA: 0x0001DA26 File Offset: 0x0001BC26
		internal ResolvedQueryDateAddExpression(int amount, TimeUnit timeUnit, ResolvedQueryExpression expression)
			: base(expression)
		{
			this._amount = amount;
			this._timeUnit = timeUnit;
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0001DA3D File Offset: 0x0001BC3D
		public int Amount
		{
			get
			{
				return this._amount;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0001DA45 File Offset: 0x0001BC45
		public TimeUnit TimeUnit
		{
			get
			{
				return this._timeUnit;
			}
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0001DA4D File Offset: 0x0001BC4D
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0001DA56 File Offset: 0x0001BC56
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0001DA5F File Offset: 0x0001BC5F
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryDateAddExpression);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0001DA6E File Offset: 0x0001BC6E
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x0400072E RID: 1838
		private readonly int _amount;

		// Token: 0x0400072F RID: 1839
		private readonly TimeUnit _timeUnit;
	}
}
