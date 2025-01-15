using System;
using System.IO;
using System.Net.Http.Internal;

namespace System.Net.Http.Handlers
{
	// Token: 0x0200002C RID: 44
	internal class ProgressWriteAsyncResult : AsyncResult
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00006040 File Offset: 0x00004240
		public ProgressWriteAsyncResult(Stream innerStream, ProgressStream progressStream, byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			: base(callback, state)
		{
			this._innerStream = innerStream;
			this._progressStream = progressStream;
			this._count = count;
			try
			{
				IAsyncResult asyncResult = innerStream.BeginWrite(buffer, offset, count, ProgressWriteAsyncResult._writeCompletedCallback, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.WriteCompleted(asyncResult);
				}
			}
			catch (Exception ex)
			{
				base.Complete(true, ex);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000060AC File Offset: 0x000042AC
		private static void WriteCompletedCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ProgressWriteAsyncResult progressWriteAsyncResult = (ProgressWriteAsyncResult)result.AsyncState;
			try
			{
				progressWriteAsyncResult.WriteCompleted(result);
			}
			catch (Exception ex)
			{
				progressWriteAsyncResult.Complete(false, ex);
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000060F4 File Offset: 0x000042F4
		private void WriteCompleted(IAsyncResult result)
		{
			this._innerStream.EndWrite(result);
			this._progressStream.ReportBytesSent(this._count, base.AsyncState);
			base.Complete(result.CompletedSynchronously);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006125 File Offset: 0x00004325
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ProgressWriteAsyncResult>(result);
		}

		// Token: 0x04000084 RID: 132
		private static readonly AsyncCallback _writeCompletedCallback = new AsyncCallback(ProgressWriteAsyncResult.WriteCompletedCallback);

		// Token: 0x04000085 RID: 133
		private readonly Stream _innerStream;

		// Token: 0x04000086 RID: 134
		private readonly ProgressStream _progressStream;

		// Token: 0x04000087 RID: 135
		private readonly int _count;
	}
}
