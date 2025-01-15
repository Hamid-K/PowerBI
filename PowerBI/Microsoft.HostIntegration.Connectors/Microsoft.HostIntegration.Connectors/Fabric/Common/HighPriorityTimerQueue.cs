using System;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200042C RID: 1068
	internal class HighPriorityTimerQueue : TimerQueue
	{
		// Token: 0x06002523 RID: 9507 RVA: 0x00071E62 File Offset: 0x00070062
		private HighPriorityTimerQueue()
			: this(IOCompletionPortWorkQueue.HighPriorityWorkQueue)
		{
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x00071E6F File Offset: 0x0007006F
		public HighPriorityTimerQueue(IOCompletionPortWorkQueue workQueue)
			: base(new DedicatedWaitableTimer(ThreadPriority.Highest))
		{
			this.m_workQueue = workQueue;
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x00071E84 File Offset: 0x00070084
		protected override bool InvokeExpiredTimer(TimerObject expiredTimer, WaitCallback callback)
		{
			this.m_workQueue.QueueWorkItem(callback, expiredTimer);
			return true;
		}

		// Token: 0x04001699 RID: 5785
		private IOCompletionPortWorkQueue m_workQueue;

		// Token: 0x0400169A RID: 5786
		public static readonly HighPriorityTimerQueue Singleton = new HighPriorityTimerQueue();
	}
}
