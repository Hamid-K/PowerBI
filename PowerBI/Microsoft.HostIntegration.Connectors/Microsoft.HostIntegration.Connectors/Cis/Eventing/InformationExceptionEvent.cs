using System;
using System.Diagnostics;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x02000488 RID: 1160
	[RDEvent(64264, TraceEventType.Information)]
	public class InformationExceptionEvent : ExceptionEventBase
	{
		// Token: 0x06002857 RID: 10327 RVA: 0x00079174 File Offset: 0x00077374
		private InformationExceptionEvent(Exception exception, int exceptionID, string context, bool includeFileInfo)
			: base(exception, exceptionID, context, includeFileInfo)
		{
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x00079A97 File Offset: 0x00077C97
		public static void Trace(TraceSource traceSource, Exception exception)
		{
			InformationExceptionEvent.Trace(traceSource, exception, 0, string.Empty, true);
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x00079AA7 File Offset: 0x00077CA7
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID)
		{
			InformationExceptionEvent.Trace(traceSource, exception, exceptionID, string.Empty, true);
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x00079AB7 File Offset: 0x00077CB7
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string context)
		{
			InformationExceptionEvent.Trace(traceSource, exception, exceptionID, context, true);
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x00079AC3 File Offset: 0x00077CC3
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string format, params object[] args)
		{
			InformationExceptionEvent.Trace(traceSource, exception, exceptionID, string.Format(format, args), true);
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x00079AD8 File Offset: 0x00077CD8
		public static void Trace(TraceSource traceSource, Exception exception, int exceptionID, string context, bool includeFileInfo)
		{
			InformationExceptionEvent informationExceptionEvent = new InformationExceptionEvent(exception, exceptionID, context, includeFileInfo);
			informationExceptionEvent.TraceTo(traceSource);
		}
	}
}
