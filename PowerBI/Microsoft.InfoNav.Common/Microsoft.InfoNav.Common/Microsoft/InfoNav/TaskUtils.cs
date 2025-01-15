using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Microsoft.InfoNav
{
	// Token: 0x0200002B RID: 43
	internal static class TaskUtils
	{
		// Token: 0x06000234 RID: 564 RVA: 0x00006FC0 File Offset: 0x000051C0
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

		// Token: 0x06000235 RID: 565 RVA: 0x00007004 File Offset: 0x00005204
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

		// Token: 0x06000236 RID: 566 RVA: 0x00007048 File Offset: 0x00005248
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

		// Token: 0x06000237 RID: 567 RVA: 0x00007088 File Offset: 0x00005288
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
	}
}
