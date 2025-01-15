using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A2E RID: 6702
	public sealed class ObjectCache : IObjectCache, IDisposable, ICacheClock
	{
		// Token: 0x0600A991 RID: 43409 RVA: 0x00230CDA File Offset: 0x0022EEDA
		public ObjectCache(ObjectStorage objectStorage, long maxCacheSize, long trimCacheSize, bool userSpecific)
		{
			this.syncRoot = new object();
			this.objectStorage = objectStorage;
			this.maxCacheSize = maxCacheSize;
			this.trimCacheSize = trimCacheSize;
			this.userSpecific = userSpecific;
		}

		// Token: 0x17002B15 RID: 11029
		// (get) Token: 0x0600A992 RID: 43410 RVA: 0x00230D0C File Offset: 0x0022EF0C
		// (set) Token: 0x0600A993 RID: 43411 RVA: 0x00230D50 File Offset: 0x0022EF50
		public DateTime Staleness
		{
			get
			{
				object obj = this.syncRoot;
				DateTime dateTime;
				lock (obj)
				{
					dateTime = this.staleness;
				}
				return dateTime;
			}
			set
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (value < this.staleness)
					{
						this.staleness = value;
					}
				}
			}
		}

		// Token: 0x17002B16 RID: 11030
		// (get) Token: 0x0600A994 RID: 43412 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public ICacheClock CacheClock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600A995 RID: 43413 RVA: 0x00230DA0 File Offset: 0x0022EFA0
		public bool TryGetValue(string key, DateTime maxStaleness, CacheVersion minVersion, out object value)
		{
			DateTime dateTime;
			long num;
			if (this.objectStorage.TryGetValue(key, out value, out dateTime, out num) && dateTime >= maxStaleness && num >= LongCacheVersion.ToLong(minVersion))
			{
				this.Staleness = dateTime;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600A996 RID: 43414 RVA: 0x00230DE1 File Offset: 0x0022EFE1
		public bool TryGetValue(StructuredCacheKey key, DateTime maxStaleness, CacheVersion minVersion, out object value)
		{
			return this.TryGetValue(key.GetCacheKey(this.userSpecific), maxStaleness, minVersion, out value);
		}

		// Token: 0x0600A997 RID: 43415 RVA: 0x00230DFC File Offset: 0x0022EFFC
		public void CommitValue(string key, CacheVersion maxVersion, int size, object value)
		{
			if (maxVersion == null)
			{
				throw new NotSupportedException();
			}
			long num = LongCacheVersion.ToLong(maxVersion);
			object obj;
			DateTime dateTime;
			long num2;
			if (!this.objectStorage.TryGetValue(key, out obj, out dateTime, out num2) || num2 <= num)
			{
				if ((long)size + this.objectStorage.CacheSize.TotalSize > this.maxCacheSize)
				{
					this.objectStorage.Purge(this.trimCacheSize - (long)size);
				}
				if ((long)size <= this.maxCacheSize)
				{
					this.objectStorage.CommitValue(key, value, (long)size, DateTime.UtcNow, num);
				}
			}
		}

		// Token: 0x0600A998 RID: 43416 RVA: 0x00230E84 File Offset: 0x0022F084
		public void CommitValue(StructuredCacheKey key, CacheVersion maxVersion, int size, object value)
		{
			this.CommitValue(key.GetCacheKey(this.userSpecific), maxVersion, size, value);
		}

		// Token: 0x0600A999 RID: 43417 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x17002B17 RID: 11031
		// (get) Token: 0x0600A99A RID: 43418 RVA: 0x00230E9C File Offset: 0x0022F09C
		public CacheVersion Current
		{
			get
			{
				return LongCacheVersion.New(this.objectStorage.CurrentVersion);
			}
		}

		// Token: 0x0600A99B RID: 43419 RVA: 0x00230EAE File Offset: 0x0022F0AE
		public CacheVersion Increment()
		{
			return LongCacheVersion.New(this.objectStorage.IncrementVersion());
		}

		// Token: 0x0400582B RID: 22571
		private readonly object syncRoot;

		// Token: 0x0400582C RID: 22572
		private readonly ObjectStorage objectStorage;

		// Token: 0x0400582D RID: 22573
		private readonly long maxCacheSize;

		// Token: 0x0400582E RID: 22574
		private readonly long trimCacheSize;

		// Token: 0x0400582F RID: 22575
		private readonly bool userSpecific;

		// Token: 0x04005830 RID: 22576
		private DateTime staleness;
	}
}
