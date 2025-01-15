using System;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000033 RID: 51
	internal abstract class ODataBatchOperationStream : Stream
	{
		// Token: 0x0600016F RID: 367 RVA: 0x00005E5F File Offset: 0x0000405F
		internal ODataBatchOperationStream(IODataBatchOperationListener listener)
		{
			this.listener = listener;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00002506 File Offset: 0x00000706
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005E6E File Offset: 0x0000406E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.listener != null)
			{
				this.listener.BatchOperationContentStreamDisposed();
				this.listener = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005E94 File Offset: 0x00004094
		protected void ValidateNotDisposed()
		{
			if (this.listener == null)
			{
				throw new ObjectDisposedException(null, Strings.ODataBatchOperationStream_Disposed);
			}
		}

		// Token: 0x040000DF RID: 223
		private IODataBatchOperationListener listener;
	}
}
