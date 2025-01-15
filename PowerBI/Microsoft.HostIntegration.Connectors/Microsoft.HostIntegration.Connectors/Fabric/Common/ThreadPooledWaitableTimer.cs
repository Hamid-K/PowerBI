using System;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000436 RID: 1078
	internal class ThreadPooledWaitableTimer : WaitableTimer
	{
		// Token: 0x06002597 RID: 9623 RVA: 0x0007338A File Offset: 0x0007158A
		public ThreadPooledWaitableTimer()
		{
			this.m_registeredWaitHandle = null;
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x00073399 File Offset: 0x00071599
		public override void SetTimer(ulong dueTime)
		{
			base.SetTimer(dueTime);
			if (this.m_registeredWaitHandle == null)
			{
				this.m_registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(this, base.WaitCallback, null, -1, false);
			}
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000733BF File Offset: 0x000715BF
		public override void CancelTimer()
		{
			base.CancelTimer();
			if (this.m_registeredWaitHandle != null)
			{
				this.m_registeredWaitHandle.Unregister(null);
				this.m_registeredWaitHandle = null;
			}
		}

		// Token: 0x040016B5 RID: 5813
		private RegisteredWaitHandle m_registeredWaitHandle;
	}
}
