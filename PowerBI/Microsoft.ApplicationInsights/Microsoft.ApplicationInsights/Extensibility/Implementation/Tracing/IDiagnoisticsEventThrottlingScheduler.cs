using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000A0 RID: 160
	internal interface IDiagnoisticsEventThrottlingScheduler
	{
		// Token: 0x060004F2 RID: 1266
		object ScheduleToRunEveryTimeIntervalInMilliseconds(int interval, Action actionToExecute);

		// Token: 0x060004F3 RID: 1267
		void RemoveScheduledRoutine(object token);
	}
}
