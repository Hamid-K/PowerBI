using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000024 RID: 36
	[ImmutableObject(true)]
	public sealed class ConceptualGroupingMetadata
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000027BA File Offset: 0x000009BA
		internal ConceptualGroupingMetadata(IReadOnlyList<ConceptualGroupedColumnContainer> groupedColumns, ConceptualBinningMetadata binningMetadata)
		{
			this._groupedColumns = groupedColumns;
			this._binningMetadata = binningMetadata;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000027D0 File Offset: 0x000009D0
		internal IReadOnlyList<ConceptualGroupedColumnContainer> GroupedColumns
		{
			get
			{
				return this._groupedColumns;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000027D8 File Offset: 0x000009D8
		internal ConceptualBinningMetadata BinningMetadata
		{
			get
			{
				return this._binningMetadata;
			}
		}

		// Token: 0x040000B6 RID: 182
		private readonly IReadOnlyList<ConceptualGroupedColumnContainer> _groupedColumns;

		// Token: 0x040000B7 RID: 183
		private readonly ConceptualBinningMetadata _binningMetadata;
	}
}
