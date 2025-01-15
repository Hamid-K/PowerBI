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
		// Token: 0x06001000 RID: 4096 RVA: 0x00036D5C File Offset: 0x00034F5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsTaskInTerminalState(this Task task)
		{
			TaskStatus status = task.Status;
			return status - TaskStatus.RanToCompletion <= 2;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00036D7C File Offset: 0x00034F7C
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

		// Token: 0x06001002 RID: 4098 RVA: 0x00036DCC File Offset: 0x00034FCC
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

		// Token: 0x06001003 RID: 4099 RVA: 0x00036E4C File Offset: 0x0003504C
		public static void WaitForTaskCompletion(this Task task)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, CancellationToken.None);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00036E5A File Offset: 0x0003505A
		public static void WaitForTaskCompletion(this Task task, int timeout)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, CancellationToken.None);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00036E68 File Offset: 0x00035068
		public static void WaitForTaskCompletion(this Task task, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, cancelToken);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00036E72 File Offset: 0x00035072
		public static void WaitForTaskCompletion(this Task task, int timeout, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, cancelToken);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00036E7C File Offset: 0x0003507C
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, CancellationToken.None);
			return task.Result;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00036E90 File Offset: 0x00035090
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task, int timeout)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, CancellationToken.None);
			return task.Result;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00036EA4 File Offset: 0x000350A4
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, cancelToken);
			return task.Result;
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00036EB4 File Offset: 0x000350B4
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task, int timeout, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, cancelToken);
			return task.Result;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00036EC4 File Offset: 0x000350C4
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

		// Token: 0x04000B02 RID: 2818
		private static Dictionary<Type, Task> completedTasks = new Dictionary<Type, Task>();
	}
}
