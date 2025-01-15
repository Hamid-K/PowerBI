using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200050C RID: 1292
	internal sealed class KnownAssemblyEntry
	{
		// Token: 0x06003FAE RID: 16302 RVA: 0x000D3FEC File Offset: 0x000D21EC
		internal KnownAssemblyEntry(AssemblyCacheEntry cacheEntry, bool seenWithEdmItemCollection)
		{
			this._cacheEntry = cacheEntry;
			this.ReferencedAssembliesAreLoaded = false;
			this.SeenWithEdmItemCollection = seenWithEdmItemCollection;
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06003FAF RID: 16303 RVA: 0x000D4009 File Offset: 0x000D2209
		internal AssemblyCacheEntry CacheEntry
		{
			get
			{
				return this._cacheEntry;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06003FB0 RID: 16304 RVA: 0x000D4011 File Offset: 0x000D2211
		// (set) Token: 0x06003FB1 RID: 16305 RVA: 0x000D4019 File Offset: 0x000D2219
		public bool ReferencedAssembliesAreLoaded { get; set; }

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x000D4022 File Offset: 0x000D2222
		// (set) Token: 0x06003FB3 RID: 16307 RVA: 0x000D402A File Offset: 0x000D222A
		public bool SeenWithEdmItemCollection { get; set; }

		// Token: 0x06003FB4 RID: 16308 RVA: 0x000D4033 File Offset: 0x000D2233
		public bool HaveSeenInCompatibleContext(object loaderCookie, EdmItemCollection itemCollection)
		{
			return this.SeenWithEdmItemCollection || itemCollection == null || ObjectItemAssemblyLoader.IsAttributeLoader(loaderCookie);
		}

		// Token: 0x0400163B RID: 5691
		private readonly AssemblyCacheEntry _cacheEntry;
	}
}
