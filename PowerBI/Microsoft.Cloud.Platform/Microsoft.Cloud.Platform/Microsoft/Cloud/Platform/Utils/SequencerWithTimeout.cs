using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000287 RID: 647
	public sealed class SequencerWithTimeout : ISequencer
	{
		// Token: 0x06001161 RID: 4449 RVA: 0x0003C994 File Offset: 0x0003AB94
		public SequencerWithTimeout([NotNull] Sequencer sequencer, TimeSpan timeout)
			: this(sequencer, timeout, SequencerWithTimeoutBehavior.ThrowOnTimeout)
		{
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0003C9A0 File Offset: 0x0003ABA0
		public SequencerWithTimeout(Sequencer sequencer, TimeSpan timeout, SequencerWithTimeoutBehavior timeoutBehavior)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer>(sequencer, "sequencer");
			ExtendedDiagnostics.EnsureArgument("timeout", timeout > TimeSpan.Zero || (int)timeout.TotalMilliseconds == -1, "Timeout may not be negative");
			this.m_sequencer = sequencer;
			this.m_timeout = timeout;
			this.m_timeoutBehavior = timeoutBehavior;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0003CA00 File Offset: 0x0003AC00
		public IAsyncResult BeginExecute(AsyncCallback callback, object context)
		{
			this.m_completionCallback = new PendingCallbackSlim(new Action<PendingCallbackReason>(this.OnPendingCallbackCompleted));
			this.m_asyncResult = new VoidAsyncResult(callback, context);
			this.m_completionCallback.ScheduleTimer((int)this.m_timeout.TotalMilliseconds);
			AsyncInvoker.InvokeMethodAsynchronously(delegate
			{
				this.m_synchronousCompletionException = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
				{
					this.m_sequencerAsyncResult = this.m_sequencer.BeginExecute(delegate(IAsyncResult ar)
					{
						if (!ar.CompletedSynchronously)
						{
							this.OnSequencerCompleted(ar);
						}
					}, null);
				});
				if (this.m_sequencerAsyncResult != null && this.m_sequencerAsyncResult.CompletedSynchronously)
				{
					this.OnSequencerCompleted(this.m_sequencerAsyncResult);
				}
				if (this.m_synchronousCompletionException != null)
				{
					this.m_completionCallback.InvokeCallback(PendingCallbackReason.Completed);
				}
			}, WaitOrNot.DontWait, "Invoke Inner Sequencer.BeginExecute()");
			return this.m_asyncResult;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0003CA66 File Offset: 0x0003AC66
		public void EndExecute(IAsyncResult asyncResult)
		{
			this.m_asyncResult.End();
			if (!this.m_completedWithTimeout)
			{
				this.m_sequencer.EndExecute(this.m_sequencerAsyncResult);
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0003CA8C File Offset: 0x0003AC8C
		public void MarkFlowAsStopped()
		{
			this.m_sequencer.MarkFlowAsStopped();
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0003CA99 File Offset: 0x0003AC99
		public Sequencer Sequencer
		{
			get
			{
				return this.m_sequencer;
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0003CAA1 File Offset: 0x0003ACA1
		private void OnSequencerCompleted(IAsyncResult sequencerAsyncResult)
		{
			this.m_sequencerAsyncResult = sequencerAsyncResult;
			if (!this.m_completionCallback.InvokeCallback(PendingCallbackReason.Completed))
			{
				TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
				{
					this.m_sequencer.EndExecute(this.m_sequencerAsyncResult);
				});
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0003CACC File Offset: 0x0003ACCC
		private void OnPendingCallbackCompleted(PendingCallbackReason reason)
		{
			if (reason != PendingCallbackReason.Completed)
			{
				if (reason != PendingCallbackReason.TimeExpired)
				{
					return;
				}
				this.m_completedWithTimeout = true;
				this.m_sequencer.MarkFlowAsStopped();
				if (this.m_timeoutBehavior == SequencerWithTimeoutBehavior.CrashOnTimeout)
				{
					TraceDump traceDump = new TraceDump();
					this.m_sequencer.Dump(traceDump);
					try
					{
						ExtendedEnvironment.FailSlow(this, "Crashing due to SequencerTimeoutException. Sequencer: {0}".FormatWithInvariantCulture(new object[] { traceDump }));
						return;
					}
					catch (CrashException ex)
					{
						this.m_asyncResult.SignalCompletion(false, ex);
						return;
					}
				}
				this.m_asyncResult.SignalCompletion(false, new SequencerTimeoutException(this.m_sequencer, this.m_timeout));
				return;
			}
			else
			{
				if (this.m_synchronousCompletionException != null)
				{
					this.m_asyncResult.SignalCompletion(false, this.m_synchronousCompletionException);
					return;
				}
				this.m_asyncResult.SignalCompletion(false);
				return;
			}
		}

		// Token: 0x04000654 RID: 1620
		private Sequencer m_sequencer;

		// Token: 0x04000655 RID: 1621
		private TimeSpan m_timeout;

		// Token: 0x04000656 RID: 1622
		private PendingCallbackSlim m_completionCallback;

		// Token: 0x04000657 RID: 1623
		private VoidAsyncResult m_asyncResult;

		// Token: 0x04000658 RID: 1624
		private IAsyncResult m_sequencerAsyncResult;

		// Token: 0x04000659 RID: 1625
		private Exception m_synchronousCompletionException;

		// Token: 0x0400065A RID: 1626
		private bool m_completedWithTimeout;

		// Token: 0x0400065B RID: 1627
		private SequencerWithTimeoutBehavior m_timeoutBehavior;
	}
}
