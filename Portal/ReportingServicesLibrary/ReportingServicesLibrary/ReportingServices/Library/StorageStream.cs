using System;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002CD RID: 717
	internal sealed class StorageStream : Stream, IHasMimeType, IReadWriteStatistics, ICommitOnClose, IBufferedStreamHintProvider, IUpdateSnapshot
	{
		// Token: 0x0600198E RID: 6542 RVA: 0x00066EAB File Offset: 0x000650AB
		public StorageStream(IStorage store)
		{
			RSTrace.CatalogTrace.Assert(store != null, "store");
			this.m_store = store;
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x00066ECD File Offset: 0x000650CD
		public override bool CanRead
		{
			get
			{
				return this.m_store.CanRead;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x00066EDA File Offset: 0x000650DA
		public override bool CanWrite
		{
			get
			{
				return this.m_store.CanWrite;
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public override void Flush()
		{
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x00066EE7 File Offset: 0x000650E7
		public override long Length
		{
			get
			{
				return this.m_store.Length;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x00066EF4 File Offset: 0x000650F4
		// (set) Token: 0x06001995 RID: 6549 RVA: 0x00061185 File Offset: 0x0005F385
		public override long Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00066EFC File Offset: 0x000650FC
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.m_store.Read(this.m_position, buffer, offset, count);
			this.m_position += (long)num;
			return num;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00066F30 File Offset: 0x00065130
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num = this.m_position;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num += offset;
				break;
			case SeekOrigin.End:
				num = this.Length + offset;
				break;
			}
			if (num > this.Length)
			{
				throw new Exception("Attempt to seek beyond end of stream");
			}
			this.m_position = num;
			return num;
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00066F87 File Offset: 0x00065187
		public override void SetLength(long value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00066F93 File Offset: 0x00065193
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_store.Write(this.m_position, buffer, offset, count);
			this.m_position += (long)count;
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00066FB8 File Offset: 0x000651B8
		protected override void Dispose(bool disposing)
		{
			this.m_store.Close(false);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00066FB8 File Offset: 0x000651B8
		public override void Close()
		{
			this.m_store.Close(false);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00066FC6 File Offset: 0x000651C6
		public void Close(bool commit)
		{
			this.m_store.Close(commit);
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x00066FD4 File Offset: 0x000651D4
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

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x00066FF8 File Offset: 0x000651F8
		public long TimeCompressing
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

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x00067020 File Offset: 0x00065220
		public long TimeUncompressing
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

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00067048 File Offset: 0x00065248
		public long CompressedLength
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

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00066EE7 File Offset: 0x000650E7
		public long UncompressedLength
		{
			get
			{
				return this.m_store.Length;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x00067070 File Offset: 0x00065270
		public bool CanProvideHints
		{
			get
			{
				IBufferedStreamHintProvider bufferedStreamHintProvider = this.m_store as IBufferedStreamHintProvider;
				return bufferedStreamHintProvider != null && bufferedStreamHintProvider.CanProvideHints;
			}
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x00067094 File Offset: 0x00065294
		public BufferedStreamHint GetReadHint(int availableBufferSpace, long readPosition)
		{
			BufferedStreamHint bufferedStreamHint = default(BufferedStreamHint);
			IBufferedStreamHintProvider bufferedStreamHintProvider = this.m_store as IBufferedStreamHintProvider;
			if (bufferedStreamHintProvider != null && bufferedStreamHintProvider.CanProvideHints)
			{
				bufferedStreamHint = bufferedStreamHintProvider.GetReadHint(availableBufferSpace, readPosition);
			}
			return bufferedStreamHint;
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x000670CC File Offset: 0x000652CC
		public void UpdateSnapshot(Guid newSnapshotId)
		{
			IUpdateSnapshot updateSnapshot = this.m_store as IUpdateSnapshot;
			if (updateSnapshot != null)
			{
				updateSnapshot.UpdateSnapshot(newSnapshotId);
			}
		}

		// Token: 0x04000950 RID: 2384
		private readonly IStorage m_store;

		// Token: 0x04000951 RID: 2385
		private long m_position;
	}
}
