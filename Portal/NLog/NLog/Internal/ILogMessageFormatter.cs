using System;
using System.Text;

namespace NLog.Internal
{
	// Token: 0x0200011F RID: 287
	internal interface ILogMessageFormatter
	{
		// Token: 0x06000ECD RID: 3789
		string FormatMessage(LogEventInfo logEvent);

		// Token: 0x06000ECE RID: 3790
		bool HasProperties(LogEventInfo logEvent);

		// Token: 0x06000ECF RID: 3791
		void AppendFormattedMessage(LogEventInfo logEvent, StringBuilder builder);
	}
}
