using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000200 RID: 512
	public sealed class DefaultResolvedQueryExpressionEqualityComparer : ResolvedQueryExpressionEqualsComparer
	{
		// Token: 0x06000DF9 RID: 3577 RVA: 0x0001B49E File Offset: 0x0001969E
		private DefaultResolvedQueryExpressionEqualityComparer()
		{
			this.StructureComparer = new DefaultResolvedQueryDefinitionEqualityComparer(this);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0001B4B2 File Offset: 0x000196B2
		internal DefaultResolvedQueryExpressionEqualityComparer(ResolvedQueryDefinitionEqualityComparer structureComparer)
		{
			this.StructureComparer = structureComparer;
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x0001B4C1 File Offset: 0x000196C1
		public static DefaultResolvedQueryExpressionEqualityComparer Instance { get; } = new DefaultResolvedQueryExpressionEqualityComparer();

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0001B4C8 File Offset: 0x000196C8
		public override ResolvedQueryDefinitionEqualityComparer StructureComparer { get; }
	}
}
