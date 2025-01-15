using System;
using System.Threading.Tasks;

namespace Azure.Identity
{
	// Token: 0x02000065 RID: 101
	internal class InMemoryTokenCacheOptions : UnsafeTokenCacheOptions
	{
		// Token: 0x0600038C RID: 908 RVA: 0x0000AD64 File Offset: 0x00008F64
		protected internal override Task<ReadOnlyMemory<byte>> RefreshCacheAsync()
		{
			return Task.FromResult<ReadOnlyMemory<byte>>(default(ReadOnlyMemory<byte>));
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000AD7F File Offset: 0x00008F7F
		protected internal override Task TokenCacheUpdatedAsync(TokenCacheUpdatedArgs tokenCacheUpdatedArgs)
		{
			return Task.CompletedTask;
		}
	}
}
