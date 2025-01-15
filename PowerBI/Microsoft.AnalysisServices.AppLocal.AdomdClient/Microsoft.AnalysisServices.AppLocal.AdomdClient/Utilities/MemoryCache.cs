using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000149 RID: 329
	internal sealed class MemoryCache : MemoryCacheBase, IDisposable
	{
		// Token: 0x0600105A RID: 4186 RVA: 0x000383BA File Offset: 0x000365BA
		public MemoryCache(MemoryCacheRetentionPolicy retentionPolicy)
			: base(retentionPolicy)
		{
			this.cache = new Dictionary<string, KeyValuePair<object, DateTime>>();
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x000383CE File Offset: 0x000365CE
		public MemoryCache(MemoryCacheRetentionPolicy retentionPolicy, IEqualityComparer<string> comparer)
			: base(retentionPolicy)
		{
			this.cache = new Dictionary<string, KeyValuePair<object, DateTime>>(comparer);
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x000383E3 File Offset: 0x000365E3
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x000383EB File Offset: 0x000365EB
		public bool IsDisposed { get; private set; }

		// Token: 0x0600105E RID: 4190 RVA: 0x000383F4 File Offset: 0x000365F4
		protected override int GetItemCount()
		{
			return this.cache.Count;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00038401 File Offset: 0x00036601
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

		// Token: 0x06001060 RID: 4192 RVA: 0x00038420 File Offset: 0x00036620
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

		// Token: 0x06001061 RID: 4193 RVA: 0x00038498 File Offset: 0x00036698
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

		// Token: 0x06001062 RID: 4194 RVA: 0x000384FC File Offset: 0x000366FC
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

		// Token: 0x06001063 RID: 4195 RVA: 0x000385C4 File Offset: 0x000367C4
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

		// Token: 0x06001064 RID: 4196 RVA: 0x00038654 File Offset: 0x00036854
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

		// Token: 0x06001065 RID: 4197 RVA: 0x00038718 File Offset: 0x00036918
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

		// Token: 0x06001066 RID: 4198 RVA: 0x000387C0 File Offset: 0x000369C0
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

		// Token: 0x06001067 RID: 4199 RVA: 0x00038814 File Offset: 0x00036A14
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

		// Token: 0x06001068 RID: 4200 RVA: 0x00038878 File Offset: 0x00036A78
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

		// Token: 0x06001069 RID: 4201 RVA: 0x000388BC File Offset: 0x00036ABC
		private IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfoImpl()
		{
			List<KeyValuePair<string, DateTime>> list = new List<KeyValuePair<string, DateTime>>(this.cache.Count);
			foreach (KeyValuePair<string, KeyValuePair<object, DateTime>> keyValuePair in this.cache)
			{
				list.Add(new KeyValuePair<string, DateTime>(keyValuePair.Key, keyValuePair.Value.Value));
			}
			return list;
		}

		// Token: 0x04000B12 RID: 2834
		private Dictionary<string, KeyValuePair<object, DateTime>> cache;

		// Token: 0x04000B13 RID: 2835
		private Timer evictionTimer;
	}
}
