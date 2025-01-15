using System;
using System.Net.Sockets;
using System.Security.Authentication;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x02000158 RID: 344
	internal class NetworkSenderFactory : INetworkSenderFactory
	{
		// Token: 0x06001037 RID: 4151 RVA: 0x00029D6C File Offset: 0x00027F6C
		public NetworkSender Create(string url, int maxQueueSize, SslProtocols sslProtocols, TimeSpan keepAliveTime)
		{
			if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
			{
				return new HttpNetworkSender(url);
			}
			if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
			{
				return new HttpNetworkSender(url);
			}
			if (url.StartsWith("tcp://", StringComparison.OrdinalIgnoreCase))
			{
				return new TcpNetworkSender(url, AddressFamily.Unspecified)
				{
					MaxQueueSize = maxQueueSize,
					SslProtocols = sslProtocols,
					KeepAliveTime = keepAliveTime
				};
			}
			if (url.StartsWith("tcp4://", StringComparison.OrdinalIgnoreCase))
			{
				return new TcpNetworkSender(url, AddressFamily.InterNetwork)
				{
					MaxQueueSize = maxQueueSize,
					SslProtocols = sslProtocols,
					KeepAliveTime = keepAliveTime
				};
			}
			if (url.StartsWith("tcp6://", StringComparison.OrdinalIgnoreCase))
			{
				return new TcpNetworkSender(url, AddressFamily.InterNetworkV6)
				{
					MaxQueueSize = maxQueueSize,
					SslProtocols = sslProtocols,
					KeepAliveTime = keepAliveTime
				};
			}
			if (url.StartsWith("udp://", StringComparison.OrdinalIgnoreCase))
			{
				return new UdpNetworkSender(url, AddressFamily.Unspecified);
			}
			if (url.StartsWith("udp4://", StringComparison.OrdinalIgnoreCase))
			{
				return new UdpNetworkSender(url, AddressFamily.InterNetwork);
			}
			if (url.StartsWith("udp6://", StringComparison.OrdinalIgnoreCase))
			{
				return new UdpNetworkSender(url, AddressFamily.InterNetworkV6);
			}
			throw new ArgumentException("Unrecognized network address", "url");
		}

		// Token: 0x04000459 RID: 1113
		public static readonly INetworkSenderFactory Default = new NetworkSenderFactory();
	}
}
