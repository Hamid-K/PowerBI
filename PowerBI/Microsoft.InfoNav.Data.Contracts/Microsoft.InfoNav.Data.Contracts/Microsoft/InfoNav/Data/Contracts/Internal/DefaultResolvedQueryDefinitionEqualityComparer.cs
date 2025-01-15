using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001FF RID: 511
	public sealed class DefaultResolvedQueryDefinitionEqualityComparer : ResolvedQueryDefinitionEqualsComparer
	{
		// Token: 0x06000DF4 RID: 3572 RVA: 0x0001B460 File Offset: 0x00019660
		private DefaultResolvedQueryDefinitionEqualityComparer()
		{
			this.ExpressionComparer = new DefaultResolvedQueryExpressionEqualityComparer(this);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0001B474 File Offset: 0x00019674
		internal DefaultResolvedQueryDefinitionEqualityComparer(DefaultResolvedQueryExpressionEqualityComparer expressionComparer)
		{
			this.ExpressionComparer = expressionComparer;
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0001B483 File Offset: 0x00019683
		public static DefaultResolvedQueryDefinitionEqualityComparer Instance { get; } = new DefaultResolvedQueryDefinitionEqualityComparer();

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0001B48A File Offset: 0x0001968A
		protected override ResolvedQueryExpressionEqualityComparer ExpressionComparer { get; }
	}
}
