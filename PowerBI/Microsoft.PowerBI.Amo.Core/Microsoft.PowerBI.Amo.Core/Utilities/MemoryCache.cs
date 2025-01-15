using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x0200013E RID: 318
	internal sealed class MemoryCache : MemoryCacheBase, IDisposable
	{
		// Token: 0x060010E8 RID: 4328 RVA: 0x0003ACBE File Offset: 0x00038EBE
		public MemoryCache(MemoryCacheRetentionPolicy retentionPolicy)
			: base(retentionPolicy)
		{
			this.cache = new Dictionary<string, KeyValuePair<object, DateTime>>();
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0003ACD2 File Offset: 0x00038ED2
		public MemoryCache(MemoryCacheRetentionPolicy retentionPolicy, IEqualityComparer<string> comparer)
			: base(retentionPolicy)
		{
			this.cache = new Dictionary<string, KeyValuePair<object, DateTime>>(comparer);
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x0003ACE7 File Offset: 0x00038EE7
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x0003ACEF File Offset: 0x00038EEF
		public bool IsDisposed { get; private set; }

		// Token: 0x060010EC RID: 4332 RVA: 0x0003ACF8 File Offset: 0x00038EF8
		protected override int GetItemCount()
		{
			return this.cache.Count;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0003AD05 File Offset: 0x00038F05
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

		// Token: 0x060010EE RID: 4334 RVA: 0x0003AD24 File Offset: 0x00038F24
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

		// Token: 0x060010EF RID: 4335 RVA: 0x0003AD9C File Offset: 0x00038F9C
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

		// Token: 0x060010F0 RID: 4336 RVA: 0x0003AE00 File Offset: 0x00039000
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

		// Token: 0x060010F1 RID: 4337 RVA: 0x0003AEC8 File Offset: 0x000390C8
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

		// Token: 0x060010F2 RID: 4338 RVA: 0x0003AF58 File Offset: 0x00039158
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

		// Token: 0x060010F3 RID: 4339 RVA: 0x0003B01C File Offset: 0x0003921C
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

		// Token: 0x060010F4 RID: 4340 RVA: 0x0003B0C4 File Offset: 0x000392C4
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

		// Token: 0x060010F5 RID: 4341 RVA: 0x0003B118 File Offset: 0x00039318
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

		// Token: 0x060010F6 RID: 4342 RVA: 0x0003B17C File Offset: 0x0003937C
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

		// Token: 0x060010F7 RID: 4343 RVA: 0x0003B1C0 File Offset: 0x000393C0
		private IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfoImpl()
		{
			List<KeyValuePair<string, DateTime>> list = new List<KeyValuePair<string, DateTime>>(this.cache.Count);
			foreach (KeyValuePair<string, KeyValuePair<object, DateTime>> keyValuePair in this.cache)
			{
				list.Add(new KeyValuePair<string, DateTime>(keyValuePair.Key, keyValuePair.Value.Value));
			}
			return list;
		}

		// Token: 0x04000ACB RID: 2763
		private Dictionary<string, KeyValuePair<object, DateTime>> cache;

		// Token: 0x04000ACC RID: 2764
		private Timer evictionTimer;
	}
}
