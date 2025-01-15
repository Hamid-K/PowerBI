using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Azure.Identity
{
	// Token: 0x02000074 RID: 116
	internal class MsalCacheReader
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		public MsalCacheReader(ITokenCache cache, string cachePath, int cacheRetryCount, TimeSpan cacheRetryDelay)
		{
			this._cachePath = cachePath;
			this._cacheLockPath = cachePath + ".lockfile";
			this._cacheRetryCount = cacheRetryCount;
			this._cacheRetryDelay = cacheRetryDelay;
			cache.SetBeforeAccessAsync(new Func<TokenCacheNotificationArgs, Task>(this.OnBeforeAccessAsync));
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000BA1C File Offset: 0x00009C1C
		private async Task OnBeforeAccessAsync(TokenCacheNotificationArgs args)
		{
			try
			{
				DateTime cacheTimestamp = File.GetLastWriteTimeUtc(this._cachePath);
				if (File.Exists(this._cachePath) && this._lastReadTime < cacheTimestamp)
				{
					SentinelFileLock sentinelFileLock = await SentinelFileLock.AcquireAsync(this._cacheLockPath, this._cacheRetryCount, this._cacheRetryDelay).ConfigureAwait(false);
					using (sentinelFileLock)
					{
						byte[] array = await this.ReadCacheFromProtectedStorageAsync().ConfigureAwait(false);
						this._lastReadTime = cacheTimestamp;
						if (array != null)
						{
							args.TokenCache.DeserializeMsalV3(array, false);
						}
					}
					SentinelFileLock cacheLock = null;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000BA68 File Offset: 0x00009C68
		private async Task<byte[]> ReadCacheFromProtectedStorageAsync()
		{
			byte[] array;
			using (FileStream file = File.OpenRead(this._cachePath))
			{
				byte[] protectedBytes = new byte[file.Length];
				await file.ReadAsync(protectedBytes, 0, protectedBytes.Length).ConfigureAwait(false);
				array = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);
			}
			return array;
		}

		// Token: 0x0400023F RID: 575
		private readonly string _cachePath;

		// Token: 0x04000240 RID: 576
		private readonly string _cacheLockPath;

		// Token: 0x04000241 RID: 577
		private readonly int _cacheRetryCount;

		// Token: 0x04000242 RID: 578
		private readonly TimeSpan _cacheRetryDelay;

		// Token: 0x04000243 RID: 579
		private DateTimeOffset _lastReadTime;
	}
}
