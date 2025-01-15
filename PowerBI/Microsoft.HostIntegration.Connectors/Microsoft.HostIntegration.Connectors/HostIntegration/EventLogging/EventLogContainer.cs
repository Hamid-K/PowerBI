using System;
using System.Diagnostics;

namespace Microsoft.HostIntegration.EventLogging
{
	// Token: 0x02000775 RID: 1909
	public abstract class EventLogContainer
	{
		// Token: 0x06003DC7 RID: 15815 RVA: 0x000CFCE0 File Offset: 0x000CDEE0
		protected EventLogContainer(string source)
		{
			object obj = EventLogContainer.lockObject;
			lock (obj)
			{
				try
				{
					this.eventLog = new EventLog();
					this.eventLog.Source = source;
				}
				catch (Exception)
				{
					this.eventLog = null;
				}
			}
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x000CFD4C File Offset: 0x000CDF4C
		public void WriteEvent(string entry, EventLogEntryType eventType)
		{
			object obj = EventLogContainer.lockObject;
			lock (obj)
			{
				if (this.eventLog != null)
				{
					try
					{
						this.eventLog.WriteEntry(entry, eventType);
					}
					catch (Exception)
					{
						this.eventLog = null;
					}
				}
			}
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x000CFDB4 File Offset: 0x000CDFB4
		public void WriteEvent(string entry, EventLogEntryType eventType, int eventId)
		{
			object obj = EventLogContainer.lockObject;
			lock (obj)
			{
				if (this.eventLog != null)
				{
					try
					{
						this.eventLog.WriteEntry(entry, eventType, eventId);
					}
					catch (Exception)
					{
						this.eventLog = null;
					}
				}
			}
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x000CFE1C File Offset: 0x000CE01C
		public void WriteEvent(string entry, EventLogEntryType eventType, int eventId, short category)
		{
			object obj = EventLogContainer.lockObject;
			lock (obj)
			{
				if (this.eventLog != null)
				{
					try
					{
						this.eventLog.WriteEntry(entry, eventType, eventId, category);
					}
					catch (Exception)
					{
						this.eventLog = null;
					}
				}
			}
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x000CFE88 File Offset: 0x000CE088
		public void WriteEvent(string entry, EventLogEntryType eventType, int eventId, short category, byte[] rawData)
		{
			object obj = EventLogContainer.lockObject;
			lock (obj)
			{
				if (this.eventLog != null)
				{
					try
					{
						this.eventLog.WriteEntry(entry, eventType, eventId, category, rawData);
					}
					catch (Exception)
					{
						this.eventLog = null;
					}
				}
			}
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x000CFEF4 File Offset: 0x000CE0F4
		protected static void Install(string source)
		{
			if (!EventLog.SourceExists(source))
			{
				EventLog.CreateEventSource(source, "Application");
			}
		}

		// Token: 0x04002495 RID: 9365
		private static object lockObject = new object();

		// Token: 0x04002496 RID: 9366
		private EventLog eventLog;
	}
}
