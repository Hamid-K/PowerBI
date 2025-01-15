using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200005C RID: 92
	internal static class TaskHelpersExtensions
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		internal static async Task<object> CastToObject(this Task task)
		{
			await task;
			return null;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		internal static async Task<object> CastToObject<T>(this Task<T> task)
		{
			TaskAwaiter<T> taskAwaiter = task.GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<T> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<T>);
			}
			return taskAwaiter.GetResult();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000C538 File Offset: 0x0000A738
		internal static void ThrowIfFaulted(this Task task)
		{
			task.GetAwaiter().GetResult();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000C553 File Offset: 0x0000A753
		internal static bool TryGetResult<TResult>(this Task<TResult> task, out TResult result)
		{
			if (task.Status == TaskStatus.RanToCompletion)
			{
				result = task.Result;
				return true;
			}
			result = default(TResult);
			return false;
		}
	}
}
