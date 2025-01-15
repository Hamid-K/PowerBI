using System;
using System.Threading.Tasks;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x02000018 RID: 24
	public interface ITelemetryService
	{
		// Token: 0x06000084 RID: 132
		void RunInActivity(ActivityKind activity, Action action);

		// Token: 0x06000085 RID: 133
		T RunInActivity<T>(ActivityKind activity, Func<T> action);

		// Token: 0x06000086 RID: 134
		Task RunInAsyncActivity(ActivityKind activity, Func<Task> action);

		// Token: 0x06000087 RID: 135
		Task<T> RunInAsyncActivity<T>(ActivityKind activity, Func<Task<T>> action);

		// Token: 0x06000088 RID: 136
		void FireEvent(DataShapingEvents eventKind, params object[] args);

		// Token: 0x06000089 RID: 137
		void FireSanitizedEvent(DataShapingEvents eventKind, params object[] args);

		// Token: 0x0600008A RID: 138
		bool TryGetTelemetryIDs(out string clientActivityId, out string currentActivityId, out string rootActivityId);
	}
}
