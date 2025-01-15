using System;
using System.IO;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015D8 RID: 5592
	internal class ProgressStream : Stream, ILeaveEngineContext<Stream>
	{
		// Token: 0x06008CA2 RID: 36002 RVA: 0x001D7D7D File Offset: 0x001D5F7D
		public ProgressStream(Stream stream, IHostProgress hostProgress)
		{
			this.stream = stream;
			this.hostProgress = hostProgress;
		}

		// Token: 0x06008CA3 RID: 36003 RVA: 0x001D7D93 File Offset: 0x001D5F93
		protected override void Dispose(bool disposing)
		{
			this.RecordBytesReadAndWritten(true);
			this.stream.Dispose();
		}

		// Token: 0x170024DD RID: 9437
		// (get) Token: 0x06008CA4 RID: 36004 RVA: 0x001D7DA7 File Offset: 0x001D5FA7
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x170024DE RID: 9438
		// (get) Token: 0x06008CA5 RID: 36005 RVA: 0x001D7DB4 File Offset: 0x001D5FB4
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x170024DF RID: 9439
		// (get) Token: 0x06008CA6 RID: 36006 RVA: 0x001D7DC1 File Offset: 0x001D5FC1
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x06008CA7 RID: 36007 RVA: 0x001D7DCE File Offset: 0x001D5FCE
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x170024E0 RID: 9440
		// (get) Token: 0x06008CA8 RID: 36008 RVA: 0x001D7DDB File Offset: 0x001D5FDB
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x170024E1 RID: 9441
		// (get) Token: 0x06008CA9 RID: 36009 RVA: 0x001D7DE8 File Offset: 0x001D5FE8
		// (set) Token: 0x06008CAA RID: 36010 RVA: 0x001D7DF5 File Offset: 0x001D5FF5
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

		// Token: 0x06008CAB RID: 36011 RVA: 0x001D7E04 File Offset: 0x001D6004
		private void RecordBytesReadAndWritten(bool force)
		{
			if (this.pendingReadBytes + this.pendingWriteBytes > 1024L || force)
			{
				this.hostProgress.RecordBytesRead(this.pendingReadBytes);
				this.hostProgress.RecordBytesWritten(this.pendingWriteBytes);
				this.pendingReadBytes = 0L;
				this.pendingWriteBytes = 0L;
			}
		}

		// Token: 0x06008CAC RID: 36012 RVA: 0x001D7E5C File Offset: 0x001D605C
		public override int ReadByte()
		{
			int num = this.stream.ReadByte();
			if (num >= 0)
			{
				this.pendingReadBytes += 1L;
			}
			return num;
		}

		// Token: 0x06008CAD RID: 36013 RVA: 0x001D7E7C File Offset: 0x001D607C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.stream.Read(buffer, offset, count);
			if (num >= 0)
			{
				this.pendingReadBytes += (long)num;
				this.RecordBytesReadAndWritten(false);
			}
			return num;
		}

		// Token: 0x06008CAE RID: 36014 RVA: 0x001D7EB3 File Offset: 0x001D60B3
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06008CAF RID: 36015 RVA: 0x001D7EC2 File Offset: 0x001D60C2
		public override void SetLength(long value)
		{
			this.stream.SetLength(value);
		}

		// Token: 0x06008CB0 RID: 36016 RVA: 0x001D7ED0 File Offset: 0x001D60D0
		public override void WriteByte(byte value)
		{
			this.stream.WriteByte(value);
			this.pendingWriteBytes += 1L;
		}

		// Token: 0x06008CB1 RID: 36017 RVA: 0x001D7EED File Offset: 0x001D60ED
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
			this.pendingWriteBytes += (long)count;
			this.RecordBytesReadAndWritten(false);
		}

		// Token: 0x06008CB2 RID: 36018 RVA: 0x001D7F13 File Offset: 0x001D6113
		public Stream Leave()
		{
			return new ProgressStream(this.stream.LeaveEngineContext<Stream>(), this.hostProgress);
		}

		// Token: 0x04004CBA RID: 19642
		private const long maxPendingBytes = 1024L;

		// Token: 0x04004CBB RID: 19643
		private readonly Stream stream;

		// Token: 0x04004CBC RID: 19644
		private readonly IHostProgress hostProgress;

		// Token: 0x04004CBD RID: 19645
		private long pendingReadBytes;

		// Token: 0x04004CBE RID: 19646
		private long pendingWriteBytes;
	}
}
