using System;
using System.Net.Sockets;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x02000159 RID: 345
	internal sealed class SocketProxy : ISocket, IDisposable
	{
		// Token: 0x0600103A RID: 4154 RVA: 0x00029E8E File Offset: 0x0002808E
		internal SocketProxy(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			this._socket = new Socket(addressFamily, socketType, protocolType);
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x00029EA4 File Offset: 0x000280A4
		public Socket UnderlyingSocket
		{
			get
			{
				return this._socket;
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00029EAC File Offset: 0x000280AC
		public void Close()
		{
			this._socket.Close();
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00029EB9 File Offset: 0x000280B9
		public bool ConnectAsync(SocketAsyncEventArgs args)
		{
			return this._socket.ConnectAsync(args);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00029EC7 File Offset: 0x000280C7
		public bool SendAsync(SocketAsyncEventArgs args)
		{
			return this._socket.SendAsync(args);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00029ED5 File Offset: 0x000280D5
		public bool SendToAsync(SocketAsyncEventArgs args)
		{
			return this._socket.SendToAsync(args);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00029EE3 File Offset: 0x000280E3
		public void Dispose()
		{
			((IDisposable)this._socket).Dispose();
		}

		// Token: 0x0400045A RID: 1114
		private readonly Socket _socket;
	}
}
