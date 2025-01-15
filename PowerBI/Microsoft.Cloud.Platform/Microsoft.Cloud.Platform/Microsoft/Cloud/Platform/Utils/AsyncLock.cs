using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200029A RID: 666
	public class AsyncLock
	{
		// Token: 0x060011EB RID: 4587 RVA: 0x0003E8E0 File Offset: 0x0003CAE0
		public AsyncLock()
		{
			this.m_semaphore = new AsyncSemaphore(1);
			this.m_releaser = ExtendedTask.FromResult<AsyncLock.AsyncLockReleaser>(new AsyncLock.AsyncLockReleaser(this));
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x0003E905 File Offset: 0x0003CB05
		internal bool IsHeld
		{
			get
			{
				return this.m_semaphore.CurrentCount == 0;
			}
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0003E918 File Offset: 0x0003CB18
		public Task<AsyncLock.AsyncLockReleaser> AcquireAsync()
		{
			Task task2 = this.m_semaphore.WaitAsync();
			if (!task2.IsCompleted)
			{
				return task2.ContinueWith<AsyncLock.AsyncLockReleaser>((Task task) => new AsyncLock.AsyncLockReleaser(this), CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			}
			return this.m_releaser;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0003E964 File Offset: 0x0003CB64
		public void SyncAcquireAndInvoke(Action action)
		{
			using (this.AcquireAsync().ExtendedResult<AsyncLock.AsyncLockReleaser>())
			{
				action();
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0003E9A4 File Offset: 0x0003CBA4
		public Task AcquireAndInvokeAsync(Action action)
		{
			TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
			this.AcquireAsync().Then(delegate(Task<AsyncLock.AsyncLockReleaser> acquireLockTask1)
			{
				TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, action);
				acquireLockTask1.Result.Dispose();
				tcs.SetWhenTaskEnds(acquireLockTask1, null, true);
			});
			return tcs.Task;
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0003E9F4 File Offset: 0x0003CBF4
		public Task AcquireAndInvokeAsync(Func<Task> taskFactory)
		{
			TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
			this.AcquireAsync().Then(delegate(Task<AsyncLock.AsyncLockReleaser> acquireLockTask1)
			{
				Task functionTask = null;
				Exception ex = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
				{
					functionTask = taskFactory();
				});
				if (ex != null)
				{
					acquireLockTask1.Result.Dispose();
					tcs.TrySetException(ex);
					return;
				}
				tcs.SetWhenTaskEnds(functionTask, new Action(acquireLockTask1.Result.Dispose));
			});
			return tcs.Task;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0003EA44 File Offset: 0x0003CC44
		public Task<TResult> AcquireAndInvokeAsync<TResult>(Func<Task<TResult>> taskFactory)
		{
			TaskCompletionSource<TResult> tcs = new TaskCompletionSource<TResult>();
			this.AcquireAsync().Then(delegate(Task<AsyncLock.AsyncLockReleaser> acquireLockTask1)
			{
				Task<TResult> functionTask = null;
				Exception ex = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
				{
					functionTask = taskFactory();
				});
				if (ex != null)
				{
					acquireLockTask1.Result.Dispose();
					tcs.TrySetException(ex);
					return;
				}
				tcs.SetWhenTaskEnds(functionTask, new Action(acquireLockTask1.Result.Dispose));
			});
			return tcs.Task;
		}

		// Token: 0x040006AF RID: 1711
		private AsyncSemaphore m_semaphore;

		// Token: 0x040006B0 RID: 1712
		private Task<AsyncLock.AsyncLockReleaser> m_releaser;

		// Token: 0x02000749 RID: 1865
		public struct AsyncLockReleaser : IDisposable
		{
			// Token: 0x06003006 RID: 12294 RVA: 0x000A4D3B File Offset: 0x000A2F3B
			internal AsyncLockReleaser(AsyncLock toRelease)
			{
				this.m_toRelease = toRelease;
			}

			// Token: 0x06003007 RID: 12295 RVA: 0x000A4D44 File Offset: 0x000A2F44
			public void Dispose()
			{
				if (this.m_toRelease != null)
				{
					this.m_toRelease.m_semaphore.Release();
					this.m_toRelease = null;
				}
			}

			// Token: 0x0400157B RID: 5499
			private AsyncLock m_toRelease;
		}
	}
}
