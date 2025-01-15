using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002A9 RID: 681
	internal class CacheSessionManager : ICacheSessionManager
	{
		// Token: 0x06001999 RID: 6553 RVA: 0x00053D4C File Offset: 0x00051F4C
		public CacheSessionManager(ITokenCacheInternal tokenCacheInternal, AuthenticationRequestParameters requestParams)
		{
			if (tokenCacheInternal == null)
			{
				throw new ArgumentNullException("tokenCacheInternal");
			}
			this.TokenCacheInternal = tokenCacheInternal;
			if (requestParams == null)
			{
				throw new ArgumentNullException("requestParams");
			}
			this._requestParams = requestParams;
			this.RequestContext = this._requestParams.RequestContext;
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x00053D9C File Offset: 0x00051F9C
		public RequestContext RequestContext { get; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x00053DA4 File Offset: 0x00051FA4
		public ITokenCacheInternal TokenCacheInternal { get; }

		// Token: 0x0600199C RID: 6556 RVA: 0x00053DAC File Offset: 0x00051FAC
		public async Task<MsalAccessTokenCacheItem> FindAccessTokenAsync()
		{
			await this.RefreshCacheForReadOperationsAsync().ConfigureAwait(false);
			return await this.TokenCacheInternal.FindAccessTokenAsync(this._requestParams).ConfigureAwait(false);
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00053DEF File Offset: 0x00051FEF
		public Task<Tuple<MsalAccessTokenCacheItem, MsalIdTokenCacheItem, Account>> SaveTokenResponseAsync(MsalTokenResponse tokenResponse)
		{
			return this.TokenCacheInternal.SaveTokenResponseAsync(this._requestParams, tokenResponse);
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00053E04 File Offset: 0x00052004
		public async Task<Account> GetAccountAssociatedWithAccessTokenAsync(MsalAccessTokenCacheItem msalAccessTokenCacheItem)
		{
			await this.RefreshCacheForReadOperationsAsync().ConfigureAwait(false);
			return await this.TokenCacheInternal.GetAccountAssociatedWithAccessTokenAsync(this._requestParams, msalAccessTokenCacheItem).ConfigureAwait(false);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00053E50 File Offset: 0x00052050
		public async Task<MsalIdTokenCacheItem> GetIdTokenCacheItemAsync(MsalAccessTokenCacheItem accessTokenCacheItem)
		{
			await this.RefreshCacheForReadOperationsAsync().ConfigureAwait(false);
			return this.TokenCacheInternal.GetIdTokenCacheItem(accessTokenCacheItem);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00053E9C File Offset: 0x0005209C
		public async Task<MsalRefreshTokenCacheItem> FindFamilyRefreshTokenAsync(string familyId)
		{
			await this.RefreshCacheForReadOperationsAsync().ConfigureAwait(false);
			if (string.IsNullOrEmpty(familyId))
			{
				throw new ArgumentNullException("familyId");
			}
			return await this.TokenCacheInternal.FindRefreshTokenAsync(this._requestParams, familyId).ConfigureAwait(false);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00053EE8 File Offset: 0x000520E8
		public async Task<MsalRefreshTokenCacheItem> FindRefreshTokenAsync()
		{
			await this.RefreshCacheForReadOperationsAsync().ConfigureAwait(false);
			return await this.TokenCacheInternal.FindRefreshTokenAsync(this._requestParams, null).ConfigureAwait(false);
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00053F2C File Offset: 0x0005212C
		public async Task<bool?> IsAppFociMemberAsync(string familyId)
		{
			await this.RefreshCacheForReadOperationsAsync().ConfigureAwait(false);
			return await this.TokenCacheInternal.IsFociMemberAsync(this._requestParams, familyId).ConfigureAwait(false);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00053F78 File Offset: 0x00052178
		public async Task<IEnumerable<IAccount>> GetAccountsAsync()
		{
			await this.RefreshCacheForReadOperationsAsync().ConfigureAwait(false);
			return await this.TokenCacheInternal.GetAccountsAsync(this._requestParams).ConfigureAwait(false);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x00053FBC File Offset: 0x000521BC
		private async Task RefreshCacheForReadOperationsAsync()
		{
			if (this.TokenCacheInternal.IsAppSubscribedToSerializationEvents())
			{
				if (!this._cacheRefreshedForRead)
				{
					CacheSessionManager.<>c__DisplayClass17_0 CS$<>8__locals1 = new CacheSessionManager.<>c__DisplayClass17_0();
					CS$<>8__locals1.<>4__this = this;
					this._requestParams.RequestContext.Logger.Verbose(() => "[Cache Session Manager] Entering the cache semaphore. " + this.TokenCacheInternal.Semaphore.GetCurrentCountLogMessage());
					await this.TokenCacheInternal.Semaphore.WaitAsync(this._requestParams.RequestContext.UserCancellationToken).ConfigureAwait(false);
					this._requestParams.RequestContext.Logger.Verbose(() => "[Cache Session Manager] Entered cache semaphore");
					CS$<>8__locals1.telemetryData = new TelemetryData();
					try
					{
						if (!this._cacheRefreshedForRead)
						{
							CS$<>8__locals1.key = CacheKeyFactory.GetKeyFromRequest(this._requestParams);
							object obj = null;
							try
							{
								ITokenCacheSerializer tokenCacheInternal = this.TokenCacheInternal;
								string clientId = this._requestParams.AppConfig.ClientId;
								IAccount account = this._requestParams.Account;
								bool flag = false;
								bool isApplicationCache = this.TokenCacheInternal.IsApplicationCache;
								string key = CS$<>8__locals1.key;
								bool flag2 = this.TokenCacheInternal.HasTokensNoLocks();
								CancellationToken userCancellationToken = this._requestParams.RequestContext.UserCancellationToken;
								TokenCacheNotificationArgs tokenCacheNotificationArgs = new TokenCacheNotificationArgs(tokenCacheInternal, clientId, account, flag, isApplicationCache, key, flag2, null, userCancellationToken, this._requestParams.RequestContext.CorrelationId, this._requestParams.Scope, this._requestParams.AuthorityManager.OriginalAuthority.TenantId, this._requestParams.RequestContext.Logger.IdentityLogger, this._requestParams.RequestContext.Logger.PiiLoggingEnabled, CS$<>8__locals1.telemetryData);
								MeasureDurationResult measureDurationResult = await this.TokenCacheInternal.OnBeforeAccessAsync(tokenCacheNotificationArgs).MeasureAsync().ConfigureAwait(false);
								this.RequestContext.ApiEvent.DurationInCacheInMs += measureDurationResult.Milliseconds;
							}
							catch (object obj)
							{
							}
							MeasureDurationResult measureDurationResult2 = await StopwatchService.MeasureCodeBlockAsync(delegate
							{
								CacheSessionManager.<>c__DisplayClass17_0.<<RefreshCacheForReadOperationsAsync>b__3>d <<RefreshCacheForReadOperationsAsync>b__3>d;
								<<RefreshCacheForReadOperationsAsync>b__3>d.<>t__builder = AsyncTaskMethodBuilder.Create();
								<<RefreshCacheForReadOperationsAsync>b__3>d.<>4__this = CS$<>8__locals1;
								<<RefreshCacheForReadOperationsAsync>b__3>d.<>1__state = -1;
								<<RefreshCacheForReadOperationsAsync>b__3>d.<>t__builder.Start<CacheSessionManager.<>c__DisplayClass17_0.<<RefreshCacheForReadOperationsAsync>b__3>d>(ref <<RefreshCacheForReadOperationsAsync>b__3>d);
								return <<RefreshCacheForReadOperationsAsync>b__3>d.<>t__builder.Task;
							}).ConfigureAwait(false);
							this.RequestContext.ApiEvent.DurationInCacheInMs += measureDurationResult2.Milliseconds;
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
							this._cacheRefreshedForRead = true;
						}
					}
					finally
					{
						this.TokenCacheInternal.Semaphore.Release();
						this._requestParams.RequestContext.Logger.Verbose(() => "[Cache Session Manager] Released cache semaphore");
						this.RequestContext.ApiEvent.CacheLevel = CS$<>8__locals1.telemetryData.CacheLevel;
					}
					CS$<>8__locals1 = null;
				}
			}
			else
			{
				this.RequestContext.ApiEvent.CacheLevel = CacheLevel.L1Cache;
			}
		}

		// Token: 0x04000B86 RID: 2950
		private readonly AuthenticationRequestParameters _requestParams;

		// Token: 0x04000B87 RID: 2951
		private bool _cacheRefreshedForRead;
	}
}
