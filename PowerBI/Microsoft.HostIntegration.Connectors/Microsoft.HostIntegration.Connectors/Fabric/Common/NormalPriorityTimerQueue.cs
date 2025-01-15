using System;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200042B RID: 1067
	internal class NormalPriorityTimerQueue : TimerQueue
	{
		// Token: 0x0600251F RID: 9503 RVA: 0x00071E25 File Offset: 0x00070025
		private NormalPriorityTimerQueue()
			: this(IOCompletionPortWorkQueue.NormalPriorityWorkQueue)
		{
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x00071E32 File Offset: 0x00070032
		public NormalPriorityTimerQueue(IOCompletionPortWorkQueue workQueue)
			: base(new DedicatedWaitableTimer())
		{
			this.m_workQueue = workQueue;
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x00071E46 File Offset: 0x00070046
		protected override bool InvokeExpiredTimer(TimerObject expiredTimer, WaitCallback callback)
		{
			this.m_workQueue.QueueWorkItem(callback, expiredTimer);
			return true;
		}

		// Token: 0x04001697 RID: 5783
		private IOCompletionPortWorkQueue m_workQueue;

		// Token: 0x04001698 RID: 5784
		public static readonly NormalPriorityTimerQueue Singleton = new NormalPriorityTimerQueue();
	}
}
