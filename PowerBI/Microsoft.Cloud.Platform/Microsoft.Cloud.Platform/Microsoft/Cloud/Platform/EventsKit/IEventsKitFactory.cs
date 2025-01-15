using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200034E RID: 846
	public interface IEventsKitFactory
	{
		// Token: 0x06001919 RID: 6425
		T CreateEventsKit<T>() where T : class;

		// Token: 0x0600191A RID: 6426
		T CreateEventsKit<T>(ActivityType activityType) where T : class;

		// Token: 0x0600191B RID: 6427
		T CreateEventsKit<T>(string performanceCountersInstanceName, PerformanceCounterPrefixSetting performanceCounterPrefixSetting) where T : class;
	}
}
