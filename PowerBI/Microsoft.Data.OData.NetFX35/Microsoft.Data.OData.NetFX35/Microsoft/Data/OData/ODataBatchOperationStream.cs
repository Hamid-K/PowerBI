using System;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x020001D3 RID: 467
	internal abstract class ODataBatchOperationStream : Stream
	{
		// Token: 0x06000DB1 RID: 3505 RVA: 0x00030A44 File Offset: 0x0002EC44
		internal ODataBatchOperationStream(IODataBatchOperationListener listener)
		{
			this.listener = listener;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00030A53 File Offset: 0x0002EC53
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00030A5A File Offset: 0x0002EC5A
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.listener != null)
			{
				this.listener.BatchOperationContentStreamDisposed();
				this.listener = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00030A80 File Offset: 0x0002EC80
		protected void ValidateNotDisposed()
		{
			if (this.listener == null)
			{
				throw new ObjectDisposedException(null, Strings.ODataBatchOperationStream_Disposed);
			}
		}

		// Token: 0x040004FE RID: 1278
		private IODataBatchOperationListener listener;
	}
}
