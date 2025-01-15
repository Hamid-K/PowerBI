using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200027B RID: 635
	public class FailingRetrierV2 : RetrierBase
	{
		// Token: 0x060010F4 RID: 4340 RVA: 0x0003A7B9 File Offset: 0x000389B9
		public FailingRetrierV2(IFailingRetriableCommandV2 command, int millSecondsToWaitBetweenRetries, int maxRetries, bool shouldTraceAsyncSteps = false)
			: base(command.GetType().Name, millSecondsToWaitBetweenRetries, maxRetries, shouldTraceAsyncSteps)
		{
			this.Command = command;
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0003A7D7 File Offset: 0x000389D7
		public FailingRetrierV2(IFailingRetriableCommandV2 command, int[] millSecondsToWaitExponentiallyBetweenRetries, int maxRetries, bool shouldTraceAsyncSteps = false)
			: base(command.GetType().Name, millSecondsToWaitExponentiallyBetweenRetries, maxRetries, shouldTraceAsyncSteps)
		{
			this.Command = command;
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0003A7F5 File Offset: 0x000389F5
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x0003A7FD File Offset: 0x000389FD
		public IFailingRetriableCommandV2 Command { get; private set; }

		// Token: 0x060010F8 RID: 4344 RVA: 0x0003A806 File Offset: 0x00038A06
		protected override IAsyncResult BeginExecuteCommand(RetrierContext retrierContext, AsyncCallback callback, object userState)
		{
			return this.Command.BeginExecute(retrierContext, callback, userState);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0003A816 File Offset: 0x00038A16
		protected override void EndExecuteCommand(IAsyncResult ar)
		{
			this.Command.EndExecute(ar);
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0003A824 File Offset: 0x00038A24
		protected override async Task<bool> IsRetryRequired(Exception commandException)
		{
			bool flag;
			if (commandException == null)
			{
				flag = false;
			}
			else
			{
				TaskAwaiter<bool> taskAwaiter = this.Command.IsPermanentError(commandException, base.RetrierContext).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<bool> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (taskAwaiter.GetResult())
				{
					ExceptionDispatchInfo.Capture(commandException).Throw();
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0003A871 File Offset: 0x00038A71
		protected override Exception OnFunctionalFlowStepException(string step, Exception ex)
		{
			if (ex.IsFatal())
			{
				return ex;
			}
			return null;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0003A77A File Offset: 0x0003897A
		protected override Exception OnSleepFlowStepException(string step, Exception ex)
		{
			return this.OnFunctionalFlowStepException(step, ex);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0003A87E File Offset: 0x00038A7E
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
