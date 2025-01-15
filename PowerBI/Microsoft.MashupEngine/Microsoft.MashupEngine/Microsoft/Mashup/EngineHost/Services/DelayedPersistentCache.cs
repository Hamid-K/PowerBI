using System;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019AC RID: 6572
	public class DelayedPersistentCache : DelegatingPersistentCache
	{
		// Token: 0x0600A68D RID: 42637 RVA: 0x002274B3 File Offset: 0x002256B3
		public DelayedPersistentCache(long maxEntryLength, Func<PersistentCache> cacheCtor)
		{
			this.syncRoot = new object();
			this.maxEntryLength = maxEntryLength;
			this.cacheCtor = cacheCtor;
		}

		// Token: 0x17002A7E RID: 10878
		// (get) Token: 0x0600A68E RID: 42638 RVA: 0x002274D4 File Offset: 0x002256D4
		protected override PersistentCache Cache
		{
			get
			{
				object obj = this.syncRoot;
				PersistentCache persistentCache;
				lock (obj)
				{
					if (this.cache == null)
					{
						this.cache = this.cacheCtor();
					}
					persistentCache = this.cache;
				}
				return persistentCache;
			}
		}

		// Token: 0x17002A7F RID: 10879
		// (get) Token: 0x0600A68F RID: 42639 RVA: 0x00227530 File Offset: 0x00225730
		// (set) Token: 0x0600A690 RID: 42640 RVA: 0x002242EA File Offset: 0x002224EA
		public override DateTime Staleness
		{
			get
			{
				object obj = this.syncRoot;
				DateTime dateTime;
				lock (obj)
				{
					if (this.cache != null)
					{
						dateTime = this.cache.Staleness;
					}
					else
					{
						dateTime = DateTime.UtcNow;
					}
				}
				return dateTime;
			}
			set
			{
				this.Cache.Staleness = value;
			}
		}

		// Token: 0x17002A80 RID: 10880
		// (get) Token: 0x0600A691 RID: 42641 RVA: 0x00227588 File Offset: 0x00225788
		public override long MaxEntryLength
		{
			get
			{
				return this.maxEntryLength;
			}
		}

		// Token: 0x0600A692 RID: 42642 RVA: 0x00227590 File Offset: 0x00225790
		public override void Dispose()
		{
			PersistentCache persistentCache = null;
			object obj = this.syncRoot;
			lock (obj)
			{
				persistentCache = this.cache;
				this.cache = null;
			}
			if (persistentCache != null)
			{
				persistentCache.Dispose();
			}
		}

		// Token: 0x040056AC RID: 22188
		private readonly object syncRoot;

		// Token: 0x040056AD RID: 22189
		private readonly long maxEntryLength;

		// Token: 0x040056AE RID: 22190
		private Func<PersistentCache> cacheCtor;

		// Token: 0x040056AF RID: 22191
		private PersistentCache cache;
	}
}
