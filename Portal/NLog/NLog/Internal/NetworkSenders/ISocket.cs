using System;
using System.Net.Sockets;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x02000154 RID: 340
	internal interface ISocket
	{
		// Token: 0x0600101F RID: 4127
		bool ConnectAsync(SocketAsyncEventArgs args);

		// Token: 0x06001020 RID: 4128
		void Close();

		// Token: 0x06001021 RID: 4129
		bool SendAsync(SocketAsyncEventArgs args);

		// Token: 0x06001022 RID: 4130
		bool SendToAsync(SocketAsyncEventArgs args);
	}
}
