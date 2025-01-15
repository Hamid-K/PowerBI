using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000BA RID: 186
	internal sealed class NativeRSEventLogProvider : IRSEventLogProvider
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x00011C44 File Offset: 0x0000FE44
		public string GetEventSourceName(RunningApplication application)
		{
			string text;
			switch (application)
			{
			case RunningApplication.WindowsService:
				text = this.AddInstanceToSourceName("Report Server Windows Service ({0})");
				break;
			case RunningApplication.WebService:
				text = this.AddInstanceToSourceName("Report Server ({0})");
				break;
			case RunningApplication.ReportManager:
				text = this.AddInstanceToSourceName("Report Manager ({0})");
				break;
			default:
				text = null;
				break;
			}
			return text;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00011C94 File Offset: 0x0000FE94
		public void WriteLog(string sourceName, EventLogEntryType type, Event evt, RSEventLog.Category category, params object[] args)
		{
			EventInstance instance = new EventInstance((long)evt, (int)category, type);
			RevertImpersonationContext.RunFromRestrictedCasContext(delegate
			{
				try
				{
					new EventLogPermission(EventLogPermissionAccess.Write, ".").Assert();
					EventLog.WriteEvent(sourceName, instance, args);
				}
				catch (Exception)
				{
				}
			});
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00011CCA File Offset: 0x0000FECA
		private string AddInstanceToSourceName(string sourceFormat)
		{
			return string.Format(CultureInfo.InvariantCulture, sourceFormat, ProcessingContext.Configuration.InstanceName);
		}

		// Token: 0x04000340 RID: 832
		private const string _ReportManagerSource = "Report Manager ({0})";

		// Token: 0x04000341 RID: 833
		private const string _ReportServerSourceFormat = "Report Server ({0})";

		// Token: 0x04000342 RID: 834
		private const string _WindowsServiceSourceFormat = "Report Server Windows Service ({0})";
	}
}
