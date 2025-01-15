using System;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000424 RID: 1060
	internal class HighPriorityTimer : Timer
	{
		// Token: 0x060024EF RID: 9455 RVA: 0x00070FE1 File Offset: 0x0006F1E1
		public HighPriorityTimer(WaitCallback callback, object state)
			: this(callback, state, HighPriorityTimerQueue.Singleton)
		{
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x00070F5F File Offset: 0x0006F15F
		public HighPriorityTimer(WaitCallback callback, object state, HighPriorityTimerQueue timerQueue)
			: base(callback, state, timerQueue)
		{
		}
	}
}
