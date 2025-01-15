using System;
using System.IO;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000298 RID: 664
	public sealed class MaxReadLengthStream : Stream
	{
		// Token: 0x060011DA RID: 4570 RVA: 0x0003E6CC File Offset: 0x0003C8CC
		public MaxReadLengthStream(Stream stream, long maxReadLength)
		{
			this.m_stream = stream;
			this.m_maxReadLength = maxReadLength;
			this.m_bytesRead = 0L;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x0003E6EA File Offset: 0x0003C8EA
		public override bool CanRead
		{
			get
			{
				return this.m_stream.CanRead;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0003E6F7 File Offset: 0x0003C8F7
		public override bool CanSeek
		{
			get
			{
				return this.m_stream.CanSeek;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x0003E704 File Offset: 0x0003C904
		public override bool CanWrite
		{
			get
			{
				return this.m_stream.CanWrite;
			}
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0003E711 File Offset: 0x0003C911
		public override void Flush()
		{
			this.m_stream.Flush();
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x0003E71E File Offset: 0x0003C91E
		public override long Length
		{
			get
			{
				return this.m_stream.Length;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0003E72B File Offset: 0x0003C92B
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x0003E738 File Offset: 0x0003C938
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

		// Token: 0x060011E2 RID: 4578 RVA: 0x0003E748 File Offset: 0x0003C948
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.m_stream.Read(buffer, offset, count);
			Interlocked.Add(ref this.m_bytesRead, (long)num);
			if (this.m_bytesRead > this.m_maxReadLength)
			{
				throw new StreamReadLengthExceededException(this.m_bytesRead, this.m_maxReadLength);
			}
			return num;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0003E793 File Offset: 0x0003C993
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_stream.Seek(offset, origin);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0003E7A2 File Offset: 0x0003C9A2
		public override void SetLength(long value)
		{
			this.m_stream.SetLength(value);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0003E7B0 File Offset: 0x0003C9B0
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_stream.Write(buffer, offset, count);
		}

		// Token: 0x040006A9 RID: 1705
		private Stream m_stream;

		// Token: 0x040006AA RID: 1706
		private long m_bytesRead;

		// Token: 0x040006AB RID: 1707
		private long m_maxReadLength;
	}
}
