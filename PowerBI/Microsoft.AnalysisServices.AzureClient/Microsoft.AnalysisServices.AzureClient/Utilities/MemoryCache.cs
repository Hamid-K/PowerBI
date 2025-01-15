using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x0200002D RID: 45
	internal sealed class MemoryCache : MemoryCacheBase, IDisposable
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00006E05 File Offset: 0x00005005
		public MemoryCache(MemoryCacheRetentionPolicy retentionPolicy)
			: base(retentionPolicy)
		{
			this.cache = new Dictionary<string, KeyValuePair<object, DateTime>>();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006E19 File Offset: 0x00005019
		public MemoryCache(MemoryCacheRetentionPolicy retentionPolicy, IEqualityComparer<string> comparer)
			: base(retentionPolicy)
		{
			this.cache = new Dictionary<string, KeyValuePair<object, DateTime>>(comparer);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00006E2E File Offset: 0x0000502E
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00006E36 File Offset: 0x00005036
		public bool IsDisposed { get; private set; }

		// Token: 0x0600015E RID: 350 RVA: 0x00006E3F File Offset: 0x0000503F
		protected override int GetItemCount()
		{
			return this.cache.Count;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006E4C File Offset: 0x0000504C
		public void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Clear();
			GC.SuppressFinalize(this);
			this.IsDisposed = true;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006E6C File Offset: 0x0000506C
		protected override bool TryLocateInCache(string key, out object item, out DateTime utcExpiration)
		{
			Dictionary<string, KeyValuePair<object, DateTime>> dictionary = this.cache;
			lock (dictionary)
			{
				KeyValuePair<object, DateTime> keyValuePair;
				if (!this.cache.TryGetValue(key, out keyValuePair))
				{
					item = null;
					utcExpiration = DateTime.MinValue;
					return false;
				}
				item = keyValuePair.Key;
				utcExpiration = keyValuePair.Value;
			}
			return true;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006EE4 File Offset: 0x000050E4
		protected override void UpdateExpirationTime(string key, DateTime utcExpiration)
		{
			Dictionary<string, KeyValuePair<object, DateTime>> dictionary = this.cache;
			lock (dictionary)
			{
				KeyValuePair<object, DateTime> keyValuePair;
				if (this.cache.TryGetValue(key, out keyValuePair))
				{
					this.cache[key] = new KeyValuePair<object, DateTime>(keyValuePair.Key, utcExpiration);
				}
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006F48 File Offset: 0x00005148
		protected override void InsertToCache(string key, object item, DateTime utcExpiration, int capacityLimit, out bool wasCacheEmpty, out object prevItem)
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			Dictionary<string, KeyValuePair<object, DateTime>> dictionary = this.cache;
			lock (dictionary)
			{
				wasCacheEmpty = this.cache.Count == 0;
				if (capacityLimit > 0 && this.cache.Count >= capacityLimit)
				{
					base.ReduceCacheSize(this.cache.Count - capacityLimit + 1, this.GetItemsExpirationInfoImpl());
				}
				KeyValuePair<object, DateTime> keyValuePair;
				if (this.cache.TryGetValue(key, out keyValuePair))
				{
					prevItem = keyValuePair.Key;
				}
				else
				{
					prevItem = null;
				}
				this.cache[key] = new KeyValuePair<object, DateTime>(item, utcExpiration);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007010 File Offset: 0x00005210
		protected override bool RemoveFromCache(string key, bool returnItem, out bool isCacheEmpty, out object item)
		{
			Dictionary<string, KeyValuePair<object, DateTime>> dictionary = this.cache;
			bool flag2;
			lock (dictionary)
			{
				KeyValuePair<object, DateTime> keyValuePair;
				item = ((returnItem && this.cache.TryGetValue(key, out keyValuePair)) ? keyValuePair.Key : null);
				flag2 = (!returnItem || item != null) && this.cache.Remove(key);
				if (returnItem && !flag2)
				{
					item = null;
				}
				isCacheEmpty = this.cache.Count == 0;
			}
			return flag2;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000070A0 File Offset: 0x000052A0
		protected override bool EvictFromCache(string key, bool lockCache, DateTime? utcExpiration, bool returnItem, out bool isCacheEmpty, out object item)
		{
			if (lockCache)
			{
				Monitor.Enter(this.cache);
			}
			bool flag;
			try
			{
				KeyValuePair<object, DateTime> keyValuePair;
				if (!this.cache.TryGetValue(key, out keyValuePair))
				{
					isCacheEmpty = this.cache.Count == 0;
					item = null;
					flag = false;
				}
				else if ((utcExpiration != null && utcExpiration.Value != keyValuePair.Value) || !this.cache.Remove(key))
				{
					isCacheEmpty = false;
					item = null;
					flag = false;
				}
				else
				{
					isCacheEmpty = this.cache.Count == 0;
					item = (returnItem ? keyValuePair.Key : null);
					flag = true;
				}
			}
			finally
			{
				if (lockCache)
				{
					Monitor.Exit(this.cache);
				}
			}
			return flag;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007164 File Offset: 0x00005364
		protected override IEnumerable ClearCache()
		{
			List<object> list = new List<object>(this.cache.Count);
			Dictionary<string, KeyValuePair<object, DateTime>> dictionary = this.cache;
			lock (dictionary)
			{
				foreach (KeyValuePair<object, DateTime> keyValuePair in this.cache.Values)
				{
					list.Add(keyValuePair.Key);
				}
				this.cache.Clear();
			}
			return list;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000720C File Offset: 0x0000540C
		protected override void SetEvictionTimer(Timer timer)
		{
			Dictionary<string, KeyValuePair<object, DateTime>> dictionary = this.cache;
			lock (dictionary)
			{
				if (this.evictionTimer != null)
				{
					this.evictionTimer.Dispose();
				}
				this.evictionTimer = timer;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007260 File Offset: 0x00005460
		protected override void ResetEvictionTimer()
		{
			Dictionary<string, KeyValuePair<object, DateTime>> dictionary = this.cache;
			lock (dictionary)
			{
				if (this.cache.Count == 0 && this.evictionTimer != null)
				{
					this.evictionTimer.Dispose();
					this.evictionTimer = null;
				}
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000072C4 File Offset: 0x000054C4
		protected override IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfo()
		{
			Dictionary<string, KeyValuePair<object, DateTime>> dictionary = this.cache;
			IList<KeyValuePair<string, DateTime>> itemsExpirationInfoImpl;
			lock (dictionary)
			{
				itemsExpirationInfoImpl = this.GetItemsExpirationInfoImpl();
			}
			return itemsExpirationInfoImpl;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007308 File Offset: 0x00005508
		private IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfoImpl()
		{
			List<KeyValuePair<string, DateTime>> list = new List<KeyValuePair<string, DateTime>>(this.cache.Count);
			foreach (KeyValuePair<string, KeyValuePair<object, DateTime>> keyValuePair in this.cache)
			{
				list.Add(new KeyValuePair<string, DateTime>(keyValuePair.Key, keyValuePair.Value.Value));
			}
			return list;
		}

		// Token: 0x040000D0 RID: 208
		private Dictionary<string, KeyValuePair<object, DateTime>> cache;

		// Token: 0x040000D1 RID: 209
		private Timer evictionTimer;
	}
}
