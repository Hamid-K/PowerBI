using System;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000016 RID: 22
	internal sealed class SelectSortIdentityKeyBuilder
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00005585 File Offset: 0x00003785
		internal SelectSortIdentityKeyBuilder(ConceptualPropertyReference source, IConceptualColumn internalLineageColumn, string calc)
		{
			this.Source = source;
			this.InternalLineageColumn = internalLineageColumn;
			this.Calc = calc;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000055A2 File Offset: 0x000037A2
		internal string Calc { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000055AA File Offset: 0x000037AA
		internal ConceptualPropertyReference Source { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000055B2 File Offset: 0x000037B2
		internal IConceptualColumn InternalLineageColumn { get; }
	}
}
