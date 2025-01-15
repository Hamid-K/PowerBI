using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x0200009E RID: 158
	internal interface IDiagnoisticsEventThrottling
	{
		// Token: 0x060004EF RID: 1263
		bool ThrottleEvent(int eventId, long keywords, out bool justExceededThreshold);

		// Token: 0x060004F0 RID: 1264
		IDictionary<int, DiagnoisticsEventCounters> CollectSnapshot();
	}
}
