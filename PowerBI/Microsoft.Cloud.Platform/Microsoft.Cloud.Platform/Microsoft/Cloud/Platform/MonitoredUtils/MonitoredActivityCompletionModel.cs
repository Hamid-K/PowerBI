using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000137 RID: 311
	public class MonitoredActivityCompletionModel
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x0001B925 File Offset: 0x00019B25
		public MonitoredActivityCompletionModel([NotNull] MonitoringScopeId monitoringScope, [NotNull] IMonitoredActivityEventsKit eventsKit)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<MonitoringScopeId>(monitoringScope, "monitoringScope");
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredActivityEventsKit>(eventsKit, "eventsKit");
			this.m_localMonitoringScope = monitoringScope;
			this.m_eventsKit = eventsKit;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001B951 File Offset: 0x00019B51
		public void FireActivityStartedEvent(bool fireVerboseEvents = true, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			this.m_stopWatch = Stopwatch.StartNew();
			if (fireVerboseEvents)
			{
				this.m_eventsKit.FireActivityStartedEvent((activityContext != null) ? activityContext.ToString() : string.Empty);
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001B97C File Offset: 0x00019B7C
		public void FireActivityCompletedSuccessfullyEvent(bool fireVerboseEvents = true, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			this.m_stopWatch.Stop();
			long elapsedMilliseconds = this.m_stopWatch.ElapsedMilliseconds;
			if (fireVerboseEvents)
			{
				this.m_eventsKit.FireActivityCompletedEvent(elapsedMilliseconds, (activityContext != null) ? activityContext.ToString() : string.Empty);
				this.m_eventsKit.FireActivityCompletedSuccessfullyEvent(elapsedMilliseconds, (activityContext != null) ? activityContext.ToString() : string.Empty);
				if (auditContext != null && auditContext.ShouldAudit())
				{
					auditContext.RecordActivity(true);
				}
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001B9F0 File Offset: 0x00019BF0
		public void FireActivityCompletedWithFailureEvent(IMonitoredError err, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			this.SetToLocalMonitoringScopeIfNullOrEmpty(err);
			this.m_stopWatch.Stop();
			long elapsedMilliseconds = this.m_stopWatch.ElapsedMilliseconds;
			if (err.MonitoringScope.Equals(this.m_localMonitoringScope))
			{
				this.m_eventsKit.FireActivityCompletedEvent(elapsedMilliseconds, (activityContext != null) ? activityContext.ToString() : string.Empty);
				this.m_eventsKit.FireActivityCompletedWithFailureEvent(elapsedMilliseconds, err, err.ErrorEventId, (activityContext != null) ? activityContext.ToString() : string.Empty);
				if (auditContext != null && auditContext.ShouldAudit())
				{
					auditContext.RecordActivity(false);
					return;
				}
			}
			else
			{
				this.m_eventsKit.FireActivityCompletedEvent(elapsedMilliseconds, (activityContext != null) ? activityContext.ToString() : string.Empty);
				this.m_eventsKit.FireActivityCompletedWithRemoteFailureEvent(elapsedMilliseconds, err, err.ErrorEventId, (activityContext != null) ? activityContext.ToString() : string.Empty);
				if (auditContext != null && auditContext.ShouldAudit())
				{
					auditContext.RecordActivity(false);
				}
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001BAD4 File Offset: 0x00019CD4
		public void FireActivityCompletedSuccessfullyDespiteErrorEvent(IMonitoredError err, ActivityContextBase activityContext = null, IAuditContext auditContext = null)
		{
			this.SetToLocalMonitoringScopeIfNullOrEmpty(err);
			this.m_stopWatch.Stop();
			long elapsedMilliseconds = this.m_stopWatch.ElapsedMilliseconds;
			this.m_eventsKit.FireActivityCompletedEvent(elapsedMilliseconds, (activityContext != null) ? activityContext.ToString() : string.Empty);
			this.m_eventsKit.FireActivityCompletedSuccessfullyDespiteFailureEvent(elapsedMilliseconds, err, err.ErrorEventId, (activityContext != null) ? activityContext.ToString() : string.Empty);
			if (auditContext != null && auditContext.ShouldAudit())
			{
				auditContext.RecordActivity(true);
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001BB51 File Offset: 0x00019D51
		public bool SetToLocalMonitoringScopeIfNullOrEmpty(IMonitoredError monitoredError)
		{
			return monitoredError.SetMonitoringScopeIfNullOrEmpty(this.m_localMonitoringScope);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00009B3B File Offset: 0x00007D3B
		[Conditional("DEBUG")]
		private void AdvanceStateAndFailSlowIfUnequal(int state, string method)
		{
		}

		// Token: 0x040002FA RID: 762
		private Stopwatch m_stopWatch;

		// Token: 0x040002FB RID: 763
		private MonitoringScopeId m_localMonitoringScope;

		// Token: 0x040002FC RID: 764
		private IMonitoredActivityEventsKit m_eventsKit;
	}
}
