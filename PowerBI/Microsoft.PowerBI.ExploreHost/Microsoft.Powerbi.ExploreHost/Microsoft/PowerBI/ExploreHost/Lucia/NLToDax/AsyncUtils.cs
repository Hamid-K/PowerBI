using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.ExploreHost.Lucia.NLToDax
{
	// Token: 0x02000073 RID: 115
	internal static class AsyncUtils
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0000A431 File Offset: 0x00008631
		internal static Task<T> AsCancellable<T>(Func<Task<T>> taskFactory, CancellationToken cancellationToken)
		{
			return Task.WhenAny<T>(new Task<T>[]
			{
				Task.Run<T>(taskFactory, cancellationToken),
				AsyncUtils.WhenCancelled<T>(cancellationToken)
			}).Unwrap<T>();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000A458 File Offset: 0x00008658
		private static async Task<T> WhenCancelled<T>(CancellationToken cancellationToken)
		{
			await Task.Delay(-1, cancellationToken);
			return default(T);
		}
	}
}
