using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Executors;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.Broker;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000148 RID: 328
	public abstract class ClientApplicationBase : ApplicationBase, IClientApplicationBase, IApplicationBase
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0003AF83 File Offset: 0x00039183
		public IAppConfig AppConfig
		{
			get
			{
				return base.ServiceBundle.Config;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x0003AF90 File Offset: 0x00039190
		public ITokenCache UserTokenCache
		{
			get
			{
				return this.UserTokenCacheInternal;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x0003AF98 File Offset: 0x00039198
		internal ITokenCacheInternal UserTokenCacheInternal { get; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x0003AFA0 File Offset: 0x000391A0
		public string Authority
		{
			get
			{
				Uri canonicalAuthority = base.ServiceBundle.Config.Authority.AuthorityInfo.CanonicalAuthority;
				if (canonicalAuthority == null)
				{
					return null;
				}
				return canonicalAuthority.ToString();
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x0003AFC7 File Offset: 0x000391C7
		internal AuthorityInfo AuthorityInfo
		{
			get
			{
				return base.ServiceBundle.Config.Authority.AuthorityInfo;
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0003AFDE File Offset: 0x000391DE
		internal ClientApplicationBase(ApplicationConfiguration config)
			: base(config)
		{
			this.UserTokenCacheInternal = config.UserTokenCacheInternalForTest ?? new TokenCache(base.ServiceBundle, false, config.UserTokenLegacyCachePersistenceForTest);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0003B00C File Offset: 0x0003920C
		public Task<IEnumerable<IAccount>> GetAccountsAsync()
		{
			return this.GetAccountsAsync(default(CancellationToken));
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0003B028 File Offset: 0x00039228
		public Task<IEnumerable<IAccount>> GetAccountsAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetAccountsInternalAsync(ApiEvent.ApiIds.GetAccounts, null, cancellationToken);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0003B038 File Offset: 0x00039238
		public Task<IEnumerable<IAccount>> GetAccountsAsync(string userFlow)
		{
			return this.GetAccountsAsync(userFlow, default(CancellationToken));
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0003B058 File Offset: 0x00039258
		public async Task<IEnumerable<IAccount>> GetAccountsAsync(string userFlow, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (string.IsNullOrWhiteSpace(userFlow))
			{
				throw new ArgumentException("userFlow should not be null or whitespace", "userFlow");
			}
			return (await this.GetAccountsInternalAsync(ApiEvent.ApiIds.GetAccountsByUserFlow, null, cancellationToken).ConfigureAwait(false)).Where((IAccount acc) => acc.HomeAccountId.ObjectId.EndsWith(userFlow, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0003B0AC File Offset: 0x000392AC
		public async Task<IAccount> GetAccountAsync(string accountId, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (await this.GetAccountsInternalAsync(ApiEvent.ApiIds.GetAccountById, accountId, cancellationToken).ConfigureAwait(false)).SingleOrDefault<IAccount>();
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0003B100 File Offset: 0x00039300
		public async Task<IAccount> GetAccountAsync(string accountId)
		{
			IAccount account;
			if (!string.IsNullOrWhiteSpace(accountId))
			{
				account = await this.GetAccountAsync(accountId, default(CancellationToken)).ConfigureAwait(false);
			}
			else
			{
				account = null;
			}
			return account;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0003B14C File Offset: 0x0003934C
		public Task RemoveAsync(IAccount account)
		{
			return this.RemoveAsync(account, default(CancellationToken));
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0003B16C File Offset: 0x0003936C
		public async Task RemoveAsync(IAccount account, CancellationToken cancellationToken = default(CancellationToken))
		{
			Guid guid = Guid.NewGuid();
			RequestContext requestContext = this.CreateRequestContext(guid, cancellationToken);
			requestContext.ApiEvent = new ApiEvent(guid);
			requestContext.ApiEvent.ApiId = ApiEvent.ApiIds.RemoveAccount;
			Authority authority2 = await Microsoft.Identity.Client.Instance.Authority.CreateAuthorityForRequestAsync(requestContext, null, null).ConfigureAwait(false);
			Authority authority = authority2;
			AuthenticationRequestParameters authenticationRequestParameters = new AuthenticationRequestParameters(base.ServiceBundle, this.UserTokenCacheInternal, new AcquireTokenCommonParameters
			{
				ApiId = requestContext.ApiEvent.ApiId
			}, requestContext, authority, null);
			if (account != null && this.UserTokenCacheInternal != null)
			{
				await this.UserTokenCacheInternal.RemoveAccountAsync(account, authenticationRequestParameters).ConfigureAwait(false);
			}
			if (this.AppConfig.IsBrokerEnabled && base.ServiceBundle.PlatformProxy.CanBrokerSupportSilentAuth())
			{
				cancellationToken.ThrowIfCancellationRequested();
				IBroker broker = base.ServiceBundle.PlatformProxy.CreateBroker(base.ServiceBundle.Config, null);
				if (broker.IsBrokerInstalledAndInvokable(authority.AuthorityInfo.AuthorityType))
				{
					await broker.RemoveAccountAsync(base.ServiceBundle.Config, account).ConfigureAwait(false);
				}
			}
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0003B1C0 File Offset: 0x000393C0
		private async Task<IEnumerable<IAccount>> GetAccountsInternalAsync(ApiEvent.ApiIds apiId, string homeAccountIdFilter, CancellationToken cancellationToken)
		{
			Guid guid = Guid.NewGuid();
			RequestContext requestContext = this.CreateRequestContext(guid, cancellationToken);
			requestContext.ApiEvent = new ApiEvent(guid);
			requestContext.ApiEvent.ApiId = apiId;
			Authority authority = await Microsoft.Identity.Client.Instance.Authority.CreateAuthorityForRequestAsync(requestContext, null, null).ConfigureAwait(false);
			AuthenticationRequestParameters authenticationRequestParameters = new AuthenticationRequestParameters(base.ServiceBundle, this.UserTokenCacheInternal, new AcquireTokenCommonParameters
			{
				ApiId = apiId
			}, requestContext, authority, homeAccountIdFilter);
			CacheSessionManager cacheSessionManager = new CacheSessionManager(this.UserTokenCacheInternal, authenticationRequestParameters);
			IEnumerable<IAccount> accountsFromCache = await cacheSessionManager.GetAccountsAsync().ConfigureAwait(false);
			IEnumerable<IAccount> accountsFromBroker = await this.GetAccountsFromBrokerAsync(homeAccountIdFilter, cacheSessionManager, cancellationToken).ConfigureAwait(false);
			if (accountsFromCache == null)
			{
				accountsFromCache = Enumerable.Empty<IAccount>();
			}
			if (accountsFromBroker == null)
			{
				accountsFromBroker = Enumerable.Empty<IAccount>();
			}
			base.ServiceBundle.ApplicationLogger.Info(() => string.Format("Found {0} cache accounts and {1} broker accounts", accountsFromCache.Count<IAccount>(), accountsFromBroker.Count<IAccount>()));
			IEnumerable<IAccount> cacheAndBrokerAccounts = this.MergeAccounts(accountsFromCache, accountsFromBroker);
			base.ServiceBundle.ApplicationLogger.Info(() => string.Format("Returning {0} accounts", cacheAndBrokerAccounts.Count<IAccount>()));
			return cacheAndBrokerAccounts;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0003B21C File Offset: 0x0003941C
		private async Task<IEnumerable<IAccount>> GetAccountsFromBrokerAsync(string homeAccountIdFilter, ICacheSessionManager cacheSessionManager, CancellationToken cancellationToken)
		{
			if (this.AppConfig.IsBrokerEnabled && base.ServiceBundle.PlatformProxy.CanBrokerSupportSilentAuth())
			{
				IBroker broker = base.ServiceBundle.PlatformProxy.CreateBroker(base.ServiceBundle.Config, null);
				if (broker.IsBrokerInstalledAndInvokable(base.ServiceBundle.Config.Authority.AuthorityInfo.AuthorityType))
				{
					IEnumerable<IAccount> enumerable = (await broker.GetAccountsAsync(this.AppConfig.ClientId, this.AppConfig.RedirectUri, this.AuthorityInfo, cacheSessionManager, base.ServiceBundle.InstanceDiscoveryManager).ConfigureAwait(false)) ?? Enumerable.Empty<IAccount>();
					if (!string.IsNullOrEmpty(homeAccountIdFilter))
					{
						enumerable = enumerable.Where((IAccount acc) => homeAccountIdFilter.Equals(acc.HomeAccountId.Identifier, StringComparison.OrdinalIgnoreCase));
					}
					return await this.FilterBrokerAccountsByEnvAsync(enumerable, cancellationToken).ConfigureAwait(false);
				}
			}
			return Enumerable.Empty<IAccount>();
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0003B278 File Offset: 0x00039478
		private async Task<IEnumerable<IAccount>> FilterBrokerAccountsByEnvAsync(IEnumerable<IAccount> brokerAccounts, CancellationToken cancellationToken)
		{
			base.ServiceBundle.ApplicationLogger.Verbose(() => "Filtering broker accounts by environment. Before filtering: " + brokerAccounts.Count<IAccount>().ToString());
			ISet<string> set = new HashSet<string>(brokerAccounts.Select((IAccount aci) => aci.Environment), StringComparer.OrdinalIgnoreCase);
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry = await base.ServiceBundle.InstanceDiscoveryManager.GetMetadataEntryTryAvoidNetworkAsync(this.AuthorityInfo, set, this.CreateRequestContext(Guid.NewGuid(), cancellationToken)).ConfigureAwait(false);
			InstanceDiscoveryMetadataEntry instanceMetadata = instanceDiscoveryMetadataEntry;
			brokerAccounts = brokerAccounts.Where((IAccount acc) => instanceMetadata.Aliases.ContainsOrdinalIgnoreCase(acc.Environment));
			base.ServiceBundle.ApplicationLogger.Verbose(() => "After filtering: " + brokerAccounts.Count<IAccount>().ToString());
			return brokerAccounts;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0003B2CC File Offset: 0x000394CC
		private IEnumerable<IAccount> MergeAccounts(IEnumerable<IAccount> cacheAccounts, IEnumerable<IAccount> brokerAccounts)
		{
			List<IAccount> list = new List<IAccount>(cacheAccounts);
			using (IEnumerator<IAccount> enumerator = brokerAccounts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IAccount account = enumerator.Current;
					if (!list.Any((IAccount x) => x.HomeAccountId.Equals(account.HomeAccountId)))
					{
						list.Add(account);
					}
					else
					{
						base.ServiceBundle.ApplicationLogger.InfoPii(delegate
						{
							string text = "Account merge eliminated broker account with ID: ";
							AccountId homeAccountId = account.HomeAccountId;
							return text + ((homeAccountId != null) ? homeAccountId.ToString() : null);
						}, () => "Account merge eliminated an account");
					}
				}
			}
			return list;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0003B380 File Offset: 0x00039580
		internal RequestContext CreateRequestContext(Guid correlationId, CancellationToken cancellationToken)
		{
			return new RequestContext(base.ServiceBundle, correlationId, cancellationToken);
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0003B38F File Offset: 0x0003958F
		public AcquireTokenSilentParameterBuilder AcquireTokenSilent(IEnumerable<string> scopes, IAccount account)
		{
			return AcquireTokenSilentParameterBuilder.Create(ClientExecutorFactory.CreateClientApplicationBaseExecutor(this), scopes, account);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0003B39E File Offset: 0x0003959E
		public AcquireTokenSilentParameterBuilder AcquireTokenSilent(IEnumerable<string> scopes, string loginHint)
		{
			if (string.IsNullOrWhiteSpace(loginHint))
			{
				throw new ArgumentNullException("loginHint");
			}
			return AcquireTokenSilentParameterBuilder.Create(ClientExecutorFactory.CreateClientApplicationBaseExecutor(this), scopes, loginHint);
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0003B3C0 File Offset: 0x000395C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[Obsolete("Use GetAccountsAsync instead (See https://aka.ms/msal-net-2-released)", true)]
		public IEnumerable<IUser> Users
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0003B3C7 File Offset: 0x000395C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use GetAccountAsync instead and pass IAccount.HomeAccountId.Identifier (See https://aka.ms/msal-net-2-released)", true)]
		public IUser GetUser(string identifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0003B3CE File Offset: 0x000395CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use RemoveAccountAsync instead (See https://aka.ms/msal-net-2-released)", true)]
		public void Remove(IUser user)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0003B3D5 File Offset: 0x000395D5
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x0003B3DD File Offset: 0x000395DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use WithComponent on AbstractApplicationBuilder<T> to configure this instead.See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public string Component { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0003B3E6 File Offset: 0x000395E6
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x0003B3EE File Offset: 0x000395EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use ExtraQueryParameters on each call instead.See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public string SliceParameters { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0003B3F7 File Offset: 0x000395F7
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x0003B3FF File Offset: 0x000395FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Can be set on AbstractApplicationBuilder<T>.WithAuthority as needed.See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		public bool ValidateAuthority { get; set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0003B408 File Offset: 0x00039608
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x0003B410 File Offset: 0x00039610
		[Obsolete("Should be set using AbstractApplicationBuilder<T>.WithRedirectUri and can be viewed with ClientApplicationBase.AppConfig.RedirectUri.See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string RedirectUri { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0003B419 File Offset: 0x00039619
		[Obsolete("Use AppConfig.ClientId instead.See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string ClientId
		{
			get
			{
				return this.AppConfig.ClientId;
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0003B426 File Offset: 0x00039626
		[Obsolete("Use AcquireTokenSilent instead.See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Task<AuthenticationResult> AcquireTokenSilentAsync(IEnumerable<string> scopes, IAccount account, string authority, bool forceRefresh)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0003B42D File Offset: 0x0003962D
		[Obsolete("Use AcquireTokenSilent instead.See https://aka.ms/msal-net-3-breaking-changes. ", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Task<AuthenticationResult> AcquireTokenSilentAsync(IEnumerable<string> scopes, IAccount account)
		{
			throw MigrationHelper.CreateMsalNet3BreakingChangesException();
		}
	}
}
