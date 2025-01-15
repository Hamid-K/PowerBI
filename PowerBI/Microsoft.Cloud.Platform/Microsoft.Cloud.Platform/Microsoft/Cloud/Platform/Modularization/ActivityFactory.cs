using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000A0 RID: 160
	[BlockServiceProvider(typeof(IActivityFactory))]
	public class ActivityFactory : Block, IActivityFactory
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x0001076A File Offset: 0x0000E96A
		public ActivityFactory()
			: this("ActivityFactory")
		{
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00010777 File Offset: 0x0000E977
		public ActivityFactory(string name)
			: base(name)
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00010780 File Offset: 0x0000E980
		public AsyncActivity CreateAsyncActivity(ActivityType activityType)
		{
			return this.CreateAsyncActivityImpl(Guid.NewGuid(), activityType);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001078E File Offset: 0x0000E98E
		public AsyncActivity CreateAsyncActivity(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			return this.CreateAsyncActivityImpl(activityId, activityType, rootActivityId, clientActivityId);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001079B File Offset: 0x0000E99B
		public SyncActivity CreateSyncActivity(ActivityType activityType)
		{
			return this.CreateSyncActivityImpl(Guid.NewGuid(), activityType);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000107A9 File Offset: 0x0000E9A9
		public SyncActivity CreateSyncActivity(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			return this.CreateSyncActivityImpl(activityId, activityType, rootActivityId, clientActivityId);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000107B6 File Offset: 0x0000E9B6
		public ExternalSyncActivity CreateExternalSyncActivity(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			return this.CreateExternalSyncActivityImpl(activityId, activityType, rootActivityId, clientActivityId);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000107C4 File Offset: 0x0000E9C4
		public SyncActivity ImportActivity(Activity activity)
		{
			if (UtilsContext.Current.Activity.Equals(activity))
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "ImportActivity: Activity to import - {0} - equals current. {1}", new object[]
				{
					activity.ActivityId,
					UtilsContext.Current.GetActivityStackRepresentation()
				});
			}
			return this.ImportActivityImpl(activity.ActivityId, activity.ActivityType, activity.RootActivityId, activity.ClientActivityId);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00010832 File Offset: 0x0000EA32
		protected virtual AsyncActivity CreateAsyncActivityImpl(Guid activityId, ActivityType activityType)
		{
			return new AsyncActivity(activityId, activityType);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001083B File Offset: 0x0000EA3B
		protected virtual AsyncActivity CreateAsyncActivityImpl(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			return new AsyncActivity(activityId, activityType, rootActivityId, clientActivityId);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00010847 File Offset: 0x0000EA47
		protected virtual SyncActivity CreateSyncActivityImpl(Guid activityId, ActivityType activityType)
		{
			return new SyncActivity(activityId, activityType);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00010850 File Offset: 0x0000EA50
		protected virtual SyncActivity CreateSyncActivityImpl(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			return new SyncActivity(activityId, activityType, rootActivityId, clientActivityId);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001085C File Offset: 0x0000EA5C
		protected virtual ExternalSyncActivity CreateExternalSyncActivityImpl(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			return new ExternalSyncActivity(activityId, activityType, rootActivityId, clientActivityId);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00010850 File Offset: 0x0000EA50
		protected virtual SyncActivity ImportActivityImpl(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			return new SyncActivity(activityId, activityType, rootActivityId, clientActivityId);
		}
	}
}
