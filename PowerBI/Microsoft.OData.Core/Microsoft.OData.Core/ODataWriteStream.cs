using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x0200008A RID: 138
	internal sealed class ODataWriteStream : ODataStream
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x0000C36C File Offset: 0x0000A56C
		internal ODataWriteStream(Stream stream, IODataStreamListener listener)
			: base(listener)
		{
			this.stream = stream;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00002393 File Offset: 0x00000593
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000C37C File Offset: 0x0000A57C
		public override long Length
		{
			get
			{
				base.ValidateNotDisposed();
				return this.stream.Length;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000C38F File Offset: 0x0000A58F
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x00002396 File Offset: 0x00000596
		public override long Position
		{
			get
			{
				base.ValidateNotDisposed();
				return this.stream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000C3A2 File Offset: 0x0000A5A2
		public override void SetLength(long value)
		{
			base.ValidateNotDisposed();
			this.stream.SetLength(value);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000C3B6 File Offset: 0x0000A5B6
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.ValidateNotDisposed();
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000C3CC File Offset: 0x0000A5CC
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			base.ValidateNotDisposed();
			return this.stream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00002396 File Offset: 0x00000596
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000C3E4 File Offset: 0x0000A5E4
		public override void Flush()
		{
			base.ValidateNotDisposed();
			this.stream.Flush();
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000C3F7 File Offset: 0x0000A5F7
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.stream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000221 RID: 545
		private Stream stream;
	}
}
