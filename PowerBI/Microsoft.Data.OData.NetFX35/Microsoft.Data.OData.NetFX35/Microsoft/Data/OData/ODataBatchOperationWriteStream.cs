using System;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x020001D4 RID: 468
	internal sealed class ODataBatchOperationWriteStream : ODataBatchOperationStream
	{
		// Token: 0x06000DB5 RID: 3509 RVA: 0x00030A96 File Offset: 0x0002EC96
		internal ODataBatchOperationWriteStream(Stream batchStream, IODataBatchOperationListener listener)
			: base(listener)
		{
			this.batchStream = batchStream;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00030AA6 File Offset: 0x0002ECA6
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00030AA9 File Offset: 0x0002ECA9
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00030AAC File Offset: 0x0002ECAC
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00030AAF File Offset: 0x0002ECAF
		public override long Length
		{
			get
			{
				base.ValidateNotDisposed();
				return this.batchStream.Length;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00030AC2 File Offset: 0x0002ECC2
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x00030AD5 File Offset: 0x0002ECD5
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

		// Token: 0x06000DBC RID: 3516 RVA: 0x00030ADC File Offset: 0x0002ECDC
		public override void SetLength(long value)
		{
			base.ValidateNotDisposed();
			this.batchStream.SetLength(value);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00030AF0 File Offset: 0x0002ECF0
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.ValidateNotDisposed();
			this.batchStream.Write(buffer, offset, count);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00030B06 File Offset: 0x0002ED06
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			base.ValidateNotDisposed();
			return this.batchStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00030B20 File Offset: 0x0002ED20
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.ValidateNotDisposed();
			this.batchStream.EndWrite(asyncResult);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00030B34 File Offset: 0x0002ED34
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00030B3B File Offset: 0x0002ED3B
		public override void Flush()
		{
			base.ValidateNotDisposed();
			this.batchStream.Flush();
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00030B4E File Offset: 0x0002ED4E
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.batchStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040004FF RID: 1279
		private Stream batchStream;
	}
}
