using System;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200012A RID: 298
	[EventsKit(6862217781056303573L, Level = EventLevel.Informational)]
	[PublishedEvent]
	[Trace(typeof(MonitoredUtilsTrace))]
	[PerformanceCountersCategory("Scheduler")]
	public interface IMonitoredSchedulerEventsKit
	{
		// Token: 0x060007F4 RID: 2036
		[Event(1066590275097244064L, 1, Level = EventLevel.Informational, Version = 1)]
		void NotifyTaskStarted(string taskName);

		// Token: 0x060007F5 RID: 2037
		[Event(2943108009849431250L, 2, Level = EventLevel.Informational, Version = 1)]
		[PerformanceCounter(859381062, "AverageExecutionDuration", CounterModifier.Increment, "duration", CounterType.AverageDelta)]
		[PerformanceCounter(1944908615, "NumberOfSucceededExecutions", CounterModifier.Increment, CounterType.NumberOfItems)]
		[PerformanceCounter(1245636087, "NumberOfExecutions", CounterModifier.Increment, CounterType.NumberOfItems)]
		void NotifyTaskSucceeded(string taskName, int duration);

		// Token: 0x060007F6 RID: 2038
		[Event(4932160858854552034L, 3, Level = EventLevel.Error, Version = 1)]
		[PerformanceCounter(859381062, "AverageExecutionDuration", CounterModifier.Increment, "duration", CounterType.AverageDelta)]
		[PerformanceCounter(1337038393, "NumberOfFailedExecutions", CounterModifier.Increment, CounterType.NumberOfItems)]
		[PerformanceCounter(1245636087, "NumberOfExecutions", CounterModifier.Increment, CounterType.NumberOfItems)]
		void NotifyTaskFailed(string taskName, int duration);

		// Token: 0x060007F7 RID: 2039
		[Event(4439799963543641499L, 4, Level = EventLevel.Warning, Version = 1)]
		void NotifyTaskIngored(string taskName);

		// Token: 0x060007F8 RID: 2040
		[Event(13575081735542734L, 5, Level = EventLevel.Error, Version = 1)]
		void NotifyTaskAborted();
	}
}
