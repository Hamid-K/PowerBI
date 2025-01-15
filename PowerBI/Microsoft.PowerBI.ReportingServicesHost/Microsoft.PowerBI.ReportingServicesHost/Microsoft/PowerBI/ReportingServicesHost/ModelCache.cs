using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Caching;
using Microsoft.InfoNav;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.Telemetry;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000050 RID: 80
	internal sealed class ModelCache<T> : IModelCache<T>, IDisposable
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00004EED File Offset: 0x000030ED
		internal ModelCache(ModelCacheConfig config)
		{
			this.m_memoryCache = new MemoryCache("memoryCache", config.MemoryCacheConfig);
			this.m_transientCacheExpiration = config.TransientCacheExpiration;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00004F18 File Offset: 0x00003118
		public bool Add(string connectionString, string modelMetadataVersion, TranslationsBehavior translationsBehavior, T value, ConnectionType connectionType)
		{
			CacheItemPolicy cacheItemPolicy = this.BuildCacheItemPolicy(connectionType);
			string text = ModelCache<T>.MemoryCacheKey.BuildKey(connectionString, modelMetadataVersion, translationsBehavior);
			bool flag = this.m_memoryCache.Add(new CacheItem(text, value), cacheItemPolicy);
			int num = this.m_memoryCache.Count<KeyValuePair<string, object>>();
			if (num > this.m_maxSize)
			{
				this.m_maxSize = num;
			}
			if (!flag)
			{
				TelemetryService.Instance.Log(new PBIWinDuplicateKeyAddedToModelCache("0", false));
			}
			return flag;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00004F84 File Offset: 0x00003184
		public bool TryGetValue(string connectionString, string modelMetadataVersion, TranslationsBehavior translationsBehavior, out T value)
		{
			this.m_requestCount++;
			string text = ModelCache<T>.MemoryCacheKey.BuildKey(connectionString, modelMetadataVersion, translationsBehavior);
			object obj = this.m_memoryCache.Get(text, null);
			if (obj == null)
			{
				value = default(T);
				return false;
			}
			this.m_cacheHits++;
			value = (T)((object)obj);
			return true;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00004FE0 File Offset: 0x000031E0
		public void Clear(string connectionString)
		{
			ModelCache<T>.MemoryCacheKey keyToMatch = new ModelCache<T>.MemoryCacheKey
			{
				SerializedConnectionStringProperties = ModelCache<T>.MemoryCacheKey.NormalizeAndSerializeConnectionStringProperties(connectionString)
			};
			foreach (string text in from k in this.m_memoryCache.GetKeys<string, object>()
				where ModelCache<T>.MemoryCacheKey.ParseKey(k).MatchesConnectionString(keyToMatch)
				select k)
			{
				this.m_memoryCache.Remove(text, null);
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005068 File Offset: 0x00003268
		public void ClearAll()
		{
			this.m_memoryCache.Dispose();
			this.m_memoryCache = new MemoryCache("memoryCache", null);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005086 File Offset: 0x00003286
		public void Dispose()
		{
			TelemetryService.Instance.Log(new PBIWinModelCacheHitSummary(this.m_requestCount, this.m_cacheHits, this.m_maxSize, TraceType.Information, "Summary of ModelCache performance"));
			this.m_memoryCache.Dispose();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000050BA File Offset: 0x000032BA
		private CacheItemPolicy BuildCacheItemPolicy(ConnectionType connectionType)
		{
			if (connectionType == ConnectionType.Transient)
			{
				return new CacheItemPolicy
				{
					SlidingExpiration = this.m_transientCacheExpiration
				};
			}
			if (connectionType == ConnectionType.Stable)
			{
				return new CacheItemPolicy
				{
					Priority = CacheItemPriority.NotRemovable
				};
			}
			throw new InvalidOperationException("ConnectionType not supported");
		}

		// Token: 0x04000113 RID: 275
		private readonly TimeSpan m_transientCacheExpiration;

		// Token: 0x04000114 RID: 276
		private MemoryCache m_memoryCache;

		// Token: 0x04000115 RID: 277
		private int m_requestCount;

		// Token: 0x04000116 RID: 278
		private int m_cacheHits;

		// Token: 0x04000117 RID: 279
		private int m_maxSize;

		// Token: 0x02000076 RID: 118
		private sealed class MemoryCacheKey
		{
			// Token: 0x1700009D RID: 157
			// (get) Token: 0x06000260 RID: 608 RVA: 0x00006F9E File Offset: 0x0000519E
			// (set) Token: 0x06000261 RID: 609 RVA: 0x00006FA6 File Offset: 0x000051A6
			public string SerializedConnectionStringProperties { get; set; }

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x06000262 RID: 610 RVA: 0x00006FAF File Offset: 0x000051AF
			// (set) Token: 0x06000263 RID: 611 RVA: 0x00006FB7 File Offset: 0x000051B7
			public string ModelMetadataVersion { get; set; }

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x06000264 RID: 612 RVA: 0x00006FC0 File Offset: 0x000051C0
			// (set) Token: 0x06000265 RID: 613 RVA: 0x00006FC8 File Offset: 0x000051C8
			public TranslationsBehavior TranslationsBehavior { get; set; }

			// Token: 0x06000266 RID: 614 RVA: 0x00006FD1 File Offset: 0x000051D1
			public bool MatchesConnectionString(ModelCache<T>.MemoryCacheKey keyToMatch)
			{
				return this.SerializedConnectionStringProperties == keyToMatch.SerializedConnectionStringProperties;
			}

			// Token: 0x06000267 RID: 615 RVA: 0x00006FE4 File Offset: 0x000051E4
			public static string BuildKey(string connectionString, string modelMetadataVersion, TranslationsBehavior translationsBehavior)
			{
				return JsonConvert.SerializeObject(new ModelCache<T>.MemoryCacheKey
				{
					SerializedConnectionStringProperties = ModelCache<T>.MemoryCacheKey.NormalizeAndSerializeConnectionStringProperties(connectionString),
					ModelMetadataVersion = modelMetadataVersion,
					TranslationsBehavior = translationsBehavior
				});
			}

			// Token: 0x06000268 RID: 616 RVA: 0x0000700A File Offset: 0x0000520A
			public static ModelCache<T>.MemoryCacheKey ParseKey(string key)
			{
				return JsonConvert.DeserializeObject<ModelCache<T>.MemoryCacheKey>(key);
			}

			// Token: 0x06000269 RID: 617 RVA: 0x00007014 File Offset: 0x00005214
			public static string NormalizeAndSerializeConnectionStringProperties(string connectionString)
			{
				DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
				dbConnectionStringBuilder.ConnectionString = connectionString;
				SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				foreach (object obj in ((IEnumerable)dbConnectionStringBuilder))
				{
					KeyValuePair<string, object> keyValuePair = (KeyValuePair<string, object>)obj;
					string text = keyValuePair.Key.ToLowerInvariant();
					SortedDictionary<string, string> sortedDictionary2 = sortedDictionary;
					string text2 = text;
					object value = keyValuePair.Value;
					sortedDictionary2.Add(text2, (value != null) ? value.ToString() : null);
				}
				return JsonConvert.SerializeObject(sortedDictionary);
			}
		}
	}
}
