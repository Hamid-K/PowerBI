using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000299 RID: 665
	public class AsyncSemaphore
	{
		// Token: 0x060011E7 RID: 4583 RVA: 0x0003E7CC File Offset: 0x0003C9CC
		public AsyncSemaphore(int initialCount)
		{
			Ensure.ArgIsNotNegative((long)initialCount, "initialCount", 0);
			this.m_waiters = new Queue<TaskCompletionSource<bool>>();
			this.m_currentCount = initialCount;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0003E7F3 File Offset: 0x0003C9F3
		internal int CurrentCount
		{
			get
			{
				return this.m_currentCount;
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0003E7FC File Offset: 0x0003C9FC
		public Task WaitAsync()
		{
			Queue<TaskCompletionSource<bool>> waiters = this.m_waiters;
			Task task;
			lock (waiters)
			{
				if (this.m_currentCount > 0)
				{
					this.m_currentCount--;
					task = AsyncSemaphore.s_completed;
				}
				else
				{
					TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
					this.m_waiters.Enqueue(taskCompletionSource);
					task = taskCompletionSource.Task;
				}
			}
			return task;
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0003E870 File Offset: 0x0003CA70
		public void Release()
		{
			TaskCompletionSource<bool> taskCompletionSource = null;
			Queue<TaskCompletionSource<bool>> waiters = this.m_waiters;
			lock (waiters)
			{
				if (this.m_waiters.Count > 0)
				{
					taskCompletionSource = this.m_waiters.Dequeue();
				}
				else
				{
					this.m_currentCount++;
				}
			}
			if (taskCompletionSource != null)
			{
				taskCompletionSource.SetResult(true);
			}
		}

		// Token: 0x040006AC RID: 1708
		private static Task s_completed = ExtendedTask.FromResult();

		// Token: 0x040006AD RID: 1709
		private Queue<TaskCompletionSource<bool>> m_waiters;

		// Token: 0x040006AE RID: 1710
		private int m_currentCount;
	}
}
