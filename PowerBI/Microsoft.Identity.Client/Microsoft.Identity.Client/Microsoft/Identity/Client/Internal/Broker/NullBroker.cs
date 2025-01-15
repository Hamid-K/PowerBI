using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.Internal.Logger;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Internal.Broker
{
	// Token: 0x02000261 RID: 609
	internal class NullBroker : IBroker
	{
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x00050A65 File Offset: 0x0004EC65
		public bool IsPopSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00050A68 File Offset: 0x0004EC68
		public NullBroker(ILoggerAdapter logger)
		{
			this._logger = logger ?? new NullLogger();
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00050A80 File Offset: 0x0004EC80
		public virtual bool IsBrokerInstalledAndInvokable(AuthorityType authorityType)
		{
			this._logger.Info("NullBroker - acting as not installed.");
			return false;
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00050A93 File Offset: 0x0004EC93
		public Task<MsalTokenResponse> AcquireTokenInteractiveAsync(AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenInteractiveParameters acquireTokenInteractiveParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00050A9A File Offset: 0x0004EC9A
		public Task<MsalTokenResponse> AcquireTokenSilentAsync(AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenSilentParameters acquireTokenSilentParameters)
		{
			this._logger.Info("NullBroker - returning null on silent request.");
			return Task.FromResult<MsalTokenResponse>(null);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00050AB2 File Offset: 0x0004ECB2
		public void HandleInstallUrl(string appLink)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00050AB9 File Offset: 0x0004ECB9
		public Task RemoveAccountAsync(ApplicationConfiguration appConfig, IAccount account)
		{
			this._logger.Info("NullBroker::RemoveAccountAsync - NOP.");
			return Task.Delay(0);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00050AD1 File Offset: 0x0004ECD1
		public Task<MsalTokenResponse> AcquireTokenSilentDefaultUserAsync(AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenSilentParameters acquireTokenSilentParameters)
		{
			this._logger.Info("NullBroker - returning null on silent request.");
			return Task.FromResult<MsalTokenResponse>(null);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00050AE9 File Offset: 0x0004ECE9
		public Task<IReadOnlyList<IAccount>> GetAccountsAsync(string clientID, string redirectUri, AuthorityInfo authorityInfo, ICacheSessionManager cacheSessionManager, IInstanceDiscoveryManager instanceDiscoveryManager)
		{
			this._logger.Info("NullBroker - returning empty list on GetAccounts request.");
			return Task.FromResult<IReadOnlyList<IAccount>>(CollectionHelpers.GetEmptyReadOnlyList<IAccount>());
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00050B05 File Offset: 0x0004ED05
		public Task<MsalTokenResponse> AcquireTokenByUsernamePasswordAsync(AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenByUsernamePasswordParameters acquireTokenByUsernamePasswordParameters)
		{
			this._logger.Info("NullBroker - returning null on ROPC request.");
			return Task.FromResult<MsalTokenResponse>(null);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00050B1D File Offset: 0x0004ED1D
		public IReadOnlyDictionary<string, string> GetSsoPolicyHeaders()
		{
			return CollectionHelpers.GetEmptyDictionary<string, string>();
		}

		// Token: 0x04000AEF RID: 2799
		private readonly ILoggerAdapter _logger;
	}
}
