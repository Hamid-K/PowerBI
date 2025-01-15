using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004A6 RID: 1190
	internal class OcAssemblyCache
	{
		// Token: 0x06003A85 RID: 14981 RVA: 0x000C14EC File Offset: 0x000BF6EC
		internal OcAssemblyCache()
		{
			this._conventionalOcCache = new Dictionary<Assembly, ImmutableAssemblyCacheEntry>();
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x000C14FF File Offset: 0x000BF6FF
		internal bool TryGetConventionalOcCacheFromAssemblyCache(Assembly assemblyToLookup, out ImmutableAssemblyCacheEntry cacheEntry)
		{
			cacheEntry = null;
			return this._conventionalOcCache.TryGetValue(assemblyToLookup, out cacheEntry);
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x000C1511 File Offset: 0x000BF711
		internal void AddAssemblyToOcCacheFromAssemblyCache(Assembly assembly, ImmutableAssemblyCacheEntry cacheEntry)
		{
			if (this._conventionalOcCache.ContainsKey(assembly))
			{
				return;
			}
			this._conventionalOcCache.Add(assembly, cacheEntry);
		}

		// Token: 0x04001421 RID: 5153
		private readonly Dictionary<Assembly, ImmutableAssemblyCacheEntry> _conventionalOcCache;
	}
}
