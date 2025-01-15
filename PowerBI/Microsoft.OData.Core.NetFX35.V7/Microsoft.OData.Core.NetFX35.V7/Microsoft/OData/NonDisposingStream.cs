using System;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000027 RID: 39
	internal sealed class NonDisposingStream : Stream
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x000050CB File Offset: 0x000032CB
		internal NonDisposingStream(Stream innerStream)
		{
			this.innerStream = innerStream;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000050DA File Offset: 0x000032DA
		public override bool CanRead
		{
			get
			{
				return this.innerStream.CanRead;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000050E7 File Offset: 0x000032E7
		public override bool CanSeek
		{
			get
			{
				return this.innerStream.CanSeek;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000050F4 File Offset: 0x000032F4
		public override bool CanWrite
		{
			get
			{
				return this.innerStream.CanWrite;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005101 File Offset: 0x00003301
		public override long Length
		{
			get
			{
				return this.innerStream.Length;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000510E File Offset: 0x0000330E
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000511B File Offset: 0x0000331B
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

		// Token: 0x060000FD RID: 253 RVA: 0x00005129 File Offset: 0x00003329
		public override void Flush()
		{
			this.innerStream.Flush();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005136 File Offset: 0x00003336
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.innerStream.Read(buffer, offset, count);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005146 File Offset: 0x00003346
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.innerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000515A File Offset: 0x0000335A
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.innerStream.EndRead(asyncResult);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005168 File Offset: 0x00003368
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.innerStream.Seek(offset, origin);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005177 File Offset: 0x00003377
		public override void SetLength(long value)
		{
			this.innerStream.SetLength(value);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005185 File Offset: 0x00003385
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.innerStream.Write(buffer, offset, count);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005195 File Offset: 0x00003395
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.innerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000051A9 File Offset: 0x000033A9
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.innerStream.EndWrite(asyncResult);
		}

		// Token: 0x040000C1 RID: 193
		private readonly Stream innerStream;
	}
}
