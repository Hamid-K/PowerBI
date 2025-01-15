using System;
using System.Net.Sockets;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000197 RID: 407
	internal class WindowsAuthenticationModule : IChannelSecurityModule
	{
		// Token: 0x06000D4B RID: 3403 RVA: 0x00002061 File Offset: 0x00000261
		internal WindowsAuthenticationModule()
		{
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void BeginAuthenticateAsClient(Socket socket, DataCacheSecurity securityProperties, AsyncCallback callback, object state, TimeSpan timeout)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00003CAB File Offset: 0x00001EAB
		public SecureStream EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void BeginAuthenticateAsServer(Socket socket, DataCacheSecurity securityProperties, AsyncCallback callback, object state, TimeSpan timeout)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00003CAB File Offset: 0x00001EAB
		public SecureStream EndAuthenticateAsServer(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}
	}
}
