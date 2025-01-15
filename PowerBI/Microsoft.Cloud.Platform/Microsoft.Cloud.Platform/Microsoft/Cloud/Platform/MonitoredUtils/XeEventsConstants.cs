using System;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200012F RID: 303
	public static class XeEventsConstants
	{
		// Token: 0x040002D7 RID: 727
		public const long EventsKitId = 4917737014806960318L;

		// Token: 0x040002D8 RID: 728
		public const long XeActivityCompletionSummaryEventId = 7527287753132401043L;

		// Token: 0x040002D9 RID: 729
		public const string XeActivityCompletionSummaryEventName = "XeActivityCompletionSummaryEvent";

		// Token: 0x040002DA RID: 730
		public const string XeActivityCompletionSummaryEventFormat = "parentActivityId={0}, howEnded={1}, startTime={2:o}, duration={3}, name={4}, properties={5}, err={6}, rootcauseErrorEventId={7}";

		// Token: 0x040002DB RID: 731
		public const long XeActivityFailureRootCauseEventId = 3030375513894374788L;

		// Token: 0x040002DC RID: 732
		public const long XeActivityUserErrorRootCauseEventId = 6952302906064617795L;

		// Token: 0x040002DD RID: 733
		public const string XeActivityFailureRootCauseEventName = "XeActivityFailureRootCauseEvent";

		// Token: 0x040002DE RID: 734
		public const string XeActivityUserErrorRootCauseEventName = "XeActivityUserErrorRootCauseEvent";

		// Token: 0x040002DF RID: 735
		public const string XeActivityFailureRootCauseEventFormat = "failureTime={0:o}, err={1}";

		// Token: 0x040002E0 RID: 736
		public const long XeEventId = 3028741171140197300L;

		// Token: 0x040002E1 RID: 737
		public const string XeEventName = "XeEvent";

		// Token: 0x040002E2 RID: 738
		public const string XeEventFormat = "name={0}, properties={1}";

		// Token: 0x040002E3 RID: 739
		public const long XeActivityUserCancellationRootCauseEventId = 1610262494167228559L;

		// Token: 0x040002E4 RID: 740
		public const string XeActivityUserCancellationRootCauseEventName = "XeActivityUserCancellationRootCauseEvent";
	}
}
