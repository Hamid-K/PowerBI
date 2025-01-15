using System;
using System.Diagnostics;

namespace Microsoft.Lucia.Diagnostics
{
	// Token: 0x0200003C RID: 60
	public static class TracingProviderExtensions
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00003CEC File Offset: 0x00001EEC
		[return: Nullable]
		public static LevelTracer ForLevel(this ITracingProvider tracingProvider, TraceLevel traceLevel)
		{
			switch (traceLevel)
			{
			case TraceLevel.Error:
				return tracingProvider.Error;
			case TraceLevel.Warning:
				return tracingProvider.Warning;
			case TraceLevel.Info:
				return tracingProvider.Info;
			case TraceLevel.Verbose:
				return tracingProvider.Verbose;
			default:
				throw new ArgumentOutOfRangeException("traceLevel");
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003D39 File Offset: 0x00001F39
		public static void Trace(this LevelTracer tracer, string message)
		{
			tracer.TraceStringCore(message);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003D42 File Offset: 0x00001F42
		public static void SanitizedTrace(this LevelTracer tracer, string message)
		{
			tracer.SanitizedTraceStringCore(message);
		}
	}
}
