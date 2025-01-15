using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x0200000C RID: 12
	internal sealed class ODataNotificationStream : Stream
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00002D57 File Offset: 0x00000F57
		internal ODataNotificationStream(Stream underlyingStream, IODataStreamListener listener)
		{
			this.stream = underlyingStream;
			this.listener = listener;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002D6D File Offset: 0x00000F6D
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002D7A File Offset: 0x00000F7A
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002D87 File Offset: 0x00000F87
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002D94 File Offset: 0x00000F94
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002DA1 File Offset: 0x00000FA1
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002DAE File Offset: 0x00000FAE
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002DBC File Offset: 0x00000FBC
		public override bool CanTimeout
		{
			get
			{
				return this.stream.CanTimeout;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002DC9 File Offset: 0x00000FC9
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002DD6 File Offset: 0x00000FD6
		public override int ReadTimeout
		{
			get
			{
				return this.stream.ReadTimeout;
			}
			set
			{
				this.stream.ReadTimeout = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002DE4 File Offset: 0x00000FE4
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002DF1 File Offset: 0x00000FF1
		public override int WriteTimeout
		{
			get
			{
				return this.stream.WriteTimeout;
			}
			set
			{
				this.stream.WriteTimeout = value;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002DFF File Offset: 0x00000FFF
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002E0C File Offset: 0x0000100C
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002E1C File Offset: 0x0000101C
		public override void SetLength(long value)
		{
			this.stream.SetLength(value);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002E2A File Offset: 0x0000102A
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002E3A File Offset: 0x0000103A
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002E49 File Offset: 0x00001049
		public override int ReadByte()
		{
			return this.stream.ReadByte();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002E56 File Offset: 0x00001056
		public override void WriteByte(byte value)
		{
			this.stream.WriteByte(value);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002E64 File Offset: 0x00001064
		public override string ToString()
		{
			return this.stream.ToString();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002E71 File Offset: 0x00001071
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this.stream.FlushAsync(cancellationToken);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002E7F File Offset: 0x0000107F
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			return this.stream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002E8F File Offset: 0x0000108F
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this.stream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002EA1 File Offset: 0x000010A1
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this.stream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002EB3 File Offset: 0x000010B3
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.listener != null)
			{
				this.listener.StreamDisposed();
				this.listener = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400001D RID: 29
		private readonly Stream stream;

		// Token: 0x0400001E RID: 30
		private IODataStreamListener listener;
	}
}
