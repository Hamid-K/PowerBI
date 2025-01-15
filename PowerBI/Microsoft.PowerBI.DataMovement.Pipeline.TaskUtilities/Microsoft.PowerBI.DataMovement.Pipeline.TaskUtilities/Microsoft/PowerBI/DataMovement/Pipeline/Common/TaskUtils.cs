using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.ExceptionUtilities;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000007 RID: 7
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class TaskUtils
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020B3 File Offset: 0x000002B3
		internal static void ExtendedWait(this Task task)
		{
			task.ExtendedWait(TimeSpan.FromMilliseconds(-1.0));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020CC File Offset: 0x000002CC
		internal static bool ExtendedWait(this Task task, TimeSpan timeout)
		{
			bool flag;
			try
			{
				flag = task.Wait(timeout);
			}
			catch (AggregateException ex)
			{
				ExceptionDispatchInfo exceptionDispatchInfo = ex.Unwrap();
				exceptionDispatchInfo.Throw();
				throw exceptionDispatchInfo.SourceException;
			}
			return flag;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002108 File Offset: 0x00000308
		internal static bool ExtendedWait(this Task task, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			bool flag;
			try
			{
				flag = task.Wait(millisecondsTimeout, cancellationToken);
			}
			catch (AggregateException ex)
			{
				ExceptionDispatchInfo exceptionDispatchInfo = ex.Unwrap();
				exceptionDispatchInfo.Throw();
				throw exceptionDispatchInfo.SourceException;
			}
			return flag;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002144 File Offset: 0x00000344
		internal static T ExtendedResult<[global::System.Runtime.CompilerServices.Nullable(2)] T>(this Task<T> task)
		{
			T result;
			try
			{
				result = task.Result;
			}
			catch (AggregateException ex)
			{
				ExceptionDispatchInfo exceptionDispatchInfo = ex.Unwrap();
				exceptionDispatchInfo.Throw();
				throw exceptionDispatchInfo.SourceException;
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000217C File Offset: 0x0000037C
		internal static void DoNotWait(this Task task)
		{
			task.ContinueWith(delegate(Task t)
			{
				if (t.Exception != null)
				{
					TraceSourceBase<TaskUtilsTraceSource>.Tracer.TraceError("TaskUtils.DoNotWait error: \n{0}", new object[] { t.Exception });
				}
			}, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B0 File Offset: 0x000003B0
		public static IAsyncResult ToApm<[global::System.Runtime.CompilerServices.Nullable(2)] TResult>(this Task<TResult> task, AsyncCallback callback, object state)
		{
			TaskUtils.<>c__DisplayClass5_0<TResult> CS$<>8__locals1 = new TaskUtils.<>c__DisplayClass5_0<TResult>();
			CS$<>8__locals1.task = task;
			CS$<>8__locals1.callback = callback;
			CS$<>8__locals1.tcs = new TaskCompletionSource<TResult>(state);
			CS$<>8__locals1.task.ContinueWith<Task>(delegate
			{
				TaskUtils.<>c__DisplayClass5_0<TResult>.<<ToApm>b__0>d <<ToApm>b__0>d;
				<<ToApm>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<ToApm>b__0>d.<>4__this = CS$<>8__locals1;
				<<ToApm>b__0>d.<>1__state = -1;
				<<ToApm>b__0>d.<>t__builder.Start<TaskUtils.<>c__DisplayClass5_0<TResult>.<<ToApm>b__0>d>(ref <<ToApm>b__0>d);
				return <<ToApm>b__0>d.<>t__builder.Task;
			}, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);
			return CS$<>8__locals1.tcs.Task;
		}
	}
}
