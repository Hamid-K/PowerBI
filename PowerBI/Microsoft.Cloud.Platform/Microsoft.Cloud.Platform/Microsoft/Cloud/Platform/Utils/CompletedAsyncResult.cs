using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001C5 RID: 453
	public class CompletedAsyncResult<TResult> : AsyncResult<TResult>
	{
		// Token: 0x06000B9A RID: 2970 RVA: 0x00028754 File Offset: 0x00026954
		public CompletedAsyncResult(AsyncCallback callback, object asyncState, TResult res)
			: this(SyncOrNot.Sync, callback, asyncState, res)
		{
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00028760 File Offset: 0x00026960
		public CompletedAsyncResult(SyncOrNot syncOrNot, AsyncCallback callback, object asyncState, TResult res)
			: base(callback, asyncState, res)
		{
			CompletedAsyncResult<TResult> <>4__this = this;
			if (syncOrNot == SyncOrNot.Sync)
			{
				this.SignalCompletion(true, res);
				return;
			}
			AsyncInvoker.InvokeMethodAsynchronously(delegate
			{
				<>4__this.SignalCompletion(false, res);
			}, null, "CompletedAsyncResult.SignalCompletion");
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000287B8 File Offset: 0x000269B8
		public static TResult End(IAsyncResult asyncResult)
		{
			return ((CompletedAsyncResult<TResult>)asyncResult).End();
		}
	}
}
