using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004EF RID: 1263
	public interface IRouterPolicy
	{
		// Token: 0x06002661 RID: 9825
		IEnumerable<EndpointIdentifier> GetNextEndpoints(IEnumerable<EndpointIdentifier> potentialEndpoints, bool singleResult);

		// Token: 0x06002662 RID: 9826
		IAsyncResult BeginCompleteRequest(AsyncCallback callback, object state);

		// Token: 0x06002663 RID: 9827
		void EndCompleteRequest(IAsyncResult result);
	}
}
