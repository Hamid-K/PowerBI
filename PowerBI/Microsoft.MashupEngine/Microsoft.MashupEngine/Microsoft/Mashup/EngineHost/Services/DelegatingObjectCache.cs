using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019AE RID: 6574
	public abstract class DelegatingObjectCache : IObjectCache, IDisposable
	{
		// Token: 0x17002A81 RID: 10881
		// (get) Token: 0x0600A697 RID: 42647
		protected abstract IObjectCache Cache { get; }

		// Token: 0x17002A82 RID: 10882
		// (get) Token: 0x0600A698 RID: 42648 RVA: 0x00227620 File Offset: 0x00225820
		public virtual DateTime Staleness
		{
			get
			{
				return this.Cache.Staleness;
			}
		}

		// Token: 0x17002A83 RID: 10883
		// (get) Token: 0x0600A699 RID: 42649 RVA: 0x0022762D File Offset: 0x0022582D
		public virtual ICacheClock CacheClock
		{
			get
			{
				return this.Cache.CacheClock;
			}
		}

		// Token: 0x0600A69A RID: 42650 RVA: 0x0022763A File Offset: 0x0022583A
		public virtual bool TryGetValue(string key, DateTime maxStaleness, CacheVersion minVersion, out object value)
		{
			return this.Cache.TryGetValue(key, maxStaleness, minVersion, out value);
		}

		// Token: 0x0600A69B RID: 42651 RVA: 0x0022764C File Offset: 0x0022584C
		public virtual bool TryGetValue(StructuredCacheKey key, DateTime maxStaleness, CacheVersion minVersion, out object value)
		{
			return this.Cache.TryGetValue(key, maxStaleness, minVersion, out value);
		}

		// Token: 0x0600A69C RID: 42652 RVA: 0x0022765E File Offset: 0x0022585E
		public virtual void CommitValue(string key, CacheVersion maxVersion, int size, object value)
		{
			this.Cache.CommitValue(key, maxVersion, size, value);
		}

		// Token: 0x0600A69D RID: 42653 RVA: 0x00227670 File Offset: 0x00225870
		public virtual void CommitValue(StructuredCacheKey key, CacheVersion maxVersion, int size, object value)
		{
			this.Cache.CommitValue(key, maxVersion, size, value);
		}

		// Token: 0x0600A69E RID: 42654 RVA: 0x00227682 File Offset: 0x00225882
		public virtual void Dispose()
		{
			this.Cache.Dispose();
		}
	}
}
