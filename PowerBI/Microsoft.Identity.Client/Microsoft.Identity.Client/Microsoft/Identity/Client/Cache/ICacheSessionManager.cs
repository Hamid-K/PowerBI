using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.OAuth2;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002AA RID: 682
	internal interface ICacheSessionManager
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060019A6 RID: 6566
		RequestContext RequestContext { get; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060019A7 RID: 6567
		ITokenCacheInternal TokenCacheInternal { get; }

		// Token: 0x060019A8 RID: 6568
		Task<MsalAccessTokenCacheItem> FindAccessTokenAsync();

		// Token: 0x060019A9 RID: 6569
		Task<Tuple<MsalAccessTokenCacheItem, MsalIdTokenCacheItem, Account>> SaveTokenResponseAsync(MsalTokenResponse tokenResponse);

		// Token: 0x060019AA RID: 6570
		Task<MsalIdTokenCacheItem> GetIdTokenCacheItemAsync(MsalAccessTokenCacheItem accessTokenCacheItem);

		// Token: 0x060019AB RID: 6571
		Task<MsalRefreshTokenCacheItem> FindRefreshTokenAsync();

		// Token: 0x060019AC RID: 6572
		Task<MsalRefreshTokenCacheItem> FindFamilyRefreshTokenAsync(string familyId);

		// Token: 0x060019AD RID: 6573
		Task<bool?> IsAppFociMemberAsync(string familyId);

		// Token: 0x060019AE RID: 6574
		Task<IEnumerable<IAccount>> GetAccountsAsync();

		// Token: 0x060019AF RID: 6575
		Task<Account> GetAccountAssociatedWithAccessTokenAsync(MsalAccessTokenCacheItem msalAccessTokenCacheItem);
	}
}
