using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000188 RID: 392
	internal interface IClientChannelFactory
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000CB4 RID: 3252
		TcpChnlClosed ChannelClosed { get; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000CB5 RID: 3253
		TcpChnlOpened ChannelOpened { get; }

		// Token: 0x06000CB6 RID: 3254
		void BeginConnect(string host, int port, AsyncCallback callback, object state);

		// Token: 0x06000CB7 RID: 3255
		TcpConnectOperationResult EndConnect(IAsyncResult asyncResult);

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000CB8 RID: 3256
		string ChannelLogPrefix { get; }
	}
}
