using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x0200198C RID: 6540
	public class CacheGroupManager
	{
		// Token: 0x0600A5E5 RID: 42469 RVA: 0x00224E24 File Offset: 0x00223024
		private CacheGroupManager(string cacheGroup)
		{
			this.syncRoot = new object();
			this.cacheGroup = cacheGroup;
			this.caches = new Dictionary<string, CacheManagerCacheInfo>();
			this.lastUsed = DateTime.UtcNow;
		}

		// Token: 0x17002A60 RID: 10848
		// (get) Token: 0x0600A5E6 RID: 42470 RVA: 0x00224E54 File Offset: 0x00223054
		public string CacheGroup
		{
			get
			{
				return this.cacheGroup;
			}
		}

		// Token: 0x17002A61 RID: 10849
		// (get) Token: 0x0600A5E7 RID: 42471 RVA: 0x00224E5C File Offset: 0x0022305C
		public CacheManagerCacheInfo[] Caches
		{
			get
			{
				object obj = this.syncRoot;
				CacheManagerCacheInfo[] array;
				lock (obj)
				{
					array = this.caches.Values.ToArray<CacheManagerCacheInfo>();
				}
				return array;
			}
		}

		// Token: 0x0600A5E8 RID: 42472 RVA: 0x00224EA8 File Offset: 0x002230A8
		public static bool IsInitialized()
		{
			return DynamicDiskCacheRoot.GetDirectory() != null;
		}

		// Token: 0x0600A5E9 RID: 42473 RVA: 0x00224EB4 File Offset: 0x002230B4
		public static CacheGroupManager GetOrCreateCacheGroupManager(string cacheGroup)
		{
			if (string.IsNullOrEmpty(cacheGroup))
			{
				throw new ArgumentNullException("cacheGroup");
			}
			Dictionary<string, CacheGroupManager> dictionary = CacheGroupManager.cacheGroups;
			CacheGroupManager cacheGroupManager2;
			lock (dictionary)
			{
				CacheGroupManager cacheGroupManager;
				if (!CacheGroupManager.cacheGroups.TryGetValue(cacheGroup, out cacheGroupManager))
				{
					cacheGroupManager = new CacheGroupManager(cacheGroup);
					CacheGroupManager.cacheGroups.Add(cacheGroup, cacheGroupManager);
				}
				cacheGroupManager2 = cacheGroupManager;
			}
			return cacheGroupManager2;
		}

		// Token: 0x0600A5EA RID: 42474 RVA: 0x00224F28 File Offset: 0x00223128
		public static void SetGarbageCollectionTimeout(TimeSpan timeout)
		{
			Dictionary<string, CacheGroupManager> dictionary = CacheGroupManager.cacheGroups;
			lock (dictionary)
			{
				if (CacheGroupManager.timer != null)
				{
					throw new InvalidOperationException("Garbage collection timeout has already been set");
				}
				CacheGroupManager.timer = new Timer(new TimerCallback(CacheGroupManager.ClearGarbage), timeout, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(1.0));
			}
		}

		// Token: 0x0600A5EB RID: 42475 RVA: 0x00224FAC File Offset: 0x002231AC
		public CacheManager CreateCacheManager(IEngineHost engineHost, IEngine engine)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.lastUsed = DateTime.UtcNow;
				this.activeEvaluations++;
			}
			return new CacheManager(this, engineHost, engine, this.cacheGroup);
		}

		// Token: 0x0600A5EC RID: 42476 RVA: 0x00225010 File Offset: 0x00223210
		public void AddCache(string identity, CacheManagerCacheInfo info)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.caches.Add(identity, info);
			}
		}

		// Token: 0x0600A5ED RID: 42477 RVA: 0x00225058 File Offset: 0x00223258
		public void DeleteCache(string identity)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.caches.Remove(identity);
			}
		}

		// Token: 0x0600A5EE RID: 42478 RVA: 0x002250A0 File Offset: 0x002232A0
		public bool CheckCache(string identity)
		{
			object obj = this.syncRoot;
			bool flag2;
			lock (obj)
			{
				CacheManagerCacheInfo cacheManagerCacheInfo;
				flag2 = this.caches.TryGetValue(identity, out cacheManagerCacheInfo);
			}
			return flag2;
		}

		// Token: 0x0600A5EF RID: 42479 RVA: 0x002250EC File Offset: 0x002232EC
		public bool TryGetCacheConfig(string identity, out CacheManagerCacheInfo cacheConfig)
		{
			object obj = this.syncRoot;
			bool flag2;
			lock (obj)
			{
				flag2 = this.caches.TryGetValue(identity, out cacheConfig);
			}
			return flag2;
		}

		// Token: 0x0600A5F0 RID: 42480 RVA: 0x00225138 File Offset: 0x00223338
		public bool TryGetCacheConfigFromDirectory(string directory, out CacheManagerCacheInfo cacheConfig)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				foreach (KeyValuePair<string, CacheManagerCacheInfo> keyValuePair in this.caches)
				{
					CompoundCacheConfig compoundCacheConfig;
					if (((CompoundCacheConfig)keyValuePair.Value.CacheConfig).TryGetByDirectory(directory, out compoundCacheConfig))
					{
						cacheConfig = new CacheManagerCacheInfo
						{
							Identifier = keyValuePair.Key,
							CacheConfig = compoundCacheConfig
						};
						return true;
					}
				}
			}
			cacheConfig = default(CacheManagerCacheInfo);
			return false;
		}

		// Token: 0x0600A5F1 RID: 42481 RVA: 0x00225200 File Offset: 0x00223400
		public void Finished()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.lastUsed = DateTime.UtcNow;
				this.activeEvaluations--;
			}
		}

		// Token: 0x0600A5F2 RID: 42482 RVA: 0x00225254 File Offset: 0x00223454
		public bool ShouldPurge(TimeSpan timeout)
		{
			object obj = this.syncRoot;
			bool flag2;
			lock (obj)
			{
				flag2 = this.activeEvaluations == 0 && DateTime.UtcNow - this.lastUsed > timeout;
			}
			return flag2;
		}

		// Token: 0x0600A5F3 RID: 42483 RVA: 0x002252B4 File Offset: 0x002234B4
		public string MakeDirectory(string id)
		{
			return Path.Combine(Path.Combine(DynamicDiskCacheRoot.GetDirectory(), this.cacheGroup), id);
		}

		// Token: 0x0600A5F4 RID: 42484 RVA: 0x002252CC File Offset: 0x002234CC
		private static void ClearGarbage(object timeoutIn)
		{
			TimeSpan timeSpan = (TimeSpan)timeoutIn;
			Dictionary<string, CacheGroupManager> dictionary = CacheGroupManager.cacheGroups;
			List<CacheGroupManager> list;
			lock (dictionary)
			{
				list = CacheGroupManager.cacheGroups.Values.ToList<CacheGroupManager>();
				for (int i = list.Count - 1; i >= 0; i--)
				{
					if (list[i].ShouldPurge(timeSpan))
					{
						CacheGroupManager.cacheGroups.Remove(list[i].CacheGroup);
					}
				}
			}
			foreach (CacheGroupManager cacheGroupManager in list)
			{
				cacheGroupManager.Purge();
			}
		}

		// Token: 0x0600A5F5 RID: 42485 RVA: 0x0000336E File Offset: 0x0000156E
		private void Purge()
		{
		}

		// Token: 0x04005652 RID: 22098
		private static readonly Dictionary<string, CacheGroupManager> cacheGroups = new Dictionary<string, CacheGroupManager>();

		// Token: 0x04005653 RID: 22099
		private static Timer timer;

		// Token: 0x04005654 RID: 22100
		private readonly object syncRoot;

		// Token: 0x04005655 RID: 22101
		private readonly string cacheGroup;

		// Token: 0x04005656 RID: 22102
		private readonly Dictionary<string, CacheManagerCacheInfo> caches;

		// Token: 0x04005657 RID: 22103
		private int activeEvaluations;

		// Token: 0x04005658 RID: 22104
		private DateTime lastUsed;
	}
}
