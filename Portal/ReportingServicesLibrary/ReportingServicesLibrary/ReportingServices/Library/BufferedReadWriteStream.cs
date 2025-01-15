using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200029C RID: 668
	internal class BufferedReadWriteStream : Stream, IHasMimeType, ReportProcessing.IErasable, IUpdateSnapshot, ServerSnapshot.IHasPerformanceData
	{
		// Token: 0x06001852 RID: 6226 RVA: 0x000629F4 File Offset: 0x00060BF4
		internal static BufferedReadWriteStream GetReadWriteStream(Stream store, int bufferSize)
		{
			return new BufferedReadWriteStream(store, bufferSize);
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x000629FD File Offset: 0x00060BFD
		internal static BufferedReadStream GetReadOnlyStream(Stream store, int bufferSize)
		{
			return new BufferedReadStream(store, bufferSize);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00062A06 File Offset: 0x00060C06
		internal static BufferedWriteStream GetWriteOnlyStream(Stream store, int bufferSize)
		{
			return new BufferedWriteStream(store, bufferSize);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00062A10 File Offset: 0x00060C10
		protected BufferedReadWriteStream(Stream store, int bufferSize)
		{
			RSTrace.CatalogTrace.Assert(store != null, "store");
			this.m_store = store;
			bufferSize = Math.Max(bufferSize, 256);
			this.m_buffer = new byte[bufferSize];
			this.SetFlags(BufferedReadWriteStream.StreamFlags.IsOpen | BufferedReadWriteStream.StreamFlags.FirstWrite);
			if (this.m_store.Length == 0L)
			{
				this.SetFlags(BufferedReadWriteStream.StreamFlags.IsEmpty);
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001856 RID: 6230 RVA: 0x00062A71 File Offset: 0x00060C71
		public override bool CanRead
		{
			get
			{
				return this.m_store.CanRead;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x00062A7E File Offset: 0x00060C7E
		public override bool CanSeek
		{
			get
			{
				return this.m_store.CanSeek;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x00062A8B File Offset: 0x00060C8B
		public override bool CanWrite
		{
			get
			{
				return this.m_store.CanWrite;
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public override void Flush()
		{
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x00062A98 File Offset: 0x00060C98
		public override long Length
		{
			get
			{
				if (this.IsFlagsSet(BufferedReadWriteStream.StreamFlags.IsOpen))
				{
					return Math.Max(this.m_bufferStartPosition + (long)this.m_bytesInBuffer, this.m_store.Length);
				}
				return this.m_lengthAfterClose;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x00062AC8 File Offset: 0x00060CC8
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x00061185 File Offset: 0x0005F385
		public override long Position
		{
			get
			{
				return this.m_bufferStartPosition + (long)this.m_positionInBuffer;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00062AD8 File Offset: 0x00060CD8
		public override int Read(byte[] buffer, int offset, int count)
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
				if (this.Position >= this.Length)
				{
					return 0;
				}
				if (this.IsFlagsSet(BufferedReadWriteStream.StreamFlags.NeedsWrite))
				{
					BufferedReadWriteStream.WriteExternalBuffer(this.m_store, this.m_bufferStartPosition, this.m_buffer, 0, this.m_bytesInBuffer);
					this.UnsetFlags(BufferedReadWriteStream.StreamFlags.NeedsWrite);
				}
				if (count >= this.m_buffer.Length)
				{
					int num2 = BufferedReadWriteStream.ReadExternalBuffer(this.m_store, this.Position, buffer, offset, count);
					this.m_bufferStartPosition = this.Position + (long)num2;
					this.m_bytesInBuffer = 0;
					this.m_positionInBuffer = 0;
					this.m_bytesReadTotal += (long)num2;
					return num2;
				}
				if (!this.FillBuffer(out num))
				{
					return 0;
				}
			}
			int num3 = Math.Min(count, this.m_bytesInBuffer - this.m_positionInBuffer);
			Array.Copy(this.m_buffer, this.m_positionInBuffer, buffer, offset, num3);
			this.m_positionInBuffer += num3;
			this.m_bytesReadTotal += (long)(num3 + num);
			this.m_bytesReadFromBuffer += (long)num3;
			return num3;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00062C18 File Offset: 0x00060E18
		public override int ReadByte()
		{
			if (this.m_positionInBuffer >= this.m_bytesInBuffer)
			{
				if (this.IsFlagsSet(BufferedReadWriteStream.StreamFlags.NeedsWrite))
				{
					BufferedReadWriteStream.WriteExternalBuffer(this.m_store, this.m_bufferStartPosition, this.m_buffer, 0, this.m_bytesInBuffer);
					this.UnsetFlags(BufferedReadWriteStream.StreamFlags.NeedsWrite);
				}
				int num;
				if (!this.FillBuffer(out num))
				{
					return -1;
				}
				this.m_bytesReadFromBuffer -= 1L;
			}
			this.m_bytesReadTotal += 1L;
			this.m_bytesReadFromBuffer += 1L;
			byte[] buffer = this.m_buffer;
			int positionInBuffer = this.m_positionInBuffer;
			this.m_positionInBuffer = positionInBuffer + 1;
			return buffer[positionInBuffer];
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00062CB4 File Offset: 0x00060EB4
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!this.CanSeek)
			{
				throw new InternalCatalogException("Stream does not support seeking");
			}
			long num = -1L;
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
			}
			if (num < 0L)
			{
				throw new InternalCatalogException("Can't seek before the beginning");
			}
			if (num > this.Length)
			{
				throw new InternalCatalogException("Can't seek beyond end of stream");
			}
			if (num >= this.m_bufferStartPosition && num - this.m_bufferStartPosition <= (long)this.m_bytesInBuffer)
			{
				this.m_positionInBuffer = (int)(num - this.m_bufferStartPosition);
			}
			else
			{
				if (this.IsFlagsSet(BufferedReadWriteStream.StreamFlags.NeedsWrite))
				{
					this.WriteBuffer();
				}
				this.m_bufferStartPosition = num;
				this.m_positionInBuffer = 0;
				this.m_bytesInBuffer = 0;
			}
			return num;
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00062D77 File Offset: 0x00060F77
		public override void SetLength(long value)
		{
			throw new InternalCatalogException("The method or operation is not implemented.");
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00062D84 File Offset: 0x00060F84
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new InternalCatalogException("Buffer can't be null on write.");
			}
			if (offset < 0)
			{
				throw new InternalCatalogException("Offset must be non-negative.");
			}
			if (count < 0)
			{
				throw new InternalCatalogException("Number of bytes to write must be non-negative (0 or more)");
			}
			if (count == 0)
			{
				return;
			}
			if (this.IsFlagsSet(BufferedReadWriteStream.StreamFlags.FirstWrite))
			{
				this.UnsetFlags(BufferedReadWriteStream.StreamFlags.FirstWrite | BufferedReadWriteStream.StreamFlags.IsEmpty);
				this.OnStreamFirstWrite();
			}
			int num = this.m_buffer.Length - this.m_positionInBuffer;
			int num2 = count - num;
			if (num2 <= 0)
			{
				Array.Copy(buffer, offset, this.m_buffer, this.m_positionInBuffer, count);
				this.m_positionInBuffer += count;
				this.m_bytesInBuffer = Math.Max(this.m_positionInBuffer, this.m_bytesInBuffer);
				this.SetFlags(BufferedReadWriteStream.StreamFlags.NeedsWrite);
				return;
			}
			if (this.m_bytesInBuffer == 0)
			{
				BufferedReadWriteStream.WriteExternalBuffer(this.m_store, this.Position, buffer, offset, count);
				this.m_positionInBuffer = 0;
				this.m_bytesInBuffer = 0;
				this.m_bufferStartPosition += (long)count;
				return;
			}
			if (num2 < this.m_buffer.Length)
			{
				Array.Copy(buffer, offset, this.m_buffer, this.m_positionInBuffer, num);
				this.m_bytesInBuffer += num;
				this.WriteBuffer();
				Array.Copy(buffer, offset + num, this.m_buffer, 0, num2);
				this.m_positionInBuffer = num2;
				this.m_bytesInBuffer = Math.Max(this.m_positionInBuffer, this.m_bytesInBuffer);
				this.SetFlags(BufferedReadWriteStream.StreamFlags.NeedsWrite);
				return;
			}
			Array.Copy(buffer, offset, this.m_buffer, this.m_positionInBuffer, num);
			this.m_bytesInBuffer = this.m_buffer.Length;
			this.WriteBuffer();
			BufferedReadWriteStream.WriteExternalBuffer(this.m_store, this.Position, buffer, offset + num, num2);
			this.m_bufferStartPosition += (long)num2;
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00062F24 File Offset: 0x00061124
		public override void WriteByte(byte value)
		{
			if (this.IsFlagsSet(BufferedReadWriteStream.StreamFlags.FirstWrite))
			{
				this.UnsetFlags(BufferedReadWriteStream.StreamFlags.FirstWrite | BufferedReadWriteStream.StreamFlags.IsEmpty);
				this.OnStreamFirstWrite();
			}
			if (this.m_positionInBuffer == this.m_buffer.Length)
			{
				this.WriteBuffer();
			}
			this.SetFlags(BufferedReadWriteStream.StreamFlags.NeedsWrite);
			byte[] buffer = this.m_buffer;
			int positionInBuffer = this.m_positionInBuffer;
			this.m_positionInBuffer = positionInBuffer + 1;
			buffer[positionInBuffer] = value;
			this.m_bytesInBuffer++;
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00062F8C File Offset: 0x0006118C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.IsFlagsSet(BufferedReadWriteStream.StreamFlags.IsOpen))
				{
					try
					{
						bool flag = false;
						if (this.IsFlagsSet(BufferedReadWriteStream.StreamFlags.IsEmpty))
						{
							((ReportProcessing.IErasable)this).Erase();
						}
						try
						{
							if (this.IsFlagsSet(BufferedReadWriteStream.StreamFlags.NeedsWrite))
							{
								this.WriteBuffer();
								flag = true;
							}
						}
						catch (Exception ex)
						{
							if (RSTrace.ChunkTracer.TraceError)
							{
								RSTrace.ChunkTracer.TraceException(TraceLevel.Error, ex.ToString());
							}
							throw;
						}
						finally
						{
							if (disposing)
							{
								this.m_lengthAfterClose = this.m_store.Length;
								ICommitOnClose commitOnClose = this.m_store as ICommitOnClose;
								if (commitOnClose != null)
								{
									commitOnClose.Close(flag);
								}
								else
								{
									this.m_store.Close();
								}
							}
						}
					}
					finally
					{
						this.m_buffer = null;
						this.UnsetFlags(BufferedReadWriteStream.StreamFlags.IsOpen | BufferedReadWriteStream.StreamFlags.NeedsWrite | BufferedReadWriteStream.StreamFlags.FirstWrite);
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x00063070 File Offset: 0x00061270
		public string MimeType
		{
			get
			{
				IHasMimeType hasMimeType = this.m_store as IHasMimeType;
				if (hasMimeType == null)
				{
					return null;
				}
				return hasMimeType.MimeType;
			}
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00063094 File Offset: 0x00061294
		bool ReportProcessing.IErasable.Erase()
		{
			ReportProcessing.IErasable erasable = this.m_store as ReportProcessing.IErasable;
			return erasable != null && erasable.Erase();
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x000630B8 File Offset: 0x000612B8
		private void AdjustForReadHint()
		{
			int num = this.m_buffer.Length;
			IBufferedStreamHintProvider bufferedStreamHintProvider = this.m_store as IBufferedStreamHintProvider;
			if (bufferedStreamHintProvider != null && bufferedStreamHintProvider.CanProvideHints)
			{
				BufferedStreamHint readHint = bufferedStreamHintProvider.GetReadHint(num, this.m_bufferStartPosition);
				if (this.CanAdjustForHint(readHint))
				{
					this.m_positionInBuffer = (int)(this.m_bufferStartPosition - readHint.SuggestedReadPosition);
					this.m_bufferStartPosition = readHint.SuggestedReadPosition;
				}
			}
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0006311C File Offset: 0x0006131C
		private bool CanAdjustForHint(BufferedStreamHint hint)
		{
			if (this.m_bufferStartPosition < hint.SuggestedReadPosition || hint.SuggestedReadPosition < 0L)
			{
				if (RSTrace.ChunkTracer.TraceWarning)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Warning, "Cannot adjust for BufferedStreamHint (m_bufferStartPosition={0}, SuggestedReadPosition={1})", new object[] { this.m_bufferStartPosition, hint.SuggestedReadPosition });
				}
				return false;
			}
			int num = this.m_buffer.Length;
			int num2 = checked((int)(this.m_bufferStartPosition - hint.SuggestedReadPosition));
			if (num2 > num)
			{
				if (RSTrace.ChunkTracer.TraceWarning)
				{
					RSTrace.ChunkTracer.Trace(TraceLevel.Warning, "Cannot adjust for BufferedStreamHint (bufferSize={0}, distance={1})", new object[] { num, num2 });
				}
				return false;
			}
			return true;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x000631D4 File Offset: 0x000613D4
		private bool FillBuffer(out int bytesRead)
		{
			long position = this.Position;
			if (position >= this.Length)
			{
				bytesRead = 0;
				return false;
			}
			this.m_bufferStartPosition = position;
			this.m_positionInBuffer = 0;
			this.AdjustForReadHint();
			bytesRead = BufferedReadWriteStream.ReadExternalBuffer(this.m_store, this.m_bufferStartPosition, this.m_buffer, 0, this.m_buffer.Length);
			this.m_bytesInBuffer = bytesRead;
			return true;
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00063235 File Offset: 0x00061435
		private static int ReadExternalBuffer(Stream store, long absolutePosition, byte[] buffer, int offset, int count)
		{
			BufferedReadWriteStream.SeekInStream(store, absolutePosition);
			return store.Read(buffer, offset, count);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x00063248 File Offset: 0x00061448
		private void WriteBuffer()
		{
			BufferedReadWriteStream.WriteExternalBuffer(this.m_store, this.m_bufferStartPosition, this.m_buffer, 0, this.m_bytesInBuffer);
			this.m_positionInBuffer = 0;
			this.m_bufferStartPosition = this.m_store.Position;
			this.m_bytesInBuffer = 0;
			this.UnsetFlags(BufferedReadWriteStream.StreamFlags.NeedsWrite);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00063299 File Offset: 0x00061499
		private static void WriteExternalBuffer(Stream store, long absolutePosition, byte[] buffer, int offset, int count)
		{
			BufferedReadWriteStream.SeekInStream(store, absolutePosition);
			store.Write(buffer, offset, count);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x000632AC File Offset: 0x000614AC
		private static void SeekInStream(Stream stream, long absolutePosition)
		{
			if (stream.CanSeek)
			{
				stream.Seek(absolutePosition, SeekOrigin.Begin);
				return;
			}
			if (stream.Position != absolutePosition)
			{
				if (RSTrace.CatalogTrace.TraceError)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, string.Format(CultureInfo.InvariantCulture, "Stream is in position '{0}', expected position '{1}'", stream.Position, absolutePosition));
				}
				throw new InternalCatalogException("Cannot seek to position in stream.");
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x00063316 File Offset: 0x00061516
		protected Stream InnerStream
		{
			get
			{
				return this.m_store;
			}
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0006331E File Offset: 0x0006151E
		protected bool IsFlagsSet(BufferedReadWriteStream.StreamFlags flags)
		{
			return (this.m_flags & flags) == flags;
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0006332B File Offset: 0x0006152B
		protected void SetFlags(BufferedReadWriteStream.StreamFlags flags)
		{
			this.m_flags |= flags;
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0006333B File Offset: 0x0006153B
		protected void UnsetFlags(BufferedReadWriteStream.StreamFlags flags)
		{
			this.m_flags &= ~flags;
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06001871 RID: 6257 RVA: 0x00063350 File Offset: 0x00061550
		// (remove) Token: 0x06001872 RID: 6258 RVA: 0x00063388 File Offset: 0x00061588
		internal event BufferedReadWriteStream.StreamFirstWriteHandler StreamFirstWrite;

		// Token: 0x06001873 RID: 6259 RVA: 0x000633C0 File Offset: 0x000615C0
		private void OnStreamFirstWrite()
		{
			BufferedReadWriteStream.StreamFirstWriteEventArgs streamFirstWriteEventArgs = new BufferedReadWriteStream.StreamFirstWriteEventArgs();
			BufferedReadWriteStream.StreamFirstWriteHandler streamFirstWrite = this.StreamFirstWrite;
			if (streamFirstWrite != null)
			{
				streamFirstWrite(this, streamFirstWriteEventArgs);
			}
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x000633E5 File Offset: 0x000615E5
		ServerSnapshot.SnapshotPerfData ServerSnapshot.IHasPerformanceData.RetrievePerfData()
		{
			return new ServerSnapshot.SnapshotPerfData(this.BytesReadTotal, this.BytesReadFromBuffer, this.TimeCompressing, this.TimeUncompressing, this.CompressedLength, this.UncompressedLength);
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x00063410 File Offset: 0x00061610
		private long TimeCompressing
		{
			get
			{
				IReadWriteStatistics readWriteStatistics = this.m_store as IReadWriteStatistics;
				if (readWriteStatistics != null)
				{
					return readWriteStatistics.TimeCompressing;
				}
				return 0L;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x00063438 File Offset: 0x00061638
		private long TimeUncompressing
		{
			get
			{
				IReadWriteStatistics readWriteStatistics = this.m_store as IReadWriteStatistics;
				if (readWriteStatistics != null)
				{
					return readWriteStatistics.TimeUncompressing;
				}
				return 0L;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x00063460 File Offset: 0x00061660
		private long CompressedLength
		{
			get
			{
				IReadWriteStatistics readWriteStatistics = this.m_store as IReadWriteStatistics;
				if (readWriteStatistics != null)
				{
					return readWriteStatistics.CompressedLength;
				}
				return 0L;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x00063488 File Offset: 0x00061688
		private long UncompressedLength
		{
			get
			{
				IReadWriteStatistics readWriteStatistics = this.m_store as IReadWriteStatistics;
				if (readWriteStatistics != null)
				{
					return readWriteStatistics.UncompressedLength;
				}
				return 0L;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x000634AD File Offset: 0x000616AD
		private long BytesReadTotal
		{
			get
			{
				return this.m_bytesReadTotal;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x000634B5 File Offset: 0x000616B5
		private long BytesReadFromBuffer
		{
			get
			{
				return this.m_bytesReadFromBuffer;
			}
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x000634C0 File Offset: 0x000616C0
		public void UpdateSnapshot(Guid newSnapshotId)
		{
			IUpdateSnapshot updateSnapshot = this.m_store as IUpdateSnapshot;
			if (updateSnapshot != null)
			{
				updateSnapshot.UpdateSnapshot(newSnapshotId);
			}
		}

		// Token: 0x040008C8 RID: 2248
		private byte[] m_buffer;

		// Token: 0x040008C9 RID: 2249
		private int m_positionInBuffer;

		// Token: 0x040008CA RID: 2250
		private int m_bytesInBuffer;

		// Token: 0x040008CB RID: 2251
		private long m_bufferStartPosition;

		// Token: 0x040008CC RID: 2252
		private long m_lengthAfterClose;

		// Token: 0x040008CD RID: 2253
		private Stream m_store;

		// Token: 0x040008CE RID: 2254
		private BufferedReadWriteStream.StreamFlags m_flags;

		// Token: 0x040008D0 RID: 2256
		private long m_bytesReadFromBuffer;

		// Token: 0x040008D1 RID: 2257
		private long m_bytesReadTotal;

		// Token: 0x020004D2 RID: 1234
		[Flags]
		protected enum StreamFlags : byte
		{
			// Token: 0x0400111D RID: 4381
			IsOpen = 1,
			// Token: 0x0400111E RID: 4382
			NeedsWrite = 2,
			// Token: 0x0400111F RID: 4383
			FirstWrite = 4,
			// Token: 0x04001120 RID: 4384
			IsEmpty = 8
		}

		// Token: 0x020004D3 RID: 1235
		internal class StreamFirstWriteEventArgs : EventArgs
		{
		}

		// Token: 0x020004D4 RID: 1236
		// (Invoke) Token: 0x0600246B RID: 9323
		internal delegate void StreamFirstWriteHandler(object sender, BufferedReadWriteStream.StreamFirstWriteEventArgs e);
	}
}
