using System;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x0200013F RID: 319
	internal abstract class ODataBatchOperationStream : Stream
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x0002D7BC File Offset: 0x0002B9BC
		internal ODataBatchOperationStream(IODataBatchOperationListener listener)
		{
			this.listener = listener;
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002D7CB File Offset: 0x0002B9CB
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002D7D2 File Offset: 0x0002B9D2
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.listener != null)
			{
				this.listener.BatchOperationContentStreamDisposed();
				this.listener = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002D7F8 File Offset: 0x0002B9F8
		protected void ValidateNotDisposed()
		{
			if (this.listener == null)
			{
				throw new ObjectDisposedException(null, Strings.ODataBatchOperationStream_Disposed);
			}
		}

		// Token: 0x04000509 RID: 1289
		private IODataBatchOperationListener listener;
	}
}
