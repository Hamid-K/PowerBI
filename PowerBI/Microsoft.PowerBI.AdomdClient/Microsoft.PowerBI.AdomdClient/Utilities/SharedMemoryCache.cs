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
		// Token: 0x060010A4 RID: 4260 RVA: 0x00039142 File Offset: 0x00037342
		private SharedMemoryCache(string cacheName, SharedMemoryCache.GlobalNames names, IDictionary<string, object> cache, IDictionary<string, KeyValuePair<string, DateTime>> metadata, ISharedItemConverter converter, MemoryCacheRetentionPolicy retentionPolicy)
			: base(retentionPolicy)
		{
			this.CacheName = cacheName;
			this.names = names;
			this.cache = cache;
			this.metadata = metadata;
			this.converter = converter;
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x00039171 File Offset: 0x00037371
		public string CacheName { get; }

		// Token: 0x060010A6 RID: 4262 RVA: 0x0003917C File Offset: 0x0003737C
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

		// Token: 0x060010A7 RID: 4263 RVA: 0x00039244 File Offset: 0x00037444
		public static SharedMemoryCache Create(string cacheName, MemoryCacheRetentionPolicy retentionPolicy, IEqualityComparer<string> comparer, PrepareItemForCaching prepareMethod, ConvertCachedItem convertMethod)
		{
			return SharedMemoryCache.Create(cacheName, retentionPolicy, comparer, new SharedItemConverter(prepareMethod, convertMethod));
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00039256 File Offset: 0x00037456
		protected override int GetItemCount()
		{
			return this.cache.Count;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00039264 File Offset: 0x00037464
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

		// Token: 0x060010AA RID: 4266 RVA: 0x00039310 File Offset: 0x00037510
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

		// Token: 0x060010AB RID: 4267 RVA: 0x00039374 File Offset: 0x00037574
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

		// Token: 0x060010AC RID: 4268 RVA: 0x0003948C File Offset: 0x0003768C
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

		// Token: 0x060010AD RID: 4269 RVA: 0x00039568 File Offset: 0x00037768
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

		// Token: 0x060010AE RID: 4270 RVA: 0x00039680 File Offset: 0x00037880
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

		// Token: 0x060010AF RID: 4271 RVA: 0x000397E4 File Offset: 0x000379E4
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

		// Token: 0x060010B0 RID: 4272 RVA: 0x0003984C File Offset: 0x00037A4C
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

		// Token: 0x060010B1 RID: 4273 RVA: 0x000398C0 File Offset: 0x00037AC0
		protected override IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfo()
		{
			IList<KeyValuePair<string, DateTime>> itemsExpirationInfoImpl;
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				itemsExpirationInfoImpl = this.GetItemsExpirationInfoImpl();
			}
			return itemsExpirationInfoImpl;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00039904 File Offset: 0x00037B04
		private IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfoImpl()
		{
			List<KeyValuePair<string, DateTime>> list = new List<KeyValuePair<string, DateTime>>(this.metadata.Count);
			foreach (KeyValuePair<string, KeyValuePair<string, DateTime>> keyValuePair in this.metadata)
			{
				list.Add(new KeyValuePair<string, DateTime>(keyValuePair.Key, keyValuePair.Value.Value));
			}
			return list;
		}

		// Token: 0x04000B13 RID: 2835
		private const string GlobalObjectNameTemplate_Cache = "MS_AS_SHARED_CACHE_{0}_CACHE";

		// Token: 0x04000B14 RID: 2836
		private const string GlobalObjectNameTemplate_Metadata = "MS_AS_SHARED_CACHE_{0}_METADATA";

		// Token: 0x04000B15 RID: 2837
		private const string GlobalObjectNameTemplate_Policy = "MS_AS_SHARED_CACHE_{0}_POLICY";

		// Token: 0x04000B16 RID: 2838
		private const string GlobalObjectNameTemplate_Timer = "MS_AS_SHARED_CACHE_{0}_TIMER";

		// Token: 0x04000B17 RID: 2839
		private const string GlobalObjectNameTemplate_Lock = "MS_AS_SHARED_CACHE_{0}_LOCK";

		// Token: 0x04000B18 RID: 2840
		private SharedMemoryCache.GlobalNames names;

		// Token: 0x04000B19 RID: 2841
		private IDictionary<string, object> cache;

		// Token: 0x04000B1A RID: 2842
		private IDictionary<string, KeyValuePair<string, DateTime>> metadata;

		// Token: 0x04000B1B RID: 2843
		private ISharedItemConverter converter;

		// Token: 0x02000202 RID: 514
		private struct GlobalNames
		{
			// Token: 0x060014A7 RID: 5287 RVA: 0x00046A1C File Offset: 0x00044C1C
			public GlobalNames(string cacheName)
			{
				this.Cache = string.Format("MS_AS_SHARED_CACHE_{0}_CACHE", cacheName);
				this.Metadata = string.Format("MS_AS_SHARED_CACHE_{0}_METADATA", cacheName);
				this.Policy = string.Format("MS_AS_SHARED_CACHE_{0}_POLICY", cacheName);
				this.Timer = string.Format("MS_AS_SHARED_CACHE_{0}_TIMER", cacheName);
				this.Lock = string.Format("MS_AS_SHARED_CACHE_{0}_LOCK", cacheName);
			}

			// Token: 0x04000EEB RID: 3819
			public readonly string Cache;

			// Token: 0x04000EEC RID: 3820
			public readonly string Metadata;

			// Token: 0x04000EED RID: 3821
			public readonly string Policy;

			// Token: 0x04000EEE RID: 3822
			public readonly string Timer;

			// Token: 0x04000EEF RID: 3823
			public readonly string Lock;
		}
	}
}
