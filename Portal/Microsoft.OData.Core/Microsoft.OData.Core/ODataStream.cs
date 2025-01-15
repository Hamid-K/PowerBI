using System;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000089 RID: 137
	internal abstract class ODataStream : Stream
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x0000C321 File Offset: 0x0000A521
		internal ODataStream(IODataStreamListener listener)
		{
			this.listener = listener;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00002396 File Offset: 0x00000596
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000C330 File Offset: 0x0000A530
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.listener != null)
			{
				this.listener.StreamDisposed();
				this.listener = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000C356 File Offset: 0x0000A556
		protected void ValidateNotDisposed()
		{
			if (this.listener == null)
			{
				throw new ObjectDisposedException(null, Strings.ODataBatchOperationStream_Disposed);
			}
		}

		// Token: 0x04000220 RID: 544
		private IODataStreamListener listener;
	}
}
