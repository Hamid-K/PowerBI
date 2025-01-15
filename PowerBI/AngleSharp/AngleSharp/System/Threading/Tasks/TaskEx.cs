using System;
using System.Collections.Generic;

namespace System.Threading.Tasks
{
	// Token: 0x02000005 RID: 5
	internal static class TaskEx
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002380 File Offset: 0x00000580
		public static Task WhenAll(params Task[] tasks)
		{
			return Task.WhenAll(tasks);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002388 File Offset: 0x00000588
		public static Task Run(Action action, CancellationToken cancel)
		{
			return Task.Run(action, cancel);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002391 File Offset: 0x00000591
		public static Task Delay(int millisecondsDelay, CancellationToken cancel)
		{
			return Task.Delay(millisecondsDelay, cancel);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000239A File Offset: 0x0000059A
		public static Task WhenAll(IEnumerable<Task> tasks)
		{
			return Task.WhenAll(tasks);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023A2 File Offset: 0x000005A2
		public static Task<TResult> FromResult<TResult>(TResult result)
		{
			return Task.FromResult<TResult>(result);
		}
	}
}
