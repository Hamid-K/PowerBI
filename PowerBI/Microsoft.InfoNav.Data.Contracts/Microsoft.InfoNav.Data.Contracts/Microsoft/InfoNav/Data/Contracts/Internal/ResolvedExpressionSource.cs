using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200020B RID: 523
	[ImmutableObject(true)]
	public sealed class ResolvedExpressionSource : ResolvedQuerySource
	{
		// Token: 0x06000F37 RID: 3895 RVA: 0x0001D68A File Offset: 0x0001B88A
		internal ResolvedExpressionSource(string name, ResolvedQueryExpression expression)
			: base(name)
		{
			this.Expression = expression;
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0001D69A File Offset: 0x0001B89A
		public ResolvedQueryExpression Expression { get; }

		// Token: 0x06000F39 RID: 3897 RVA: 0x0001D6A2 File Offset: 0x0001B8A2
		public override bool AcceptEquals(ResolvedQueryDefinitionEqualityComparer comparer, ResolvedQuerySource other)
		{
			return comparer.Equals(this, other as ResolvedExpressionSource);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0001D6B1 File Offset: 0x0001B8B1
		public override int AcceptGetHashCode(ResolvedQueryDefinitionEqualityComparer comparer)
		{
			return comparer.GetHashCode(this);
		}
	}
}
