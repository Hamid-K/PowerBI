using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000279 RID: 633
	public abstract class RetrierBase : Sequencer
	{
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0003A5A5 File Offset: 0x000387A5
		public int NumberOfPerformedAttempts
		{
			get
			{
				return this.m_retrierContext.CurrentAttemptNumber;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x0003A5B2 File Offset: 0x000387B2
		protected RetrierContext RetrierContext
		{
			get
			{
				return this.m_retrierContext;
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003A5BC File Offset: 0x000387BC
		protected RetrierBase([NotNull] string commandName, int millisecondsToWaitBetweenRetries, int maxRetries, bool shouldTraceAsyncSteps = false)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(commandName, "commandName");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(millisecondsToWaitBetweenRetries, "millisecondsToWaitBetweenRetries");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(maxRetries, "maxRetries");
			this.m_commandName = commandName;
			this.m_millisecondsToWaitBetweenRetries = millisecondsToWaitBetweenRetries;
			this.m_retrierContext = new RetrierContext(maxRetries);
			base.ShouldTraceAsyncSteps = shouldTraceAsyncSteps;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0003A614 File Offset: 0x00038814
		protected RetrierBase([NotNull] string commandName, int[] millisecondsToWaitExponentiallyBetweenRetries, int maxRetries, bool shouldTraceAsyncSteps = false)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(commandName, "commandName");
			ExtendedDiagnostics.EnsureArrayNotNullOrEmpty(millisecondsToWaitExponentiallyBetweenRetries, "millisecondsToWaitExponentiallyBetweenRetries");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(maxRetries, "maxRetries");
			this.m_commandName = commandName;
			this.m_millisecondsToWaitExponentiallyBetweenRetries = millisecondsToWaitExponentiallyBetweenRetries;
			this.m_retrierContext = new RetrierContext(maxRetries);
			base.ShouldTraceAsyncSteps = shouldTraceAsyncSteps;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0003A66A File Offset: 0x0003886A
		protected override IEnumerable<IFlowStep> Run()
		{
			RetrierBase.<>c__DisplayClass11_0 CS$<>8__locals1 = new RetrierBase.<>c__DisplayClass11_0();
			CS$<>8__locals1.<>4__this = this;
			int numberOfRemainingAttempts = this.m_retrierContext.MaxAttemptsNumber;
			CS$<>8__locals1.commandException = null;
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Retrying {0} with max attempts {1}", new object[] { this.m_commandName, numberOfRemainingAttempts });
			while (numberOfRemainingAttempts >= 0)
			{
				RetrierBase.<>c__DisplayClass11_1 CS$<>8__locals2 = new RetrierBase.<>c__DisplayClass11_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				RetrierContext retrierContext = this.m_retrierContext;
				int num = retrierContext.CurrentAttemptNumber;
				retrierContext.CurrentAttemptNumber = num + 1;
				RetrierContext retrierContext2 = new RetrierContext(this.m_retrierContext);
				yield return base.RunAsyncStep<RetrierContext>("Executing command " + this.m_commandName, new Sequencer.FlowStepExceptionHandler(this.OnFunctionalFlowStepException), new Sequencer.AsyncBeginFunction<RetrierContext>(this.BeginExecuteCommand), new Sequencer.AsyncEndFunction(this.EndExecuteCommand), retrierContext2);
				CS$<>8__locals2.CS$<>8__locals1.commandException = base.LastStepExceptionUnwrapped;
				CS$<>8__locals2.isRetryRequired = false;
				yield return base.RunAsyncStep("Is retry required", null, delegate
				{
					RetrierBase.<>c__DisplayClass11_1.<<Run>b__0>d <<Run>b__0>d;
					<<Run>b__0>d.<>4__this = CS$<>8__locals2;
					<<Run>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<Run>b__0>d.<>1__state = -1;
					AsyncTaskMethodBuilder <>t__builder = <<Run>b__0>d.<>t__builder;
					<>t__builder.Start<RetrierBase.<>c__DisplayClass11_1.<<Run>b__0>d>(ref <<Run>b__0>d);
					return <<Run>b__0>d.<>t__builder.Task;
				});
				if (!CS$<>8__locals2.isRetryRequired)
				{
					break;
				}
				int nextWaitTime = this.GetNextWaitTime();
				if (nextWaitTime > 0 && numberOfRemainingAttempts > 0)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Temprary error running command [{0}]. Sleeping [{1}] msec before next attempt.", new object[] { this.m_commandName, nextWaitTime });
					yield return base.RunAsyncStep<int>("Async sleep", new Sequencer.FlowStepExceptionHandler(this.OnSleepFlowStepException), new Sequencer.AsyncBeginFunction<int>(AsyncTimer.BeginSleep), new Sequencer.AsyncEndFunction(AsyncTimer.EndSleep), nextWaitTime);
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Retry running command [{0}]. Number of remaining attempts[{1}]", new object[] { this.m_commandName, numberOfRemainingAttempts });
				}
				if (numberOfRemainingAttempts != 2147483647)
				{
					num = numberOfRemainingAttempts;
					numberOfRemainingAttempts = num - 1;
				}
				CS$<>8__locals2 = null;
			}
			if (numberOfRemainingAttempts < 0)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Retrying on command {0} has depleted.", new object[] { this.m_commandName });
				this.OnRetryDepleted(CS$<>8__locals1.commandException);
			}
			yield break;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0003A67C File Offset: 0x0003887C
		private int GetNextWaitTime()
		{
			if (this.m_millisecondsToWaitExponentiallyBetweenRetries == null)
			{
				return this.m_millisecondsToWaitBetweenRetries;
			}
			if (this.RetrierContext.CurrentAttemptNumber < this.m_millisecondsToWaitExponentiallyBetweenRetries.Length)
			{
				return this.m_millisecondsToWaitExponentiallyBetweenRetries[this.RetrierContext.CurrentAttemptNumber - 1];
			}
			return this.m_millisecondsToWaitExponentiallyBetweenRetries[this.m_millisecondsToWaitExponentiallyBetweenRetries.Length - 1];
		}

		// Token: 0x060010E4 RID: 4324
		protected abstract IAsyncResult BeginExecuteCommand(RetrierContext retrierContext, AsyncCallback callback, object userState);

		// Token: 0x060010E5 RID: 4325
		protected abstract void EndExecuteCommand(IAsyncResult ar);

		// Token: 0x060010E6 RID: 4326
		protected abstract Task<bool> IsRetryRequired(Exception commandException);

		// Token: 0x060010E7 RID: 4327 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnRetryDepleted(Exception commandException)
		{
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0001819D File Offset: 0x0001639D
		protected virtual Exception OnFunctionalFlowStepException(string step, Exception ex)
		{
			return ex;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0001819D File Offset: 0x0001639D
		protected virtual Exception OnSleepFlowStepException(string step, Exception ex)
		{
			return ex;
		}

		// Token: 0x04000637 RID: 1591
		private readonly string m_commandName;

		// Token: 0x04000638 RID: 1592
		private readonly int m_millisecondsToWaitBetweenRetries;

		// Token: 0x04000639 RID: 1593
		private readonly int[] m_millisecondsToWaitExponentiallyBetweenRetries;

		// Token: 0x0400063A RID: 1594
		private readonly RetrierContext m_retrierContext;

		// Token: 0x0400063B RID: 1595
		private const int c_infiniteAttempts = 2147483647;
	}
}
