using System;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000095 RID: 149
	internal sealed class ThreadWorkItemCancelableStep : CancelablePhaseBase
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000E78C File Offset: 0x0000C98C
		internal ThreadWorkItemCancelableStep(RunningJobContext jobCtx, WaitCallback callback, object callbackState, bool allowCancel)
			: base(jobCtx, callback, callbackState, allowCancel)
		{
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000E799 File Offset: 0x0000C999
		protected override void Execute()
		{
			throw new NotImplementedException();
		}
	}
}
