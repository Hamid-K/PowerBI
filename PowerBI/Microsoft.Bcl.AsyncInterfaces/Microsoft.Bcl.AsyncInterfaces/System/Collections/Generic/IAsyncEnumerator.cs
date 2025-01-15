using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
	// Token: 0x02000015 RID: 21
	public interface IAsyncEnumerator<[Nullable(2)] out T> : IAsyncDisposable
	{
		// Token: 0x0600001E RID: 30
		ValueTask<bool> MoveNextAsync();

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31
		[Nullable(1)]
		T Current
		{
			[NullableContext(1)]
			get;
		}
	}
}
