using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003C3 RID: 963
	[CLSCompliant(false)]
	public class ExceedSizeLimitJsonEvent : EtwEvent
	{
		// Token: 0x06001DE1 RID: 7649 RVA: 0x0007141C File Offset: 0x0006F61C
		public ExceedSizeLimitJsonEvent(EtwEvent etwEvent)
			: base(-1L, etwEvent.EventName, etwEvent.Source, "Failed to serialize the json string as a valid event. The entire serialized string must fit within 32K characters. JSON=" + etwEvent.ToJsonString().Substring(0, 2000), etwEvent.Activity, etwEvent.Timestamp, etwEvent.Level, etwEvent.ElementId, etwEvent.ProcessId, etwEvent.ThreadId, ExceedSizeLimitJsonEvent.s_emptyEventParameters)
		{
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x00071484 File Offset: 0x0006F684
		public ExceedSizeLimitJsonEvent(long eventId, TraceEvent traceEvent, string jsonEvent)
			: base(eventId, traceEvent.EventName, traceEvent.ProviderName, "Failed to serialize the json string as a valid event. The entire serialized string must fit within 32K characters. JSON=" + jsonEvent.Substring(0, 2000), Activity.Empty, traceEvent.TimeStamp.ToUniversalTime(), EventLevel.Inherit, ElementId.Any, traceEvent.ProcessID, traceEvent.ThreadID, ExceedSizeLimitJsonEvent.s_emptyEventParameters)
		{
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x0000E568 File Offset: 0x0000C768
		protected override bool ShouldSerializeToStream()
		{
			return false;
		}

		// Token: 0x04000A30 RID: 2608
		private const long c_unknownEventId = -1L;

		// Token: 0x04000A31 RID: 2609
		private const int c_eventPartialStringLength = 2000;

		// Token: 0x04000A32 RID: 2610
		private static IEnumerable<EventParameter> s_emptyEventParameters = Enumerable.Empty<EventParameter>();

		// Token: 0x04000A33 RID: 2611
		private const string JSONEVENT_EXCEEDSIZELIMIT_ERROR = "Failed to serialize the json string as a valid event. The entire serialized string must fit within 32K characters. JSON=";
	}
}
