using System;
using System.Diagnostics;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000011 RID: 17
	public sealed class DefaultTraceLogger : ILogger
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003670 File Offset: 0x00001870
		public void Trace(TraceLevel traceLevel, string message)
		{
			switch (traceLevel)
			{
			case TraceLevel.Off:
				return;
			case TraceLevel.Error:
				Logger.Error(message, Array.Empty<object>());
				return;
			case TraceLevel.Warning:
				Logger.Warning(message, Array.Empty<object>());
				return;
			case TraceLevel.Info:
				Logger.Info(message, Array.Empty<object>());
				return;
			}
			Logger.Trace(message, Array.Empty<object>());
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000036C8 File Offset: 0x000018C8
		public void Trace(TraceLevel traceLevel, Func<string> fnMessage)
		{
			switch (traceLevel)
			{
			case TraceLevel.Off:
				return;
			case TraceLevel.Error:
				Logger.Error(fnMessage(), Array.Empty<object>());
				return;
			case TraceLevel.Warning:
				Logger.Warning(fnMessage(), Array.Empty<object>());
				return;
			case TraceLevel.Info:
				Logger.Info(fnMessage(), Array.Empty<object>());
				return;
			}
			Logger.Trace(fnMessage(), Array.Empty<object>());
		}
	}
}
