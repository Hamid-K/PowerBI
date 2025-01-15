using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200027E RID: 638
	public class RetrierAsync
	{
		// Token: 0x06001109 RID: 4361 RVA: 0x0003AA3C File Offset: 0x00038C3C
		public RetrierAsync(Func<Task> retryAction, Func<Exception, RetrierContext, Task<bool>> isPermanentErrorCallback, int maxRetry, TimeSpan waitTime)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<Task>>(retryAction, "retryAction");
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<Exception, RetrierContext, Task<bool>>>(isPermanentErrorCallback, "isPermanentErrorCallback");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(maxRetry, "maxRetry");
			this.retryAction = retryAction;
			this.isPermanentErrorCallback = isPermanentErrorCallback;
			this.waitTime = waitTime;
			this.RetrierContext = new RetrierContext(maxRetry);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0003AA92 File Offset: 0x00038C92
		public RetrierAsync(Func<Task> retryAction, Func<Exception, RetrierContext, Task<bool>> isPermanentErrorCallback, int maxRetry, TimeSpan[] exponentialWaitTime)
			: this(retryAction, isPermanentErrorCallback, maxRetry, TimeSpan.MinValue)
		{
			ExtendedDiagnostics.EnsureArrayNotNullOrEmpty(exponentialWaitTime, "exponentialWaitTime");
			this.exponentialWaitTime = exponentialWaitTime;
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x0003AAB6 File Offset: 0x00038CB6
		public RetrierContext RetrierContext { get; }

		// Token: 0x0600110C RID: 4364 RVA: 0x0003AAC0 File Offset: 0x00038CC0
		public async Task Execute()
		{
			Exception e = null;
			object obj;
			for (;;)
			{
				int num = 0;
				int num2;
				try
				{
					e = null;
					RetrierContext retrierContext = this.RetrierContext;
					num2 = retrierContext.CurrentAttemptNumber + 1;
					retrierContext.CurrentAttemptNumber = num2;
					TraceSourceBase<UtilsTrace>.Tracer.TraceInformation("Attempt {0}; Max retry count {1}; NonFailureRetries {2}", new object[]
					{
						this.RetrierContext.CurrentAttemptNumber,
						this.RetrierContext.MaxAttemptsNumber,
						this.RetrierContext.NonFailureRetries
					});
					await this.retryAction();
				}
				catch (Exception obj)
				{
					num = 1;
				}
				num2 = num;
				if (num2 == 1)
				{
					Exception ex = (Exception)obj;
					if (ex.IsFatal())
					{
						Exception ex2 = obj as Exception;
						if (ex2 == null)
						{
							break;
						}
						ExceptionDispatchInfo.Capture(ex2).Throw();
					}
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("Error receieved; Exception: {0}", new object[] { ex.ToString() });
					TaskAwaiter<bool> taskAwaiter = this.isPermanentErrorCallback(ex, this.RetrierContext).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult() || this.RetrierContext.CurrentAttemptNumber > this.RetrierContext.MaxAttemptsNumber)
					{
						Exception ex3 = obj as Exception;
						if (ex3 == null)
						{
							goto Block_7;
						}
						ExceptionDispatchInfo.Capture(ex3).Throw();
					}
					e = ex;
					ex = null;
				}
				obj = null;
				if (e != null)
				{
					TimeSpan timeSpan = this.waitTime;
					if (this.exponentialWaitTime != null)
					{
						timeSpan = ((this.RetrierContext.CurrentAttemptNumber >= this.exponentialWaitTime.Length) ? this.exponentialWaitTime[this.exponentialWaitTime.Length - 1] : this.exponentialWaitTime[this.RetrierContext.CurrentAttemptNumber - 1]);
					}
					TraceSourceBase<UtilsTrace>.Tracer.TraceInformation("Sleep for {0} ms", new object[] { timeSpan.TotalMilliseconds });
					await Task.Delay(timeSpan);
				}
				if (e == null || this.RetrierContext.CurrentAttemptNumber > this.RetrierContext.MaxAttemptsNumber)
				{
					goto IL_0348;
				}
			}
			throw obj;
			Block_7:
			throw obj;
			IL_0348:
			if (e != null)
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceWarning("Retry failed");
			}
			else
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceInformation("Retry succeeded");
			}
		}

		// Token: 0x04000641 RID: 1601
		private readonly Func<Task> retryAction;

		// Token: 0x04000642 RID: 1602
		private readonly Func<Exception, RetrierContext, Task<bool>> isPermanentErrorCallback;

		// Token: 0x04000643 RID: 1603
		private readonly TimeSpan waitTime;

		// Token: 0x04000644 RID: 1604
		private readonly TimeSpan[] exponentialWaitTime;
	}
}
