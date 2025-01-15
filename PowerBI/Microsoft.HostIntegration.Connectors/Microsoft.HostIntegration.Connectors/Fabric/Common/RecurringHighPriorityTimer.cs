using System;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000426 RID: 1062
	internal class RecurringHighPriorityTimer : RecurringTimer
	{
		// Token: 0x060024F3 RID: 9459 RVA: 0x0007100A File Offset: 0x0006F20A
		public RecurringHighPriorityTimer(WaitCallback callback, object state, TimeSpan expirationInterval)
			: this(callback, state, expirationInterval, HighPriorityTimerQueue.Singleton)
		{
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x0007101A File Offset: 0x0006F21A
		public RecurringHighPriorityTimer(WaitCallback callback, object state, TimeSpan expirationInterval, HighPriorityTimerQueue timerQueue)
			: base(callback, state, expirationInterval, timerQueue)
		{
		}
	}
}
