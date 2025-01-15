using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Microsoft.DataShaping.Common
{
	// Token: 0x02000019 RID: 25
	internal static class TaskUtils
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00003E50 File Offset: 0x00002050
		internal static Task RunSynchronously(Action action)
		{
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
			try
			{
				action();
				taskCompletionSource.SetResult(null);
			}
			catch (Exception ex)
			{
				taskCompletionSource.SetException(ex);
			}
			return taskCompletionSource.Task;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003E94 File Offset: 0x00002094
		internal static Task<TResult> RunSynchronously<TResult>(Func<TResult> func)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			try
			{
				taskCompletionSource.SetResult(func());
			}
			catch (Exception ex)
			{
				taskCompletionSource.SetException(ex);
			}
			return taskCompletionSource.Task;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003ED8 File Offset: 0x000020D8
		internal static void WaitAndUnwrap(this Task task)
		{
			try
			{
				task.Wait();
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException != null)
				{
					ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				}
				throw;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003F18 File Offset: 0x00002118
		internal static T WaitAndUnwrapResult<T>(this Task<T> task)
		{
			T result;
			try
			{
				result = task.Result;
			}
			catch (AggregateException ex)
			{
				if (ex.InnerException != null)
				{
					ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				}
				throw;
			}
			return result;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003F5C File Offset: 0x0000215C
		internal static Task CreateCompletedTask()
		{
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
			taskCompletionSource.SetResult(null);
			return taskCompletionSource.Task;
		}

		// Token: 0x04000047 RID: 71
		internal static Task CompletedTask = TaskUtils.CreateCompletedTask();
	}
}
