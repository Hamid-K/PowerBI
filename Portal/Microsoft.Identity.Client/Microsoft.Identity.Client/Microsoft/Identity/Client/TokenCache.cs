using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.PlatformsCommon.Factories;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000169 RID: 361
	public sealed class TokenCache : ITokenCacheInternal, ITokenCache, ITokenCacheSerializer
	{
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0003BF92 File Offset: 0x0003A192
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x0003BF9C File Offset: 0x0003A19C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use the equivalent flag TokenCacheNotificationArgs.HasStateChanged, which indicates if the operation triggering the notification is modifying the cache or not. Setting the flag is not required.")]
		public bool HasStateChanged
		{
			get
			{
				return this._hasStateChanged;
			}
			set
			{
				this._hasStateChanged = value;
			}
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0003BFA8 File Offset: 0x0003A1A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is expected to be removed in MSAL.NET v5. We recommend using SerializeMsalV3/DeserializeMsalV3. Read more: https://aka.ms/msal-net-3x-cache-breaking-change", false)]
		public CacheData SerializeUnifiedAndAdalCache()
		{
			this.Validate();
			this.ServiceBundle.ApplicationLogger.Info(() => "[ADAL Caching] Legacy SerializeUnifiedAndAdalCache being called. " + this._semaphoreSlim.GetCurrentCountLogMessage() + ".");
			this._semaphoreSlim.Wait();
			this.ServiceBundle.ApplicationLogger.Info("[ADAL Caching] Acquired semaphore");
			CacheData cacheData;
			try
			{
				byte[] array = this.Serialize();
				byte[] array2 = this.LegacyCachePersistence.LoadCache();
				cacheData = new CacheData
				{
					AdalV3State = array2,
					UnifiedState = array
				};
			}
			finally
			{
				this._semaphoreSlim.Release();
			}
			return cacheData;
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0003C040 File Offset: 0x0003A240
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is expected to be removed in MSAL.NET v5. We recommend using SerializeMsalV3/DeserializeMsalV3. Read more: https://aka.ms/msal-net-3x-cache-breaking-change", false)]
		public void DeserializeUnifiedAndAdalCache(CacheData cacheData)
		{
			this.Validate();
			this.ServiceBundle.ApplicationLogger.Info("[ADAL Caching] Legacy SerializeUnifiedAndAdalCache being called. Acquiring semaphore " + this._semaphoreSlim.GetCurrentCountLogMessage());
			this._semaphoreSlim.Wait();
			this.ServiceBundle.ApplicationLogger.Info("[ADAL Caching] Acquired semaphore");
			try
			{
				this.Deserialize(cacheData.UnifiedState);
				this.LegacyCachePersistence.WriteCache(cacheData.AdalV3State);
			}
			finally
			{
				this._semaphoreSlim.Release();
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0003C0D4 File Offset: 0x0003A2D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is expected to be removed in MSAL.NET v5. We recommend using SerializeMsalV3/DeserializeMsalV3. Read more: https://aka.ms/msal-net-3x-cache-breaking-change", false)]
		public byte[] Serialize()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0003C0DB File Offset: 0x0003A2DB
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is expected to be removed in MSAL.NET v5. We recommend using SerializeMsalV3/DeserializeMsalV3. Read more: https://aka.ms/msal-net-3x-cache-breaking-change", false)]
		public void Deserialize(byte[] msalV2State)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0003C0E2 File Offset: 0x0003A2E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is removed in MSAL.NET v4. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		public byte[] SerializeAdalV3()
		{
			throw new NotImplementedException("This is removed in MSAL.NET v4. Read more: https://aka.ms/msal-net-4x-cache-breaking-change");
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0003C0EE File Offset: 0x0003A2EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is removed in MSAL.NET v4. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		public void DeserializeAdalV3(byte[] adalV3State)
		{
			throw new NotImplementedException("This is removed in MSAL.NET v4. Read more: https://aka.ms/msal-net-4x-cache-breaking-change");
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0003C0FA File Offset: 0x0003A2FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is removed in MSAL.NET v4. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		public byte[] SerializeMsalV2()
		{
			throw new NotImplementedException("This is removed in MSAL.NET v4. Read more: https://aka.ms/msal-net-4x-cache-breaking-change");
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0003C106 File Offset: 0x0003A306
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is removed in MSAL.NET v4. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		public void DeserializeMsalV2(byte[] msalV2State)
		{
			throw new NotImplementedException("This is removed in MSAL.NET v4. Read more: https://aka.ms/msal-net-4x-cache-breaking-change");
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0003C112 File Offset: 0x0003A312
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is removed in MSAL.NET v4. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		public byte[] SerializeMsalV3()
		{
			throw new NotImplementedException("This is removed in MSAL.NET v4. Read more: https://aka.ms/msal-net-4x-cache-breaking-change");
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0003C11E File Offset: 0x0003A31E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This is removed in MSAL.NET v4. Read more: https://aka.ms/msal-net-4x-cache-breaking-change", false)]
		public void DeserializeMsalV3(byte[] msalV3State, bool shouldClearExistingCache)
		{
			throw new NotImplementedException("This is removed in MSAL.NET v4. Read more: https://aka.ms/msal-net-4x-cache-breaking-change");
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0003C12A File Offset: 0x0003A32A
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x0003C132 File Offset: 0x0003A332
		internal ITokenCacheAccessor Accessor { get; set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x0003C13B File Offset: 0x0003A33B
		internal IServiceBundle ServiceBundle { get; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x0003C143 File Offset: 0x0003A343
		internal ILegacyCachePersistence LegacyCachePersistence { get; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0003C14B File Offset: 0x0003A34B
		internal string ClientId
		{
			get
			{
				return this.ServiceBundle.Config.ClientId;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0003C15D File Offset: 0x0003A35D
		ITokenCacheAccessor ITokenCacheInternal.Accessor
		{
			get
			{
				return this.Accessor;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x0003C165 File Offset: 0x0003A365
		ILegacyCachePersistence ITokenCacheInternal.LegacyPersistence
		{
			get
			{
				return this.LegacyCachePersistence;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0003C16D File Offset: 0x0003A36D
		internal bool IsAppTokenCache { get; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0003C175 File Offset: 0x0003A375
		bool ITokenCacheInternal.IsApplicationCache
		{
			get
			{
				return this.IsAppTokenCache;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0003C17D File Offset: 0x0003A37D
		OptionalSemaphoreSlim ITokenCacheInternal.Semaphore
		{
			get
			{
				return this._semaphoreSlim;
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0003C185 File Offset: 0x0003A385
		[Obsolete("The recommended way to get a cache is by using IClientApplicationBase.UserTokenCache or IClientApplicationBase.AppTokenCache")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public TokenCache()
			: this(null, false, null)
		{
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0003C190 File Offset: 0x0003A390
		internal TokenCache(IServiceBundle serviceBundle, bool isApplicationTokenCache, ILegacyCachePersistence legacyCachePersistenceForTest = null)
		{
			if (serviceBundle == null)
			{
				throw new ArgumentNullException("serviceBundle");
			}
			this._semaphoreSlim = new OptionalSemaphoreSlim(serviceBundle.Config.CacheSynchronizationEnabled);
			IPlatformProxy platformProxy = ((serviceBundle != null) ? serviceBundle.PlatformProxy : null) ?? PlatformProxyFactory.CreatePlatformProxy(null);
			this.Accessor = platformProxy.CreateTokenCacheAccessor(serviceBundle.Config.AccessorOptions, isApplicationTokenCache);
			this._featureFlags = platformProxy.GetFeatureFlags();
			this.LegacyCachePersistence = legacyCachePersistenceForTest ?? platformProxy.CreateLegacyCachePersistence();
			this.IsAppTokenCache = isApplicationTokenCache;
			this.ServiceBundle = serviceBundle;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0003C221 File Offset: 0x0003A421
		public void SetIosKeychainSecurityGroup(string securityGroup)
		{
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0003C224 File Offset: 0x0003A424
		private void UpdateAppMetadata(string clientId, string environment, string familyId)
		{
			if (this._featureFlags.IsFociEnabled)
			{
				MsalAppMetadataCacheItem msalAppMetadataCacheItem = new MsalAppMetadataCacheItem(clientId, environment, familyId);
				this.Accessor.SaveAppMetadata(msalAppMetadataCacheItem);
			}
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0003C254 File Offset: 0x0003A454
		private void DeleteAccessTokensWithIntersectingScopes(AuthenticationRequestParameters requestParams, IEnumerable<string> environmentAliases, string tenantId, HashSet<string> scopeSet, string homeAccountId, string tokenType)
		{
			if (requestParams.RequestContext.Logger.IsLoggingEnabled(LogLevel.Info))
			{
				requestParams.RequestContext.Logger.Info(() => "Looking for scopes for the authority in the cache which intersect with " + requestParams.Scope.AsSingleString());
			}
			List<MsalAccessTokenCacheItem> accessTokensToDelete = new List<MsalAccessTokenCacheItem>();
			string internalPartitionKeyFromResponse = CacheKeyFactory.GetInternalPartitionKeyFromResponse(requestParams, homeAccountId);
			Func<string> <>9__4;
			foreach (MsalAccessTokenCacheItem msalAccessTokenCacheItem in this.Accessor.GetAllAccessTokens(internalPartitionKeyFromResponse, null))
			{
				if (msalAccessTokenCacheItem.ClientId.Equals(this.ClientId, StringComparison.OrdinalIgnoreCase) && environmentAliases.Contains(msalAccessTokenCacheItem.Environment) && string.Equals(msalAccessTokenCacheItem.TokenType ?? "", tokenType ?? "", StringComparison.OrdinalIgnoreCase) && string.Equals(msalAccessTokenCacheItem.TenantId, tenantId, StringComparison.OrdinalIgnoreCase) && msalAccessTokenCacheItem.ScopeSet.Overlaps(scopeSet))
				{
					ILoggerAdapter logger = requestParams.RequestContext.Logger;
					Func<string> func;
					if ((func = <>9__4) == null)
					{
						func = (<>9__4 = () => string.Format("Intersecting scopes found: {0}", scopeSet));
					}
					logger.Verbose(func);
					accessTokensToDelete.Add(msalAccessTokenCacheItem);
				}
			}
			requestParams.RequestContext.Logger.Info(() => "Intersecting scope entries count - " + accessTokensToDelete.Count.ToString());
			if (!requestParams.IsClientCredentialRequest && requestParams.ApiId != ApiEvent.ApiIds.AcquireTokenForSystemAssignedManagedIdentity && requestParams.ApiId != ApiEvent.ApiIds.AcquireTokenForUserAssignedManagedIdentity)
			{
				accessTokensToDelete.RemoveAll((MsalAccessTokenCacheItem item) => !item.HomeAccountId.Equals(homeAccountId, StringComparison.OrdinalIgnoreCase));
				requestParams.RequestContext.Logger.Info(() => "Matching entries after filtering by user - " + accessTokensToDelete.Count.ToString());
			}
			foreach (MsalAccessTokenCacheItem msalAccessTokenCacheItem2 in accessTokensToDelete)
			{
				this.Accessor.DeleteAccessToken(msalAccessTokenCacheItem2);
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0003C4A4 File Offset: 0x0003A6A4
		private static string GetAccessTokenExpireLogMessageContent(MsalAccessTokenCacheItem msalAccessTokenCacheItem)
		{
			return string.Format(CultureInfo.InvariantCulture, "[Current time ({0}) - Expiration Time ({1}) - Extended Expiration Time ({2})]", DateTime.UtcNow, msalAccessTokenCacheItem.ExpiresOn, msalAccessTokenCacheItem.ExtendedExpiresOn);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0003C4D8 File Offset: 0x0003A6D8
		private bool RtMatchesAccount(MsalRefreshTokenCacheItem rtItem, MsalAccountCacheItem account)
		{
			bool flag = rtItem.HomeAccountId.Equals(account.HomeAccountId, StringComparison.OrdinalIgnoreCase);
			bool flag2 = rtItem.IsFRT || rtItem.ClientId.Equals(this.ClientId, StringComparison.OrdinalIgnoreCase);
			return flag && flag2;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0003C517 File Offset: 0x0003A717
		private static bool FrtExists(IEnumerable<MsalRefreshTokenCacheItem> refreshTokens)
		{
			return refreshTokens.Any((MsalRefreshTokenCacheItem rt) => rt.IsFRT);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0003C540 File Offset: 0x0003A740
		async Task<Tuple<MsalAccessTokenCacheItem, MsalIdTokenCacheItem, Account>> ITokenCacheInternal.SaveTokenResponseAsync(AuthenticationRequestParameters requestParams, MsalTokenResponse response)
		{
			TokenCache.<>c__DisplayClass49_0 CS$<>8__locals1 = new TokenCache.<>c__DisplayClass49_0();
			CS$<>8__locals1.<>4__this = this;
			ILoggerAdapter logger = requestParams.RequestContext.Logger;
			response.Log(logger, LogLevel.Verbose);
			MsalAccessTokenCacheItem msalAccessTokenCacheItem = null;
			MsalRefreshTokenCacheItem msalRefreshTokenCacheItem = null;
			MsalIdTokenCacheItem msalIdTokenCacheItem = null;
			MsalAccountCacheItem msalAccountCacheItem = null;
			IdToken idToken = IdToken.Parse(response.IdToken);
			if (idToken == null)
			{
				logger.Info("[SaveTokenResponseAsync] ID Token not present in response. ");
			}
			string tenantId = TokenResponseHelper.GetTenantId(idToken, requestParams);
			string username = TokenResponseHelper.GetUsernameFromIdToken(idToken);
			string homeAccountId = TokenResponseHelper.GetHomeAccountId(requestParams, response, idToken);
			string suggestedWebCacheKey = CacheKeyFactory.GetExternalCacheKeyFromResponse(requestParams, homeAccountId);
			if (requestParams.AppConfig.MultiCloudSupportEnabled && !string.IsNullOrEmpty(response.AuthorityUrl))
			{
				Uri uri = new Uri(response.AuthorityUrl);
				requestParams.AuthorityManager = new AuthorityManager(requestParams.RequestContext, Authority.CreateAuthorityWithEnvironment(requestParams.Authority.AuthorityInfo, uri.Host));
			}
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry = await requestParams.AuthorityManager.GetInstanceDiscoveryEntryAsync().ConfigureAwait(false);
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadata = instanceDiscoveryMetadataEntry;
			if (!string.IsNullOrEmpty(response.AccessToken))
			{
				msalAccessTokenCacheItem = new MsalAccessTokenCacheItem(instanceDiscoveryMetadata.PreferredCache, requestParams.AppConfig.ClientId, response, tenantId, homeAccountId, requestParams.AuthenticationScheme.KeyId, CacheKeyFactory.GetOboKey(requestParams.LongRunningOboCacheKey, requestParams.UserAssertion));
			}
			if (!string.IsNullOrEmpty(response.RefreshToken))
			{
				msalRefreshTokenCacheItem = new MsalRefreshTokenCacheItem(instanceDiscoveryMetadata.PreferredCache, requestParams.AppConfig.ClientId, response, homeAccountId)
				{
					OboCacheKey = CacheKeyFactory.GetOboKey(requestParams.LongRunningOboCacheKey, requestParams.UserAssertion)
				};
				if (!this._featureFlags.IsFociEnabled)
				{
					msalRefreshTokenCacheItem.FamilyId = null;
				}
			}
			Account account = null;
			if (idToken != null)
			{
				msalIdTokenCacheItem = new MsalIdTokenCacheItem(instanceDiscoveryMetadata.PreferredCache, requestParams.AppConfig.ClientId, response, tenantId, homeAccountId);
				Dictionary<string, string> wamAccountIds = TokenResponseHelper.GetWamAccountIds(requestParams, response);
				msalAccountCacheItem = new MsalAccountCacheItem(instanceDiscoveryMetadata.PreferredCache, response.ClientInfo, homeAccountId, idToken, username, tenantId, wamAccountIds);
				IDictionary<string, TenantProfile> dictionary = null;
				if (msalIdTokenCacheItem.TenantId != null)
				{
					dictionary = await this.GetTenantProfilesAsync(requestParams, homeAccountId).ConfigureAwait(false);
					if (dictionary != null)
					{
						TenantProfile tenantProfile = new TenantProfile(msalIdTokenCacheItem);
						dictionary[msalIdTokenCacheItem.TenantId] = tenantProfile;
					}
				}
				account = new Account(homeAccountId, username, instanceDiscoveryMetadata.PreferredNetwork, wamAccountIds, (dictionary != null) ? dictionary.Values : null);
				wamAccountIds = null;
			}
			logger.Verbose(() => "[SaveTokenResponseAsync] Entering token cache semaphore. Count " + CS$<>8__locals1.<>4__this._semaphoreSlim.GetCurrentCountLogMessage() + ".");
			await this._semaphoreSlim.WaitAsync(requestParams.RequestContext.UserCancellationToken).ConfigureAwait(false);
			logger.Verbose(() => "[SaveTokenResponseAsync] Entered token cache semaphore. ");
			CS$<>8__locals1.tokenCacheInternal = this;
			Tuple<MsalAccessTokenCacheItem, MsalIdTokenCacheItem, Account> tuple;
			try
			{
				this.HasStateChanged = true;
				object obj = null;
				try
				{
					if (CS$<>8__locals1.tokenCacheInternal.IsAppSubscribedToSerializationEvents())
					{
						TokenCache.<>c__DisplayClass49_1 CS$<>8__locals2 = new TokenCache.<>c__DisplayClass49_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.args = new TokenCacheNotificationArgs(this, this.ClientId, account, true, CS$<>8__locals2.CS$<>8__locals1.tokenCacheInternal.IsApplicationCache, suggestedWebCacheKey, CS$<>8__locals2.CS$<>8__locals1.tokenCacheInternal.HasTokensNoLocks(), null, requestParams.RequestContext.UserCancellationToken, requestParams.RequestContext.CorrelationId, requestParams.Scope, requestParams.AuthorityManager.OriginalAuthority.TenantId, requestParams.RequestContext.Logger.IdentityLogger, requestParams.RequestContext.Logger.PiiLoggingEnabled, null);
						MeasureDurationResult measureDurationResult = await StopwatchService.MeasureCodeBlockAsync(delegate
						{
							TokenCache.<>c__DisplayClass49_1.<<Microsoft-Identity-Client-ITokenCacheInternal-SaveTokenResponseAsync>b__3>d <<Microsoft-Identity-Client-ITokenCacheInternal-SaveTokenResponseAsync>b__3>d;
							<<Microsoft-Identity-Client-ITokenCacheInternal-SaveTokenResponseAsync>b__3>d.<>t__builder = AsyncTaskMethodBuilder.Create();
							<<Microsoft-Identity-Client-ITokenCacheInternal-SaveTokenResponseAsync>b__3>d.<>4__this = CS$<>8__locals2;
							<<Microsoft-Identity-Client-ITokenCacheInternal-SaveTokenResponseAsync>b__3>d.<>1__state = -1;
							<<Microsoft-Identity-Client-ITokenCacheInternal-SaveTokenResponseAsync>b__3>d.<>t__builder.Start<TokenCache.<>c__DisplayClass49_1.<<Microsoft-Identity-Client-ITokenCacheInternal-SaveTokenResponseAsync>b__3>d>(ref <<Microsoft-Identity-Client-ITokenCacheInternal-SaveTokenResponseAsync>b__3>d);
							return <<Microsoft-Identity-Client-ITokenCacheInternal-SaveTokenResponseAsync>b__3>d.<>t__builder.Task;
						}).ConfigureAwait(false);
						requestParams.RequestContext.ApiEvent.DurationInCacheInMs += measureDurationResult.Milliseconds;
					}
					if (TokenCache.ShouldCacheAccessToken(msalAccessTokenCacheItem, response.TokenSource))
					{
						logger.Info("[SaveTokenResponseAsync] Saving AT in cache and removing overlapping ATs...");
						this.DeleteAccessTokensWithIntersectingScopes(requestParams, instanceDiscoveryMetadata.Aliases, tenantId, msalAccessTokenCacheItem.ScopeSet, msalAccessTokenCacheItem.HomeAccountId, msalAccessTokenCacheItem.TokenType);
						this.Accessor.SaveAccessToken(msalAccessTokenCacheItem);
					}
					if (idToken != null)
					{
						logger.Info("[SaveTokenResponseAsync] Saving Id Token and Account in cache ...");
						this.Accessor.SaveIdToken(msalIdTokenCacheItem);
						this.MergeWamAccountIds(msalAccountCacheItem);
						this.Accessor.SaveAccount(msalAccountCacheItem);
					}
					if (msalRefreshTokenCacheItem != null)
					{
						logger.Info("[SaveTokenResponseAsync] Saving RT in cache...");
						this.Accessor.SaveRefreshToken(msalRefreshTokenCacheItem);
					}
					this.UpdateAppMetadata(requestParams.AppConfig.ClientId, instanceDiscoveryMetadata.PreferredCache, response.FamilyId);
					this.SaveToLegacyAdalCache(requestParams, response, msalRefreshTokenCacheItem, msalIdTokenCacheItem, tenantId, instanceDiscoveryMetadata);
				}
				catch (object obj)
				{
				}
				if (CS$<>8__locals1.tokenCacheInternal.IsAppSubscribedToSerializationEvents())
				{
					DateTimeOffset? dateTimeOffset = TokenCache.CalculateSuggestedCacheExpiry(this.Accessor, logger);
					TokenCacheNotificationArgs tokenCacheNotificationArgs = new TokenCacheNotificationArgs(this, this.ClientId, account, true, CS$<>8__locals1.tokenCacheInternal.IsApplicationCache, suggestedWebCacheKey, CS$<>8__locals1.tokenCacheInternal.HasTokensNoLocks(), dateTimeOffset, requestParams.RequestContext.UserCancellationToken, requestParams.RequestContext.CorrelationId, requestParams.Scope, requestParams.AuthorityManager.OriginalAuthority.TenantId, requestParams.RequestContext.Logger.IdentityLogger, requestParams.RequestContext.Logger.PiiLoggingEnabled, null);
					MeasureDurationResult measureDurationResult2 = await CS$<>8__locals1.tokenCacheInternal.OnAfterAccessAsync(tokenCacheNotificationArgs).MeasureAsync().ConfigureAwait(false);
					requestParams.RequestContext.ApiEvent.DurationInCacheInMs += measureDurationResult2.Milliseconds;
					this.LogCacheContents(requestParams);
				}
				this.HasStateChanged = false;
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				obj = null;
				tuple = Tuple.Create<MsalAccessTokenCacheItem, MsalIdTokenCacheItem, Account>(msalAccessTokenCacheItem, msalIdTokenCacheItem, account);
			}
			finally
			{
				this._semaphoreSlim.Release();
				logger.Verbose(() => "[SaveTokenResponseAsync] Released token cache semaphore. ");
			}
			return tuple;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0003C593 File Offset: 0x0003A793
		private static bool ShouldCacheAccessToken(MsalAccessTokenCacheItem msalAccessTokenCacheItem, TokenSource tokenSource)
		{
			return msalAccessTokenCacheItem != null && tokenSource != TokenSource.Broker;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0003C5A4 File Offset: 0x0003A7A4
		private void LogCacheContents(AuthenticationRequestParameters requestParameters)
		{
			if (requestParameters.RequestContext.Logger.IsLoggingEnabled(LogLevel.Verbose))
			{
				List<MsalAccessTokenCacheItem> allAccessTokens = this.Accessor.GetAllAccessTokens(null, null);
				List<MsalRefreshTokenCacheItem> allRefreshTokens = this.Accessor.GetAllRefreshTokens(null, null);
				List<MsalAccessTokenCacheItem> list = allAccessTokens.Take(10).ToList<MsalAccessTokenCacheItem>();
				List<MsalRefreshTokenCacheItem> list2 = allRefreshTokens.Take(10).ToList<MsalRefreshTokenCacheItem>();
				StringBuilder tokenCacheKeyLog = new StringBuilder();
				tokenCacheKeyLog.AppendLine(string.Format("Total number of access tokens in the cache: {0}", allAccessTokens.Count));
				tokenCacheKeyLog.AppendLine(string.Format("Total number of refresh tokens in the cache: {0}", allRefreshTokens.Count));
				tokenCacheKeyLog.AppendLine(string.Format("First {0} access token cache keys:", list.Count));
				foreach (MsalAccessTokenCacheItem msalAccessTokenCacheItem in list)
				{
					tokenCacheKeyLog.AppendLine("AT Cache Key: " + msalAccessTokenCacheItem.ToLogString(requestParameters.RequestContext.Logger.PiiLoggingEnabled));
				}
				tokenCacheKeyLog.AppendLine(string.Format("First {0} refresh token cache keys:", list2.Count));
				foreach (MsalRefreshTokenCacheItem msalRefreshTokenCacheItem in list2)
				{
					tokenCacheKeyLog.AppendLine("RT Cache Key: " + msalRefreshTokenCacheItem.ToLogString(requestParameters.RequestContext.Logger.PiiLoggingEnabled));
				}
				requestParameters.RequestContext.Logger.Verbose(() => tokenCacheKeyLog.ToString());
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0003C784 File Offset: 0x0003A984
		private bool IsLegacyAdalCacheEnabled(AuthenticationRequestParameters requestParams)
		{
			if (requestParams.IsClientCredentialRequest)
			{
				return false;
			}
			if (this.ServiceBundle.PlatformProxy.LegacyCacheRequiresSerialization && !((ITokenCacheInternal)this).IsAppSubscribedToSerializationEvents())
			{
				return false;
			}
			if (!this.ServiceBundle.Config.LegacyCacheCompatibilityEnabled)
			{
				return false;
			}
			if (requestParams.AuthorityInfo.AuthorityType != AuthorityType.Aad)
			{
				return false;
			}
			requestParams.RequestContext.Logger.Info("IsLegacyAdalCacheEnabled: yes");
			return true;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0003C7F0 File Offset: 0x0003A9F0
		private void SaveToLegacyAdalCache(AuthenticationRequestParameters requestParams, MsalTokenResponse response, MsalRefreshTokenCacheItem msalRefreshTokenCacheItem, MsalIdTokenCacheItem msalIdTokenCacheItem, string tenantId, InstanceDiscoveryMetadataEntry instanceDiscoveryMetadata)
		{
			if (msalRefreshTokenCacheItem != null && msalRefreshTokenCacheItem.RawClientInfo != null && msalIdTokenCacheItem != null)
			{
				IdToken idToken = msalIdTokenCacheItem.IdToken;
				if (((idToken != null) ? idToken.GetUniqueId() : null) != null && this.IsLegacyAdalCacheEnabled(requestParams))
				{
					Authority authority = Authority.CreateAuthorityWithEnvironment(Authority.CreateAuthorityWithTenant(requestParams.AuthorityInfo, tenantId).AuthorityInfo, instanceDiscoveryMetadata.PreferredCache);
					CacheFallbackOperations.WriteAdalRefreshToken(requestParams.RequestContext.Logger, this.LegacyCachePersistence, msalRefreshTokenCacheItem, msalIdTokenCacheItem, authority.AuthorityInfo.CanonicalAuthority.ToString(), msalIdTokenCacheItem.IdToken.GetUniqueId(), response.Scope);
					return;
				}
			}
			requestParams.RequestContext.Logger.Verbose(() => "Not saving to ADAL legacy cache. ");
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0003C8B8 File Offset: 0x0003AAB8
		internal static DateTimeOffset? CalculateSuggestedCacheExpiry(ITokenCacheAccessor accessor, ILoggerAdapter logger)
		{
			if (accessor.GetAllRefreshTokens(null, null).Count != 0)
			{
				return null;
			}
			List<MsalAccessTokenCacheItem> allAccessTokens = accessor.GetAllAccessTokens(null, null);
			if (allAccessTokens.Count == 0)
			{
				logger.Warning("[CalculateSuggestedCacheExpiry] No access tokens or refresh tokens found in the accessor. Not returning any expiration.");
				return null;
			}
			DateTimeOffset dateTimeOffset = allAccessTokens.Max((MsalAccessTokenCacheItem item) => item.ExpiresOn);
			if (dateTimeOffset < DateTimeOffset.UtcNow + Constants.AccessTokenExpirationBuffer)
			{
				return null;
			}
			return new DateTimeOffset?(dateTimeOffset);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0003C954 File Offset: 0x0003AB54
		private void MergeWamAccountIds(MsalAccountCacheItem msalAccountCacheItem)
		{
			MsalAccountCacheItem account = this.Accessor.GetAccount(msalAccountCacheItem);
			IDictionary<string, string> dictionary = ((account != null) ? account.WamAccountIds : null);
			msalAccountCacheItem.WamAccountIds.MergeDifferentEntries(dictionary);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0003C988 File Offset: 0x0003AB88
		async Task<MsalAccessTokenCacheItem> ITokenCacheInternal.FindAccessTokenAsync(AuthenticationRequestParameters requestParams)
		{
			ILoggerAdapter logger = requestParams.RequestContext.Logger;
			AuthorityInfo authorityInfo = requestParams.AuthorityInfo;
			MsalAccessTokenCacheItem msalAccessTokenCacheItem;
			if (((authorityInfo != null) ? authorityInfo.CanonicalAuthority : null) == null)
			{
				logger.Warning("[FindAccessTokenAsync] No authority provided. Skipping cache lookup. ");
				msalAccessTokenCacheItem = null;
			}
			else
			{
				string keyFromRequest = CacheKeyFactory.GetKeyFromRequest(requestParams);
				List<MsalAccessTokenCacheItem> list = this.Accessor.GetAllAccessTokens(keyFromRequest, logger);
				requestParams.RequestContext.Logger.Always(string.Format("[FindAccessTokenAsync] Discovered {0} access tokens in cache using partition key: {1}", list.Count, keyFromRequest));
				if (list.Count == 0)
				{
					logger.Verbose(() => "[FindAccessTokenAsync] No access tokens found in the cache. Skipping filtering. ");
					requestParams.RequestContext.ApiEvent.CacheInfo = CacheRefreshReason.NoCachedAccessToken;
					msalAccessTokenCacheItem = null;
				}
				else
				{
					TokenCache.FilterTokensByHomeAccountTenantOrAssertion(list, requestParams);
					TokenCache.FilterTokensByTokenType(list, requestParams);
					TokenCache.FilterTokensByScopes(list, requestParams);
					list = await this.FilterTokensByEnvironmentAsync(list, requestParams).ConfigureAwait(false);
					this.FilterTokensByClientId<MsalAccessTokenCacheItem>(list);
					CacheRefreshReason cacheRefreshReason = CacheRefreshReason.NotApplicable;
					if (list.Count == 0)
					{
						logger.Verbose(() => "[FindAccessTokenAsync] No tokens found for matching authority, client_id, user and scopes. ");
						msalAccessTokenCacheItem = null;
					}
					else
					{
						MsalAccessTokenCacheItem msalAccessTokenCacheItem2 = TokenCache.GetSingleToken(list, requestParams);
						msalAccessTokenCacheItem2 = TokenCache.FilterTokensByPopKeyId(msalAccessTokenCacheItem2, requestParams);
						msalAccessTokenCacheItem2 = this.FilterTokensByExpiry(msalAccessTokenCacheItem2, requestParams);
						if (msalAccessTokenCacheItem2 == null)
						{
							cacheRefreshReason = CacheRefreshReason.Expired;
						}
						requestParams.RequestContext.ApiEvent.CacheInfo = cacheRefreshReason;
						msalAccessTokenCacheItem = msalAccessTokenCacheItem2;
					}
				}
			}
			return msalAccessTokenCacheItem;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0003C9D4 File Offset: 0x0003ABD4
		private static void FilterTokensByScopes(List<MsalAccessTokenCacheItem> tokenCacheItems, AuthenticationRequestParameters requestParams)
		{
			ILoggerAdapter logger = requestParams.RequestContext.Logger;
			if (tokenCacheItems.Count == 0)
			{
				logger.Verbose(() => "Not filtering by scopes, because there are no candidates");
				return;
			}
			IEnumerable<string> requestScopes = requestParams.Scope.Where((string s) => !OAuth2Value.ReservedScopes.Contains(s));
			tokenCacheItems.FilterWithLogging(delegate(MsalAccessTokenCacheItem item)
			{
				bool accepted = ScopeHelper.ScopeContains(item.ScopeSet, requestScopes);
				if (logger.IsLoggingEnabled(LogLevel.Verbose))
				{
					logger.Verbose(() => "Access token with scopes " + string.Join(" ", item.ScopeSet) + " " + string.Format("passes scope filter? {0} ", accepted));
				}
				return accepted;
			}, logger, "Filtering by scopes", true);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0003CA7C File Offset: 0x0003AC7C
		private static void FilterTokensByTokenType(List<MsalAccessTokenCacheItem> tokenCacheItems, AuthenticationRequestParameters requestParams)
		{
			tokenCacheItems.FilterWithLogging((MsalAccessTokenCacheItem item) => string.Equals(item.TokenType ?? "bearer", requestParams.AuthenticationScheme.AccessTokenType, StringComparison.OrdinalIgnoreCase), requestParams.RequestContext.Logger, "Filtering by token type", true);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0003CAC0 File Offset: 0x0003ACC0
		private static void FilterTokensByHomeAccountTenantOrAssertion(List<MsalAccessTokenCacheItem> tokenCacheItems, AuthenticationRequestParameters requestParams)
		{
			string requestTenantId = requestParams.Authority.TenantId;
			bool flag = true;
			if (ApiEvent.IsOnBehalfOfRequest(requestParams.ApiId))
			{
				tokenCacheItems.FilterWithLogging((MsalAccessTokenCacheItem item) => !string.IsNullOrEmpty(item.OboCacheKey) && item.OboCacheKey.Equals((!string.IsNullOrEmpty(requestParams.LongRunningOboCacheKey)) ? requestParams.LongRunningOboCacheKey : requestParams.UserAssertion.AssertionHash, StringComparison.OrdinalIgnoreCase), requestParams.RequestContext.Logger, (!string.IsNullOrEmpty(requestParams.LongRunningOboCacheKey)) ? ("Filtering AT by user-provided cache key: " + requestParams.LongRunningOboCacheKey) : ("Filtering AT by user assertion: " + requestParams.UserAssertion.AssertionHash), true);
				flag = !string.IsNullOrEmpty(requestTenantId) && !AadAuthority.IsCommonOrganizationsOrConsumersTenant(requestTenantId);
			}
			if (flag)
			{
				tokenCacheItems.FilterWithLogging((MsalAccessTokenCacheItem item) => string.Equals(item.TenantId ?? string.Empty, requestTenantId ?? string.Empty, StringComparison.OrdinalIgnoreCase), requestParams.RequestContext.Logger, "Filtering AT by tenant id", true);
			}
			else
			{
				requestParams.RequestContext.Logger.Warning("Have not filtered by tenant ID. This can happen in OBO scenario where authority is /common or /organizations. Please use tenanted authority.");
			}
			if (requestParams.ApiId != ApiEvent.ApiIds.AcquireTokenForClient && requestParams.ApiId != ApiEvent.ApiIds.AcquireTokenForSystemAssignedManagedIdentity && requestParams.ApiId != ApiEvent.ApiIds.AcquireTokenForUserAssignedManagedIdentity && !ApiEvent.IsOnBehalfOfRequest(requestParams.ApiId))
			{
				tokenCacheItems.FilterWithLogging(delegate(MsalAccessTokenCacheItem item)
				{
					string homeAccountId = item.HomeAccountId;
					AccountId homeAccountId2 = requestParams.Account.HomeAccountId;
					return homeAccountId.Equals((homeAccountId2 != null) ? homeAccountId2.Identifier : null, StringComparison.OrdinalIgnoreCase);
				}, requestParams.RequestContext.Logger, "Filtering AT by home account id", true);
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0003CC48 File Offset: 0x0003AE48
		private MsalAccessTokenCacheItem FilterTokensByExpiry(MsalAccessTokenCacheItem msalAccessTokenCacheItem, AuthenticationRequestParameters requestParams)
		{
			ILoggerAdapter logger = requestParams.RequestContext.Logger;
			if (msalAccessTokenCacheItem != null)
			{
				if (msalAccessTokenCacheItem.ExpiresOn > DateTime.UtcNow + Constants.AccessTokenExpirationBuffer)
				{
					if (msalAccessTokenCacheItem.ExpiresOn > DateTime.UtcNow + TimeSpan.FromDays(3650.0))
					{
						logger.Error("Access token expiration too large. This can be the result of a bug or corrupt cache. Token will be ignored as it is likely expired." + TokenCache.GetAccessTokenExpireLogMessageContent(msalAccessTokenCacheItem));
						return null;
					}
					logger.Info(() => "Access token is not expired. Returning the found cache entry. " + TokenCache.GetAccessTokenExpireLogMessageContent(msalAccessTokenCacheItem));
					return msalAccessTokenCacheItem;
				}
				else
				{
					if (this.ServiceBundle.Config.IsExtendedTokenLifetimeEnabled && msalAccessTokenCacheItem.ExtendedExpiresOn > DateTime.UtcNow + Constants.AccessTokenExpirationBuffer)
					{
						logger.Info(() => "Access token is expired.  IsExtendedLifeTimeEnabled=TRUE and ExtendedExpiresOn is not exceeded.  Returning the found cache entry. " + TokenCache.GetAccessTokenExpireLogMessageContent(msalAccessTokenCacheItem));
						msalAccessTokenCacheItem.IsExtendedLifeTimeToken = true;
						return msalAccessTokenCacheItem;
					}
					logger.Info(() => "Access token has expired or about to expire. " + TokenCache.GetAccessTokenExpireLogMessageContent(msalAccessTokenCacheItem));
				}
			}
			return null;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0003CD74 File Offset: 0x0003AF74
		private static MsalAccessTokenCacheItem GetSingleToken(List<MsalAccessTokenCacheItem> tokenCacheItems, AuthenticationRequestParameters requestParams)
		{
			if (tokenCacheItems.Count == 1)
			{
				return tokenCacheItems[0];
			}
			requestParams.RequestContext.Logger.Error("Multiple access tokens found for matching authority, client_id, user and scopes. ");
			throw new MsalClientException("multiple_matching_tokens_detected", "The cache contains multiple tokens satisfying the requirements. Try to clear token cache. ");
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0003CDAC File Offset: 0x0003AFAC
		private async Task<List<MsalAccessTokenCacheItem>> FilterTokensByEnvironmentAsync(List<MsalAccessTokenCacheItem> tokenCacheItems, AuthenticationRequestParameters requestParams)
		{
			ILoggerAdapter logger = requestParams.RequestContext.Logger;
			List<MsalAccessTokenCacheItem> list;
			if (tokenCacheItems.Count == 0)
			{
				logger.Verbose(() => "Not filtering AT by environment, because there are no candidates");
				list = tokenCacheItems;
			}
			else
			{
				InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry = await this.ServiceBundle.InstanceDiscoveryManager.GetMetadataEntryTryAvoidNetworkAsync(requestParams.AuthorityInfo, tokenCacheItems.Select((MsalAccessTokenCacheItem at) => at.Environment), requestParams.RequestContext).ConfigureAwait(false);
				InstanceDiscoveryMetadataEntry instanceMetadata = instanceDiscoveryMetadataEntry;
				List<MsalAccessTokenCacheItem> itemsFilteredByAlias = tokenCacheItems.FilterWithLogging((MsalAccessTokenCacheItem item) => item.Environment.Equals(instanceMetadata.PreferredCache, StringComparison.OrdinalIgnoreCase), requestParams.RequestContext.Logger, "Filtering AT by preferred environment " + instanceMetadata.PreferredCache, false);
				if (itemsFilteredByAlias.Count > 0)
				{
					if (logger.IsLoggingEnabled(LogLevel.Verbose))
					{
						logger.Verbose(() => string.Format("Filtered AT by preferred alias returning {0} tokens.", itemsFilteredByAlias.Count));
					}
					list = itemsFilteredByAlias;
				}
				else
				{
					list = tokenCacheItems.FilterWithLogging((MsalAccessTokenCacheItem item) => instanceMetadata.Aliases.ContainsOrdinalIgnoreCase(item.Environment), requestParams.RequestContext.Logger, "Filtering AT by environment", true);
				}
			}
			return list;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0003CE00 File Offset: 0x0003B000
		private static MsalAccessTokenCacheItem FilterTokensByPopKeyId(MsalAccessTokenCacheItem item, AuthenticationRequestParameters authenticationRequest)
		{
			if (item == null)
			{
				return null;
			}
			string requestKid = authenticationRequest.AuthenticationScheme.KeyId;
			if (string.IsNullOrEmpty(item.KeyId) && string.IsNullOrEmpty(requestKid))
			{
				authenticationRequest.RequestContext.Logger.Verbose(() => "Bearer token found");
				return item;
			}
			if (string.Equals(item.KeyId, requestKid, StringComparison.OrdinalIgnoreCase))
			{
				authenticationRequest.RequestContext.Logger.Verbose(() => "Keyed token found");
				return item;
			}
			authenticationRequest.RequestContext.Logger.Info(() => "A token bound to the wrong key was found. Token key id: " + item.KeyId + " Request key id: " + requestKid);
			return null;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0003CEF6 File Offset: 0x0003B0F6
		private void FilterTokensByClientId<T>(List<T> tokenCacheItems) where T : MsalCredentialCacheItemBase
		{
			tokenCacheItems.RemoveAll((T x) => !x.ClientId.Equals(this.ClientId, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0003CF0C File Offset: 0x0003B10C
		internal async Task ExpireAllAccessTokensForTestAsync()
		{
			ITokenCacheAccessor accessor = ((ITokenCacheInternal)this).Accessor;
			foreach (MsalAccessTokenCacheItem msalAccessTokenCacheItem in accessor.GetAllAccessTokens(null, null))
			{
				accessor.SaveAccessToken(msalAccessTokenCacheItem.WithExpiresOn(DateTimeOffset.UtcNow));
			}
			if (((ITokenCacheInternal)this).IsAppSubscribedToSerializationEvents())
			{
				TokenCacheNotificationArgs tokenCacheNotificationArgs = new TokenCacheNotificationArgs(this, this.ClientId, null, true, ((ITokenCacheInternal)this).IsApplicationCache, null, ((ITokenCacheInternal)this).HasTokensNoLocks(), null, default(CancellationToken), default(Guid), null, null, null, false, null);
				await ((ITokenCacheInternal)this).OnAfterAccessAsync(tokenCacheNotificationArgs).ConfigureAwait(false);
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0003CF50 File Offset: 0x0003B150
		async Task<MsalRefreshTokenCacheItem> ITokenCacheInternal.FindRefreshTokenAsync(AuthenticationRequestParameters requestParams, string familyId)
		{
			MsalRefreshTokenCacheItem msalRefreshTokenCacheItem;
			if (requestParams.Authority == null)
			{
				msalRefreshTokenCacheItem = null;
			}
			else
			{
				string keyFromRequest = CacheKeyFactory.GetKeyFromRequest(requestParams);
				List<MsalRefreshTokenCacheItem> refreshTokens = this.Accessor.GetAllRefreshTokens(keyFromRequest, null);
				requestParams.RequestContext.Logger.Always(string.Format("[FindRefreshTokenAsync] Discovered {0} refresh tokens in cache using key: {1}", refreshTokens.Count, keyFromRequest));
				if (refreshTokens.Count != 0)
				{
					TokenCache.FilterRefreshTokensByHomeAccountIdOrAssertion(refreshTokens, requestParams, familyId);
					if (!requestParams.AppConfig.MultiCloudSupportEnabled)
					{
						TokenCache.<>c__DisplayClass66_1 CS$<>8__locals2 = new TokenCache.<>c__DisplayClass66_1();
						InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry = await this.ServiceBundle.InstanceDiscoveryManager.GetMetadataEntryTryAvoidNetworkAsync(requestParams.AuthorityInfo, refreshTokens.Select((MsalRefreshTokenCacheItem rt) => rt.Environment), requestParams.RequestContext).ConfigureAwait(false);
						CS$<>8__locals2.aliases = instanceDiscoveryMetadataEntry.Aliases;
						refreshTokens.RemoveAll((MsalRefreshTokenCacheItem item) => !CS$<>8__locals2.aliases.ContainsOrdinalIgnoreCase(item.Environment));
						CS$<>8__locals2 = null;
					}
					requestParams.RequestContext.Logger.Info(() => "[FindRefreshTokenAsync] Refresh token found in the cache? - " + (refreshTokens.Count != 0).ToString());
					if (refreshTokens.Count > 0)
					{
						return refreshTokens.FirstOrDefault<MsalRefreshTokenCacheItem>();
					}
				}
				else
				{
					requestParams.RequestContext.Logger.Verbose(() => "[FindRefreshTokenAsync] No RTs found in the MSAL cache ");
				}
				requestParams.RequestContext.Logger.Verbose(() => "[FindRefreshTokenAsync] Checking ADAL cache for matching RT. ");
				if (this.IsLegacyAdalCacheEnabled(requestParams) && requestParams.Account != null && string.IsNullOrEmpty(familyId))
				{
					string[] aliases = (await this.ServiceBundle.InstanceDiscoveryManager.GetMetadataEntryTryAvoidNetworkAsync(requestParams.AuthorityInfo, refreshTokens.Select((MsalRefreshTokenCacheItem rt) => rt.Environment), requestParams.RequestContext).ConfigureAwait(false)).Aliases;
					msalRefreshTokenCacheItem = CacheFallbackOperations.GetRefreshToken(requestParams.RequestContext.Logger, this.LegacyCachePersistence, aliases, requestParams.AppConfig.ClientId, requestParams.Account);
				}
				else
				{
					msalRefreshTokenCacheItem = null;
				}
			}
			return msalRefreshTokenCacheItem;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0003CFA4 File Offset: 0x0003B1A4
		private static void FilterRefreshTokensByHomeAccountIdOrAssertion(List<MsalRefreshTokenCacheItem> cacheItems, AuthenticationRequestParameters requestParams, string familyId)
		{
			if (ApiEvent.IsOnBehalfOfRequest(requestParams.ApiId))
			{
				cacheItems.FilterWithLogging((MsalRefreshTokenCacheItem item) => !string.IsNullOrEmpty(item.OboCacheKey) && item.OboCacheKey.Equals((!string.IsNullOrEmpty(requestParams.LongRunningOboCacheKey)) ? requestParams.LongRunningOboCacheKey : requestParams.UserAssertion.AssertionHash, StringComparison.OrdinalIgnoreCase), requestParams.RequestContext.Logger, (!string.IsNullOrEmpty(requestParams.LongRunningOboCacheKey)) ? ("Filtering RT by user-provided cache key: " + requestParams.LongRunningOboCacheKey) : ("Filtering RT by user assertion: " + requestParams.UserAssertion.AssertionHash), true);
			}
			else
			{
				cacheItems.FilterWithLogging(delegate(MsalRefreshTokenCacheItem item)
				{
					string homeAccountId = item.HomeAccountId;
					AccountId homeAccountId2 = requestParams.Account.HomeAccountId;
					return homeAccountId.Equals((homeAccountId2 != null) ? homeAccountId2.Identifier : null, StringComparison.OrdinalIgnoreCase);
				}, requestParams.RequestContext.Logger, "Filtering RT by home account id", true);
			}
			cacheItems.FilterWithLogging((MsalRefreshTokenCacheItem item) => string.Equals(item.FamilyId ?? string.Empty, familyId ?? string.Empty, StringComparison.OrdinalIgnoreCase), requestParams.RequestContext.Logger, "Filtering RT by family id", true);
			if (string.IsNullOrEmpty(familyId))
			{
				cacheItems.FilterWithLogging((MsalRefreshTokenCacheItem item) => item.ClientId.Equals(requestParams.AppConfig.ClientId, StringComparison.OrdinalIgnoreCase), requestParams.RequestContext.Logger, "Filtering RT by client id", true);
			}
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0003D0C8 File Offset: 0x0003B2C8
		async Task<bool?> ITokenCacheInternal.IsFociMemberAsync(AuthenticationRequestParameters requestParams, string familyId)
		{
			ILoggerAdapter logger = requestParams.RequestContext.Logger;
			Uri uri;
			if (requestParams == null)
			{
				uri = null;
			}
			else
			{
				AuthorityInfo authorityInfo = requestParams.AuthorityInfo;
				uri = ((authorityInfo != null) ? authorityInfo.CanonicalAuthority : null);
			}
			bool? flag;
			if (uri == null)
			{
				logger.Warning("No authority details, can't check app metadata. Returning unknown. ");
				flag = null;
			}
			else
			{
				List<MsalAppMetadataCacheItem> allAppMetadata = this.Accessor.GetAllAppMetadata();
				MsalAppMetadataCacheItem msalAppMetadataCacheItem = (await this.ServiceBundle.InstanceDiscoveryManager.GetMetadataEntryTryAvoidNetworkAsync(requestParams.AuthorityInfo, allAppMetadata.Select((MsalAppMetadataCacheItem m) => m.Environment), requestParams.RequestContext).ConfigureAwait(false)).Aliases.Select((string env) => this.Accessor.GetAppMetadata(new MsalAppMetadataCacheItem(this.ClientId, env, null))).FirstOrDefault((MsalAppMetadataCacheItem item) => item != null);
				if (msalAppMetadataCacheItem == null)
				{
					logger.Warning("No app metadata found. Returning unknown. ");
					flag = null;
				}
				else
				{
					flag = new bool?(msalAppMetadataCacheItem.FamilyId == familyId);
				}
			}
			return flag;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0003D11C File Offset: 0x0003B31C
		async Task<IEnumerable<IAccount>> ITokenCacheInternal.GetAccountsAsync(AuthenticationRequestParameters requestParameters)
		{
			ILoggerAdapter logger = requestParameters.RequestContext.Logger;
			string environment = requestParameters.AuthorityInfo.Host;
			bool flag = !this._featureFlags.IsFociEnabled;
			string keyFromRequest = CacheKeyFactory.GetKeyFromRequest(requestParameters);
			List<MsalRefreshTokenCacheItem> refreshTokenCacheItems = this.Accessor.GetAllRefreshTokens(keyFromRequest, null);
			List<MsalAccountCacheItem> accountCacheItems = this.Accessor.GetAllAccounts(keyFromRequest, null);
			if (flag)
			{
				this.FilterTokensByClientId<MsalRefreshTokenCacheItem>(refreshTokenCacheItems);
			}
			if (logger.IsLoggingEnabled(LogLevel.Verbose))
			{
				logger.Verbose(() => string.Format("[GetAccounts] Found {0} RTs and {1} accounts in MSAL cache. ", refreshTokenCacheItems.Count, accountCacheItems.Count));
			}
			ISet<string> set = new HashSet<string>(accountCacheItems.Select((MsalAccountCacheItem aci) => aci.Environment), StringComparer.OrdinalIgnoreCase);
			set.UnionWith(refreshTokenCacheItems.Select((MsalRefreshTokenCacheItem rt) => rt.Environment));
			AdalUsersForMsal adalUsersResult = null;
			if (this.IsLegacyAdalCacheEnabled(requestParameters))
			{
				adalUsersResult = CacheFallbackOperations.GetAllAdalUsersForMsal(logger, this.LegacyCachePersistence, this.ClientId);
				set.UnionWith(adalUsersResult.GetAdalUserEnvironments());
			}
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry = await this.ServiceBundle.InstanceDiscoveryManager.GetMetadataEntryTryAvoidNetworkAsync(requestParameters.AuthorityInfo, set, requestParameters.RequestContext).ConfigureAwait(false);
			InstanceDiscoveryMetadataEntry instanceMetadata = instanceDiscoveryMetadataEntry;
			if (!requestParameters.AppConfig.MultiCloudSupportEnabled)
			{
				refreshTokenCacheItems.RemoveAll((MsalRefreshTokenCacheItem rt) => !instanceMetadata.Aliases.ContainsOrdinalIgnoreCase(rt.Environment));
				accountCacheItems.RemoveAll((MsalAccountCacheItem acc) => !instanceMetadata.Aliases.ContainsOrdinalIgnoreCase(acc.Environment));
			}
			if (logger.IsLoggingEnabled(LogLevel.Verbose))
			{
				logger.Verbose(() => string.Format("[GetAccounts] Found {0} RTs and {1} accounts in MSAL cache after environment filtering. ", refreshTokenCacheItems.Count, accountCacheItems.Count));
			}
			IDictionary<string, Account> clientInfoToAccountMap = new Dictionary<string, Account>();
			foreach (MsalRefreshTokenCacheItem rtItem in refreshTokenCacheItems)
			{
				foreach (MsalAccountCacheItem account in accountCacheItems)
				{
					if (this.RtMatchesAccount(rtItem, account))
					{
						IDictionary<string, TenantProfile> dictionary = await this.GetTenantProfilesAsync(requestParameters, account.HomeAccountId).ConfigureAwait(false);
						clientInfoToAccountMap[rtItem.HomeAccountId] = new Account(account.HomeAccountId, account.PreferredUsername, requestParameters.AppConfig.MultiCloudSupportEnabled ? account.Environment : environment, account.WamAccountIds, (dictionary != null) ? dictionary.Values : null);
						break;
					}
					account = null;
				}
				List<MsalAccountCacheItem>.Enumerator enumerator2 = default(List<MsalAccountCacheItem>.Enumerator);
				rtItem = null;
			}
			List<MsalRefreshTokenCacheItem>.Enumerator enumerator = default(List<MsalRefreshTokenCacheItem>.Enumerator);
			if (this.IsLegacyAdalCacheEnabled(requestParameters))
			{
				TokenCache.UpdateMapWithAdalAccountsWithClientInfo(environment, instanceMetadata.Aliases, adalUsersResult, clientInfoToAccountMap);
			}
			if (requestParameters.AppConfig.IsBrokerEnabled && this.ServiceBundle.PlatformProxy.BrokerSupportsWamAccounts)
			{
				foreach (MsalAccountCacheItem account in accountCacheItems)
				{
					if (!clientInfoToAccountMap.ContainsKey(account.HomeAccountId) && account.WamAccountIds != null && account.WamAccountIds.ContainsKey(requestParameters.AppConfig.ClientId))
					{
						IDictionary<string, TenantProfile> dictionary2 = await this.GetTenantProfilesAsync(requestParameters, account.HomeAccountId).ConfigureAwait(false);
						Account account2 = new Account(account.HomeAccountId, account.PreferredUsername, account.Environment, account.WamAccountIds, (dictionary2 != null) ? dictionary2.Values : null);
						clientInfoToAccountMap[account.HomeAccountId] = account2;
					}
					account = null;
				}
				List<MsalAccountCacheItem>.Enumerator enumerator2 = default(List<MsalAccountCacheItem>.Enumerator);
			}
			List<IAccount> accounts = new List<IAccount>(clientInfoToAccountMap.Values);
			if (this.IsLegacyAdalCacheEnabled(requestParameters))
			{
				TokenCache.UpdateWithAdalAccountsWithoutClientInfo(environment, instanceMetadata.Aliases, adalUsersResult, accounts);
			}
			if (!string.IsNullOrEmpty(requestParameters.HomeAccountId))
			{
				accounts = accounts.Where((IAccount acc) => acc.HomeAccountId.Identifier.Equals(requestParameters.HomeAccountId, StringComparison.OrdinalIgnoreCase)).ToList<IAccount>();
				if (logger.IsLoggingEnabled(LogLevel.Verbose))
				{
					logger.Verbose(() => string.Format("Filtered by home account id. Remaining accounts {0} ", accounts.Count));
				}
			}
			return accounts;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0003D168 File Offset: 0x0003B368
		private static void UpdateMapWithAdalAccountsWithClientInfo(string envFromRequest, IEnumerable<string> envAliases, AdalUsersForMsal adalUsers, IDictionary<string, Account> clientInfoToAccountMap)
		{
			foreach (KeyValuePair<string, AdalUserInfo> keyValuePair in ((adalUsers != null) ? adalUsers.GetUsersWithClientInfo(envAliases) : null))
			{
				string text = ClientInfo.CreateFromJson(keyValuePair.Key).ToAccountIdentifier();
				if (!clientInfoToAccountMap.ContainsKey(text))
				{
					clientInfoToAccountMap[text] = new Account(text, keyValuePair.Value.DisplayableId, envFromRequest, null, null);
				}
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0003D1EC File Offset: 0x0003B3EC
		private static void UpdateWithAdalAccountsWithoutClientInfo(string envFromRequest, IEnumerable<string> envAliases, AdalUsersForMsal adalUsers, List<IAccount> accounts)
		{
			List<string> list = accounts.Select((IAccount a) => a.Username).Distinct<string>().ToList<string>();
			foreach (AdalUserInfo adalUserInfo in ((adalUsers != null) ? adalUsers.GetUsersWithoutClientInfo(envAliases) : null))
			{
				if (!string.IsNullOrEmpty(adalUserInfo.DisplayableId) && !list.Contains(adalUserInfo.DisplayableId))
				{
					accounts.Add(new Account(null, adalUserInfo.DisplayableId, envFromRequest, null, null));
					list.Add(adalUserInfo.DisplayableId);
				}
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0003D2A8 File Offset: 0x0003B4A8
		MsalIdTokenCacheItem ITokenCacheInternal.GetIdTokenCacheItem(MsalAccessTokenCacheItem msalAccessTokenCacheItem)
		{
			return this.Accessor.GetIdToken(msalAccessTokenCacheItem);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0003D2B8 File Offset: 0x0003B4B8
		private async Task<IDictionary<string, TenantProfile>> GetTenantProfilesAsync(AuthenticationRequestParameters requestParameters, string homeAccountId)
		{
			IDictionary<string, TenantProfile> dictionary;
			if (!requestParameters.AuthorityInfo.CanBeTenanted)
			{
				dictionary = null;
			}
			else if (homeAccountId == null)
			{
				requestParameters.RequestContext.Logger.Warning("No homeAccountId, skipping tenant profiles");
				dictionary = null;
			}
			else
			{
				List<MsalIdTokenCacheItem> idTokenCacheItems = this.Accessor.GetAllIdTokens(homeAccountId, null);
				this.FilterTokensByClientId<MsalIdTokenCacheItem>(idTokenCacheItems);
				if (!requestParameters.AppConfig.MultiCloudSupportEnabled)
				{
					TokenCache.<>c__DisplayClass73_1 CS$<>8__locals2 = new TokenCache.<>c__DisplayClass73_1();
					ISet<string> set = new HashSet<string>(idTokenCacheItems.Select((MsalIdTokenCacheItem aci) => aci.Environment), StringComparer.OrdinalIgnoreCase);
					InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry = await this.ServiceBundle.InstanceDiscoveryManager.GetMetadataEntryTryAvoidNetworkAsync(requestParameters.AuthorityInfo, set, requestParameters.RequestContext).ConfigureAwait(false);
					CS$<>8__locals2.instanceMetadata = instanceDiscoveryMetadataEntry;
					idTokenCacheItems.RemoveAll((MsalIdTokenCacheItem idToken) => !CS$<>8__locals2.instanceMetadata.Aliases.ContainsOrdinalIgnoreCase(idToken.Environment));
					CS$<>8__locals2 = null;
				}
				idTokenCacheItems.RemoveAll((MsalIdTokenCacheItem idToken) => !homeAccountId.Equals(idToken.HomeAccountId));
				Dictionary<string, TenantProfile> dictionary2 = new Dictionary<string, TenantProfile>();
				foreach (MsalIdTokenCacheItem msalIdTokenCacheItem in idTokenCacheItems)
				{
					dictionary2[msalIdTokenCacheItem.TenantId] = new TenantProfile(msalIdTokenCacheItem);
				}
				dictionary = dictionary2;
			}
			return dictionary;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0003D30C File Offset: 0x0003B50C
		async Task<Account> ITokenCacheInternal.GetAccountAssociatedWithAccessTokenAsync(AuthenticationRequestParameters requestParameters, MsalAccessTokenCacheItem msalAccessTokenCacheItem)
		{
			IDictionary<string, TenantProfile> dictionary = await this.GetTenantProfilesAsync(requestParameters, msalAccessTokenCacheItem.HomeAccountId).ConfigureAwait(false);
			ITokenCacheAccessor accessor = this.Accessor;
			string environment = msalAccessTokenCacheItem.Environment;
			string tenantId = msalAccessTokenCacheItem.TenantId;
			string homeAccountId = msalAccessTokenCacheItem.HomeAccountId;
			IAccount account = requestParameters.Account;
			MsalAccountCacheItem account2 = accessor.GetAccount(new MsalAccountCacheItem(environment, tenantId, homeAccountId, (account != null) ? account.Username : null));
			return new Account(msalAccessTokenCacheItem.HomeAccountId, (account2 != null) ? account2.PreferredUsername : null, (account2 != null) ? account2.Environment : null, (account2 != null) ? account2.WamAccountIds : null, (dictionary != null) ? dictionary.Values : null);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0003D360 File Offset: 0x0003B560
		async Task<bool> ITokenCacheInternal.StopLongRunningOboProcessAsync(string longRunningOboCacheKey, AuthenticationRequestParameters requestParameters)
		{
			requestParameters.RequestContext.Logger.Verbose(() => "[StopLongRunningOboProcessAsync] Entering token cache semaphore. Count " + this._semaphoreSlim.GetCurrentCountLogMessage());
			await this._semaphoreSlim.WaitAsync(requestParameters.RequestContext.UserCancellationToken).ConfigureAwait(false);
			requestParameters.RequestContext.Logger.Verbose(() => "[StopLongRunningOboProcessAsync] Entered token cache semaphore");
			bool tokensRemoved;
			try
			{
				requestParameters.RequestContext.Logger.Info(() => "[StopLongRunningOboProcessAsync] Stopping long running OBO process by removing tokens from cache.");
				ITokenCacheInternal tokenCacheInternal = this;
				object obj = null;
				try
				{
					if (tokenCacheInternal.IsAppSubscribedToSerializationEvents())
					{
						TokenCacheNotificationArgs args = new TokenCacheNotificationArgs(this, this.ClientId, null, false, tokenCacheInternal.IsApplicationCache, longRunningOboCacheKey, tokenCacheInternal.HasTokensNoLocks(), null, requestParameters.RequestContext.UserCancellationToken, requestParameters.RequestContext.CorrelationId, requestParameters.Scope, requestParameters.AuthorityManager.OriginalAuthority.TenantId, requestParameters.RequestContext.Logger.IdentityLogger, requestParameters.RequestContext.Logger.PiiLoggingEnabled, null);
						await tokenCacheInternal.OnBeforeAccessAsync(args).ConfigureAwait(false);
						await tokenCacheInternal.OnBeforeWriteAsync(args).ConfigureAwait(false);
						args = null;
					}
					tokensRemoved = this.RemoveOboTokensInternal(longRunningOboCacheKey, requestParameters.RequestContext);
				}
				catch (object obj)
				{
				}
				if (tokenCacheInternal.IsAppSubscribedToSerializationEvents())
				{
					await tokenCacheInternal.OnAfterAccessAsync(new TokenCacheNotificationArgs(this, this.ClientId, null, true, tokenCacheInternal.IsApplicationCache, longRunningOboCacheKey, tokenCacheInternal.HasTokensNoLocks(), null, requestParameters.RequestContext.UserCancellationToken, requestParameters.RequestContext.CorrelationId, requestParameters.Scope, requestParameters.AuthorityManager.OriginalAuthority.TenantId, requestParameters.RequestContext.Logger.IdentityLogger, requestParameters.RequestContext.Logger.PiiLoggingEnabled, null)).ConfigureAwait(false);
				}
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				obj = null;
				tokenCacheInternal = null;
			}
			finally
			{
				this.HasStateChanged = false;
				this._semaphoreSlim.Release();
			}
			return tokensRemoved;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0003D3B4 File Offset: 0x0003B5B4
		async Task ITokenCacheInternal.RemoveAccountAsync(IAccount account, AuthenticationRequestParameters requestParameters)
		{
			requestParameters.RequestContext.Logger.Verbose(() => "[RemoveAccountAsync] Entering token cache semaphore. Count " + this._semaphoreSlim.GetCurrentCountLogMessage());
			await this._semaphoreSlim.WaitAsync(requestParameters.RequestContext.UserCancellationToken).ConfigureAwait(false);
			requestParameters.RequestContext.Logger.Verbose(() => "[RemoveAccountAsync] Entered token cache semaphore");
			AccountId homeAccountId = account.HomeAccountId;
			string cacheKey = ((homeAccountId != null) ? homeAccountId.Identifier : null);
			try
			{
				requestParameters.RequestContext.Logger.Info("[RemoveAccountAsync] Removing account from cache.");
				ITokenCacheInternal tokenCacheInternal = this;
				object obj = null;
				try
				{
					if (tokenCacheInternal.IsAppSubscribedToSerializationEvents())
					{
						TokenCacheNotificationArgs args = new TokenCacheNotificationArgs(this, this.ClientId, account, true, tokenCacheInternal.IsApplicationCache, cacheKey, tokenCacheInternal.HasTokensNoLocks(), null, requestParameters.RequestContext.UserCancellationToken, requestParameters.RequestContext.CorrelationId, requestParameters.Scope, requestParameters.AuthorityManager.OriginalAuthority.TenantId, requestParameters.RequestContext.Logger.IdentityLogger, requestParameters.RequestContext.Logger.PiiLoggingEnabled, null);
						await tokenCacheInternal.OnBeforeAccessAsync(args).ConfigureAwait(false);
						await tokenCacheInternal.OnBeforeWriteAsync(args).ConfigureAwait(false);
						args = null;
					}
					this.RemoveAccountInternal(account, requestParameters.RequestContext);
					if (this.IsLegacyAdalCacheEnabled(requestParameters))
					{
						CacheFallbackOperations.RemoveAdalUser(requestParameters.RequestContext.Logger, this.LegacyCachePersistence, this.ClientId, (account != null) ? account.Username : null, cacheKey);
					}
				}
				catch (object obj)
				{
				}
				if (tokenCacheInternal.IsAppSubscribedToSerializationEvents())
				{
					await tokenCacheInternal.OnAfterAccessAsync(new TokenCacheNotificationArgs(this, this.ClientId, account, true, tokenCacheInternal.IsApplicationCache, cacheKey, tokenCacheInternal.HasTokensNoLocks(), null, requestParameters.RequestContext.UserCancellationToken, requestParameters.RequestContext.CorrelationId, requestParameters.Scope, requestParameters.AuthorityManager.OriginalAuthority.TenantId, requestParameters.RequestContext.Logger.IdentityLogger, requestParameters.RequestContext.Logger.PiiLoggingEnabled, null)).ConfigureAwait(false);
				}
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex = obj2 as Exception;
					if (ex == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				obj = null;
				tokenCacheInternal = null;
			}
			finally
			{
				this.HasStateChanged = false;
				this._semaphoreSlim.Release();
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0003D407 File Offset: 0x0003B607
		bool ITokenCacheInternal.HasTokensNoLocks()
		{
			return this.Accessor.HasAccessOrRefreshTokens();
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0003D414 File Offset: 0x0003B614
		private bool RemoveOboTokensInternal(string oboPartitionKey, RequestContext requestContext)
		{
			ILoggerAdapter logger = requestContext.Logger;
			List<MsalRefreshTokenCacheItem> allRefreshTokens = this.Accessor.GetAllRefreshTokens(oboPartitionKey, logger);
			allRefreshTokens.RemoveAll((MsalRefreshTokenCacheItem item) => !((item != null) ? new bool?(item.OboCacheKey.Equals(oboPartitionKey, StringComparison.OrdinalIgnoreCase)) : null).Value);
			bool flag;
			int num = this.RemoveRefreshTokens(allRefreshTokens, logger, out flag);
			List<MsalAccessTokenCacheItem> allAccessTokens = this.Accessor.GetAllAccessTokens(oboPartitionKey, logger);
			allAccessTokens.RemoveAll((MsalAccessTokenCacheItem item) => !((item != null) ? new bool?(item.OboCacheKey.Equals(oboPartitionKey, StringComparison.OrdinalIgnoreCase)) : null).Value);
			int num2 = this.RemoveAccessTokens(allAccessTokens, logger, flag);
			return num > 0 || num2 > 0;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0003D4A4 File Offset: 0x0003B6A4
		internal void RemoveAccountInternal(IAccount account, RequestContext requestContext)
		{
			if (account.HomeAccountId == null)
			{
				return;
			}
			string partitionKey = account.HomeAccountId.Identifier;
			ILoggerAdapter logger = requestContext.Logger;
			List<MsalRefreshTokenCacheItem> allRefreshTokens = this.Accessor.GetAllRefreshTokens(partitionKey, logger);
			allRefreshTokens.RemoveAll((MsalRefreshTokenCacheItem item) => !item.HomeAccountId.Equals(partitionKey, StringComparison.OrdinalIgnoreCase));
			bool flag;
			this.RemoveRefreshTokens(allRefreshTokens, logger, out flag);
			List<MsalAccessTokenCacheItem> allAccessTokens = this.Accessor.GetAllAccessTokens(partitionKey, logger);
			allAccessTokens.RemoveAll((MsalAccessTokenCacheItem item) => !item.HomeAccountId.Equals(partitionKey, StringComparison.OrdinalIgnoreCase));
			this.RemoveAccessTokens(allAccessTokens, logger, flag);
			this.RemoveIdTokens(partitionKey, logger, flag);
			this.RemoveAccounts(account);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0003D554 File Offset: 0x0003B754
		private int RemoveRefreshTokens(List<MsalRefreshTokenCacheItem> refreshTokens, ILoggerAdapter logger, out bool filterByClientId)
		{
			filterByClientId = !this._featureFlags.IsFociEnabled || !TokenCache.FrtExists(refreshTokens);
			if (filterByClientId)
			{
				this.FilterTokensByClientId<MsalRefreshTokenCacheItem>(refreshTokens);
			}
			foreach (MsalRefreshTokenCacheItem msalRefreshTokenCacheItem in refreshTokens)
			{
				this.Accessor.DeleteRefreshToken(msalRefreshTokenCacheItem);
			}
			logger.Info(() => string.Format("[RemoveRefreshTokens] Deleted {0} refresh tokens.", refreshTokens.Count));
			return refreshTokens.Count;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0003D608 File Offset: 0x0003B808
		private int RemoveAccessTokens(List<MsalAccessTokenCacheItem> accessTokens, ILoggerAdapter logger, bool filterByClientId)
		{
			if (filterByClientId)
			{
				this.FilterTokensByClientId<MsalAccessTokenCacheItem>(accessTokens);
			}
			foreach (MsalAccessTokenCacheItem msalAccessTokenCacheItem in accessTokens)
			{
				this.Accessor.DeleteAccessToken(msalAccessTokenCacheItem);
			}
			logger.Info(() => string.Format("[RemoveAccessTokens] Deleted {0} access tokens.", accessTokens.Count));
			return accessTokens.Count;
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0003D69C File Offset: 0x0003B89C
		private int RemoveIdTokens(string partitionKey, ILoggerAdapter logger, bool filterByClientId)
		{
			List<MsalIdTokenCacheItem> idTokens = this.Accessor.GetAllIdTokens(partitionKey, null);
			idTokens.RemoveAll((MsalIdTokenCacheItem item) => !item.HomeAccountId.Equals(partitionKey, StringComparison.OrdinalIgnoreCase));
			if (filterByClientId)
			{
				this.FilterTokensByClientId<MsalIdTokenCacheItem>(idTokens);
			}
			foreach (MsalIdTokenCacheItem msalIdTokenCacheItem in idTokens)
			{
				this.Accessor.DeleteIdToken(msalIdTokenCacheItem);
			}
			logger.Info(() => string.Format("[RemoveIdTokens] Deleted {0} ID tokens.", idTokens.Count));
			return idTokens.Count;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0003D760 File Offset: 0x0003B960
		private void RemoveAccounts(IAccount account)
		{
			if (account != null)
			{
				List<MsalAccountCacheItem> allAccounts = this.Accessor.GetAllAccounts(account.HomeAccountId.Identifier, null);
				allAccounts.RemoveAll((MsalAccountCacheItem item) => !item.HomeAccountId.Equals(account.HomeAccountId.Identifier, StringComparison.OrdinalIgnoreCase) || !item.PreferredUsername.Equals(account.Username, StringComparison.OrdinalIgnoreCase));
				foreach (MsalAccountCacheItem msalAccountCacheItem in allAccounts)
				{
					this.Accessor.DeleteAccount(msalAccountCacheItem);
				}
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0003D7F8 File Offset: 0x0003B9F8
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x0003D800 File Offset: 0x0003BA00
		internal TokenCacheCallback BeforeAccess { get; set; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0003D809 File Offset: 0x0003BA09
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x0003D811 File Offset: 0x0003BA11
		internal TokenCacheCallback BeforeWrite { get; set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0003D81A File Offset: 0x0003BA1A
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x0003D822 File Offset: 0x0003BA22
		internal TokenCacheCallback AfterAccess { get; set; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0003D82B File Offset: 0x0003BA2B
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x0003D833 File Offset: 0x0003BA33
		internal Func<TokenCacheNotificationArgs, Task> AsyncBeforeAccess { get; set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0003D83C File Offset: 0x0003BA3C
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x0003D844 File Offset: 0x0003BA44
		internal Func<TokenCacheNotificationArgs, Task> AsyncAfterAccess { get; set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0003D84D File Offset: 0x0003BA4D
		// (set) Token: 0x060011E9 RID: 4585 RVA: 0x0003D855 File Offset: 0x0003BA55
		internal Func<TokenCacheNotificationArgs, Task> AsyncBeforeWrite { get; set; }

		// Token: 0x060011EA RID: 4586 RVA: 0x0003D85E File Offset: 0x0003BA5E
		bool ITokenCacheInternal.IsAppSubscribedToSerializationEvents()
		{
			return this.BeforeAccess != null || this.AfterAccess != null || this.BeforeWrite != null || this.AsyncBeforeAccess != null || this.AsyncAfterAccess != null || this.AsyncBeforeWrite != null;
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0003D894 File Offset: 0x0003BA94
		async Task ITokenCacheInternal.OnAfterAccessAsync(TokenCacheNotificationArgs args)
		{
			TokenCacheCallback afterAccess = this.AfterAccess;
			if (afterAccess != null)
			{
				afterAccess(args);
			}
			if (this.AsyncAfterAccess != null)
			{
				await this.AsyncAfterAccess(args).ConfigureAwait(false);
			}
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0003D8E0 File Offset: 0x0003BAE0
		async Task ITokenCacheInternal.OnBeforeAccessAsync(TokenCacheNotificationArgs args)
		{
			TokenCacheCallback beforeAccess = this.BeforeAccess;
			if (beforeAccess != null)
			{
				beforeAccess(args);
			}
			if (this.AsyncBeforeAccess != null)
			{
				await this.AsyncBeforeAccess(args).ConfigureAwait(false);
			}
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0003D92C File Offset: 0x0003BB2C
		async Task ITokenCacheInternal.OnBeforeWriteAsync(TokenCacheNotificationArgs args)
		{
			this.HasStateChanged = true;
			args.HasStateChanged = true;
			TokenCacheCallback beforeWrite = this.BeforeWrite;
			if (beforeWrite != null)
			{
				beforeWrite(args);
			}
			if (this.AsyncBeforeWrite != null)
			{
				await this.AsyncBeforeWrite(args).ConfigureAwait(false);
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0003D977 File Offset: 0x0003BB77
		public void SetBeforeAccess(TokenCacheCallback beforeAccess)
		{
			this.Validate();
			this.BeforeAccess = beforeAccess;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0003D986 File Offset: 0x0003BB86
		public void SetAfterAccess(TokenCacheCallback afterAccess)
		{
			this.Validate();
			this.AfterAccess = afterAccess;
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0003D995 File Offset: 0x0003BB95
		public void SetBeforeWrite(TokenCacheCallback beforeWrite)
		{
			this.Validate();
			this.BeforeWrite = beforeWrite;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0003D9A4 File Offset: 0x0003BBA4
		public void SetBeforeAccessAsync(Func<TokenCacheNotificationArgs, Task> beforeAccess)
		{
			this.Validate();
			this.AsyncBeforeAccess = beforeAccess;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0003D9B3 File Offset: 0x0003BBB3
		public void SetAfterAccessAsync(Func<TokenCacheNotificationArgs, Task> afterAccess)
		{
			this.Validate();
			this.AsyncAfterAccess = afterAccess;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0003D9C2 File Offset: 0x0003BBC2
		public void SetBeforeWriteAsync(Func<TokenCacheNotificationArgs, Task> beforeWrite)
		{
			this.Validate();
			this.AsyncBeforeWrite = beforeWrite;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0003D9D1 File Offset: 0x0003BBD1
		private void Validate()
		{
			if (this.ServiceBundle.Config.AccessorOptions != null)
			{
				throw new MsalClientException("static_cache_with_external_serialization", "You configured MSAL cache serialization at the same time with internal caching options. These are mutually exclusive. Use only one option. Web site and web api scenarios should rely on external cache serialization, as internal cache serialization cannot scale. See https://aka.ms/msal-net-token-cache-serialization .");
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0003D9F5 File Offset: 0x0003BBF5
		byte[] ITokenCacheSerializer.SerializeAdalV3()
		{
			return this.LegacyCachePersistence.LoadCache();
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0003DA02 File Offset: 0x0003BC02
		void ITokenCacheSerializer.DeserializeAdalV3(byte[] adalV3State)
		{
			this.LegacyCachePersistence.WriteCache(adalV3State);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0003DA10 File Offset: 0x0003BC10
		byte[] ITokenCacheSerializer.SerializeMsalV2()
		{
			return new TokenCacheDictionarySerializer(this.Accessor).Serialize(this._unknownNodes);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0003DA28 File Offset: 0x0003BC28
		void ITokenCacheSerializer.DeserializeMsalV2(byte[] msalV2State)
		{
			this._unknownNodes = new TokenCacheDictionarySerializer(this.Accessor).Deserialize(msalV2State, false);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0003DA42 File Offset: 0x0003BC42
		byte[] ITokenCacheSerializer.SerializeMsalV3()
		{
			return new TokenCacheJsonSerializer(this.Accessor).Serialize(this._unknownNodes);
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0003DA5A File Offset: 0x0003BC5A
		void ITokenCacheSerializer.DeserializeMsalV3(byte[] msalV3State, bool shouldClearExistingCache)
		{
			if (msalV3State == null || msalV3State.Length == 0)
			{
				if (shouldClearExistingCache)
				{
					this.Accessor.Clear(null);
				}
				return;
			}
			this._unknownNodes = new TokenCacheJsonSerializer(this.Accessor).Deserialize(msalV3State, shouldClearExistingCache);
		}

		// Token: 0x04000549 RID: 1353
		internal const int ExpirationTooLongInDays = 3650;

		// Token: 0x0400054A RID: 1354
		private readonly IFeatureFlags _featureFlags;

		// Token: 0x0400054B RID: 1355
		private volatile bool _hasStateChanged;

		// Token: 0x04000550 RID: 1360
		private readonly OptionalSemaphoreSlim _semaphoreSlim;

		// Token: 0x04000557 RID: 1367
		private IDictionary<string, JToken> _unknownNodes;

		// Token: 0x020003DB RID: 987
		// (Invoke) Token: 0x06001E28 RID: 7720
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Microsoft.Identity.Client.TokenCacheCallback instead. See https://aka.ms/msal-net-3x-cache-breaking-change", true)]
		public delegate void TokenCacheNotification(TokenCacheNotificationArgs args);
	}
}
