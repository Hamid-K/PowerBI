using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000157 RID: 343
	internal interface ITokenCacheInternal : ITokenCache, ITokenCacheSerializer
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001103 RID: 4355
		OptionalSemaphoreSlim Semaphore { get; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001104 RID: 4356
		ILegacyCachePersistence LegacyPersistence { get; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001105 RID: 4357
		ITokenCacheAccessor Accessor { get; }

		// Token: 0x06001106 RID: 4358
		Task RemoveAccountAsync(IAccount account, AuthenticationRequestParameters requestParameters);

		// Token: 0x06001107 RID: 4359
		Task<bool> StopLongRunningOboProcessAsync(string longRunningOboCacheKey, AuthenticationRequestParameters requestParameters);

		// Token: 0x06001108 RID: 4360
		Task<IEnumerable<IAccount>> GetAccountsAsync(AuthenticationRequestParameters requestParameters);

		// Token: 0x06001109 RID: 4361
		Task<Tuple<MsalAccessTokenCacheItem, MsalIdTokenCacheItem, Account>> SaveTokenResponseAsync(AuthenticationRequestParameters requestParams, MsalTokenResponse response);

		// Token: 0x0600110A RID: 4362
		Task<MsalAccessTokenCacheItem> FindAccessTokenAsync(AuthenticationRequestParameters requestParams);

		// Token: 0x0600110B RID: 4363
		MsalIdTokenCacheItem GetIdTokenCacheItem(MsalAccessTokenCacheItem msalAccessTokenCacheItem);

		// Token: 0x0600110C RID: 4364
		Task<MsalRefreshTokenCacheItem> FindRefreshTokenAsync(AuthenticationRequestParameters requestParams, string familyId = null);

		// Token: 0x0600110D RID: 4365
		Task<Account> GetAccountAssociatedWithAccessTokenAsync(AuthenticationRequestParameters requestParameters, MsalAccessTokenCacheItem msalAccessTokenCacheItem);

		// Token: 0x0600110E RID: 4366
		Task<bool?> IsFociMemberAsync(AuthenticationRequestParameters requestParams, string familyId);

		// Token: 0x0600110F RID: 4367
		void SetIosKeychainSecurityGroup(string securityGroup);

		// Token: 0x06001110 RID: 4368
		Task OnAfterAccessAsync(TokenCacheNotificationArgs args);

		// Token: 0x06001111 RID: 4369
		Task OnBeforeAccessAsync(TokenCacheNotificationArgs args);

		// Token: 0x06001112 RID: 4370
		Task OnBeforeWriteAsync(TokenCacheNotificationArgs args);

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001113 RID: 4371
		bool IsApplicationCache { get; }

		// Token: 0x06001114 RID: 4372
		bool HasTokensNoLocks();

		// Token: 0x06001115 RID: 4373
		bool IsAppSubscribedToSerializationEvents();
	}
}
