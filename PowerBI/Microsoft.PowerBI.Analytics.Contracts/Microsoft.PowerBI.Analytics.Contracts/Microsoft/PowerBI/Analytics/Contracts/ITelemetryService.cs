using System;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x02000018 RID: 24
	public interface ITelemetryService
	{
		// Token: 0x06000037 RID: 55
		void RunInActivity(string activityName, Action action);

		// Token: 0x06000038 RID: 56
		T RunInActivity<T>(string activityName, Func<T> action);

		// Token: 0x06000039 RID: 57
		Task RunInAsyncActivity(string activityName, Func<Task> action);

		// Token: 0x0600003A RID: 58
		Task<T> RunInAsyncActivity<T>(string activityName, Func<Task<T>> action);

		// Token: 0x0600003B RID: 59
		void FireEvent(string eventName, params object[] args);
	}
}
