using System;

namespace Microsoft.OData
{
	// Token: 0x02000030 RID: 48
	internal abstract class ODataBatchOperationReadStream : ODataBatchOperationStream
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00005BAD File Offset: 0x00003DAD
		internal ODataBatchOperationReadStream(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener)
			: base(listener)
		{
			this.batchReaderStream = batchReaderStream;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00002503 File Offset: 0x00000703
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00002500 File Offset: 0x00000700
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00002500 File Offset: 0x00000700
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00002506 File Offset: 0x00000706
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00002506 File Offset: 0x00000706
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00002506 File Offset: 0x00000706
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

		// Token: 0x0600014E RID: 334 RVA: 0x00002506 File Offset: 0x00000706
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00002506 File Offset: 0x00000706
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00002506 File Offset: 0x00000706
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005BBD File Offset: 0x00003DBD
		internal static ODataBatchOperationReadStream Create(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener, int length)
		{
			return new ODataBatchOperationReadStream.ODataBatchOperationReadStreamWithLength(batchReaderStream, listener, length);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005BC7 File Offset: 0x00003DC7
		internal static ODataBatchOperationReadStream Create(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener)
		{
			return new ODataBatchOperationReadStream.ODataBatchOperationReadStreamWithDelimiter(batchReaderStream, listener);
		}

		// Token: 0x040000D5 RID: 213
		protected ODataBatchReaderStream batchReaderStream;

		// Token: 0x0200024D RID: 589
		private sealed class ODataBatchOperationReadStreamWithLength : ODataBatchOperationReadStream
		{
			// Token: 0x06001756 RID: 5974 RVA: 0x0004722D File Offset: 0x0004542D
			internal ODataBatchOperationReadStreamWithLength(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener, int length)
				: base(batchReaderStream, listener)
			{
				ExceptionUtils.CheckIntegerNotNegative(length, "length");
				this.length = length;
			}

			// Token: 0x06001757 RID: 5975 RVA: 0x0004724C File Offset: 0x0004544C
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

			// Token: 0x04000AC6 RID: 2758
			private int length;
		}

		// Token: 0x0200024E RID: 590
		private sealed class ODataBatchOperationReadStreamWithDelimiter : ODataBatchOperationReadStream
		{
			// Token: 0x06001758 RID: 5976 RVA: 0x000472B4 File Offset: 0x000454B4
			internal ODataBatchOperationReadStreamWithDelimiter(ODataBatchReaderStream batchReaderStream, IODataBatchOperationListener listener)
				: base(batchReaderStream, listener)
			{
			}

			// Token: 0x06001759 RID: 5977 RVA: 0x000472C0 File Offset: 0x000454C0
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

			// Token: 0x04000AC7 RID: 2759
			private bool exhausted;
		}
	}
}
