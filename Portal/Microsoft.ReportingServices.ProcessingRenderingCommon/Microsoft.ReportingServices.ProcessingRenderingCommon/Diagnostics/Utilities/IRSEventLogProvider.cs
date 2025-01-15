using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B9 RID: 185
	internal interface IRSEventLogProvider
	{
		// Token: 0x06000613 RID: 1555
		string GetEventSourceName(RunningApplication application);

		// Token: 0x06000614 RID: 1556
		void WriteLog(string sourceName, EventLogEntryType type, Event evt, RSEventLog.Category category, params object[] args);
	}
}
