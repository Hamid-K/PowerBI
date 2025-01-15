using System;

namespace Microsoft.OData
{
	// Token: 0x02000084 RID: 132
	internal abstract class ODataReadStream : ODataStream
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x0000C039 File Offset: 0x0000A239
		private ODataReadStream(ODataBatchReaderStream batchReaderStream, IODataStreamListener listener)
			: base(listener)
		{
			this.batchReaderStream = batchReaderStream;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00002393 File Offset: 0x00000593
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00002390 File Offset: 0x00000590
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00002396 File Offset: 0x00000596
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00002396 File Offset: 0x00000596
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00002396 File Offset: 0x00000596
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

		// Token: 0x0600049D RID: 1181 RVA: 0x00002396 File Offset: 0x00000596
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00002396 File Offset: 0x00000596
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00002396 File Offset: 0x00000596
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000C049 File Offset: 0x0000A249
		internal static ODataReadStream Create(ODataBatchReaderStream batchReaderStream, IODataStreamListener listener, int length)
		{
			return new ODataReadStream.ODataBatchOperationReadStreamWithLength(batchReaderStream, listener, length);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000C053 File Offset: 0x0000A253
		internal static ODataReadStream Create(ODataBatchReaderStream batchReaderStream, IODataStreamListener listener)
		{
			return new ODataReadStream.ODataBatchOperationReadStreamWithDelimiter(batchReaderStream, listener);
		}

		// Token: 0x0400020F RID: 527
		protected ODataBatchReaderStream batchReaderStream;

		// Token: 0x020002AF RID: 687
		private sealed class ODataBatchOperationReadStreamWithLength : ODataReadStream
		{
			// Token: 0x06001CD5 RID: 7381 RVA: 0x000570A5 File Offset: 0x000552A5
			internal ODataBatchOperationReadStreamWithLength(ODataBatchReaderStream batchReaderStream, IODataStreamListener listener, int length)
				: base(batchReaderStream, listener)
			{
				ExceptionUtils.CheckIntegerNotNegative(length, "length");
				this.length = length;
			}

			// Token: 0x06001CD6 RID: 7382 RVA: 0x000570C4 File Offset: 0x000552C4
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

			// Token: 0x04000C6D RID: 3181
			private int length;
		}

		// Token: 0x020002B0 RID: 688
		private sealed class ODataBatchOperationReadStreamWithDelimiter : ODataReadStream
		{
			// Token: 0x06001CD7 RID: 7383 RVA: 0x0005712C File Offset: 0x0005532C
			internal ODataBatchOperationReadStreamWithDelimiter(ODataBatchReaderStream batchReaderStream, IODataStreamListener listener)
				: base(batchReaderStream, listener)
			{
			}

			// Token: 0x06001CD8 RID: 7384 RVA: 0x00057138 File Offset: 0x00055338
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

			// Token: 0x04000C6E RID: 3182
			private bool exhausted;
		}
	}
}
