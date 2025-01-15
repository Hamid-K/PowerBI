using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003C1 RID: 961
	public class IllFormattedJsonEvent : EtwEvent
	{
		// Token: 0x06001DD9 RID: 7641 RVA: 0x00071344 File Offset: 0x0006F544
		public IllFormattedJsonEvent(long eventId, string eventName, string message)
			: base(eventId, eventName, "IllFormattedJsonEvent", message, Activity.Empty, DateTime.MinValue, EventLevel.Inherit, ElementId.Any, 0, 0, IllFormattedJsonEvent.s_emptyEventParameters)
		{
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0000E568 File Offset: 0x0000C768
		protected override bool ShouldSerializeToStream()
		{
			return false;
		}

		// Token: 0x04000A26 RID: 2598
		private const string c_source = "IllFormattedJsonEvent";

		// Token: 0x04000A27 RID: 2599
		private const int c_unknownProcessId = 0;

		// Token: 0x04000A28 RID: 2600
		private const int c_unknownThreadId = 0;

		// Token: 0x04000A29 RID: 2601
		private static IEnumerable<EventParameter> s_emptyEventParameters = Enumerable.Empty<EventParameter>();
	}
}
