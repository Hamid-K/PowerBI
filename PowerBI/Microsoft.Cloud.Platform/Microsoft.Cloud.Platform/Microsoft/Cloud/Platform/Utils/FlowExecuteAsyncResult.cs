using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000283 RID: 643
	internal class FlowExecuteAsyncResult : AsyncResult
	{
		// Token: 0x06001159 RID: 4441 RVA: 0x00023A12 File Offset: 0x00021C12
		public FlowExecuteAsyncResult(AsyncCallback callback, object state)
			: base(callback, state)
		{
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00023D93 File Offset: 0x00021F93
		public void End()
		{
			base.EndInternal();
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00027E42 File Offset: 0x00026042
		public void SignalCompletion(bool completedSynchronously)
		{
			base.SignalCompletionInternal(completedSynchronously);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00023A2D File Offset: 0x00021C2D
		public void SignalCompletion(bool completedSynchronously, Exception exception)
		{
			base.SignalCompletionInternal(completedSynchronously, exception);
		}
	}
}
