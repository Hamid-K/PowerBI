using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.OAuth2;

namespace Microsoft.Identity.Client.Internal.Broker
{
	// Token: 0x02000260 RID: 608
	internal interface IBroker
	{
		// Token: 0x06001831 RID: 6193
		bool IsBrokerInstalledAndInvokable(AuthorityType authorityType);

		// Token: 0x06001832 RID: 6194
		Task<MsalTokenResponse> AcquireTokenInteractiveAsync(AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenInteractiveParameters acquireTokenInteractiveParameters);

		// Token: 0x06001833 RID: 6195
		Task<MsalTokenResponse> AcquireTokenSilentAsync(AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenSilentParameters acquireTokenSilentParameters);

		// Token: 0x06001834 RID: 6196
		Task<MsalTokenResponse> AcquireTokenSilentDefaultUserAsync(AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenSilentParameters acquireTokenSilentParameters);

		// Token: 0x06001835 RID: 6197
		Task<MsalTokenResponse> AcquireTokenByUsernamePasswordAsync(AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenByUsernamePasswordParameters acquireTokenByUsernamePasswordParameters);

		// Token: 0x06001836 RID: 6198
		IReadOnlyDictionary<string, string> GetSsoPolicyHeaders();

		// Token: 0x06001837 RID: 6199
		void HandleInstallUrl(string appLink);

		// Token: 0x06001838 RID: 6200
		Task<IReadOnlyList<IAccount>> GetAccountsAsync(string clientId, string redirectUri, AuthorityInfo authorityInfo, ICacheSessionManager cacheSessionManager, IInstanceDiscoveryManager instanceDiscoveryManager);

		// Token: 0x06001839 RID: 6201
		Task RemoveAccountAsync(ApplicationConfiguration appConfig, IAccount account);

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600183A RID: 6202
		bool IsPopSupported { get; }
	}
}
