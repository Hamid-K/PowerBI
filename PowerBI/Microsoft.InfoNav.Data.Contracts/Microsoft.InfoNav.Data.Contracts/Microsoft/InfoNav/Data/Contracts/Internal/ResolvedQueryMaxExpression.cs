using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200023F RID: 575
	[ImmutableObject(true)]
	public sealed class ResolvedQueryMaxExpression : ResolvedQueryUnaryExpression
	{
		// Token: 0x0600116E RID: 4462 RVA: 0x0001F7BD File Offset: 0x0001D9BD
		internal ResolvedQueryMaxExpression(ResolvedQueryExpression expression, IncludeAllTypes includeAllTypes)
			: base(expression)
		{
			this.IncludeAllTypes = includeAllTypes;
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x0001F7CD File Offset: 0x0001D9CD
		public IncludeAllTypes IncludeAllTypes { get; }

		// Token: 0x06001170 RID: 4464 RVA: 0x0001F7D5 File Offset: 0x0001D9D5
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0001F7DE File Offset: 0x0001D9DE
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0001F7E7 File Offset: 0x0001D9E7
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryMaxExpression);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0001F7F6 File Offset: 0x0001D9F6
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
