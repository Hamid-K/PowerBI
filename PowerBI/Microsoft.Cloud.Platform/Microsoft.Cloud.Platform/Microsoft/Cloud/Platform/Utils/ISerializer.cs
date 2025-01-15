using System;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000289 RID: 649
	public interface ISerializer<T>
	{
		// Token: 0x06001177 RID: 4471
		IAsyncResult BeginAcquireLock(T key, int timeoutInMilliSeconds, AsyncCallback callback, object state);

		// Token: 0x06001178 RID: 4472
		IAsyncResult BeginAcquireLock(T key, AsyncCallback callback, object state);

		// Token: 0x06001179 RID: 4473
		IDisposable EndAcquireLock(IAsyncResult ar);

		// Token: 0x0600117A RID: 4474
		bool TryAcquireLock(T key, out IDisposable lockHandle);

		// Token: 0x0600117B RID: 4475
		Task<IDisposable> AcquireLockAsync(T key);

		// Token: 0x0600117C RID: 4476
		Task<IDisposable> AcquireLockAsync(T key, int timeoutInMilliseconds);
	}
}
