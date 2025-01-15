using System;
using System.Globalization;
using System.Text;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000372 RID: 882
	public class WindowsEventLogMessageFormatter
	{
		// Token: 0x06001A2B RID: 6699 RVA: 0x00060A04 File Offset: 0x0005EC04
		public static string Format(WireEventBase publishedEvent, string friendlyName)
		{
			EventsKitIdentifiers eventsKitIdentifiers = new EventsKitIdentifiers(publishedEvent.Id.EventId);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, WindowsEventLogMessageFormatter.c_windowsEventLogBaseFormat, new object[]
			{
				publishedEvent.ToMonitoringString(),
				publishedEvent.Activity.ActivityType,
				publishedEvent.Activity.ActivityId,
				publishedEvent.Activity.RootActivityId,
				publishedEvent.Activity.ClientActivityId,
				publishedEvent.Id.ElementId,
				friendlyName,
				eventsKitIdentifiers.EventId,
				eventsKitIdentifiers.EventsKitId,
				publishedEvent.UtcTimestamp.ToUniversalTime()
			});
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00060AE0 File Offset: 0x0005ECE0
		public static string Format(WireEventBase publishedEvent, string friendlyName, string exceptionString)
		{
			StringBuilder stringBuilder = new StringBuilder(WindowsEventLogMessageFormatter.Format(publishedEvent, friendlyName));
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, WindowsEventLogMessageFormatter.c_windowsEventLogExceptionFormat, new object[] { exceptionString });
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00060B24 File Offset: 0x0005ED24
		public static string Format(WireEventBase publishedEvent, string friendlyName, string exceptionString, ErrorCorrelationId errorCorrelationId)
		{
			StringBuilder stringBuilder = new StringBuilder(WindowsEventLogMessageFormatter.Format(publishedEvent, friendlyName));
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, WindowsEventLogMessageFormatter.c_windowsEventLogCorrelationFormat, new object[] { errorCorrelationId.CorrelationId, errorCorrelationId.SequenceNumber, exceptionString });
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		// Token: 0x0400090A RID: 2314
		private static string c_windowsEventLogBaseFormat = "{0}\r\n    \r\nAdditional event information:\r\nActivity Short Name={1}\r\nActivity Identifier={2}\r\nRoot Activity Identifier={3}\r\nClient Activity Identifier={4}\r\nElement Identifier={5}\r\nEvent Name={6}\r\nEvent Identifier={7}\r\nEvent Kit Identifier={8}\r\nTimestamp={9:u}".Replace("\n\r", Environment.NewLine);

		// Token: 0x0400090B RID: 2315
		private static string c_windowsEventLogCorrelationFormat = "Error Correlation Id={0}\r\nError Correlation Sequence Number={1}\r\nException={2}".Replace("\n\r", Environment.NewLine);

		// Token: 0x0400090C RID: 2316
		private static string c_windowsEventLogExceptionFormat = "Exception={0}";
	}
}
