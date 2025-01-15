using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.OAuth2;

namespace Microsoft.Identity.Client.Internal.Requests.Silent
{
	// Token: 0x0200024F RID: 591
	internal class SilentRequest : RequestBase
	{
		// Token: 0x060017D8 RID: 6104 RVA: 0x0004FAC8 File Offset: 0x0004DCC8
		public SilentRequest(IServiceBundle serviceBundle, AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenSilentParameters silentParameters, ISilentAuthRequestStrategy clientStrategyOverride = null, ISilentAuthRequestStrategy brokerStrategyOverride = null)
			: base(serviceBundle, authenticationRequestParameters, silentParameters)
		{
			SilentRequest <>4__this = this;
			this._silentParameters = silentParameters;
			this._brokerStrategyLazy = new Lazy<ISilentAuthRequestStrategy>(() => brokerStrategyOverride ?? new BrokerSilentStrategy(<>4__this, serviceBundle, authenticationRequestParameters, silentParameters, serviceBundle.PlatformProxy.CreateBroker(serviceBundle.Config, null)));
			this._clientStrategy = clientStrategyOverride ?? new CacheSilentStrategy(this, serviceBundle, authenticationRequestParameters, silentParameters);
			this._logger = authenticationRequestParameters.RequestContext.Logger;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0004FB74 File Offset: 0x0004DD74
		protected override async Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			await this.UpdateRequestWithAccountAsync().ConfigureAwait(false);
			bool isBrokerConfigured = base.AuthenticationRequestParameters.AppConfig.IsBrokerEnabled && base.ServiceBundle.PlatformProxy.CanBrokerSupportSilentAuth();
			AuthenticationResult authenticationResult2;
			try
			{
				if (base.AuthenticationRequestParameters.Account == null)
				{
					this._logger.Verbose(() => "No account passed to AcquireTokenSilent. ");
					throw new MsalUiRequiredException("user_null", "No account or login hint was passed to the AcquireTokenSilent call. ", null, UiRequiredExceptionClassification.AcquireTokenSilentFailed);
				}
				if (isBrokerConfigured)
				{
					this._logger.Info("Broker is configured and enabled, attempting to use broker instead.");
					AuthenticationResult authenticationResult = await this._brokerStrategyLazy.Value.ExecuteAsync(cancellationToken).ConfigureAwait(false);
					if (authenticationResult != null)
					{
						this._logger.Verbose(() => "Broker responded to silent request.");
						return authenticationResult;
					}
				}
				this._logger.Verbose(() => "Attempting to acquire token using local cache.");
				authenticationResult2 = await this._clientStrategy.ExecuteAsync(cancellationToken).ConfigureAwait(false);
			}
			catch (MsalException ex)
			{
				this._logger.Verbose(delegate
				{
					if (!isBrokerConfigured)
					{
						return "Token cache could not satisfy silent request.";
					}
					return "Broker could not satisfy silent request.";
				});
				throw ex;
			}
			return authenticationResult2;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0004FBBF File Offset: 0x0004DDBF
		internal new Task<AuthenticationResult> CacheTokenResponseAndCreateAuthenticationResultAsync(MsalTokenResponse response)
		{
			return base.CacheTokenResponseAndCreateAuthenticationResultAsync(response);
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0004FBC8 File Offset: 0x0004DDC8
		internal Task<AuthenticationResult> ExecuteTestAsync(CancellationToken cancellationToken)
		{
			return this.ExecuteAsync(cancellationToken);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0004FBD4 File Offset: 0x0004DDD4
		private async Task UpdateRequestWithAccountAsync()
		{
			IAccount account = await this.GetAccountFromParamsOrLoginHintAsync(this._silentParameters.Account, this._silentParameters.LoginHint).ConfigureAwait(false);
			base.AuthenticationRequestParameters.Account = account;
			Authority authority = await Authority.CreateAuthorityForRequestAsync(base.AuthenticationRequestParameters.RequestContext, base.AuthenticationRequestParameters.AuthorityOverride, account).ConfigureAwait(false);
			base.AuthenticationRequestParameters.AuthorityManager = new AuthorityManager(base.AuthenticationRequestParameters.RequestContext, authority);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0004FC18 File Offset: 0x0004DE18
		private async Task<IAccount> GetSingleAccountForLoginHintAsync(string loginHint)
		{
			IAccount account;
			if (!string.IsNullOrEmpty(loginHint))
			{
				List<IAccount> list = (await base.CacheManager.GetAccountsAsync().ConfigureAwait(false)).Where((IAccount a) => !string.IsNullOrWhiteSpace(a.Username) && a.Username.Equals(loginHint, StringComparison.OrdinalIgnoreCase)).ToList<IAccount>();
				if (((IReadOnlyCollection<IAccount>)list).Count == 0)
				{
					throw new MsalUiRequiredException("no_account_for_login_hint", "You are trying to acquire a token silently using a login hint. No account was found in the token cache having this login hint. ", null, UiRequiredExceptionClassification.AcquireTokenSilentFailed);
				}
				if (((IReadOnlyCollection<IAccount>)list).Count > 1)
				{
					throw new MsalUiRequiredException("multiple_accounts_for_login_hint", "You are trying to acquire a token silently using a login hint. Multiple accounts were found in the token cache having this login hint. Please choose an account manually an pass it in to AcquireTokenSilently. ", null, UiRequiredExceptionClassification.AcquireTokenSilentFailed);
				}
				account = ((IReadOnlyList<IAccount>)list)[0];
			}
			else
			{
				account = null;
			}
			return account;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0004FC64 File Offset: 0x0004DE64
		private async Task<IAccount> GetAccountFromParamsOrLoginHintAsync(IAccount account, string loginHint)
		{
			IAccount account2;
			if (account != null)
			{
				account2 = account;
			}
			else
			{
				account2 = await this.GetSingleAccountForLoginHintAsync(loginHint).ConfigureAwait(false);
			}
			return account2;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
		protected override void ValidateAccountIdentifiers(ClientInfo fromServer)
		{
			if (fromServer != null)
			{
				AuthenticationRequestParameters authenticationRequestParameters = base.AuthenticationRequestParameters;
				bool flag;
				if (authenticationRequestParameters == null)
				{
					flag = null != null;
				}
				else
				{
					IAccount account = authenticationRequestParameters.Account;
					flag = ((account != null) ? account.HomeAccountId : null) != null;
				}
				if (flag)
				{
					AuthenticationRequestParameters authenticationRequestParameters2 = base.AuthenticationRequestParameters;
					if (!PublicClientApplication.IsOperatingSystemAccount((authenticationRequestParameters2 != null) ? authenticationRequestParameters2.Account : null))
					{
						if (base.AuthenticationRequestParameters.AuthorityInfo.AuthorityType == AuthorityType.B2C && fromServer.UniqueTenantIdentifier.Equals(base.AuthenticationRequestParameters.Account.HomeAccountId.TenantId, StringComparison.OrdinalIgnoreCase))
						{
							return;
						}
						if (fromServer.UniqueObjectIdentifier.Equals(base.AuthenticationRequestParameters.Account.HomeAccountId.ObjectId, StringComparison.OrdinalIgnoreCase) && fromServer.UniqueTenantIdentifier.Equals(base.AuthenticationRequestParameters.Account.HomeAccountId.TenantId, StringComparison.OrdinalIgnoreCase))
						{
							return;
						}
						base.AuthenticationRequestParameters.RequestContext.Logger.Error("Returned user identifiers do not match the sent user identifier");
						base.AuthenticationRequestParameters.RequestContext.Logger.ErrorPii(string.Concat(new string[]
						{
							"User identifier returned by AAD (uid:",
							fromServer.UniqueObjectIdentifier,
							" utid:",
							fromServer.UniqueTenantIdentifier,
							") does not match the user identifier sent. (uid:",
							base.AuthenticationRequestParameters.Account.HomeAccountId.ObjectId,
							" utid:",
							base.AuthenticationRequestParameters.Account.HomeAccountId.TenantId,
							")"
						}), string.Empty);
						throw new MsalClientException("user_mismatch", "Returned user identifier does not match the sent user identifier when saving the token to the cache. ");
					}
				}
			}
		}

		// Token: 0x04000A73 RID: 2675
		private readonly AcquireTokenSilentParameters _silentParameters;

		// Token: 0x04000A74 RID: 2676
		private readonly ISilentAuthRequestStrategy _clientStrategy;

		// Token: 0x04000A75 RID: 2677
		private readonly Lazy<ISilentAuthRequestStrategy> _brokerStrategyLazy;

		// Token: 0x04000A76 RID: 2678
		private readonly ILoggerAdapter _logger;
	}
}
