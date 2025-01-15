using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200007F RID: 127
	internal class TaskTimerInternal : IDisposable
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00011DCE File Offset: 0x0000FFCE
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00011DD6 File Offset: 0x0000FFD6
		public TimeSpan Delay
		{
			get
			{
				return this.delay;
			}
			set
			{
				if ((value <= TimeSpan.Zero || value.TotalMilliseconds > 2147483647.0) && value != TaskTimerInternal.InfiniteTimeSpan)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.delay = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00011E16 File Offset: 0x00010016
		public bool IsStarted
		{
			get
			{
				return this.tokenSource != null;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00011E24 File Offset: 0x00010024
		public void Start(Func<Task> elapsed)
		{
			TaskTimerInternal.<>c__DisplayClass8_0 CS$<>8__locals1 = new TaskTimerInternal.<>c__DisplayClass8_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.elapsed = elapsed;
			CS$<>8__locals1.newTokenSource = new CancellationTokenSource();
			Task.Delay(this.Delay, CS$<>8__locals1.newTokenSource.Token).ContinueWith<Task>(delegate(Task previousTask)
			{
				TaskTimerInternal.<>c__DisplayClass8_0.<<Start>b__0>d <<Start>b__0>d;
				<<Start>b__0>d.<>4__this = CS$<>8__locals1;
				<<Start>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<Start>b__0>d.<>1__state = -1;
				AsyncTaskMethodBuilder <>t__builder = <<Start>b__0>d.<>t__builder;
				<>t__builder.Start<TaskTimerInternal.<>c__DisplayClass8_0.<<Start>b__0>d>(ref <<Start>b__0>d);
				return <<Start>b__0>d.<>t__builder.Task;
			}, CancellationToken.None, TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			TaskTimerInternal.CancelAndDispose(Interlocked.Exchange<CancellationTokenSource>(ref this.tokenSource, CS$<>8__locals1.newTokenSource));
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00011E9D File Offset: 0x0001009D
		public void Cancel()
		{
			TaskTimerInternal.CancelAndDispose(Interlocked.Exchange<CancellationTokenSource>(ref this.tokenSource, null));
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00011EB0 File Offset: 0x000100B0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00011EC0 File Offset: 0x000100C0
		private static void LogException(Exception exception)
		{
			AggregateException ex = exception as AggregateException;
			if (ex != null)
			{
				ex = ex.Flatten();
				foreach (Exception ex2 in ex.InnerExceptions)
				{
					CoreEventSource.Log.LogError(ex2.ToInvariantString(), "Incorrect");
				}
			}
			CoreEventSource.Log.LogError(exception.ToInvariantString(), "Incorrect");
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00011F44 File Offset: 0x00010144
		private static void CancelAndDispose(CancellationTokenSource tokenSource)
		{
			if (tokenSource != null)
			{
				tokenSource.Cancel();
				tokenSource.Dispose();
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00011F55 File Offset: 0x00010155
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Cancel();
			}
		}

		// Token: 0x0400019B RID: 411
		public static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);

		// Token: 0x0400019C RID: 412
		private TimeSpan delay = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400019D RID: 413
		private CancellationTokenSource tokenSource;
	}
}
