using System;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000034 RID: 52
	internal sealed class ODataBatchOperationWriteStream : ODataBatchOperationStream
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00005EAA File Offset: 0x000040AA
		internal ODataBatchOperationWriteStream(Stream batchStream, IODataBatchOperationListener listener)
			: base(listener)
		{
			this.batchStream = batchStream;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00002500 File Offset: 0x00000700
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00002500 File Offset: 0x00000700
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00002503 File Offset: 0x00000703
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005EBA File Offset: 0x000040BA
		public override long Length
		{
			get
			{
				base.ValidateNotDisposed();
				return this.batchStream.Length;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005ECD File Offset: 0x000040CD
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00002506 File Offset: 0x00000706
		public override long Position
		{
			get
			{
				base.ValidateNotDisposed();
				return this.batchStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005EE0 File Offset: 0x000040E0
		public override void SetLength(long value)
		{
			base.ValidateNotDisposed();
			this.batchStream.SetLength(value);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005EF4 File Offset: 0x000040F4
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.ValidateNotDisposed();
			this.batchStream.Write(buffer, offset, count);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005F0A File Offset: 0x0000410A
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			base.ValidateNotDisposed();
			return this.batchStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005F24 File Offset: 0x00004124
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.ValidateNotDisposed();
			this.batchStream.EndWrite(asyncResult);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00002506 File Offset: 0x00000706
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005F38 File Offset: 0x00004138
		public override void Flush()
		{
			base.ValidateNotDisposed();
			this.batchStream.Flush();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005F4B File Offset: 0x0000414B
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.batchStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040000E0 RID: 224
		private Stream batchStream;
	}
}
