using System;
using System.Collections.Specialized;
using System.Runtime.Caching;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000062 RID: 98
	public sealed class MemoryCache<T> : IDisposable where T : class
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x00009E0B File Offset: 0x0000800B
		public MemoryCache(string name, CacheItemPolicy defaultPolicy = null, NameValueCollection config = null)
		{
			Contract.CheckNonEmpty(name, "name");
			this._cache = new MemoryCache(name, config);
			this._defaultCacheItemPolicy = defaultPolicy ?? new CacheItemPolicy();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00009E3C File Offset: 0x0000803C
		public MemoryCache(string name, CacheItemPolicy defaultPolicy = null, int? cacheMemoryLimitMegabytes = null, int? physicalMemoryLimitPercentage = null, int? pollingInterval = null)
		{
			Contract.CheckNonEmpty(name, "name");
			NameValueCollection nameValueCollection = new NameValueCollection();
			if (cacheMemoryLimitMegabytes != null)
			{
				nameValueCollection.Add("cacheMemoryLimitMegabytes", cacheMemoryLimitMegabytes.ToString());
			}
			if (physicalMemoryLimitPercentage != null)
			{
				nameValueCollection.Add("physicalMemoryLimitPercentage", physicalMemoryLimitPercentage.ToString());
			}
			if (pollingInterval != null)
			{
				nameValueCollection.Add("pollingInterval", pollingInterval.ToString());
			}
			this._cache = new MemoryCache(name, nameValueCollection);
			this._defaultCacheItemPolicy = defaultPolicy ?? new CacheItemPolicy();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00009EE0 File Offset: 0x000080E0
		public void Dispose()
		{
			MemoryCache cache = this._cache;
			if (cache == null)
			{
				return;
			}
			cache.Dispose();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00009EF2 File Offset: 0x000080F2
		public T AddOrGetExisting(string key, T value, CacheItemPolicy policy = null, string regionName = null)
		{
			Contract.CheckNonEmpty(key, "key");
			Contract.CheckValue<T>(value, "value");
			return (T)((object)this._cache.AddOrGetExisting(key, value, policy ?? this._defaultCacheItemPolicy, regionName));
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00009F2E File Offset: 0x0000812E
		public T AddOrGetExisting(string key, T value, DateTimeOffset absoluteExpiration, string regionName = null)
		{
			Contract.CheckNonEmpty(key, "key");
			Contract.CheckValue<T>(value, "value");
			return (T)((object)this._cache.AddOrGetExisting(key, value, absoluteExpiration, regionName));
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00009F60 File Offset: 0x00008160
		public bool Contains(string key, string regionName = null)
		{
			Contract.CheckNonEmpty(key, "key");
			return this._cache.Contains(key, regionName);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00009F7A File Offset: 0x0000817A
		public T Get(string key, string regionName = null)
		{
			Contract.CheckNonEmpty(key, "key");
			return (T)((object)this._cache.Get(key, regionName));
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00009F99 File Offset: 0x00008199
		public long GetCount(string regionName = null)
		{
			return this._cache.GetCount(regionName);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00009FA7 File Offset: 0x000081A7
		public T Remove(string key, string regionName = null)
		{
			Contract.CheckNonEmpty(key, "key");
			return (T)((object)this._cache.Remove(key, regionName));
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00009FC6 File Offset: 0x000081C6
		public void Set(string key, T value, CacheItemPolicy policy = null, string regionName = null)
		{
			Contract.CheckNonEmpty(key, "key");
			Contract.CheckValue<T>(value, "value");
			this._cache.Set(key, value, policy ?? this._defaultCacheItemPolicy, regionName);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00009FFD File Offset: 0x000081FD
		public void Set(string key, object value, DateTimeOffset absoluteExpiration, string regionName = null)
		{
			Contract.CheckNonEmpty(key, "key");
			Contract.CheckValue<object>(value, "value");
			this._cache.Set(key, value, absoluteExpiration, regionName);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000A025 File Offset: 0x00008225
		public long Trim(int percent)
		{
			return this._cache.Trim(percent);
		}

		// Token: 0x040000CA RID: 202
		private readonly MemoryCache _cache;

		// Token: 0x040000CB RID: 203
		private readonly CacheItemPolicy _defaultCacheItemPolicy;
	}
}
