using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x02000136 RID: 310
	internal static class AsyncHelper
	{
		// Token: 0x0600108E RID: 4238 RVA: 0x00039660 File Offset: 0x00037860
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsTaskInTerminalState(this Task task)
		{
			TaskStatus status = task.Status;
			return status - TaskStatus.RanToCompletion <= 2;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00039680 File Offset: 0x00037880
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

		// Token: 0x06001090 RID: 4240 RVA: 0x000396D0 File Offset: 0x000378D0
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

		// Token: 0x06001091 RID: 4241 RVA: 0x00039750 File Offset: 0x00037950
		public static void WaitForTaskCompletion(this Task task)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, CancellationToken.None);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0003975E File Offset: 0x0003795E
		public static void WaitForTaskCompletion(this Task task, int timeout)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, CancellationToken.None);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0003976C File Offset: 0x0003796C
		public static void WaitForTaskCompletion(this Task task, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, cancelToken);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00039776 File Offset: 0x00037976
		public static void WaitForTaskCompletion(this Task task, int timeout, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, cancelToken);
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00039780 File Offset: 0x00037980
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, CancellationToken.None);
			return task.Result;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00039794 File Offset: 0x00037994
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task, int timeout)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, CancellationToken.None);
			return task.Result;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000397A8 File Offset: 0x000379A8
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, cancelToken);
			return task.Result;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000397B8 File Offset: 0x000379B8
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task, int timeout, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, cancelToken);
			return task.Result;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000397C8 File Offset: 0x000379C8
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

		// Token: 0x04000ABB RID: 2747
		private static Dictionary<Type, Task> completedTasks = new Dictionary<Type, Task>();
	}
}
