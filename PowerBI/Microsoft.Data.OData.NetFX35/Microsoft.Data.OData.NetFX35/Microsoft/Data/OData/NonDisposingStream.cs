using System;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x020001CA RID: 458
	internal sealed class NonDisposingStream : Stream
	{
		// Token: 0x06000D78 RID: 3448 RVA: 0x00030164 File Offset: 0x0002E364
		internal NonDisposingStream(Stream innerStream)
		{
			this.innerStream = innerStream;
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00030173 File Offset: 0x0002E373
		public override bool CanRead
		{
			get
			{
				return this.innerStream.CanRead;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00030180 File Offset: 0x0002E380
		public override bool CanSeek
		{
			get
			{
				return this.innerStream.CanSeek;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x0003018D File Offset: 0x0002E38D
		public override bool CanWrite
		{
			get
			{
				return this.innerStream.CanWrite;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0003019A File Offset: 0x0002E39A
		public override long Length
		{
			get
			{
				return this.innerStream.Length;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x000301A7 File Offset: 0x0002E3A7
		// (set) Token: 0x06000D7E RID: 3454 RVA: 0x000301B4 File Offset: 0x0002E3B4
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

		// Token: 0x06000D7F RID: 3455 RVA: 0x000301C2 File Offset: 0x0002E3C2
		public override void Flush()
		{
			this.innerStream.Flush();
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x000301CF File Offset: 0x0002E3CF
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.innerStream.Read(buffer, offset, count);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000301DF File Offset: 0x0002E3DF
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.innerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000301F3 File Offset: 0x0002E3F3
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.innerStream.EndRead(asyncResult);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00030201 File Offset: 0x0002E401
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.innerStream.Seek(offset, origin);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00030210 File Offset: 0x0002E410
		public override void SetLength(long value)
		{
			this.innerStream.SetLength(value);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0003021E File Offset: 0x0002E41E
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.innerStream.Write(buffer, offset, count);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0003022E File Offset: 0x0002E42E
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.innerStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00030242 File Offset: 0x0002E442
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.innerStream.EndWrite(asyncResult);
		}

		// Token: 0x040004B0 RID: 1200
		private readonly Stream innerStream;
	}
}
