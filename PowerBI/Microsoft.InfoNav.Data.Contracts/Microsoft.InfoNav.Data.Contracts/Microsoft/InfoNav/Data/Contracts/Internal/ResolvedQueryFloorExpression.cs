using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000238 RID: 568
	[ImmutableObject(true)]
	public sealed class ResolvedQueryFloorExpression : ResolvedQueryUnaryExpression
	{
		// Token: 0x0600113B RID: 4411 RVA: 0x0001F560 File Offset: 0x0001D760
		internal ResolvedQueryFloorExpression(ResolvedQueryExpression expression, double size, TimeUnit? timeUnit)
			: base(expression)
		{
			this._size = size;
			this._timeUnit = timeUnit;
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0001F577 File Offset: 0x0001D777
		public double Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0001F57F File Offset: 0x0001D77F
		public TimeUnit? TimeUnit
		{
			get
			{
				return this._timeUnit;
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0001F587 File Offset: 0x0001D787
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0001F590 File Offset: 0x0001D790
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0001F599 File Offset: 0x0001D799
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryFloorExpression);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0001F5A8 File Offset: 0x0001D7A8
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x0400077A RID: 1914
		private readonly double _size;

		// Token: 0x0400077B RID: 1915
		private readonly TimeUnit? _timeUnit;
	}
}
