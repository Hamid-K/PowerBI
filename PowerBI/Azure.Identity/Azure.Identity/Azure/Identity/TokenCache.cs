using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;

namespace Azure.Identity
{
	// Token: 0x02000082 RID: 130
	internal class TokenCache
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000D733 File Offset: 0x0000B933
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0000D73B File Offset: 0x0000B93B
		internal byte[] Data { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000D744 File Offset: 0x0000B944
		internal bool IsCaeEnabled { get; }

		// Token: 0x0600045E RID: 1118 RVA: 0x0000D74C File Offset: 0x0000B94C
		public TokenCache(TokenCachePersistenceOptions options = null, bool enableCae = false)
			: this(options, null, null, enableCae)
		{
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000D758 File Offset: 0x0000B958
		internal TokenCache(TokenCachePersistenceOptions options, MsalCacheHelperWrapper cacheHelperWrapper, Func<IPublicClientApplication> publicApplicationFactory = null, bool enableCae = false)
		{
			this._cacheHelperWrapper = cacheHelperWrapper ?? new MsalCacheHelperWrapper();
			Func<IPublicClientApplication> func = publicApplicationFactory;
			if (publicApplicationFactory == null && (func = TokenCache.<>c.<>9__18_0) == null)
			{
				func = (TokenCache.<>c.<>9__18_0 = () => PublicClientApplicationBuilder.Create(Guid.NewGuid().ToString()).Build());
			}
			this._publicClientApplicationFactory = func;
			this.IsCaeEnabled = enableCae;
			UnsafeTokenCacheOptions unsafeTokenCacheOptions = options as UnsafeTokenCacheOptions;
			if (unsafeTokenCacheOptions != null)
			{
				this.TokenCacheUpdatedAsync = new Func<TokenCacheUpdatedArgs, Task>(unsafeTokenCacheOptions.TokenCacheUpdatedAsync);
				this.RefreshCacheFromOptionsAsync = new Func<TokenCacheRefreshArgs, CancellationToken, Task<TokenCacheData>>(unsafeTokenCacheOptions.RefreshCacheAsync);
				this._lastUpdated = DateTimeOffset.UtcNow;
				this._cacheAccessMap = new ConditionalWeakTable<object, TokenCache.CacheTimestamp>();
				return;
			}
			this._allowUnencryptedStorage = options != null && options.UnsafeAllowUnencryptedStorage;
			this._name = (((options != null) ? options.Name : null) ?? "msal.cache") + (enableCae ? ".cae" : ".nocae");
			this._persistToDisk = true;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000D858 File Offset: 0x0000BA58
		internal virtual async Task RegisterCache(bool async, ITokenCache tokenCache, CancellationToken cancellationToken)
		{
			if (this._persistToDisk)
			{
				(await this.GetCacheHelperAsync(async, cancellationToken).ConfigureAwait(false)).RegisterCache(tokenCache);
			}
			else
			{
				if (async)
				{
					await this._lock.WaitAsync(cancellationToken).ConfigureAwait(false);
				}
				else
				{
					this._lock.Wait(cancellationToken);
				}
				try
				{
					TokenCache.CacheTimestamp cacheTimestamp;
					if (!this._cacheAccessMap.TryGetValue(tokenCache, out cacheTimestamp))
					{
						tokenCache.SetBeforeAccessAsync(new Func<TokenCacheNotificationArgs, Task>(this.OnBeforeCacheAccessAsync));
						tokenCache.SetAfterAccessAsync(new Func<TokenCacheNotificationArgs, Task>(this.OnAfterCacheAccessAsync));
						this._cacheAccessMap.Add(tokenCache, new TokenCache.CacheTimestamp());
					}
				}
				finally
				{
					this._lock.Release();
				}
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000D8B4 File Offset: 0x0000BAB4
		private async Task OnBeforeCacheAccessAsync(TokenCacheNotificationArgs args)
		{
			await this._lock.WaitAsync().ConfigureAwait(false);
			try
			{
				if (this.RefreshCacheFromOptionsAsync != null)
				{
					ConfiguredTaskAwaitable<TokenCacheData>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.RefreshCacheFromOptionsAsync(new TokenCacheRefreshArgs(args, this.IsCaeEnabled), default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						ConfiguredTaskAwaitable<TokenCacheData>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<TokenCacheData>.ConfiguredTaskAwaiter);
					}
					this.Data = configuredTaskAwaiter.GetResult().CacheBytes.ToArray();
				}
				args.TokenCache.DeserializeMsalV3(this.Data, true);
				this._cacheAccessMap.GetOrCreateValue(args.TokenCache).Update();
			}
			finally
			{
				this._lock.Release();
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000D900 File Offset: 0x0000BB00
		private async Task OnAfterCacheAccessAsync(TokenCacheNotificationArgs args)
		{
			if (args.HasStateChanged)
			{
				await this.UpdateCacheDataAsync(args.TokenCache).ConfigureAwait(false);
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000D94C File Offset: 0x0000BB4C
		private async Task UpdateCacheDataAsync(ITokenCacheSerializer tokenCache)
		{
			await this._lock.WaitAsync().ConfigureAwait(false);
			try
			{
				TokenCache.CacheTimestamp cacheTimestamp;
				if (!this._cacheAccessMap.TryGetValue(tokenCache, out cacheTimestamp) || cacheTimestamp.Value < this._lastUpdated)
				{
					this.Data = await this.MergeCacheData(this.Data, tokenCache.SerializeMsalV3()).ConfigureAwait(false);
				}
				else
				{
					this.Data = tokenCache.SerializeMsalV3();
				}
				if (this.TokenCacheUpdatedAsync != null)
				{
					byte[] array = this.Data.ToArray<byte>();
					await this.TokenCacheUpdatedAsync(new TokenCacheUpdatedArgs(array, this.IsCaeEnabled)).ConfigureAwait(false);
				}
				this._lastUpdated = this._cacheAccessMap.GetOrCreateValue(tokenCache).Update();
			}
			finally
			{
				this._lock.Release();
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000D998 File Offset: 0x0000BB98
		private async Task<byte[]> MergeCacheData(byte[] cacheA, byte[] cacheB)
		{
			byte[] merged = null;
			IPublicClientApplication client = this._publicClientApplicationFactory();
			client.UserTokenCache.SetBeforeAccess(delegate(TokenCacheNotificationArgs args)
			{
				args.TokenCache.DeserializeMsalV3(cacheA, false);
			});
			await client.GetAccountsAsync().ConfigureAwait(false);
			client.UserTokenCache.SetBeforeAccess(delegate(TokenCacheNotificationArgs args)
			{
				args.TokenCache.DeserializeMsalV3(cacheB, false);
			});
			client.UserTokenCache.SetAfterAccess(delegate(TokenCacheNotificationArgs args)
			{
				merged = args.TokenCache.SerializeMsalV3();
			});
			await client.GetAccountsAsync().ConfigureAwait(false);
			return merged;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000D9EC File Offset: 0x0000BBEC
		private Task<MsalCacheHelperWrapper> GetCacheHelperAsync(bool async, CancellationToken cancellationToken)
		{
			TokenCache.<GetCacheHelperAsync>d__26 <GetCacheHelperAsync>d__;
			<GetCacheHelperAsync>d__.<>t__builder = AsyncTaskMethodBuilder<MsalCacheHelperWrapper>.Create();
			<GetCacheHelperAsync>d__.<>4__this = this;
			<GetCacheHelperAsync>d__.async = async;
			<GetCacheHelperAsync>d__.cancellationToken = cancellationToken;
			<GetCacheHelperAsync>d__.<>1__state = -1;
			<GetCacheHelperAsync>d__.<>t__builder.Start<TokenCache.<GetCacheHelperAsync>d__26>(ref <GetCacheHelperAsync>d__);
			return <GetCacheHelperAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000DA40 File Offset: 0x0000BC40
		private async Task<MsalCacheHelperWrapper> GetProtectedCacheHelperAsync(bool async, string name)
		{
			StorageCreationProperties storageCreationProperties = new StorageCreationPropertiesBuilder(name, Constants.DefaultMsalTokenCacheDirectory).WithMacKeyChain("Microsoft.Developer.IdentityService", name).WithLinuxKeyring("msal.cache", "default", name, Constants.DefaultMsaltokenCacheKeyringAttribute1, Constants.DefaultMsaltokenCacheKeyringAttribute2).Build();
			return await this.InitializeCacheHelper(async, storageCreationProperties).ConfigureAwait(false);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000DA94 File Offset: 0x0000BC94
		private async Task<MsalCacheHelperWrapper> GetFallbackCacheHelperAsync(bool async, string name = "msal.cache")
		{
			StorageCreationProperties storageCreationProperties = new StorageCreationPropertiesBuilder(name, Constants.DefaultMsalTokenCacheDirectory).WithMacKeyChain("Microsoft.Developer.IdentityService", name).WithLinuxUnprotectedFile().Build();
			return await this.InitializeCacheHelper(async, storageCreationProperties).ConfigureAwait(false);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000DAE8 File Offset: 0x0000BCE8
		private async Task<MsalCacheHelperWrapper> InitializeCacheHelper(bool async, StorageCreationProperties storageProperties)
		{
			if (async)
			{
				await this._cacheHelperWrapper.InitializeAsync(storageProperties, null).ConfigureAwait(false);
			}
			else
			{
				this._cacheHelperWrapper.InitializeAsync(storageProperties, null).GetAwaiter().GetResult();
			}
			return this._cacheHelperWrapper;
		}

		// Token: 0x04000275 RID: 629
		private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

		// Token: 0x04000276 RID: 630
		private DateTimeOffset _lastUpdated;

		// Token: 0x04000277 RID: 631
		private ConditionalWeakTable<object, TokenCache.CacheTimestamp> _cacheAccessMap;

		// Token: 0x04000278 RID: 632
		internal Func<IPublicClientApplication> _publicClientApplicationFactory;

		// Token: 0x04000279 RID: 633
		private readonly bool _allowUnencryptedStorage;

		// Token: 0x0400027A RID: 634
		private readonly string _name;

		// Token: 0x0400027B RID: 635
		private readonly bool _persistToDisk;

		// Token: 0x0400027C RID: 636
		private AsyncLockWithValue<MsalCacheHelperWrapper> cacheHelperLock = new AsyncLockWithValue<MsalCacheHelperWrapper>();

		// Token: 0x0400027D RID: 637
		private readonly MsalCacheHelperWrapper _cacheHelperWrapper;

		// Token: 0x04000280 RID: 640
		internal Func<TokenCacheUpdatedArgs, Task> TokenCacheUpdatedAsync;

		// Token: 0x04000281 RID: 641
		internal Func<TokenCacheRefreshArgs, CancellationToken, Task<TokenCacheData>> RefreshCacheFromOptionsAsync;

		// Token: 0x02000124 RID: 292
		private class CacheTimestamp
		{
			// Token: 0x06000612 RID: 1554 RVA: 0x0001AD55 File Offset: 0x00018F55
			public CacheTimestamp()
			{
				this.Update();
			}

			// Token: 0x06000613 RID: 1555 RVA: 0x0001AD64 File Offset: 0x00018F64
			public DateTimeOffset Update()
			{
				this._timestamp = DateTimeOffset.UtcNow;
				return this._timestamp;
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x06000614 RID: 1556 RVA: 0x0001AD77 File Offset: 0x00018F77
			public DateTimeOffset Value
			{
				get
				{
					return this._timestamp;
				}
			}

			// Token: 0x04000654 RID: 1620
			private DateTimeOffset _timestamp;
		}
	}
}
