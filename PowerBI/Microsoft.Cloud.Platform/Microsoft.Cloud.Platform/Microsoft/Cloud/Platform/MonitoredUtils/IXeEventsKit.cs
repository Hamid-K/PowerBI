using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000131 RID: 305
	[EventsKit(4917737014806960318L)]
	[PerformanceCountersCategory("Activities")]
	[PublishedEvent]
	[Trace(typeof(MonitoredUtilsTrace))]
	public interface IXeEventsKit
	{
		// Token: 0x06000805 RID: 2053
		[Event(7527287753132401043L, 2, Level = EventLevel.Informational, Format = "EmptyFormat")]
		[ActivityMonitoring]
		[Logging]
		[Reporting]
		[Support]
		void FireXeActivityCompletionSummaryEvent(XeInfo xeInfo, Guid parentActivityId, ActivityEndedWith howEnded, DateTime startTime, long duration, string name, string properties, [AnalysisPayload(0)] IMonitoredError err, long rootcauseErrorEventId);

		// Token: 0x06000806 RID: 2054
		[Event(3030375513894374788L, 4, Level = EventLevel.Error, Format = "EmptyFormat")]
		[ActivityMonitoring]
		[Logging]
		[Reporting]
		[Support]
		void FireXeActivityFailureRootCauseEvent(XeInfo xeInfo, DateTime failureTime, MonitoredException err);

		// Token: 0x06000807 RID: 2055
		[Event(3028741171140197300L, 6, Level = EventLevel.Informational, Format = "EmptyFormat")]
		[Logging]
		[Reporting]
		[Support]
		void FireXeEvent(XeInfo xeInfo, string name, string properties);

		// Token: 0x06000808 RID: 2056
		[Event(6952302906064617795L, 7, Level = EventLevel.Error, Format = "EmptyFormat")]
		[ActivityMonitoring]
		[Logging]
		[Reporting]
		[Support]
		void FireXeActivityUserErrorRootCauseEvent(XeInfo xeInfo, DateTime failureTime, MonitoredException err);

		// Token: 0x06000809 RID: 2057
		[Event(1610262494167228559L, 8, Level = EventLevel.Informational, Format = "EmptyFormat")]
		[ActivityMonitoring]
		[Logging]
		[Reporting]
		[Support]
		void FireXeActivityUserCancellationRootCauseEvent(XeInfo xeInfo, DateTime failureTime, MonitoredException err);
	}
}
