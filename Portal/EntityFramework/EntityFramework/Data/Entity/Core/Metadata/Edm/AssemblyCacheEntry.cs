using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000507 RID: 1287
	internal abstract class AssemblyCacheEntry
	{
		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06003F95 RID: 16277
		internal abstract IList<EdmType> TypesInAssembly { get; }

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06003F96 RID: 16278
		internal abstract IList<Assembly> ClosureAssemblies { get; }

		// Token: 0x06003F97 RID: 16279 RVA: 0x000D3C7C File Offset: 0x000D1E7C
		internal bool TryGetEdmType(string typeName, out EdmType edmType)
		{
			edmType = null;
			foreach (EdmType edmType2 in this.TypesInAssembly)
			{
				if (edmType2.Identity == typeName)
				{
					edmType = edmType2;
					break;
				}
			}
			return edmType != null;
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x000D3CE0 File Offset: 0x000D1EE0
		internal bool ContainsType(string typeName)
		{
			EdmType edmType = null;
			return this.TryGetEdmType(typeName, out edmType);
		}
	}
}
