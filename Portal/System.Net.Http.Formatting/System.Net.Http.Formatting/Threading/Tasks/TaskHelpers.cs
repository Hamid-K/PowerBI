using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200005B RID: 91
	internal static class TaskHelpers
	{
		// Token: 0x06000351 RID: 849 RVA: 0x0000C443 File Offset: 0x0000A643
		internal static Task Canceled()
		{
			return TaskHelpers.CancelCache<TaskHelpers.AsyncVoid>.Canceled;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000C44A File Offset: 0x0000A64A
		internal static Task<TResult> Canceled<TResult>()
		{
			return TaskHelpers.CancelCache<TResult>.Canceled;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000C451 File Offset: 0x0000A651
		internal static Task Completed()
		{
			return TaskHelpers._defaultCompleted;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000C458 File Offset: 0x0000A658
		internal static Task FromError(Exception exception)
		{
			return TaskHelpers.FromError<TaskHelpers.AsyncVoid>(exception);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000C460 File Offset: 0x0000A660
		internal static Task<TResult> FromError<TResult>(Exception exception)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			taskCompletionSource.SetException(exception);
			return taskCompletionSource.Task;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000C473 File Offset: 0x0000A673
		internal static Task<object> NullResult()
		{
			return TaskHelpers._completedTaskReturningNull;
		}

		// Token: 0x04000132 RID: 306
		private static readonly Task _defaultCompleted = Task.FromResult<TaskHelpers.AsyncVoid>(default(TaskHelpers.AsyncVoid));

		// Token: 0x04000133 RID: 307
		private static readonly Task<object> _completedTaskReturningNull = Task.FromResult<object>(null);

		// Token: 0x0200008E RID: 142
		private struct AsyncVoid
		{
		}

		// Token: 0x0200008F RID: 143
		private static class CancelCache<TResult>
		{
			// Token: 0x0600040D RID: 1037 RVA: 0x0000F16E File Offset: 0x0000D36E
			private static Task<TResult> GetCancelledTask()
			{
				TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
				taskCompletionSource.SetCanceled();
				return taskCompletionSource.Task;
			}

			// Token: 0x04000217 RID: 535
			public static readonly Task<TResult> Canceled = TaskHelpers.CancelCache<TResult>.GetCancelledTask();
		}
	}
}
