using System;
using System.Net.Sockets;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001A2 RID: 418
	internal class TcpClientChannelFactory : IClientChannelFactory
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0002EAE3 File Offset: 0x0002CCE3
		// (set) Token: 0x06000DAE RID: 3502 RVA: 0x0002EAEB File Offset: 0x0002CCEB
		public OnMessageReceived MessageReceived { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0002EAF4 File Offset: 0x0002CCF4
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x0002EAFC File Offset: 0x0002CCFC
		public TcpChnlClosed ChannelClosed { get; private set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0002EB05 File Offset: 0x0002CD05
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x0002EB0D File Offset: 0x0002CD0D
		public TcpChnlOpened ChannelOpened { get; private set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0002EB16 File Offset: 0x0002CD16
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x0002EB1E File Offset: 0x0002CD1E
		public string ChannelLogPrefix { get; private set; }

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0002EB28 File Offset: 0x0002CD28
		public TcpClientChannelFactory(TcpChnlClosed chnlClosed, TcpChnlOpened chnlOpened, OnMessageReceived msgReceived, GetRecvBuffers getBuffers, SocketConnectionFactory socketConnectionFactory, TcpTransportProperty tcpProp, string logPrefix)
		{
			this._tcpTransportProperty = tcpProp;
			this._getRecvBuffers = getBuffers;
			this.ChannelClosed = chnlClosed;
			this.ChannelOpened = chnlOpened;
			this.MessageReceived = msgReceived;
			this._socketConnectionFactory = socketConnectionFactory;
			this.ChannelLogPrefix = "DistributedCache.ClientChannel." + logPrefix + ".";
			this._logSource = "DistributedCache.TcpClientChannelFactory." + logPrefix;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002EB94 File Offset: 0x0002CD94
		public void BeginConnect(string host, int port, AsyncCallback callback, object state)
		{
			AsyncResult<TcpConnectOperationResult> asyncResult = new AsyncResult<TcpConnectOperationResult>(callback, state);
			this._socketConnectionFactory.BeginConnect(host, port, new AsyncCallback(this.TcpConnectionCallback), asyncResult, TcpUtility.ConnectionTimeout);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002DCA6 File Offset: 0x0002BEA6
		public TcpConnectOperationResult EndConnect(IAsyncResult asyncResult)
		{
			return ((AsyncResult<TcpConnectOperationResult>)asyncResult).EndInvoke();
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002EBCC File Offset: 0x0002CDCC
		private void TcpConnectionCallback(IAsyncResult result)
		{
			AsyncResult<TcpConnectOperationResult> asyncResult = (AsyncResult<TcpConnectOperationResult>)result.AsyncState;
			try
			{
				Socket socket = this._socketConnectionFactory.EndConnect(result);
				TcpSocketChannel tcpSocketChannel = new TcpSocketChannel(socket, false);
				tcpSocketChannel.Initialize(null, this.MessageReceived, this._getRecvBuffers, this._tcpTransportProperty, new VelocityWireProtocol(), this.ChannelLogPrefix);
				TcpConnectOperationResult tcpConnectOperationResult = new TcpConnectOperationResult
				{
					Channel = tcpSocketChannel
				};
				asyncResult.SetAsCompleted(tcpConnectOperationResult, false);
			}
			catch (Exception ex)
			{
				if (!TcpUtility.HandleException(this._logSource, ex))
				{
					throw;
				}
				TcpConnectOperationResult tcpConnectOperationResult2 = new TcpConnectOperationResult
				{
					ConnectionFailureException = ex
				};
				asyncResult.SetAsCompleted(tcpConnectOperationResult2, false);
			}
		}

		// Token: 0x0400096F RID: 2415
		private readonly GetRecvBuffers _getRecvBuffers;

		// Token: 0x04000970 RID: 2416
		private readonly TcpTransportProperty _tcpTransportProperty;

		// Token: 0x04000971 RID: 2417
		private readonly SocketConnectionFactory _socketConnectionFactory;

		// Token: 0x04000972 RID: 2418
		private readonly string _logSource;
	}
}
