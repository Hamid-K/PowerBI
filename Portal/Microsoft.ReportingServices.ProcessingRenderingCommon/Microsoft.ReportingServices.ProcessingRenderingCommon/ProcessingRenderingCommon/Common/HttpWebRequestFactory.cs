using System;
using System.Net.Http;

namespace Microsoft.ReportingServices.ProcessingRenderingCommon.Common
{
	// Token: 0x020000D6 RID: 214
	public sealed class HttpWebRequestFactory
	{
		// Token: 0x0600077C RID: 1916 RVA: 0x00013CDF File Offset: 0x00011EDF
		private HttpWebRequestFactory()
		{
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00013CE7 File Offset: 0x00011EE7
		public static HttpClient Create()
		{
			return HttpWebRequestFactory.lazy.Value;
		}

		// Token: 0x0400043A RID: 1082
		private static readonly Lazy<HttpClient> lazy = new Lazy<HttpClient>(() => new HttpClient(new TlsTracingHttpMessageHandler()));
	}
}
