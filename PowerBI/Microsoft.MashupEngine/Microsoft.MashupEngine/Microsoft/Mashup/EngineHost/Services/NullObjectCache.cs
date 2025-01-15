using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A2A RID: 6698
	internal sealed class NullObjectCache : IObjectCache, IDisposable, ICacheClock
	{
		// Token: 0x17002B0A RID: 11018
		// (get) Token: 0x0600A96B RID: 43371 RVA: 0x00018EAC File Offset: 0x000170AC
		// (set) Token: 0x0600A96C RID: 43372 RVA: 0x0000336E File Offset: 0x0000156E
		public DateTime Staleness
		{
			get
			{
				return DateTime.UtcNow;
			}
			set
			{
			}
		}

		// Token: 0x17002B0B RID: 11019
		// (get) Token: 0x0600A96D RID: 43373 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public ICacheClock CacheClock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600A96E RID: 43374 RVA: 0x000BF254 File Offset: 0x000BD454
		public bool TryGetValue(string key, DateTime maxStaleness, CacheVersion minVersion, out object value)
		{
			value = null;
			return false;
		}

		// Token: 0x0600A96F RID: 43375 RVA: 0x000BF254 File Offset: 0x000BD454
		public bool TryGetValue(StructuredCacheKey key, DateTime maxStaleness, CacheVersion minVersion, out object value)
		{
			value = null;
			return false;
		}

		// Token: 0x0600A970 RID: 43376 RVA: 0x0000336E File Offset: 0x0000156E
		public void CommitValue(string key, CacheVersion maxVersion, int size, object value)
		{
		}

		// Token: 0x0600A971 RID: 43377 RVA: 0x0000336E File Offset: 0x0000156E
		public void CommitValue(StructuredCacheKey key, CacheVersion maxVersion, int size, object value)
		{
		}

		// Token: 0x17002B0C RID: 11020
		// (get) Token: 0x0600A972 RID: 43378 RVA: 0x00230C4F File Offset: 0x0022EE4F
		public CacheVersion Current
		{
			get
			{
				return LongCacheVersion.New(0L);
			}
		}

		// Token: 0x0600A973 RID: 43379 RVA: 0x00230C58 File Offset: 0x0022EE58
		public CacheVersion Increment()
		{
			return this.Current;
		}

		// Token: 0x0600A974 RID: 43380 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}
	}
}
