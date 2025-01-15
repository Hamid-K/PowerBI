using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000140 RID: 320
	internal abstract class ODataBatchOperationReadStream : ODataBatchOperationStream
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x0002D80E File Offset: 0x0002BA0E
		internal ODataBatchOperationReadStream(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener)
			: base(listener)
		{
			this.batchReaderStream = batchReaderStream;
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0002D81E File Offset: 0x0002BA1E
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0002D821 File Offset: 0x0002BA21
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0002D824 File Offset: 0x0002BA24
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0002D827 File Offset: 0x0002BA27
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0002D82E File Offset: 0x0002BA2E
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x0002D835 File Offset: 0x0002BA35
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

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002D83C File Offset: 0x0002BA3C
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002D843 File Offset: 0x0002BA43
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002D84A File Offset: 0x0002BA4A
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0002D851 File Offset: 0x0002BA51
		internal static ODataBatchOperationReadStream Create(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener, int length)
		{
			return new ODataBatchOperationReadStream.ODataBatchOperationReadStreamWithLength(batchReaderStream, listener, length);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0002D85B File Offset: 0x0002BA5B
		internal static ODataBatchOperationReadStream Create(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener)
		{
			return new ODataBatchOperationReadStream.ODataBatchOperationReadStreamWithDelimiter(batchReaderStream, listener);
		}

		// Token: 0x0400050A RID: 1290
		protected ODataBatchReaderStream batchReaderStream;

		// Token: 0x02000141 RID: 321
		private sealed class ODataBatchOperationReadStreamWithLength : ODataBatchOperationReadStream
		{
			// Token: 0x06000C2F RID: 3119 RVA: 0x0002D864 File Offset: 0x0002BA64
			internal ODataBatchOperationReadStreamWithLength(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener, int length)
				: base(batchReaderStream, listener)
			{
				ExceptionUtils.CheckIntegerNotNegative(length, "length");
				this.length = length;
			}

			// Token: 0x06000C30 RID: 3120 RVA: 0x0002D880 File Offset: 0x0002BA80
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

			// Token: 0x0400050B RID: 1291
			private int length;
		}

		// Token: 0x02000142 RID: 322
		private sealed class ODataBatchOperationReadStreamWithDelimiter : ODataBatchOperationReadStream
		{
			// Token: 0x06000C31 RID: 3121 RVA: 0x0002D8E7 File Offset: 0x0002BAE7
			internal ODataBatchOperationReadStreamWithDelimiter(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener)
				: base(batchReaderStream, listener)
			{
			}

			// Token: 0x06000C32 RID: 3122 RVA: 0x0002D8F4 File Offset: 0x0002BAF4
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

			// Token: 0x0400050C RID: 1292
			private bool exhausted;
		}
	}
}
