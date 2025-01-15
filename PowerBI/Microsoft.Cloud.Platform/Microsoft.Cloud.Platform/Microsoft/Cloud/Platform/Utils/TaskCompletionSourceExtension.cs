using System;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200018C RID: 396
	internal static class TaskCompletionSourceExtension
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x00023650 File Offset: 0x00021850
		public static async Task SetResultAsync<T>(this TaskCompletionSource<T> tcs, T t)
		{
			await Task.Yield();
			tcs.SetResult(t);
		}
	}
}
