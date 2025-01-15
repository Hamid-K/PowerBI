using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200024B RID: 587
	[ImmutableObject(true)]
	public sealed class ResolvedQueryPercentileExpression : ResolvedQueryUnaryExpression
	{
		// Token: 0x060011B8 RID: 4536 RVA: 0x0001FAFC File Offset: 0x0001DCFC
		internal ResolvedQueryPercentileExpression(bool exclusive, double k, ResolvedQueryExpression expression)
			: base(expression)
		{
			this._exclusive = exclusive;
			this._k = k;
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0001FB13 File Offset: 0x0001DD13
		public bool Exclusive
		{
			get
			{
				return this._exclusive;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0001FB1B File Offset: 0x0001DD1B
		public double K
		{
			get
			{
				return this._k;
			}
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0001FB23 File Offset: 0x0001DD23
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0001FB2C File Offset: 0x0001DD2C
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0001FB35 File Offset: 0x0001DD35
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryPercentileExpression);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0001FB44 File Offset: 0x0001DD44
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x04000795 RID: 1941
		private readonly bool _exclusive;

		// Token: 0x04000796 RID: 1942
		private readonly double _k;
	}
}
