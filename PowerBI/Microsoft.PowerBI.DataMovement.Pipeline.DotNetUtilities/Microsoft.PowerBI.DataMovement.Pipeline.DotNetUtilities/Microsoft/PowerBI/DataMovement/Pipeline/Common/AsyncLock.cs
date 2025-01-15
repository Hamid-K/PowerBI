using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AsyncLock : IDisposable
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021DA File Offset: 0x000003DA
		internal AsyncLock()
		{
			this.m_semaphore = new SemaphoreSlim(1);
			this.m_releaser = Task.FromResult<AsyncLock.Releaser>(new AsyncLock.Releaser(this));
			this.m_disposeCts = new CancellationTokenSource();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000220C File Offset: 0x0000040C
		internal Task<AsyncLock.Releaser> LockAsync()
		{
			Task task = this.m_semaphore.WaitAsync(this.m_disposeCts.Token);
			if (!task.IsCompleted)
			{
				return task.ContinueWith<AsyncLock.Releaser>(delegate(Task t, object state)
				{
					if (t.IsCanceled)
					{
						throw new ObjectDisposedException(typeof(AsyncLock).FullName);
					}
					if (t.IsFaulted)
					{
						ExceptionDispatchInfo.Capture(t.Exception).Throw();
						throw new NotImplementedException();
					}
					return new AsyncLock.Releaser((AsyncLock)state);
				}, this, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
			}
			if (this.m_disposeCts.IsCancellationRequested)
			{
				throw new ObjectDisposedException(typeof(AsyncLock).FullName);
			}
			return this.m_releaser;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002296 File Offset: 0x00000496
		public void Dispose()
		{
			if (!this.m_disposed)
			{
				this.m_disposeCts.Cancel();
				this.m_disposed = true;
			}
		}

		// Token: 0x04000011 RID: 17
		private readonly SemaphoreSlim m_semaphore;

		// Token: 0x04000012 RID: 18
		private readonly Task<AsyncLock.Releaser> m_releaser;

		// Token: 0x04000013 RID: 19
		private readonly CancellationTokenSource m_disposeCts;

		// Token: 0x04000014 RID: 20
		private bool m_disposed;

		// Token: 0x02000010 RID: 16
		[NullableContext(0)]
		internal struct Releaser : IDisposable
		{
			// Token: 0x0600001D RID: 29 RVA: 0x000024E0 File Offset: 0x000006E0
			[NullableContext(1)]
			internal Releaser(AsyncLock toRelease)
			{
				this.m_toRelease = toRelease;
			}

			// Token: 0x0600001E RID: 30 RVA: 0x000024E9 File Offset: 0x000006E9
			public void Dispose()
			{
				if (this.m_toRelease != null && !this.m_toRelease.m_disposed)
				{
					this.m_toRelease.m_semaphore.Release();
				}
			}

			// Token: 0x0400001A RID: 26
			[Nullable(1)]
			private readonly AsyncLock m_toRelease;
		}
	}
}
