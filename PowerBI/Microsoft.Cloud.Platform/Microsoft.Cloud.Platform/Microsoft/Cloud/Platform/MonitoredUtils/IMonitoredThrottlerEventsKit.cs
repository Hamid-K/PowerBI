using System;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200012D RID: 301
	[EventsKit(4898286443209560384L)]
	[PerformanceCountersCategory("Throttlers")]
	public interface IMonitoredThrottlerEventsKit
	{
		// Token: 0x060007FB RID: 2043
		[Event(1166419430347426007L, 1, Level = EventLevel.Verbose, Version = 1)]
		[PerformanceCounter(800516866, "CurrentRunningOperations", CounterModifier.Set, "runningOperations", CounterType.NumberOfItems)]
		[PerformanceCounter(1514720771, "CurrentPendingOperations", CounterModifier.Set, "pendingOperations", CounterType.NumberOfItems)]
		void NotifyThrottlerItemsCountChanged(int maxRunningOperations, int runningOperations, int maxPendingOperations, int pendingOperations);

		// Token: 0x060007FC RID: 2044
		[Event(8207115451424873908L, 2, Level = EventLevel.Error, Version = 1)]
		[Trace(typeof(MonitoredUtilsTrace))]
		[PublishedEvent]
		[PerformanceCounter(1660597200, "NumberOfItemsOverflowed", CounterModifier.Increment, CounterType.CounterDelta)]
		[PerformanceCounter(121170645, "TotalItemsOverflowed", CounterModifier.Increment, CounterType.NumberOfItems)]
		void NotifyThrottlerOverflow();

		// Token: 0x060007FD RID: 2045
		[Event(5120885287248296789L, 3, Level = EventLevel.Error, Format = "Throttler has detected a potential deadlock, operation {0} has exceeded timeout, {1}", Version = 1)]
		void NotifyOnPotentialDeadlockDetected(string operationName, string actionDetails);
	}
}
