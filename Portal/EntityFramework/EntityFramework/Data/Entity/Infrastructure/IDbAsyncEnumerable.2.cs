using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000245 RID: 581
	public interface IDbAsyncEnumerable<out T> : IDbAsyncEnumerable
	{
		// Token: 0x06001EA8 RID: 7848
		IDbAsyncEnumerator<T> GetAsyncEnumerator();
	}
}
