using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000136 RID: 310
	[BlockServiceProvider(typeof(IMonitoredActivityCompletionModelFactory), PublishWhen = BlockServicePublish.Default)]
	public class MonitoredActivityCompletionModelFactory : Block, IMonitoredActivityCompletionModelFactory
	{
		// Token: 0x06000817 RID: 2071 RVA: 0x0001B773 File Offset: 0x00019973
		public MonitoredActivityCompletionModelFactory()
			: this("MonitoredActivityCompletionModelFactory")
		{
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00010777 File Offset: 0x0000E977
		public MonitoredActivityCompletionModelFactory(string name)
			: base(name)
		{
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001B780 File Offset: 0x00019980
		public void SatisfyDependencies([NotNull] IElementInstanceId elementInstanceId, [NotNull] IEventsKitFactory eventsKitFactory, [NotNull] IActivityFactory activityFactory)
		{
			Ensure.ArgNotNull<IElementInstanceId>(elementInstanceId, "elementInstanceId");
			Ensure.IsNull<IElementInstanceId>(this.m_elementInstanceId, "m_elementInstanceId");
			Ensure.ArgNotNull<IEventsKitFactory>(eventsKitFactory, "eventsKitFactory");
			Ensure.IsNull<IEventsKitFactory>(this.m_eventsKitFactory, "m_eventsKitFactory");
			Ensure.ArgNotNull<IActivityFactory>(activityFactory, "activityFactory");
			Ensure.IsNull<IActivityFactory>(this.m_activityFactory, "m_activityFactory");
			this.m_elementInstanceId = elementInstanceId;
			this.m_eventsKitFactory = eventsKitFactory;
			this.m_activityFactory = activityFactory;
			this.DetermineLocalMonitoringScope();
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001B7F9 File Offset: 0x000199F9
		protected override BlockInitializationStatus OnInitialize()
		{
			if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			this.DetermineLocalMonitoringScope();
			return BlockInitializationStatus.Done;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001B810 File Offset: 0x00019A10
		public MonitoredActivityCompletionModel CreateMonitoredActivityCompletionModel([NotNull] ActivityType activityType)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			IMonitoredActivityEventsKit monitoredActivityEventsKit = this.m_eventsKitFactory.CreateEventsKit<IMonitoredActivityEventsKit>(activityType);
			ExtendedDiagnostics.EnsureNotNull<IMonitoredActivityEventsKit>(monitoredActivityEventsKit, "eventsKit");
			return new MonitoredActivityCompletionModel(this.m_localMonitoringScope, monitoredActivityEventsKit);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001B84C File Offset: 0x00019A4C
		public void CreateSyncActivityAndInvokeWithNewModel(ActivityType activityType, Action action)
		{
			this.CreateSyncActivityAndInvokeWithNewModel(activityType, action, null);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001B857 File Offset: 0x00019A57
		public void CreateSyncActivityAndInvokeWithNewModel([NotNull] ActivityType activityType, Action action, Predicate<IMonitoredError> shouldActivityEndWithSuccess)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			this.InvokeMonitoredActivity(action, this.m_activityFactory.CreateSyncActivity(activityType), shouldActivityEndWithSuccess);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001B878 File Offset: 0x00019A78
		public void CreateSyncActivityAndInvokeWithNewModel(Activity activity, Action action)
		{
			this.CreateSyncActivityAndInvokeWithNewModel(activity, action, null);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001B883 File Offset: 0x00019A83
		public void CreateSyncActivityAndInvokeWithNewModel([NotNull] Activity activity, Action action, Predicate<IMonitoredError> shouldActivityEndWithSuccess)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Activity>(activity, "activity");
			this.InvokeMonitoredActivity(action, this.m_activityFactory.ImportActivity(activity), shouldActivityEndWithSuccess);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001B8A4 File Offset: 0x00019AA4
		public IEventsKitFactory GetEventsKitFactory()
		{
			return this.m_eventsKitFactory;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001B8AC File Offset: 0x00019AAC
		private void InvokeMonitoredActivity([NotNull] Action action, SyncActivity syncActivity, Predicate<IMonitoredError> shouldActivityEndWithSuccess)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(action, "action");
			try
			{
				new MonitoredActivityInvoker(syncActivity, this, shouldActivityEndWithSuccess).Invoke(action);
			}
			finally
			{
				if (syncActivity != null)
				{
					((IDisposable)syncActivity).Dispose();
				}
			}
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		private void DetermineLocalMonitoringScope()
		{
			string text = this.m_elementInstanceId.GetElementInstanceId().ToString();
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(text, "elementId");
			this.m_localMonitoringScope = new MonitoringScopeId(text);
		}

		// Token: 0x040002F6 RID: 758
		[BlockServiceDependency]
		private IElementInstanceId m_elementInstanceId;

		// Token: 0x040002F7 RID: 759
		[BlockServiceDependency]
		private IEventsKitFactory m_eventsKitFactory;

		// Token: 0x040002F8 RID: 760
		[BlockServiceDependency]
		private IActivityFactory m_activityFactory;

		// Token: 0x040002F9 RID: 761
		private MonitoringScopeId m_localMonitoringScope;
	}
}
