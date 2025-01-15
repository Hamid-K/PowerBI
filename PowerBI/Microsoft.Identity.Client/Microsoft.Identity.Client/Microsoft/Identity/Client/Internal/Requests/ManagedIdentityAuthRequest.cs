using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.ManagedIdentity;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Internal.Requests
{
	// Token: 0x02000248 RID: 584
	internal class ManagedIdentityAuthRequest : RequestBase
	{
		// Token: 0x06001795 RID: 6037 RVA: 0x0004E387 File Offset: 0x0004C587
		public ManagedIdentityAuthRequest(IServiceBundle serviceBundle, AuthenticationRequestParameters authenticationRequestParameters, AcquireTokenForManagedIdentityParameters managedIdentityParameters)
			: base(serviceBundle, authenticationRequestParameters, managedIdentityParameters)
		{
			this._managedIdentityParameters = managedIdentityParameters;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0004E39C File Offset: 0x0004C59C
		protected override async Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken)
		{
			AuthenticationResult authenticationResult = null;
			ILoggerAdapter logger = base.AuthenticationRequestParameters.RequestContext.Logger;
			AuthenticationResult authenticationResult2;
			if (this._managedIdentityParameters.ForceRefresh)
			{
				base.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo = CacheRefreshReason.ForceRefreshOrClaims;
				logger.Info("[ManagedIdentityRequest] Skipped looking for a cached access token because ForceRefresh was set.");
				authenticationResult = await this.GetAccessTokenAsync(cancellationToken, logger).ConfigureAwait(false);
				authenticationResult2 = authenticationResult;
			}
			else
			{
				MsalAccessTokenCacheItem msalAccessTokenCacheItem = await this.GetCachedAccessTokenAsync().ConfigureAwait(false);
				if (msalAccessTokenCacheItem != null)
				{
					authenticationResult = this.CreateAuthenticationResultFromCache(msalAccessTokenCacheItem);
					logger.Info("[ManagedIdentityRequest] Access token retrieved from cache.");
					int num = 0;
					try
					{
						if (SilentRequestHelper.NeedsRefresh(msalAccessTokenCacheItem))
						{
							logger.Info("[ManagedIdentityRequest] Initiating a proactive refresh.");
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
					logger.Info("[ManagedIdentityRequest] No cached access token. Getting a token from the managed identity endpoint.");
					authenticationResult = await this.GetAccessTokenAsync(cancellationToken, logger).ConfigureAwait(false);
				}
				authenticationResult2 = authenticationResult;
			}
			return authenticationResult2;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0004E3E8 File Offset: 0x0004C5E8
		private async Task<AuthenticationResult> GetAccessTokenAsync(CancellationToken cancellationToken, ILoggerAdapter logger)
		{
			logger.Verbose(() => "[ManagedIdentityRequest] Entering managed identity request semaphore.");
			await ManagedIdentityAuthRequest.s_semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);
			logger.Verbose(() => "[ManagedIdentityRequest] Entered managed identity request semaphore.");
			AuthenticationResult authenticationResult2;
			try
			{
				AuthenticationResult authenticationResult;
				if (this._managedIdentityParameters.ForceRefresh || base.AuthenticationRequestParameters.RequestContext.ApiEvent.CacheInfo == CacheRefreshReason.ProactivelyRefreshed)
				{
					authenticationResult = await this.SendTokenRequestForManagedIdentityAsync(logger, cancellationToken).ConfigureAwait(false);
				}
				else
				{
					logger.Info("[ManagedIdentityRequest] Checking for a cached access token.");
					MsalAccessTokenCacheItem msalAccessTokenCacheItem = await this.GetCachedAccessTokenAsync().ConfigureAwait(false);
					if (msalAccessTokenCacheItem != null)
					{
						authenticationResult = this.CreateAuthenticationResultFromCache(msalAccessTokenCacheItem);
					}
					else
					{
						authenticationResult = await this.SendTokenRequestForManagedIdentityAsync(logger, cancellationToken).ConfigureAwait(false);
					}
				}
				authenticationResult2 = authenticationResult;
			}
			finally
			{
				ManagedIdentityAuthRequest.s_semaphoreSlim.Release();
				logger.Verbose(() => "[ManagedIdentityRequest] Released managed identity request semaphore.");
			}
			return authenticationResult2;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0004E43C File Offset: 0x0004C63C
		private async Task<AuthenticationResult> SendTokenRequestForManagedIdentityAsync(ILoggerAdapter logger, CancellationToken cancellationToken)
		{
			logger.Info("[ManagedIdentityRequest] Acquiring a token from the managed identity endpoint.");
			await base.ResolveAuthorityAsync().ConfigureAwait(false);
			MsalTokenResponse msalTokenResponse = MsalTokenResponse.CreateFromManagedIdentityResponse(await new ManagedIdentityClient(base.AuthenticationRequestParameters.RequestContext).SendTokenRequestForManagedIdentityAsync(this._managedIdentityParameters, cancellationToken).ConfigureAwait(false));
			msalTokenResponse.Scope = base.AuthenticationRequestParameters.Scope.AsSingleString();
			return await base.CacheTokenResponseAndCreateAuthenticationResultAsync(msalTokenResponse).ConfigureAwait(false);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0004E490 File Offset: 0x0004C690
		private async Task<MsalAccessTokenCacheItem> GetCachedAccessTokenAsync()
		{
			MsalAccessTokenCacheItem msalAccessTokenCacheItem = await base.CacheManager.FindAccessTokenAsync().ConfigureAwait(false);
			MsalAccessTokenCacheItem msalAccessTokenCacheItem2;
			if (msalAccessTokenCacheItem != null)
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

		// Token: 0x0600179A RID: 6042 RVA: 0x0004E4D4 File Offset: 0x0004C6D4
		private AuthenticationResult CreateAuthenticationResultFromCache(MsalAccessTokenCacheItem cachedAccessTokenItem)
		{
			return new AuthenticationResult(cachedAccessTokenItem, null, base.AuthenticationRequestParameters.AuthenticationScheme, base.AuthenticationRequestParameters.RequestContext.CorrelationId, TokenSource.Cache, base.AuthenticationRequestParameters.RequestContext.ApiEvent, null, null, null);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0004E518 File Offset: 0x0004C718
		protected override KeyValuePair<string, string>? GetCcsHeader(IDictionary<string, string> additionalBodyParameters)
		{
			return null;
		}

		// Token: 0x04000A5E RID: 2654
		private readonly AcquireTokenForManagedIdentityParameters _managedIdentityParameters;

		// Token: 0x04000A5F RID: 2655
		private static readonly SemaphoreSlim s_semaphoreSlim = new SemaphoreSlim(1, 1);
	}
}
