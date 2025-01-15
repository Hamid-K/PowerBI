using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200013C RID: 316
	internal class MonitoredAsyncActivity : AsyncActivity
	{
		// Token: 0x0600083C RID: 2108 RVA: 0x0001BED8 File Offset: 0x0001A0D8
		public MonitoredAsyncActivity(Guid activityId, ActivityType activityType, [NotNull] IActivityEvents eventsKit)
			: base(activityId, activityType)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityEvents>(eventsKit, "eventsKit");
			this.m_eventsKit = eventsKit;
			this.m_previousActivity = UtilsContext.Current.Activity;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001BF04 File Offset: 0x0001A104
		public MonitoredAsyncActivity(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId, [NotNull] IActivityEvents eventsKit)
			: base(activityId, activityType, rootActivityId, clientActivityId)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityEvents>(eventsKit, "eventsKit");
			this.m_eventsKit = eventsKit;
			this.m_previousActivity = UtilsContext.Current.Activity;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001BF35 File Offset: 0x0001A135
		protected override void OnGetBeginAsyncScopeEnded(bool fireVerboseEvents = true)
		{
			this.m_stopwatch = Stopwatch.StartNew();
			this.m_eventsKit.FireActivityCorrelationEvent(this.m_previousActivity.ActivityId);
			if (fireVerboseEvents)
			{
				this.m_eventsKit.FireImplementationActivityStartedEvent();
			}
			base.OnGetBeginAsyncScopeEnded(true);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001BF6D File Offset: 0x0001A16D
		protected override void OnDisposingEndAsyncScopeStarted(bool fireVerboseEvents = true)
		{
			base.OnDisposingEndAsyncScopeStarted(true);
			if (fireVerboseEvents)
			{
				this.m_eventsKit.FireImplementationActivityEndedEvent(this.m_stopwatch.ElapsedMilliseconds);
			}
		}

		// Token: 0x04000304 RID: 772
		private Stopwatch m_stopwatch;

		// Token: 0x04000305 RID: 773
		private IActivityEvents m_eventsKit;

		// Token: 0x04000306 RID: 774
		private Activity m_previousActivity;
	}
}
