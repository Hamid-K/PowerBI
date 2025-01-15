using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.OAuth2;

namespace Microsoft.Identity.Client.Internal.Requests.Silent
{
	// Token: 0x0200024D RID: 589
	internal class CacheSilentStrategy : ISilentAuthRequestStrategy
	{
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x0004F8A9 File Offset: 0x0004DAA9
		private AuthenticationRequestParameters AuthenticationRequestParameters { get; }

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x0004F8B1 File Offset: 0x0004DAB1
		private ICacheSessionManager CacheManager
		{
			get
			{
				return this.AuthenticationRequestParameters.CacheSessionManager;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x0004F8BE File Offset: 0x0004DABE
		protected IServiceBundle ServiceBundle { get; }

		// Token: 0x060017D0 RID: 6096 RVA: 0x0004F8C6 File Offset: 0x0004DAC6
		public CacheSilentStrategy(SilentRequest request, IServiceBundle serviceBundle, AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenSilentParameters silentParameters)
		{
			this.AuthenticationRequestParameters = authenticationRequestParameters;
			this._silentParameters = silentParameters;
			this.ServiceBundle = serviceBundle;
			this._silentRequest = request;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0004F8EC File Offset: 0x0004DAEC
		public async Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			CacheSilentStrategy.<>c__DisplayClass12_0 CS$<>8__locals1 = new CacheSilentStrategy.<>c__DisplayClass12_0();
			CS$<>8__locals1.cancellationToken = cancellationToken;
			CS$<>8__locals1.<>4__this = this;
			ILoggerAdapter logger = this.AuthenticationRequestParameters.RequestContext.Logger;
			MsalAccessTokenCacheItem cachedAccessTokenItem = null;
			CacheRefreshReason cacheInfoTelemetry = CacheRefreshReason.NotApplicable;
			this.ThrowIfCurrentBrokerAccount();
			AuthenticationResult authResult = null;
			if (!this._silentParameters.ForceRefresh && string.IsNullOrEmpty(this.AuthenticationRequestParameters.Claims))
			{
				MsalAccessTokenCacheItem msalAccessTokenCacheItem = await this.CacheManager.FindAccessTokenAsync().ConfigureAwait(false);
				cachedAccessTokenItem = msalAccessTokenCacheItem;
				if (cachedAccessTokenItem != null)
				{
					logger.Info("Returning access token found in cache. RefreshOn exists ? " + (cachedAccessTokenItem.RefreshOn != null).ToString());
					this.AuthenticationRequestParameters.RequestContext.ApiEvent.IsAccessTokenCacheHit = true;
					Metrics.IncrementTotalAccessTokensFromCache();
					authResult = await this.CreateAuthenticationResultAsync(cachedAccessTokenItem).ConfigureAwait(false);
				}
				else if (this.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo != CacheRefreshReason.Expired)
				{
					cacheInfoTelemetry = CacheRefreshReason.NoCachedAccessToken;
				}
			}
			else
			{
				cacheInfoTelemetry = CacheRefreshReason.ForceRefreshOrClaims;
				logger.Info("Skipped looking for an Access Token because ForceRefresh or Claims were set. ");
			}
			if (this.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo == CacheRefreshReason.NotApplicable)
			{
				this.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo = cacheInfoTelemetry;
			}
			int num = 0;
			try
			{
				if (cachedAccessTokenItem == null)
				{
					authResult = await this.RefreshRtOrFailAsync(CS$<>8__locals1.cancellationToken).ConfigureAwait(false);
				}
				else if (SilentRequestHelper.NeedsRefresh(cachedAccessTokenItem))
				{
					this.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo = CacheRefreshReason.ProactivelyRefreshed;
					SilentRequestHelper.ProcessFetchInBackground(cachedAccessTokenItem, delegate
					{
						Task<AuthenticationResult> task;
						using (CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[] { CS$<>8__locals1.cancellationToken }))
						{
							task = CS$<>8__locals1.<>4__this.RefreshRtOrFailAsync(cancellationTokenSource.Token);
						}
						return task;
					}, logger, this.ServiceBundle, this.AuthenticationRequestParameters.RequestContext.ApiEvent.ApiId);
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
				MsalServiceException ex = (MsalServiceException)obj;
				logger.Warning(string.Format("Refreshing the RT failed. Is the exception retryable? {0}. Is there an AT in the cache that is usable? {1} ", ex.IsRetryable, cachedAccessTokenItem != null));
				if (cachedAccessTokenItem != null && ex.IsRetryable)
				{
					logger.Info("Returning existing access token. It is not expired, but should be refreshed. ");
					return await this.CreateAuthenticationResultAsync(cachedAccessTokenItem).ConfigureAwait(false);
				}
				logger.Warning("Failed to refresh the RT and cannot use existing AT (expired or missing). ");
				Exception ex2 = obj as Exception;
				if (ex2 == null)
				{
					throw obj;
				}
				ExceptionDispatchInfo.Capture(ex2).Throw();
			}
			CS$<>8__locals1 = null;
			logger = null;
			cachedAccessTokenItem = null;
			authResult = null;
			AuthenticationResult authenticationResult;
			return authenticationResult;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0004F938 File Offset: 0x0004DB38
		private void ThrowIfCurrentBrokerAccount()
		{
			if (PublicClientApplication.IsOperatingSystemAccount(this.AuthenticationRequestParameters.Account))
			{
				this.AuthenticationRequestParameters.RequestContext.Logger.Verbose(() => "OperatingSystemAccount is only supported by some brokers");
				throw new MsalUiRequiredException("current_broker_account", "Only some brokers (WAM) can log in the current OS account. ", null, UiRequiredExceptionClassification.AcquireTokenSilentFailed);
			}
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0004F9A0 File Offset: 0x0004DBA0
		private async Task<AuthenticationResult> RefreshRtOrFailAsync(CancellationToken cancellationToken)
		{
			MsalTokenResponse msalTokenResponse = await this.TryGetTokenUsingFociAsync(cancellationToken).ConfigureAwait(false);
			if (msalTokenResponse == null)
			{
				msalTokenResponse = await SilentRequestHelper.RefreshAccessTokenAsync(await this.FindRefreshTokenOrFailAsync().ConfigureAwait(false), this._silentRequest, this.AuthenticationRequestParameters, cancellationToken).ConfigureAwait(false);
			}
			return await this._silentRequest.CacheTokenResponseAndCreateAuthenticationResultAsync(msalTokenResponse).ConfigureAwait(false);
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0004F9EC File Offset: 0x0004DBEC
		private async Task<AuthenticationResult> CreateAuthenticationResultAsync(MsalAccessTokenCacheItem cachedAccessTokenItem)
		{
			MsalIdTokenCacheItem msalIdTokenCacheItem = await this.CacheManager.GetIdTokenCacheItemAsync(cachedAccessTokenItem).ConfigureAwait(false);
			MsalIdTokenCacheItem msalIdTokenItem = msalIdTokenCacheItem;
			Account account = await this.CacheManager.GetAccountAssociatedWithAccessTokenAsync(cachedAccessTokenItem).ConfigureAwait(false);
			return new AuthenticationResult(cachedAccessTokenItem, msalIdTokenItem, this.AuthenticationRequestParameters.AuthenticationScheme, this.AuthenticationRequestParameters.RequestContext.CorrelationId, TokenSource.Cache, this.AuthenticationRequestParameters.RequestContext.ApiEvent, account, null, null);
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0004FA38 File Offset: 0x0004DC38
		private async Task<MsalTokenResponse> TryGetTokenUsingFociAsync(CancellationToken cancellationToken)
		{
			MsalTokenResponse msalTokenResponse;
			if (!this.ServiceBundle.PlatformProxy.GetFeatureFlags().IsFociEnabled)
			{
				msalTokenResponse = null;
			}
			else
			{
				ILoggerAdapter logger = this.AuthenticationRequestParameters.RequestContext.Logger;
				bool? flag = await this.CacheManager.IsAppFociMemberAsync("1").ConfigureAwait(false);
				if (flag != null && !flag.Value)
				{
					this.AuthenticationRequestParameters.RequestContext.Logger.Verbose(() => "[FOCI] App is not part of the family, skipping FOCI. ");
					msalTokenResponse = null;
				}
				else
				{
					logger.Verbose(() => "[FOCI] App is part of the family or unknown, looking for FRT. ");
					MsalRefreshTokenCacheItem familyRefreshToken = await this.CacheManager.FindFamilyRefreshTokenAsync("1").ConfigureAwait(false);
					logger.Verbose(() => "[FOCI] FRT found? " + (familyRefreshToken != null).ToString());
					if (familyRefreshToken != null)
					{
						try
						{
							MsalTokenResponse msalTokenResponse2 = await SilentRequestHelper.RefreshAccessTokenAsync(familyRefreshToken, this._silentRequest, this.AuthenticationRequestParameters, cancellationToken).ConfigureAwait(false);
							logger.Verbose(() => "[FOCI] FRT refresh succeeded. ");
							return msalTokenResponse2;
						}
						catch (MsalServiceException ex)
						{
							if ("invalid_grant".Equals((ex != null) ? ex.ErrorCode : null, StringComparison.OrdinalIgnoreCase) && "client_mismatch".Equals((ex != null) ? ex.SubError : null, StringComparison.OrdinalIgnoreCase))
							{
								logger.Error("[FOCI] FRT refresh failed - client mismatch. ");
								return null;
							}
							logger.Error("[FOCI] FRT refresh failed - other error. ");
							throw;
						}
					}
					msalTokenResponse = null;
				}
			}
			return msalTokenResponse;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0004FA84 File Offset: 0x0004DC84
		private async Task<MsalRefreshTokenCacheItem> FindRefreshTokenOrFailAsync()
		{
			object obj = await this.CacheManager.FindRefreshTokenAsync().ConfigureAwait(false);
			if (obj == null)
			{
				this.AuthenticationRequestParameters.RequestContext.Logger.Verbose(() => "No Refresh Token was found in the cache. ");
				throw new MsalUiRequiredException("no_tokens_found", "No Refresh Token found in the cache. ", null, UiRequiredExceptionClassification.AcquireTokenSilentFailed);
			}
			return obj;
		}

		// Token: 0x04000A70 RID: 2672
		private readonly AcquireTokenSilentParameters _silentParameters;

		// Token: 0x04000A71 RID: 2673
		private const string TheOnlyFamilyId = "1";

		// Token: 0x04000A72 RID: 2674
		private readonly SilentRequest _silentRequest;
	}
}
