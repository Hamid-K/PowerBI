using System;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002CF RID: 719
	public sealed class PeriodicTimer : TimerBase
	{
		// Token: 0x06001339 RID: 4921 RVA: 0x00042928 File Offset: 0x00040B28
		internal PeriodicTimer([NotNull] string identity, [NotNull] IWorkTicketFactory workTicketFactory, [NotNull] PeriodicTimerPolicy policy, [NotNull] TimerCallback timerCallback, object state, TimerCreationFlags timerCreationFlags)
			: base(identity, workTicketFactory, timerCallback, state, timerCreationFlags)
		{
			using (DisposeController disposeController = new DisposeController(this))
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<PeriodicTimerPolicy>(policy, "policy");
				this.m_lock = new object();
				this.m_periodicTimer = new Timer(new TimerCallback(this.OnPeriodicCallback), null, -1, -1);
				this.m_policy = policy;
				this.m_stopped = false;
				this.m_shutdownWorkTicket = workTicketFactory.CreateWorkTicket(this);
				policy.SetInitialSchedule(this.m_periodicTimer);
				disposeController.PreventDispose();
			}
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000429C4 File Offset: 0x00040BC4
		public void UpdatePeriod(TimeSpan period, bool tickNow)
		{
			int num = TimerBase.ConvertTimeSpanToInt(period);
			this.UpdatePeriod(num, tickNow);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x000429E0 File Offset: 0x00040BE0
		public void UpdatePeriod(int period)
		{
			this.UpdatePeriod(period, false);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000429EC File Offset: 0x00040BEC
		public void UpdatePeriod(int period, bool tickNow)
		{
			Timer timerIfAvailable = this.GetTimerIfAvailable();
			bool flag = false;
			if (timerIfAvailable != null)
			{
				try
				{
					this.m_policy.UpdatePeriod(period, timerIfAvailable, tickNow);
					flag = true;
				}
				catch (ObjectDisposedException)
				{
				}
			}
			if (flag)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "PeriodicTimer '{0}' UpdatePeriod({1}) completed successfully", new object[] { base.Name, period });
				return;
			}
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "PeriodicTimer '{0}' UpdatePeriod({1}) cancelled as shutdown sequence has started", new object[] { base.Name, period });
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00042A80 File Offset: 0x00040C80
		protected override void OnCallback(PendingCallbackReason reason)
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "PeriodicTimer '{0}' shutdown command has been received", new object[] { base.Name });
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_stopped = true;
			}
			AsyncInvoker.InvokeMethodAsynchronously(new Action(this.Shutdown), WaitOrNot.DontWait, "PeriodicTimer.Shutdown");
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00042AF8 File Offset: 0x00040CF8
		private void Shutdown()
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "PeriodicTimer '{0}' starts its shutdown sequence", new object[] { base.Name });
			Timer timer = null;
			object @lock = this.m_lock;
			lock (@lock)
			{
				timer = this.m_periodicTimer;
				this.m_periodicTimer = null;
			}
			if (timer != null)
			{
				ManualResetEvent manualResetEvent = new ManualResetEvent(false);
				timer.Dispose(manualResetEvent);
				manualResetEvent.WaitOne();
			}
			if (this.m_shutdownWorkTicket != null)
			{
				this.m_shutdownWorkTicket.Dispose();
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00042B90 File Offset: 0x00040D90
		private void OnPeriodicCallback(object state)
		{
			Timer timerIfAvailable = this.GetTimerIfAvailable();
			bool flag = false;
			if (timerIfAvailable != null)
			{
				try
				{
					this.m_policy.OnPeriodicCallback(base.Name, timerIfAvailable, new Action(base.InvokeTimerCallback));
					flag = true;
				}
				catch (ObjectDisposedException)
				{
				}
			}
			if (!flag)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "PeriodicTimer '{0}' ignores a timer callback because it has started the shutdown sequence", new object[] { base.Name });
			}
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00042C04 File Offset: 0x00040E04
		private Timer GetTimerIfAvailable()
		{
			Timer timer = null;
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.m_stopped && this.m_periodicTimer != null)
				{
					timer = this.m_periodicTimer;
				}
			}
			return timer;
		}

		// Token: 0x04000733 RID: 1843
		private object m_lock;

		// Token: 0x04000734 RID: 1844
		private Timer m_periodicTimer;

		// Token: 0x04000735 RID: 1845
		private PeriodicTimerPolicy m_policy;

		// Token: 0x04000736 RID: 1846
		private bool m_stopped;

		// Token: 0x04000737 RID: 1847
		private WorkTicket m_shutdownWorkTicket;
	}
}
