using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000A6 RID: 166
	internal sealed class ModelSortBindingInfo
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x000178B6 File Offset: 0x00015AB6
		internal ModelSortBindingInfo(IConceptualColumn field, IReadOnlyList<int> sourceSelects)
		{
			this.Field = field;
			this.SourceSelects = sourceSelects;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x000178CC File Offset: 0x00015ACC
		internal IConceptualColumn Field { get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x000178D4 File Offset: 0x00015AD4
		internal IReadOnlyList<int> SourceSelects { get; }
	}
}
