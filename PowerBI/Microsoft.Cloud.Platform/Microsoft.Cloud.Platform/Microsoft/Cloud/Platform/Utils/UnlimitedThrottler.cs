using System;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002B8 RID: 696
	public sealed class UnlimitedThrottler : IThrottler
	{
		// Token: 0x060012C4 RID: 4804 RVA: 0x000414E3 File Offset: 0x0003F6E3
		public IAsyncResult BeginTryAcquireLock(string id, AsyncCallback userCallback, object userContext)
		{
			return new CompletedAsyncResult<IDisposable>(userCallback, userContext, new EmptyDisposable());
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x000414F1 File Offset: 0x0003F6F1
		public IDisposable EndTryAcquireLock(IAsyncResult result)
		{
			return CompletedAsyncResult<IDisposable>.End(result);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x000414F9 File Offset: 0x0003F6F9
		public Task<IDisposable> TryAcquireLockAsync(string id)
		{
			return Task.FromResult<IDisposable>(new EmptyDisposable());
		}
	}
}
