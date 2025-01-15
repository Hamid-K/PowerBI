using System;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000135 RID: 309
	public interface IMonitoredActivityCompletionModelFactory
	{
		// Token: 0x06000811 RID: 2065
		MonitoredActivityCompletionModel CreateMonitoredActivityCompletionModel(ActivityType activityType);

		// Token: 0x06000812 RID: 2066
		void CreateSyncActivityAndInvokeWithNewModel(ActivityType activityType, Action action);

		// Token: 0x06000813 RID: 2067
		void CreateSyncActivityAndInvokeWithNewModel(ActivityType activityType, Action action, Predicate<IMonitoredError> shouldActivityEndWithSuccess);

		// Token: 0x06000814 RID: 2068
		void CreateSyncActivityAndInvokeWithNewModel(Activity activity, Action action);

		// Token: 0x06000815 RID: 2069
		void CreateSyncActivityAndInvokeWithNewModel(Activity activity, Action action, Predicate<IMonitoredError> shouldActivityEndWithSuccess);

		// Token: 0x06000816 RID: 2070
		IEventsKitFactory GetEventsKitFactory();
	}
}
