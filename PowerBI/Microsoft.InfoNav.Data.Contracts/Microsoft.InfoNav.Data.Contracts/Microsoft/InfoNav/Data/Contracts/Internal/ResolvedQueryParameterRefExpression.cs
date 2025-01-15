using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200024A RID: 586
	[ImmutableObject(true)]
	public class ResolvedQueryParameterRefExpression : ResolvedQueryExpression
	{
		// Token: 0x060011B2 RID: 4530 RVA: 0x0001FABB File Offset: 0x0001DCBB
		internal ResolvedQueryParameterRefExpression(ResolvedQueryParameterDeclaration declaration)
		{
			this.Declaration = declaration;
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0001FACA File Offset: 0x0001DCCA
		public ResolvedQueryParameterDeclaration Declaration { get; }

		// Token: 0x060011B4 RID: 4532 RVA: 0x0001FAD2 File Offset: 0x0001DCD2
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0001FADB File Offset: 0x0001DCDB
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0001FAE4 File Offset: 0x0001DCE4
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryParameterRefExpression);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0001FAF3 File Offset: 0x0001DCF3
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
