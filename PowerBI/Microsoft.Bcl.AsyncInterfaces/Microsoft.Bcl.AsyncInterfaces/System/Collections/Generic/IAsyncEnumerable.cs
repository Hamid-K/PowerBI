using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	public interface IAsyncEnumerable<[Nullable(2)] out T>
	{
		// Token: 0x0600001D RID: 29
		IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken));
	}
}
