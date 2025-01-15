using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Internal.Requests
{
	// Token: 0x02000242 RID: 578
	internal class ClientCredentialRequest : RequestBase
	{
		// Token: 0x06001774 RID: 6004 RVA: 0x0004DAAA File Offset: 0x0004BCAA
		public ClientCredentialRequest(IServiceBundle serviceBundle, AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenForClientParameters clientParameters)
			: base(serviceBundle, authenticationRequestParameters, clientParameters)
		{
			this._clientParameters = clientParameters;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0004DABC File Offset: 0x0004BCBC
		protected override async Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			if (base.AuthenticationRequestParameters.Scope == null || base.AuthenticationRequestParameters.Scope.Count == 0)
			{
				throw new MsalClientException("scopes_required_client_credentials", "At least one scope needs to be requested for this authentication flow. ");
			}
			ILoggerAdapter logger = base.AuthenticationRequestParameters.RequestContext.Logger;
			AadAuthority aadAuthority = base.AuthenticationRequestParameters.Authority as AadAuthority;
			if (aadAuthority != null && aadAuthority.IsCommonOrOrganizationsTenant())
			{
				logger.Error("The current authority is targeting the /common or /organizations endpoint which is not recommended. See https://aka.ms/msal-net-client-credentials for more details.");
			}
			AuthenticationResult authenticationResult2;
			if (this._clientParameters.ForceRefresh || !string.IsNullOrEmpty(base.AuthenticationRequestParameters.Claims))
			{
				base.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo = CacheRefreshReason.ForceRefreshOrClaims;
				logger.Info("[ClientCredentialRequest] Skipped looking for a cached access token because ForceRefresh or Claims were set.");
				AuthenticationResult authenticationResult = await this.GetAccessTokenAsync(cancellationToken, logger).ConfigureAwait(false);
				authenticationResult2 = authenticationResult;
			}
			else
			{
				MsalAccessTokenCacheItem msalAccessTokenCacheItem = await this.GetCachedAccessTokenAsync().ConfigureAwait(false);
				AuthenticationResult authenticationResult;
				if (msalAccessTokenCacheItem != null)
				{
					authenticationResult = this.CreateAuthenticationResultFromCache(msalAccessTokenCacheItem);
					int num = 0;
					try
					{
						if (SilentRequestHelper.NeedsRefresh(msalAccessTokenCacheItem))
						{
							base.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo = CacheRefreshReason.ProactivelyRefreshed;
							SilentRequestHelper.ProcessFetchInBackground(msalAccessTokenCacheItem, delegate
							{
								Task<AuthenticationResult> accessTokenAsync;
								using (CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[] { cancellationToken }))
								{
									accessTokenAsync = this.GetAccessTokenAsync(cancellationTokenSource.Token, logger);
								}
								return accessTokenAsync;
							}, logger, base.ServiceBundle, base.AuthenticationRequestParameters.RequestContext.ApiEvent.ApiId);
						}
					}
					catch (MsalServiceException obj)
					{
						num = 1;
					}
					if (num == 1)
					{
						object obj;
						return await base.HandleTokenRefreshErrorAsync((MsalServiceException)obj, msalAccessTokenCacheItem).ConfigureAwait(false);
					}
				}
				else
				{
					if (base.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo != CacheRefreshReason.Expired)
					{
						base.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo = CacheRefreshReason.NoCachedAccessToken;
					}
					authenticationResult = await this.GetAccessTokenAsync(cancellationToken, logger).ConfigureAwait(false);
				}
				authenticationResult2 = authenticationResult;
			}
			return authenticationResult2;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0004DB08 File Offset: 0x0004BD08
		private async Task<AuthenticationResult> GetAccessTokenAsync(CancellationToken cancellationToken, ILoggerAdapter logger)
		{
			await base.ResolveAuthorityAsync().ConfigureAwait(false);
			AuthenticationResult authenticationResult;
			if (base.ServiceBundle.Config.AppTokenProvider == null)
			{
				authenticationResult = await base.CacheTokenResponseAndCreateAuthenticationResultAsync(await base.SendTokenRequestAsync(this.GetBodyParameters(), cancellationToken).ConfigureAwait(false)).ConfigureAwait(false);
			}
			else
			{
				logger.Verbose(() => "[ClientCredentialRequest] Entering client credential request semaphore.");
				await ClientCredentialRequest.s_semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);
				logger.Verbose(() => "[ClientCredentialRequest] Entered client credential request semaphore.");
				try
				{
					AuthenticationResult authenticationResult2;
					if (this._clientParameters.ForceRefresh || base.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo == CacheRefreshReason.ProactivelyRefreshed || !string.IsNullOrEmpty(base.AuthenticationRequestParameters.Claims))
					{
						authenticationResult2 = await this.SendTokenRequestToAppTokenProviderAsync(logger, cancellationToken).ConfigureAwait(false);
					}
					else
					{
						MsalAccessTokenCacheItem msalAccessTokenCacheItem = await this.GetCachedAccessTokenAsync().ConfigureAwait(false);
						if (msalAccessTokenCacheItem == null)
						{
							authenticationResult2 = await this.SendTokenRequestToAppTokenProviderAsync(logger, cancellationToken).ConfigureAwait(false);
						}
						else
						{
							logger.Verbose(() => "[ClientCredentialRequest] Checking for a cached access token.");
							authenticationResult2 = this.CreateAuthenticationResultFromCache(msalAccessTokenCacheItem);
						}
					}
					authenticationResult = authenticationResult2;
				}
				finally
				{
					ClientCredentialRequest.s_semaphoreSlim.Release();
					logger.Verbose(() => "[ClientCredentialRequest] Released client credential request semaphore.");
				}
			}
			return authenticationResult;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0004DB5C File Offset: 0x0004BD5C
		private async Task<AuthenticationResult> SendTokenRequestToAppTokenProviderAsync(ILoggerAdapter logger, CancellationToken cancellationToken)
		{
			logger.Info("[ClientCredentialRequest] Acquiring a token from the token provider.");
			AppTokenProviderParameters appTokenProviderParameters = new AppTokenProviderParameters
			{
				Scopes = this.GetOverriddenScopes(base.AuthenticationRequestParameters.Scope),
				CorrelationId = base.AuthenticationRequestParameters.RequestContext.CorrelationId.ToString(),
				Claims = base.AuthenticationRequestParameters.Claims,
				TenantId = base.AuthenticationRequestParameters.Authority.TenantId,
				CancellationToken = cancellationToken
			};
			MsalTokenResponse msalTokenResponse = MsalTokenResponse.CreateFromAppProviderResponse(await base.ServiceBundle.Config.AppTokenProvider(appTokenProviderParameters).ConfigureAwait(false));
			msalTokenResponse.Scope = appTokenProviderParameters.Scopes.AsSingleString();
			msalTokenResponse.CorrelationId = appTokenProviderParameters.CorrelationId;
			return await base.CacheTokenResponseAndCreateAuthenticationResultAsync(msalTokenResponse).ConfigureAwait(false);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0004DBB0 File Offset: 0x0004BDB0
		private async Task<MsalAccessTokenCacheItem> GetCachedAccessTokenAsync()
		{
			MsalAccessTokenCacheItem msalAccessTokenCacheItem = await base.CacheManager.FindAccessTokenAsync().ConfigureAwait(false);
			MsalAccessTokenCacheItem msalAccessTokenCacheItem2;
			if (msalAccessTokenCacheItem != null && !this._clientParameters.ForceRefresh)
			{
				base.AuthenticationRequestParameters.RequestContext.ApiEvent.IsAccessTokenCacheHit = true;
				Metrics.IncrementTotalAccessTokensFromCache();
				msalAccessTokenCacheItem2 = msalAccessTokenCacheItem;
			}
			else
			{
				msalAccessTokenCacheItem2 = null;
			}
			return msalAccessTokenCacheItem2;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0004DBF4 File Offset: 0x0004BDF4
		private AuthenticationResult CreateAuthenticationResultFromCache(MsalAccessTokenCacheItem cachedAccessTokenItem)
		{
			return new AuthenticationResult(cachedAccessTokenItem, null, base.AuthenticationRequestParameters.AuthenticationScheme, base.AuthenticationRequestParameters.RequestContext.CorrelationId, TokenSource.Cache, base.AuthenticationRequestParameters.RequestContext.ApiEvent, null, null, null);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0004DC37 File Offset: 0x0004BE37
		protected override SortedSet<string> GetOverriddenScopes(ISet<string> inputScopes)
		{
			return new SortedSet<string>(inputScopes);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0004DC3F File Offset: 0x0004BE3F
		private Dictionary<string, string> GetBodyParameters()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["grant_type"] = "client_credentials";
			dictionary["scope"] = base.AuthenticationRequestParameters.Scope.AsSingleString();
			return dictionary;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0004DC74 File Offset: 0x0004BE74
		protected override KeyValuePair<string, string>? GetCcsHeader(IDictionary<string, string> additionalBodyParameters)
		{
			return null;
		}

		// Token: 0x04000A4A RID: 2634
		private readonly AcquireTokenForClientParameters _clientParameters;

		// Token: 0x04000A4B RID: 2635
		private static readonly SemaphoreSlim s_semaphoreSlim = new SemaphoreSlim(1, 1);
	}
}
