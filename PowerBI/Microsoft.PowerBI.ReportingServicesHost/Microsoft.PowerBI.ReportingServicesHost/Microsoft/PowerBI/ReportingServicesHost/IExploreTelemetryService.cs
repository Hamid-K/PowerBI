using System;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000065 RID: 101
	internal interface IExploreTelemetryService
	{
		// Token: 0x06000240 RID: 576
		void RunInActivity(string activityName, Action<ExploreBaseEvent> action);

		// Token: 0x06000241 RID: 577
		T RunInActivity<T>(string activityName, Func<ExploreBaseEvent, T> action);
	}
}
