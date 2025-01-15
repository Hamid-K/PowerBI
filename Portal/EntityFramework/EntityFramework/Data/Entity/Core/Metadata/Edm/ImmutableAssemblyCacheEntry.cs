using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200050A RID: 1290
	internal class ImmutableAssemblyCacheEntry : AssemblyCacheEntry
	{
		// Token: 0x06003FA4 RID: 16292 RVA: 0x000D3EBC File Offset: 0x000D20BC
		internal ImmutableAssemblyCacheEntry(MutableAssemblyCacheEntry mutableEntry)
		{
			this._typesInAssembly = new ReadOnlyCollection<EdmType>(new List<EdmType>(mutableEntry.TypesInAssembly));
			this._closureAssemblies = new ReadOnlyCollection<Assembly>(new List<Assembly>(mutableEntry.ClosureAssemblies));
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06003FA5 RID: 16293 RVA: 0x000D3EF0 File Offset: 0x000D20F0
		internal override IList<EdmType> TypesInAssembly
		{
			get
			{
				return this._typesInAssembly;
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06003FA6 RID: 16294 RVA: 0x000D3EF8 File Offset: 0x000D20F8
		internal override IList<Assembly> ClosureAssemblies
		{
			get
			{
				return this._closureAssemblies;
			}
		}

		// Token: 0x04001638 RID: 5688
		private readonly ReadOnlyCollection<EdmType> _typesInAssembly;

		// Token: 0x04001639 RID: 5689
		private readonly ReadOnlyCollection<Assembly> _closureAssemblies;
	}
}
