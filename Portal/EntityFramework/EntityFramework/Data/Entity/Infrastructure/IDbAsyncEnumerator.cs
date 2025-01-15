using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000246 RID: 582
	public interface IDbAsyncEnumerator : IDisposable
	{
		// Token: 0x06001EA9 RID: 7849
		Task<bool> MoveNextAsync(CancellationToken cancellationToken);

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001EAA RID: 7850
		object Current { get; }
	}
}
