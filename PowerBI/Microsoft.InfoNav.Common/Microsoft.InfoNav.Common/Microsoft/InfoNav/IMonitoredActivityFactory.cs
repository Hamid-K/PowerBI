using System;
using System.Threading.Tasks;

namespace Microsoft.InfoNav
{
	// Token: 0x02000019 RID: 25
	public interface IMonitoredActivityFactory
	{
		// Token: 0x060001A7 RID: 423
		void CreateAndInvokeActivitySync(IActivityType activityType, MonitoredActivityAction action);

		// Token: 0x060001A8 RID: 424
		Task CreateAndInvokeActivityAsync(IActivityType activityType, Func<Task> action);

		// Token: 0x060001A9 RID: 425
		Task<T> CreateAndInvokeActivityAsync<T>(IActivityType activityType, Func<Task<T>> action);
	}
}
