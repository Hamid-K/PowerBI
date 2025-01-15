using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001999 RID: 6553
	public class ClearablePersistentCache : CacheDelegatingPersistentCache, IClearablePersistentCache, IPersistentCache, IDisposable
	{
		// Token: 0x0600A637 RID: 42551 RVA: 0x0022614C File Offset: 0x0022434C
		public ClearablePersistentCache(PersistentCache cache)
			: base(cache)
		{
			this.syncRoot = new object();
			this.observations = new Dictionary<string, ClearablePersistentCache.Observation>();
		}

		// Token: 0x0600A638 RID: 42552 RVA: 0x0022616C File Offset: 0x0022436C
		public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				minVersion = CacheVersion.GetMaxVersion(this.minVersion, minVersion);
			}
			if (!base.TryGetStorage(key, maxStaleness, minVersion, out storage))
			{
				obj = this.syncRoot;
				lock (obj)
				{
					this.Observe(key);
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600A639 RID: 42553 RVA: 0x002261F4 File Offset: 0x002243F4
		public override void CommitStorage(string key, CacheVersion maxVersion, IStorage storage)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				maxVersion = CacheVersion.GetMinVersion(this.Commit(key), maxVersion);
			}
			base.CommitStorage(key, maxVersion ?? base.CacheClock.Current, storage);
		}

		// Token: 0x0600A63A RID: 42554 RVA: 0x00226258 File Offset: 0x00224458
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				minVersion = CacheVersion.GetMaxVersion(this.minVersion, minVersion);
			}
			return base.OpenStorage(key, maxStaleness, minVersion, pageSize, maxPageCount);
		}

		// Token: 0x0600A63B RID: 42555 RVA: 0x002262B0 File Offset: 0x002244B0
		public virtual void Clear()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.minVersion = base.CacheClock.Increment();
			}
		}

		// Token: 0x17002A6A RID: 10858
		// (get) Token: 0x0600A63C RID: 42556 RVA: 0x002262FC File Offset: 0x002244FC
		public override CacheVersion Current
		{
			get
			{
				object obj = this.syncRoot;
				CacheVersion cacheVersion;
				lock (obj)
				{
					if (this.currentVersion == null)
					{
						this.currentVersion = base.Current;
					}
					cacheVersion = this.currentVersion;
				}
				return cacheVersion;
			}
		}

		// Token: 0x0600A63D RID: 42557 RVA: 0x00226354 File Offset: 0x00224554
		public override CacheVersion Increment()
		{
			CacheVersion cacheVersion = base.Increment();
			object obj = this.syncRoot;
			lock (obj)
			{
				this.currentVersion = cacheVersion;
			}
			return cacheVersion;
		}

		// Token: 0x0600A63E RID: 42558 RVA: 0x002263A0 File Offset: 0x002245A0
		private void Observe(string key)
		{
			ClearablePersistentCache.Observation observation;
			if (!this.observations.TryGetValue(key, out observation))
			{
				observation = new ClearablePersistentCache.Observation();
				this.observations.Add(key, observation);
			}
			if (observation.version == null)
			{
				observation.version = base.CacheClock.Current;
			}
			observation.refCount++;
		}

		// Token: 0x0600A63F RID: 42559 RVA: 0x002263F8 File Offset: 0x002245F8
		private CacheVersion Commit(string key)
		{
			ClearablePersistentCache.Observation observation;
			if (this.observations.TryGetValue(key, out observation))
			{
				observation.refCount--;
				if (observation.refCount == 0)
				{
					this.observations.Remove(key);
				}
				return observation.version;
			}
			return null;
		}

		// Token: 0x04005680 RID: 22144
		private readonly object syncRoot;

		// Token: 0x04005681 RID: 22145
		private readonly Dictionary<string, ClearablePersistentCache.Observation> observations;

		// Token: 0x04005682 RID: 22146
		private CacheVersion minVersion;

		// Token: 0x04005683 RID: 22147
		private CacheVersion currentVersion;

		// Token: 0x0200199A RID: 6554
		private sealed class Observation
		{
			// Token: 0x04005684 RID: 22148
			public CacheVersion version;

			// Token: 0x04005685 RID: 22149
			public int refCount;
		}
	}
}
