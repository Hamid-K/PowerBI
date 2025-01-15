using System;
using System.Net;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x02000155 RID: 341
	internal interface IWebRequestFactory
	{
		// Token: 0x06001023 RID: 4131
		WebRequest CreateWebRequest(Uri address);
	}
}
