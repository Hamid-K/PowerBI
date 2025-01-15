using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Microsoft.Identity.Client.Http
{
	// Token: 0x02000287 RID: 647
	internal static class HttpClientConfig
	{
		// Token: 0x060018E8 RID: 6376 RVA: 0x000523B9 File Offset: 0x000505B9
		public static void ConfigureRequestHeadersAndSize(HttpClient httpClient)
		{
			httpClient.MaxResponseContentBufferSize = 1048576L;
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		// Token: 0x04000B43 RID: 2883
		public const long MaxResponseContentBufferSizeInBytes = 1048576L;

		// Token: 0x04000B44 RID: 2884
		public const int MaxConnections = 50;

		// Token: 0x04000B45 RID: 2885
		public static readonly TimeSpan ConnectionLifeTime = TimeSpan.FromMinutes(1.0);
	}
}
