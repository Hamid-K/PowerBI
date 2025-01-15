using System;
using System.IO;
using System.Threading;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Utils
{
	// Token: 0x02000039 RID: 57
	public class ReadWriteStream : Stream
	{
		// Token: 0x060002CF RID: 719 RVA: 0x0000B7C6 File Offset: 0x000099C6
		public override void Flush()
		{
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000882B File Offset: 0x00006A2B
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000882B File Offset: 0x00006A2B
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000B7C8 File Offset: 0x000099C8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (offset != 0)
			{
				throw new ArgumentException("Offset must be zero.", "count");
			}
			if (this._readOffset == 0)
			{
				this._writeDone.WaitOne();
			}
			int num = this._bytesInBuffer - this._readOffset;
			int num2 = ((count < num) ? count : num);
			Array.Copy(this._buffer, this._readOffset, buffer, 0, num2);
			this._readOffset += num2;
			if (this._readOffset >= this._bytesInBuffer)
			{
				this._readOffset = 0;
				this._readDone.Set();
			}
			return num2;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000B858 File Offset: 0x00009A58
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (offset != 0)
			{
				throw new ArgumentException("Offset must be zero.", "count");
			}
			this._readDone.WaitOne();
			if (count > this._bufferSize)
			{
				this._buffer = new byte[count];
				this._bufferSize = count;
			}
			Array.Copy(buffer, this._buffer, count);
			this._bytesInBuffer = count;
			this._writeDone.Set();
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00005557 File Offset: 0x00003757
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00003DC2 File Offset: 0x00001FC2
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00005557 File Offset: 0x00003757
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000882B File Offset: 0x00006A2B
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		public override long Position { get; set; }

		// Token: 0x060002DA RID: 730 RVA: 0x0000B8D1 File Offset: 0x00009AD1
		protected override void Dispose(bool disposing)
		{
			this.Write(new byte[0], 0, 0);
		}

		// Token: 0x040000A8 RID: 168
		private int _bufferSize;

		// Token: 0x040000A9 RID: 169
		private int _bytesInBuffer;

		// Token: 0x040000AA RID: 170
		private readonly AutoResetEvent _writeDone = new AutoResetEvent(false);

		// Token: 0x040000AB RID: 171
		private readonly AutoResetEvent _readDone = new AutoResetEvent(true);

		// Token: 0x040000AC RID: 172
		private byte[] _buffer;

		// Token: 0x040000AD RID: 173
		private int _readOffset;
	}
}
