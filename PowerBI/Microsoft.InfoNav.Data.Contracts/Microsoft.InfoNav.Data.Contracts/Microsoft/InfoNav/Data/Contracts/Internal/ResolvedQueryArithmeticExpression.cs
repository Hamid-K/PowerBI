using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000210 RID: 528
	[ImmutableObject(true)]
	public sealed class ResolvedQueryArithmeticExpression : ResolvedQueryBinaryExpression
	{
		// Token: 0x06000F50 RID: 3920 RVA: 0x0001D7AF File Offset: 0x0001B9AF
		internal ResolvedQueryArithmeticExpression(ResolvedQueryExpression left, ResolvedQueryExpression right, QueryArithmeticOperatorKind arithmeticOperatorKind)
			: base(left, right)
		{
			this._arithmeticOperatorKind = arithmeticOperatorKind;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0001D7C0 File Offset: 0x0001B9C0
		public QueryArithmeticOperatorKind Operator
		{
			get
			{
				return this._arithmeticOperatorKind;
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0001D7C8 File Offset: 0x0001B9C8
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0001D7D1 File Offset: 0x0001B9D1
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0001D7DA File Offset: 0x0001B9DA
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryArithmeticExpression);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0001D7E9 File Offset: 0x0001B9E9
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x04000720 RID: 1824
		private readonly QueryArithmeticOperatorKind _arithmeticOperatorKind;
	}
}
