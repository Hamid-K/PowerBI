using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200001D RID: 29
	[NullableContext(1)]
	[Nullable(0)]
	public static class TaskAsyncEnumerableExtensions
	{
		// Token: 0x06000038 RID: 56 RVA: 0x0000232F File Offset: 0x0000052F
		public static ConfiguredAsyncDisposable ConfigureAwait(this IAsyncDisposable source, bool continueOnCapturedContext)
		{
			return new ConfiguredAsyncDisposable(source, continueOnCapturedContext);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002338 File Offset: 0x00000538
		[return: Nullable(new byte[] { 0, 1 })]
		public static ConfiguredCancelableAsyncEnumerable<T> ConfigureAwait<[Nullable(2)] T>(this IAsyncEnumerable<T> source, bool continueOnCapturedContext)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(source, continueOnCapturedContext, default(CancellationToken));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002355 File Offset: 0x00000555
		[return: Nullable(new byte[] { 0, 1 })]
		public static ConfiguredCancelableAsyncEnumerable<T> WithCancellation<[Nullable(2)] T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(source, true, cancellationToken);
		}
	}
}
