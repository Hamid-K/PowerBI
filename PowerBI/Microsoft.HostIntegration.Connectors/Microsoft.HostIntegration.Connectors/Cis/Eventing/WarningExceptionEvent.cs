using System;
using System.Diagnostics;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x0200049E RID: 1182
	[RDEvent(64263, TraceEventType.Warning)]
	public class WarningExceptionEvent : ExceptionEventBase
	{
		// Token: 0x06002906 RID: 10502 RVA: 0x00079174 File Offset: 0x00077374
		private WarningExceptionEvent(Exception exception, int exceptionID, string context, bool includeFileInfo)
			: base(exception, exceptionID, context, includeFileInfo)
		{
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x0007CEFB File Offset: 0x0007B0FB
		public static void Trace(TraceSource traceSource, Exception exception)
		{
			WarningExceptionEvent.Trace(traceSource, exception, 0, string.Empty, true);
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x0007CF0B File Offset: 0x0007B10B
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID)
		{
			WarningExceptionEvent.Trace(traceSource, exception, exceptionID, string.Empty, true);
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x0007CF1B File Offset: 0x0007B11B
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string context)
		{
			WarningExceptionEvent.Trace(traceSource, exception, exceptionID, context, true);
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x0007CF27 File Offset: 0x0007B127
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string format, params object[] args)
		{
			WarningExceptionEvent.Trace(traceSource, exception, exceptionID, string.Format(format, args), true);
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x0007CF3C File Offset: 0x0007B13C
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string context, bool includeFileInfo)
		{
			WarningExceptionEvent warningExceptionEvent = new WarningExceptionEvent(exception, exceptionID, context, includeFileInfo);
			warningExceptionEvent.TraceTo(traceSource);
		}
	}
}
