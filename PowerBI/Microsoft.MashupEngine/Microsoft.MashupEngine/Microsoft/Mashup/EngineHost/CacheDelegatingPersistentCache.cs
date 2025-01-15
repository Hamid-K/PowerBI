using System;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x0200197E RID: 6526
	public abstract class CacheDelegatingPersistentCache : DelegatingPersistentCache
	{
		// Token: 0x0600A59A RID: 42394 RVA: 0x0022439E File Offset: 0x0022259E
		public CacheDelegatingPersistentCache(PersistentCache cache)
		{
			this.cache = cache;
		}

		// Token: 0x17002A49 RID: 10825
		// (get) Token: 0x0600A59B RID: 42395 RVA: 0x002243AD File Offset: 0x002225AD
		protected sealed override PersistentCache Cache
		{
			get
			{
				return this.cache;
			}
		}

		// Token: 0x04005632 RID: 22066
		private readonly PersistentCache cache;
	}
}
