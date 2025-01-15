using System;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002B1 RID: 689
	public interface IThrottler
	{
		// Token: 0x0600128B RID: 4747
		IAsyncResult BeginTryAcquireLock(string id, AsyncCallback userCallback, object userContext);

		// Token: 0x0600128C RID: 4748
		IDisposable EndTryAcquireLock(IAsyncResult result);

		// Token: 0x0600128D RID: 4749
		Task<IDisposable> TryAcquireLockAsync(string id);
	}
}
