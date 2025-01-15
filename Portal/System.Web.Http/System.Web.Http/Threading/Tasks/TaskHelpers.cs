using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200000B RID: 11
	internal static class TaskHelpers
	{
		// Token: 0x06000053 RID: 83 RVA: 0x0000311B File Offset: 0x0000131B
		internal static Task Canceled()
		{
			return TaskHelpers.CancelCache<TaskHelpers.AsyncVoid>.Canceled;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003122 File Offset: 0x00001322
		internal static Task<TResult> Canceled<TResult>()
		{
			return TaskHelpers.CancelCache<TResult>.Canceled;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003129 File Offset: 0x00001329
		internal static Task Completed()
		{
			return TaskHelpers._defaultCompleted;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003130 File Offset: 0x00001330
		internal static Task FromError(Exception exception)
		{
			return TaskHelpers.FromError<TaskHelpers.AsyncVoid>(exception);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003138 File Offset: 0x00001338
		internal static Task<TResult> FromError<TResult>(Exception exception)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			taskCompletionSource.SetException(exception);
			return taskCompletionSource.Task;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000314B File Offset: 0x0000134B
		internal static Task<object> NullResult()
		{
			return TaskHelpers._completedTaskReturningNull;
		}

		// Token: 0x0400000B RID: 11
		private static readonly Task _defaultCompleted = Task.FromResult<TaskHelpers.AsyncVoid>(default(TaskHelpers.AsyncVoid));

		// Token: 0x0400000C RID: 12
		private static readonly Task<object> _completedTaskReturningNull = Task.FromResult<object>(null);

		// Token: 0x0200018F RID: 399
		private struct AsyncVoid
		{
		}

		// Token: 0x02000190 RID: 400
		private static class CancelCache<TResult>
		{
			// Token: 0x06000A31 RID: 2609 RVA: 0x0001A5AD File Offset: 0x000187AD
			private static Task<TResult> GetCancelledTask()
			{
				TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
				taskCompletionSource.SetCanceled();
				return taskCompletionSource.Task;
			}

			// Token: 0x040002BA RID: 698
			public static readonly Task<TResult> Canceled = TaskHelpers.CancelCache<TResult>.GetCancelledTask();
		}
	}
}
