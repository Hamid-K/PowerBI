using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x02000147 RID: 327
	internal sealed class SharedMemoryCache : MemoryCacheBase
	{
		// Token: 0x0600113F RID: 4415 RVA: 0x0003BD76 File Offset: 0x00039F76
		private SharedMemoryCache(string cacheName, SharedMemoryCache.GlobalNames names, IDictionary<string, object> cache, IDictionary<string, KeyValuePair<string, DateTime>> metadata, ISharedItemConverter converter, MemoryCacheRetentionPolicy retentionPolicy)
			: base(retentionPolicy)
		{
			this.CacheName = cacheName;
			this.names = names;
			this.cache = cache;
			this.metadata = metadata;
			this.converter = converter;
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0003BDA5 File Offset: 0x00039FA5
		public string CacheName { get; }

		// Token: 0x06001141 RID: 4417 RVA: 0x0003BDB0 File Offset: 0x00039FB0
		public static SharedMemoryCache Create(string cacheName, MemoryCacheRetentionPolicy retentionPolicy, IEqualityComparer<string> comparer = null, ISharedItemConverter converter = null)
		{
			SharedMemoryCache.GlobalNames globalNames = new SharedMemoryCache.GlobalNames(cacheName);
			IDictionary<string, object> dictionary;
			IDictionary<string, KeyValuePair<string, DateTime>> dictionary2;
			using (GlobalContext.CreateGlobalLockScope(globalNames.Lock))
			{
				object obj;
				if (!GlobalContext.TryGetGlobalObject(globalNames.Policy, out obj))
				{
					GlobalContext.SetGlobalObject(globalNames.Policy, retentionPolicy.ToGlobalObject());
					dictionary = ((comparer != null) ? new Dictionary<string, object>(comparer) : new Dictionary<string, object>());
					GlobalContext.SetGlobalObject(globalNames.Cache, dictionary);
					dictionary2 = ((comparer != null) ? new Dictionary<string, KeyValuePair<string, DateTime>>(comparer) : new Dictionary<string, KeyValuePair<string, DateTime>>());
					GlobalContext.SetGlobalObject(globalNames.Metadata, dictionary2);
				}
				else
				{
					retentionPolicy = MemoryCacheRetentionPolicy.FromGlobalObject(obj);
					dictionary = GlobalContext.GetGlobalObject<IDictionary<string, object>>(globalNames.Cache);
					dictionary2 = GlobalContext.GetGlobalObject<IDictionary<string, KeyValuePair<string, DateTime>>>(globalNames.Metadata);
				}
			}
			return new SharedMemoryCache(cacheName, globalNames, dictionary, dictionary2, converter, retentionPolicy);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0003BE78 File Offset: 0x0003A078
		public static SharedMemoryCache Create(string cacheName, MemoryCacheRetentionPolicy retentionPolicy, IEqualityComparer<string> comparer, PrepareItemForCaching prepareMethod, ConvertCachedItem convertMethod)
		{
			return SharedMemoryCache.Create(cacheName, retentionPolicy, comparer, new SharedItemConverter(prepareMethod, convertMethod));
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0003BE8A File Offset: 0x0003A08A
		protected override int GetItemCount()
		{
			return this.cache.Count;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0003BE98 File Offset: 0x0003A098
		protected override bool TryLocateInCache(string key, out object item, out DateTime utcExpiration)
		{
			KeyValuePair<string, DateTime> keyValuePair;
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				if (!this.cache.TryGetValue(key, out item) || !this.metadata.TryGetValue(key, out keyValuePair))
				{
					utcExpiration = DateTime.MinValue;
					return false;
				}
			}
			if (this.converter != null && !string.IsNullOrEmpty(keyValuePair.Key))
			{
				item = this.converter.ConvertCachedItem(this.CacheName, item, keyValuePair.Key);
			}
			utcExpiration = keyValuePair.Value;
			return true;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0003BF44 File Offset: 0x0003A144
		protected override void UpdateExpirationTime(string key, DateTime utcExpiration)
		{
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				KeyValuePair<string, DateTime> keyValuePair;
				if (this.metadata.TryGetValue(key, out keyValuePair))
				{
					this.metadata[key] = new KeyValuePair<string, DateTime>(keyValuePair.Key, utcExpiration);
				}
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0003BFA8 File Offset: 0x0003A1A8
		protected override void InsertToCache(string key, object item, DateTime utcExpiration, int capacityLimit, out bool wasCacheEmpty, out object prevItem)
		{
			string text;
			if (this.converter != null)
			{
				this.converter.PrepareItemForCaching(this.CacheName, ref item, out text);
			}
			else
			{
				text = null;
			}
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				wasCacheEmpty = this.cache.Count == 0;
				if (capacityLimit > 0 && this.cache.Count >= capacityLimit)
				{
					base.ReduceCacheSize(this.cache.Count - capacityLimit + 1, this.GetItemsExpirationInfoImpl());
				}
				if (this.cache.TryGetValue(key, out prevItem))
				{
					KeyValuePair<string, DateTime> keyValuePair;
					if (this.converter != null && this.metadata.TryGetValue(key, out keyValuePair))
					{
						text = keyValuePair.Key;
					}
					else
					{
						text = null;
					}
				}
				this.cache[key] = item;
				this.metadata[key] = new KeyValuePair<string, DateTime>(text, utcExpiration);
			}
			if (prevItem != null && !string.IsNullOrEmpty(text))
			{
				prevItem = this.converter.ConvertCachedItem(this.CacheName, prevItem, text);
			}
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0003C0C0 File Offset: 0x0003A2C0
		protected override bool RemoveFromCache(string key, bool returnItem, out bool isCacheEmpty, out object item)
		{
			item = null;
			string text = null;
			bool flag;
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				if (returnItem)
				{
					if (!this.cache.TryGetValue(key, out item))
					{
						isCacheEmpty = this.cache.Count == 0;
						return false;
					}
					KeyValuePair<string, DateTime> keyValuePair;
					if (this.metadata.TryGetValue(key, out keyValuePair))
					{
						text = keyValuePair.Key;
					}
				}
				flag = this.cache.Remove(key);
				if (flag)
				{
					this.metadata.Remove(key);
				}
				isCacheEmpty = this.cache.Count == 0;
			}
			if (returnItem && this.converter != null && !string.IsNullOrEmpty(text))
			{
				item = this.converter.ConvertCachedItem(this.CacheName, item, text);
			}
			return flag;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0003C19C File Offset: 0x0003A39C
		protected override bool EvictFromCache(string key, bool lockCache, DateTime? utcExpiration, bool returnItem, out bool isCacheEmpty, out object item)
		{
			object obj = (lockCache ? GlobalContext.GetGlobalLock(this.names.Lock) : null);
			item = null;
			string text = null;
			if (lockCache)
			{
				Monitor.Enter(obj);
			}
			try
			{
				KeyValuePair<string, DateTime> keyValuePair;
				if (!this.metadata.TryGetValue(key, out keyValuePair))
				{
					isCacheEmpty = this.cache.Count == 0;
					return false;
				}
				if ((utcExpiration != null && utcExpiration.Value != keyValuePair.Value) || (returnItem && !this.cache.TryGetValue(key, out item)) || !this.cache.Remove(key))
				{
					isCacheEmpty = false;
					return false;
				}
				this.metadata.Remove(key);
				isCacheEmpty = this.cache.Count == 0;
				text = keyValuePair.Key;
			}
			finally
			{
				if (lockCache)
				{
					Monitor.Exit(obj);
				}
			}
			if (returnItem && this.converter != null && !string.IsNullOrEmpty(text))
			{
				item = this.converter.ConvertCachedItem(this.CacheName, item, text);
			}
			return true;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0003C2B4 File Offset: 0x0003A4B4
		protected override IEnumerable ClearCache()
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>(this.cache.Count);
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				if (this.converter != null)
				{
					using (IEnumerator<KeyValuePair<string, object>> enumerator = this.cache.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, object> keyValuePair = enumerator.Current;
							KeyValuePair<string, DateTime> keyValuePair2;
							string text = (this.metadata.TryGetValue(keyValuePair.Key, out keyValuePair2) ? keyValuePair2.Key : null);
							list.Add(new KeyValuePair<string, object>(text, keyValuePair.Value));
						}
						goto IL_00CB;
					}
				}
				foreach (object obj in this.cache.Values)
				{
					list.Add(new KeyValuePair<string, object>(null, obj));
				}
				IL_00CB:
				this.cache.Clear();
				this.metadata.Clear();
			}
			if (this.converter != null)
			{
				return list.Select(delegate(KeyValuePair<string, object> kvp)
				{
					if (!string.IsNullOrEmpty(kvp.Key))
					{
						return this.converter.ConvertCachedItem(this.CacheName, kvp.Value, kvp.Key);
					}
					return kvp.Value;
				});
			}
			return list.Select((KeyValuePair<string, object> kvp) => kvp.Value);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0003C418 File Offset: 0x0003A618
		protected override void SetEvictionTimer(Timer timer)
		{
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				Timer timer2;
				if (GlobalContext.TryGetGlobalObject<Timer>(this.names.Timer, out timer2))
				{
					timer2.Dispose();
				}
				GlobalContext.SetGlobalObject(this.names.Timer, timer);
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0003C480 File Offset: 0x0003A680
		protected override void ResetEvictionTimer()
		{
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				Timer timer;
				if (this.cache.Count == 0 && GlobalContext.TryGetGlobalObject<Timer>(this.names.Timer, out timer))
				{
					timer.Dispose();
					GlobalContext.SetGlobalObject(this.names.Timer, null);
				}
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0003C4F4 File Offset: 0x0003A6F4
		protected override IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfo()
		{
			IList<KeyValuePair<string, DateTime>> itemsExpirationInfoImpl;
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				itemsExpirationInfoImpl = this.GetItemsExpirationInfoImpl();
			}
			return itemsExpirationInfoImpl;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0003C538 File Offset: 0x0003A738
		private IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfoImpl()
		{
			List<KeyValuePair<string, DateTime>> list = new List<KeyValuePair<string, DateTime>>(this.metadata.Count);
			foreach (KeyValuePair<string, KeyValuePair<string, DateTime>> keyValuePair in this.metadata)
			{
				list.Add(new KeyValuePair<string, DateTime>(keyValuePair.Key, keyValuePair.Value.Value));
			}
			return list;
		}

		// Token: 0x04000AD9 RID: 2777
		private const string GlobalObjectNameTemplate_Cache = "MS_AS_SHARED_CACHE_{0}_CACHE";

		// Token: 0x04000ADA RID: 2778
		private const string GlobalObjectNameTemplate_Metadata = "MS_AS_SHARED_CACHE_{0}_METADATA";

		// Token: 0x04000ADB RID: 2779
		private const string GlobalObjectNameTemplate_Policy = "MS_AS_SHARED_CACHE_{0}_POLICY";

		// Token: 0x04000ADC RID: 2780
		private const string GlobalObjectNameTemplate_Timer = "MS_AS_SHARED_CACHE_{0}_TIMER";

		// Token: 0x04000ADD RID: 2781
		private const string GlobalObjectNameTemplate_Lock = "MS_AS_SHARED_CACHE_{0}_LOCK";

		// Token: 0x04000ADE RID: 2782
		private SharedMemoryCache.GlobalNames names;

		// Token: 0x04000ADF RID: 2783
		private IDictionary<string, object> cache;

		// Token: 0x04000AE0 RID: 2784
		private IDictionary<string, KeyValuePair<string, DateTime>> metadata;

		// Token: 0x04000AE1 RID: 2785
		private ISharedItemConverter converter;

		// Token: 0x020001DF RID: 479
		private struct GlobalNames
		{
			// Token: 0x0600140F RID: 5135 RVA: 0x00045178 File Offset: 0x00043378
			public GlobalNames(string cacheName)
			{
				this.Cache = string.Format("MS_AS_SHARED_CACHE_{0}_CACHE", cacheName);
				this.Metadata = string.Format("MS_AS_SHARED_CACHE_{0}_METADATA", cacheName);
				this.Policy = string.Format("MS_AS_SHARED_CACHE_{0}_POLICY", cacheName);
				this.Timer = string.Format("MS_AS_SHARED_CACHE_{0}_TIMER", cacheName);
				this.Lock = string.Format("MS_AS_SHARED_CACHE_{0}_LOCK", cacheName);
			}

			// Token: 0x040011B7 RID: 4535
			public readonly string Cache;

			// Token: 0x040011B8 RID: 4536
			public readonly string Metadata;

			// Token: 0x040011B9 RID: 4537
			public readonly string Policy;

			// Token: 0x040011BA RID: 4538
			public readonly string Timer;

			// Token: 0x040011BB RID: 4539
			public readonly string Lock;
		}
	}
}
