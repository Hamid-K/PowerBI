using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x0200009C RID: 156
	internal static class EventSourceKeywords
	{
		// Token: 0x040001F5 RID: 501
		public const long UserActionable = 1L;

		// Token: 0x040001F6 RID: 502
		public const long Diagnostics = 2L;

		// Token: 0x040001F7 RID: 503
		public const long VerboseFailure = 4L;

		// Token: 0x040001F8 RID: 504
		public const long ErrorFailure = 8L;

		// Token: 0x040001F9 RID: 505
		public const long ReservedUserKeywordBegin = 16L;
	}
}
