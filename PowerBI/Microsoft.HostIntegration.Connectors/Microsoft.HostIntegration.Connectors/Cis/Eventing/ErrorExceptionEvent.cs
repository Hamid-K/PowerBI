using System;
using System.Diagnostics;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x02000485 RID: 1157
	[RDEvent(64262, TraceEventType.Error)]
	public class ErrorExceptionEvent : ExceptionEventBase
	{
		// Token: 0x0600282A RID: 10282 RVA: 0x00079174 File Offset: 0x00077374
		private ErrorExceptionEvent(Exception exception, int exceptionID, string context, bool includeFileInfo)
			: base(exception, exceptionID, context, includeFileInfo)
		{
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000791DF File Offset: 0x000773DF
		public static void Trace(TraceSource traceSource, Exception exception)
		{
			ErrorExceptionEvent.Trace(traceSource, exception, 0, string.Empty, true);
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000791EF File Offset: 0x000773EF
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID)
		{
			ErrorExceptionEvent.Trace(traceSource, exception, exceptionID, string.Empty, true);
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x000791FF File Offset: 0x000773FF
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string context)
		{
			ErrorExceptionEvent.Trace(traceSource, exception, exceptionID, context, true);
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x0007920B File Offset: 0x0007740B
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string format, params object[] args)
		{
			ErrorExceptionEvent.Trace(traceSource, exception, exceptionID, string.Format(format, args), true);
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x00079220 File Offset: 0x00077420
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string context, bool includeFileInfo)
		{
			ErrorExceptionEvent errorExceptionEvent = new ErrorExceptionEvent(exception, exceptionID, context, includeFileInfo);
			errorExceptionEvent.TraceTo(traceSource);
		}
	}
}
