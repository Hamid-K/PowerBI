using System;
using System.Globalization;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002B6 RID: 694
	public class BasedPoliciesThrottler : ThrottlerBase
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00040AAA File Offset: 0x0003ECAA
		public int CurrentlyRunningOperations
		{
			get
			{
				return this.m_throttlingPolicy.CurrentlyRunningOperations;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x00040AB7 File Offset: 0x0003ECB7
		public int MaxConcurrentOperations
		{
			get
			{
				return this.m_throttlingPolicy.MaxConcurrentOperations;
			}
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00040AC4 File Offset: 0x0003ECC4
		public BasedPoliciesThrottler(string name, int maxPendingOperations, IThrottlerNotifications notifications, IThrottlingPolicy policy, QueueFullPolicyType policyType)
			: this(name, maxPendingOperations, notifications, policy, policyType, BasedPoliciesThrottler.c_defaultExpirationTimeout, null, true)
		{
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00040AE5 File Offset: 0x0003ECE5
		public BasedPoliciesThrottler(string name, int maxPendingOperations, IThrottlerNotifications notifications, IThrottlingPolicy policy)
			: this(name, maxPendingOperations, notifications, policy, QueueFullPolicyType.DropNewOperation, BasedPoliciesThrottler.c_defaultExpirationTimeout)
		{
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00040AF8 File Offset: 0x0003ECF8
		public BasedPoliciesThrottler(string name, int maxPendingOperations, IThrottlerNotifications notifications, IThrottlingPolicy policy, bool failSlowOnDeadlockDetection)
			: this(name, maxPendingOperations, notifications, policy, QueueFullPolicyType.DropNewOperation, BasedPoliciesThrottler.c_defaultExpirationTimeout, null, failSlowOnDeadlockDetection)
		{
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00040B1C File Offset: 0x0003ED1C
		public BasedPoliciesThrottler(string name, int maxPendingOperations, IThrottlerNotifications notifications, IThrottlingPolicy policy, QueueFullPolicyType policyType, TimeSpan operationTimeToLive)
			: this(name, maxPendingOperations, notifications, policy, policyType, operationTimeToLive, null, true)
		{
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00040B3C File Offset: 0x0003ED3C
		public BasedPoliciesThrottler([NotNull] string name, int maxPendingOperations, [NotNull] IThrottlerNotifications notifications, [NotNull] IThrottlingPolicy policy, QueueFullPolicyType policyType, TimeSpan operationTimeToLive, ExpirationDetector.OnObjectExpired expirationDetectorCallback, bool failSlowOnDeadlockDetection)
			: base(name, maxPendingOperations, notifications, policyType)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IThrottlingPolicy>(policy, "policy");
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxPendingOperations, "maxPendingOperations");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative((long)operationTimeToLive.TotalMilliseconds, "operationTimeToLive");
			this.m_throttlingPolicy = policy;
			this.m_timerFactory = new TimerFactory(name + ".Timer", TimerCreationFlags.Crash);
			this.m_scheduledPolicyEvaluationTime = TimeToRun.Infinite;
			this.m_failSlowOnDeadlockDetection = failSlowOnDeadlockDetection;
			ExpirationDetector.OnObjectExpired onObjectExpired = ((expirationDetectorCallback == null) ? new ExpirationDetector.OnObjectExpired(this.OnRunningOperationExpired) : expirationDetectorCallback);
			this.m_runningOperationsExpirationDetector = new ExpirationDetector(name + ".ExpirationDetector", operationTimeToLive, onObjectExpired);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00040BF8 File Offset: 0x0003EDF8
		public override IAsyncResult BeginTryAcquireLock(string name, AsyncCallback userCallback, object userContext)
		{
			TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Throttler '{0}' BeginAcquireLock invoked with operation '{1}'", new object[] { this.Name, name });
			ThrottlerBase.ThrottlerWorkTicket throttlerWorkTicket = new ThrottlerBase.ThrottlerWorkTicket(this, base.WorkTicketManager);
			bool flag = true;
			IAsyncResult asyncResult;
			using (DisposeController disposeController = new DisposeController(throttlerWorkTicket))
			{
				ThrottlerAsyncResult throttlerAsyncResult = new ThrottlerAsyncResult(name, throttlerWorkTicket, userCallback, userContext);
				object locker = base.Locker;
				lock (locker)
				{
					if (!base.Active)
					{
						TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Warning, "BasedPoliciesThrottler '{0}' drops operation '{1}' as the shutdown sequence has started", new object[] { this.Name, throttlerAsyncResult.Name });
						throw new ShutdownSequenceStartedException(string.Concat(new string[] { "BasedPoliciesThrottler '", this.Name, "' drops operation '", throttlerAsyncResult.Name, "' as the shutdown sequence has started" }));
					}
					DateTime utcNow = DateTime.UtcNow;
					flag = base.PendingOperations.Enqueue(throttlerAsyncResult);
					if ((double)base.PendingOperations.Count > (double)base.PendingOperations.MaxCapacity * 0.98)
					{
						TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Warning, "PendingOperations queue near full max capacity of {0} operations: {1} operations pending", new object[]
						{
							base.PendingOperations.MaxCapacity.ToString(),
							base.PendingOperations.Count.ToString()
						});
						TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Warning, "The oldest pending operation was enqueued at {0}: {1} seconds ago", new object[]
						{
							base.PendingOperations.Peek().EnqueuedDateTime.ToString(CultureInfo.InvariantCulture),
							(DateTime.UtcNow - base.PendingOperations.Peek().EnqueuedDateTime).TotalSeconds.ToString()
						});
					}
					if (base.PendingOperations.Count > 0)
					{
						this.TryRunPendingOperations(utcNow);
					}
				}
				if (flag)
				{
					base.Notifications.OnStateChanged(base.PendingOperations.Count, this.m_throttlingPolicy.CurrentlyRunningOperations);
				}
				else
				{
					TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Warning, "Throttler '{0}' drops operation '{1}'", new object[] { this.Name, throttlerAsyncResult.Name });
					base.Notifications.OnOverflow();
					throttlerAsyncResult.SignalCompletion(true);
				}
				disposeController.PreventDispose();
				asyncResult = throttlerAsyncResult;
			}
			return asyncResult;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00040E7C File Offset: 0x0003F07C
		[CanBeNull]
		public override IDisposable EndTryAcquireLock([NotNull] IAsyncResult result)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IAsyncResult>(result, "result");
			ThrottlerAsyncResult throttlerAsyncResult = (ThrottlerAsyncResult)result;
			TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Throttler '{0}' EndAcquireLock invoked with operation '{1}'", new object[] { this.Name, throttlerAsyncResult.Name });
			IDisposable disposable;
			using (DisposeController disposeController = new DisposeController(throttlerAsyncResult.WorkTicket))
			{
				throttlerAsyncResult.End();
				if (throttlerAsyncResult.IsQueueFull)
				{
					disposable = null;
				}
				else
				{
					bool flag = true;
					object locker = base.Locker;
					lock (locker)
					{
						DateTime utcNow = DateTime.UtcNow;
						if (!base.Active)
						{
							TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Warning, "Throttler '{0}' drops operation '{1}' as it has been stopped", new object[] { this.Name, throttlerAsyncResult.Name });
							this.m_throttlingPolicy.OnOperationCompleted(utcNow);
							flag = false;
						}
					}
					if (!flag)
					{
						base.Notifications.OnStateChanged(base.PendingOperations.Count, this.m_throttlingPolicy.CurrentlyRunningOperations);
						throw new ShutdownSequenceStartedException(string.Concat(new string[] { "Throttler '", this.Name, "' drops operation '", throttlerAsyncResult.Name, "' as it has been stopped" }));
					}
					ThrottlerBase.ThrottlerWorkTicket throttlerWorkTicket = (ThrottlerBase.ThrottlerWorkTicket)throttlerAsyncResult.WorkTicket;
					throttlerWorkTicket.Activate();
					disposeController.PreventDispose();
					if (this.sm_expirationDetectionEnabledTweak.Value)
					{
						try
						{
							this.m_runningOperationsExpirationDetector.StartWatching(throttlerAsyncResult.Name, throttlerWorkTicket);
							throttlerWorkTicket.IsWatched = true;
						}
						catch (ShutdownSequenceStartedException)
						{
						}
					}
					disposable = throttlerAsyncResult.WorkTicket;
				}
			}
			return disposable;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00041054 File Offset: 0x0003F254
		private void OnRunningOperationExpired(string name, object ticket)
		{
			if (!this.m_failSlowOnDeadlockDetection)
			{
				TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Info, "Operation {0} has exceeded timeout. Ignoring since Expiration Detector is disabled", new object[] { name });
				base.Notifications.OnPotentialDeadlockDetected(name, "ignoring timeout since Expiration Detector is disabled");
				return;
			}
			TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Fatal, "Operation {0} has exceeded timeout", new object[] { name });
			base.Notifications.OnPotentialDeadlockDetected(name, "calling ExtendedEnvironment.FailSlow to crash the process");
			this.m_expiredWorkTicket = (ThrottlerBase.ThrottlerWorkTicket)ticket;
			ExtendedEnvironment.FailSlow(this, string.Format(CultureInfo.InvariantCulture, "Throttler has detected a potential dead-lock: operation {0} has exceeded timeout", new object[] { name }));
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x000410EC File Offset: 0x0003F2EC
		private void ScheduleNextPolicyEvaluationTimer(DateTime now, DateTime when)
		{
			if (when == this.m_scheduledPolicyEvaluationTime)
			{
				TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Timer is already scheduled to wake up at {0}", new object[] { when });
				return;
			}
			this.m_scheduledPolicyEvaluationTime = when;
			TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Scheduling timer to wake up at {0}", new object[] { when });
			if (this.m_scheduledPolicyEvaluationTimer != null)
			{
				this.m_scheduledPolicyEvaluationTimer.Dispose();
				this.m_scheduledPolicyEvaluationTimer = null;
			}
			if (this.m_scheduledPolicyEvaluationTime != TimeToRun.Infinite)
			{
				int num = (int)this.m_scheduledPolicyEvaluationTime.Subtract(now).TotalMilliseconds;
				num = ((num == 0) ? 1 : num);
				this.m_scheduledPolicyEvaluationTimer = this.m_timerFactory.ScheduleOneShotTimer(this.Name + ".ScheduledPolicyEvaluationTimer", num, new TimerCallback(this.OnTimeout), this.m_scheduledPolicyEvaluationTime);
			}
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x000411D0 File Offset: 0x0003F3D0
		private void OnTimeout(object sender)
		{
			bool flag = false;
			object locker = base.Locker;
			lock (locker)
			{
				if (!base.Active)
				{
					return;
				}
				DateTime utcNow = DateTime.UtcNow;
				DateTime dateTime = (DateTime)sender;
				if (utcNow < dateTime)
				{
					this.m_scheduledPolicyEvaluationTime = TimeToRun.Infinite;
				}
				flag = this.TryRunPendingOperations(utcNow);
			}
			if (flag)
			{
				base.Notifications.OnStateChanged(base.PendingOperations.Count, this.m_throttlingPolicy.CurrentlyRunningOperations);
			}
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00041268 File Offset: 0x0003F468
		private bool TryAcquireLockInternal(string name, DateTime now)
		{
			TimeToRun timeToRun = this.m_throttlingPolicy.GetTimeToRun(now);
			if (timeToRun.IsNow)
			{
				TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Operation '{0}' is about to run", new object[] { name });
				this.m_throttlingPolicy.OnOperationStarted(now);
				return true;
			}
			TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Operation '{0}' may not run now", new object[] { name });
			this.ScheduleNextPolicyEvaluationTimer(now, timeToRun.When);
			return false;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x000412DC File Offset: 0x0003F4DC
		private bool TryRunPendingOperations(DateTime now)
		{
			bool flag = false;
			int count = base.PendingOperations.Count;
			for (int i = 0; i < count; i++)
			{
				ThrottlerAsyncResult pendingOperationToRun = base.PendingOperations.Peek();
				if (!this.TryAcquireLockInternal(pendingOperationToRun.Name, now))
				{
					return flag;
				}
				flag = true;
				TraceSourceBase<ThrottlerTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Removing operation '{0}' from the pendig operations queue", new object[] { pendingOperationToRun.Name });
				base.PendingOperations.Dequeue();
				AsyncInvoker.InvokeMethodAsynchronously(delegate
				{
					pendingOperationToRun.SignalCompletion(false);
				}, WaitOrNot.DontWait, "Throttler.TryRunPendingOperations");
			}
			return flag;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0004137C File Offset: 0x0003F57C
		protected override void OnCurrentlyRunningOperationEnded(ThrottlerBase.ThrottlerWorkTicket ticket)
		{
			if (ticket.IsWatched)
			{
				try
				{
					this.m_runningOperationsExpirationDetector.StopWatching(ticket);
				}
				catch (ShutdownSequenceStartedException)
				{
				}
			}
			object locker = base.Locker;
			lock (locker)
			{
				DateTime utcNow = DateTime.UtcNow;
				this.m_throttlingPolicy.OnOperationCompleted(utcNow);
				if (!base.Active)
				{
					return;
				}
				this.TryRunPendingOperations(utcNow);
			}
			base.Notifications.OnStateChanged(base.PendingOperations.Count, this.m_throttlingPolicy.CurrentlyRunningOperations);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00041420 File Offset: 0x0003F620
		public override void Stop()
		{
			this.m_timerFactory.Stop();
			this.m_runningOperationsExpirationDetector.Stop();
			base.Stop();
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0004143E File Offset: 0x0003F63E
		public override void WaitForStopToComplete()
		{
			this.m_timerFactory.WaitForStopToComplete();
			this.m_runningOperationsExpirationDetector.WaitForStopToComplete();
			base.WaitForStopToComplete();
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0004145C File Offset: 0x0003F65C
		public override void Shutdown()
		{
			this.m_timerFactory.Shutdown();
			this.m_timerFactory = null;
			this.m_runningOperationsExpirationDetector.Shutdown();
			this.m_runningOperationsExpirationDetector = null;
			base.WaitForStopToComplete();
		}

		// Token: 0x040006EB RID: 1771
		private IThrottlingPolicy m_throttlingPolicy;

		// Token: 0x040006EC RID: 1772
		private TimerFactory m_timerFactory;

		// Token: 0x040006ED RID: 1773
		private OneShotTimer m_scheduledPolicyEvaluationTimer;

		// Token: 0x040006EE RID: 1774
		private DateTime m_scheduledPolicyEvaluationTime;

		// Token: 0x040006EF RID: 1775
		private ExpirationDetector m_runningOperationsExpirationDetector;

		// Token: 0x040006F0 RID: 1776
		private bool m_failSlowOnDeadlockDetection;

		// Token: 0x040006F1 RID: 1777
		private ThrottlerBase.ThrottlerWorkTicket m_expiredWorkTicket;

		// Token: 0x040006F2 RID: 1778
		internal const string c_throttlerExpirationDetectorEnabledTweakName = "Microsoft.Cloud.Platform.Utils.Throttler.ExpirationDetectorEnabled";

		// Token: 0x040006F3 RID: 1779
		private Tweak<bool> sm_expirationDetectionEnabledTweak = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.Throttler.ExpirationDetectorEnabled", "When set, throttler enables expiration detection", false);

		// Token: 0x040006F4 RID: 1780
		private static TimeSpan c_defaultExpirationTimeout = TimeSpan.FromMilliseconds(3600000.0);
	}
}
