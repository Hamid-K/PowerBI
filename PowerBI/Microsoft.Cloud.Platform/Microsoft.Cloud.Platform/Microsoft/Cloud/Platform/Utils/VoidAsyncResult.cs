using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002E5 RID: 741
	public class VoidAsyncResult : AsyncResult
	{
		// Token: 0x060013C7 RID: 5063 RVA: 0x00023A12 File Offset: 0x00021C12
		public VoidAsyncResult(AsyncCallback callback, object context)
			: base(callback, context)
		{
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00023A2D File Offset: 0x00021C2D
		public void SignalCompletion(bool completedSynchronously, Exception ex)
		{
			base.SignalCompletionInternal(completedSynchronously, ex);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00027E42 File Offset: 0x00026042
		public void SignalCompletion(bool completedSynchronously)
		{
			base.SignalCompletionInternal(completedSynchronously);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00023D93 File Offset: 0x00021F93
		public virtual void End()
		{
			base.EndInternal();
		}
	}
}
