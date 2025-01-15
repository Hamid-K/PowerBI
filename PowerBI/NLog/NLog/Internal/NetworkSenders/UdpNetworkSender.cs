using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using NLog.Common;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x0200015C RID: 348
	internal class UdpNetworkSender : NetworkSender
	{
		// Token: 0x06001062 RID: 4194 RVA: 0x0002A874 File Offset: 0x00028A74
		public UdpNetworkSender(string url, AddressFamily addressFamily)
			: base(url)
		{
			this.AddressFamily = addressFamily;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x0002A88F File Offset: 0x00028A8F
		// (set) Token: 0x06001064 RID: 4196 RVA: 0x0002A897 File Offset: 0x00028A97
		internal AddressFamily AddressFamily { get; set; }

		// Token: 0x06001065 RID: 4197 RVA: 0x0002A8A0 File Offset: 0x00028AA0
		protected internal virtual ISocket CreateSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			SocketProxy socketProxy = new SocketProxy(addressFamily, socketType, protocolType);
			Uri uri;
			if (Uri.TryCreate(base.Address, UriKind.Absolute, out uri) && uri.Host.Equals(IPAddress.Broadcast.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				socketProxy.UnderlyingSocket.EnableBroadcast = true;
			}
			return socketProxy;
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0002A8EB File Offset: 0x00028AEB
		protected override void DoInitialize()
		{
			this._endpoint = this.ParseEndpointAddress(new Uri(base.Address), this.AddressFamily);
			this._socket = this.CreateSocket(this._endpoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0002A924 File Offset: 0x00028B24
		protected override void DoClose(AsyncContinuation continuation)
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				this.CloseSocket(continuation);
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0002A968 File Offset: 0x00028B68
		private void CloseSocket(AsyncContinuation continuation)
		{
			try
			{
				ISocket socket = this._socket;
				this._socket = null;
				if (socket != null)
				{
					socket.Close();
				}
				continuation(null);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				continuation(ex);
			}
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0002A9BC File Offset: 0x00028BBC
		protected override void DoSend(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation)
		{
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
				socketAsyncEventArgs.SetBuffer(bytes, offset, length);
				socketAsyncEventArgs.UserToken = asyncContinuation;
				socketAsyncEventArgs.Completed += this.SocketOperationCompleted;
				socketAsyncEventArgs.RemoteEndPoint = this._endpoint;
				if (!this._socket.SendToAsync(socketAsyncEventArgs))
				{
					this.SocketOperationCompleted(this._socket, socketAsyncEventArgs);
				}
			}
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0002AA48 File Offset: 0x00028C48
		private void SocketOperationCompleted(object sender, SocketAsyncEventArgs e)
		{
			AsyncContinuation asyncContinuation = e.UserToken as AsyncContinuation;
			Exception ex = null;
			if (e.SocketError != SocketError.Success)
			{
				ex = new IOException("Error: " + e.SocketError);
			}
			e.Dispose();
			if (asyncContinuation != null)
			{
				asyncContinuation(ex);
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0002AA96 File Offset: 0x00028C96
		public override void CheckSocket()
		{
			if (this._socket == null)
			{
				this.DoInitialize();
			}
		}

		// Token: 0x0400046B RID: 1131
		private readonly object _lockObject = new object();

		// Token: 0x0400046C RID: 1132
		private ISocket _socket;

		// Token: 0x0400046D RID: 1133
		private EndPoint _endpoint;
	}
}
