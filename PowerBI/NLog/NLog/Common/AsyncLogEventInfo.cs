using System;

namespace NLog.Common
{
	// Token: 0x020001BA RID: 442
	public struct AsyncLogEventInfo
	{
		// Token: 0x06001383 RID: 4995 RVA: 0x000353C8 File Offset: 0x000335C8
		public AsyncLogEventInfo(LogEventInfo logEvent, AsyncContinuation continuation)
		{
			this.LogEvent = logEvent;
			this.Continuation = continuation;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x000353D8 File Offset: 0x000335D8
		public LogEventInfo LogEvent { get; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x000353E0 File Offset: 0x000335E0
		public AsyncContinuation Continuation { get; }

		// Token: 0x06001386 RID: 4998 RVA: 0x000353E8 File Offset: 0x000335E8
		public static bool operator ==(AsyncLogEventInfo eventInfo1, AsyncLogEventInfo eventInfo2)
		{
			return eventInfo1.Continuation == eventInfo2.Continuation && eventInfo1.LogEvent == eventInfo2.LogEvent;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0003540C File Offset: 0x0003360C
		public static bool operator !=(AsyncLogEventInfo eventInfo1, AsyncLogEventInfo eventInfo2)
		{
			return eventInfo1.Continuation != eventInfo2.Continuation || eventInfo1.LogEvent != eventInfo2.LogEvent;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00035434 File Offset: 0x00033634
		public override bool Equals(object obj)
		{
			AsyncLogEventInfo asyncLogEventInfo = (AsyncLogEventInfo)obj;
			return this == asyncLogEventInfo;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00035454 File Offset: 0x00033654
		public override int GetHashCode()
		{
			return this.LogEvent.GetHashCode() ^ this.Continuation.GetHashCode();
		}
	}
}
