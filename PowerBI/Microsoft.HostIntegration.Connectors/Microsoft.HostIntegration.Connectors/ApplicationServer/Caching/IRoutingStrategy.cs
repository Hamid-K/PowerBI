using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200000E RID: 14
	internal interface IRoutingStrategy : IDisposable
	{
		// Token: 0x0600006D RID: 109
		IEnumerable<EndpointID> GetHostEndpoints(string cacheName);

		// Token: 0x0600006E RID: 110
		ResponseBody SendMessageAndWait(RequestBody request, out IRequestTracker tracker);

		// Token: 0x0600006F RID: 111
		ResponseBody SendMultiMessageAndWaitForAll(MultiRequest multiRequest, out IRequestTracker tracker);
	}
}
