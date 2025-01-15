using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000276 RID: 630
	public interface IReaderWriterSerializer<T> where T : IEquatable<T>
	{
		// Token: 0x060010BE RID: 4286
		IAsyncResult BeginAcquireReaderLock(T key, AsyncCallback callback, object state);

		// Token: 0x060010BF RID: 4287
		IDisposable EndAcquireReaderLock(IAsyncResult ar);

		// Token: 0x060010C0 RID: 4288
		IAsyncResult BeginAcquireWriterLock(T key, AsyncCallback callback, object state);

		// Token: 0x060010C1 RID: 4289
		IDisposable EndAcquireWriterLock(IAsyncResult ar);

		// Token: 0x060010C2 RID: 4290
		bool TryAcquireWriterLock(T key, out IDisposable lockHandle);

		// Token: 0x060010C3 RID: 4291
		bool TryAcquireReaderLock(T key, out IDisposable lockHandle);

		// Token: 0x060010C4 RID: 4292
		IDisposable DowngradeToReaderLock(T key, IDisposable lockHandle);
	}
}
