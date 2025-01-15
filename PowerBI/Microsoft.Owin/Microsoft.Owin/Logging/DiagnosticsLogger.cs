using System;
using System.Diagnostics;

namespace Microsoft.Owin.Logging
{
	// Token: 0x0200002A RID: 42
	internal class DiagnosticsLogger : ILogger
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00004C1A File Offset: 0x00002E1A
		public DiagnosticsLogger(TraceSource traceSource)
		{
			this._traceSource = traceSource;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00004C29 File Offset: 0x00002E29
		public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
		{
			if (!this._traceSource.Switch.ShouldTrace(eventType))
			{
				return false;
			}
			if (formatter != null)
			{
				this._traceSource.TraceEvent(eventType, eventId, formatter(state, exception));
			}
			return true;
		}

		// Token: 0x04000059 RID: 89
		private readonly TraceSource _traceSource;
	}
}
