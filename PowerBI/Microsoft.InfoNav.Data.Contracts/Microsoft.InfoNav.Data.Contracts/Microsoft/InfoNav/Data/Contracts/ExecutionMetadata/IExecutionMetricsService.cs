using System;

namespace Microsoft.InfoNav.Data.Contracts.ExecutionMetadata
{
	// Token: 0x020000FB RID: 251
	public interface IExecutionMetricsService
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000688 RID: 1672
		bool ExceededMaxEventCount { get; }

		// Token: 0x06000689 RID: 1673
		ITimedEventTracker BeginEvent(string eventName, string componentName);

		// Token: 0x0600068A RID: 1674
		IInstantEventTracker FireInstantEvent(string eventName, string componentName, bool bypassMaxEventCount = false);

		// Token: 0x0600068B RID: 1675
		void AttachExternalEvent(ExecutionEvent execEvent);

		// Token: 0x0600068C RID: 1676
		ExecutionMetrics ToExecutionMetrics();
	}
}
