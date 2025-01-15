using System;
using System.Diagnostics;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000358 RID: 856
	[Trace(typeof(EventingTrace))]
	[EventsKit(4263091980201196060L)]
	[PublishedEvent]
	public interface IEtwSessionsManagerEventsKit
	{
		// Token: 0x06001961 RID: 6497
		[WindowsEventLog(EventLogEntryType.Error, 8100)]
		[Event(5092838154480983671L, 1, Level = EventLevel.Error)]
		void NotifyEventSourceOperationTimeout(string operationName, string eventSourceName, Guid provider, string sessionName, MonitoredException ex);

		// Token: 0x06001962 RID: 6498
		[WindowsEventLog(EventLogEntryType.Error, 8101)]
		[Event(4052507777771462140L, 2, Level = EventLevel.Error)]
		void NotifyEventSourceException(string operationName, string eventSourceName, Guid provider, string sessionName, MonitoredException ex);

		// Token: 0x06001963 RID: 6499
		[WindowsEventLog(EventLogEntryType.Error, 8102)]
		[Event(7999294443256301660L, 3, Level = EventLevel.Error)]
		void NotifyFailedWritingEvent(string eventName, string exceptionType, string exceptionMessage, int failuresCount);
	}
}
