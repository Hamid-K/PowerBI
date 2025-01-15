using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001997 RID: 6551
	public class ClearableObjectCache : CacheDelegatingObjectCache, IClearableObjectCache, IObjectCache, IDisposable, ICacheClock
	{
		// Token: 0x0600A62D RID: 42541 RVA: 0x00225EA2 File Offset: 0x002240A2
		public ClearableObjectCache(IObjectCache cache)
			: base(cache)
		{
			this.syncRoot = new object();
			this.observations = new Dictionary<string, ClearableObjectCache.Observation>();
		}

		// Token: 0x17002A68 RID: 10856
		// (get) Token: 0x0600A62E RID: 42542 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override ICacheClock CacheClock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600A62F RID: 42543 RVA: 0x00225EC4 File Offset: 0x002240C4
		public override bool TryGetValue(string key, DateTime maxStaleness, CacheVersion minVersion, out object value)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				minVersion = CacheVersion.GetMaxVersion(this.minVersion, minVersion);
			}
			if (!base.TryGetValue(key, maxStaleness, minVersion, out value))
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

		// Token: 0x0600A630 RID: 42544 RVA: 0x00225F4C File Offset: 0x0022414C
		public override void CommitValue(string key, CacheVersion maxVersion, int size, object value)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				maxVersion = CacheVersion.GetMinVersion(this.Commit(key), maxVersion);
			}
			base.CommitValue(key, maxVersion ?? this.CacheClock.Current, size, value);
		}

		// Token: 0x0600A631 RID: 42545 RVA: 0x00225FB0 File Offset: 0x002241B0
		public virtual void Clear()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.minVersion = this.CacheClock.Increment();
			}
		}

		// Token: 0x17002A69 RID: 10857
		// (get) Token: 0x0600A632 RID: 42546 RVA: 0x00225FFC File Offset: 0x002241FC
		public virtual CacheVersion Current
		{
			get
			{
				object obj = this.syncRoot;
				CacheVersion cacheVersion;
				lock (obj)
				{
					if (this.currentVersion == null)
					{
						this.currentVersion = this.Cache.CacheClock.Current;
					}
					cacheVersion = this.currentVersion;
				}
				return cacheVersion;
			}
		}

		// Token: 0x0600A633 RID: 42547 RVA: 0x0022605C File Offset: 0x0022425C
		public virtual CacheVersion Increment()
		{
			CacheVersion cacheVersion = this.Cache.CacheClock.Increment();
			object obj = this.syncRoot;
			lock (obj)
			{
				this.currentVersion = cacheVersion;
			}
			return cacheVersion;
		}

		// Token: 0x0600A634 RID: 42548 RVA: 0x002260B0 File Offset: 0x002242B0
		private void Observe(string key)
		{
			ClearableObjectCache.Observation observation;
			if (!this.observations.TryGetValue(key, out observation))
			{
				observation = new ClearableObjectCache.Observation();
				this.observations.Add(key, observation);
			}
			if (observation.version == null)
			{
				observation.version = this.Current;
			}
			observation.refCount++;
		}

		// Token: 0x0600A635 RID: 42549 RVA: 0x00226104 File Offset: 0x00224304
		private CacheVersion Commit(string key)
		{
			ClearableObjectCache.Observation observation;
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

		// Token: 0x0400567A RID: 22138
		private readonly object syncRoot;

		// Token: 0x0400567B RID: 22139
		private readonly Dictionary<string, ClearableObjectCache.Observation> observations;

		// Token: 0x0400567C RID: 22140
		private CacheVersion minVersion;

		// Token: 0x0400567D RID: 22141
		private CacheVersion currentVersion;

		// Token: 0x02001998 RID: 6552
		private sealed class Observation
		{
			// Token: 0x0400567E RID: 22142
			public CacheVersion version;

			// Token: 0x0400567F RID: 22143
			public int refCount;
		}
	}
}
