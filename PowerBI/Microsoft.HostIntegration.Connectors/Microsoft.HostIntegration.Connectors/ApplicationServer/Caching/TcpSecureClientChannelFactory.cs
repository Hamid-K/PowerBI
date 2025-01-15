using System;
using System.Net.Sockets;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200019B RID: 411
	internal class TcpSecureClientChannelFactory : IClientChannelFactory
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0002DBA7 File Offset: 0x0002BDA7
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x0002DBAF File Offset: 0x0002BDAF
		public OnMessageReceived MessageReceived { get; set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x0002DBB8 File Offset: 0x0002BDB8
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x0002DBC0 File Offset: 0x0002BDC0
		public TcpChnlClosed ChannelClosed { get; private set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0002DBC9 File Offset: 0x0002BDC9
		// (set) Token: 0x06000D6A RID: 3434 RVA: 0x0002DBD1 File Offset: 0x0002BDD1
		public TcpChnlOpened ChannelOpened { get; private set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0002DBDA File Offset: 0x0002BDDA
		// (set) Token: 0x06000D6C RID: 3436 RVA: 0x0002DBE2 File Offset: 0x0002BDE2
		public string ChannelLogPrefix { get; private set; }

		// Token: 0x06000D6D RID: 3437 RVA: 0x0002DBEC File Offset: 0x0002BDEC
		public TcpSecureClientChannelFactory(TcpChnlClosed chnlClosed, TcpChnlOpened chnlOpened, OnMessageReceived msgReceived, GetRecvBuffers getRecvBuffers, SocketConnectionFactory socketConnectionFactory, DataCacheSecurity securityProperties, TcpTransportProperty tcpProp, string logPrefix)
		{
			this.ChannelClosed = chnlClosed;
			this.ChannelOpened = chnlOpened;
			this.MessageReceived = msgReceived;
			this._tcpTransportProperty = tcpProp;
			this._getRecvBuffers = getRecvBuffers;
			this._socketConnectionFactory = socketConnectionFactory;
			this.ChannelLogPrefix = "DistributedCache.SecureClientChannel." + logPrefix + ".";
			this._logSource = "TcpSecureClientChannelFactory." + logPrefix;
			this._securityProperties = securityProperties;
			this._securityModule = SecurityModuleFactory.GetChannelSecurityModule(securityProperties, this.ChannelLogPrefix);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0002DC70 File Offset: 0x0002BE70
		public void BeginConnect(string host, int port, AsyncCallback callback, object state)
		{
			AsyncResult<TcpConnectOperationResult> asyncResult = new AsyncResult<TcpConnectOperationResult>(callback, state);
			this._socketConnectionFactory.BeginConnect(host, port, new AsyncCallback(this.TcpConnectionCallback), asyncResult, TcpUtility.ConnectionTimeout);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0002DCA6 File Offset: 0x0002BEA6
		public TcpConnectOperationResult EndConnect(IAsyncResult asyncResult)
		{
			return ((AsyncResult<TcpConnectOperationResult>)asyncResult).EndInvoke();
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0002DCB4 File Offset: 0x0002BEB4
		private void TcpConnectionCallback(IAsyncResult result)
		{
			AsyncResult<TcpConnectOperationResult> asyncResult = (AsyncResult<TcpConnectOperationResult>)result.AsyncState;
			Socket socket = null;
			try
			{
				socket = this._socketConnectionFactory.EndConnect(result);
				this._securityModule.BeginAuthenticateAsClient(socket, this._securityProperties, new AsyncCallback(this.AuthenticationCallback), asyncResult, TcpUtility.ConnectionTimeout);
			}
			catch (Exception ex)
			{
				if (socket != null)
				{
					socket.Dispose();
				}
				if (!TcpUtility.HandleSocketException(this._logSource, ex) && !TcpUtility.HandleAuthException(this._logSource, ex))
				{
					throw;
				}
				TcpConnectOperationResult tcpConnectOperationResult = new TcpConnectOperationResult
				{
					ConnectionFailureException = ex
				};
				asyncResult.SetAsCompleted(tcpConnectOperationResult, false);
			}
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0002DD58 File Offset: 0x0002BF58
		private void AuthenticationCallback(IAsyncResult asyncResult)
		{
			AsyncResult<TcpConnectOperationResult> asyncResult2 = (AsyncResult<TcpConnectOperationResult>)asyncResult.AsyncState;
			try
			{
				SecureStream secureStream = this._securityModule.EndAuthenticateAsClient(asyncResult);
				TcpSecureSocketChannel tcpSecureSocketChannel = new TcpSecureSocketChannel(secureStream);
				tcpSecureSocketChannel.Initialize(null, this.MessageReceived, this._getRecvBuffers, this._tcpTransportProperty, new VelocityWireProtocol(), this.ChannelLogPrefix);
				TcpConnectOperationResult tcpConnectOperationResult = new TcpConnectOperationResult
				{
					Channel = tcpSecureSocketChannel
				};
				asyncResult2.SetAsCompleted(tcpConnectOperationResult, false);
			}
			catch (Exception ex)
			{
				if (!TcpUtility.HandleAuthException(this._logSource, ex))
				{
					throw;
				}
				TcpConnectOperationResult tcpConnectOperationResult2 = new TcpConnectOperationResult
				{
					ConnectionFailureException = ex
				};
				asyncResult2.SetAsCompleted(tcpConnectOperationResult2, false);
			}
		}

		// Token: 0x0400094A RID: 2378
		private readonly GetRecvBuffers _getRecvBuffers;

		// Token: 0x0400094B RID: 2379
		private readonly TcpTransportProperty _tcpTransportProperty;

		// Token: 0x0400094C RID: 2380
		private readonly SocketConnectionFactory _socketConnectionFactory;

		// Token: 0x0400094D RID: 2381
		private readonly DataCacheSecurity _securityProperties;

		// Token: 0x0400094E RID: 2382
		private readonly IChannelSecurityModule _securityModule;

		// Token: 0x0400094F RID: 2383
		private readonly string _logSource;
	}
}
