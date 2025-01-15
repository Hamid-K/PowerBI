using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000015 RID: 21
	public interface IOAuthTracingService
	{
		// Token: 0x06000089 RID: 137
		void WriteTrace(string traceName, TraceEventType severityLevel, Dictionary<string, object> traceValues, bool isPii);
	}
}
