using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000226 RID: 550
	public class SyncActivity : ContextMemberProxy<Activity>
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x00032EC5 File Offset: 0x000310C5
		// (set) Token: 0x06000E7E RID: 3710 RVA: 0x00032ECD File Offset: 0x000310CD
		public Activity Activity { get; private set; }

		// Token: 0x06000E7F RID: 3711 RVA: 0x00032ED8 File Offset: 0x000310D8
		public SyncActivity(Guid activityId, ActivityType activityType)
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "SyncActivity ctor: {0}", new object[] { UtilsContext.Current.GetActivityStackRepresentation() });
			this.Activity = new Activity(activityId, activityType);
			Activity activity = UtilsContext.Current.Activity;
			if (activity.Equals(Activity.Empty))
			{
				this.Activity.RootActivityId = activityId;
			}
			else
			{
				this.Activity.RootActivityId = activity.RootActivityId;
				this.Activity.ClientActivityId = activity.ClientActivityId;
			}
			base.ContextMember = UtilsContext.Current.PushActivity(this.Activity);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00032F7C File Offset: 0x0003117C
		public SyncActivity(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "SyncActivity ctor: {0}", new object[] { UtilsContext.Current.GetActivityStackRepresentation() });
			this.Activity = ((!string.IsNullOrEmpty(clientActivityId)) ? new Activity(activityId, activityType, rootActivityId, clientActivityId) : new Activity(activityId, activityType, rootActivityId));
			base.ContextMember = UtilsContext.Current.PushActivity(this.Activity);
		}
	}
}
