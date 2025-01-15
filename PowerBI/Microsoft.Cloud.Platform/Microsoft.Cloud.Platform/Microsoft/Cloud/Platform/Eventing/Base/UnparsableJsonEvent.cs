using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003C2 RID: 962
	[CLSCompliant(false)]
	public class UnparsableJsonEvent : EtwEvent
	{
		// Token: 0x06001DDC RID: 7644 RVA: 0x00071382 File Offset: 0x0006F582
		public UnparsableJsonEvent(string jsonEvent, Exception ex)
			: this(DateTime.MinValue, 0, 0, jsonEvent, ex)
		{
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x00071394 File Offset: 0x0006F594
		private UnparsableJsonEvent(DateTime dateTime, int processId, int threadId, string jsonEvent, Exception ex)
			: base(-1L, "unknown", "UnparsableJsonEvent", "Failed to parse a json string as a valid event. JSON=" + jsonEvent + "; ex=" + ex.ToString(), Activity.Empty, dateTime, EventLevel.Inherit, ElementId.Any, processId, threadId, UnparsableJsonEvent.s_emptyEventParameters)
		{
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x000713E0 File Offset: 0x0006F5E0
		public static UnparsableJsonEvent CreateFromTraceEvent(TraceEvent traceEvent, string jsonEvent, Exception ex)
		{
			return new UnparsableJsonEvent(traceEvent.TimeStamp.ToUniversalTime(), traceEvent.ProcessID, traceEvent.ThreadID, jsonEvent, ex);
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x0000E568 File Offset: 0x0000C768
		protected override bool ShouldSerializeToStream()
		{
			return false;
		}

		// Token: 0x04000A2A RID: 2602
		private const long c_unknownEventId = -1L;

		// Token: 0x04000A2B RID: 2603
		private const int c_unknownProcessId = 0;

		// Token: 0x04000A2C RID: 2604
		private const int c_unknownThreadId = 0;

		// Token: 0x04000A2D RID: 2605
		private const string c_unknownEventName = "unknown";

		// Token: 0x04000A2E RID: 2606
		private const string c_source = "UnparsableJsonEvent";

		// Token: 0x04000A2F RID: 2607
		private static IEnumerable<EventParameter> s_emptyEventParameters = Enumerable.Empty<EventParameter>();
	}
}
