using System;
using System.Threading.Tasks;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000082 RID: 130
	internal static class TaskHelper
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x000101CC File Offset: 0x0000E3CC
		internal static Task<T> FromException<T>(Exception ex)
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetException(ex);
			return taskCompletionSource.Task;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000101DF File Offset: 0x0000E3DF
		internal static Task<T> FromCancellation<T>()
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetCanceled();
			return taskCompletionSource.Task;
		}
	}
}
