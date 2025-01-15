using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C12 RID: 7186
	public static class PersistentCacheExtensions
	{
		// Token: 0x0600B349 RID: 45897 RVA: 0x00247788 File Offset: 0x00245988
		public static bool TryGetValue(this IPersistentCache cache, OneOf<string, StructuredCacheKey> key, out Stream stream)
		{
			IStorage storage;
			if (cache.TryGetStorage(key, out storage))
			{
				stream = new PersistentCacheExtensions.StorageStream(storage, storage.OpenStream(0));
				return true;
			}
			stream = null;
			return false;
		}

		// Token: 0x0600B34A RID: 45898 RVA: 0x002477B8 File Offset: 0x002459B8
		public static bool TryGetValue(this IPersistentCache cache, OneOf<string, StructuredCacheKey> key, DateTime maxStaleness, CacheVersion minVersion, out Stream stream)
		{
			IStorage storage;
			if ((key.Is<string>() && cache.TryGetStorage(key.As<string>(), maxStaleness, minVersion, out storage)) || (key.Is<StructuredCacheKey>() && cache.TryGetStorage(key.As<StructuredCacheKey>(), maxStaleness, minVersion, out storage)))
			{
				stream = new PersistentCacheExtensions.StorageStream(storage, storage.OpenStream(0));
				return true;
			}
			stream = null;
			return false;
		}

		// Token: 0x0600B34B RID: 45899 RVA: 0x00247814 File Offset: 0x00245A14
		public static Stream BeginAdd(this IPersistentCache cache)
		{
			IStorage storage = cache.CreateStorage();
			Stream stream = storage.CreateStream();
			return new PersistentCacheExtensions.StorageStream(storage, stream);
		}

		// Token: 0x0600B34C RID: 45900 RVA: 0x00247834 File Offset: 0x00245A34
		public static Stream EndAdd(this IPersistentCache cache, OneOf<string, StructuredCacheKey> key, Stream stream)
		{
			PersistentCacheExtensions.StorageStream storageStream = (PersistentCacheExtensions.StorageStream)stream;
			Stream stream2 = storageStream.Storage.CommitStream(0, storageStream.Stream);
			if (key.Is<string>())
			{
				cache.CommitStorage(key.As<string>(), storageStream.Storage);
			}
			else
			{
				cache.CommitStorage(key.As<StructuredCacheKey>(), storageStream.Storage);
			}
			storageStream.Storage.Close();
			return stream2;
		}

		// Token: 0x0600B34D RID: 45901 RVA: 0x002478A0 File Offset: 0x00245AA0
		public static bool TryGetStorage(this IPersistentCache cache, OneOf<string, StructuredCacheKey> key, out IStorage storage)
		{
			if (key.Is<string>())
			{
				return cache.TryGetStorage(key.As<string>(), DateTime.MinValue, null, out storage);
			}
			return cache.TryGetStorage(key.As<StructuredCacheKey>(), DateTime.MinValue, null, out storage);
		}

		// Token: 0x0600B34E RID: 45902 RVA: 0x002478D4 File Offset: 0x00245AD4
		public static void CommitStorage(this IPersistentCache cache, OneOf<string, StructuredCacheKey> key, IStorage storage)
		{
			if (key.Is<string>())
			{
				cache.CommitStorage(key.As<string>(), null, storage);
				return;
			}
			cache.CommitStorage(key.As<StructuredCacheKey>(), null, storage);
		}

		// Token: 0x0600B34F RID: 45903 RVA: 0x00247900 File Offset: 0x00245B00
		public static OneOf<string, StructuredCacheKey> AppendPart(this OneOf<string, StructuredCacheKey> cacheKey, string part)
		{
			if (cacheKey.Is<string>())
			{
				return cacheKey.As<string>().Replace("/", "//") + "/" + part;
			}
			StructuredCacheKey structuredCacheKey = cacheKey.As<StructuredCacheKey>();
			return new StructuredCacheKey(structuredCacheKey.Credentials, structuredCacheKey.Parts.Add(part));
		}

		// Token: 0x02001C13 RID: 7187
		private class StorageStream : Stream
		{
			// Token: 0x0600B350 RID: 45904 RVA: 0x00247961 File Offset: 0x00245B61
			public StorageStream(IStorage storage, Stream stream)
			{
				this.storage = storage;
				this.stream = stream;
			}

			// Token: 0x17002CE9 RID: 11497
			// (get) Token: 0x0600B351 RID: 45905 RVA: 0x00247977 File Offset: 0x00245B77
			public IStorage Storage
			{
				get
				{
					return this.storage;
				}
			}

			// Token: 0x17002CEA RID: 11498
			// (get) Token: 0x0600B352 RID: 45906 RVA: 0x0024797F File Offset: 0x00245B7F
			public Stream Stream
			{
				get
				{
					return this.stream;
				}
			}

			// Token: 0x17002CEB RID: 11499
			// (get) Token: 0x0600B353 RID: 45907 RVA: 0x00247987 File Offset: 0x00245B87
			public override bool CanRead
			{
				get
				{
					return this.stream.CanRead;
				}
			}

			// Token: 0x17002CEC RID: 11500
			// (get) Token: 0x0600B354 RID: 45908 RVA: 0x00247994 File Offset: 0x00245B94
			public override bool CanSeek
			{
				get
				{
					return this.stream.CanSeek;
				}
			}

			// Token: 0x17002CED RID: 11501
			// (get) Token: 0x0600B355 RID: 45909 RVA: 0x002479A1 File Offset: 0x00245BA1
			public override bool CanWrite
			{
				get
				{
					return this.stream.CanWrite;
				}
			}

			// Token: 0x0600B356 RID: 45910 RVA: 0x002479AE File Offset: 0x00245BAE
			public override void Flush()
			{
				this.stream.Flush();
			}

			// Token: 0x17002CEE RID: 11502
			// (get) Token: 0x0600B357 RID: 45911 RVA: 0x002479BB File Offset: 0x00245BBB
			public override long Length
			{
				get
				{
					return this.stream.Length;
				}
			}

			// Token: 0x17002CEF RID: 11503
			// (get) Token: 0x0600B358 RID: 45912 RVA: 0x002479C8 File Offset: 0x00245BC8
			// (set) Token: 0x0600B359 RID: 45913 RVA: 0x002479D5 File Offset: 0x00245BD5
			public override long Position
			{
				get
				{
					return this.stream.Position;
				}
				set
				{
					this.stream.Position = value;
				}
			}

			// Token: 0x0600B35A RID: 45914 RVA: 0x002479E3 File Offset: 0x00245BE3
			public override void Close()
			{
				if (this.stream != null)
				{
					this.stream.Close();
					this.stream = null;
				}
				if (this.storage != null)
				{
					this.storage.Close();
					this.storage = null;
				}
			}

			// Token: 0x0600B35B RID: 45915 RVA: 0x00247A19 File Offset: 0x00245C19
			public override int ReadByte()
			{
				return this.stream.ReadByte();
			}

			// Token: 0x0600B35C RID: 45916 RVA: 0x00247A26 File Offset: 0x00245C26
			public override int Read(byte[] buffer, int offset, int count)
			{
				return this.stream.Read(buffer, offset, count);
			}

			// Token: 0x0600B35D RID: 45917 RVA: 0x00247A36 File Offset: 0x00245C36
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this.stream.Seek(offset, origin);
			}

			// Token: 0x0600B35E RID: 45918 RVA: 0x00247A45 File Offset: 0x00245C45
			public override void SetLength(long value)
			{
				this.stream.SetLength(value);
			}

			// Token: 0x0600B35F RID: 45919 RVA: 0x00247A53 File Offset: 0x00245C53
			public override void WriteByte(byte value)
			{
				this.stream.WriteByte(value);
			}

			// Token: 0x0600B360 RID: 45920 RVA: 0x00247A61 File Offset: 0x00245C61
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.stream.Write(buffer, offset, count);
			}

			// Token: 0x04005B78 RID: 23416
			private IStorage storage;

			// Token: 0x04005B79 RID: 23417
			private Stream stream;
		}
	}
}
