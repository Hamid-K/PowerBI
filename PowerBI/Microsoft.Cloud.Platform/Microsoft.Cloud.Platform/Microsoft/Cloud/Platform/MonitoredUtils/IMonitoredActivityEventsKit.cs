using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000134 RID: 308
	[EventsKit(770309609189163108L)]
	[PerformanceCountersCategory("Activities")]
	[PublishedEvent]
	[Trace(typeof(MonitoredUtilsTrace))]
	public interface IMonitoredActivityEventsKit
	{
		// Token: 0x0600080A RID: 2058
		[Event(4846840956219803356L, 1, Level = EventLevel.Informational)]
		[PerformanceCounter(2087567373, "CurrentlyRunningTransactions", CounterModifier.Increment, CounterType.NumberOfItems)]
		[Logging]
		[Support]
		void FireActivityStartedEvent(string ai);

		// Token: 0x0600080B RID: 2059
		[Event(4484404129487526552L, 2, Level = EventLevel.Verbose)]
		[PerformanceCounter(2087567373, "CurrentlyRunningTransactions", CounterModifier.Decrement, CounterType.NumberOfItems)]
		void FireActivityCompletedEvent(long duration, string ai);

		// Token: 0x0600080C RID: 2060
		[Event(5071707678334261657L, 3, Level = EventLevel.Informational)]
		[ActivityMonitoring]
		[Reporting]
		[Logging]
		[Support]
		void FireActivityCompletedSuccessfullyEvent(long duration, string ai);

		// Token: 0x0600080D RID: 2061
		[Event(5464362666731113465L, 4, Level = EventLevel.Warning)]
		[ActivityMonitoring]
		[Reporting]
		[Logging]
		[Support]
		void FireActivityCompletedSuccessfullyDespiteFailureEvent(long duration, [AnalysisPayload(0)] IMonitoredError err, long rootcauseErrorEventId, string ai);

		// Token: 0x0600080E RID: 2062
		[Event(2187010839073174704L, 5, Level = EventLevel.Error, Format = "Failure of activity due to: {1}  [Root-cause correlation: EventId = {2}]")]
		[ActivityMonitoring]
		[FilteredWindowsEventLog(2000)]
		[Reporting]
		[Logging]
		[Support]
		void FireActivityCompletedWithFailureEvent(long duration, [AnalysisPayload(0)] IMonitoredError err, long rootcauseErrorEventId, string ai);

		// Token: 0x0600080F RID: 2063
		[Event(5867609055991075014L, 6, Level = EventLevel.Warning)]
		[ActivityMonitoring]
		[Reporting]
		[Support]
		void FireActivityCompletedWithRemoteFailureEvent(long duration, [AnalysisPayload(0)] IMonitoredError err, long rootcauseErrorEventId, string ai);

		// Token: 0x06000810 RID: 2064
		[Event(6994294237486268222L, 7, Level = EventLevel.Informational)]
		[ActivityMonitoring]
		[Logging]
		[Reporting]
		[Support]
		void FireActivityCompletionSummaryEvent(Guid parentActivityId, ActivityEndedWith howEnded, long duration, [AnalysisPayload(0)] IMonitoredError err, long rootcauseErrorEventId, string ai);
	}
}
