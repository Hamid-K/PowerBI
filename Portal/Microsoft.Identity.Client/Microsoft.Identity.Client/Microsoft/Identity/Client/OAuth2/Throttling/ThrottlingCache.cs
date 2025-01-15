using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.OAuth2.Throttling
{
	// Token: 0x02000218 RID: 536
	internal class ThrottlingCache
	{
		// Token: 0x0600163B RID: 5691 RVA: 0x000497EC File Offset: 0x000479EC
		public ThrottlingCache(int? customCleanupIntervalMs = null)
		{
			this.s_cleanupCacheInterval = ((customCleanupIntervalMs != null) ? TimeSpan.FromMilliseconds((double)customCleanupIntervalMs.Value) : TimeSpan.FromMilliseconds(300000.0));
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00049844 File Offset: 0x00047A44
		public void AddAndCleanup(string key, ThrottlingCacheEntry entry, ILoggerAdapter logger)
		{
			this._cache.AddOrUpdate(key, entry, delegate(string _, ThrottlingCacheEntry oldEntry)
			{
				if (!(entry.CreationTime > oldEntry.CreationTime))
				{
					return oldEntry;
				}
				return entry;
			});
			this.CleanCache(logger);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00049884 File Offset: 0x00047A84
		public bool TryGetOrRemoveExpired(string key, ILoggerAdapter logger, out MsalServiceException ex)
		{
			ex = null;
			ThrottlingCacheEntry entry;
			if (!this._cache.TryGetValue(key, out entry))
			{
				return false;
			}
			logger.Info(() => string.Format("[Throttling] Entry found. Creation: {0} Expiration: {1} ", entry.CreationTime, entry.ExpirationTime));
			if (entry.IsExpired)
			{
				logger.Info(() => "[Throttling] Removing entry because it is expired");
				ThrottlingCacheEntry throttlingCacheEntry;
				this._cache.TryRemove(key, out throttlingCacheEntry);
				return false;
			}
			logger.InfoPii(() => "[Throttling] Returning valid entry for key " + key, () => "[Throttling] Returning valid entry.");
			ex = entry.Exception;
			return true;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00049958 File Offset: 0x00047B58
		public void Clear()
		{
			this._cache.Clear();
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00049965 File Offset: 0x00047B65
		public bool IsEmpty()
		{
			return !this._cache.Any<KeyValuePair<string, ThrottlingCacheEntry>>();
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x00049975 File Offset: 0x00047B75
		internal ConcurrentDictionary<string, ThrottlingCacheEntry> CacheForTest
		{
			get
			{
				return this._cache;
			}
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00049980 File Offset: 0x00047B80
		private void CleanCache(ILoggerAdapter logger)
		{
			if (this._lastCleanupTime + this.s_cleanupCacheInterval < DateTimeOffset.UtcNow && !this._cleanupInProgress)
			{
				logger.Verbose(() => "[Throttling] Acquiring lock to cleanup throttling state");
				object padlock = ThrottlingCache._padlock;
				lock (padlock)
				{
					if (!this._cleanupInProgress)
					{
						logger.Verbose(() => string.Format("[Throttling] Cache size before cleaning up {0}", this._cache.Count));
						this._cleanupInProgress = true;
						this.CleanupCacheNoLocks();
						this._lastCleanupTime = DateTimeOffset.UtcNow;
						this._cleanupInProgress = false;
						logger.Verbose(() => string.Format("[Throttling] Cache size after cleaning up {0}", this._cache.Count));
					}
				}
			}
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00049A5C File Offset: 0x00047C5C
		private void CleanupCacheNoLocks()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, ThrottlingCacheEntry> keyValuePair in this._cache)
			{
				if (keyValuePair.Value.IsExpired)
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (string text in list)
			{
				ThrottlingCacheEntry throttlingCacheEntry;
				this._cache.TryRemove(text, out throttlingCacheEntry);
			}
		}

		// Token: 0x0400096F RID: 2415
		internal const int DefaultCleanupIntervalMs = 300000;

		// Token: 0x04000970 RID: 2416
		private volatile bool _cleanupInProgress;

		// Token: 0x04000971 RID: 2417
		private static readonly object _padlock = new object();

		// Token: 0x04000972 RID: 2418
		private readonly TimeSpan s_cleanupCacheInterval;

		// Token: 0x04000973 RID: 2419
		private DateTimeOffset _lastCleanupTime = DateTimeOffset.UtcNow;

		// Token: 0x04000974 RID: 2420
		private readonly ConcurrentDictionary<string, ThrottlingCacheEntry> _cache = new ConcurrentDictionary<string, ThrottlingCacheEntry>();
	}
}
