using System;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x02000135 RID: 309
	internal sealed class NonDisposingStream : Stream
	{
		// Token: 0x06000BBB RID: 3003 RVA: 0x0002CA26 File Offset: 0x0002AC26
		internal NonDisposingStream(Stream innerStream)
		{
			this.innerStream = innerStream;
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0002CA35 File Offset: 0x0002AC35
		public override bool CanRead
		{
			get
			{
				return this.innerStream.CanRead;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0002CA42 File Offset: 0x0002AC42
		public override bool CanSeek
		{
			get
			{
				return this.innerStream.CanSeek;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0002CA4F File Offset: 0x0002AC4F
		public override bool CanWrite
		{
			get
			{
				return this.innerStream.CanWrite;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0002CA5C File Offset: 0x0002AC5C
		public override long Length
		{
			get
			{
				return this.innerStream.Length;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0002CA69 File Offset: 0x0002AC69
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x0002CA76 File Offset: 0x0002AC76
		public override long Position
		{
			get
			{
				return this.innerStream.Position;
			}
			set
			{
				this.innerStream.Position = value;
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002CA84 File Offset: 0x0002AC84
		public override void Flush()
		{
			this.innerStream.Flush();
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002CA91 File Offset: 0x0002AC91
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.innerStream.Read(buffer, offset, count);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002CAA1 File Offset: 0x0002ACA1
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.innerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002CAB5 File Offset: 0x0002ACB5
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.innerStream.EndRead(asyncResult);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0002CAC3 File Offset: 0x0002ACC3
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.innerStream.Seek(offset, origin);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002CAD2 File Offset: 0x0002ACD2
		public override void SetLength(long value)
		{
			this.innerStream.SetLength(value);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002CAE0 File Offset: 0x0002ACE0
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.innerStream.Write(buffer, offset, count);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002CAF0 File Offset: 0x0002ACF0
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.innerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002CB04 File Offset: 0x0002AD04
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.innerStream.EndWrite(asyncResult);
		}

		// Token: 0x040004E9 RID: 1257
		private readonly Stream innerStream;
	}
}
