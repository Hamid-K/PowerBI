using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002CC RID: 716
	public abstract class PeriodicTimerPolicy
	{
		// Token: 0x0600132C RID: 4908
		internal abstract void SetInitialSchedule(Timer timer);

		// Token: 0x0600132D RID: 4909
		internal abstract bool OnPeriodicCallback(string identity, Timer timer, Action callback);

		// Token: 0x0600132E RID: 4910
		internal abstract void UpdatePeriod(int period, Timer timer, bool tickNow);

		// Token: 0x0600132F RID: 4911 RVA: 0x0004262F File Offset: 0x0004082F
		internal void EnsureValidPeriod(int period)
		{
			ExtendedDiagnostics.EnsureArgument(period, "period", period > 0 || period == -1);
		}
	}
}
