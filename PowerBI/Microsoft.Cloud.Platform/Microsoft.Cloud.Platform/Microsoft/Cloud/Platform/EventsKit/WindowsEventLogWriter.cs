using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000371 RID: 881
	public class WindowsEventLogWriter
	{
		// Token: 0x06001A28 RID: 6696 RVA: 0x0006096A File Offset: 0x0005EB6A
		public WindowsEventLogWriter(string eventLogSourceName)
		{
			this.m_eventLogSourceName = eventLogSourceName;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0006097C File Offset: 0x0005EB7C
		public void WriteEntry(string message, EventLogEntryType severity, int eventId)
		{
			try
			{
				EventLog.WriteEntry(this.m_eventLogSourceName, ExtendedText.ChopTextByLength(message, 31855), severity, eventId);
			}
			catch (InvalidOperationException ex)
			{
				throw new WindowsEventLogException(ex.Message, null, ex);
			}
			catch (Win32Exception ex2)
			{
				throw new WindowsEventLogException(ex2.Message, null, ex2);
			}
			catch (SecurityException ex3)
			{
				throw new WindowsEventLogException(ex3.Message, null, ex3);
			}
		}

		// Token: 0x04000907 RID: 2311
		public static readonly string LogName = "Application";

		// Token: 0x04000908 RID: 2312
		private const int c_maxMessageLength = 31855;

		// Token: 0x04000909 RID: 2313
		private readonly string m_eventLogSourceName;
	}
}
