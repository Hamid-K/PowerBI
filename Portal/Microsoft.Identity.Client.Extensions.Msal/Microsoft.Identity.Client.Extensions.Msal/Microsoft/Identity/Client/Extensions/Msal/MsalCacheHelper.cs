using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x02000017 RID: 23
	public class MsalCacheHelper
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002FB8 File Offset: 0x000011B8
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002FC0 File Offset: 0x000011C0
		internal CrossPlatLock CacheLock { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002FC9 File Offset: 0x000011C9
		internal Storage CacheStore { get; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000057 RID: 87 RVA: 0x00002FD1 File Offset: 0x000011D1
		// (remove) Token: 0x06000058 RID: 88 RVA: 0x00003002 File Offset: 0x00001202
		public event EventHandler<CacheChangedEventArgs> CacheChanged
		{
			add
			{
				if (!this._storageCreationProperties.IsCacheEventConfigured)
				{
					throw new InvalidOperationException("To use this event, please configure the clientId and the authority using  StorageCreationPropertiesBuilder.WithCacheChangedEvent");
				}
				this._cacheChangedEventHandler = (EventHandler<CacheChangedEventArgs>)Delegate.Combine(this._cacheChangedEventHandler, value);
			}
			remove
			{
				this._cacheChangedEventHandler = (EventHandler<CacheChangedEventArgs>)Delegate.Remove(this._cacheChangedEventHandler, value);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000301C File Offset: 0x0000121C
		private static async Task<HashSet<string>> GetAccountIdentifiersNoLockAsync(StorageCreationProperties storageCreationProperties, TraceSourceLogger logger)
		{
			HashSet<string> accountIdentifiers = new HashSet<string>();
			if (storageCreationProperties.IsCacheEventConfigured && File.Exists(storageCreationProperties.CacheFilePath))
			{
				IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder.Create(storageCreationProperties.ClientId).WithAuthority(storageCreationProperties.Authority, true).Build();
				publicClientApplication.UserTokenCache.SetBeforeAccess(delegate(TokenCacheNotificationArgs args)
				{
					Storage storage = null;
					try
					{
						storage = Storage.Create(storageCreationProperties, MsalCacheHelper.s_staticLogger.Value.Source);
						byte[] array = null;
						try
						{
							array = storage.ReadData();
						}
						catch
						{
						}
						if (array != null)
						{
							args.TokenCache.DeserializeMsalV3(array, false);
						}
					}
					catch (Exception ex)
					{
						TraceSourceLogger logger2 = logger;
						string text = "An error occured while reading the token cache: ";
						Exception ex2 = ex;
						logger2.LogError(text + ((ex2 != null) ? ex2.ToString() : null));
						logger.LogError("Deleting the token cache as it might be corrupt.");
						storage.Clear(true);
					}
				});
				foreach (IAccount account in await publicClientApplication.GetAccountsAsync().ConfigureAwait(false))
				{
					accountIdentifiers.Add(account.HomeAccountId.Identifier);
				}
			}
			return accountIdentifiers;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003068 File Offset: 0x00001268
		private MsalCacheHelper(StorageCreationProperties storageCreationProperties, TraceSource logger, HashSet<string> knownAccountIds, FileSystemWatcher cacheWatcher)
		{
			this._logger = ((logger == null) ? MsalCacheHelper.s_staticLogger.Value : new TraceSourceLogger(logger));
			this._storageCreationProperties = storageCreationProperties;
			this.CacheStore = Storage.Create(this._storageCreationProperties, this._logger.Source);
			this._knownAccountIds = knownAccountIds;
			this._cacheWatcher = cacheWatcher;
			if (this._cacheWatcher != null)
			{
				this._cacheWatcher.Changed += this.OnCacheFileChangedAsync;
				this._cacheWatcher.Deleted += this.OnCacheFileChangedAsync;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000310C File Offset: 0x0000130C
		private async void OnCacheFileChangedAsync(object sender, FileSystemEventArgs args)
		{
			EventHandler<CacheChangedEventArgs> cacheChangedEventHandler = this._cacheChangedEventHandler;
			if (((cacheChangedEventHandler != null) ? cacheChangedEventHandler.GetInvocationList() : null) != null)
			{
				EventHandler<CacheChangedEventArgs> cacheChangedEventHandler2 = this._cacheChangedEventHandler;
				if (cacheChangedEventHandler2 != null && cacheChangedEventHandler2.GetInvocationList().Length != 0)
				{
					try
					{
						IEnumerable<string> enumerable2;
						IEnumerable<string> enumerable3;
						using (MsalCacheHelper.CreateCrossPlatLock(this._storageCreationProperties))
						{
							HashSet<string> hashSet = await MsalCacheHelper.GetAccountIdentifiersNoLockAsync(this._storageCreationProperties, this._logger).ConfigureAwait(false);
							IEnumerable<string> enumerable = hashSet.Intersect(this._knownAccountIds);
							enumerable2 = this._knownAccountIds.Except(enumerable);
							enumerable3 = hashSet.Except(enumerable);
							this._knownAccountIds = hashSet;
						}
						CrossPlatLock crossPlatLock = null;
						if (enumerable3.Any<string>() || enumerable2.Any<string>())
						{
							this._cacheChangedEventHandler(sender, new CacheChangedEventArgs(enumerable3, enumerable2));
						}
					}
					catch (Exception ex)
					{
						this._logger.LogWarning(string.Format("Exception within File Watcher : {0}", ex));
					}
				}
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000314C File Offset: 0x0000134C
		internal MsalCacheHelper(ITokenCache userTokenCache, Storage store, TraceSource logger = null)
		{
			this._logger = ((logger == null) ? MsalCacheHelper.s_staticLogger.Value : new TraceSourceLogger(logger));
			this.CacheStore = store;
			this._storageCreationProperties = store.StorageCreationProperties;
			this.RegisterCache(userTokenCache);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000031A0 File Offset: 0x000013A0
		public static async Task<MsalCacheHelper> CreateAsync(StorageCreationProperties storageCreationProperties, TraceSource logger = null)
		{
			if (storageCreationProperties == null)
			{
				throw new ArgumentNullException("storageCreationProperties");
			}
			MsalCacheHelper msalCacheHelper2;
			using (MsalCacheHelper.CreateCrossPlatLock(storageCreationProperties))
			{
				TraceSourceLogger traceSourceLogger = ((logger == null) ? MsalCacheHelper.s_staticLogger.Value : new TraceSourceLogger(logger));
				HashSet<string> hashSet = null;
				FileSystemWatcher fileSystemWatcher = null;
				if (storageCreationProperties.IsCacheEventConfigured)
				{
					hashSet = await MsalCacheHelper.GetAccountIdentifiersNoLockAsync(storageCreationProperties, traceSourceLogger).ConfigureAwait(false);
					fileSystemWatcher = new FileSystemWatcher(storageCreationProperties.CacheDirectory, storageCreationProperties.CacheFileName);
				}
				MsalCacheHelper msalCacheHelper = new MsalCacheHelper(storageCreationProperties, logger, hashSet, fileSystemWatcher);
				try
				{
					if (!SharedUtilities.IsMonoPlatform() && storageCreationProperties.IsCacheEventConfigured)
					{
						fileSystemWatcher.EnableRaisingEvents = true;
					}
				}
				catch (PlatformNotSupportedException)
				{
					msalCacheHelper._logger.LogError("Cannot fire the CacheChanged event because the target framework does not support FileSystemWatcher. This is a known issue with Mono.");
				}
				msalCacheHelper2 = msalCacheHelper;
			}
			return msalCacheHelper2;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000031EB File Offset: 0x000013EB
		public static string UserRootDirectory
		{
			get
			{
				return SharedUtilities.GetUserRootDirectory();
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000031F4 File Offset: 0x000013F4
		public void RegisterCache(ITokenCache tokenCache)
		{
			if (tokenCache == null)
			{
				throw new ArgumentNullException("tokenCache");
			}
			this._logger.LogInformation("Registering token cache with on disk storage");
			tokenCache.SetBeforeAccess(new TokenCacheCallback(this.BeforeAccessNotification));
			tokenCache.SetAfterAccess(new TokenCacheCallback(this.AfterAccessNotification));
			this._logger.LogInformation("Done initializing");
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003253 File Offset: 0x00001453
		public void UnregisterCache(ITokenCache tokenCache)
		{
			if (tokenCache == null)
			{
				throw new ArgumentNullException("tokenCache");
			}
			tokenCache.SetBeforeAccess(null);
			tokenCache.SetAfterAccess(null);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003274 File Offset: 0x00001474
		[Obsolete("Applications should not delete the entire cache to log out all users. Instead, call app.RemoveAsync(IAccount) for each account in the cache. ", false)]
		public void Clear()
		{
			using (MsalCacheHelper.CreateCrossPlatLock(this._storageCreationProperties))
			{
				this.CacheStore.Clear(true);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000032B8 File Offset: 0x000014B8
		public byte[] LoadUnencryptedTokenCache()
		{
			byte[] array;
			using (MsalCacheHelper.CreateCrossPlatLock(this._storageCreationProperties))
			{
				array = this.CacheStore.ReadData();
			}
			return array;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000032FC File Offset: 0x000014FC
		public void SaveUnencryptedTokenCache(byte[] tokenCache)
		{
			using (MsalCacheHelper.CreateCrossPlatLock(this._storageCreationProperties))
			{
				this.CacheStore.WriteData(tokenCache);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003340 File Offset: 0x00001540
		private static CrossPlatLock CreateCrossPlatLock(StorageCreationProperties storageCreationProperties)
		{
			return new CrossPlatLock(storageCreationProperties.CacheFilePath + ".lockfile", storageCreationProperties.LockRetryDelay, storageCreationProperties.LockRetryCount);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003364 File Offset: 0x00001564
		internal void BeforeAccessNotification(TokenCacheNotificationArgs args)
		{
			this._logger.LogInformation("Before access");
			this._logger.LogInformation("Acquiring lock for token cache");
			this.CacheLock = MsalCacheHelper.CreateCrossPlatLock(this._storageCreationProperties);
			this._logger.LogInformation("Before access, the store has changed");
			byte[] array;
			try
			{
				array = this.CacheStore.ReadData();
			}
			catch (Exception)
			{
				this._logger.LogError("Could not read the token cache. Ignoring. See previous error message.");
				return;
			}
			this._logger.LogInformation(string.Format("Read '{0}' bytes from storage", (array != null) ? new int?(array.Length) : null));
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				try
				{
					this._logger.LogInformation("Deserializing the store");
					args.TokenCache.DeserializeMsalV3(array, true);
				}
				catch (Exception ex)
				{
					this._logger.LogError(string.Format("An exception was encountered while deserializing the {0} : {1}", "MsalCacheHelper", ex));
					this._logger.LogError("No data found in the store, clearing the cache in memory.");
					this.CacheStore.Clear(true);
					throw;
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000034A4 File Offset: 0x000016A4
		internal void AfterAccessNotification(TokenCacheNotificationArgs args)
		{
			try
			{
				this._logger.LogInformation("After access");
				if (args.HasStateChanged)
				{
					this._logger.LogInformation("After access, cache in memory HasChanged");
					byte[] array;
					try
					{
						array = args.TokenCache.SerializeMsalV3();
					}
					catch (Exception ex)
					{
						this._logger.LogError(string.Format("An exception was encountered while serializing the {0} : {1}", "MsalCacheHelper", ex));
						this._logger.LogError("No data found in the store, clearing the cache in memory.");
						this.CacheStore.Clear(true);
						throw;
					}
					if (array != null)
					{
						this._logger.LogInformation(string.Format("Serializing '{0}' bytes", array.Length));
						try
						{
							this.CacheStore.WriteData(array);
						}
						catch (Exception)
						{
							this._logger.LogError("Could not write the token cache. Ignoring. See previous error message.");
						}
					}
				}
			}
			finally
			{
				this.ReleaseFileLock();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003598 File Offset: 0x00001798
		private void ReleaseFileLock()
		{
			CrossPlatLock cacheLock = this.CacheLock;
			this.CacheLock = null;
			if (cacheLock != null)
			{
				cacheLock.Dispose();
			}
			this._logger.LogInformation("Released lock");
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000035C2 File Offset: 0x000017C2
		public void VerifyPersistence()
		{
			this.CacheStore.VerifyPersistence();
		}

		// Token: 0x0400005B RID: 91
		public const string LinuxKeyRingDefaultCollection = "default";

		// Token: 0x0400005C RID: 92
		public const string LinuxKeyRingSessionCollection = "session";

		// Token: 0x0400005D RID: 93
		private static readonly Lazy<TraceSourceLogger> s_staticLogger = new Lazy<TraceSourceLogger>(() => new TraceSourceLogger(EnvUtils.GetNewTraceSource("MsalCacheHelperSingleton")));

		// Token: 0x0400005E RID: 94
		private readonly object _lockObject = new object();

		// Token: 0x0400005F RID: 95
		private readonly StorageCreationProperties _storageCreationProperties;

		// Token: 0x04000062 RID: 98
		private readonly TraceSourceLogger _logger;

		// Token: 0x04000063 RID: 99
		private HashSet<string> _knownAccountIds;

		// Token: 0x04000064 RID: 100
		private readonly FileSystemWatcher _cacheWatcher;

		// Token: 0x04000065 RID: 101
		private EventHandler<CacheChangedEventArgs> _cacheChangedEventHandler;
	}
}
