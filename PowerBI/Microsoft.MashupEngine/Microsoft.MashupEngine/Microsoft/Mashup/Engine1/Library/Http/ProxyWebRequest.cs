using System;
using System.Net;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A71 RID: 2673
	public static class ProxyWebRequest
	{
		// Token: 0x06004AD1 RID: 19153 RVA: 0x000F8936 File Offset: 0x000F6B36
		public static WebRequest CreateRequest(Uri requestUri)
		{
			return ProxyWebRequest.RequestWithWebProxy(WebRequest.Create(requestUri));
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x000F8943 File Offset: 0x000F6B43
		public static WebRequest CreateRequest(string requestUriString)
		{
			return ProxyWebRequest.RequestWithWebProxy(WebRequest.Create(requestUriString));
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x000F8950 File Offset: 0x000F6B50
		private static WebRequest RequestWithWebProxy(WebRequest webRequest)
		{
			IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
			if (webRequest != null && defaultWebProxy != null)
			{
				defaultWebProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
				webRequest.Proxy = defaultWebProxy;
			}
			return webRequest;
		}
	}
}
