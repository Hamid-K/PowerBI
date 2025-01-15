using System;
using System.ServiceModel;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000417 RID: 1047
	internal interface ISharedCommunicationObject : ICommunicationObject
	{
		// Token: 0x06002461 RID: 9313
		IAsyncResult BeginDispose(AsyncCallback callback, object state);

		// Token: 0x06002462 RID: 9314
		IAsyncResult BeginDispose(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06002463 RID: 9315
		void EndDispose(IAsyncResult result);

		// Token: 0x06002464 RID: 9316
		void Dispose();

		// Token: 0x06002465 RID: 9317
		void Dispose(TimeSpan timeout);

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002466 RID: 9318
		bool IsDisposed { get; }
	}
}
