using System;
using System.Diagnostics;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200022E RID: 558
	[EventsKit(8799175355587644124L, Level = EventLevel.Informational)]
	[PublishedEvent]
	[Trace(typeof(UtilsTrace))]
	public interface IDumperEventsKit
	{
		// Token: 0x06000E92 RID: 3730
		[Obsolete("Use new event version instead")]
		[Event(5625152255017737644L, 1, Level = EventLevel.Error, Format = "Fatal unique dump {0} occured. Error: {1}", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 5811)]
		void NotifyFatalUniqueDumpOccured(string dumpName, MonitoredException mex);

		// Token: 0x06000E93 RID: 3731
		[Event(4369344292731694472L, 2, Level = EventLevel.Error, Format = "Fatal duplicate dump {0} occured. Error: {1}", Version = 1)]
		[FilteredWindowsEventLog(5812)]
		void NotifyFatalDuplicateDumpOccured(string dumpName, MonitoredException mex);

		// Token: 0x06000E94 RID: 3732
		[Obsolete("Use new event version instead")]
		[Event(1822183764549996220L, 3, Level = EventLevel.Error, Format = "Nonfatal unique dump {0} occured. Error: {1}", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 5813)]
		void NotifyNonfatalUniqueDumpOccured(string dumpName, MonitoredException mex);

		// Token: 0x06000E95 RID: 3733
		[Event(3636418203498118875L, 4, Level = EventLevel.Error, Format = "Nonfatal duplicate dump {0} occured. Error: {1}", Version = 1)]
		[FilteredWindowsEventLog(5814)]
		void NotifyNonfatalDuplicateDumpOccured(string dumpName, MonitoredException mex);

		// Token: 0x06000E96 RID: 3734
		[Obsolete("Use new event version instead")]
		[Event(483707365285537556L, 5, Level = EventLevel.Error, Format = "Fatal unique dump {0} occured. DumpUID: {1}. Error: {2}", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 5811)]
		void NotifyFatalUniqueDumpOccured2(string dumpName, string dumpUid, MonitoredException mex);

		// Token: 0x06000E97 RID: 3735
		[Obsolete("Use new event version instead")]
		[Event(1161811441628451546L, 6, Level = EventLevel.Error, Format = "Nonfatal unique dump {0} occured. DumpUID: {1}. Error: {2}", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 5813)]
		void NotifyNonfatalUniqueDumpOccured2(string dumpName, string dumpUid, MonitoredException mex);

		// Token: 0x06000E98 RID: 3736
		[Event(5732058480550043929L, 7, Level = EventLevel.Error, Format = "Fatal unique dump {0} occured. PBIDumpUID: {1}. Error: {2}", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 5811)]
		void NotifyFatalUniqueDumpOccured3(string dumpName, string pbiDumpUid, MonitoredException mex);

		// Token: 0x06000E99 RID: 3737
		[Event(1537909457214452475L, 8, Level = EventLevel.Error, Format = "Nonfatal unique dump {0} occured. PBIDumpUID: {1}. Error: {2}", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 5813)]
		void NotifyNonfatalUniqueDumpOccured3(string dumpName, string pbiDumpUid, MonitoredException mex);
	}
}
