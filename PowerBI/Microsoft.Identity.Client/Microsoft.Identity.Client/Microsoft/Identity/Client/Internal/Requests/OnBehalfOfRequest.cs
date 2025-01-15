using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Internal.Logger;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Internal.Requests
{
	// Token: 0x02000249 RID: 585
	internal class OnBehalfOfRequest : RequestBase
	{
		// Token: 0x0600179D RID: 6045 RVA: 0x0004E53C File Offset: 0x0004C73C
		public OnBehalfOfRequest(IServiceBundle serviceBundle, AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenOnBehalfOfParameters onBehalfOfParameters)
			: base(serviceBundle, authenticationRequestParameters, onBehalfOfParameters)
		{
			this._onBehalfOfParameters = onBehalfOfParameters;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0004E550 File Offset: 0x0004C750
		protected override async Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			OnBehalfOfRequest.<>c__DisplayClass3_0 CS$<>8__locals1 = new OnBehalfOfRequest.<>c__DisplayClass3_0();
			CS$<>8__locals1.cancellationToken = cancellationToken;
			CS$<>8__locals1.<>4__this = this;
			if (base.AuthenticationRequestParameters.Scope == null || base.AuthenticationRequestParameters.Scope.Count == 0)
			{
				throw new MsalClientException("scopes_required_client_credentials", "At least one scope needs to be requested for this authentication flow. ");
			}
			await base.ResolveAuthorityAsync().ConfigureAwait(false);
			MsalAccessTokenCacheItem cachedAccessToken = null;
			ILoggerAdapter logger = base.AuthenticationRequestParameters.RequestContext.Logger;
			AuthenticationResult authResult = null;
			AadAuthority aadAuthority = base.AuthenticationRequestParameters.Authority as AadAuthority;
			if (aadAuthority != null && aadAuthority.IsCommonOrOrganizationsTenant())
			{
				logger.Error("The current authority is targeting the /common or /organizations endpoint. Instead, it should target the same tenant as the client, which can be found in the 'tid' claim of the incoming client token. See https://aka.ms/msal-net-on-behalf-of for more details.");
			}
			CacheRefreshReason cacheInfoTelemetry = CacheRefreshReason.NotApplicable;
			AuthenticationResult authenticationResult;
			if (base.AuthenticationRequestParameters.ApiId == ApiEvent.ApiIds.InitiateLongRunningObo && !this._onBehalfOfParameters.SearchInCacheForLongRunningObo)
			{
				logger.Info("[OBO Request] Initiating long running process. Fetching OBO token from ESTS.");
				authenticationResult = await this.FetchNewAccessTokenAsync(CS$<>8__locals1.cancellationToken).ConfigureAwait(false);
			}
			else
			{
				if (!this._onBehalfOfParameters.ForceRefresh && string.IsNullOrEmpty(base.AuthenticationRequestParameters.Claims))
				{
					using (logger.LogBlockDuration("[OBO Request] Looking in the cache for an access token", LogLevel.Verbose))
					{
						cachedAccessToken = await base.CacheManager.FindAccessTokenAsync().ConfigureAwait(false);
					}
					DurationLogHelper durationLogHelper = null;
					if (cachedAccessToken != null)
					{
						OnBehalfOfRequest.<>c__DisplayClass3_1 CS$<>8__locals2 = new OnBehalfOfRequest.<>c__DisplayClass3_1();
						CS$<>8__locals2.cachedIdToken = await base.CacheManager.GetIdTokenCacheItemAsync(cachedAccessToken).ConfigureAwait(false);
						Account account = await base.CacheManager.GetAccountAssociatedWithAccessTokenAsync(cachedAccessToken).ConfigureAwait(false);
						logger.Info(() => "[OBO Request] Found a valid access token in the cache. ID token also found? " + (CS$<>8__locals2.cachedIdToken != null).ToString());
						base.AuthenticationRequestParameters.RequestContext.ApiEvent.IsAccessTokenCacheHit = true;
						Metrics.IncrementTotalAccessTokensFromCache();
						authResult = new AuthenticationResult(cachedAccessToken, CS$<>8__locals2.cachedIdToken, base.AuthenticationRequestParameters.AuthenticationScheme, base.AuthenticationRequestParameters.RequestContext.CorrelationId, TokenSource.Cache, base.AuthenticationRequestParameters.RequestContext.ApiEvent, account, null, null);
						CS$<>8__locals2 = null;
					}
					else if (base.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo != CacheRefreshReason.Expired)
					{
						cacheInfoTelemetry = CacheRefreshReason.NoCachedAccessToken;
					}
				}
				else
				{
					logger.Info("[OBO Request] Skipped looking for an Access Token in the cache because ForceRefresh or Claims were set. ");
					cacheInfoTelemetry = CacheRefreshReason.ForceRefreshOrClaims;
				}
				if (base.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo == CacheRefreshReason.NotApplicable)
				{
					base.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo = cacheInfoTelemetry;
				}
				int num = 0;
				try
				{
					if (cachedAccessToken == null)
					{
						authResult = await this.RefreshRtOrFetchNewAccessTokenAsync(CS$<>8__locals1.cancellationToken).ConfigureAwait(false);
					}
					else if (SilentRequestHelper.NeedsRefresh(cachedAccessToken))
					{
						base.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo = CacheRefreshReason.ProactivelyRefreshed;
						SilentRequestHelper.ProcessFetchInBackground(cachedAccessToken, delegate
						{
							Task<AuthenticationResult> task;
							using (CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[] { CS$<>8__locals1.cancellationToken }))
							{
								task = CS$<>8__locals1.<>4__this.RefreshRtOrFetchNewAccessTokenAsync(cancellationTokenSource.Token);
							}
							return task;
						}, logger, base.ServiceBundle, base.AuthenticationRequestParameters.RequestContext.ApiEvent.ApiId);
					}
					return authResult;
				}
				catch (MsalServiceException obj)
				{
					num = 1;
				}
				if (num == 1)
				{
					object obj;
					authenticationResult = await base.HandleTokenRefreshErrorAsync((MsalServiceException)obj, cachedAccessToken).ConfigureAwait(false);
				}
				else
				{
					CS$<>8__locals1 = null;
					cachedAccessToken = null;
					logger = null;
					authResult = null;
				}
			}
			return authenticationResult;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0004E59C File Offset: 0x0004C79C
		private async Task<AuthenticationResult> RefreshRtOrFetchNewAccessTokenAsync(CancellationToken cancellationToken)
		{
			ILoggerAdapter logger = base.AuthenticationRequestParameters.RequestContext.Logger;
			if (ApiEvent.IsLongRunningObo(base.AuthenticationRequestParameters.ApiId))
			{
				base.AuthenticationRequestParameters.RequestContext.Logger.Info("[OBO request] Long-running OBO flow, trying to refresh using a refresh token flow.");
				MsalRefreshTokenCacheItem msalRefreshTokenCacheItem = await base.CacheManager.FindRefreshTokenAsync().ConfigureAwait(false);
				if (msalRefreshTokenCacheItem != null)
				{
					logger.Info("[OBO request] Found a refresh token");
					if (!string.IsNullOrEmpty(msalRefreshTokenCacheItem.RawClientInfo))
					{
						ClientInfo clientInfo = ClientInfo.CreateFromJson(msalRefreshTokenCacheItem.RawClientInfo);
						this._ccsRoutingHint = CoreHelpers.GetCcsClientInfoHint(clientInfo.UniqueObjectIdentifier, clientInfo.UniqueTenantIdentifier);
					}
					else
					{
						logger.Info("[OBO request] No client info associated with RT. This is OBO for a Service Principal.");
					}
					return await base.CacheTokenResponseAndCreateAuthenticationResultAsync(await SilentRequestHelper.RefreshAccessTokenAsync(msalRefreshTokenCacheItem, this, base.AuthenticationRequestParameters, cancellationToken).ConfigureAwait(false)).ConfigureAwait(false);
				}
				if (base.AuthenticationRequestParameters.ApiId == ApiEvent.ApiIds.AcquireTokenInLongRunningObo)
				{
					base.AuthenticationRequestParameters.RequestContext.Logger.Error("[OBO request] AcquireTokenInLongRunningProcess was called and no access or refresh tokens were found in the cache.");
					throw new MsalClientException("obo_cache_key_not_in_cache_error", "The token cache does not contain a token with an OBO cache key that matches the longRunningProcessSessionKey passed into ILongRunningWebApi.AcquireTokenInLongRunningProcess method. Call ILongRunningWebApi.InitiateLongRunningProcessInWebApi method with this longRunningProcessSessionKey first or call ILongRunningWebApi.AcquireTokenInLongRunningProcess method with an already used longRunningProcessSessionKey. See https://aka.ms/msal-net-long-running-obo .");
				}
				base.AuthenticationRequestParameters.RequestContext.Logger.Info("[OBO request] No refresh token was found in the cache. Fetching OBO tokens from ESTS.");
			}
			else
			{
				logger.Info("[OBO request] Fetching tokens via normal OBO flow.");
			}
			return await this.FetchNewAccessTokenAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0004E5E8 File Offset: 0x0004C7E8
		private async Task<AuthenticationResult> FetchNewAccessTokenAsync(CancellationToken cancellationToken)
		{
			MsalTokenResponse msalTokenResponse = await base.SendTokenRequestAsync(this.GetBodyParameters(), cancellationToken).ConfigureAwait(false);
			if (!ApiEvent.IsLongRunningObo(base.AuthenticationRequestParameters.ApiId))
			{
				msalTokenResponse.RefreshToken = null;
			}
			if (msalTokenResponse.ClientInfo == null && base.AuthenticationRequestParameters.AuthorityInfo.IsClientInfoSupported)
			{
				base.AuthenticationRequestParameters.RequestContext.Logger.Info("[OBO request] This is an on behalf of request for a service principal as no client info returned in the token response.");
			}
			return await base.CacheTokenResponseAndCreateAuthenticationResultAsync(msalTokenResponse).ConfigureAwait(false);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0004E634 File Offset: 0x0004C834
		private Dictionary<string, string> GetBodyParameters()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["client_info"] = "1";
			dictionary["grant_type"] = this._onBehalfOfParameters.UserAssertion.AssertionType;
			dictionary["assertion"] = this._onBehalfOfParameters.UserAssertion.Assertion;
			dictionary["requested_token_use"] = "on_behalf_of";
			return dictionary;
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0004E69C File Offset: 0x0004C89C
		protected override KeyValuePair<string, string>? GetCcsHeader(IDictionary<string, string> additionalBodyParameters)
		{
			if (string.IsNullOrEmpty(this._ccsRoutingHint))
			{
				return null;
			}
			return new KeyValuePair<string, string>?(new KeyValuePair<string, string>("x-anchormailbox", this._ccsRoutingHint));
		}

		// Token: 0x04000A60 RID: 2656
		private readonly AcquireTokenOnBehalfOfParameters _onBehalfOfParameters;

		// Token: 0x04000A61 RID: 2657
		private string _ccsRoutingHint;
	}
}
