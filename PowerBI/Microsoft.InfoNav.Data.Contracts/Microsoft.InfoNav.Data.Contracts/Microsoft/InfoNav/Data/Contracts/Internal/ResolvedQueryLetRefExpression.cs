using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200023D RID: 573
	[ImmutableObject(true)]
	public class ResolvedQueryLetRefExpression : ResolvedQueryExpression
	{
		// Token: 0x06001162 RID: 4450 RVA: 0x0001F73B File Offset: 0x0001D93B
		internal ResolvedQueryLetRefExpression(ResolvedQueryLetBinding binding)
		{
			this.Binding = binding;
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0001F74A File Offset: 0x0001D94A
		public ResolvedQueryLetBinding Binding { get; }

		// Token: 0x06001164 RID: 4452 RVA: 0x0001F752 File Offset: 0x0001D952
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0001F75B File Offset: 0x0001D95B
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0001F764 File Offset: 0x0001D964
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryLetRefExpression);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0001F773 File Offset: 0x0001D973
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
