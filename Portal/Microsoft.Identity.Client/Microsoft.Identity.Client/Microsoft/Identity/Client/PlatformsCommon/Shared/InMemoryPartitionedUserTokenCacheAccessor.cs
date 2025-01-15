using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001F4 RID: 500
	internal class InMemoryPartitionedUserTokenCacheAccessor : ITokenCacheAccessor
	{
		// Token: 0x06001566 RID: 5478 RVA: 0x00047348 File Offset: 0x00045548
		public InMemoryPartitionedUserTokenCacheAccessor(ILoggerAdapter logger, CacheOptions tokenCacheAccessorOptions)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
			this._tokenCacheAccessorOptions = tokenCacheAccessorOptions ?? new CacheOptions();
			if (this._tokenCacheAccessorOptions.UseSharedCache)
			{
				this.AccessTokenCacheDictionary = InMemoryPartitionedUserTokenCacheAccessor.s_accessTokenCacheDictionary;
				this.RefreshTokenCacheDictionary = InMemoryPartitionedUserTokenCacheAccessor.s_refreshTokenCacheDictionary;
				this.IdTokenCacheDictionary = InMemoryPartitionedUserTokenCacheAccessor.s_idTokenCacheDictionary;
				this.AccountCacheDictionary = InMemoryPartitionedUserTokenCacheAccessor.s_accountCacheDictionary;
				this.AppMetadataDictionary = InMemoryPartitionedUserTokenCacheAccessor.s_appMetadataDictionary;
				return;
			}
			this.AccessTokenCacheDictionary = new ConcurrentDictionary<string, ConcurrentDictionary<string, MsalAccessTokenCacheItem>>();
			this.RefreshTokenCacheDictionary = new ConcurrentDictionary<string, ConcurrentDictionary<string, MsalRefreshTokenCacheItem>>();
			this.IdTokenCacheDictionary = new ConcurrentDictionary<string, ConcurrentDictionary<string, MsalIdTokenCacheItem>>();
			this.AccountCacheDictionary = new ConcurrentDictionary<string, ConcurrentDictionary<string, MsalAccountCacheItem>>();
			this.AppMetadataDictionary = new ConcurrentDictionary<string, MsalAppMetadataCacheItem>();
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00047400 File Offset: 0x00045600
		public void SaveAccessToken(MsalAccessTokenCacheItem item)
		{
			string cacheKey = item.CacheKey;
			string keyFromCachedItem = CacheKeyFactory.GetKeyFromCachedItem(item);
			this.AccessTokenCacheDictionary.GetOrAdd(keyFromCachedItem, new ConcurrentDictionary<string, MsalAccessTokenCacheItem>())[cacheKey] = item;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00047434 File Offset: 0x00045634
		public void SaveRefreshToken(MsalRefreshTokenCacheItem item)
		{
			string cacheKey = item.CacheKey;
			string keyFromCachedItem = CacheKeyFactory.GetKeyFromCachedItem(item);
			this.RefreshTokenCacheDictionary.GetOrAdd(keyFromCachedItem, new ConcurrentDictionary<string, MsalRefreshTokenCacheItem>())[cacheKey] = item;
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00047468 File Offset: 0x00045668
		public void SaveIdToken(MsalIdTokenCacheItem item)
		{
			string cacheKey = item.CacheKey;
			string keyFromCachedItem = CacheKeyFactory.GetKeyFromCachedItem(item);
			this.IdTokenCacheDictionary.GetOrAdd(keyFromCachedItem, new ConcurrentDictionary<string, MsalIdTokenCacheItem>())[cacheKey] = item;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0004749C File Offset: 0x0004569C
		public void SaveAccount(MsalAccountCacheItem item)
		{
			string cacheKey = item.CacheKey;
			string keyFromCachedItem = CacheKeyFactory.GetKeyFromCachedItem(item);
			this.AccountCacheDictionary.GetOrAdd(keyFromCachedItem, new ConcurrentDictionary<string, MsalAccountCacheItem>())[cacheKey] = item;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x000474D0 File Offset: 0x000456D0
		public void SaveAppMetadata(MsalAppMetadataCacheItem item)
		{
			string cacheKey = item.CacheKey;
			this.AppMetadataDictionary[cacheKey] = item;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x000474F4 File Offset: 0x000456F4
		public MsalIdTokenCacheItem GetIdToken(MsalAccessTokenCacheItem accessTokenCacheItem)
		{
			string idTokenKeyFromCachedItem = CacheKeyFactory.GetIdTokenKeyFromCachedItem(accessTokenCacheItem);
			ConcurrentDictionary<string, MsalIdTokenCacheItem> concurrentDictionary;
			this.IdTokenCacheDictionary.TryGetValue(idTokenKeyFromCachedItem, out concurrentDictionary);
			MsalIdTokenCacheItem msalIdTokenCacheItem;
			if (concurrentDictionary != null && concurrentDictionary.TryGetValue(accessTokenCacheItem.GetIdTokenItem().CacheKey, out msalIdTokenCacheItem))
			{
				return msalIdTokenCacheItem;
			}
			this._logger.WarningPii("[Internal cache] Could not find an id token for the access token with key " + accessTokenCacheItem.CacheKey, "[Internal cache] Could not find an id token for the access token for realm " + accessTokenCacheItem.TenantId + " ");
			return null;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00047564 File Offset: 0x00045764
		public MsalAccountCacheItem GetAccount(MsalAccountCacheItem accountCacheItem)
		{
			string keyFromAccount = CacheKeyFactory.GetKeyFromAccount(accountCacheItem);
			ConcurrentDictionary<string, MsalAccountCacheItem> concurrentDictionary;
			this.AccountCacheDictionary.TryGetValue(keyFromAccount, out concurrentDictionary);
			MsalAccountCacheItem msalAccountCacheItem = null;
			if (concurrentDictionary != null)
			{
				concurrentDictionary.TryGetValue(accountCacheItem.CacheKey, out msalAccountCacheItem);
			}
			return msalAccountCacheItem;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0004759C File Offset: 0x0004579C
		public MsalAppMetadataCacheItem GetAppMetadata(MsalAppMetadataCacheItem appMetadataItem)
		{
			MsalAppMetadataCacheItem msalAppMetadataCacheItem;
			this.AppMetadataDictionary.TryGetValue(appMetadataItem.CacheKey, out msalAppMetadataCacheItem);
			return msalAppMetadataCacheItem;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000475C0 File Offset: 0x000457C0
		public void DeleteAccessToken(MsalAccessTokenCacheItem item)
		{
			string keyFromCachedItem = CacheKeyFactory.GetKeyFromCachedItem(item);
			ConcurrentDictionary<string, MsalAccessTokenCacheItem> concurrentDictionary;
			this.AccessTokenCacheDictionary.TryGetValue(keyFromCachedItem, out concurrentDictionary);
			MsalAccessTokenCacheItem msalAccessTokenCacheItem;
			if (concurrentDictionary == null || !concurrentDictionary.TryRemove(item.CacheKey, out msalAccessTokenCacheItem))
			{
				this._logger.InfoPii(() => "[Internal cache] Cannot delete access token because it was not found in the cache. Key " + item.CacheKey + ".", () => "[Internal cache] Cannot delete access token because it was not found in the cache.");
			}
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x00047644 File Offset: 0x00045844
		public void DeleteRefreshToken(MsalRefreshTokenCacheItem item)
		{
			string keyFromCachedItem = CacheKeyFactory.GetKeyFromCachedItem(item);
			ConcurrentDictionary<string, MsalRefreshTokenCacheItem> concurrentDictionary;
			this.RefreshTokenCacheDictionary.TryGetValue(keyFromCachedItem, out concurrentDictionary);
			MsalRefreshTokenCacheItem msalRefreshTokenCacheItem;
			if (concurrentDictionary == null || !concurrentDictionary.TryRemove(item.CacheKey, out msalRefreshTokenCacheItem))
			{
				this._logger.InfoPii(() => "[Internal cache] Cannot delete refresh token because it was not found in the cache. Key " + item.CacheKey + ".", () => "[Internal cache] Cannot delete refresh token because it was not found in the cache.");
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x000476C8 File Offset: 0x000458C8
		public void DeleteIdToken(MsalIdTokenCacheItem item)
		{
			string keyFromCachedItem = CacheKeyFactory.GetKeyFromCachedItem(item);
			ConcurrentDictionary<string, MsalIdTokenCacheItem> concurrentDictionary;
			this.IdTokenCacheDictionary.TryGetValue(keyFromCachedItem, out concurrentDictionary);
			MsalIdTokenCacheItem msalIdTokenCacheItem;
			if (concurrentDictionary == null || !concurrentDictionary.TryRemove(item.CacheKey, out msalIdTokenCacheItem))
			{
				this._logger.InfoPii(() => "[Internal cache] Cannot delete ID token because it was not found in the cache. Key " + item.CacheKey + ".", () => "[Internal cache] Cannot delete ID token because it was not found in the cache.");
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0004774C File Offset: 0x0004594C
		public void DeleteAccount(MsalAccountCacheItem item)
		{
			string keyFromCachedItem = CacheKeyFactory.GetKeyFromCachedItem(item);
			ConcurrentDictionary<string, MsalAccountCacheItem> concurrentDictionary;
			this.AccountCacheDictionary.TryGetValue(keyFromCachedItem, out concurrentDictionary);
			MsalAccountCacheItem msalAccountCacheItem;
			if (concurrentDictionary == null || !concurrentDictionary.TryRemove(item.CacheKey, out msalAccountCacheItem))
			{
				this._logger.InfoPii(() => "[Internal cache] Cannot delete account because it was not found in the cache. Key " + item.CacheKey + ".", () => "[Internal cache] Cannot delete account because it was not found in the cache");
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x000477D0 File Offset: 0x000459D0
		public virtual List<MsalAccessTokenCacheItem> GetAllAccessTokens(string partitionKey = null, ILoggerAdapter requestlogger = null)
		{
			(requestlogger ?? this._logger).Always(string.Format("[Internal cache] Total number of cache partitions found while getting access tokens: {0}", this.AccessTokenCacheDictionary.Count));
			if (string.IsNullOrEmpty(partitionKey))
			{
				return (from kv in this.AccessTokenCacheDictionary.SelectMany((KeyValuePair<string, ConcurrentDictionary<string, MsalAccessTokenCacheItem>> dict) => dict.Value)
					select kv.Value).ToList<MsalAccessTokenCacheItem>();
			}
			ConcurrentDictionary<string, MsalAccessTokenCacheItem> concurrentDictionary;
			this.AccessTokenCacheDictionary.TryGetValue(partitionKey, out concurrentDictionary);
			List<MsalAccessTokenCacheItem> list;
			if (concurrentDictionary == null)
			{
				list = null;
			}
			else
			{
				IEnumerable<MsalAccessTokenCacheItem> enumerable = concurrentDictionary.Select((KeyValuePair<string, MsalAccessTokenCacheItem> kv) => kv.Value);
				list = ((enumerable != null) ? enumerable.ToList<MsalAccessTokenCacheItem>() : null);
			}
			return list ?? CollectionHelpers.GetEmptyList<MsalAccessTokenCacheItem>();
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x000478B4 File Offset: 0x00045AB4
		public virtual List<MsalRefreshTokenCacheItem> GetAllRefreshTokens(string partitionKey = null, ILoggerAdapter requestlogger = null)
		{
			(requestlogger ?? this._logger).Always(string.Format("[Internal cache] Total number of cache partitions found while getting refresh tokens: {0}", this.RefreshTokenCacheDictionary.Count));
			if (string.IsNullOrEmpty(partitionKey))
			{
				return (from kv in this.RefreshTokenCacheDictionary.SelectMany((KeyValuePair<string, ConcurrentDictionary<string, MsalRefreshTokenCacheItem>> dict) => dict.Value)
					select kv.Value).ToList<MsalRefreshTokenCacheItem>();
			}
			ConcurrentDictionary<string, MsalRefreshTokenCacheItem> concurrentDictionary;
			this.RefreshTokenCacheDictionary.TryGetValue(partitionKey, out concurrentDictionary);
			List<MsalRefreshTokenCacheItem> list;
			if (concurrentDictionary == null)
			{
				list = null;
			}
			else
			{
				IEnumerable<MsalRefreshTokenCacheItem> enumerable = concurrentDictionary.Select((KeyValuePair<string, MsalRefreshTokenCacheItem> kv) => kv.Value);
				list = ((enumerable != null) ? enumerable.ToList<MsalRefreshTokenCacheItem>() : null);
			}
			return list ?? CollectionHelpers.GetEmptyList<MsalRefreshTokenCacheItem>();
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00047998 File Offset: 0x00045B98
		public virtual List<MsalIdTokenCacheItem> GetAllIdTokens(string partitionKey = null, ILoggerAdapter requestlogger = null)
		{
			if (string.IsNullOrEmpty(partitionKey))
			{
				return (from kv in this.IdTokenCacheDictionary.SelectMany((KeyValuePair<string, ConcurrentDictionary<string, MsalIdTokenCacheItem>> dict) => dict.Value)
					select kv.Value).ToList<MsalIdTokenCacheItem>();
			}
			ConcurrentDictionary<string, MsalIdTokenCacheItem> concurrentDictionary;
			this.IdTokenCacheDictionary.TryGetValue(partitionKey, out concurrentDictionary);
			List<MsalIdTokenCacheItem> list;
			if (concurrentDictionary == null)
			{
				list = null;
			}
			else
			{
				IEnumerable<MsalIdTokenCacheItem> enumerable = concurrentDictionary.Select((KeyValuePair<string, MsalIdTokenCacheItem> kv) => kv.Value);
				list = ((enumerable != null) ? enumerable.ToList<MsalIdTokenCacheItem>() : null);
			}
			return list ?? CollectionHelpers.GetEmptyList<MsalIdTokenCacheItem>();
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00047A50 File Offset: 0x00045C50
		public virtual List<MsalAccountCacheItem> GetAllAccounts(string partitionKey = null, ILoggerAdapter requestlogger = null)
		{
			if (string.IsNullOrEmpty(partitionKey))
			{
				return (from kv in this.AccountCacheDictionary.SelectMany((KeyValuePair<string, ConcurrentDictionary<string, MsalAccountCacheItem>> dict) => dict.Value)
					select kv.Value).ToList<MsalAccountCacheItem>();
			}
			ConcurrentDictionary<string, MsalAccountCacheItem> concurrentDictionary;
			this.AccountCacheDictionary.TryGetValue(partitionKey, out concurrentDictionary);
			List<MsalAccountCacheItem> list;
			if (concurrentDictionary == null)
			{
				list = null;
			}
			else
			{
				IEnumerable<MsalAccountCacheItem> enumerable = concurrentDictionary.Select((KeyValuePair<string, MsalAccountCacheItem> kv) => kv.Value);
				list = ((enumerable != null) ? enumerable.ToList<MsalAccountCacheItem>() : null);
			}
			return list ?? CollectionHelpers.GetEmptyList<MsalAccountCacheItem>();
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00047B08 File Offset: 0x00045D08
		public virtual List<MsalAppMetadataCacheItem> GetAllAppMetadata()
		{
			return this.AppMetadataDictionary.Select((KeyValuePair<string, MsalAppMetadataCacheItem> kv) => kv.Value).ToList<MsalAppMetadataCacheItem>();
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00047B39 File Offset: 0x00045D39
		public void SetiOSKeychainSecurityGroup(string keychainSecurityGroup)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00047B40 File Offset: 0x00045D40
		public virtual void Clear(ILoggerAdapter requestlogger = null)
		{
			(requestlogger ?? this._logger).Always("[Internal cache] Clearing user token cache accessor.");
			this.AccessTokenCacheDictionary.Clear();
			this.RefreshTokenCacheDictionary.Clear();
			this.IdTokenCacheDictionary.Clear();
			this.AccountCacheDictionary.Clear();
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00047B90 File Offset: 0x00045D90
		public virtual bool HasAccessOrRefreshTokens()
		{
			if (!this.RefreshTokenCacheDictionary.Any((KeyValuePair<string, ConcurrentDictionary<string, MsalRefreshTokenCacheItem>> partition) => partition.Value.Count > 0))
			{
				return this.AccessTokenCacheDictionary.Any((KeyValuePair<string, ConcurrentDictionary<string, MsalAccessTokenCacheItem>> partition) => partition.Value.Any((KeyValuePair<string, MsalAccessTokenCacheItem> token) => !token.Value.IsExpiredWithBuffer()));
			}
			return true;
		}

		// Token: 0x040008CF RID: 2255
		internal readonly ConcurrentDictionary<string, ConcurrentDictionary<string, MsalAccessTokenCacheItem>> AccessTokenCacheDictionary;

		// Token: 0x040008D0 RID: 2256
		internal readonly ConcurrentDictionary<string, ConcurrentDictionary<string, MsalRefreshTokenCacheItem>> RefreshTokenCacheDictionary;

		// Token: 0x040008D1 RID: 2257
		internal readonly ConcurrentDictionary<string, ConcurrentDictionary<string, MsalIdTokenCacheItem>> IdTokenCacheDictionary;

		// Token: 0x040008D2 RID: 2258
		internal readonly ConcurrentDictionary<string, ConcurrentDictionary<string, MsalAccountCacheItem>> AccountCacheDictionary;

		// Token: 0x040008D3 RID: 2259
		internal readonly ConcurrentDictionary<string, MsalAppMetadataCacheItem> AppMetadataDictionary;

		// Token: 0x040008D4 RID: 2260
		private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, MsalAccessTokenCacheItem>> s_accessTokenCacheDictionary = new ConcurrentDictionary<string, ConcurrentDictionary<string, MsalAccessTokenCacheItem>>();

		// Token: 0x040008D5 RID: 2261
		private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, MsalRefreshTokenCacheItem>> s_refreshTokenCacheDictionary = new ConcurrentDictionary<string, ConcurrentDictionary<string, MsalRefreshTokenCacheItem>>();

		// Token: 0x040008D6 RID: 2262
		private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, MsalIdTokenCacheItem>> s_idTokenCacheDictionary = new ConcurrentDictionary<string, ConcurrentDictionary<string, MsalIdTokenCacheItem>>();

		// Token: 0x040008D7 RID: 2263
		private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, MsalAccountCacheItem>> s_accountCacheDictionary = new ConcurrentDictionary<string, ConcurrentDictionary<string, MsalAccountCacheItem>>();

		// Token: 0x040008D8 RID: 2264
		private static readonly ConcurrentDictionary<string, MsalAppMetadataCacheItem> s_appMetadataDictionary = new ConcurrentDictionary<string, MsalAppMetadataCacheItem>();

		// Token: 0x040008D9 RID: 2265
		protected readonly ILoggerAdapter _logger;

		// Token: 0x040008DA RID: 2266
		private readonly CacheOptions _tokenCacheAccessorOptions;
	}
}
