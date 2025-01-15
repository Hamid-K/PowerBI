using System;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000267 RID: 615
	internal sealed class PendingCallbackSlim
	{
		// Token: 0x06001022 RID: 4130 RVA: 0x000378DA File Offset: 0x00035ADA
		public PendingCallbackSlim([NotNull] Action<PendingCallbackReason> callback)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action<PendingCallbackReason>>(callback, "callback");
			this.m_lock = new object();
			this.m_callback = callback;
			this.m_callbackReason = PendingCallbackReason.Pending;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00037908 File Offset: 0x00035B08
		public bool ScheduleTimer(int dueTime)
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

		// Token: 0x06001024 RID: 4132 RVA: 0x000379C8 File Offset: 0x00035BC8
		public bool InvokeCallback(PendingCallbackReason reason)
		{
			ExtendedDiagnostics.EnsureArgument(reason.ToString(), "reason", reason == PendingCallbackReason.Canceled || reason == PendingCallbackReason.Completed || reason == PendingCallbackReason.Shutdown);
			return this.Complete(reason, null);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000379F8 File Offset: 0x00035BF8
		private void OnTimerExpired(object state)
		{
			TopLevelHandler.Run(this, delegate
			{
				this.Complete(PendingCallbackReason.TimeExpired, state);
			});
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00037A2C File Offset: 0x00035C2C
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
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "PendingCallbackSlim ignores timer callback as timer has been rescheduled");
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
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "PendingCallbackSlim invokes callback with reason '{0}'", new object[] { reason });
			this.m_callback(reason);
			this.m_callback = null;
			return true;
		}

		// Token: 0x04000608 RID: 1544
		private object m_lock;

		// Token: 0x04000609 RID: 1545
		private Action<PendingCallbackReason> m_callback;

		// Token: 0x0400060A RID: 1546
		private volatile PendingCallbackReason m_callbackReason;

		// Token: 0x0400060B RID: 1547
		private Timer m_timer;

		// Token: 0x0400060C RID: 1548
		private object m_timerGeneration;
	}
}
