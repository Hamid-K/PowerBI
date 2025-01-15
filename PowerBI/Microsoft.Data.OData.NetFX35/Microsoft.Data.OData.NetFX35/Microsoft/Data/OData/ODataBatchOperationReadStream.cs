using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001D5 RID: 469
	internal abstract class ODataBatchOperationReadStream : ODataBatchOperationStream
	{
		// Token: 0x06000DC3 RID: 3523 RVA: 0x00030B61 File Offset: 0x0002ED61
		internal ODataBatchOperationReadStream(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener)
			: base(listener)
		{
			this.batchReaderStream = batchReaderStream;
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00030B71 File Offset: 0x0002ED71
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00030B74 File Offset: 0x0002ED74
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00030B77 File Offset: 0x0002ED77
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00030B7A File Offset: 0x0002ED7A
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00030B81 File Offset: 0x0002ED81
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x00030B88 File Offset: 0x0002ED88
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00030B8F File Offset: 0x0002ED8F
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00030B96 File Offset: 0x0002ED96
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00030B9D File Offset: 0x0002ED9D
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00030BA4 File Offset: 0x0002EDA4
		internal static ODataBatchOperationReadStream Create(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener, int length)
		{
			return new ODataBatchOperationReadStream.ODataBatchOperationReadStreamWithLength(batchReaderStream, listener, length);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00030BAE File Offset: 0x0002EDAE
		internal static ODataBatchOperationReadStream Create(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener)
		{
			return new ODataBatchOperationReadStream.ODataBatchOperationReadStreamWithDelimiter(batchReaderStream, listener);
		}

		// Token: 0x04000500 RID: 1280
		protected ODataBatchReaderStream batchReaderStream;

		// Token: 0x020001D6 RID: 470
		private sealed class ODataBatchOperationReadStreamWithLength : ODataBatchOperationReadStream
		{
			// Token: 0x06000DCF RID: 3535 RVA: 0x00030BB7 File Offset: 0x0002EDB7
			internal ODataBatchOperationReadStreamWithLength(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener, int length)
				: base(batchReaderStream, listener)
			{
				ExceptionUtils.CheckIntegerNotNegative(length, "length");
				this.length = length;
			}

			// Token: 0x06000DD0 RID: 3536 RVA: 0x00030BD4 File Offset: 0x0002EDD4
			public override int Read(byte[] buffer, int offset, int count)
			{
				ExceptionUtils.CheckArgumentNotNull<byte[]>(buffer, "buffer");
				ExceptionUtils.CheckIntegerNotNegative(offset, "offset");
				ExceptionUtils.CheckIntegerNotNegative(count, "count");
				base.ValidateNotDisposed();
				if (this.length == 0)
				{
					return 0;
				}
				int num = this.batchReaderStream.ReadWithLength(buffer, offset, Math.Min(count, this.length));
				this.length -= num;
				return num;
			}

			// Token: 0x04000501 RID: 1281
			private int length;
		}

		// Token: 0x020001D7 RID: 471
		private sealed class ODataBatchOperationReadStreamWithDelimiter : ODataBatchOperationReadStream
		{
			// Token: 0x06000DD1 RID: 3537 RVA: 0x00030C3B File Offset: 0x0002EE3B
			internal ODataBatchOperationReadStreamWithDelimiter(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener)
				: base(batchReaderStream, listener)
			{
			}

			// Token: 0x06000DD2 RID: 3538 RVA: 0x00030C48 File Offset: 0x0002EE48
			public override int Read(byte[] buffer, int offset, int count)
			{
				ExceptionUtils.CheckArgumentNotNull<byte[]>(buffer, "buffer");
				ExceptionUtils.CheckIntegerNotNegative(offset, "offset");
				ExceptionUtils.CheckIntegerNotNegative(count, "count");
				base.ValidateNotDisposed();
				if (this.exhausted)
				{
					return 0;
				}
				int num = this.batchReaderStream.ReadWithDelimiter(buffer, offset, count);
				if (num < count)
				{
					this.exhausted = true;
				}
				return num;
			}

			// Token: 0x04000502 RID: 1282
			private bool exhausted;
		}
	}
}
