using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x0200004F RID: 79
	internal sealed class NonDisposingStream : Stream
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00007BEF File Offset: 0x00005DEF
		internal NonDisposingStream(Stream innerStream)
		{
			this.innerStream = innerStream;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00007BFE File Offset: 0x00005DFE
		public override bool CanRead
		{
			get
			{
				return this.innerStream.CanRead;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00007C0B File Offset: 0x00005E0B
		public override bool CanSeek
		{
			get
			{
				return this.innerStream.CanSeek;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00007C18 File Offset: 0x00005E18
		public override bool CanWrite
		{
			get
			{
				return this.innerStream.CanWrite;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00007C25 File Offset: 0x00005E25
		public override long Length
		{
			get
			{
				return this.innerStream.Length;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00007C32 File Offset: 0x00005E32
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00007C3F File Offset: 0x00005E3F
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

		// Token: 0x06000276 RID: 630 RVA: 0x00007C4D File Offset: 0x00005E4D
		public override void Flush()
		{
			this.innerStream.Flush();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00007C5A File Offset: 0x00005E5A
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.innerStream.Read(buffer, offset, count);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00007C6C File Offset: 0x00005E6C
		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return await this.innerStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00007CD2 File Offset: 0x00005ED2
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.innerStream.Seek(offset, origin);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00007CE1 File Offset: 0x00005EE1
		public override void SetLength(long value)
		{
			this.innerStream.SetLength(value);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00007CEF File Offset: 0x00005EEF
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.innerStream.Write(buffer, offset, count);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00007CFF File Offset: 0x00005EFF
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this.innerStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0400012B RID: 299
		private readonly Stream innerStream;
	}
}
