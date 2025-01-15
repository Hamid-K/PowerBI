using System;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000425 RID: 1061
	internal class HighPriorityTimerThatShouldNotExpire : TimerThatShouldNotExpire
	{
		// Token: 0x060024F1 RID: 9457 RVA: 0x00070FF0 File Offset: 0x0006F1F0
		public HighPriorityTimerThatShouldNotExpire(WaitCallback callback, object state)
			: this(callback, state, HighPriorityTimerQueue.Singleton)
		{
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x00070FFF File Offset: 0x0006F1FF
		public HighPriorityTimerThatShouldNotExpire(WaitCallback callback, object state, HighPriorityTimerQueue timerQueue)
			: base(callback, state, timerQueue)
		{
		}
	}
}
