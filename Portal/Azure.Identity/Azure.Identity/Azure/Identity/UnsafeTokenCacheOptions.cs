using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Identity
{
	// Token: 0x0200008A RID: 138
	public abstract class UnsafeTokenCacheOptions : TokenCachePersistenceOptions
	{
		// Token: 0x06000484 RID: 1156
		protected internal abstract Task TokenCacheUpdatedAsync(TokenCacheUpdatedArgs tokenCacheUpdatedArgs);

		// Token: 0x06000485 RID: 1157
		protected internal abstract Task<ReadOnlyMemory<byte>> RefreshCacheAsync();

		// Token: 0x06000486 RID: 1158 RVA: 0x0000DF5C File Offset: 0x0000C15C
		protected internal virtual async Task<TokenCacheData> RefreshCacheAsync(TokenCacheRefreshArgs args, CancellationToken cancellationToken = default(CancellationToken))
		{
			ReadOnlyMemory<byte> readOnlyMemory = await this.RefreshCacheAsync().ConfigureAwait(false);
			return new TokenCacheData(readOnlyMemory);
		}
	}
}
