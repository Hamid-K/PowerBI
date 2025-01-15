using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000138 RID: 312
	public class MonitoredActivityInvoker
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x0001BB5F File Offset: 0x00019D5F
		public MonitoredActivityInvoker(SyncActivity syncActivity, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory)
			: this(syncActivity, monitoredActivityCompletionModelFactory, null)
		{
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001BB6C File Offset: 0x00019D6C
		public MonitoredActivityInvoker([NotNull] SyncActivity syncActivity, [NotNull] IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, Predicate<IMonitoredError> shouldActivityEndWithSuccess)
		{
			Ensure.ArgNotNull<SyncActivity>(syncActivity, "syncActivity");
			Ensure.ArgSatisfiesCondition("syncActivity", UtilsContext.Current.Activity.Equals(syncActivity.Activity), "MonitoredActivityInvoker must be created when the sync activity is on the context stack");
			Ensure.ArgNotNull<IMonitoredActivityCompletionModelFactory>(monitoredActivityCompletionModelFactory, "monitoredActivityCompletionModelFactory");
			this.m_syncActivity = syncActivity;
			this.m_shouldActivityEndWithSuccess = shouldActivityEndWithSuccess;
			this.m_monitoredActivityEventsKit = monitoredActivityCompletionModelFactory.CreateMonitoredActivityCompletionModel(syncActivity.Activity.ActivityType);
			this.m_monitoredActivityEventsKit.FireActivityStartedEvent(true, null, null);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001BBEC File Offset: 0x00019DEC
		public void FireActivityCompletedSuccessfullyEvent()
		{
			this.EnsureActivity();
			this.m_monitoredActivityEventsKit.FireActivityCompletedSuccessfullyEvent(true, null, null);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001BC02 File Offset: 0x00019E02
		public void FireActivityCompletedWithFailureEvent(IMonitoredError err)
		{
			this.EnsureActivity();
			this.m_monitoredActivityEventsKit.FireActivityCompletedWithFailureEvent(err, null, null);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001BC18 File Offset: 0x00019E18
		public void FireActivityCompletedWithFailureEvent(Exception ex)
		{
			this.EnsureActivity();
			this.m_monitoredActivityEventsKit.FireActivityCompletedWithFailureEvent(this.GetMonitoredError(ex), null, null);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001BC34 File Offset: 0x00019E34
		public void FireActivityCompletedSuccessfullyDespiteErrorEvent(IMonitoredError err)
		{
			this.EnsureActivity();
			this.m_monitoredActivityEventsKit.FireActivityCompletedSuccessfullyDespiteErrorEvent(err, null, null);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001BC4C File Offset: 0x00019E4C
		public void Invoke([NotNull] Action action)
		{
			Ensure.ArgNotNull<Action>(action, "action");
			Exception exception = null;
			ExceptionFilters.TryFilterCatchFaultFinally(action, delegate(Exception ex)
			{
				exception = ex;
				return ExceptionDisposition.ContinueSearch;
			}, null, delegate
			{
				this.FireCompletionEventDueToError(exception);
			}, null);
			this.m_monitoredActivityEventsKit.FireActivityCompletedSuccessfullyEvent(true, null, null);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001BCA8 File Offset: 0x00019EA8
		private void EnsureActivity()
		{
			ExtendedDiagnostics.EnsureOperation(this.m_syncActivity.Activity.Equals(UtilsContext.Current.Activity), "Current activity '{0}' does not equal the expected activity '{1}'".FormatWithInvariantCulture(new object[]
			{
				UtilsContext.Current.Activity,
				this.m_syncActivity.Activity
			}));
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001BD00 File Offset: 0x00019F00
		private void FireCompletionEventDueToError(Exception ex)
		{
			if (ex != null && !(ex is IMonitoredError))
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("Activity Threw Non Monitored Exception: {0}", new object[] { ex });
			}
			IMonitoredError monitoredError = this.GetMonitoredError(ex);
			if (this.m_shouldActivityEndWithSuccess != null && this.m_shouldActivityEndWithSuccess(monitoredError))
			{
				this.FireActivityCompletedSuccessfullyDespiteErrorEvent(monitoredError);
				return;
			}
			this.FireActivityCompletedWithFailureEvent(monitoredError);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001BD5E File Offset: 0x00019F5E
		private IMonitoredError GetMonitoredError(Exception ex)
		{
			return (ex as IMonitoredError) ?? new MonitoredActivityInvokerException(this.m_syncActivity.Activity, null, ex);
		}

		// Token: 0x040002FD RID: 765
		private SyncActivity m_syncActivity;

		// Token: 0x040002FE RID: 766
		private Predicate<IMonitoredError> m_shouldActivityEndWithSuccess;

		// Token: 0x040002FF RID: 767
		private MonitoredActivityCompletionModel m_monitoredActivityEventsKit;
	}
}
