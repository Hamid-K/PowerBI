using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003C0 RID: 960
	[CLSCompliant(false)]
	public class UnparsableEtwEvent : EtwEvent
	{
		// Token: 0x06001DD6 RID: 7638 RVA: 0x000712DC File Offset: 0x0006F4DC
		public UnparsableEtwEvent(TraceEvent traceEvent)
			: base(-1L, "unknown", traceEvent.ProviderName, traceEvent.ToString(), Activity.Empty, traceEvent.TimeStamp.ToUniversalTime(), traceEvent.Level.ToTraceVerbosity(), ElementId.Any, traceEvent.ProcessID, traceEvent.ThreadID, UnparsableEtwEvent.s_emptyEventParameters)
		{
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0000E568 File Offset: 0x0000C768
		protected override bool ShouldSerializeToStream()
		{
			return false;
		}

		// Token: 0x04000A23 RID: 2595
		private const long c_unknownEventId = -1L;

		// Token: 0x04000A24 RID: 2596
		private const string c_unknownEventName = "unknown";

		// Token: 0x04000A25 RID: 2597
		private static IEnumerable<EventParameter> s_emptyEventParameters = Enumerable.Empty<EventParameter>();
	}
}
