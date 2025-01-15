using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002ED RID: 749
	public sealed class SemanticQueryDefinitionBuilder : SemanticQueryDefinitionBuilder<SemanticQueryDataShapeCommandBuilder>
	{
		// Token: 0x060018DF RID: 6367 RVA: 0x0002CA5C File Offset: 0x0002AC5C
		public SemanticQueryDefinitionBuilder()
			: this(null)
		{
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0002CA78 File Offset: 0x0002AC78
		public SemanticQueryDefinitionBuilder(int? version)
			: this(null, null, version)
		{
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0002CA83 File Offset: 0x0002AC83
		public SemanticQueryDefinitionBuilder(SemanticQueryDataShapeCommandBuilder parent, Action<QueryDefinition> addToParent, int? version)
			: base(parent, addToParent, version)
		{
		}
	}
}
