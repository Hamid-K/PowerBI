using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004F0 RID: 1264
	public interface IRouterRequest
	{
		// Token: 0x06002664 RID: 9828
		IAsyncResult BeginGetNextEndpoints(IEnumerable<EndpointIdentifier> previousEndpoints, IEnumerable<EndpointFault> exceptions, AsyncCallback callback, object state);

		// Token: 0x06002665 RID: 9829
		IEnumerable<EndpointIdentifier> EndGetNextEndpoints(IAsyncResult asyncResult);

		// Token: 0x06002666 RID: 9830
		IAsyncResult BeginCompleteRequest(AsyncCallback callback, object state);

		// Token: 0x06002667 RID: 9831
		void EndCompleteRequest(IAsyncResult result);
	}
}
