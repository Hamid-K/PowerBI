using System;
using System.Diagnostics;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200013A RID: 314
	internal class MonitoredSyncActivity : SyncActivity
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		internal MonitoredSyncActivity(Guid activityId, ActivityType activityType, IActivityEvents eventsKit)
			: base(activityId, activityType)
		{
			this.Initialize(eventsKit);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001BE05 File Offset: 0x0001A005
		internal MonitoredSyncActivity(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId, IActivityEvents eventsKit)
			: base(activityId, activityType, rootActivityId, clientActivityId)
		{
			this.Initialize(eventsKit);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001BE1A File Offset: 0x0001A01A
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.m_stopwatch.IsRunning)
			{
				this.m_stopwatch.Stop();
				this.m_eventsKit.FireImplementationActivityEndedEvent(this.m_stopwatch.ElapsedMilliseconds);
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001BE54 File Offset: 0x0001A054
		private void Initialize(IActivityEvents eventsKit)
		{
			this.m_eventsKit = eventsKit;
			this.m_eventsKit.FireImplementationActivityStartedEvent();
			this.m_stopwatch = Stopwatch.StartNew();
		}

		// Token: 0x04000300 RID: 768
		private IActivityEvents m_eventsKit;

		// Token: 0x04000301 RID: 769
		private Stopwatch m_stopwatch;
	}
}
