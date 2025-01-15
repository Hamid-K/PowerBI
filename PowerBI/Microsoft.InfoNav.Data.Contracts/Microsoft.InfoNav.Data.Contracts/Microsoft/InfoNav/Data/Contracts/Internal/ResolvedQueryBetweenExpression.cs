using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000213 RID: 531
	[ImmutableObject(true)]
	public sealed class ResolvedQueryBetweenExpression : ResolvedQueryExpression
	{
		// Token: 0x06000F62 RID: 3938 RVA: 0x0001D890 File Offset: 0x0001BA90
		internal ResolvedQueryBetweenExpression(ResolvedQueryExpression expression, ResolvedQueryExpression lowerBound, ResolvedQueryExpression upperBound)
		{
			this._expression = expression;
			this._lowerBound = lowerBound;
			this._upperBound = upperBound;
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0001D8AD File Offset: 0x0001BAAD
		public ResolvedQueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0001D8B5 File Offset: 0x0001BAB5
		public ResolvedQueryExpression LowerBound
		{
			get
			{
				return this._lowerBound;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0001D8BD File Offset: 0x0001BABD
		public ResolvedQueryExpression UpperBound
		{
			get
			{
				return this._upperBound;
			}
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0001D8C5 File Offset: 0x0001BAC5
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0001D8CE File Offset: 0x0001BACE
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0001D8D7 File Offset: 0x0001BAD7
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryBetweenExpression);
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0001D8E6 File Offset: 0x0001BAE6
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x04000725 RID: 1829
		private readonly ResolvedQueryExpression _expression;

		// Token: 0x04000726 RID: 1830
		private readonly ResolvedQueryExpression _lowerBound;

		// Token: 0x04000727 RID: 1831
		private readonly ResolvedQueryExpression _upperBound;
	}
}
