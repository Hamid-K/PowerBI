using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000152 RID: 338
	internal sealed class SharedMemoryCache : MemoryCacheBase
	{
		// Token: 0x060010B1 RID: 4273 RVA: 0x00039472 File Offset: 0x00037672
		private SharedMemoryCache(string cacheName, SharedMemoryCache.GlobalNames names, IDictionary<string, object> cache, IDictionary<string, KeyValuePair<string, DateTime>> metadata, ISharedItemConverter converter, MemoryCacheRetentionPolicy retentionPolicy)
			: base(retentionPolicy)
		{
			this.CacheName = cacheName;
			this.names = names;
			this.cache = cache;
			this.metadata = metadata;
			this.converter = converter;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x000394A1 File Offset: 0x000376A1
		public string CacheName { get; }

		// Token: 0x060010B3 RID: 4275 RVA: 0x000394AC File Offset: 0x000376AC
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

		// Token: 0x060010B4 RID: 4276 RVA: 0x00039574 File Offset: 0x00037774
		public static SharedMemoryCache Create(string cacheName, MemoryCacheRetentionPolicy retentionPolicy, IEqualityComparer<string> comparer, PrepareItemForCaching prepareMethod, ConvertCachedItem convertMethod)
		{
			return SharedMemoryCache.Create(cacheName, retentionPolicy, comparer, new SharedItemConverter(prepareMethod, convertMethod));
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00039586 File Offset: 0x00037786
		protected override int GetItemCount()
		{
			return this.cache.Count;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00039594 File Offset: 0x00037794
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

		// Token: 0x060010B7 RID: 4279 RVA: 0x00039640 File Offset: 0x00037840
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

		// Token: 0x060010B8 RID: 4280 RVA: 0x000396A4 File Offset: 0x000378A4
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

		// Token: 0x060010B9 RID: 4281 RVA: 0x000397BC File Offset: 0x000379BC
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

		// Token: 0x060010BA RID: 4282 RVA: 0x00039898 File Offset: 0x00037A98
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

		// Token: 0x060010BB RID: 4283 RVA: 0x000399B0 File Offset: 0x00037BB0
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

		// Token: 0x060010BC RID: 4284 RVA: 0x00039B14 File Offset: 0x00037D14
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

		// Token: 0x060010BD RID: 4285 RVA: 0x00039B7C File Offset: 0x00037D7C
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

		// Token: 0x060010BE RID: 4286 RVA: 0x00039BF0 File Offset: 0x00037DF0
		protected override IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfo()
		{
			IList<KeyValuePair<string, DateTime>> itemsExpirationInfoImpl;
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				itemsExpirationInfoImpl = this.GetItemsExpirationInfoImpl();
			}
			return itemsExpirationInfoImpl;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00039C34 File Offset: 0x00037E34
		private IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfoImpl()
		{
			List<KeyValuePair<string, DateTime>> list = new List<KeyValuePair<string, DateTime>>(this.metadata.Count);
			foreach (KeyValuePair<string, KeyValuePair<string, DateTime>> keyValuePair in this.metadata)
			{
				list.Add(new KeyValuePair<string, DateTime>(keyValuePair.Key, keyValuePair.Value.Value));
			}
			return list;
		}

		// Token: 0x04000B20 RID: 2848
		private const string GlobalObjectNameTemplate_Cache = "MS_AS_SHARED_CACHE_{0}_CACHE";

		// Token: 0x04000B21 RID: 2849
		private const string GlobalObjectNameTemplate_Metadata = "MS_AS_SHARED_CACHE_{0}_METADATA";

		// Token: 0x04000B22 RID: 2850
		private const string GlobalObjectNameTemplate_Policy = "MS_AS_SHARED_CACHE_{0}_POLICY";

		// Token: 0x04000B23 RID: 2851
		private const string GlobalObjectNameTemplate_Timer = "MS_AS_SHARED_CACHE_{0}_TIMER";

		// Token: 0x04000B24 RID: 2852
		private const string GlobalObjectNameTemplate_Lock = "MS_AS_SHARED_CACHE_{0}_LOCK";

		// Token: 0x04000B25 RID: 2853
		private SharedMemoryCache.GlobalNames names;

		// Token: 0x04000B26 RID: 2854
		private IDictionary<string, object> cache;

		// Token: 0x04000B27 RID: 2855
		private IDictionary<string, KeyValuePair<string, DateTime>> metadata;

		// Token: 0x04000B28 RID: 2856
		private ISharedItemConverter converter;

		// Token: 0x02000202 RID: 514
		private struct GlobalNames
		{
			// Token: 0x060014B4 RID: 5300 RVA: 0x00046F58 File Offset: 0x00045158
			public GlobalNames(string cacheName)
			{
				this.Cache = string.Format("MS_AS_SHARED_CACHE_{0}_CACHE", cacheName);
				this.Metadata = string.Format("MS_AS_SHARED_CACHE_{0}_METADATA", cacheName);
				this.Policy = string.Format("MS_AS_SHARED_CACHE_{0}_POLICY", cacheName);
				this.Timer = string.Format("MS_AS_SHARED_CACHE_{0}_TIMER", cacheName);
				this.Lock = string.Format("MS_AS_SHARED_CACHE_{0}_LOCK", cacheName);
			}

			// Token: 0x04000F01 RID: 3841
			public readonly string Cache;

			// Token: 0x04000F02 RID: 3842
			public readonly string Metadata;

			// Token: 0x04000F03 RID: 3843
			public readonly string Policy;

			// Token: 0x04000F04 RID: 3844
			public readonly string Timer;

			// Token: 0x04000F05 RID: 3845
			public readonly string Lock;
		}
	}
}
