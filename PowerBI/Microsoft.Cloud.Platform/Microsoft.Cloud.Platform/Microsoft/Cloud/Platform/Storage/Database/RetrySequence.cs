using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000042 RID: 66
	internal abstract class RetrySequence : Sequencer
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00005F7C File Offset: 0x0000417C
		protected RetrySequence(IThrottler throttler, DatabaseCommand command, DatabaseMonitoringContext monitoringContext)
		{
			this.m_throttler = throttler;
			this.Command = command;
			this.m_monitoringContext = monitoringContext;
			this.m_retryProfile = command.Specification.RetryProfile.QueryRetryProfile;
			this.m_tries = 0;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005FB6 File Offset: 0x000041B6
		protected sealed override IEnumerable<IFlowStep> Run()
		{
			RetrySequence.<>c__DisplayClass6_0 CS$<>8__locals1 = new RetrySequence.<>c__DisplayClass6_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.h = null;
			if (this.m_throttler != null)
			{
				this.m_monitoringContext.NotifyThrottleBegin();
				yield return base.RunAsyncStep<string>("Database query Throttle {0}".FormatWithInvariantCulture(new object[] { this.m_monitoringContext.Scope }), new Sequencer.AsyncBeginFunction<string>(this.m_throttler.BeginTryAcquireLock), delegate(IAsyncResult ar)
				{
					CS$<>8__locals1.h = CS$<>8__locals1.<>4__this.m_throttler.EndTryAcquireLock(ar);
				}, this.m_monitoringContext.Scope);
				if (CS$<>8__locals1.h == null)
				{
					DatabaseThrottlerOverflowException ex2 = new DatabaseThrottlerOverflowException();
					this.m_monitoringContext.NotifyError(ex2);
					throw ex2;
				}
				this.m_monitoringContext.NotifyThrottleCompleted();
			}
			using (CS$<>8__locals1.h)
			{
				RetrySequence.<>c__DisplayClass6_1 CS$<>8__locals2 = new RetrySequence.<>c__DisplayClass6_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				do
				{
					CS$<>8__locals2.retry = false;
					OpenConnectionSequence openConnectionSequence = new OpenConnectionSequence(this.m_monitoringContext, this.Command);
					yield return base.RunAsyncStep("Connect to {0}".FormatWithInvariantCulture(new object[] { this.m_monitoringContext.Scope }), new Sequencer.AsyncBeginFunction(openConnectionSequence.BeginExecute), new Sequencer.AsyncEndFunction(openConnectionSequence.EndExecute));
					this.m_monitoringContext.NotifyRequestBegin();
					RetrySequence.<>c__DisplayClass6_2 CS$<>8__locals3 = new RetrySequence.<>c__DisplayClass6_2();
					CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
					CS$<>8__locals3.dc = new DisposeController(this.Command.Connection);
					try
					{
						string name = base.GetType().Name;
						Sequencer.FlowStepExceptionHandler flowStepExceptionHandler;
						if ((flowStepExceptionHandler = CS$<>8__locals3.CS$<>8__locals2.<>9__1) == null)
						{
							flowStepExceptionHandler = (CS$<>8__locals3.CS$<>8__locals2.<>9__1 = delegate(string step, Exception ex)
							{
								CS$<>8__locals3.CS$<>8__locals2.retry = CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.ShouldRetry(ex);
								if (CS$<>8__locals3.CS$<>8__locals2.retry)
								{
									CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_monitoringContext.NotifyRequestRetry(ex);
									return null;
								}
								return ex;
							});
						}
						yield return base.RunAsyncStep(name, flowStepExceptionHandler, new Sequencer.AsyncBeginFunction(this.BeginQuery), delegate(IAsyncResult ar)
						{
							if (CS$<>8__locals3.CS$<>8__locals2.CS$<>8__locals1.<>4__this.EndQuery(ar))
							{
								CS$<>8__locals3.dc.PreventDispose();
							}
							WaitHandle asyncWaitHandle = ar.AsyncWaitHandle;
							if (asyncWaitHandle != null)
							{
								asyncWaitHandle.Close();
							}
						});
					}
					finally
					{
						if (CS$<>8__locals3.dc != null)
						{
							((IDisposable)CS$<>8__locals3.dc).Dispose();
						}
					}
					CS$<>8__locals3 = null;
					this.m_monitoringContext.NotifyRequestComplete();
					if (CS$<>8__locals2.retry)
					{
						this.m_tries++;
						this.Command.Recycle();
						int num = Math.Min(this.m_retryProfile.IntervalInMilliseconds * this.m_tries, 60000) + Randomizer.GetI32(1, 32);
						yield return base.RunAsyncStep<int>("Sleep {0}ms between retries (QueryRetrySequence)".FormatWithInvariantCulture(new object[] { num }), new Sequencer.AsyncBeginFunction<int>(AsyncTimer.BeginSleep), new Sequencer.AsyncEndFunction(AsyncTimer.EndSleep), num);
					}
				}
				while (CS$<>8__locals2.retry);
				CS$<>8__locals2 = null;
			}
			IDisposable disposable = null;
			yield break;
			yield break;
		}

		// Token: 0x060001A0 RID: 416
		protected abstract IAsyncResult BeginQuery(AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060001A1 RID: 417
		protected abstract bool EndQuery(IAsyncResult asyncResult);

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00005FC6 File Offset: 0x000041C6
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00005FCE File Offset: 0x000041CE
		private protected DatabaseCommand Command { protected get; private set; }

		// Token: 0x060001A4 RID: 420 RVA: 0x00005FD8 File Offset: 0x000041D8
		private bool ShouldRetry(Exception ex)
		{
			bool flag = false;
			if (this.m_tries < this.m_retryProfile.RetryCount)
			{
				SqlException ex2 = ex as SqlException;
				if (ex2 != null)
				{
					int errorCode = DatabaseErrors.GetErrorCode(ex2);
					if (RetrySequence.s_errorsToRetryOn.Contains(errorCode))
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x040000B7 RID: 183
		private readonly IThrottler m_throttler;

		// Token: 0x040000B8 RID: 184
		private readonly DatabaseMonitoringContext m_monitoringContext;

		// Token: 0x040000B9 RID: 185
		private readonly DatabaseRetryProfile m_retryProfile;

		// Token: 0x040000BA RID: 186
		private int m_tries;

		// Token: 0x040000BB RID: 187
		private static readonly HashSet<int> s_errorsToRetryOn = new HashSet<int>(new int[]
		{
			1205, 40197, 40501, 10053, 10054, 10060, 40613, 40143, 40501, 40540,
			233, 64, 20, 10928, 10929
		});
	}
}
