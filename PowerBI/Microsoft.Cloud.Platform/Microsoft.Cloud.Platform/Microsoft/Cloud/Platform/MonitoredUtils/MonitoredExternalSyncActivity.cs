using System;
using System.Diagnostics;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200013B RID: 315
	internal class MonitoredExternalSyncActivity : ExternalSyncActivity
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x0001BE73 File Offset: 0x0001A073
		public MonitoredExternalSyncActivity(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId, IActivityEvents eventsKit)
			: base(activityId, activityType, rootActivityId, clientActivityId)
		{
			this.m_eventsKit = eventsKit;
			this.m_eventsKit.FireImplementationActivityStartedEvent();
			this.m_stopwatch = Stopwatch.StartNew();
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001BE9E File Offset: 0x0001A09E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.m_stopwatch.IsRunning)
			{
				this.m_stopwatch.Stop();
				this.m_eventsKit.FireImplementationActivityEndedEvent(this.m_stopwatch.ElapsedMilliseconds);
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000302 RID: 770
		private IActivityEvents m_eventsKit;

		// Token: 0x04000303 RID: 771
		private Stopwatch m_stopwatch;
	}
}
