using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200055E RID: 1374
	public abstract class TypeMapping : MappingItem
	{
		// Token: 0x0600431E RID: 17182 RVA: 0x000E6E7D File Offset: 0x000E507D
		internal TypeMapping()
		{
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x0600431F RID: 17183
		internal abstract EntitySetBaseMapping SetMapping { get; }

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06004320 RID: 17184
		internal abstract ReadOnlyCollection<EntityTypeBase> Types { get; }

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06004321 RID: 17185
		internal abstract ReadOnlyCollection<EntityTypeBase> IsOfTypes { get; }

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06004322 RID: 17186
		internal abstract ReadOnlyCollection<MappingFragment> MappingFragments { get; }
	}
}
