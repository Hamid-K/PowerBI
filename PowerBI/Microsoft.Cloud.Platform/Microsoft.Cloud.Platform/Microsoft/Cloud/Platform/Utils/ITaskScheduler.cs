using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000155 RID: 341
	public interface ITaskScheduler
	{
		// Token: 0x060008E3 RID: 2275
		IScheduledTaskHandle RegisterScheduledTask(string policyName, IScheduledTask task);

		// Token: 0x060008E4 RID: 2276
		IScheduledTaskHandle RegisterScheduledTask(IScheduledTaskPolicyProvider policy, IScheduledTask task);
	}
}
