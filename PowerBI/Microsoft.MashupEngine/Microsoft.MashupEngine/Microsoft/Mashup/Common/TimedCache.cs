using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C2F RID: 7215
	public sealed class TimedCache<K, V>
	{
		// Token: 0x0600B417 RID: 46103 RVA: 0x00248E9C File Offset: 0x0024709C
		public TimedCache(TimeSpan cacheEntryRefreshPeriod)
		{
			this.cacheEntryRefreshPeriod = cacheEntryRefreshPeriod;
			this.cache = new Dictionary<K, TimedCache<K, V>.CachedEntry<V>>();
		}

		// Token: 0x0600B418 RID: 46104 RVA: 0x00248EB8 File Offset: 0x002470B8
		public bool TryGetValue(K key, out V value)
		{
			Dictionary<K, TimedCache<K, V>.CachedEntry<V>> dictionary = this.cache;
			lock (dictionary)
			{
				TimedCache<K, V>.CachedEntry<V> cachedEntry;
				if (this.cache.TryGetValue(key, out cachedEntry))
				{
					if (cachedEntry.CachedAt >= DateTime.UtcNow - this.cacheEntryRefreshPeriod)
					{
						value = cachedEntry.Value;
						return true;
					}
					this.cache.Remove(key);
				}
			}
			value = default(V);
			return false;
		}

		// Token: 0x0600B419 RID: 46105 RVA: 0x00248F48 File Offset: 0x00247148
		public void AddOrUpdate(K key, V value)
		{
			TimedCache<K, V>.CachedEntry<V> cachedEntry = new TimedCache<K, V>.CachedEntry<V>(value, DateTime.UtcNow);
			Dictionary<K, TimedCache<K, V>.CachedEntry<V>> dictionary = this.cache;
			lock (dictionary)
			{
				this.cache[key] = cachedEntry;
			}
		}

		// Token: 0x0600B41A RID: 46106 RVA: 0x00248F9C File Offset: 0x0024719C
		public IEnumerable<KeyValuePair<K, V>> GetAll()
		{
			Dictionary<K, TimedCache<K, V>.CachedEntry<V>> dictionary = this.cache;
			IEnumerable<KeyValuePair<K, V>> enumerable;
			lock (dictionary)
			{
				List<KeyValuePair<K, V>> list = new List<KeyValuePair<K, V>>();
				foreach (KeyValuePair<K, TimedCache<K, V>.CachedEntry<V>> keyValuePair in this.cache.ToList<KeyValuePair<K, TimedCache<K, V>.CachedEntry<V>>>())
				{
					if (keyValuePair.Value.CachedAt >= DateTime.UtcNow - this.cacheEntryRefreshPeriod)
					{
						list.Add(new KeyValuePair<K, V>(keyValuePair.Key, keyValuePair.Value.Value));
					}
					else
					{
						this.cache.Remove(keyValuePair.Key);
					}
				}
				enumerable = list;
			}
			return enumerable;
		}

		// Token: 0x0600B41B RID: 46107 RVA: 0x00249078 File Offset: 0x00247278
		public void Remove(K key)
		{
			Dictionary<K, TimedCache<K, V>.CachedEntry<V>> dictionary = this.cache;
			lock (dictionary)
			{
				this.cache.Remove(key);
			}
		}

		// Token: 0x0600B41C RID: 46108 RVA: 0x002490C0 File Offset: 0x002472C0
		public bool ContainsKey(K key)
		{
			Dictionary<K, TimedCache<K, V>.CachedEntry<V>> dictionary = this.cache;
			lock (dictionary)
			{
				TimedCache<K, V>.CachedEntry<V> cachedEntry;
				if (this.cache.TryGetValue(key, out cachedEntry))
				{
					if (cachedEntry.CachedAt >= DateTime.UtcNow - this.cacheEntryRefreshPeriod)
					{
						return true;
					}
					this.cache.Remove(key);
				}
			}
			return false;
		}

		// Token: 0x04005BAE RID: 23470
		private readonly Dictionary<K, TimedCache<K, V>.CachedEntry<V>> cache;

		// Token: 0x04005BAF RID: 23471
		private readonly TimeSpan cacheEntryRefreshPeriod;

		// Token: 0x02001C30 RID: 7216
		private struct CachedEntry<T>
		{
			// Token: 0x0600B41D RID: 46109 RVA: 0x0024913C File Offset: 0x0024733C
			public CachedEntry(T value, DateTime cachedAt)
			{
				this.Value = value;
				this.CachedAt = cachedAt;
			}

			// Token: 0x04005BB0 RID: 23472
			public readonly T Value;

			// Token: 0x04005BB1 RID: 23473
			public readonly DateTime CachedAt;
		}
	}
}
