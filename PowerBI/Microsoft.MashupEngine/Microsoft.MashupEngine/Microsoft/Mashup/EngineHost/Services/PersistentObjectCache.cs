using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A41 RID: 6721
	public sealed class PersistentObjectCache : IPersistentObjectCache, IDisposable, ICacheClock
	{
		// Token: 0x0600A9FB RID: 43515 RVA: 0x00231F3C File Offset: 0x0023013C
		public PersistentObjectCache(IObjectCache objectCache, PersistentCache persistentCache)
		{
			this.objectCache = objectCache;
			this.persistentCache = persistentCache;
		}

		// Token: 0x17002B29 RID: 11049
		// (get) Token: 0x0600A9FC RID: 43516 RVA: 0x00231F52 File Offset: 0x00230152
		public DateTime Staleness
		{
			get
			{
				if (this.objectCache.Staleness <= this.persistentCache.Staleness)
				{
					return this.objectCache.Staleness;
				}
				return this.persistentCache.Staleness;
			}
		}

		// Token: 0x17002B2A RID: 11050
		// (get) Token: 0x0600A9FD RID: 43517 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public ICacheClock CacheClock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600A9FE RID: 43518 RVA: 0x00231F88 File Offset: 0x00230188
		public bool TryGetValue(string key, DateTime maxStaleness, CacheVersion minVersion, Func<Stream, object> deserializer, out object value)
		{
			CacheVersion cacheVersion = this.objectCache.CacheClock.Current;
			if (this.objectCache.TryGetValue(key, maxStaleness, MultiLevelCacheVersion.GetVersion1(minVersion), out value))
			{
				return true;
			}
			IStorage storage;
			if (this.persistentCache.TryGetStorage(key, maxStaleness, MultiLevelCacheVersion.GetVersion2(minVersion), out storage))
			{
				int num;
				using (storage)
				{
					using (Stream stream = storage.OpenStream(0))
					{
						if (stream.CanSeek)
						{
							num = (int)Math.Min(2147483647L, stream.Length);
							value = deserializer(stream);
						}
						else
						{
							Stream stream2 = CountingStream.New(stream);
							value = deserializer(stream2);
							num = (int)Math.Min(2147483647L, stream2.Length);
						}
					}
				}
				this.objectCache.CommitValue(key, cacheVersion, num, value);
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600A9FF RID: 43519 RVA: 0x00232084 File Offset: 0x00230284
		public bool TryGetValue(StructuredCacheKey key, DateTime maxStaleness, CacheVersion minVersion, Func<Stream, object> deserializer, out object value)
		{
			return (this.persistentCache.UserSpecific == null && this.TryGetValue(key.GetCacheKey(true), maxStaleness, minVersion, deserializer, out value)) || this.TryGetValue(key.GetCacheKey(this.persistentCache.UserSpecific.GetValueOrDefault()), maxStaleness, minVersion, deserializer, out value);
		}

		// Token: 0x0600AA00 RID: 43520 RVA: 0x002320E4 File Offset: 0x002302E4
		public void CommitValue(string key, CacheVersion maxVersion, Action<Stream, object> serializer, object value)
		{
			CacheVersion version = MultiLevelCacheVersion.GetVersion1(maxVersion);
			CacheVersion version2 = MultiLevelCacheVersion.GetVersion2(maxVersion);
			int num;
			using (IStorage storage = this.persistentCache.CreateStorage())
			{
				Stream stream = storage.CreateStream();
				try
				{
					Stream stream2 = CountingStream.New(stream);
					serializer(stream2, value);
					num = (int)Math.Min(2147483647L, stream2.Length);
					stream = storage.CommitStream(0, stream);
				}
				finally
				{
					stream.Dispose();
				}
				this.persistentCache.CommitStorage(key, version2, storage);
			}
			this.objectCache.CommitValue(key, version, num, value);
		}

		// Token: 0x0600AA01 RID: 43521 RVA: 0x00232198 File Offset: 0x00230398
		public void CommitValue(StructuredCacheKey key, CacheVersion maxVersion, Action<Stream, object> serializer, object value)
		{
			this.CommitValue(key.GetCacheKey(this.persistentCache.UserSpecific.GetValueOrDefault(true)), maxVersion, serializer, value);
		}

		// Token: 0x0600AA02 RID: 43522 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x17002B2B RID: 11051
		// (get) Token: 0x0600AA03 RID: 43523 RVA: 0x002321C9 File Offset: 0x002303C9
		public CacheVersion Current
		{
			get
			{
				return new MultiLevelCacheVersion(this.objectCache.CacheClock.Current, this.persistentCache.CacheClock.Current);
			}
		}

		// Token: 0x0600AA04 RID: 43524 RVA: 0x002321F0 File Offset: 0x002303F0
		public CacheVersion Increment()
		{
			return new MultiLevelCacheVersion(this.objectCache.CacheClock.Increment(), this.persistentCache.CacheClock.Increment());
		}

		// Token: 0x04005859 RID: 22617
		private readonly IObjectCache objectCache;

		// Token: 0x0400585A RID: 22618
		private readonly PersistentCache persistentCache;
	}
}
