using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Throttling
{
	// Token: 0x02000104 RID: 260
	public sealed class AsyncTrafficSmoother : IAsyncTrafficSmoother
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x0001996C File Offset: 0x00017B6C
		public AsyncTrafficSmoother(TimeSpan smoothingInterval, int initiallyExpectedNumRequestsPerInterval)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(smoothingInterval, "smoothingInterval");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(initiallyExpectedNumRequestsPerInterval, "initiallyExpectedNumRequestsPerInterval");
			ExtendedDiagnostics.EnsureOperation(smoothingInterval >= this.c_subintervalFrequency, "Smoothing interval must be set to greater than 100ms. This is an empirically observed scale/perf limit for the smoother's subinterval frequency.");
			int num = Convert.ToInt32(smoothingInterval.TotalMilliseconds / this.c_subintervalFrequency.TotalMilliseconds);
			this.m_numWorkRequestsAllowedInCurrentSubinterval = (double)initiallyExpectedNumRequestsPerInterval / (double)num;
			this.m_numRequestsRunInSubinterval = new int[num];
			int[] numRequestsRunInSubinterval = this.m_numRequestsRunInSubinterval;
			int num2 = 0;
			this.m_numRequestsRunInInterval = initiallyExpectedNumRequestsPerInterval;
			numRequestsRunInSubinterval[num2] = initiallyExpectedNumRequestsPerInterval;
			this.m_currentSubintervalIndex++;
			this.ProcessNextSubinterval().DoNotWait();
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00019A30 File Offset: 0x00017C30
		public Task Smooth()
		{
			object @lock = this.m_lock;
			Task task;
			lock (@lock)
			{
				if (this.m_numWorkRequestsAllowedInCurrentSubinterval - (double)this.m_numRequestsRunInSubinterval[this.m_currentSubintervalIndex] >= 1.0)
				{
					this.m_numRequestsRunInSubinterval[this.m_currentSubintervalIndex]++;
					task = AsyncTrafficSmoother.c_completedTask;
				}
				else
				{
					TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
					this.m_queuedWorkRequests.Enqueue(taskCompletionSource);
					task = taskCompletionSource.Task;
				}
			}
			return task;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00019AC4 File Offset: 0x00017CC4
		private async Task ProcessNextSubinterval()
		{
			for (;;)
			{
				await Task.Delay(this.c_subintervalFrequency);
				object @lock = this.m_lock;
				lock (@lock)
				{
					this.m_numRequestsRunInInterval += this.m_numRequestsRunInSubinterval[this.m_currentSubintervalIndex];
					this.UpdateMovingAverage();
					this.m_currentSubintervalIndex = (this.m_currentSubintervalIndex + 1) % this.m_numRequestsRunInSubinterval.Length;
					this.m_numRequestsRunInInterval -= this.m_numRequestsRunInSubinterval[this.m_currentSubintervalIndex];
					this.m_numRequestsRunInSubinterval[this.m_currentSubintervalIndex] = 0;
					while (this.m_numWorkRequestsAllowedInCurrentSubinterval - (double)this.m_numRequestsRunInSubinterval[this.m_currentSubintervalIndex] >= 1.0 && this.m_queuedWorkRequests.Count > 0)
					{
						TaskCompletionSource<bool> taskCompletionSource = this.m_queuedWorkRequests.Dequeue();
						this.m_numRequestsRunInSubinterval[this.m_currentSubintervalIndex]++;
						this.ReleaseQueuedTask(taskCompletionSource).DoNotWait();
					}
					continue;
				}
				return;
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00019B0C File Offset: 0x00017D0C
		private void UpdateMovingAverage()
		{
			double num = (double)(this.m_queuedWorkRequests.Count + this.m_numRequestsRunInInterval) / (double)this.m_numRequestsRunInSubinterval.Length;
			this.m_fractionalWorkCount += num % 1.0;
			if (this.m_fractionalWorkCount >= 1.0)
			{
				num += 1.0;
				this.m_fractionalWorkCount -= 1.0;
			}
			if (num < this.m_numWorkRequestsAllowedInCurrentSubinterval)
			{
				num = (num + this.m_numWorkRequestsAllowedInCurrentSubinterval) / 2.0;
			}
			this.m_numWorkRequestsAllowedInCurrentSubinterval = num;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00019BA8 File Offset: 0x00017DA8
		private async Task ReleaseQueuedTask(TaskCompletionSource<bool> tcs)
		{
			await Task.Yield();
			tcs.SetResult(true);
		}

		// Token: 0x04000284 RID: 644
		private readonly TimeSpan c_subintervalFrequency = TimeSpan.FromMilliseconds(100.0);

		// Token: 0x04000285 RID: 645
		private static readonly Task c_completedTask = Task.FromResult<bool>(true);

		// Token: 0x04000286 RID: 646
		private readonly object m_lock = new object();

		// Token: 0x04000287 RID: 647
		private Queue<TaskCompletionSource<bool>> m_queuedWorkRequests = new Queue<TaskCompletionSource<bool>>();

		// Token: 0x04000288 RID: 648
		private double m_numWorkRequestsAllowedInCurrentSubinterval;

		// Token: 0x04000289 RID: 649
		private int m_currentSubintervalIndex;

		// Token: 0x0400028A RID: 650
		private int[] m_numRequestsRunInSubinterval;

		// Token: 0x0400028B RID: 651
		private int m_numRequestsRunInInterval;

		// Token: 0x0400028C RID: 652
		private double m_fractionalWorkCount;
	}
}
