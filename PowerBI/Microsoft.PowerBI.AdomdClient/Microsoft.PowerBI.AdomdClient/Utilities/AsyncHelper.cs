using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000141 RID: 321
	internal static class AsyncHelper
	{
		// Token: 0x06000FF3 RID: 4083 RVA: 0x00036A2C File Offset: 0x00034C2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsTaskInTerminalState(this Task task)
		{
			TaskStatus status = task.Status;
			return status - TaskStatus.RanToCompletion <= 2;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00036A4C File Offset: 0x00034C4C
		public static void EnsureTaskInTerminalState(this Task task, CancellationTokenSource cts = null)
		{
			if (!task.IsTaskInTerminalState())
			{
				try
				{
					if (cts != null)
					{
						cts.Cancel();
					}
					task.GetAwaiter().GetResult();
				}
				catch (TaskCanceledException)
				{
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00036A9C File Offset: 0x00034C9C
		public static Task<TResult> GetCompletedTaskWithDefaultValue<TResult>()
		{
			Dictionary<Type, Task> dictionary = AsyncHelper.completedTasks;
			Task<TResult> task2;
			lock (dictionary)
			{
				Task task;
				if (!AsyncHelper.completedTasks.TryGetValue(typeof(TResult), out task))
				{
					task = Task.FromResult<TResult>(default(TResult));
					AsyncHelper.completedTasks.Add(typeof(TResult), task);
				}
				task2 = (Task<TResult>)task;
			}
			return task2;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00036B1C File Offset: 0x00034D1C
		public static void WaitForTaskCompletion(this Task task)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, CancellationToken.None);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00036B2A File Offset: 0x00034D2A
		public static void WaitForTaskCompletion(this Task task, int timeout)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, CancellationToken.None);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00036B38 File Offset: 0x00034D38
		public static void WaitForTaskCompletion(this Task task, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, cancelToken);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00036B42 File Offset: 0x00034D42
		public static void WaitForTaskCompletion(this Task task, int timeout, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, cancelToken);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00036B4C File Offset: 0x00034D4C
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, CancellationToken.None);
			return task.Result;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00036B60 File Offset: 0x00034D60
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task, int timeout)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, CancellationToken.None);
			return task.Result;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00036B74 File Offset: 0x00034D74
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, cancelToken);
			return task.Result;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00036B84 File Offset: 0x00034D84
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task, int timeout, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, cancelToken);
			return task.Result;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00036B94 File Offset: 0x00034D94
		private static void WaitForTaskCompletionImpl(Task task, int timeout, CancellationToken cancelToken)
		{
			try
			{
				if (!task.Wait(timeout, cancelToken))
				{
					throw new TimeoutException(RuntimeSR.AsyncHelper_TimeoutElapsed);
				}
			}
			catch (AggregateException ex)
			{
				ReadOnlyCollection<Exception> innerExceptions = ex.Flatten().InnerExceptions;
				if (innerExceptions != null && innerExceptions.Count > 0)
				{
					throw innerExceptions[0];
				}
				throw;
			}
		}

		// Token: 0x04000AF5 RID: 2805
		private static Dictionary<Type, Task> completedTasks = new Dictionary<Type, Task>();
	}
}
