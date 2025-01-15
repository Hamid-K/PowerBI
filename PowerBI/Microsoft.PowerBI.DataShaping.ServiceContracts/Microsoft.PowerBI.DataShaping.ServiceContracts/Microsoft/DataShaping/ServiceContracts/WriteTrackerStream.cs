using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x0200000E RID: 14
	public sealed class WriteTrackerStream : Stream
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000027D5 File Offset: 0x000009D5
		public WriteTrackerStream(Stream stream)
		{
			this.m_stream = stream;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000027E4 File Offset: 0x000009E4
		public long BytesWritten
		{
			get
			{
				return this.m_bytesWritten;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000027EC File Offset: 0x000009EC
		public override bool CanRead
		{
			get
			{
				return this.m_stream.CanRead;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000027F9 File Offset: 0x000009F9
		public override bool CanSeek
		{
			get
			{
				return this.m_stream.CanSeek;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002806 File Offset: 0x00000A06
		public override bool CanWrite
		{
			get
			{
				return this.m_stream.CanWrite;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002813 File Offset: 0x00000A13
		public override long Length
		{
			get
			{
				return this.m_stream.Length;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002820 File Offset: 0x00000A20
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000282D File Offset: 0x00000A2D
		public override long Position
		{
			get
			{
				return this.m_stream.Position;
			}
			set
			{
				this.m_stream.Position = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000283B File Offset: 0x00000A3B
		public override bool CanTimeout
		{
			get
			{
				return this.m_stream.CanTimeout;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002848 File Offset: 0x00000A48
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002855 File Offset: 0x00000A55
		public override int ReadTimeout
		{
			get
			{
				return this.m_stream.ReadTimeout;
			}
			set
			{
				this.m_stream.ReadTimeout = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002863 File Offset: 0x00000A63
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002870 File Offset: 0x00000A70
		public override int WriteTimeout
		{
			get
			{
				return this.m_stream.WriteTimeout;
			}
			set
			{
				this.m_stream.WriteTimeout = value;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000287E File Offset: 0x00000A7E
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			return this.m_stream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000288E File Offset: 0x00000A8E
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.m_stream.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000028A5 File Offset: 0x00000AA5
		public override void Flush()
		{
			this.m_stream.Flush();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000028B2 File Offset: 0x00000AB2
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this.m_stream.FlushAsync(cancellationToken);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000028C0 File Offset: 0x00000AC0
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.m_stream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000028D4 File Offset: 0x00000AD4
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.m_stream.EndRead(asyncResult);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000028E2 File Offset: 0x00000AE2
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.m_bytesWritten += (long)count;
			return this.m_stream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002905 File Offset: 0x00000B05
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.m_stream.EndWrite(asyncResult);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002913 File Offset: 0x00000B13
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_stream.Read(buffer, offset, count);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002923 File Offset: 0x00000B23
		public override int ReadByte()
		{
			return this.m_stream.ReadByte();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002930 File Offset: 0x00000B30
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this.m_stream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002942 File Offset: 0x00000B42
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_stream.Seek(offset, origin);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002951 File Offset: 0x00000B51
		public override void SetLength(long value)
		{
			this.m_stream.SetLength(value);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000295F File Offset: 0x00000B5F
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_bytesWritten += (long)count;
			this.m_stream.Write(buffer, offset, count);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000297E File Offset: 0x00000B7E
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.m_bytesWritten += (long)count;
			return this.m_stream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000299F File Offset: 0x00000B9F
		public override void WriteByte(byte value)
		{
			this.m_bytesWritten += 1L;
			this.m_stream.WriteByte(value);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000029BC File Offset: 0x00000BBC
		public override bool Equals(object obj)
		{
			return this.m_stream.Equals(obj);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000029CA File Offset: 0x00000BCA
		public override int GetHashCode()
		{
			return this.m_stream.GetHashCode();
		}

		// Token: 0x04000080 RID: 128
		private readonly Stream m_stream;

		// Token: 0x04000081 RID: 129
		private long m_bytesWritten;
	}
}
