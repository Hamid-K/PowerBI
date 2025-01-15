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
	// Token: 0x020001F3 RID: 499
	internal class InMemoryPartitionedAppTokenCacheAccessor : ITokenCacheAccessor
	{
		// Token: 0x06001550 RID: 5456 RVA: 0x00046F88 File Offset: 0x00045188
		public InMemoryPartitionedAppTokenCacheAccessor(ILoggerAdapter logger, CacheOptions tokenCacheAccessorOptions)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
			this._tokenCacheAccessorOptions = tokenCacheAccessorOptions ?? new CacheOptions();
			if (this._tokenCacheAccessorOptions.UseSharedCache)
			{
				this.AccessTokenCacheDictionary = InMemoryPartitionedAppTokenCacheAccessor.s_accessTokenCacheDictionary;
				this.AppMetadataDictionary = InMemoryPartitionedAppTokenCacheAccessor.s_appMetadataDictionary;
				return;
			}
			this.AccessTokenCacheDictionary = new ConcurrentDictionary<string, ConcurrentDictionary<string, MsalAccessTokenCacheItem>>();
			this.AppMetadataDictionary = new ConcurrentDictionary<string, MsalAppMetadataCacheItem>();
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00046FFC File Offset: 0x000451FC
		public void SaveAccessToken(MsalAccessTokenCacheItem item)
		{
			string cacheKey = item.CacheKey;
			string clientCredentialKey = CacheKeyFactory.GetClientCredentialKey(item.ClientId, item.TenantId, item.KeyId);
			this.AccessTokenCacheDictionary.GetOrAdd(clientCredentialKey, new ConcurrentDictionary<string, MsalAccessTokenCacheItem>())[cacheKey] = item;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00047040 File Offset: 0x00045240
		public void SaveRefreshToken(MsalRefreshTokenCacheItem item)
		{
			throw new MsalClientException("combined_user_app_cache_not_supported", "Using a combined flat storage, like a file, to store both app and user tokens is not supported. Use a partitioned token cache (for ex. distributed cache like Redis) or separate files for app and user token caches. See https://aka.ms/msal-net-token-cache-serialization .");
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00047051 File Offset: 0x00045251
		public void SaveIdToken(MsalIdTokenCacheItem item)
		{
			throw new MsalClientException("combined_user_app_cache_not_supported", "Using a combined flat storage, like a file, to store both app and user tokens is not supported. Use a partitioned token cache (for ex. distributed cache like Redis) or separate files for app and user token caches. See https://aka.ms/msal-net-token-cache-serialization .");
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00047062 File Offset: 0x00045262
		public void SaveAccount(MsalAccountCacheItem item)
		{
			throw new MsalClientException("combined_user_app_cache_not_supported", "Using a combined flat storage, like a file, to store both app and user tokens is not supported. Use a partitioned token cache (for ex. distributed cache like Redis) or separate files for app and user token caches. See https://aka.ms/msal-net-token-cache-serialization .");
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00047074 File Offset: 0x00045274
		public void SaveAppMetadata(MsalAppMetadataCacheItem item)
		{
			string cacheKey = item.CacheKey;
			this.AppMetadataDictionary[cacheKey] = item;
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00047095 File Offset: 0x00045295
		public MsalIdTokenCacheItem GetIdToken(MsalAccessTokenCacheItem accessTokenCacheItem)
		{
			throw new MsalClientException("combined_user_app_cache_not_supported", "Using a combined flat storage, like a file, to store both app and user tokens is not supported. Use a partitioned token cache (for ex. distributed cache like Redis) or separate files for app and user token caches. See https://aka.ms/msal-net-token-cache-serialization .");
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x000470A6 File Offset: 0x000452A6
		public MsalAccountCacheItem GetAccount(MsalAccountCacheItem accountCacheItem)
		{
			throw new MsalClientException("combined_user_app_cache_not_supported", "Using a combined flat storage, like a file, to store both app and user tokens is not supported. Use a partitioned token cache (for ex. distributed cache like Redis) or separate files for app and user token caches. See https://aka.ms/msal-net-token-cache-serialization .");
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x000470B8 File Offset: 0x000452B8
		public MsalAppMetadataCacheItem GetAppMetadata(MsalAppMetadataCacheItem appMetadataItem)
		{
			MsalAppMetadataCacheItem msalAppMetadataCacheItem;
			this.AppMetadataDictionary.TryGetValue(appMetadataItem.CacheKey, out msalAppMetadataCacheItem);
			return msalAppMetadataCacheItem;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x000470DC File Offset: 0x000452DC
		public void DeleteAccessToken(MsalAccessTokenCacheItem item)
		{
			string clientCredentialKey = CacheKeyFactory.GetClientCredentialKey(item.ClientId, item.TenantId, item.KeyId);
			ConcurrentDictionary<string, MsalAccessTokenCacheItem> concurrentDictionary;
			this.AccessTokenCacheDictionary.TryGetValue(clientCredentialKey, out concurrentDictionary);
			MsalAccessTokenCacheItem msalAccessTokenCacheItem;
			if (concurrentDictionary == null || !concurrentDictionary.TryRemove(item.CacheKey, out msalAccessTokenCacheItem))
			{
				this._logger.InfoPii(() => "[Internal cache] Cannot delete access token because it was not found in the cache. Key " + item.CacheKey + ".", () => "[Internal cache] Cannot delete access token because it was not found in the cache.");
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0004717A File Offset: 0x0004537A
		public void DeleteRefreshToken(MsalRefreshTokenCacheItem item)
		{
			throw new MsalClientException("combined_user_app_cache_not_supported", "Using a combined flat storage, like a file, to store both app and user tokens is not supported. Use a partitioned token cache (for ex. distributed cache like Redis) or separate files for app and user token caches. See https://aka.ms/msal-net-token-cache-serialization .");
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0004718B File Offset: 0x0004538B
		public void DeleteIdToken(MsalIdTokenCacheItem item)
		{
			throw new MsalClientException("combined_user_app_cache_not_supported", "Using a combined flat storage, like a file, to store both app and user tokens is not supported. Use a partitioned token cache (for ex. distributed cache like Redis) or separate files for app and user token caches. See https://aka.ms/msal-net-token-cache-serialization .");
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0004719C File Offset: 0x0004539C
		public void DeleteAccount(MsalAccountCacheItem item)
		{
			throw new MsalClientException("combined_user_app_cache_not_supported", "Using a combined flat storage, like a file, to store both app and user tokens is not supported. Use a partitioned token cache (for ex. distributed cache like Redis) or separate files for app and user token caches. See https://aka.ms/msal-net-token-cache-serialization .");
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x000471B0 File Offset: 0x000453B0
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

		// Token: 0x0600155E RID: 5470 RVA: 0x00047292 File Offset: 0x00045492
		public virtual List<MsalRefreshTokenCacheItem> GetAllRefreshTokens(string partitionKey = null, ILoggerAdapter requestlogger = null)
		{
			return CollectionHelpers.GetEmptyList<MsalRefreshTokenCacheItem>();
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00047299 File Offset: 0x00045499
		public virtual List<MsalIdTokenCacheItem> GetAllIdTokens(string partitionKey = null, ILoggerAdapter requestlogger = null)
		{
			return CollectionHelpers.GetEmptyList<MsalIdTokenCacheItem>();
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x000472A0 File Offset: 0x000454A0
		public virtual List<MsalAccountCacheItem> GetAllAccounts(string partitionKey = null, ILoggerAdapter requestlogger = null)
		{
			return CollectionHelpers.GetEmptyList<MsalAccountCacheItem>();
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000472A7 File Offset: 0x000454A7
		public List<MsalAppMetadataCacheItem> GetAllAppMetadata()
		{
			return this.AppMetadataDictionary.Select((KeyValuePair<string, MsalAppMetadataCacheItem> kv) => kv.Value).ToList<MsalAppMetadataCacheItem>();
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x000472D8 File Offset: 0x000454D8
		public void SetiOSKeychainSecurityGroup(string keychainSecurityGroup)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x000472DF File Offset: 0x000454DF
		public virtual void Clear(ILoggerAdapter requestlogger = null)
		{
			ILoggerAdapter loggerAdapter = requestlogger ?? this._logger;
			this.AccessTokenCacheDictionary.Clear();
			loggerAdapter.Always("[Internal cache] Clearing app token cache accessor.");
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00047301 File Offset: 0x00045501
		public virtual bool HasAccessOrRefreshTokens()
		{
			return this.AccessTokenCacheDictionary.Any((KeyValuePair<string, ConcurrentDictionary<string, MsalAccessTokenCacheItem>> partition) => partition.Value.Any((KeyValuePair<string, MsalAccessTokenCacheItem> token) => !token.Value.IsExpiredWithBuffer()));
		}

		// Token: 0x040008C9 RID: 2249
		internal readonly ConcurrentDictionary<string, ConcurrentDictionary<string, MsalAccessTokenCacheItem>> AccessTokenCacheDictionary;

		// Token: 0x040008CA RID: 2250
		internal readonly ConcurrentDictionary<string, MsalAppMetadataCacheItem> AppMetadataDictionary;

		// Token: 0x040008CB RID: 2251
		private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, MsalAccessTokenCacheItem>> s_accessTokenCacheDictionary = new ConcurrentDictionary<string, ConcurrentDictionary<string, MsalAccessTokenCacheItem>>();

		// Token: 0x040008CC RID: 2252
		private static readonly ConcurrentDictionary<string, MsalAppMetadataCacheItem> s_appMetadataDictionary = new ConcurrentDictionary<string, MsalAppMetadataCacheItem>(1, 1);

		// Token: 0x040008CD RID: 2253
		protected readonly ILoggerAdapter _logger;

		// Token: 0x040008CE RID: 2254
		private readonly CacheOptions _tokenCacheAccessorOptions;
	}
}
