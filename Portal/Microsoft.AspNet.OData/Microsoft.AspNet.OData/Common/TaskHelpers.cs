using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.OData.Common
{
	// Token: 0x02000067 RID: 103
	internal static class TaskHelpers
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x0000CD4C File Offset: 0x0000AF4C
		internal static Task Canceled()
		{
			return TaskHelpers.CancelCache<TaskHelpers.AsyncVoid>.Canceled;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000CD53 File Offset: 0x0000AF53
		internal static Task Completed()
		{
			return TaskHelpers._defaultCompleted;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000CD5A File Offset: 0x0000AF5A
		internal static Task FromError(Exception exception)
		{
			return TaskHelpers.FromError<TaskHelpers.AsyncVoid>(exception);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000CD62 File Offset: 0x0000AF62
		internal static Task<TResult> FromError<TResult>(Exception exception)
		{
			TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
			taskCompletionSource.SetException(exception);
			return taskCompletionSource.Task;
		}

		// Token: 0x040000C8 RID: 200
		private static readonly Task _defaultCompleted = Task.FromResult<TaskHelpers.AsyncVoid>(default(TaskHelpers.AsyncVoid));

		// Token: 0x02000203 RID: 515
		private struct AsyncVoid
		{
		}

		// Token: 0x02000204 RID: 516
		private static class CancelCache<TResult>
		{
			// Token: 0x0600102B RID: 4139 RVA: 0x000404D5 File Offset: 0x0003E6D5
			private static Task<TResult> GetCancelledTask()
			{
				TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
				taskCompletionSource.SetCanceled();
				return taskCompletionSource.Task;
			}

			// Token: 0x040004B2 RID: 1202
			public static readonly Task<TResult> Canceled = TaskHelpers.CancelCache<TResult>.GetCancelledTask();
		}
	}
}
