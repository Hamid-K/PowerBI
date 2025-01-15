using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200014E RID: 334
	public sealed class AsyncTaskDelayer : IDisposable, IIdentifiable
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x0001E4BC File Offset: 0x0001C6BC
		public AsyncTaskDelayer(TimeSpan delayResolution, TimeSpan maximumDelay, string name)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			ExtendedDiagnostics.EnsureArgument(delayResolution, "delayResolution", delayResolution >= TimeSpan.FromMilliseconds(1.0));
			ExtendedDiagnostics.EnsureArgument(maximumDelay, "maximumDelay", maximumDelay > delayResolution);
			double num = maximumDelay.TotalMilliseconds / delayResolution.TotalMilliseconds;
			ExtendedDiagnostics.EnsureArgument(maximumDelay, "maximumDelay", num == (double)((int)num));
			this.Name = name;
			this.m_delayResolution = delayResolution;
			this.m_delayResolutionTotalMilliseconds = (int)delayResolution.TotalMilliseconds;
			this.m_maximumDelay = maximumDelay;
			this.m_maxBuckets = (int)num + 1;
			this.m_pendingTaskBuckets = new ConcurrentQueue<AsyncTaskDelayer.DelayedTask>[this.m_maxBuckets];
			for (int i = 0; i < this.m_maxBuckets; i++)
			{
				this.m_pendingTaskBuckets[i] = new ConcurrentQueue<AsyncTaskDelayer.DelayedTask>();
			}
			this.m_asyncDelayLoopTask = this.AsyncDelayLoop();
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001E594 File Offset: 0x0001C794
		public AsyncTaskDelayer.DelayedTask Delay(TimeSpan delay)
		{
			ExtendedDiagnostics.EnsureArgument(delay, "delay", delay >= this.m_delayResolution);
			ExtendedDiagnostics.EnsureArgument(delay, "delay", delay <= this.m_maximumDelay);
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(this.Name);
			}
			int num = (int)delay.TotalMilliseconds;
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
			AsyncTaskDelayer.DelayedTask delayedTask = new AsyncTaskDelayer.DelayedTask(taskCompletionSource);
			int num2 = (this.m_nextBucket + Math.Max(1, num / this.m_delayResolutionTotalMilliseconds)) % this.m_maxBuckets;
			this.m_pendingTaskBuckets[num2].Enqueue(delayedTask);
			if (this.m_disposed)
			{
				taskCompletionSource.SetResult(true);
			}
			return delayedTask;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001E63C File Offset: 0x0001C83C
		private async Task AsyncDelayLoop()
		{
			await Task.Yield();
			Stopwatch sw = new Stopwatch();
			TimeSpan timeSpan = TimeSpan.FromMilliseconds(0.0);
			while (!this.m_disposed)
			{
				sw.Restart();
				ConcurrentQueue<AsyncTaskDelayer.DelayedTask> concurrentQueue = this.m_pendingTaskBuckets[this.m_nextBucket];
				this.m_nextBucket = (this.m_nextBucket + 1) % this.m_maxBuckets;
				if (concurrentQueue.Count > 0)
				{
					this.ReleaseTasksInBucket(concurrentQueue);
				}
				sw.Stop();
				timeSpan += sw.Elapsed;
				if (timeSpan < this.m_delayResolution)
				{
					sw.Restart();
					TimeSpan nextDelay = this.m_delayResolution - timeSpan;
					await Task.Delay(nextDelay);
					sw.Stop();
					timeSpan = sw.Elapsed - nextDelay;
					nextDelay = default(TimeSpan);
				}
				else
				{
					timeSpan -= this.m_delayResolution;
				}
			}
			ConcurrentQueue<AsyncTaskDelayer.DelayedTask>[] pendingTaskBuckets = this.m_pendingTaskBuckets;
			for (int i = 0; i < pendingTaskBuckets.Length; i++)
			{
				this.ReleaseTasksInBucket(pendingTaskBuckets[i]);
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0001E684 File Offset: 0x0001C884
		private void ReleaseTasksInBucket(ConcurrentQueue<AsyncTaskDelayer.DelayedTask> bucket)
		{
			List<AsyncTaskDelayer.DelayedTask> list = new List<AsyncTaskDelayer.DelayedTask>(bucket.Count);
			AsyncTaskDelayer.DelayedTask delayedTask;
			while (bucket.TryDequeue(out delayedTask))
			{
				list.Add(delayedTask);
			}
			this.ReleaseTasks(list.ToArray()).DoNotWait();
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001E6C4 File Offset: 0x0001C8C4
		private async Task ReleaseTasks(AsyncTaskDelayer.DelayedTask[] tasksToRelease)
		{
			await Task.Yield();
			for (int i = 0; i < tasksToRelease.Length; i++)
			{
				tasksToRelease[i].TryCompleteTask();
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001E709 File Offset: 0x0001C909
		public void Dispose()
		{
			this.m_disposed = true;
			this.m_asyncDelayLoopTask.ExtendedWait();
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0001E71F File Offset: 0x0001C91F
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0001E727 File Offset: 0x0001C927
		public string Name { get; private set; }

		// Token: 0x0400034E RID: 846
		private readonly TimeSpan m_delayResolution;

		// Token: 0x0400034F RID: 847
		private readonly int m_delayResolutionTotalMilliseconds;

		// Token: 0x04000350 RID: 848
		private readonly TimeSpan m_maximumDelay;

		// Token: 0x04000351 RID: 849
		private readonly ConcurrentQueue<AsyncTaskDelayer.DelayedTask>[] m_pendingTaskBuckets;

		// Token: 0x04000352 RID: 850
		private readonly int m_maxBuckets;

		// Token: 0x04000353 RID: 851
		private volatile int m_nextBucket;

		// Token: 0x04000354 RID: 852
		private volatile bool m_disposed;

		// Token: 0x04000355 RID: 853
		private readonly Task m_asyncDelayLoopTask;

		// Token: 0x0200061B RID: 1563
		public sealed class DelayedTask : IDisposable
		{
			// Token: 0x06002C85 RID: 11397 RVA: 0x0009D99A File Offset: 0x0009BB9A
			internal DelayedTask(TaskCompletionSource<bool> taskCompletionSource)
			{
				ExtendedDiagnostics.EnsureNotNull<TaskCompletionSource<bool>>(taskCompletionSource, "taskCompletionSource");
				this.m_taskCompletionSource = taskCompletionSource;
			}

			// Token: 0x17000713 RID: 1811
			// (get) Token: 0x06002C86 RID: 11398 RVA: 0x0009D9B6 File Offset: 0x0009BBB6
			public Task<bool> Task
			{
				get
				{
					TaskCompletionSource<bool> taskCompletionSource = this.m_taskCompletionSource;
					if (taskCompletionSource == null)
					{
						throw new ObjectDisposedException("DelayedTask");
					}
					return taskCompletionSource.Task;
				}
			}

			// Token: 0x06002C87 RID: 11399 RVA: 0x0009D9D3 File Offset: 0x0009BBD3
			public void Dispose()
			{
				this.m_taskCompletionSource = null;
			}

			// Token: 0x06002C88 RID: 11400 RVA: 0x0009D9E0 File Offset: 0x0009BBE0
			internal void TryCompleteTask()
			{
				TaskCompletionSource<bool> taskCompletionSource = this.m_taskCompletionSource;
				if (taskCompletionSource != null && !taskCompletionSource.Task.IsCompleted)
				{
					taskCompletionSource.SetResult(true);
				}
			}

			// Token: 0x040010F1 RID: 4337
			private volatile TaskCompletionSource<bool> m_taskCompletionSource;
		}
	}
}
