using System;
using System.Diagnostics;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x0200000C RID: 12
	public interface ILogger
	{
		// Token: 0x06000039 RID: 57
		void Trace(TraceLevel traceLevel, string message);

		// Token: 0x0600003A RID: 58
		void Trace(TraceLevel traceLevel, Func<string> fnMessage);
	}
}
