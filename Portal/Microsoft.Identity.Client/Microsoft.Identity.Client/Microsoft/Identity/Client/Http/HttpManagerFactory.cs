using System;

namespace Microsoft.Identity.Client.Http
{
	// Token: 0x02000289 RID: 649
	internal sealed class HttpManagerFactory
	{
		// Token: 0x060018FA RID: 6394 RVA: 0x0005277B File Offset: 0x0005097B
		public static IHttpManager GetHttpManager(IMsalHttpClientFactory httpClientFactory, bool withRetry, bool isManagedIdentity)
		{
			if (!withRetry)
			{
				return new HttpManager(httpClientFactory);
			}
			if (!isManagedIdentity)
			{
				return new HttpManagerWithRetry(httpClientFactory);
			}
			return new HttpManagerManagedIdentity(httpClientFactory);
		}
	}
}
