using System;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200029E RID: 670
	internal class BufferedCompressedReadStream : Stream, IHasMimeType, ServerSnapshot.IHasPerformanceData
	{
		// Token: 0x0600187E RID: 6270 RVA: 0x000634F0 File Offset: 0x000616F0
		internal BufferedCompressedReadStream(Stream store, short version)
		{
			RSTrace.CatalogTrace.Assert(store != null, "store");
			this.m_header = new IndexedStreamHeader(store, version);
			this.m_store = store;
			this.SkipPastHeaderOffset();
			this.m_positionInBuffer = 0;
			this.m_bytesInBuffer = 0;
			if (!this.FillBuffer())
			{
				throw new InternalCatalogException("Could not initialize stream");
			}
			this.m_isOpen = true;
			this.m_compressedLength = this.m_header.LastCompressedOffset;
			this.m_uncompressedLength = this.m_header.LastUncompressedOffset;
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0006357A File Offset: 0x0006177A
		private void SkipPastHeaderOffset()
		{
			this.m_store.Seek(IndexedStreamHeader.OffsetLength, SeekOrigin.Begin);
			this.m_bufferStartPosition = IndexedStreamHeader.OffsetLength;
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x00063599 File Offset: 0x00061799
		public override bool CanRead
		{
			get
			{
				return this.m_store.CanRead;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x00005BEF File Offset: 0x00003DEF
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x000635A6 File Offset: 0x000617A6
		public override bool CanSeek
		{
			get
			{
				return this.m_store.CanSeek;
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public override void Flush()
		{
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x000635B3 File Offset: 0x000617B3
		// (set) Token: 0x06001885 RID: 6277 RVA: 0x00061185 File Offset: 0x0005F385
		public override long Position
		{
			get
			{
				return IndexedStreamHeader.OffsetToExternal(this.InternalPosition);
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x000635C0 File Offset: 0x000617C0
		private long InternalPosition
		{
			get
			{
				return this.m_bufferStartPosition + (long)this.m_positionInBuffer;
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x000635D0 File Offset: 0x000617D0
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.m_isOpen)
				{
					if (disposing)
					{
						this.m_store.Close();
					}
					this.m_buffer = null;
					this.m_isOpen = false;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x0006361C File Offset: 0x0006181C
		public override long Length
		{
			get
			{
				return IndexedStreamHeader.OffsetToExternal(this.InternalUncompressedLength);
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x00063629 File Offset: 0x00061829
		private long InternalUncompressedLength
		{
			get
			{
				return this.m_header.LastUncompressedOffset;
			}
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00063636 File Offset: 0x00061836
		ServerSnapshot.SnapshotPerfData ServerSnapshot.IHasPerformanceData.RetrievePerfData()
		{
			return new ServerSnapshot.SnapshotPerfData(this.BytesReadTotal, this.BytesReadFromBuffer, 0L, this.TimeInCompression, this.CompressedLength, this.UncompressedLength);
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0006365D File Offset: 0x0006185D
		private long CompressedLength
		{
			get
			{
				return this.m_compressedLength;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x00063665 File Offset: 0x00061865
		private long UncompressedLength
		{
			get
			{
				return this.m_uncompressedLength;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0006366D File Offset: 0x0006186D
		private long TimeInCompression
		{
			get
			{
				return this.m_timeDecompressionMs;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x00063675 File Offset: 0x00061875
		private long BytesReadTotal
		{
			get
			{
				return this.m_bytesReadTotal;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0006367D File Offset: 0x0006187D
		private long BytesReadFromBuffer
		{
			get
			{
				return this.m_bytesReadFromBuffer;
			}
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00063685 File Offset: 0x00061885
		public override int Read(byte[] buffer, int offset, int count)
		{
			return StreamSupport.ReadToCountOrEnd(buffer, offset, count, new StreamSupport.StreamRead(this.ReadFromBuffer));
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0006369C File Offset: 0x0006189C
		private int ReadFromBuffer(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new InternalCatalogException("Buffer can't be null on read");
			}
			if (offset < 0)
			{
				throw new InternalCatalogException("Offset must be non-negative");
			}
			if (count < 0)
			{
				throw new InternalCatalogException("Number of bytes to read must be a positive number");
			}
			if (count == 0)
			{
				return 0;
			}
			int num = 0;
			if (this.m_positionInBuffer >= this.m_bytesInBuffer)
			{
				num = this.m_positionInBuffer - this.m_bytesInBuffer;
				if (!this.FillBuffer())
				{
					return -1;
				}
			}
			int num2 = this.m_bytesInBuffer - this.m_positionInBuffer;
			int num3 = ((num2 < count) ? num2 : count);
			Array.Copy(this.m_buffer, this.m_positionInBuffer, buffer, offset, num3);
			this.m_positionInBuffer += num3;
			this.m_bytesReadTotal += (long)num3;
			this.m_bytesReadFromBuffer += (long)(num3 - num);
			return num3;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0006375C File Offset: 0x0006195C
		public override int ReadByte()
		{
			if (this.m_positionInBuffer >= this.m_bytesInBuffer)
			{
				if (!this.FillBuffer())
				{
					return -1;
				}
				this.m_bytesReadFromBuffer -= 1L;
			}
			this.m_bytesReadFromBuffer += 1L;
			this.m_bytesReadTotal += 1L;
			byte[] buffer = this.m_buffer;
			int positionInBuffer = this.m_positionInBuffer;
			this.m_positionInBuffer = positionInBuffer + 1;
			return buffer[positionInBuffer];
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000637C6 File Offset: 0x000619C6
		private bool FillBuffer()
		{
			if (this.InternalPosition >= this.InternalUncompressedLength)
			{
				return false;
			}
			this.ReadAndUncompressBuffer();
			return true;
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000637E0 File Offset: 0x000619E0
		private void ReadAndUncompressBuffer()
		{
			long internalPosition = this.InternalPosition;
			IndexStruct indexStruct = null;
			this.m_header.GetBufferForOffset(internalPosition, out indexStruct);
			long uncompressedBufferEnd = indexStruct.UncompressedBufferEnd;
			long num = indexStruct.CompressedBufferEnd - indexStruct.CompressedBufferStart;
			long num2 = indexStruct.UncompressedBufferEnd - indexStruct.UncompressedBufferStart;
			byte[] array = new byte[num];
			if ((long)this.RandomAccessRead(indexStruct.CompressedBufferStart, array, 0, (int)num) != num)
			{
				throw new InternalCatalogException("Expected same nr of bytes");
			}
			UncompressHelper.Uncompress(this.Version, array, (int)num2, out this.m_buffer, ref this.m_timeDecompressionMs);
			this.m_bufferStartPosition = indexStruct.UncompressedBufferStart;
			this.m_positionInBuffer = (int)this.m_header.GetRelativeUncompressedPosition(internalPosition);
			this.m_bytesInBuffer = this.m_buffer.Length;
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00063898 File Offset: 0x00061A98
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Length + offset;
				break;
			default:
				throw new InternalCatalogException("Incorrect seek origin");
			}
			if (num < 0L)
			{
				throw new InternalCatalogException("Can't seek before the beginning");
			}
			long num2 = IndexedStreamHeader.OffsetToInternal(num);
			if (num2 < this.m_bufferStartPosition || num2 > this.m_bufferStartPosition + (long)this.m_bytesInBuffer)
			{
				this.m_bytesInBuffer = 0;
				this.m_bufferStartPosition = this.m_header.GetUncompressedBufferStartPosition(num2);
				this.m_positionInBuffer = (int)this.m_header.GetRelativeUncompressedPosition(num2);
			}
			else
			{
				this.m_positionInBuffer = (int)(num2 - this.m_bufferStartPosition);
			}
			return num;
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00061828 File Offset: 0x0005FA28
		public override void SetLength(long value)
		{
			throw new InternalCatalogException("SetLength not supported");
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00061DE6 File Offset: 0x0005FFE6
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new InternalCatalogException("Write not supported");
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x00063950 File Offset: 0x00061B50
		public string MimeType
		{
			get
			{
				IHasMimeType hasMimeType = this.m_store as IHasMimeType;
				if (hasMimeType != null)
				{
					return hasMimeType.MimeType;
				}
				return null;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x00063974 File Offset: 0x00061B74
		private long Version
		{
			get
			{
				return this.m_header.Version;
			}
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00063981 File Offset: 0x00061B81
		private int RandomAccessRead(long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			this.m_store.Seek(dataIndex, SeekOrigin.Begin);
			return this.m_store.Read(buffer, bufferIndex, length);
		}

		// Token: 0x040008D2 RID: 2258
		private int m_positionInBuffer;

		// Token: 0x040008D3 RID: 2259
		private long m_bufferStartPosition;

		// Token: 0x040008D4 RID: 2260
		private int m_bytesInBuffer;

		// Token: 0x040008D5 RID: 2261
		private byte[] m_buffer;

		// Token: 0x040008D6 RID: 2262
		private bool m_isOpen;

		// Token: 0x040008D7 RID: 2263
		private IndexedStreamHeader m_header;

		// Token: 0x040008D8 RID: 2264
		private Stream m_store;

		// Token: 0x040008D9 RID: 2265
		private long m_compressedLength;

		// Token: 0x040008DA RID: 2266
		private long m_uncompressedLength;

		// Token: 0x040008DB RID: 2267
		private long m_timeDecompressionMs;

		// Token: 0x040008DC RID: 2268
		private long m_bytesReadFromBuffer;

		// Token: 0x040008DD RID: 2269
		private long m_bytesReadTotal;
	}
}
