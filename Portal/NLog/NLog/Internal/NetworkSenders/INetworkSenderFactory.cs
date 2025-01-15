using System;
using System.Security.Authentication;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x02000153 RID: 339
	internal interface INetworkSenderFactory
	{
		// Token: 0x0600101E RID: 4126
		NetworkSender Create(string url, int maxQueueSize, SslProtocols sslProtocols, TimeSpan keepAliveTime);
	}
}
