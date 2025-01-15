using System;
using System.Net.Http;
using Microsoft.Identity.Client.Http;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001F9 RID: 505
	internal class SimpleHttpClientFactory : IMsalHttpClientFactory
	{
		// Token: 0x06001587 RID: 5511 RVA: 0x00047C92 File Offset: 0x00045E92
		private static HttpClient InitializeClient()
		{
			HttpClient httpClient = new HttpClient(new HttpClientHandler
			{
				UseDefaultCredentials = true
			});
			HttpClientConfig.ConfigureRequestHeadersAndSize(httpClient);
			return httpClient;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00047CAB File Offset: 0x00045EAB
		public HttpClient GetHttpClient()
		{
			return SimpleHttpClientFactory.s_httpClient.Value;
		}

		// Token: 0x040008E2 RID: 2274
		private static readonly Lazy<HttpClient> s_httpClient = new Lazy<HttpClient>(new Func<HttpClient>(SimpleHttpClientFactory.InitializeClient));
	}
}
