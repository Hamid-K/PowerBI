using System;
using System.Diagnostics;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000330 RID: 816
	[Trace(typeof(EventingTrace))]
	[EventsKit(3109928834459394064L)]
	[PublishedEvent]
	public interface IEventFilesCollectionEventsKit
	{
		// Token: 0x060017FD RID: 6141
		[WindowsEventLog(EventLogEntryType.Error, 8300)]
		[Event(6301566715087065401L, 1, Level = EventLevel.Error, Version = 1)]
		void NotifyEventFilesCollectionFailed(string filesToMove, MonitoredException exception);

		// Token: 0x060017FE RID: 6142
		[FilteredWindowsEventLog(8302)]
		[Event(7747344651403954344L, 2, Level = EventLevel.Error, Version = 1)]
		void NotifyFailedToCompressEventFile(string file, MonitoredException exception);

		// Token: 0x060017FF RID: 6143
		[FilteredWindowsEventLog(8303)]
		[Event(2965132040698877587L, 3, Level = EventLevel.Error, Version = 1)]
		void NotifyFailedToDeleteSourceFileAfterCompression(string fileToDelete, MonitoredException exception);

		// Token: 0x06001800 RID: 6144
		[WindowsEventLog(EventLogEntryType.Error, 8301)]
		[Event(3172130530083372255L, 4, Level = EventLevel.Error, Version = 1)]
		void NotifyEventFilesDeletionFailed(string fileToDelete, int directorySizeInMb, int directorySizeLimitInMb, MonitoredException exception);

		// Token: 0x06001801 RID: 6145
		[WindowsEventLog(EventLogEntryType.Error, 8304)]
		[Event(3562900168931917041L, 5, Level = EventLevel.Error, Version = 1)]
		void NotifyTempDirectoryAndTargetDirectoryNotOnSameDrive(string tempDriveLetter, string targetDriveLetter, MonitoredException exception);

		// Token: 0x06001802 RID: 6146
		[WindowsEventLog(EventLogEntryType.Error, 8305)]
		[Event(5719280906109245493L, 6, Level = EventLevel.Error, Version = 1)]
		void NotifyExhaustedCollectFileMaximumRetries(string fileToCollect, int retriesAttempted, MonitoredException exception);
	}
}
