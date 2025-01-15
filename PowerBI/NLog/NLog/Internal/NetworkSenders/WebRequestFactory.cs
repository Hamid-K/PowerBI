using System;
using System.Net;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x02000156 RID: 342
	internal class WebRequestFactory : IWebRequestFactory
	{
		// Token: 0x06001024 RID: 4132 RVA: 0x00029C0A File Offset: 0x00027E0A
		public WebRequest CreateWebRequest(Uri address)
		{
			return WebRequest.Create(address);
		}
	}
}
