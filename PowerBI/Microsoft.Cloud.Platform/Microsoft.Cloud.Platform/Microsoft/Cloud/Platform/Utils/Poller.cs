using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200026D RID: 621
	public sealed class Poller : Sequencer
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00038D53 File Offset: 0x00036F53
		private static ITraceSource Trace
		{
			get
			{
				return TraceSourceBase<UtilsTrace>.Tracer;
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00038D5A File Offset: 0x00036F5A
		public Poller([NotNull] IPollingCommand command, TimeSpan delayInterval, TimeSpan timeout)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IPollingCommand>(command, "command");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(delayInterval, "delayInterval");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(delayInterval, "timeout");
			this.m_command = command;
			this.m_delayInterval = delayInterval;
			this.m_timeout = timeout;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00038D98 File Offset: 0x00036F98
		protected override IEnumerable<IFlowStep> Run()
		{
			this.m_startTime = ExtendedDateTime.UtcNow;
			while (!this.IsTimedOut())
			{
				yield return base.RunAsyncStep("Executing command {0}".FormatWithInvariantCulture(new object[] { this.m_command.Name }), new Sequencer.FlowStepExceptionHandler(this.PollerFlowStepExceptionHandler), new Sequencer.AsyncBeginFunction(this.m_command.BeginExecute), new Sequencer.AsyncEndFunction(this.m_command.EndExecute));
				if (!this.m_command.IsPollingAttemptRequired)
				{
					yield break;
				}
				if (this.m_delayInterval > TimeSpan.Zero)
				{
					if (this.WillTimeOut(this.m_delayInterval))
					{
						break;
					}
					Poller.Trace.TraceVerbose("Polling is still required for command '{0}'. Sleeping '{1}' seconds before next attempt.", new object[]
					{
						this.m_command.Name,
						this.m_delayInterval.TotalSeconds
					});
					yield return base.RunAsyncStep<TimeSpan>("Async sleep {0} seconds".FormatWithInvariantCulture(new object[] { this.m_delayInterval.TotalSeconds }), new Sequencer.FlowStepExceptionHandler(this.PollerFlowStepExceptionHandler), new Sequencer.AsyncBeginFunction<TimeSpan>(AsyncTimer.BeginSleep), new Sequencer.AsyncEndFunction(AsyncTimer.EndSleep), this.m_delayInterval);
				}
			}
			Poller.Trace.TraceWarning("Polling on command '{0}' has depleted due to timeout of '{1}' seconds.", new object[]
			{
				this.m_command.Name,
				this.m_timeout.TotalSeconds
			});
			this.m_command.OnPollingTimeoutDepletion(this.m_timeout);
			yield break;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00038DA8 File Offset: 0x00036FA8
		private Exception PollerFlowStepExceptionHandler(string step, Exception ex)
		{
			if (!this.m_command.IsTransientError(ex))
			{
				Poller.Trace.TraceError("Poller caught a non-transient error {0}, will not continue to poll command {1}", new object[]
				{
					ex.Message,
					this.m_command.Name
				});
				return ex;
			}
			Poller.Trace.TraceInformation("Poller caught a transient error {0}. will continue to poll command {1}", new object[]
			{
				ex.Message,
				this.m_command.Name
			});
			return null;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00038E1E File Offset: 0x0003701E
		private bool IsTimedOut()
		{
			return this.WillTimeOut(TimeSpan.Zero);
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00038E2C File Offset: 0x0003702C
		private bool WillTimeOut(TimeSpan period)
		{
			if (this.m_timeout == TimeSpan.MaxValue || this.m_timeout == TimeConstants.InfiniteTimeSpan)
			{
				return false;
			}
			DateTime dateTime;
			try
			{
				dateTime = this.m_startTime + this.m_timeout;
			}
			catch (ArgumentOutOfRangeException)
			{
				return false;
			}
			return ExtendedDateTime.UtcNow + period > dateTime;
		}

		// Token: 0x0400061D RID: 1565
		private readonly IPollingCommand m_command;

		// Token: 0x0400061E RID: 1566
		private readonly TimeSpan m_delayInterval;

		// Token: 0x0400061F RID: 1567
		private readonly TimeSpan m_timeout;

		// Token: 0x04000620 RID: 1568
		private DateTime m_startTime;
	}
}
