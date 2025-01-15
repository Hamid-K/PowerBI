using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001C6 RID: 454
	public sealed class CompletedAsyncResult : VoidAsyncResult
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x000287C5 File Offset: 0x000269C5
		public CompletedAsyncResult(AsyncCallback callback, object asyncState)
			: this(SyncOrNot.Sync, callback, asyncState)
		{
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000287D0 File Offset: 0x000269D0
		public CompletedAsyncResult(SyncOrNot syncOrNot, AsyncCallback callback, object asyncState)
			: base(callback, asyncState)
		{
			if (syncOrNot == SyncOrNot.Sync)
			{
				base.SignalCompletion(true);
				return;
			}
			AsyncInvoker.InvokeMethodAsynchronously(delegate
			{
				base.SignalCompletion(false);
			}, null, "CompletedAsyncResult.SignalCompletion");
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000287FC File Offset: 0x000269FC
		public CompletedAsyncResult(Exception ex, AsyncCallback callback, object asyncState)
			: this(ex, SyncOrNot.Sync, callback, asyncState)
		{
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00028808 File Offset: 0x00026A08
		public CompletedAsyncResult(Exception ex, SyncOrNot syncOrNot, AsyncCallback callback, object asyncState)
			: base(callback, asyncState)
		{
			CompletedAsyncResult <>4__this = this;
			if (syncOrNot == SyncOrNot.Sync)
			{
				base.SignalCompletion(true, ex);
				return;
			}
			AsyncInvoker.InvokeMethodAsynchronously(delegate
			{
				<>4__this.SignalCompletion(false, ex);
			}, null, "CompletedAsyncResult.SignalCompletion");
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002885A File Offset: 0x00026A5A
		public static void End(IAsyncResult asyncResult)
		{
			((CompletedAsyncResult)asyncResult).End();
		}
	}
}
