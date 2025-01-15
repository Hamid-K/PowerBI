using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000227 RID: 551
	public class ExternalSyncActivity : ContextMemberProxy<Activity>
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x00032FEB File Offset: 0x000311EB
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x00032FF3 File Offset: 0x000311F3
		public Activity Activity { get; private set; }

		// Token: 0x06000E83 RID: 3715 RVA: 0x00032FFC File Offset: 0x000311FC
		public ExternalSyncActivity(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "ExternalSyncActivity ctor: {0}", new object[] { UtilsContext.Current.GetActivityStackRepresentation() });
			this.Activity = ((!string.IsNullOrEmpty(clientActivityId)) ? new Activity(activityId, activityType, rootActivityId, clientActivityId) : new Activity(activityId, activityType, rootActivityId));
			base.ContextMember = UtilsContext.Current.PushActivity(this.Activity);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003306C File Offset: 0x0003126C
		protected override void Dispose(bool disposing)
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "ExternalSyncActivity before dispose: {0}", new object[] { UtilsContext.Current.GetActivityStackRepresentation() });
			UtilsContext.Current.PopActivity(this.Activity, UtilsContext.ContextOperationOptions.DisableValidations);
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "ExternalSyncActivity after dispose: {0}", new object[] { UtilsContext.Current.GetActivityStackRepresentation() });
		}
	}
}
