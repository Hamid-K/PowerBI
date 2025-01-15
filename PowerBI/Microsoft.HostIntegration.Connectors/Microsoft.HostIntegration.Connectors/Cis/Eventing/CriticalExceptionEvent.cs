using System;
using System.Diagnostics;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x02000484 RID: 1156
	[RDEvent(64261, TraceEventType.Critical)]
	public class CriticalExceptionEvent : ExceptionEventBase
	{
		// Token: 0x06002824 RID: 10276 RVA: 0x00079174 File Offset: 0x00077374
		private CriticalExceptionEvent(Exception exception, int exceptionID, string context, bool includeFileInfo)
			: base(exception, exceptionID, context, includeFileInfo)
		{
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x00079181 File Offset: 0x00077381
		public static void Trace(TraceSource traceSource, Exception exception)
		{
			CriticalExceptionEvent.Trace(traceSource, exception, 0, string.Empty, true);
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x00079191 File Offset: 0x00077391
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID)
		{
			CriticalExceptionEvent.Trace(traceSource, exception, exceptionID, string.Empty, true);
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x000791A1 File Offset: 0x000773A1
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string context)
		{
			CriticalExceptionEvent.Trace(traceSource, exception, exceptionID, context, true);
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x000791AD File Offset: 0x000773AD
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string format, params object[] args)
		{
			CriticalExceptionEvent.Trace(traceSource, exception, exceptionID, string.Format(format, args), true);
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x000791C0 File Offset: 0x000773C0
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string context, bool includeFileInfo)
		{
			CriticalExceptionEvent criticalExceptionEvent = new CriticalExceptionEvent(exception, exceptionID, context, includeFileInfo);
			criticalExceptionEvent.TraceTo(traceSource);
		}
	}
}
