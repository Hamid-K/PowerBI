using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000240 RID: 576
	[ImmutableObject(true)]
	public sealed class ResolvedQueryMeasureExpression : ResolvedQueryPropertyExpression
	{
		// Token: 0x06001174 RID: 4468 RVA: 0x0001F7FF File Offset: 0x0001D9FF
		internal ResolvedQueryMeasureExpression(ResolvedQueryExpression expression, IConceptualMeasure measure)
			: base(expression)
		{
			this._measure = measure;
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0001F80F File Offset: 0x0001DA0F
		public IConceptualMeasure Measure
		{
			get
			{
				return this._measure;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0001F817 File Offset: 0x0001DA17
		public override IConceptualProperty Property
		{
			get
			{
				return this._measure;
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0001F81F File Offset: 0x0001DA1F
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0001F828 File Offset: 0x0001DA28
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0001F831 File Offset: 0x0001DA31
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryMeasureExpression);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0001F840 File Offset: 0x0001DA40
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x04000789 RID: 1929
		private readonly IConceptualMeasure _measure;
	}
}
