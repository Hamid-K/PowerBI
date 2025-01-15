using System;
using System.Diagnostics;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x0200049D RID: 1181
	[RDEvent(64265, TraceEventType.Verbose)]
	public class VerboseExceptionEvent : ExceptionEventBase
	{
		// Token: 0x06002900 RID: 10496 RVA: 0x00079174 File Offset: 0x00077374
		private VerboseExceptionEvent(Exception exception, int exceptionID, string context, bool includeFileInfo)
			: base(exception, exceptionID, context, includeFileInfo)
		{
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x0007CE9A File Offset: 0x0007B09A
		public static void Trace(TraceSource traceSource, Exception exception)
		{
			VerboseExceptionEvent.Trace(traceSource, exception, 0, string.Empty, true);
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x0007CEAA File Offset: 0x0007B0AA
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID)
		{
			VerboseExceptionEvent.Trace(traceSource, exception, exceptionID, string.Empty, true);
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x0007CEBA File Offset: 0x0007B0BA
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string context)
		{
			VerboseExceptionEvent.Trace(traceSource, exception, exceptionID, context, true);
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x0007CEC6 File Offset: 0x0007B0C6
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string format, params object[] args)
		{
			VerboseExceptionEvent.Trace(traceSource, exception, exceptionID, string.Format(format, args), true);
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x0007CEDC File Offset: 0x0007B0DC
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string context, bool includeFileInfo)
		{
			VerboseExceptionEvent verboseExceptionEvent = new VerboseExceptionEvent(exception, exceptionID, context, includeFileInfo);
			verboseExceptionEvent.TraceTo(traceSource);
		}
	}
}
