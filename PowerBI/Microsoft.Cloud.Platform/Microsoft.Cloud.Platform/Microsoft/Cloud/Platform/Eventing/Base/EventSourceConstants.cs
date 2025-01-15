using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003C6 RID: 966
	public static class EventSourceConstants
	{
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x00071518 File Offset: 0x0006F718
		public static IEnumerable<string> CommonEventSourcePayloadNames
		{
			get
			{
				return EventSourceConstants.s_commonEventSourcePayloadNames;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x0007151F File Offset: 0x0006F71F
		public static int CommonEventSourcePayloadNamesCount
		{
			get
			{
				return EventSourceConstants.s_commonEventSourcePayloadNames.Length;
			}
		}

		// Token: 0x04000A34 RID: 2612
		public const string EventsKitIdPayloadName = "generatedEventsKitId";

		// Token: 0x04000A35 RID: 2613
		public const string ActivityIdPayloadName = "generatedActivityId";

		// Token: 0x04000A36 RID: 2614
		public const string RootActivityIdPayloadName = "generatedRootActivityId";

		// Token: 0x04000A37 RID: 2615
		public const string ActivityTypePayloadName = "generatedActivityType";

		// Token: 0x04000A38 RID: 2616
		public const string ClientActivityIdPayloadName = "generatedClientActivityId";

		// Token: 0x04000A39 RID: 2617
		public const string ElementIdPayloadName = "generatedElementId";

		// Token: 0x04000A3A RID: 2618
		public const int ElementIdPayloadOffset = 0;

		// Token: 0x04000A3B RID: 2619
		public const int ActivityIdPayloadOffset = 1;

		// Token: 0x04000A3C RID: 2620
		public const int ActivityTypePayloadOffset = 2;

		// Token: 0x04000A3D RID: 2621
		public const int RootActivityIdPayloadOffset = 3;

		// Token: 0x04000A3E RID: 2622
		public const int ClientActivityIdPayloadOffset = 4;

		// Token: 0x04000A3F RID: 2623
		public const int EventsKitIdPayloadOffset = 5;

		// Token: 0x04000A40 RID: 2624
		private static readonly string[] s_commonEventSourcePayloadNames = new string[] { "generatedElementId", "generatedActivityId", "generatedActivityType", "generatedRootActivityId", "generatedClientActivityId", "generatedEventsKitId" };
	}
}
