using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Builder
{
	// Token: 0x02000044 RID: 68
	internal class NotFound
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000704C File Offset: 0x0000524C
		private static Task CreateCompletedTask()
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			tcs.SetResult(null);
			return tcs.Task;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000706C File Offset: 0x0000526C
		public Task Invoke(IDictionary<string, object> env)
		{
			env["owin.ResponseStatusCode"] = 404;
			return NotFound.Completed;
		}

		// Token: 0x0400007D RID: 125
		private static readonly Task Completed = NotFound.CreateCompletedTask();
	}
}
