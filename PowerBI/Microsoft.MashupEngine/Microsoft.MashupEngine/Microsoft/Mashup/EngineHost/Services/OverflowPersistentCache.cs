using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A34 RID: 6708
	public sealed class OverflowPersistentCache : CacheDelegatingPersistentCache
	{
		// Token: 0x0600A9B0 RID: 43440 RVA: 0x002310D7 File Offset: 0x0022F2D7
		public OverflowPersistentCache(PersistentCache cache, ITempPageService tempPageService)
			: base(cache)
		{
			this.tempPageService = tempPageService;
		}

		// Token: 0x0600A9B1 RID: 43441 RVA: 0x002310E8 File Offset: 0x0022F2E8
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			int num = (int)Math.Min((long)maxPageCount, this.MaxEntryLength / (long)pageSize);
			IPagedStorage pagedStorage = base.OpenStorage(key, maxStaleness, minVersion, pageSize, num);
			if (pagedStorage.MaxPageCount < maxPageCount)
			{
				pagedStorage = new OverflowPersistentCache.OverflowPagedStorage(this, pagedStorage, maxPageCount);
			}
			return pagedStorage;
		}

		// Token: 0x0400583E RID: 22590
		private readonly ITempPageService tempPageService;

		// Token: 0x02001A35 RID: 6709
		private sealed class OverflowPagedStorage : IPagedStorage, IDisposable
		{
			// Token: 0x0600A9B2 RID: 43442 RVA: 0x0023112B File Offset: 0x0022F32B
			public OverflowPagedStorage(OverflowPersistentCache cache, IPagedStorage pagedStorage, int maxPageCount)
			{
				this.cache = cache;
				this.pagedStorage = pagedStorage;
				this.maxPageCount = maxPageCount;
			}

			// Token: 0x17002B1D RID: 11037
			// (get) Token: 0x0600A9B3 RID: 43443 RVA: 0x00231148 File Offset: 0x0022F348
			public int PageSize
			{
				get
				{
					return this.pagedStorage.PageSize;
				}
			}

			// Token: 0x17002B1E RID: 11038
			// (get) Token: 0x0600A9B4 RID: 43444 RVA: 0x00231155 File Offset: 0x0022F355
			public int MaxPageCount
			{
				get
				{
					return this.maxPageCount;
				}
			}

			// Token: 0x0600A9B5 RID: 43445 RVA: 0x0023115D File Offset: 0x0022F35D
			public Stream OpenPage(int pageIndex, out bool created)
			{
				if (pageIndex < this.pagedStorage.MaxPageCount)
				{
					return this.pagedStorage.OpenPage(pageIndex, out created);
				}
				created = true;
				return new OverflowPersistentCache.OverflowPagedStorage.OverflowStream(this.cache.tempPageService.CreatePage((uint)this.PageSize));
			}

			// Token: 0x0600A9B6 RID: 43446 RVA: 0x00231199 File Offset: 0x0022F399
			public void CommitPage(Stream stream)
			{
				if (!(stream is OverflowPersistentCache.OverflowPagedStorage.OverflowStream))
				{
					this.pagedStorage.CommitPage(stream);
				}
			}

			// Token: 0x0600A9B7 RID: 43447 RVA: 0x002311AF File Offset: 0x0022F3AF
			public void Dispose()
			{
				this.pagedStorage.Dispose();
			}

			// Token: 0x0400583F RID: 22591
			private readonly OverflowPersistentCache cache;

			// Token: 0x04005840 RID: 22592
			private readonly IPagedStorage pagedStorage;

			// Token: 0x04005841 RID: 22593
			private readonly int maxPageCount;

			// Token: 0x02001A36 RID: 6710
			private sealed class OverflowStream : DelegatingStream
			{
				// Token: 0x0600A9B8 RID: 43448 RVA: 0x0000FF57 File Offset: 0x0000E157
				public OverflowStream(Stream stream)
					: base(stream)
				{
				}
			}
		}
	}
}
