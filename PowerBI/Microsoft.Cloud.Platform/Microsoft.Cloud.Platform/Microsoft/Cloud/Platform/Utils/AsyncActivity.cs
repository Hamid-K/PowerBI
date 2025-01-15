using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000225 RID: 549
	public class AsyncActivity : AsyncContextMemberProxy<Activity>
	{
		// Token: 0x06000E78 RID: 3704 RVA: 0x00032DC2 File Offset: 0x00030FC2
		public AsyncActivity(Guid activityId, ActivityType activityType)
			: base(new Activity(activityId, activityType), 0)
		{
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00032DD2 File Offset: 0x00030FD2
		public AsyncActivity(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
			: base(new Activity(activityId, activityType, rootActivityId, clientActivityId), 0)
		{
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00032DE5 File Offset: 0x00030FE5
		public Activity Activity
		{
			get
			{
				return base.Member;
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00032DF0 File Offset: 0x00030FF0
		protected override void OnGetBeginAsyncScopeStarted()
		{
			base.OnGetBeginAsyncScopeStarted();
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Beginning of GetBeginAsyncScope: {0}", new object[] { UtilsContext.Current.GetActivityStackRepresentation() });
			Activity activity = UtilsContext.Current.Activity;
			if (activity.Equals(Activity.Empty))
			{
				this.Activity.RootActivityId = (this.Activity.RootActivityId.Equals(Guid.Empty) ? this.Activity.ActivityId : this.Activity.RootActivityId);
				return;
			}
			this.Activity.RootActivityId = activity.RootActivityId;
			this.Activity.ClientActivityId = activity.ClientActivityId;
		}

		// Token: 0x0400059F RID: 1439
		private static readonly string s_emptyGuid = Guid.Empty.ToString();
	}
}
