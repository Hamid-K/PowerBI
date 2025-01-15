using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000335 RID: 821
	public abstract class EventsKitBase
	{
		// Token: 0x0600182A RID: 6186 RVA: 0x00058AAB File Offset: 0x00056CAB
		protected EventsKitBase([NotNull] ElementId elementId, ActivityType activityType, [NotNull] string performanceCountersInstanceName, IEventingServer eventsServer)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ElementId>(elementId, "elementId");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(performanceCountersInstanceName, "performanceCountersInstanceName");
			this.ElementId = elementId;
			this.ActivityType = activityType;
			this.PerformanceCountersInstanceName = performanceCountersInstanceName;
			this.EventingServer = eventsServer;
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x00058AE6 File Offset: 0x00056CE6
		// (set) Token: 0x0600182C RID: 6188 RVA: 0x00058AEE File Offset: 0x00056CEE
		private protected IEventingServer EventingServer { protected get; private set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x00058AF7 File Offset: 0x00056CF7
		// (set) Token: 0x0600182E RID: 6190 RVA: 0x00058AFF File Offset: 0x00056CFF
		private protected ElementId ElementId { protected get; private set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x00058B08 File Offset: 0x00056D08
		// (set) Token: 0x06001830 RID: 6192 RVA: 0x00058B10 File Offset: 0x00056D10
		private protected ActivityType ActivityType { protected get; private set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x00058B19 File Offset: 0x00056D19
		// (set) Token: 0x06001832 RID: 6194 RVA: 0x00058B21 File Offset: 0x00056D21
		private protected string PerformanceCountersInstanceName { protected get; private set; }

		// Token: 0x06001833 RID: 6195 RVA: 0x00058B2A File Offset: 0x00056D2A
		protected void WriteCallStackToTrace(string eventName, Exception exception)
		{
			TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}: exception has no call stack. Exception: {1}", new object[]
			{
				eventName,
				exception.ToString()
			});
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00058B50 File Offset: 0x00056D50
		protected void AssertValid()
		{
			if (this.ActivityType != null && !this.ActivityType.Equals(UtilsContext.Current.Activity.ActivityType))
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceWarning("Event Fired under illegal activityType Expected: {0}, Actual:{1}", new object[]
				{
					this.ActivityType,
					UtilsContext.Current.Activity.ActivityType
				});
			}
		}
	}
}
