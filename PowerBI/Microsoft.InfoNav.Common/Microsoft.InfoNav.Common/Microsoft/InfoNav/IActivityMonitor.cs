using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000014 RID: 20
	public interface IActivityMonitor<TType>
	{
		// Token: 0x0600019F RID: 415
		void RunActivitySync(TType activityType, Action action);

		// Token: 0x060001A0 RID: 416
		TValue RunActivitySync<TValue>(TType activityType, Func<TValue> action);
	}
}
