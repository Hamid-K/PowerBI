using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200024C RID: 588
	[ImmutableObject(true)]
	public sealed class ResolvedQueryPrimitiveTypeExpression : ResolvedQueryExpression
	{
		// Token: 0x060011BF RID: 4543 RVA: 0x0001FB4D File Offset: 0x0001DD4D
		internal ResolvedQueryPrimitiveTypeExpression(ConceptualPrimitiveType type)
		{
			this.Type = type;
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0001FB5C File Offset: 0x0001DD5C
		public ConceptualPrimitiveType Type { get; }

		// Token: 0x060011C1 RID: 4545 RVA: 0x0001FB64 File Offset: 0x0001DD64
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0001FB6D File Offset: 0x0001DD6D
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0001FB76 File Offset: 0x0001DD76
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryPrimitiveTypeExpression);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0001FB85 File Offset: 0x0001DD85
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
