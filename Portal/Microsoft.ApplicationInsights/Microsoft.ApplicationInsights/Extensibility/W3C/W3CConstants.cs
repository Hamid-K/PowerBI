using System;

namespace Microsoft.ApplicationInsights.Extensibility.W3C
{
	// Token: 0x02000062 RID: 98
	internal static class W3CConstants
	{
		// Token: 0x04000144 RID: 324
		internal const string TraceIdTag = "w3c_traceId";

		// Token: 0x04000145 RID: 325
		internal const string SpanIdTag = "w3c_spanId";

		// Token: 0x04000146 RID: 326
		internal const string ParentSpanIdTag = "w3c_parentSpanId";

		// Token: 0x04000147 RID: 327
		internal const string VersionTag = "w3c_version";

		// Token: 0x04000148 RID: 328
		internal const string SampledTag = "w3c_sampled";

		// Token: 0x04000149 RID: 329
		internal const string TracestateTag = "w3c_tracestate";

		// Token: 0x0400014A RID: 330
		internal const string DefaultVersion = "00";

		// Token: 0x0400014B RID: 331
		internal const string TraceFlagRecordedAndNotRequested = "02";

		// Token: 0x0400014C RID: 332
		internal const string TraceFlagRecordedAndRequested = "03";

		// Token: 0x0400014D RID: 333
		internal const byte RequestedTraceFlag = 1;

		// Token: 0x0400014E RID: 334
		internal const string LegacyRootIdProperty = "ai_legacyRootId";

		// Token: 0x0400014F RID: 335
		internal const string LegacyRequestIdProperty = "ai_legacyRequestId";
	}
}
