using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200050B RID: 1291
	internal class KnownAssembliesSet
	{
		// Token: 0x06003FA7 RID: 16295 RVA: 0x000D3F00 File Offset: 0x000D2100
		internal KnownAssembliesSet()
		{
			this._assemblies = new Dictionary<Assembly, KnownAssemblyEntry>();
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x000D3F13 File Offset: 0x000D2113
		internal KnownAssembliesSet(KnownAssembliesSet set)
		{
			this._assemblies = new Dictionary<Assembly, KnownAssemblyEntry>(set._assemblies);
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x000D3F2C File Offset: 0x000D212C
		internal virtual bool TryGetKnownAssembly(Assembly assembly, object loaderCookie, EdmItemCollection itemCollection, out KnownAssemblyEntry entry)
		{
			return this._assemblies.TryGetValue(assembly, out entry) && entry.HaveSeenInCompatibleContext(loaderCookie, itemCollection);
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06003FAA RID: 16298 RVA: 0x000D3F4F File Offset: 0x000D214F
		internal IEnumerable<Assembly> Assemblies
		{
			get
			{
				return this._assemblies.Keys;
			}
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x000D3F5C File Offset: 0x000D215C
		public IEnumerable<KnownAssemblyEntry> GetEntries(object loaderCookie, EdmItemCollection itemCollection)
		{
			return this._assemblies.Values.Where((KnownAssemblyEntry e) => e.HaveSeenInCompatibleContext(loaderCookie, itemCollection));
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x000D3F9C File Offset: 0x000D219C
		internal bool Contains(Assembly assembly, object loaderCookie, EdmItemCollection itemCollection)
		{
			KnownAssemblyEntry knownAssemblyEntry;
			return this.TryGetKnownAssembly(assembly, loaderCookie, itemCollection, out knownAssemblyEntry);
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x000D3FB4 File Offset: 0x000D21B4
		internal void Add(Assembly assembly, KnownAssemblyEntry knownAssemblyEntry)
		{
			KnownAssemblyEntry knownAssemblyEntry2;
			if (this._assemblies.TryGetValue(assembly, out knownAssemblyEntry2))
			{
				this._assemblies[assembly] = knownAssemblyEntry;
				return;
			}
			this._assemblies.Add(assembly, knownAssemblyEntry);
		}

		// Token: 0x0400163A RID: 5690
		private readonly Dictionary<Assembly, KnownAssemblyEntry> _assemblies;
	}
}
