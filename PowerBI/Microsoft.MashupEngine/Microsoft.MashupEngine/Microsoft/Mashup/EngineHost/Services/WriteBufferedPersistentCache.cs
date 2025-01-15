using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B54 RID: 6996
	public class WriteBufferedPersistentCache : CacheDelegatingPersistentCache
	{
		// Token: 0x0600AF1A RID: 44826 RVA: 0x0023DA96 File Offset: 0x0023BC96
		public WriteBufferedPersistentCache(PersistentCache cache, bool cancelCommitsOnDispose, int maxMemoryLength = 1048576)
			: base(cache)
		{
			this.maxMemoryLength = maxMemoryLength;
			if (cancelCommitsOnDispose)
			{
				this.waitHandle = new ManualResetEvent(true);
			}
			this.queue = new Queue<Tuple<string, WriteBufferedPersistentCache.TemporaryStorage>>();
		}

		// Token: 0x0600AF1B RID: 44827 RVA: 0x0023DACC File Offset: 0x0023BCCC
		public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			long num = LongCacheVersion.ToLong(MultiLevelCacheVersion.GetVersion1(minVersion));
			object obj = this.syncRoot;
			lock (obj)
			{
				foreach (Tuple<string, WriteBufferedPersistentCache.TemporaryStorage> tuple in this.queue)
				{
					if (tuple.Item1 == key && tuple.Item2.CreatedAt >= maxStaleness && LongCacheVersion.ToLong(MultiLevelCacheVersion.GetVersion1(tuple.Item2.Version)) >= num)
					{
						tuple.Item2.AddRef();
						storage = tuple.Item2;
						return true;
					}
				}
				if (this.commitingItem != null && this.commitingItem.Item1 == key && this.commitingItem.Item2.CreatedAt >= maxStaleness && LongCacheVersion.ToLong(MultiLevelCacheVersion.GetVersion1(this.commitingItem.Item2.Version)) >= num)
				{
					this.commitingItem.Item2.AddRef();
					storage = this.commitingItem.Item2;
					return true;
				}
			}
			return base.TryGetStorage(key, maxStaleness, MultiLevelCacheVersion.GetVersion2(minVersion), out storage);
		}

		// Token: 0x0600AF1C RID: 44828 RVA: 0x0023DC48 File Offset: 0x0023BE48
		public override IStorage CreateStorage()
		{
			return new WriteBufferedPersistentCache.TemporaryStorage(this.Cache, this.MaxEntryLength, this.maxMemoryLength);
		}

		// Token: 0x0600AF1D RID: 44829 RVA: 0x0023DC64 File Offset: 0x0023BE64
		public override void CommitStorage(string key, CacheVersion maxVersion, IStorage storage)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				WriteBufferedPersistentCache.TemporaryStorage temporaryStorage = (WriteBufferedPersistentCache.TemporaryStorage)storage;
				temporaryStorage.AddRef();
				temporaryStorage.Version = maxVersion;
				temporaryStorage.CreatedAt = DateTime.UtcNow;
				this.queue.Enqueue(new Tuple<string, WriteBufferedPersistentCache.TemporaryStorage>(key, temporaryStorage));
				if (!this.running)
				{
					this.running = true;
					EvaluatorThreadPool.Start(new ParameterizedThreadStart(this.AsyncCommitStorage), key);
				}
			}
		}

		// Token: 0x0600AF1E RID: 44830 RVA: 0x0023D290 File Offset: 0x0023B490
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			if ((long)pageSize * (long)maxPageCount > this.MaxEntryLength)
			{
				throw new PersistentCacheException(Strings.Cache_EntryTooLarge, null);
			}
			return base.OpenStorage(key, maxStaleness, MultiLevelCacheVersion.GetVersion2(minVersion), pageSize, maxPageCount);
		}

		// Token: 0x0600AF1F RID: 44831 RVA: 0x0023DCF0 File Offset: 0x0023BEF0
		public override void Dispose()
		{
			if (this.waitHandle != null)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					while (this.queue.Count > 0)
					{
						this.queue.Dequeue().Item2.Release();
					}
				}
				this.waitHandle.WaitOne();
			}
		}

		// Token: 0x17002BEA RID: 11242
		// (get) Token: 0x0600AF20 RID: 44832 RVA: 0x0023DD64 File Offset: 0x0023BF64
		public override CacheVersion Current
		{
			get
			{
				object obj = this.syncRoot;
				long num;
				lock (obj)
				{
					num = this.queueVersion;
				}
				return new MultiLevelCacheVersion(LongCacheVersion.New(num), this.Cache.CacheClock.Current);
			}
		}

		// Token: 0x0600AF21 RID: 44833 RVA: 0x0023DDC0 File Offset: 0x0023BFC0
		public override CacheVersion Increment()
		{
			object obj = this.syncRoot;
			long num2;
			lock (obj)
			{
				long num = this.queueVersion + 1L;
				this.queueVersion = num;
				num2 = num;
			}
			return new MultiLevelCacheVersion(LongCacheVersion.New(num2), this.Cache.CacheClock.Increment());
		}

		// Token: 0x0600AF22 RID: 44834 RVA: 0x0023DE28 File Offset: 0x0023C028
		private void AsyncCommitStorage(object obj)
		{
			byte[] array = new byte[4096];
			for (;;)
			{
				object obj2 = this.syncRoot;
				Tuple<string, WriteBufferedPersistentCache.TemporaryStorage> tuple;
				lock (obj2)
				{
					if (this.queue.Count == 0)
					{
						this.running = false;
						break;
					}
					tuple = this.queue.Dequeue();
					this.commitingItem = tuple;
					if (this.waitHandle != null)
					{
						this.waitHandle.Reset();
					}
				}
				string item = tuple.Item1;
				WriteBufferedPersistentCache.TemporaryStorage item2 = tuple.Item2;
				this.AsyncCommitStorage(item, item2, array);
				obj2 = this.syncRoot;
				lock (obj2)
				{
					this.commitingItem = null;
				}
				item2.Release();
				if (this.waitHandle != null)
				{
					this.waitHandle.Set();
				}
			}
		}

		// Token: 0x0600AF23 RID: 44835 RVA: 0x0023DF20 File Offset: 0x0023C120
		private void AsyncCommitStorage(string key, WriteBufferedPersistentCache.TemporaryStorage storage, byte[] buffer)
		{
			IStorage baseStorage = storage.GetBaseStorage();
			foreach (int num in storage.NonCachedStreamIds)
			{
				Stream stream = baseStorage.CreateStream();
				using (Stream stream2 = storage.OpenStream(num))
				{
					stream2.CopyTo(stream, buffer);
				}
				baseStorage.CommitStream(num, stream).Close();
			}
			base.CommitStorage(key, MultiLevelCacheVersion.GetVersion2(storage.Version), baseStorage);
		}

		// Token: 0x04005A48 RID: 23112
		private const int defaultMaxMemoryLength = 1048576;

		// Token: 0x04005A49 RID: 23113
		private readonly object syncRoot = new object();

		// Token: 0x04005A4A RID: 23114
		private readonly int maxMemoryLength;

		// Token: 0x04005A4B RID: 23115
		private readonly Queue<Tuple<string, WriteBufferedPersistentCache.TemporaryStorage>> queue;

		// Token: 0x04005A4C RID: 23116
		private long queueVersion;

		// Token: 0x04005A4D RID: 23117
		private bool running;

		// Token: 0x04005A4E RID: 23118
		private ManualResetEvent waitHandle;

		// Token: 0x04005A4F RID: 23119
		private Tuple<string, WriteBufferedPersistentCache.TemporaryStorage> commitingItem;

		// Token: 0x02001B55 RID: 6997
		private class TemporaryStorage : IStorage, IDisposable
		{
			// Token: 0x0600AF24 RID: 44836 RVA: 0x0023DFC4 File Offset: 0x0023C1C4
			public TemporaryStorage(PersistentCache baseCache, long maxEntryLength, int maxMemoryLength)
			{
				this.maxEntryLength = maxEntryLength;
				this.dictionary = new Dictionary<int, StreamSharer>();
				this.refCount = 1L;
				this.maxMemoryLength = maxMemoryLength;
				this.baseCache = baseCache;
			}

			// Token: 0x17002BEB RID: 11243
			// (get) Token: 0x0600AF25 RID: 44837 RVA: 0x0023DFFF File Offset: 0x0023C1FF
			// (set) Token: 0x0600AF26 RID: 44838 RVA: 0x0023E007 File Offset: 0x0023C207
			public DateTime CreatedAt
			{
				get
				{
					return this.createdAt;
				}
				set
				{
					this.createdAt = value;
				}
			}

			// Token: 0x17002BEC RID: 11244
			// (get) Token: 0x0600AF27 RID: 44839 RVA: 0x0023E010 File Offset: 0x0023C210
			// (set) Token: 0x0600AF28 RID: 44840 RVA: 0x0023E018 File Offset: 0x0023C218
			public CacheVersion Version
			{
				get
				{
					return this.version;
				}
				set
				{
					this.version = value;
				}
			}

			// Token: 0x17002BED RID: 11245
			// (get) Token: 0x0600AF29 RID: 44841 RVA: 0x0023E024 File Offset: 0x0023C224
			public IEnumerable<int> StreamIds
			{
				get
				{
					object obj = this.syncRoot;
					IEnumerable<int> enumerable;
					lock (obj)
					{
						enumerable = this.dictionary.Keys.ToArray<int>();
					}
					return enumerable;
				}
			}

			// Token: 0x17002BEE RID: 11246
			// (get) Token: 0x0600AF2A RID: 44842 RVA: 0x0023E070 File Offset: 0x0023C270
			public IEnumerable<int> NonCachedStreamIds
			{
				get
				{
					object obj = this.syncRoot;
					IEnumerable<int> enumerable;
					lock (obj)
					{
						enumerable = (from entry in this.dictionary
							where !((WriteBufferedPersistentCache.TemporaryStream)entry.Value.Stream).IsCached
							select entry.Key).ToArray<int>();
					}
					return enumerable;
				}
			}

			// Token: 0x0600AF2B RID: 44843 RVA: 0x0023E100 File Offset: 0x0023C300
			public IStorage GetBaseStorage()
			{
				if (this.baseStorage == null)
				{
					object obj = this.syncRoot;
					lock (obj)
					{
						if (this.baseStorage == null)
						{
							this.baseStorage = this.baseCache.CreateStorage();
						}
					}
				}
				return this.baseStorage;
			}

			// Token: 0x0600AF2C RID: 44844 RVA: 0x0023E164 File Offset: 0x0023C364
			public Stream OpenStream(int id)
			{
				object obj = this.syncRoot;
				Stream stream;
				lock (obj)
				{
					stream = this.dictionary[id].Open();
				}
				return stream;
			}

			// Token: 0x0600AF2D RID: 44845 RVA: 0x0023E1B4 File Offset: 0x0023C3B4
			public Stream CreateStream()
			{
				return new WriteBufferedPersistentCache.TemporaryStream(this, this.maxMemoryLength, this.maxEntryLength);
			}

			// Token: 0x0600AF2E RID: 44846 RVA: 0x0023E1C8 File Offset: 0x0023C3C8
			public Stream CommitStream(int id, Stream stream)
			{
				WriteBufferedPersistentCache.TemporaryStream temporaryStream = (WriteBufferedPersistentCache.TemporaryStream)stream;
				if (temporaryStream.IsCached)
				{
					temporaryStream.CommitToStorage(id);
				}
				stream.Position = 0L;
				StreamSharer streamSharer = new StreamSharer(stream);
				object obj = this.syncRoot;
				lock (obj)
				{
					this.dictionary.Add(id, streamSharer);
				}
				return streamSharer.Open();
			}

			// Token: 0x0600AF2F RID: 44847 RVA: 0x0023E23C File Offset: 0x0023C43C
			public void Close()
			{
				this.Dispose();
			}

			// Token: 0x0600AF30 RID: 44848 RVA: 0x0023E244 File Offset: 0x0023C444
			public void Dispose()
			{
				if (!this.disposed)
				{
					this.Release();
					this.disposed = true;
				}
			}

			// Token: 0x0600AF31 RID: 44849 RVA: 0x0023E25C File Offset: 0x0023C45C
			public void AddRef()
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					this.refCount += 1L;
				}
			}

			// Token: 0x0600AF32 RID: 44850 RVA: 0x0023E2A8 File Offset: 0x0023C4A8
			public void Release()
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					this.refCount -= 1L;
					if (this.refCount == 0L)
					{
						foreach (StreamSharer streamSharer in this.dictionary.Values)
						{
							streamSharer.Close();
						}
						if (this.baseStorage != null)
						{
							this.baseStorage.Close();
						}
					}
				}
			}

			// Token: 0x04005A50 RID: 23120
			private readonly int maxMemoryLength;

			// Token: 0x04005A51 RID: 23121
			private readonly long maxEntryLength;

			// Token: 0x04005A52 RID: 23122
			private readonly object syncRoot = new object();

			// Token: 0x04005A53 RID: 23123
			private readonly PersistentCache baseCache;

			// Token: 0x04005A54 RID: 23124
			private readonly Dictionary<int, StreamSharer> dictionary;

			// Token: 0x04005A55 RID: 23125
			private IStorage baseStorage;

			// Token: 0x04005A56 RID: 23126
			private long refCount;

			// Token: 0x04005A57 RID: 23127
			private DateTime createdAt;

			// Token: 0x04005A58 RID: 23128
			private CacheVersion version;

			// Token: 0x04005A59 RID: 23129
			private bool disposed;
		}

		// Token: 0x02001B57 RID: 6999
		private class TemporaryStream : Stream
		{
			// Token: 0x0600AF37 RID: 44855 RVA: 0x0023E380 File Offset: 0x0023C580
			public TemporaryStream(WriteBufferedPersistentCache.TemporaryStorage storage, int maxMemoryLength, long maxEntryLength)
			{
				this.maxMemoryLength = maxMemoryLength;
				this.maxEntryLength = maxEntryLength;
				this.stream = new MemoryPagesStream();
				this.position = 0L;
				this.storage = storage;
			}

			// Token: 0x17002BEF RID: 11247
			// (get) Token: 0x0600AF38 RID: 44856 RVA: 0x0023E3B0 File Offset: 0x0023C5B0
			public override bool CanRead
			{
				get
				{
					return this.stream.CanRead;
				}
			}

			// Token: 0x17002BF0 RID: 11248
			// (get) Token: 0x0600AF39 RID: 44857 RVA: 0x0023E3BD File Offset: 0x0023C5BD
			public override bool CanSeek
			{
				get
				{
					return this.stream.CanSeek;
				}
			}

			// Token: 0x17002BF1 RID: 11249
			// (get) Token: 0x0600AF3A RID: 44858 RVA: 0x0023E3CA File Offset: 0x0023C5CA
			public override bool CanWrite
			{
				get
				{
					return this.stream.CanWrite;
				}
			}

			// Token: 0x0600AF3B RID: 44859 RVA: 0x0023E3D7 File Offset: 0x0023C5D7
			public override void Flush()
			{
				this.stream.Flush();
			}

			// Token: 0x17002BF2 RID: 11250
			// (get) Token: 0x0600AF3C RID: 44860 RVA: 0x0023E3E4 File Offset: 0x0023C5E4
			public override long Length
			{
				get
				{
					return this.stream.Length;
				}
			}

			// Token: 0x0600AF3D RID: 44861 RVA: 0x0023E3F1 File Offset: 0x0023C5F1
			public override void Close()
			{
				this.stream.Close();
			}

			// Token: 0x17002BF3 RID: 11251
			// (get) Token: 0x0600AF3E RID: 44862 RVA: 0x0023E3FE File Offset: 0x0023C5FE
			// (set) Token: 0x0600AF3F RID: 44863 RVA: 0x0023E406 File Offset: 0x0023C606
			public override long Position
			{
				get
				{
					return this.position;
				}
				set
				{
					this.position = value;
					this.stream.Position = value;
				}
			}

			// Token: 0x17002BF4 RID: 11252
			// (get) Token: 0x0600AF40 RID: 44864 RVA: 0x0023E41B File Offset: 0x0023C61B
			public bool IsCached
			{
				get
				{
					return this.cached;
				}
			}

			// Token: 0x0600AF41 RID: 44865 RVA: 0x0023E423 File Offset: 0x0023C623
			public override int ReadByte()
			{
				int num = this.stream.ReadByte();
				if (num != -1)
				{
					this.position += 1L;
				}
				return num;
			}

			// Token: 0x0600AF42 RID: 44866 RVA: 0x0023E444 File Offset: 0x0023C644
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num = this.stream.Read(buffer, offset, count);
				this.position += (long)num;
				return num;
			}

			// Token: 0x0600AF43 RID: 44867 RVA: 0x0023E470 File Offset: 0x0023C670
			public override long Seek(long offset, SeekOrigin origin)
			{
				switch (origin)
				{
				case SeekOrigin.Begin:
					this.Position = offset;
					break;
				case SeekOrigin.Current:
					this.Position += offset;
					break;
				case SeekOrigin.End:
					this.Position = this.Length - offset;
					break;
				default:
					throw new InvalidOperationException();
				}
				return this.Position;
			}

			// Token: 0x0600AF44 RID: 44868 RVA: 0x0023E4C6 File Offset: 0x0023C6C6
			public override void SetLength(long value)
			{
				this.stream.SetLength(value);
			}

			// Token: 0x0600AF45 RID: 44869 RVA: 0x0023E4D4 File Offset: 0x0023C6D4
			public override void WriteByte(byte value)
			{
				this.CheckLength(1);
				this.stream.WriteByte(value);
				this.position += 1L;
			}

			// Token: 0x0600AF46 RID: 44870 RVA: 0x0023E4F8 File Offset: 0x0023C6F8
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.CheckLength(count);
				this.stream.Write(buffer, offset, count);
				this.position += (long)count;
			}

			// Token: 0x0600AF47 RID: 44871 RVA: 0x0023E51E File Offset: 0x0023C71E
			public void CommitToStorage(int id)
			{
				this.stream = this.storage.GetBaseStorage().CommitStream(id, this.stream);
			}

			// Token: 0x0600AF48 RID: 44872 RVA: 0x0023E540 File Offset: 0x0023C740
			private void CheckLength(int count)
			{
				if (this.position + (long)count > this.maxEntryLength)
				{
					throw new PersistentCacheException(Strings.Cache_EntryTooLarge, null);
				}
				if (!this.cached && this.position < (long)this.maxMemoryLength && this.position >= (long)(this.maxMemoryLength - count))
				{
					Stream stream = this.storage.GetBaseStorage().CreateStream();
					this.stream.Position = 0L;
					this.stream.CopyTo(stream, 4096);
					this.stream = stream;
					this.cached = true;
				}
			}

			// Token: 0x04005A5D RID: 23133
			private readonly WriteBufferedPersistentCache.TemporaryStorage storage;

			// Token: 0x04005A5E RID: 23134
			private readonly int maxMemoryLength;

			// Token: 0x04005A5F RID: 23135
			private readonly long maxEntryLength;

			// Token: 0x04005A60 RID: 23136
			private Stream stream;

			// Token: 0x04005A61 RID: 23137
			private long position;

			// Token: 0x04005A62 RID: 23138
			private bool cached;
		}
	}
}
