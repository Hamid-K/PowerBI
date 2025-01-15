using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000005 RID: 5
	internal static class TaskHelpersExtensions
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002324 File Offset: 0x00000524
		internal static async Task<object> CastToObject(this Task task)
		{
			await task;
			return null;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000236C File Offset: 0x0000056C
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

		// Token: 0x06000015 RID: 21 RVA: 0x000023B4 File Offset: 0x000005B4
		internal static void ThrowIfFaulted(this Task task)
		{
			task.GetAwaiter().GetResult();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023CF File Offset: 0x000005CF
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
