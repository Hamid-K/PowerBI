using System;
using System.Diagnostics;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020004F3 RID: 1267
	public class CommonHISEventLogging
	{
		// Token: 0x06002B07 RID: 11015 RVA: 0x00094291 File Offset: 0x00092491
		static CommonHISEventLogging()
		{
			CommonHISEventLogging.objEventLog.Source = "HIS Application Integration";
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000942AC File Offset: 0x000924AC
		public void WriteEvent(string entry, EventLogEntryType eventType)
		{
			CommonHISEventLogging.objEventLog.WriteEntry(entry, eventType);
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000942BA File Offset: 0x000924BA
		public void WriteEvent(string entry, EventLogEntryType eventType, int EventId)
		{
			CommonHISEventLogging.objEventLog.WriteEntry(entry, eventType, EventId);
		}

		// Token: 0x04001A1D RID: 6685
		private static EventLog objEventLog = new EventLog();

		// Token: 0x04001A1E RID: 6686
		private const string eventApplicationName = "HIS Application Integration";
	}
}
