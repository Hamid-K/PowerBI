using System;
using System.Diagnostics;

namespace Microsoft.Owin.Logging
{
	// Token: 0x0200002C RID: 44
	public interface ILogger
	{
		// Token: 0x060001DF RID: 479
		bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter);
	}
}
