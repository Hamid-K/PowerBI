using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000248 RID: 584
	public interface IDbAsyncEnumerator<out T> : IDbAsyncEnumerator, IDisposable
	{
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001EAD RID: 7853
		T Current { get; }
	}
}
