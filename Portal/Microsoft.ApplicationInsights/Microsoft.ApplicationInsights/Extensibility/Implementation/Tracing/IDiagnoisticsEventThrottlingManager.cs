using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x0200009F RID: 159
	internal interface IDiagnoisticsEventThrottlingManager
	{
		// Token: 0x060004F1 RID: 1265
		bool ThrottleEvent(int eventId, long keywords);
	}
}
