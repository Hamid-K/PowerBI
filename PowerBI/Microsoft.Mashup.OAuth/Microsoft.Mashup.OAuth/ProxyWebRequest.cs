using System;
using System.Net;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000021 RID: 33
	internal static class ProxyWebRequest
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000053F5 File Offset: 0x000035F5
		internal static WebRequest CreateRequest(Uri requestUri)
		{
			return ProxyWebRequest.RequestWithWebProxy(WebRequest.Create(requestUri));
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005404 File Offset: 0x00003604
		private static WebRequest RequestWithWebProxy(WebRequest webRequest)
		{
			OAuthSettings.InitializeTls12And13();
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
