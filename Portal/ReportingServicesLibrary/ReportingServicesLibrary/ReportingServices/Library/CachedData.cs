using System;
using System.IO;
using System.Text;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200026A RID: 618
	internal sealed class CachedData : IDisposable
	{
		// Token: 0x06001643 RID: 5699 RVA: 0x000588F8 File Offset: 0x00056AF8
		public CachedData(RSStream stream)
		{
			RSTrace.CatalogTrace.Assert(!stream.IsClosed);
			MemoryThenFileStream memoryThenFileStream = stream.InnerStream as MemoryThenFileStream;
			if (memoryThenFileStream != null && memoryThenFileStream.FileName != null)
			{
				RSTrace.CatalogTrace.Assert(memoryThenFileStream.OwnsFile, "memFile.OwnsFile");
				this.m_streamToCache = stream;
				memoryThenFileStream.ReopenFileReadOnly();
				this.m_fileName = memoryThenFileStream.FileName;
				this.IncrementFileRefCount();
			}
			else
			{
				RSTrace.CatalogTrace.Assert(stream.CanRead && stream.CanSeek);
				long position = stream.Position;
				stream.Seek(0L, SeekOrigin.Begin);
				this.m_cachedBytes = new byte[stream.Length];
				long num = (long)stream.Read(this.m_cachedBytes, 0, (int)stream.Length);
				stream.Seek(position, SeekOrigin.Begin);
				if (num != stream.Length)
				{
					throw new InternalCatalogException("Reading data stream to cache data failed to read all the data.");
				}
			}
			this.m_name = stream.Name;
			this.m_mimeType = stream.MimeType;
			this.m_extension = stream.Extension;
			this.m_encoding = stream.Encoding;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00058A18 File Offset: 0x00056C18
		public void MarkDataAsCached()
		{
			object cacheDataLock = this.m_cacheDataLock;
			lock (cacheDataLock)
			{
				if (this.m_streamToCache != null)
				{
					MemoryThenFileStream memoryThenFileStream = this.m_streamToCache.InnerStream as MemoryThenFileStream;
					RSTrace.CatalogTrace.Assert(memoryThenFileStream != null && memoryThenFileStream.FileName != null);
					RSTrace.CatalogTrace.Assert(memoryThenFileStream.OwnsFile);
					RSTrace.CatalogTrace.Assert(this.IsFileBackedData);
					memoryThenFileStream.ReleaseFileOwnerShip();
					this.m_streamToCache = null;
				}
			}
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00058AB4 File Offset: 0x00056CB4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00058AC0 File Offset: 0x00056CC0
		private void Dispose(bool disposing)
		{
			object cacheDataLock = this.m_cacheDataLock;
			lock (cacheDataLock)
			{
				if (!this.m_disposed)
				{
					if (disposing)
					{
						this.DecrementFileRefCount();
						this.m_cachedBytes = null;
					}
					this.m_disposed = true;
					this.m_streamToCache = null;
				}
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00058B20 File Offset: 0x00056D20
		public CachedData.CacheStream GetNewStream()
		{
			object cacheDataLock = this.m_cacheDataLock;
			CachedData.CacheStream cacheStream;
			lock (cacheDataLock)
			{
				if (this.m_fileName == null && this.m_cachedBytes == null)
				{
					cacheStream = null;
				}
				else
				{
					Stream stream;
					if (this.IsFileBackedData)
					{
						stream = MemoryThenFileStream.LoadFromPartition(this.m_fileName);
						if (stream == null)
						{
							return null;
						}
					}
					else
					{
						stream = new MemoryStream(this.m_cachedBytes);
					}
					cacheStream = this.GetCachedStream(stream);
				}
			}
			return cacheStream;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00058BA0 File Offset: 0x00056DA0
		public long GetInMemoryByteCount()
		{
			if (this.m_cachedBytes != null)
			{
				return (long)this.m_cachedBytes.Length;
			}
			RSTrace.CatalogTrace.Assert(this.IsFileBackedData, "IsFileBackedData");
			return 0L;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00058BCA File Offset: 0x00056DCA
		private CachedData.CacheStream GetCachedStream(Stream innerStream)
		{
			return new CachedData.CacheStream(innerStream, this)
			{
				Name = this.m_name,
				MimeType = this.m_mimeType,
				Extension = this.m_extension,
				Encoding = this.m_encoding
			};
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00058C04 File Offset: 0x00056E04
		private void DecrementFileRefCount()
		{
			object cacheDataLock = this.m_cacheDataLock;
			lock (cacheDataLock)
			{
				if (this.IsFileBackedData)
				{
					this.m_fileRefCount--;
					RSTrace.CacheTracer.Assert(this.m_fileRefCount >= 0, "m_fileRefCount < 0 after decrement");
					if (this.m_fileRefCount <= 0)
					{
						Global.PartitionManager.DeleteFile(this.m_fileName);
						this.m_fileName = null;
					}
				}
			}
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00058C90 File Offset: 0x00056E90
		private void IncrementFileRefCount()
		{
			object cacheDataLock = this.m_cacheDataLock;
			lock (cacheDataLock)
			{
				if (this.IsFileBackedData)
				{
					this.m_fileRefCount++;
				}
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x00058CE0 File Offset: 0x00056EE0
		private bool IsFileBackedData
		{
			get
			{
				return this.m_fileName != null;
			}
		}

		// Token: 0x04000818 RID: 2072
		private RSStream m_streamToCache;

		// Token: 0x04000819 RID: 2073
		private byte[] m_cachedBytes;

		// Token: 0x0400081A RID: 2074
		private string m_fileName;

		// Token: 0x0400081B RID: 2075
		private readonly object m_cacheDataLock = new object();

		// Token: 0x0400081C RID: 2076
		private int m_fileRefCount;

		// Token: 0x0400081D RID: 2077
		private bool m_disposed;

		// Token: 0x0400081E RID: 2078
		private string m_name;

		// Token: 0x0400081F RID: 2079
		private string m_mimeType;

		// Token: 0x04000820 RID: 2080
		private string m_extension;

		// Token: 0x04000821 RID: 2081
		private Encoding m_encoding;

		// Token: 0x020004C3 RID: 1219
		internal class CacheStream : RSStream
		{
			// Token: 0x06002437 RID: 9271 RVA: 0x00085DDC File Offset: 0x00083FDC
			public CacheStream(Stream innerStream, CachedData owningCacheObject)
				: base(innerStream, false)
			{
				this.m_owner = owningCacheObject;
				this.m_owner.IncrementFileRefCount();
			}

			// Token: 0x06002438 RID: 9272 RVA: 0x00085DF8 File Offset: 0x00083FF8
			public CacheStream(RSStream rsStream, CachedData owningCacheObject)
				: base(rsStream)
			{
				this.m_owner = owningCacheObject;
				this.m_owner.IncrementFileRefCount();
			}

			// Token: 0x06002439 RID: 9273 RVA: 0x00085E13 File Offset: 0x00084013
			protected override void Dispose(bool disposing)
			{
				if (!base.IsClosed)
				{
					base.Dispose(disposing);
					this.m_owner.DecrementFileRefCount();
				}
			}

			// Token: 0x04001103 RID: 4355
			private CachedData m_owner;
		}
	}
}
