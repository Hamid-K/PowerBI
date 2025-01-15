using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000510 RID: 1296
	internal class MutableAssemblyCacheEntry : AssemblyCacheEntry
	{
		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06003FC7 RID: 16327 RVA: 0x000D42BB File Offset: 0x000D24BB
		internal override IList<EdmType> TypesInAssembly
		{
			get
			{
				return this._typesInAssembly;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x000D42C3 File Offset: 0x000D24C3
		internal override IList<Assembly> ClosureAssemblies
		{
			get
			{
				return this._closureAssemblies;
			}
		}

		// Token: 0x04001647 RID: 5703
		private readonly List<EdmType> _typesInAssembly = new List<EdmType>();

		// Token: 0x04001648 RID: 5704
		private readonly List<Assembly> _closureAssemblies = new List<Assembly>();
	}
}
