using System;
using System.Globalization;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000268 RID: 616
	public abstract class PendingCallback : WorkTicket, IIdentifiable
	{
		// Token: 0x06001027 RID: 4135 RVA: 0x00037AF8 File Offset: 0x00035CF8
		protected PendingCallback([NotNull] string identity, [NotNull] IWorkTicketFactory workTicketFactory)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(identity, "identity");
			ExtendedDiagnostics.EnsureArgumentNotNull<IWorkTicketFactory>(workTicketFactory, "workTicketFactory");
			this.m_lock = new object();
			this.m_identity = identity;
			workTicketFactory.CreateWorkTicket(this, this);
			this.m_callbackReason = PendingCallbackReason.Pending;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00037B48 File Offset: 0x00035D48
		protected bool ScheduleTimer(int dueTime)
		{
			ExtendedDiagnostics.EnsureArgument(dueTime, "dueTime", dueTime > 0 || dueTime == -1);
			object @lock = this.m_lock;
			bool flag2;
			lock (@lock)
			{
				if (this.m_callbackReason != PendingCallbackReason.Pending)
				{
					flag2 = false;
				}
				else
				{
					if (this.m_timer != null)
					{
						this.m_timer.Dispose();
						this.m_timerGeneration = null;
						this.m_timer = null;
					}
					if (dueTime != -1)
					{
						this.m_timerGeneration = new object();
						this.m_timer = new Timer(new TimerCallback(this.OnTimerExpired), this.m_timerGeneration, -1, -1);
						this.m_timer.Change(dueTime, -1);
					}
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06001029 RID: 4137
		protected abstract void OnCallback(PendingCallbackReason reason);

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x00037C08 File Offset: 0x00035E08
		public string Name
		{
			get
			{
				return this.m_identity;
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00037C10 File Offset: 0x00035E10
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "PendingCallback '{0}', state={1}", new object[] { this.Name, this.m_callbackReason });
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00037C40 File Offset: 0x00035E40
		public bool InvokeCallback(PendingCallbackReason reason)
		{
			ExtendedDiagnostics.EnsureArgument(reason.ToString(), "reason", reason == PendingCallbackReason.Canceled || reason == PendingCallbackReason.Completed || reason == PendingCallbackReason.Shutdown);
			return this.Complete(reason, null);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00037C70 File Offset: 0x00035E70
		private void OnTimerExpired(object state)
		{
			TopLevelHandler.Run(this, delegate
			{
				this.Complete(PendingCallbackReason.TimeExpired, state);
			});
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00037CA4 File Offset: 0x00035EA4
		private bool Complete(PendingCallbackReason reason, object state)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_callbackReason != PendingCallbackReason.Pending)
				{
					return false;
				}
				if (reason == PendingCallbackReason.TimeExpired && state != this.m_timerGeneration)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "PendingCallback '{0}' ignores timer callback as timer has been rescheduled", new object[] { this.Name });
					return false;
				}
				if (this.m_timer != null)
				{
					this.m_timer.Dispose();
					this.m_timerGeneration = null;
					this.m_timer = null;
				}
				this.m_callbackReason = reason;
			}
			try
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "PendingCallback '{0}' invokes callback with reason '{1}'", new object[] { this.Name, reason });
				this.OnCallback(reason);
			}
			finally
			{
				this.Dispose();
			}
			return true;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00037D94 File Offset: 0x00035F94
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.m_callbackReason == PendingCallbackReason.Pending)
				{
					this.InvokeCallback(PendingCallbackReason.Canceled);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0400060D RID: 1549
		private object m_lock;

		// Token: 0x0400060E RID: 1550
		private string m_identity;

		// Token: 0x0400060F RID: 1551
		private volatile PendingCallbackReason m_callbackReason;

		// Token: 0x04000610 RID: 1552
		private Timer m_timer;

		// Token: 0x04000611 RID: 1553
		private object m_timerGeneration;
	}
}
