using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200050E RID: 1294
	internal class LockedAssemblyCache : IDisposable
	{
		// Token: 0x06003FBA RID: 16314 RVA: 0x000D4122 File Offset: 0x000D2322
		internal LockedAssemblyCache(object lockObject, Dictionary<Assembly, ImmutableAssemblyCacheEntry> globalAssemblyCache)
		{
			this._lockObject = lockObject;
			this._globalAssemblyCache = globalAssemblyCache;
			Monitor.Enter(this._lockObject);
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x000D4143 File Offset: 0x000D2343
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			Monitor.Exit(this._lockObject);
			this._lockObject = null;
			this._globalAssemblyCache = null;
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x000D4164 File Offset: 0x000D2364
		[Conditional("DEBUG")]
		private void AssertLockedByThisThread()
		{
			bool flag = false;
			Monitor.TryEnter(this._lockObject, ref flag);
			if (flag)
			{
				Monitor.Exit(this._lockObject);
			}
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x000D418E File Offset: 0x000D238E
		internal bool TryGetValue(Assembly assembly, out ImmutableAssemblyCacheEntry cacheEntry)
		{
			return this._globalAssemblyCache.TryGetValue(assembly, out cacheEntry);
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x000D419D File Offset: 0x000D239D
		internal void Add(Assembly assembly, ImmutableAssemblyCacheEntry assemblyCacheEntry)
		{
			this._globalAssemblyCache.Add(assembly, assemblyCacheEntry);
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x000D41AC File Offset: 0x000D23AC
		internal void Clear()
		{
			this._globalAssemblyCache.Clear();
		}

		// Token: 0x04001640 RID: 5696
		private object _lockObject;

		// Token: 0x04001641 RID: 5697
		private Dictionary<Assembly, ImmutableAssemblyCacheEntry> _globalAssemblyCache;
	}
}
