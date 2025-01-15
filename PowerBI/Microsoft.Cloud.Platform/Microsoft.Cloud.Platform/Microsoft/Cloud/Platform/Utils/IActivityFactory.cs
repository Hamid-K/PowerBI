using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000224 RID: 548
	public interface IActivityFactory
	{
		// Token: 0x06000E72 RID: 3698
		AsyncActivity CreateAsyncActivity(ActivityType activityType);

		// Token: 0x06000E73 RID: 3699
		AsyncActivity CreateAsyncActivity(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId);

		// Token: 0x06000E74 RID: 3700
		SyncActivity CreateSyncActivity(ActivityType activityType);

		// Token: 0x06000E75 RID: 3701
		SyncActivity CreateSyncActivity(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId);

		// Token: 0x06000E76 RID: 3702
		ExternalSyncActivity CreateExternalSyncActivity(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId);

		// Token: 0x06000E77 RID: 3703
		SyncActivity ImportActivity(Activity activity);
	}
}
