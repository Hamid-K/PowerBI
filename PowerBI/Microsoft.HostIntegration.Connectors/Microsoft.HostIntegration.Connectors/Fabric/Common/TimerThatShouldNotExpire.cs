using System;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000422 RID: 1058
	internal class TimerThatShouldNotExpire : Timer
	{
		// Token: 0x060024E6 RID: 9446 RVA: 0x00070E60 File Offset: 0x0006F060
		public TimerThatShouldNotExpire(WaitCallback callback, object state)
			: base(callback, state, NormalPriorityTimerQueue.Singleton)
		{
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x00070F5F File Offset: 0x0006F15F
		public TimerThatShouldNotExpire(WaitCallback callback, object state, TimerQueue timerQueue)
			: base(callback, state, timerQueue)
		{
		}

		// Token: 0x04001684 RID: 5764
		internal static TimerThatShouldNotExpire TimerThatNeverExpires = new TimerThatShouldNotExpire(null, null);
	}
}
