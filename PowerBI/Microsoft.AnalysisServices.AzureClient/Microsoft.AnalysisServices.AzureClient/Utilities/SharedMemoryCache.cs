using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x02000036 RID: 54
	internal sealed class SharedMemoryCache : MemoryCacheBase
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x00007EBE File Offset: 0x000060BE
		private SharedMemoryCache(string cacheName, SharedMemoryCache.GlobalNames names, IDictionary<string, object> cache, IDictionary<string, KeyValuePair<string, DateTime>> metadata, ISharedItemConverter converter, MemoryCacheRetentionPolicy retentionPolicy)
			: base(retentionPolicy)
		{
			this.CacheName = cacheName;
			this.names = names;
			this.cache = cache;
			this.metadata = metadata;
			this.converter = converter;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00007EED File Offset: 0x000060ED
		public string CacheName { get; }

		// Token: 0x060001B3 RID: 435 RVA: 0x00007EF8 File Offset: 0x000060F8
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

		// Token: 0x060001B4 RID: 436 RVA: 0x00007FC0 File Offset: 0x000061C0
		public static SharedMemoryCache Create(string cacheName, MemoryCacheRetentionPolicy retentionPolicy, IEqualityComparer<string> comparer, PrepareItemForCaching prepareMethod, ConvertCachedItem convertMethod)
		{
			return SharedMemoryCache.Create(cacheName, retentionPolicy, comparer, new SharedItemConverter(prepareMethod, convertMethod));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007FD2 File Offset: 0x000061D2
		protected override int GetItemCount()
		{
			return this.cache.Count;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007FE0 File Offset: 0x000061E0
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

		// Token: 0x060001B7 RID: 439 RVA: 0x0000808C File Offset: 0x0000628C
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

		// Token: 0x060001B8 RID: 440 RVA: 0x000080F0 File Offset: 0x000062F0
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

		// Token: 0x060001B9 RID: 441 RVA: 0x00008208 File Offset: 0x00006408
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

		// Token: 0x060001BA RID: 442 RVA: 0x000082E4 File Offset: 0x000064E4
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

		// Token: 0x060001BB RID: 443 RVA: 0x000083FC File Offset: 0x000065FC
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

		// Token: 0x060001BC RID: 444 RVA: 0x00008560 File Offset: 0x00006760
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

		// Token: 0x060001BD RID: 445 RVA: 0x000085C8 File Offset: 0x000067C8
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

		// Token: 0x060001BE RID: 446 RVA: 0x0000863C File Offset: 0x0000683C
		protected override IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfo()
		{
			IList<KeyValuePair<string, DateTime>> itemsExpirationInfoImpl;
			using (GlobalContext.CreateGlobalLockScope(this.names.Lock))
			{
				itemsExpirationInfoImpl = this.GetItemsExpirationInfoImpl();
			}
			return itemsExpirationInfoImpl;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008680 File Offset: 0x00006880
		private IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfoImpl()
		{
			List<KeyValuePair<string, DateTime>> list = new List<KeyValuePair<string, DateTime>>(this.metadata.Count);
			foreach (KeyValuePair<string, KeyValuePair<string, DateTime>> keyValuePair in this.metadata)
			{
				list.Add(new KeyValuePair<string, DateTime>(keyValuePair.Key, keyValuePair.Value.Value));
			}
			return list;
		}

		// Token: 0x040000DE RID: 222
		private const string GlobalObjectNameTemplate_Cache = "MS_AS_SHARED_CACHE_{0}_CACHE";

		// Token: 0x040000DF RID: 223
		private const string GlobalObjectNameTemplate_Metadata = "MS_AS_SHARED_CACHE_{0}_METADATA";

		// Token: 0x040000E0 RID: 224
		private const string GlobalObjectNameTemplate_Policy = "MS_AS_SHARED_CACHE_{0}_POLICY";

		// Token: 0x040000E1 RID: 225
		private const string GlobalObjectNameTemplate_Timer = "MS_AS_SHARED_CACHE_{0}_TIMER";

		// Token: 0x040000E2 RID: 226
		private const string GlobalObjectNameTemplate_Lock = "MS_AS_SHARED_CACHE_{0}_LOCK";

		// Token: 0x040000E3 RID: 227
		private SharedMemoryCache.GlobalNames names;

		// Token: 0x040000E4 RID: 228
		private IDictionary<string, object> cache;

		// Token: 0x040000E5 RID: 229
		private IDictionary<string, KeyValuePair<string, DateTime>> metadata;

		// Token: 0x040000E6 RID: 230
		private ISharedItemConverter converter;

		// Token: 0x02000074 RID: 116
		private struct GlobalNames
		{
			// Token: 0x060002E6 RID: 742 RVA: 0x0000C48C File Offset: 0x0000A68C
			public GlobalNames(string cacheName)
			{
				this.Cache = string.Format("MS_AS_SHARED_CACHE_{0}_CACHE", cacheName);
				this.Metadata = string.Format("MS_AS_SHARED_CACHE_{0}_METADATA", cacheName);
				this.Policy = string.Format("MS_AS_SHARED_CACHE_{0}_POLICY", cacheName);
				this.Timer = string.Format("MS_AS_SHARED_CACHE_{0}_TIMER", cacheName);
				this.Lock = string.Format("MS_AS_SHARED_CACHE_{0}_LOCK", cacheName);
			}

			// Token: 0x04000229 RID: 553
			public readonly string Cache;

			// Token: 0x0400022A RID: 554
			public readonly string Metadata;

			// Token: 0x0400022B RID: 555
			public readonly string Policy;

			// Token: 0x0400022C RID: 556
			public readonly string Timer;

			// Token: 0x0400022D RID: 557
			public readonly string Lock;
		}
	}
}
