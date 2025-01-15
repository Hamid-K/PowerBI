using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A1D RID: 6685
	public class MemoryPersistentCache : PersistentCache
	{
		// Token: 0x0600A927 RID: 43303 RVA: 0x0022FE18 File Offset: 0x0022E018
		public MemoryPersistentCache(ObjectStorage objectStorage, ITempPageService tempPageService, long maxCacheSize, long trimCacheSize, long maxEntryLength, bool userSpecific)
		{
			this.syncRoot = new object();
			this.overflowCache = new OverflowPersistentCache(new NullPersistentCache(), tempPageService);
			this.objectStorage = objectStorage;
			this.maxCacheSize = maxCacheSize;
			this.trimCacheSize = trimCacheSize;
			this.maxEntryLength = maxEntryLength;
			this.userSpecific = userSpecific;
			this.staleness = DateTime.UtcNow;
		}

		// Token: 0x17002AFE RID: 11006
		// (get) Token: 0x0600A928 RID: 43304 RVA: 0x0022FE78 File Offset: 0x0022E078
		// (set) Token: 0x0600A929 RID: 43305 RVA: 0x0022FEBC File Offset: 0x0022E0BC
		public override DateTime Staleness
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

		// Token: 0x17002AFF RID: 11007
		// (get) Token: 0x0600A92A RID: 43306 RVA: 0x0022FF0C File Offset: 0x0022E10C
		public override long MaxEntryLength
		{
			get
			{
				return this.maxEntryLength;
			}
		}

		// Token: 0x17002B00 RID: 11008
		// (get) Token: 0x0600A92B RID: 43307 RVA: 0x0022FF14 File Offset: 0x0022E114
		public override CacheSize CacheSize
		{
			get
			{
				return this.objectStorage.CacheSize;
			}
		}

		// Token: 0x17002B01 RID: 11009
		// (get) Token: 0x0600A92C RID: 43308 RVA: 0x0022FF21 File Offset: 0x0022E121
		public override bool? UserSpecific
		{
			get
			{
				return new bool?(this.userSpecific);
			}
		}

		// Token: 0x0600A92D RID: 43309 RVA: 0x0022FF30 File Offset: 0x0022E130
		public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			object obj;
			DateTime dateTime;
			long num;
			if (this.objectStorage.TryGetValue(key, out obj, out dateTime, out num) && dateTime >= maxStaleness && num >= LongCacheVersion.ToLong(minVersion))
			{
				this.Staleness = dateTime;
				storage = (MemoryPersistentCache.MemoryStorage)obj;
				return true;
			}
			storage = null;
			return false;
		}

		// Token: 0x0600A92E RID: 43310 RVA: 0x0022FF7A File Offset: 0x0022E17A
		public override IStorage CreateStorage()
		{
			return new MemoryPersistentCache.MemoryStorage();
		}

		// Token: 0x0600A92F RID: 43311 RVA: 0x0022FF84 File Offset: 0x0022E184
		public override void CommitStorage(string key, CacheVersion maxVersion, IStorage storage)
		{
			if (maxVersion == null)
			{
				throw new NotSupportedException();
			}
			long num = LongCacheVersion.ToLong(maxVersion);
			MemoryPersistentCache.MemoryStorage memoryStorage = (MemoryPersistentCache.MemoryStorage)storage;
			object obj;
			DateTime dateTime;
			long num2;
			if (memoryStorage.Size <= this.maxEntryLength && (!this.objectStorage.TryGetValue(key, out obj, out dateTime, out num2) || num2 <= num))
			{
				if (memoryStorage.Size + this.objectStorage.CacheSize.TotalSize > this.maxCacheSize)
				{
					this.objectStorage.Purge(this.trimCacheSize - memoryStorage.Size);
				}
				this.objectStorage.CommitValue(key, memoryStorage, memoryStorage.Size, DateTime.UtcNow, num);
			}
		}

		// Token: 0x0600A930 RID: 43312 RVA: 0x00230023 File Offset: 0x0022E223
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			return this.overflowCache.OpenStorage(key, maxStaleness, minVersion, pageSize, maxPageCount);
		}

		// Token: 0x0600A931 RID: 43313 RVA: 0x00230037 File Offset: 0x0022E237
		public override void Purge()
		{
			this.objectStorage.Purge(this.trimCacheSize);
		}

		// Token: 0x0600A932 RID: 43314 RVA: 0x0023004A File Offset: 0x0022E24A
		public override void Dispose()
		{
			this.overflowCache.Dispose();
		}

		// Token: 0x17002B02 RID: 11010
		// (get) Token: 0x0600A933 RID: 43315 RVA: 0x00230057 File Offset: 0x0022E257
		public override CacheVersion Current
		{
			get
			{
				return LongCacheVersion.New(this.objectStorage.CurrentVersion);
			}
		}

		// Token: 0x0600A934 RID: 43316 RVA: 0x00230069 File Offset: 0x0022E269
		public override CacheVersion Increment()
		{
			return LongCacheVersion.New(this.objectStorage.IncrementVersion());
		}

		// Token: 0x04005803 RID: 22531
		private readonly object syncRoot;

		// Token: 0x04005804 RID: 22532
		private readonly ObjectStorage objectStorage;

		// Token: 0x04005805 RID: 22533
		private readonly IPersistentCache overflowCache;

		// Token: 0x04005806 RID: 22534
		private readonly long maxCacheSize;

		// Token: 0x04005807 RID: 22535
		private readonly long trimCacheSize;

		// Token: 0x04005808 RID: 22536
		private readonly long maxEntryLength;

		// Token: 0x04005809 RID: 22537
		private readonly bool userSpecific;

		// Token: 0x0400580A RID: 22538
		private DateTime staleness;

		// Token: 0x02001A1E RID: 6686
		private class MemoryStorage : IStorage, IDisposable
		{
			// Token: 0x0600A935 RID: 43317 RVA: 0x0023007B File Offset: 0x0022E27B
			public MemoryStorage()
			{
				this.entries = new Dictionary<int, MemoryPersistentCache.MemoryStorage.LengthAndPages>();
			}

			// Token: 0x17002B03 RID: 11011
			// (get) Token: 0x0600A936 RID: 43318 RVA: 0x00230090 File Offset: 0x0022E290
			public long Size
			{
				get
				{
					Dictionary<int, MemoryPersistentCache.MemoryStorage.LengthAndPages> dictionary = this.entries;
					long num;
					lock (dictionary)
					{
						num = this.size;
					}
					return num;
				}
			}

			// Token: 0x17002B04 RID: 11012
			// (get) Token: 0x0600A937 RID: 43319 RVA: 0x002300D4 File Offset: 0x0022E2D4
			public IEnumerable<int> StreamIds
			{
				get
				{
					Dictionary<int, MemoryPersistentCache.MemoryStorage.LengthAndPages> dictionary = this.entries;
					IEnumerable<int> enumerable;
					lock (dictionary)
					{
						enumerable = this.entries.Keys.ToArray<int>();
					}
					return enumerable;
				}
			}

			// Token: 0x0600A938 RID: 43320 RVA: 0x00230120 File Offset: 0x0022E320
			public Stream OpenStream(int id)
			{
				Dictionary<int, MemoryPersistentCache.MemoryStorage.LengthAndPages> dictionary = this.entries;
				Stream stream;
				lock (dictionary)
				{
					MemoryPersistentCache.MemoryStorage.LengthAndPages lengthAndPages = this.entries[id];
					stream = new MemoryPagesStream(lengthAndPages.length, lengthAndPages.pages);
				}
				return stream;
			}

			// Token: 0x0600A939 RID: 43321 RVA: 0x0023017C File Offset: 0x0022E37C
			public Stream CreateStream()
			{
				return new MemoryPagesStream();
			}

			// Token: 0x0600A93A RID: 43322 RVA: 0x00230184 File Offset: 0x0022E384
			public Stream CommitStream(int id, Stream stream)
			{
				Dictionary<int, MemoryPersistentCache.MemoryStorage.LengthAndPages> dictionary = this.entries;
				lock (dictionary)
				{
					MemoryPersistentCache.MemoryStorage.LengthAndPages lengthAndPages = new MemoryPersistentCache.MemoryStorage.LengthAndPages
					{
						length = stream.Length,
						pages = ((MemoryPagesStream)stream).GetPages()
					};
					this.size += lengthAndPages.length;
					this.entries.Add(id, lengthAndPages);
				}
				stream.Position = 0L;
				return stream;
			}

			// Token: 0x0600A93B RID: 43323 RVA: 0x0000336E File Offset: 0x0000156E
			public void Close()
			{
			}

			// Token: 0x0600A93C RID: 43324 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x0400580B RID: 22539
			private readonly Dictionary<int, MemoryPersistentCache.MemoryStorage.LengthAndPages> entries;

			// Token: 0x0400580C RID: 22540
			private long size;

			// Token: 0x02001A1F RID: 6687
			private struct LengthAndPages
			{
				// Token: 0x0400580D RID: 22541
				public long length;

				// Token: 0x0400580E RID: 22542
				public List<byte[]> pages;
			}
		}

		// Token: 0x02001A20 RID: 6688
		private class Entry
		{
			// Token: 0x0600A93D RID: 43325 RVA: 0x00230210 File Offset: 0x0022E410
			public Entry(string key, MemoryPersistentCache.MemoryStorage storage)
			{
				this.key = key;
				this.storage = storage;
			}

			// Token: 0x17002B05 RID: 11013
			// (get) Token: 0x0600A93E RID: 43326 RVA: 0x00230226 File Offset: 0x0022E426
			public string Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x17002B06 RID: 11014
			// (get) Token: 0x0600A93F RID: 43327 RVA: 0x0023022E File Offset: 0x0022E42E
			public MemoryPersistentCache.MemoryStorage Storage
			{
				get
				{
					return this.storage;
				}
			}

			// Token: 0x17002B07 RID: 11015
			// (get) Token: 0x0600A940 RID: 43328 RVA: 0x00230236 File Offset: 0x0022E436
			// (set) Token: 0x0600A941 RID: 43329 RVA: 0x0023023E File Offset: 0x0022E43E
			public DateTime CreateTime { get; set; }

			// Token: 0x17002B08 RID: 11016
			// (get) Token: 0x0600A942 RID: 43330 RVA: 0x00230247 File Offset: 0x0022E447
			// (set) Token: 0x0600A943 RID: 43331 RVA: 0x0023024F File Offset: 0x0022E44F
			public DateTime LastAccessTime { get; set; }

			// Token: 0x0400580F RID: 22543
			private readonly string key;

			// Token: 0x04005810 RID: 22544
			private readonly MemoryPersistentCache.MemoryStorage storage;
		}

		// Token: 0x02001A21 RID: 6689
		private class EntryAccessTimeComparer : IComparer<MemoryPersistentCache.Entry>
		{
			// Token: 0x0600A944 RID: 43332 RVA: 0x00230258 File Offset: 0x0022E458
			public int Compare(MemoryPersistentCache.Entry x, MemoryPersistentCache.Entry y)
			{
				return x.LastAccessTime.CompareTo(y.LastAccessTime);
			}

			// Token: 0x04005813 RID: 22547
			public static readonly IComparer<MemoryPersistentCache.Entry> Instance = new MemoryPersistentCache.EntryAccessTimeComparer();
		}
	}
}
