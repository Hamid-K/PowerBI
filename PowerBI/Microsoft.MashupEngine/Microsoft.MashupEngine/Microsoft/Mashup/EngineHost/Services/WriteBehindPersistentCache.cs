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
	// Token: 0x02001B51 RID: 6993
	public class WriteBehindPersistentCache : CacheDelegatingPersistentCache
	{
		// Token: 0x0600AEF1 RID: 44785 RVA: 0x0023D0C5 File Offset: 0x0023B2C5
		public WriteBehindPersistentCache(PersistentCache cache, string tempPath, bool cancelCommitsOnDispose)
			: base(cache)
		{
			this.tempPath = tempPath;
			if (cancelCommitsOnDispose)
			{
				this.waitHandle = new ManualResetEvent(true);
			}
			this.queue = new Queue<Tuple<string, WriteBehindPersistentCache.TemporaryStorage>>();
			Directory.CreateDirectory(tempPath);
		}

		// Token: 0x0600AEF2 RID: 44786 RVA: 0x0023D104 File Offset: 0x0023B304
		public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			long num = LongCacheVersion.ToLong(MultiLevelCacheVersion.GetVersion1(minVersion));
			object obj = this.syncRoot;
			lock (obj)
			{
				foreach (Tuple<string, WriteBehindPersistentCache.TemporaryStorage> tuple in this.queue)
				{
					if (tuple.Item1 == key && tuple.Item2.CreatedAt >= maxStaleness && LongCacheVersion.ToLong(MultiLevelCacheVersion.GetVersion1(tuple.Item2.Version)) >= num)
					{
						tuple.Item2.AddRef();
						storage = tuple.Item2;
						return true;
					}
				}
			}
			return base.TryGetStorage(key, maxStaleness, MultiLevelCacheVersion.GetVersion2(minVersion), out storage);
		}

		// Token: 0x0600AEF3 RID: 44787 RVA: 0x0023D1F0 File Offset: 0x0023B3F0
		public override IStorage CreateStorage()
		{
			return new WriteBehindPersistentCache.TemporaryStorage(this.tempPath, this.MaxEntryLength);
		}

		// Token: 0x0600AEF4 RID: 44788 RVA: 0x0023D204 File Offset: 0x0023B404
		public override void CommitStorage(string key, CacheVersion maxVersion, IStorage storage)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				WriteBehindPersistentCache.TemporaryStorage temporaryStorage = (WriteBehindPersistentCache.TemporaryStorage)storage;
				temporaryStorage.AddRef();
				temporaryStorage.Version = maxVersion;
				temporaryStorage.CreatedAt = DateTime.UtcNow;
				this.queue.Enqueue(new Tuple<string, WriteBehindPersistentCache.TemporaryStorage>(key, temporaryStorage));
				if (!this.running)
				{
					this.running = true;
					EvaluatorThreadPool.Start(new ParameterizedThreadStart(this.AsyncCommitStorage), key);
				}
			}
		}

		// Token: 0x0600AEF5 RID: 44789 RVA: 0x0023D290 File Offset: 0x0023B490
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			if ((long)pageSize * (long)maxPageCount > this.MaxEntryLength)
			{
				throw new PersistentCacheException(Strings.Cache_EntryTooLarge, null);
			}
			return base.OpenStorage(key, maxStaleness, MultiLevelCacheVersion.GetVersion2(minVersion), pageSize, maxPageCount);
		}

		// Token: 0x0600AEF6 RID: 44790 RVA: 0x0023D2C0 File Offset: 0x0023B4C0
		public override void Dispose()
		{
			if (this.waitHandle != null)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					this.queue.Clear();
				}
				this.waitHandle.WaitOne();
			}
		}

		// Token: 0x17002BE1 RID: 11233
		// (get) Token: 0x0600AEF7 RID: 44791 RVA: 0x0023D31C File Offset: 0x0023B51C
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

		// Token: 0x0600AEF8 RID: 44792 RVA: 0x0023D378 File Offset: 0x0023B578
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

		// Token: 0x0600AEF9 RID: 44793 RVA: 0x0023D3E0 File Offset: 0x0023B5E0
		private void AsyncCommitStorage(object obj)
		{
			for (;;)
			{
				object obj2 = this.syncRoot;
				Tuple<string, WriteBehindPersistentCache.TemporaryStorage> tuple;
				lock (obj2)
				{
					if (this.queue.Count == 0)
					{
						this.running = false;
						break;
					}
					tuple = this.queue.Dequeue();
					if (this.waitHandle != null)
					{
						this.waitHandle.Reset();
					}
				}
				string item = tuple.Item1;
				WriteBehindPersistentCache.TemporaryStorage item2 = tuple.Item2;
				this.AsyncCommitStorage(item, item2);
				item2.Release();
				if (this.waitHandle != null)
				{
					this.waitHandle.Set();
				}
			}
		}

		// Token: 0x0600AEFA RID: 44794 RVA: 0x0023D488 File Offset: 0x0023B688
		private void AsyncCommitStorage(string key, WriteBehindPersistentCache.TemporaryStorage storage)
		{
			byte[] array = new byte[4096];
			using (IStorage storage2 = base.CreateStorage())
			{
				foreach (int num in storage.StreamIds)
				{
					Stream stream = storage2.CreateStream();
					using (Stream stream2 = storage.OpenStream(num))
					{
						for (;;)
						{
							int num2 = stream2.Read(array, 0, array.Length);
							if (num2 == 0)
							{
								break;
							}
							stream.Write(array, 0, num2);
						}
					}
					storage2.CommitStream(num, stream).Close();
				}
				base.CommitStorage(key, MultiLevelCacheVersion.GetVersion2(storage.Version), storage2);
			}
		}

		// Token: 0x04005A34 RID: 23092
		private readonly object syncRoot = new object();

		// Token: 0x04005A35 RID: 23093
		private readonly string tempPath;

		// Token: 0x04005A36 RID: 23094
		private readonly Queue<Tuple<string, WriteBehindPersistentCache.TemporaryStorage>> queue;

		// Token: 0x04005A37 RID: 23095
		private long queueVersion;

		// Token: 0x04005A38 RID: 23096
		private bool running;

		// Token: 0x04005A39 RID: 23097
		private ManualResetEvent waitHandle;

		// Token: 0x02001B52 RID: 6994
		private class TemporaryStorage : IStorage, IDisposable
		{
			// Token: 0x0600AEFB RID: 44795 RVA: 0x0023D564 File Offset: 0x0023B764
			public TemporaryStorage(string tempPath, long maxEntryLength)
			{
				this.tempPath = tempPath;
				this.maxEntryLength = maxEntryLength;
				this.dictionary = new Dictionary<int, StreamSharer>();
				this.refCount = 1L;
			}

			// Token: 0x17002BE2 RID: 11234
			// (get) Token: 0x0600AEFC RID: 44796 RVA: 0x0023D598 File Offset: 0x0023B798
			// (set) Token: 0x0600AEFD RID: 44797 RVA: 0x0023D5A0 File Offset: 0x0023B7A0
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

			// Token: 0x17002BE3 RID: 11235
			// (get) Token: 0x0600AEFE RID: 44798 RVA: 0x0023D5A9 File Offset: 0x0023B7A9
			// (set) Token: 0x0600AEFF RID: 44799 RVA: 0x0023D5B1 File Offset: 0x0023B7B1
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

			// Token: 0x17002BE4 RID: 11236
			// (get) Token: 0x0600AF00 RID: 44800 RVA: 0x0023D5BC File Offset: 0x0023B7BC
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

			// Token: 0x0600AF01 RID: 44801 RVA: 0x0023D608 File Offset: 0x0023B808
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

			// Token: 0x0600AF02 RID: 44802 RVA: 0x0023D658 File Offset: 0x0023B858
			public Stream CreateStream()
			{
				return new WriteBehindPersistentCache.TemporaryStream(this.tempPath, 1048576, this.maxEntryLength);
			}

			// Token: 0x0600AF03 RID: 44803 RVA: 0x0023D670 File Offset: 0x0023B870
			public Stream CommitStream(int id, Stream stream)
			{
				stream.Position = 0L;
				StreamSharer streamSharer = new StreamSharer(stream);
				object obj = this.syncRoot;
				lock (obj)
				{
					this.dictionary.Add(id, streamSharer);
				}
				return streamSharer.Open();
			}

			// Token: 0x0600AF04 RID: 44804 RVA: 0x0023D6CC File Offset: 0x0023B8CC
			public void Close()
			{
				this.Dispose();
			}

			// Token: 0x0600AF05 RID: 44805 RVA: 0x0023D6D4 File Offset: 0x0023B8D4
			public void Dispose()
			{
				if (!this.disposed)
				{
					this.Release();
					this.disposed = true;
				}
			}

			// Token: 0x0600AF06 RID: 44806 RVA: 0x0023D6EC File Offset: 0x0023B8EC
			public void AddRef()
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					this.refCount += 1L;
				}
			}

			// Token: 0x0600AF07 RID: 44807 RVA: 0x0023D738 File Offset: 0x0023B938
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
						this.dictionary = null;
						this.tempPath = null;
					}
				}
			}

			// Token: 0x04005A3A RID: 23098
			private const int maxMemoryLength = 1048576;

			// Token: 0x04005A3B RID: 23099
			private readonly long maxEntryLength;

			// Token: 0x04005A3C RID: 23100
			private readonly object syncRoot = new object();

			// Token: 0x04005A3D RID: 23101
			private Dictionary<int, StreamSharer> dictionary;

			// Token: 0x04005A3E RID: 23102
			private string tempPath;

			// Token: 0x04005A3F RID: 23103
			private long refCount;

			// Token: 0x04005A40 RID: 23104
			private DateTime createdAt;

			// Token: 0x04005A41 RID: 23105
			private CacheVersion version;

			// Token: 0x04005A42 RID: 23106
			private bool disposed;
		}

		// Token: 0x02001B53 RID: 6995
		private class TemporaryStream : Stream
		{
			// Token: 0x0600AF08 RID: 44808 RVA: 0x0023D7DC File Offset: 0x0023B9DC
			public TemporaryStream(string tempDirectoryPath, int maxMemoryLength, long maxEntryLength)
			{
				this.tempDirectoryPath = tempDirectoryPath;
				this.maxMemoryLength = maxMemoryLength;
				this.maxEntryLength = maxEntryLength;
				this.stream = new MemoryPagesStream();
				this.position = 0L;
			}

			// Token: 0x17002BE5 RID: 11237
			// (get) Token: 0x0600AF09 RID: 44809 RVA: 0x0023D80C File Offset: 0x0023BA0C
			public override bool CanRead
			{
				get
				{
					return this.stream.CanRead;
				}
			}

			// Token: 0x17002BE6 RID: 11238
			// (get) Token: 0x0600AF0A RID: 44810 RVA: 0x0023D819 File Offset: 0x0023BA19
			public override bool CanSeek
			{
				get
				{
					return this.stream.CanSeek;
				}
			}

			// Token: 0x17002BE7 RID: 11239
			// (get) Token: 0x0600AF0B RID: 44811 RVA: 0x0023D826 File Offset: 0x0023BA26
			public override bool CanWrite
			{
				get
				{
					return this.stream.CanWrite;
				}
			}

			// Token: 0x0600AF0C RID: 44812 RVA: 0x0023D833 File Offset: 0x0023BA33
			public override void Flush()
			{
				this.stream.Flush();
			}

			// Token: 0x17002BE8 RID: 11240
			// (get) Token: 0x0600AF0D RID: 44813 RVA: 0x0023D840 File Offset: 0x0023BA40
			public override long Length
			{
				get
				{
					return this.stream.Length;
				}
			}

			// Token: 0x0600AF0E RID: 44814 RVA: 0x0023D84D File Offset: 0x0023BA4D
			public override void Close()
			{
				this.stream.Close();
			}

			// Token: 0x17002BE9 RID: 11241
			// (get) Token: 0x0600AF0F RID: 44815 RVA: 0x0023D85A File Offset: 0x0023BA5A
			// (set) Token: 0x0600AF10 RID: 44816 RVA: 0x0023D862 File Offset: 0x0023BA62
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

			// Token: 0x0600AF11 RID: 44817 RVA: 0x0023D877 File Offset: 0x0023BA77
			public override int ReadByte()
			{
				int num = this.stream.ReadByte();
				if (num != -1)
				{
					this.position += 1L;
				}
				return num;
			}

			// Token: 0x0600AF12 RID: 44818 RVA: 0x0023D898 File Offset: 0x0023BA98
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num = this.stream.Read(buffer, offset, count);
				this.position += (long)num;
				return num;
			}

			// Token: 0x0600AF13 RID: 44819 RVA: 0x0023D8C4 File Offset: 0x0023BAC4
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

			// Token: 0x0600AF14 RID: 44820 RVA: 0x0023D91A File Offset: 0x0023BB1A
			public override void SetLength(long value)
			{
				this.stream.SetLength(value);
			}

			// Token: 0x0600AF15 RID: 44821 RVA: 0x0023D928 File Offset: 0x0023BB28
			public override void WriteByte(byte value)
			{
				this.CheckLength(1);
				this.stream.WriteByte(value);
				this.position += 1L;
			}

			// Token: 0x0600AF16 RID: 44822 RVA: 0x0023D94C File Offset: 0x0023BB4C
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.CheckLength(count);
				this.stream.Write(buffer, offset, count);
				this.position += (long)count;
			}

			// Token: 0x0600AF17 RID: 44823 RVA: 0x0023D974 File Offset: 0x0023BB74
			private void CheckLength(int count)
			{
				if (this.position + (long)count > this.maxEntryLength)
				{
					throw new PersistentCacheException(Strings.Cache_EntryTooLarge, null);
				}
				if (this.position < (long)this.maxMemoryLength && this.position >= (long)(this.maxMemoryLength - count))
				{
					Stream stream = this.CreateStream();
					this.stream.Position = 0L;
					WriteBehindPersistentCache.TemporaryStream.Copy(this.stream, stream, 4096);
					stream.Position = this.position;
					this.stream = stream;
				}
			}

			// Token: 0x0600AF18 RID: 44824 RVA: 0x0023D9F8 File Offset: 0x0023BBF8
			private Stream CreateStream()
			{
				string text = Path.Combine(this.tempDirectoryPath, Guid.NewGuid().ToString());
				Stream stream;
				try
				{
					stream = File.Create(text, 4096, FileOptions.DeleteOnClose);
				}
				catch (DirectoryNotFoundException)
				{
					Directory.CreateDirectory(Path.GetDirectoryName(text));
					stream = File.Create(text, 4096, FileOptions.DeleteOnClose);
				}
				return stream;
			}

			// Token: 0x0600AF19 RID: 44825 RVA: 0x0023DA68 File Offset: 0x0023BC68
			private static void Copy(Stream source, Stream destination, int bufferSize)
			{
				byte[] array = new byte[bufferSize];
				for (;;)
				{
					int num = source.Read(array, 0, array.Length);
					if (num == 0)
					{
						break;
					}
					destination.Write(array, 0, num);
				}
			}

			// Token: 0x04005A43 RID: 23107
			private string tempDirectoryPath;

			// Token: 0x04005A44 RID: 23108
			private int maxMemoryLength;

			// Token: 0x04005A45 RID: 23109
			private long maxEntryLength;

			// Token: 0x04005A46 RID: 23110
			private Stream stream;

			// Token: 0x04005A47 RID: 23111
			private long position;
		}
	}
}
