using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004ED RID: 1261
	public interface IRouter
	{
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x0600265E RID: 9822
		string Identifier { get; }

		// Token: 0x0600265F RID: 9823
		IAsyncResult BeginCreateRequest(object[] keys, AsyncCallback callback, object state);

		// Token: 0x06002660 RID: 9824
		IRouterRequest EndCreateRequest(IAsyncResult result);
	}
}
