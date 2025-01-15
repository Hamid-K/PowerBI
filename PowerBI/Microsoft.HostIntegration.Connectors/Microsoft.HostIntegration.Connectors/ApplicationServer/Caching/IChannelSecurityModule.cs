using System;
using System.Net.Sockets;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000189 RID: 393
	internal interface IChannelSecurityModule
	{
		// Token: 0x06000CB9 RID: 3257
		void BeginAuthenticateAsClient(Socket socket, DataCacheSecurity securityProperties, AsyncCallback callback, object state, TimeSpan timeout);

		// Token: 0x06000CBA RID: 3258
		SecureStream EndAuthenticateAsClient(IAsyncResult asyncResult);

		// Token: 0x06000CBB RID: 3259
		void BeginAuthenticateAsServer(Socket socket, DataCacheSecurity securityProperties, AsyncCallback callback, object state, TimeSpan timeout);

		// Token: 0x06000CBC RID: 3260
		SecureStream EndAuthenticateAsServer(IAsyncResult asyncResult);
	}
}
