using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x02000025 RID: 37
	internal static class AsyncHelper
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00005CCC File Offset: 0x00003ECC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsTaskInTerminalState(this Task task)
		{
			TaskStatus status = task.Status;
			return status - TaskStatus.RanToCompletion <= 2;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005CEC File Offset: 0x00003EEC
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

		// Token: 0x06000112 RID: 274 RVA: 0x00005D3C File Offset: 0x00003F3C
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

		// Token: 0x06000113 RID: 275 RVA: 0x00005DBC File Offset: 0x00003FBC
		public static Task GetCanceledTask(CancellationToken cancellationToken)
		{
			return new Task(AsyncHelper.cancelTaskCallback, cancellationToken);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005DC9 File Offset: 0x00003FC9
		public static Task<TResult> GetCanceledTask<TResult>(CancellationToken cancellationToken)
		{
			return new Task<TResult>(AsyncHelper.GetCancelTaskCallback<TResult>(), cancellationToken);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005DD6 File Offset: 0x00003FD6
		public static void WaitForTaskCompletion(this Task task)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, CancellationToken.None);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public static void WaitForTaskCompletion(this Task task, int timeout)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, CancellationToken.None);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005DF2 File Offset: 0x00003FF2
		public static void WaitForTaskCompletion(this Task task, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, cancelToken);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005DFC File Offset: 0x00003FFC
		public static void WaitForTaskCompletion(this Task task, int timeout, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, cancelToken);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005E06 File Offset: 0x00004006
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, CancellationToken.None);
			return task.Result;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005E1A File Offset: 0x0000401A
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task, int timeout)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, CancellationToken.None);
			return task.Result;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005E2E File Offset: 0x0000402E
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, -1, cancelToken);
			return task.Result;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005E3E File Offset: 0x0000403E
		public static TResult WaitForTaskCompletionAndGetResult<TResult>(this Task<TResult> task, int timeout, CancellationToken cancelToken)
		{
			AsyncHelper.WaitForTaskCompletionImpl(task, timeout, cancelToken);
			return task.Result;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005E50 File Offset: 0x00004050
		private static Func<TResult> GetCancelTaskCallback<TResult>()
		{
			Dictionary<Type, Delegate> dictionary = AsyncHelper.cancelTaskCallbacks;
			Func<TResult> func;
			lock (dictionary)
			{
				Delegate @delegate;
				if (!AsyncHelper.cancelTaskCallbacks.TryGetValue(typeof(TResult), out @delegate))
				{
					@delegate = new Func<TResult>(delegate
					{
						throw new OperationCanceledException();
					});
					AsyncHelper.cancelTaskCallbacks.Add(typeof(TResult), @delegate);
				}
				func = (Func<TResult>)@delegate;
			}
			return func;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005EE0 File Offset: 0x000040E0
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

		// Token: 0x040000BF RID: 191
		private static Dictionary<Type, Task> completedTasks = new Dictionary<Type, Task>();

		// Token: 0x040000C0 RID: 192
		private static Dictionary<Type, Delegate> cancelTaskCallbacks = new Dictionary<Type, Delegate>();

		// Token: 0x040000C1 RID: 193
		private static Action cancelTaskCallback = delegate
		{
			throw new OperationCanceledException();
		};
	}
}
