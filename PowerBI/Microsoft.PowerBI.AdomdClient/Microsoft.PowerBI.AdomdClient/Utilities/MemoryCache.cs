using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000149 RID: 329
	internal sealed class MemoryCache : MemoryCacheBase, IDisposable
	{
		// Token: 0x0600104D RID: 4173 RVA: 0x0003808A File Offset: 0x0003628A
		public MemoryCache(MemoryCacheRetentionPolicy retentionPolicy)
			: base(retentionPolicy)
		{
			this.cache = new Dictionary<string, KeyValuePair<object, DateTime>>();
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0003809E File Offset: 0x0003629E
		public MemoryCache(MemoryCacheRetentionPolicy retentionPolicy, IEqualityComparer<string> comparer)
			: base(retentionPolicy)
		{
			this.cache = new Dictionary<string, KeyValuePair<object, DateTime>>(comparer);
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000380B3 File Offset: 0x000362B3
		// (set) Token: 0x06001050 RID: 4176 RVA: 0x000380BB File Offset: 0x000362BB
		public bool IsDisposed { get; private set; }

		// Token: 0x06001051 RID: 4177 RVA: 0x000380C4 File Offset: 0x000362C4
		protected override int GetItemCount()
		{
			return this.cache.Count;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x000380D1 File Offset: 0x000362D1
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

		// Token: 0x06001053 RID: 4179 RVA: 0x000380F0 File Offset: 0x000362F0
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

		// Token: 0x06001054 RID: 4180 RVA: 0x00038168 File Offset: 0x00036368
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

		// Token: 0x06001055 RID: 4181 RVA: 0x000381CC File Offset: 0x000363CC
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

		// Token: 0x06001056 RID: 4182 RVA: 0x00038294 File Offset: 0x00036494
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

		// Token: 0x06001057 RID: 4183 RVA: 0x00038324 File Offset: 0x00036524
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

		// Token: 0x06001058 RID: 4184 RVA: 0x000383E8 File Offset: 0x000365E8
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

		// Token: 0x06001059 RID: 4185 RVA: 0x00038490 File Offset: 0x00036690
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

		// Token: 0x0600105A RID: 4186 RVA: 0x000384E4 File Offset: 0x000366E4
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

		// Token: 0x0600105B RID: 4187 RVA: 0x00038548 File Offset: 0x00036748
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

		// Token: 0x0600105C RID: 4188 RVA: 0x0003858C File Offset: 0x0003678C
		private IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfoImpl()
		{
			List<KeyValuePair<string, DateTime>> list = new List<KeyValuePair<string, DateTime>>(this.cache.Count);
			foreach (KeyValuePair<string, KeyValuePair<object, DateTime>> keyValuePair in this.cache)
			{
				list.Add(new KeyValuePair<string, DateTime>(keyValuePair.Key, keyValuePair.Value.Value));
			}
			return list;
		}

		// Token: 0x04000B05 RID: 2821
		private Dictionary<string, KeyValuePair<object, DateTime>> cache;

		// Token: 0x04000B06 RID: 2822
		private Timer evictionTimer;
	}
}
