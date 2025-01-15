using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000242 RID: 578
	[ImmutableObject(true)]
	public sealed class ResolvedQueryMinExpression : ResolvedQueryUnaryExpression
	{
		// Token: 0x06001181 RID: 4481 RVA: 0x0001F88B File Offset: 0x0001DA8B
		internal ResolvedQueryMinExpression(ResolvedQueryExpression expression, IncludeAllTypes includeAllTypes)
			: base(expression)
		{
			this.IncludeAllTypes = includeAllTypes;
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x0001F89B File Offset: 0x0001DA9B
		public IncludeAllTypes IncludeAllTypes { get; }

		// Token: 0x06001183 RID: 4483 RVA: 0x0001F8A3 File Offset: 0x0001DAA3
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0001F8AC File Offset: 0x0001DAAC
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0001F8B5 File Offset: 0x0001DAB5
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryMinExpression);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0001F8C4 File Offset: 0x0001DAC4
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}
	}
}
