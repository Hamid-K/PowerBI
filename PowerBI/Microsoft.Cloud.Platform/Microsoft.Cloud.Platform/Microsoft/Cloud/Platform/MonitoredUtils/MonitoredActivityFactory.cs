using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200013D RID: 317
	public class MonitoredActivityFactory : ActivityFactory
	{
		// Token: 0x06000840 RID: 2112 RVA: 0x0001BF8F File Offset: 0x0001A18F
		public MonitoredActivityFactory()
			: base("MonitoredActivityManager")
		{
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001BF9C File Offset: 0x0001A19C
		public MonitoredActivityFactory(string name)
			: base(name)
		{
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001BFA5 File Offset: 0x0001A1A5
		public void SatisfyDependencies([NotNull] IEventsKitFactory eventsKitFactory)
		{
			Ensure.ArgNotNull<IEventsKitFactory>(eventsKitFactory, "eventsKitFactory");
			Ensure.IsNull<IEventsKitFactory>(this.m_eventsKitFactory, "m_eventsKitFactory");
			this.m_eventsKitFactory = eventsKitFactory;
			this.m_eventsKit = this.m_eventsKitFactory.CreateEventsKit<IActivityEvents>();
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001BFDA File Offset: 0x0001A1DA
		protected override BlockInitializationStatus OnInitialize()
		{
			if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			this.m_eventsKit = this.m_eventsKitFactory.CreateEventsKit<IActivityEvents>();
			return BlockInitializationStatus.Done;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001BFF9 File Offset: 0x0001A1F9
		protected override AsyncActivity CreateAsyncActivityImpl(Guid activityId, ActivityType activityType)
		{
			return new MonitoredAsyncActivity(activityId, activityType, this.m_eventsKit);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001C008 File Offset: 0x0001A208
		protected override AsyncActivity CreateAsyncActivityImpl(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			return new MonitoredAsyncActivity(activityId, activityType, rootActivityId, clientActivityId, this.m_eventsKit);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001C01C File Offset: 0x0001A21C
		protected override SyncActivity CreateSyncActivityImpl(Guid activityId, ActivityType activityType)
		{
			Activity activity = UtilsContext.Current.Activity;
			SyncActivity syncActivity = new MonitoredSyncActivity(activityId, activityType, this.m_eventsKit);
			this.m_eventsKit.FireActivityCorrelationEvent(activity.ActivityId);
			return syncActivity;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001C054 File Offset: 0x0001A254
		protected override SyncActivity CreateSyncActivityImpl(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			Activity activity = UtilsContext.Current.Activity;
			SyncActivity syncActivity = new MonitoredSyncActivity(activityId, activityType, rootActivityId, clientActivityId, this.m_eventsKit);
			this.m_eventsKit.FireActivityCorrelationEvent(activity.ActivityId);
			return syncActivity;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001C08D File Offset: 0x0001A28D
		protected override SyncActivity ImportActivityImpl(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			return new MonitoredSyncActivity(activityId, activityType, rootActivityId, clientActivityId, this.m_eventsKit);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001C09F File Offset: 0x0001A29F
		protected override ExternalSyncActivity CreateExternalSyncActivityImpl(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			return new MonitoredExternalSyncActivity(activityId, activityType, rootActivityId, clientActivityId, this.m_eventsKit);
		}

		// Token: 0x04000307 RID: 775
		[BlockServiceDependency]
		private IEventsKitFactory m_eventsKitFactory;

		// Token: 0x04000308 RID: 776
		private IActivityEvents m_eventsKit;
	}
}
