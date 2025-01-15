using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200000C RID: 12
	internal static class TaskHelpersExtensions
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003180 File Offset: 0x00001380
		internal static async Task<object> CastToObject(this Task task)
		{
			await task;
			return null;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000031C8 File Offset: 0x000013C8
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

		// Token: 0x0600005C RID: 92 RVA: 0x00003210 File Offset: 0x00001410
		internal static void ThrowIfFaulted(this Task task)
		{
			task.GetAwaiter().GetResult();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000322B File Offset: 0x0000142B
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
