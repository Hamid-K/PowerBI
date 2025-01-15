using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000064 RID: 100
	internal sealed class SingleRunningTaskManager : IDisposable
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x00008F24 File Offset: 0x00007124
		internal Task Queue(Func<CancellationToken, Task> task)
		{
			SingleRunningTaskManager.<>c__DisplayClass3_0 CS$<>8__locals1 = new SingleRunningTaskManager.<>c__DisplayClass3_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.task = task;
			if (Interlocked.Read(ref this.m_disposedState) == 1L)
			{
				throw new ObjectDisposedException("SingleRunningTaskManager");
			}
			CS$<>8__locals1.currentTokenSource = new CancellationTokenSource();
			CS$<>8__locals1.currentToken = CS$<>8__locals1.currentTokenSource.Token;
			CancellationTokenSource cancellationTokenSource = Interlocked.Exchange<CancellationTokenSource>(ref this.m_runningTokenSource, CS$<>8__locals1.currentTokenSource);
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			return Task.Factory.StartNew<Task>(delegate
			{
				SingleRunningTaskManager.<>c__DisplayClass3_0.<<Queue>b__0>d <<Queue>b__0>d;
				<<Queue>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<Queue>b__0>d.<>4__this = CS$<>8__locals1;
				<<Queue>b__0>d.<>1__state = -1;
				<<Queue>b__0>d.<>t__builder.Start<SingleRunningTaskManager.<>c__DisplayClass3_0.<<Queue>b__0>d>(ref <<Queue>b__0>d);
				return <<Queue>b__0>d.<>t__builder.Task;
			}, CS$<>8__locals1.currentToken, TaskCreationOptions.LongRunning | TaskCreationOptions.DenyChildAttach, TaskScheduler.Default).Unwrap();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00008FBE File Offset: 0x000071BE
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00008FC8 File Offset: 0x000071C8
		private void Dispose(bool disposing)
		{
			if (disposing && Interlocked.CompareExchange(ref this.m_disposedState, 1L, 0L) == 0L)
			{
				CancellationTokenSource cancellationTokenSource = Interlocked.Exchange<CancellationTokenSource>(ref this.m_runningTokenSource, null);
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
				Task.Run(delegate
				{
					this.m_semaphore.Wait();
					this.m_semaphore.Dispose();
				});
			}
		}

		// Token: 0x04000138 RID: 312
		private readonly SemaphoreSlim m_semaphore = new SemaphoreSlim(1, 1);

		// Token: 0x04000139 RID: 313
		private long m_disposedState;

		// Token: 0x0400013A RID: 314
		private CancellationTokenSource m_runningTokenSource;
	}
}
