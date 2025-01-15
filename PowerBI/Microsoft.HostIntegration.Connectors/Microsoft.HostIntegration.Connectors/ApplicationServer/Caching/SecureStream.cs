using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000193 RID: 403
	internal class SecureStream : IDisposable
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0002D0E0 File Offset: 0x0002B2E0
		internal Socket InnerSocket
		{
			get
			{
				return this._socket;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0002D0E8 File Offset: 0x0002B2E8
		internal EndPoint LocalEndPoint
		{
			get
			{
				return this._localEndPoint;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0002D0F0 File Offset: 0x0002B2F0
		internal EndPoint RemoteEndPoint
		{
			get
			{
				return this._remoteEndPoint;
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002D0F8 File Offset: 0x0002B2F8
		internal SecureStream(AuthenticatedStream authenticatedStream, Socket socket)
		{
			this._authenticatedStream = authenticatedStream;
			this._socket = socket;
			this._remoteEndPoint = this._socket.RemoteEndPoint;
			this._localEndPoint = this._socket.LocalEndPoint;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0002D130 File Offset: 0x0002B330
		internal int Read(ArraySegment<byte> buffer)
		{
			return this.Read(buffer.Array, buffer.Offset, buffer.Count);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0002D14D File Offset: 0x0002B34D
		internal int Read(byte[] buffer, int offset, int count)
		{
			return this._authenticatedStream.Read(buffer, offset, count);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0002D15D File Offset: 0x0002B35D
		internal void BeginRead(ArraySegment<byte> buffer, AsyncCallback callback, object state)
		{
			this.BeginRead(buffer.Array, buffer.Offset, buffer.Count, callback, state);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0002D17C File Offset: 0x0002B37C
		private void BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this._authenticatedStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0002D191 File Offset: 0x0002B391
		internal int EndRead(IAsyncResult asyncResult)
		{
			return this._authenticatedStream.EndRead(asyncResult);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0002D19F File Offset: 0x0002B39F
		internal void Write(ArraySegment<byte> buffer)
		{
			this.Write(buffer.Array, buffer.Offset, buffer.Count);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0002D1BC File Offset: 0x0002B3BC
		internal void Write(byte[] buffer, int offset, int count)
		{
			this._authenticatedStream.Write(buffer, offset, count);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0002D1CC File Offset: 0x0002B3CC
		internal void Flush()
		{
			this._authenticatedStream.Flush();
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0002D1D9 File Offset: 0x0002B3D9
		public void Dispose()
		{
			if (this._authenticatedStream != null)
			{
				this._authenticatedStream.Close();
			}
		}

		// Token: 0x0400092F RID: 2351
		private Socket _socket;

		// Token: 0x04000930 RID: 2352
		private AuthenticatedStream _authenticatedStream;

		// Token: 0x04000931 RID: 2353
		private readonly EndPoint _remoteEndPoint;

		// Token: 0x04000932 RID: 2354
		private readonly EndPoint _localEndPoint;
	}
}
