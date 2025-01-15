using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B47 RID: 6983
	internal sealed class TracingPersistentCache : CacheDelegatingPersistentCache
	{
		// Token: 0x0600AEBA RID: 44730 RVA: 0x0023C747 File Offset: 0x0023A947
		public TracingPersistentCache(PersistentCache cache, IEvaluationConstants evaluationConstants, string identity)
			: base(cache)
		{
			this.cacheStats = new CacheStats(identity, evaluationConstants, 100, 1000);
		}

		// Token: 0x0600AEBB RID: 44731 RVA: 0x0023C764 File Offset: 0x0023A964
		public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			bool flag = base.TryGetStorage(key, maxStaleness, minVersion, out storage);
			this.cacheStats.Access(flag);
			return flag;
		}

		// Token: 0x0600AEBC RID: 44732 RVA: 0x0023C78A File Offset: 0x0023A98A
		public override void CommitStorage(string key, CacheVersion maxVersion, IStorage storage)
		{
			base.CommitStorage(key, maxVersion, storage);
			this.cacheStats.Size(this.CacheSize);
		}

		// Token: 0x0600AEBD RID: 44733 RVA: 0x0023C7A8 File Offset: 0x0023A9A8
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			IPagedStorage pagedStorage = this.Cache.OpenStorage(key, maxStaleness, minVersion, pageSize, maxPageCount);
			return new TracingPersistentCache.TracingPagedStorage(this, pagedStorage);
		}

		// Token: 0x04005A17 RID: 23063
		private const int traceFrequency = 100;

		// Token: 0x04005A18 RID: 23064
		private const int resetFrequency = 1000;

		// Token: 0x04005A19 RID: 23065
		private readonly CacheStats cacheStats;

		// Token: 0x02001B48 RID: 6984
		private sealed class TracingPagedStorage : IPagedStorage, IDisposable
		{
			// Token: 0x0600AEBE RID: 44734 RVA: 0x0023C7CF File Offset: 0x0023A9CF
			public TracingPagedStorage(TracingPersistentCache cache, IPagedStorage storage)
			{
				this.cache = cache;
				this.storage = storage;
			}

			// Token: 0x17002BD3 RID: 11219
			// (get) Token: 0x0600AEBF RID: 44735 RVA: 0x0023C7E5 File Offset: 0x0023A9E5
			public int PageSize
			{
				get
				{
					return this.storage.PageSize;
				}
			}

			// Token: 0x17002BD4 RID: 11220
			// (get) Token: 0x0600AEC0 RID: 44736 RVA: 0x0023C7F2 File Offset: 0x0023A9F2
			public int MaxPageCount
			{
				get
				{
					return this.storage.MaxPageCount;
				}
			}

			// Token: 0x0600AEC1 RID: 44737 RVA: 0x0023C7FF File Offset: 0x0023A9FF
			public Stream OpenPage(int pageIndex, out bool created)
			{
				Stream stream = this.storage.OpenPage(pageIndex, out created);
				this.cache.cacheStats.Access(!created);
				return stream;
			}

			// Token: 0x0600AEC2 RID: 44738 RVA: 0x0023C823 File Offset: 0x0023AA23
			public void CommitPage(Stream stream)
			{
				this.storage.CommitPage(stream);
			}

			// Token: 0x0600AEC3 RID: 44739 RVA: 0x0023C831 File Offset: 0x0023AA31
			public void Dispose()
			{
				this.storage.Dispose();
				this.cache.cacheStats.Size(this.cache.CacheSize);
			}

			// Token: 0x04005A1A RID: 23066
			private readonly TracingPersistentCache cache;

			// Token: 0x04005A1B RID: 23067
			private readonly IPagedStorage storage;
		}
	}
}
