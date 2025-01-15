using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000446 RID: 1094
	internal interface ISessionListener : IListener, ITransportObject
	{
		// Token: 0x0600264F RID: 9807
		IInputSession Accept();

		// Token: 0x06002650 RID: 9808
		IInputSession Accept(TimeSpan timeout);

		// Token: 0x06002651 RID: 9809
		IAsyncResult BeginAccept(AsyncCallback callback, object state);

		// Token: 0x06002652 RID: 9810
		IAsyncResult BeginAccept(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06002653 RID: 9811
		IInputSession EndAccept(IAsyncResult ar);
	}
}
