using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200013E RID: 318
	public abstract class MonitoredActivitySequencer : ActivitySequencer
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0001C0B1 File Offset: 0x0001A2B1
		public IAuditContext AuditContext
		{
			get
			{
				return this.m_auditContext;
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001C0BC File Offset: 0x0001A2BC
		protected MonitoredActivitySequencer(AsyncActivity asyncActivity, IMonitoredActivityCompletionModelFactory monitoredActivityCompletionModelFactory, bool fireVerboseEvents = true, IAuditContext auditContext = null)
			: base(asyncActivity)
		{
			this.m_monitoredActivityEventsKit = monitoredActivityCompletionModelFactory.CreateMonitoredActivityCompletionModel(asyncActivity.Activity.ActivityType);
			this.m_fireVerboseEvents = fireVerboseEvents;
			if (auditContext == null)
			{
				this.m_auditContext = new NoOpAuditContext();
			}
			else
			{
				this.m_auditContext = auditContext;
			}
			base.OnActivityStarted += this.ActivityStarted;
			base.OnActivityEndedNormally += this.ActivityEndedNormally;
			base.OnActivityEndedWithError += this.ActivityEndedWithError;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00005EB7 File Offset: 0x000040B7
		protected virtual ActivityContextBase ActivityContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001C13D File Offset: 0x0001A33D
		protected virtual MonitoredActivitySequencerResult ShouldActivityEndWithFailure()
		{
			return new MonitoredActivitySequencerResult(null, ActivityResult.Success);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001C146 File Offset: 0x0001A346
		protected virtual bool ShouldActivityEndWithSuccess(IMonitoredError error)
		{
			return error.IsBenign();
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001C14E File Offset: 0x0001A34E
		protected void SetToLocalMonitoringScopeIfNullOrEmpty(IMonitoredError monitoredError)
		{
			this.m_monitoredActivityEventsKit.SetToLocalMonitoringScopeIfNullOrEmpty(monitoredError);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0000E568 File Offset: 0x0000C768
		protected override bool FireVerboseEvents()
		{
			return false;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001C15D File Offset: 0x0001A35D
		protected IEnumerable<IFlowStep> RunFlowMethods(params IEnumerable<IFlowStep>[] flowMethods)
		{
			foreach (IEnumerable<IFlowStep> enumerable in flowMethods)
			{
				foreach (IFlowStep flowStep in enumerable)
				{
					yield return flowStep;
				}
				IEnumerator<IFlowStep> enumerator = null;
			}
			IEnumerable<IFlowStep>[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001C16D File Offset: 0x0001A36D
		private void ActivityStarted()
		{
			this.m_monitoredActivityEventsKit.FireActivityStartedEvent(this.m_fireVerboseEvents, this.ActivityContext, this.AuditContext);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001C18C File Offset: 0x0001A38C
		private void ActivityEndedNormally()
		{
			MonitoredActivitySequencerResult monitoredActivitySequencerResult = this.ShouldActivityEndWithFailure();
			if (monitoredActivitySequencerResult.Result != ActivityResult.Success)
			{
				this.m_monitoredActivityEventsKit.FireActivityCompletedWithFailureEvent(monitoredActivitySequencerResult.Error, this.ActivityContext, this.AuditContext);
				return;
			}
			if (monitoredActivitySequencerResult.Error == null)
			{
				this.m_monitoredActivityEventsKit.FireActivityCompletedSuccessfullyEvent(this.m_fireVerboseEvents, this.ActivityContext, this.AuditContext);
				return;
			}
			this.m_monitoredActivityEventsKit.FireActivityCompletedSuccessfullyDespiteErrorEvent(monitoredActivitySequencerResult.Error, this.ActivityContext, this.AuditContext);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001C20C File Offset: 0x0001A40C
		private void ActivityEndedWithError(IMonitoredError ex)
		{
			if (ex.IsBenign() || this.ShouldActivityEndWithSuccess(ex))
			{
				this.m_monitoredActivityEventsKit.FireActivityCompletedSuccessfullyDespiteErrorEvent(ex, this.ActivityContext, this.AuditContext);
				return;
			}
			this.m_monitoredActivityEventsKit.FireActivityCompletedWithFailureEvent(ex, this.ActivityContext, this.AuditContext);
		}

		// Token: 0x04000309 RID: 777
		private readonly MonitoredActivityCompletionModel m_monitoredActivityEventsKit;

		// Token: 0x0400030A RID: 778
		private readonly bool m_fireVerboseEvents;

		// Token: 0x0400030B RID: 779
		private readonly IAuditContext m_auditContext;
	}
}
