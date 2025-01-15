using System;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x02000145 RID: 325
	internal sealed class ODataBatchOperationWriteStream : ODataBatchOperationStream
	{
		// Token: 0x06000C4B RID: 3147 RVA: 0x0002DC1D File Offset: 0x0002BE1D
		internal ODataBatchOperationWriteStream(Stream batchStream, IODataBatchOperationListener listener)
			: base(listener)
		{
			this.batchStream = batchStream;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0002DC2D File Offset: 0x0002BE2D
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0002DC30 File Offset: 0x0002BE30
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0002DC33 File Offset: 0x0002BE33
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0002DC36 File Offset: 0x0002BE36
		public override long Length
		{
			get
			{
				base.ValidateNotDisposed();
				return this.batchStream.Length;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0002DC49 File Offset: 0x0002BE49
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x0002DC5C File Offset: 0x0002BE5C
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

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002DC63 File Offset: 0x0002BE63
		public override void SetLength(long value)
		{
			base.ValidateNotDisposed();
			this.batchStream.SetLength(value);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002DC77 File Offset: 0x0002BE77
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.ValidateNotDisposed();
			this.batchStream.Write(buffer, offset, count);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002DC8D File Offset: 0x0002BE8D
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			base.ValidateNotDisposed();
			return this.batchStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002DCA7 File Offset: 0x0002BEA7
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.ValidateNotDisposed();
			this.batchStream.EndWrite(asyncResult);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002DCBB File Offset: 0x0002BEBB
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002DCC2 File Offset: 0x0002BEC2
		public override void Flush()
		{
			base.ValidateNotDisposed();
			this.batchStream.Flush();
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002DCD5 File Offset: 0x0002BED5
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.batchStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000514 RID: 1300
		private Stream batchStream;
	}
}
