using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A2B RID: 6699
	internal sealed class NullPersistentCache : PersistentCache
	{
		// Token: 0x17002B0D RID: 11021
		// (get) Token: 0x0600A976 RID: 43382 RVA: 0x00018EAC File Offset: 0x000170AC
		// (set) Token: 0x0600A977 RID: 43383 RVA: 0x0000336E File Offset: 0x0000156E
		public override DateTime Staleness
		{
			get
			{
				return DateTime.UtcNow;
			}
			set
			{
			}
		}

		// Token: 0x17002B0E RID: 11022
		// (get) Token: 0x0600A978 RID: 43384 RVA: 0x001819C2 File Offset: 0x0017FBC2
		public override long MaxEntryLength
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17002B0F RID: 11023
		// (get) Token: 0x0600A979 RID: 43385 RVA: 0x00230C60 File Offset: 0x0022EE60
		public override CacheSize CacheSize
		{
			get
			{
				return default(CacheSize);
			}
		}

		// Token: 0x17002B10 RID: 11024
		// (get) Token: 0x0600A97A RID: 43386 RVA: 0x00230C76 File Offset: 0x0022EE76
		public override bool? UserSpecific
		{
			get
			{
				return new bool?(false);
			}
		}

		// Token: 0x0600A97B RID: 43387 RVA: 0x000BF254 File Offset: 0x000BD454
		public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			storage = null;
			return false;
		}

		// Token: 0x0600A97C RID: 43388 RVA: 0x00230C7E File Offset: 0x0022EE7E
		public override IStorage CreateStorage()
		{
			return new NullPersistentCache.NullStorage();
		}

		// Token: 0x0600A97D RID: 43389 RVA: 0x0000336E File Offset: 0x0000156E
		public override void CommitStorage(string key, CacheVersion maxVersion, IStorage storage)
		{
		}

		// Token: 0x0600A97E RID: 43390 RVA: 0x00230C85 File Offset: 0x0022EE85
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			return new NullPersistentCache.NullPagedStorage(pageSize, maxPageCount);
		}

		// Token: 0x0600A97F RID: 43391 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Purge()
		{
		}

		// Token: 0x0600A980 RID: 43392 RVA: 0x0000336E File Offset: 0x0000156E
		public override void Dispose()
		{
		}

		// Token: 0x17002B11 RID: 11025
		// (get) Token: 0x0600A981 RID: 43393 RVA: 0x00230C4F File Offset: 0x0022EE4F
		public override CacheVersion Current
		{
			get
			{
				return LongCacheVersion.New(0L);
			}
		}

		// Token: 0x0600A982 RID: 43394 RVA: 0x00230C90 File Offset: 0x0022EE90
		public override CacheVersion Increment()
		{
			return this.Current;
		}

		// Token: 0x02001A2C RID: 6700
		private sealed class NullStorage : IStorage, IDisposable
		{
			// Token: 0x17002B12 RID: 11026
			// (get) Token: 0x0600A984 RID: 43396 RVA: 0x00230C98 File Offset: 0x0022EE98
			public IEnumerable<int> StreamIds
			{
				get
				{
					return EmptyArray<int>.Instance;
				}
			}

			// Token: 0x0600A985 RID: 43397 RVA: 0x0000EE09 File Offset: 0x0000D009
			public Stream OpenStream(int id)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x0600A986 RID: 43398 RVA: 0x0023017C File Offset: 0x0022E37C
			public Stream CreateStream()
			{
				return new MemoryPagesStream();
			}

			// Token: 0x0600A987 RID: 43399 RVA: 0x00230C9F File Offset: 0x0022EE9F
			public Stream CommitStream(int id, Stream stream)
			{
				stream.Position = 0L;
				return stream;
			}

			// Token: 0x0600A988 RID: 43400 RVA: 0x0000336E File Offset: 0x0000156E
			public void Close()
			{
			}

			// Token: 0x0600A989 RID: 43401 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}
		}

		// Token: 0x02001A2D RID: 6701
		private sealed class NullPagedStorage : IPagedStorage, IDisposable
		{
			// Token: 0x0600A98B RID: 43403 RVA: 0x00230CAA File Offset: 0x0022EEAA
			public NullPagedStorage(int pageSize, int maxPageCount)
			{
				this.pageSize = pageSize;
				this.maxPageCount = maxPageCount;
			}

			// Token: 0x17002B13 RID: 11027
			// (get) Token: 0x0600A98C RID: 43404 RVA: 0x00230CC0 File Offset: 0x0022EEC0
			public int PageSize
			{
				get
				{
					return this.pageSize;
				}
			}

			// Token: 0x17002B14 RID: 11028
			// (get) Token: 0x0600A98D RID: 43405 RVA: 0x00230CC8 File Offset: 0x0022EEC8
			public int MaxPageCount
			{
				get
				{
					return this.maxPageCount;
				}
			}

			// Token: 0x0600A98E RID: 43406 RVA: 0x00230CD0 File Offset: 0x0022EED0
			public Stream OpenPage(int pageIndex, out bool created)
			{
				created = true;
				return new MemoryPagesStream();
			}

			// Token: 0x0600A98F RID: 43407 RVA: 0x0000336E File Offset: 0x0000156E
			public void CommitPage(Stream stream)
			{
			}

			// Token: 0x0600A990 RID: 43408 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x04005829 RID: 22569
			private readonly int pageSize;

			// Token: 0x0400582A RID: 22570
			private readonly int maxPageCount;
		}
	}
}
