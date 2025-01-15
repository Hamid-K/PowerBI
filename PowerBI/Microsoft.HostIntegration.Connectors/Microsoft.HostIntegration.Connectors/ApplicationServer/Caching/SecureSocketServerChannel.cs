using System;
using System.Diagnostics;
using System.Net.Sockets;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000192 RID: 402
	internal sealed class SecureSocketServerChannel : SocketServerChannel
	{
		// Token: 0x06000D14 RID: 3348 RVA: 0x0002CF08 File Offset: 0x0002B108
		public SecureSocketServerChannel(ServiceConfigurationManager serviceConfigurationManager, int port, IServerSocketProtocol protocol, IChannelSecurityModule securityModule)
			: base(serviceConfigurationManager, port, protocol, "DistributedCache.SecureSocketServerChannel.")
		{
			this._securityProperties = serviceConfigurationManager.AdvancedProperties.SecurityProperties.GetDataCacheSecurity();
			this._securityModule = securityModule;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0002CF38 File Offset: 0x0002B138
		protected override void CompleteAccept(IAsyncResult asyncResult)
		{
			Socket socket = null;
			try
			{
				socket = this._listenerSocket.EndAccept(asyncResult);
				this._securityModule.BeginAuthenticateAsServer(socket, this._securityProperties, new AsyncCallback(this.AuthenticationCallback), this, TcpUtility.ConnectionTimeout);
			}
			catch (Exception ex)
			{
				if (socket != null)
				{
					socket.Close();
				}
				if (!TcpUtility.HandleSocketException(this._logSource, ex) && !TcpUtility.HandleAuthException(this._logSource, ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0002CFB4 File Offset: 0x0002B1B4
		private void AuthenticationCallback(IAsyncResult asyncResult)
		{
			ITcpChannel tcpChannel = null;
			try
			{
				SecureStream secureStream = this._securityModule.EndAuthenticateAsServer(asyncResult);
				if (this._serviceConfigurationManager != null)
				{
					tcpChannel = new TcpSecureSocketChannel(secureStream, this._serviceConfigurationManager.AdvancedProperties.DnsDomain);
				}
				else
				{
					tcpChannel = new TcpSecureSocketChannel(secureStream);
				}
				tcpChannel.Initialize(new TcpChnlClosed(base.OnTcpChnlClosed), this._serverReceiveCallback, new GetRecvBuffers(base.GetBuffers), this._transportProperty, base.Protocol, "DistributedCache.SecureSocketServerChannel.");
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(this._logSource, "ChannelID = {0} Connection opened.", new object[] { tcpChannel.ToString() });
				}
				lock (this._tcpChannels)
				{
					this._tcpChannels.Add(tcpChannel, DateTime.UtcNow.Ticks);
				}
				base.OnConnectionCreated();
				tcpChannel.ReceiveMessage();
			}
			catch (Exception ex)
			{
				if (tcpChannel != null)
				{
					tcpChannel.CloseGracefully();
				}
				if (!TcpUtility.HandleAuthException(this._logSource, ex))
				{
					throw;
				}
			}
		}

		// Token: 0x0400092C RID: 2348
		private const string _logSourcePrefix = "DistributedCache.SecureSocketServerChannel.";

		// Token: 0x0400092D RID: 2349
		private readonly DataCacheSecurity _securityProperties;

		// Token: 0x0400092E RID: 2350
		private readonly IChannelSecurityModule _securityModule;
	}
}
