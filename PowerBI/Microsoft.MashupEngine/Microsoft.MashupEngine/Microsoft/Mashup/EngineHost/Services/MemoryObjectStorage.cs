using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A1A RID: 6682
	public sealed class MemoryObjectStorage : ObjectStorage
	{
		// Token: 0x0600A91C RID: 43292 RVA: 0x0022FB18 File Offset: 0x0022DD18
		public MemoryObjectStorage()
		{
			this.entries = new LruCache<string, MemoryObjectStorage.Entry>(() => false, null);
		}

		// Token: 0x17002AFC RID: 11004
		// (get) Token: 0x0600A91D RID: 43293 RVA: 0x0022FB4C File Offset: 0x0022DD4C
		public override CacheSize CacheSize
		{
			get
			{
				LruCache<string, MemoryObjectStorage.Entry> lruCache = this.entries;
				CacheSize cacheSize;
				lock (lruCache)
				{
					cacheSize = new CacheSize(this.entries.Count, this.currentSize);
				}
				return cacheSize;
			}
		}

		// Token: 0x17002AFD RID: 11005
		// (get) Token: 0x0600A91E RID: 43294 RVA: 0x0022FBA0 File Offset: 0x0022DDA0
		public override long CurrentVersion
		{
			get
			{
				LruCache<string, MemoryObjectStorage.Entry> lruCache = this.entries;
				long num;
				lock (lruCache)
				{
					num = this.currentVersion;
				}
				return num;
			}
		}

		// Token: 0x0600A91F RID: 43295 RVA: 0x0022FBE4 File Offset: 0x0022DDE4
		public override long IncrementVersion()
		{
			LruCache<string, MemoryObjectStorage.Entry> lruCache = this.entries;
			long num;
			lock (lruCache)
			{
				num = this.currentVersion + 1L;
				this.currentVersion = num;
				num = num;
			}
			return num;
		}

		// Token: 0x0600A920 RID: 43296 RVA: 0x0022FC34 File Offset: 0x0022DE34
		public override bool TryGetValue(string key, out object value, out DateTime createdAt, out long version)
		{
			LruCache<string, MemoryObjectStorage.Entry> lruCache = this.entries;
			lock (lruCache)
			{
				MemoryObjectStorage.Entry entry;
				if (this.entries.TryGetValue(key, out entry))
				{
					value = entry.value;
					createdAt = entry.createdAt;
					version = entry.version;
					return true;
				}
			}
			value = null;
			createdAt = default(DateTime);
			version = -1L;
			return false;
		}

		// Token: 0x0600A921 RID: 43297 RVA: 0x0022FCB4 File Offset: 0x0022DEB4
		public override void CommitValue(string key, object value, long size, DateTime createdAt, long version)
		{
			LruCache<string, MemoryObjectStorage.Entry> lruCache = this.entries;
			lock (lruCache)
			{
				MemoryObjectStorage.Entry entry;
				if (this.entries.TryGetValue(key, out entry))
				{
					this.entries.Remove(key);
					this.currentSize -= entry.size;
				}
				entry = new MemoryObjectStorage.Entry(value, size, createdAt, version);
				this.currentSize += entry.size;
				this.entries.Add(key, entry);
			}
		}

		// Token: 0x0600A922 RID: 43298 RVA: 0x0022FD4C File Offset: 0x0022DF4C
		public override void Purge(long maxSize)
		{
			LruCache<string, MemoryObjectStorage.Entry> lruCache = this.entries;
			lock (lruCache)
			{
				while (this.currentSize > maxSize)
				{
					KeyValuePair<string, MemoryObjectStorage.Entry>? oldest = this.entries.Oldest;
					if (oldest == null)
					{
						break;
					}
					this.entries.Remove(oldest.Value.Key);
					this.currentSize -= oldest.Value.Value.size;
				}
			}
		}

		// Token: 0x040057FA RID: 22522
		private readonly LruCache<string, MemoryObjectStorage.Entry> entries;

		// Token: 0x040057FB RID: 22523
		private long currentSize;

		// Token: 0x040057FC RID: 22524
		private long currentVersion;

		// Token: 0x02001A1B RID: 6683
		private class Entry
		{
			// Token: 0x0600A923 RID: 43299 RVA: 0x0022FDE4 File Offset: 0x0022DFE4
			public Entry(object value, long size, DateTime createdAt, long version)
			{
				this.value = value;
				this.size = size;
				this.createdAt = createdAt;
				this.version = version;
			}

			// Token: 0x040057FD RID: 22525
			public readonly object value;

			// Token: 0x040057FE RID: 22526
			public readonly long size;

			// Token: 0x040057FF RID: 22527
			public readonly DateTime createdAt;

			// Token: 0x04005800 RID: 22528
			public readonly long version;
		}
	}
}
