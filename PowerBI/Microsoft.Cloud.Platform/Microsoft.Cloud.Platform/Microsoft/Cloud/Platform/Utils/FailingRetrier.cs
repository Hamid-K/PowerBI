using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200027A RID: 634
	public class FailingRetrier : RetrierBase
	{
		// Token: 0x060010EA RID: 4330 RVA: 0x0003A6D3 File Offset: 0x000388D3
		public FailingRetrier(IFailingRetriableCommand command, TimeSpan timeToWaitBetweenRetries, int maxRetries)
			: base(command.GetType().Name, (int)timeToWaitBetweenRetries.TotalMilliseconds, maxRetries, false)
		{
			this.Command = command;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0003A6F7 File Offset: 0x000388F7
		public FailingRetrier(IFailingRetriableCommand command, TimeSpan timeToWaitBetweenRetries, int maxRetries, bool shouldTraceAsyncSteps)
			: base(command.GetType().Name, (int)timeToWaitBetweenRetries.TotalMilliseconds, maxRetries, shouldTraceAsyncSteps)
		{
			this.Command = command;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x0003A71C File Offset: 0x0003891C
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x0003A724 File Offset: 0x00038924
		public IFailingRetriableCommand Command { get; private set; }

		// Token: 0x060010EE RID: 4334 RVA: 0x0003A72D File Offset: 0x0003892D
		protected override IAsyncResult BeginExecuteCommand(RetrierContext retrierContext, AsyncCallback callback, object userState)
		{
			return this.Command.BeginExecute(retrierContext, callback, userState);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0003A73D File Offset: 0x0003893D
		protected override void EndExecuteCommand(IAsyncResult ar)
		{
			this.Command.EndExecute(ar);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0003A74B File Offset: 0x0003894B
		protected override Task<bool> IsRetryRequired(Exception commandException)
		{
			return Task.FromResult<bool>(commandException != null && !this.Command.IsPermanentError(commandException));
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0003A767 File Offset: 0x00038967
		protected override Exception OnFunctionalFlowStepException(string step, Exception ex)
		{
			if (this.Command.IsPermanentError(ex))
			{
				return ex;
			}
			return null;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0003A77A File Offset: 0x0003897A
		protected override Exception OnSleepFlowStepException(string step, Exception ex)
		{
			return this.OnFunctionalFlowStepException(step, ex);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0003A784 File Offset: 0x00038984
		protected override void OnRetryDepleted(Exception commandException)
		{
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "Maximum number of retries reached for command {0}, throwing the last temporary exception {1}.", new object[]
			{
				this.Command.GetType(),
				commandException
			});
			ExceptionDispatchInfo.Capture(commandException).Throw();
		}
	}
}
