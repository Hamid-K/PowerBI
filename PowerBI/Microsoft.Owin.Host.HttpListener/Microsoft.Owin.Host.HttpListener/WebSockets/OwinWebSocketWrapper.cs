using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Owin.Host.HttpListener.WebSockets
{
	// Token: 0x0200000C RID: 12
	internal class OwinWebSocketWrapper
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002CA4 File Offset: 0x00000EA4
		internal OwinWebSocketWrapper(WebSocketContext context, CancellationToken ct)
		{
			this._context = context;
			this._webSocket = this._context.WebSocket;
			this._cancellationToken = ct;
			this._environment = new Dictionary<string, object>();
			this._environment["websocket.SendAsync"] = new Func<ArraySegment<byte>, int, bool, CancellationToken, Task>(this.SendAsync);
			this._environment["websocket.ReceiveAsync"] = new Func<ArraySegment<byte>, CancellationToken, Task<Tuple<int, bool, int>>>(this.ReceiveAsync);
			this._environment["websocket.CloseAsync"] = new Func<int, string, CancellationToken, Task>(this.CloseAsync);
			this._environment["websocket.CallCancelled"] = ct;
			this._environment["websocket.Version"] = "1.0";
			this._environment[typeof(WebSocketContext).FullName] = this._context;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002D80 File Offset: 0x00000F80
		internal IDictionary<string, object> Environment
		{
			get
			{
				return this._environment;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002D88 File Offset: 0x00000F88
		internal Task SendAsync(ArraySegment<byte> buffer, int messageType, bool endOfMessage, CancellationToken cancel)
		{
			if (messageType == 8)
			{
				return this.RedirectSendToCloseAsync(buffer, cancel);
			}
			if (messageType == 9 || messageType == 10)
			{
				return Task.FromResult<int>(0);
			}
			return this._webSocket.SendAsync(buffer, OwinWebSocketWrapper.OpCodeToEnum(messageType), endOfMessage, cancel);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002DC0 File Offset: 0x00000FC0
		internal async Task<Tuple<int, bool, int>> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancel)
		{
			WebSocketReceiveResult webSocketReceiveResult = await this._webSocket.ReceiveAsync(buffer, cancel);
			WebSocketReceiveResult nativeResult = webSocketReceiveResult;
			if (nativeResult.MessageType == WebSocketMessageType.Close)
			{
				this._environment["websocket.ClientCloseStatus"] = (int)(nativeResult.CloseStatus ?? WebSocketCloseStatus.NormalClosure);
				this._environment["websocket.ClientCloseDescription"] = nativeResult.CloseStatusDescription ?? string.Empty;
			}
			return new Tuple<int, bool, int>(OwinWebSocketWrapper.EnumToOpCode(nativeResult.MessageType), nativeResult.EndOfMessage, nativeResult.Count);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002E13 File Offset: 0x00001013
		internal Task CloseAsync(int status, string description, CancellationToken cancel)
		{
			return this._webSocket.CloseOutputAsync((WebSocketCloseStatus)status, description, cancel);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002E24 File Offset: 0x00001024
		private Task RedirectSendToCloseAsync(ArraySegment<byte> buffer, CancellationToken cancel)
		{
			if (buffer.Array == null || buffer.Count == 0)
			{
				return this.CloseAsync(1000, string.Empty, cancel);
			}
			if (buffer.Count >= 2)
			{
				int statusCode = ((int)buffer.Array[buffer.Offset] << 8) | (int)buffer.Array[buffer.Offset + 1];
				string description = Encoding.UTF8.GetString(buffer.Array, buffer.Offset + 2, buffer.Count - 2);
				return this.CloseAsync(statusCode, description, cancel);
			}
			throw new ArgumentOutOfRangeException("buffer");
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002EBC File Offset: 0x000010BC
		internal async Task CleanupAsync()
		{
			switch (this._webSocket.State)
			{
			case WebSocketState.Open:
			case WebSocketState.CloseSent:
				this._webSocket.Abort();
				break;
			case WebSocketState.CloseReceived:
			{
				using (CancellationTokenSource timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[] { this._cancellationToken }))
				{
					timeoutCts.CancelAfter(TimeSpan.FromSeconds(15.0));
					await this._webSocket.CloseAsync(this._webSocket.CloseStatus ?? WebSocketCloseStatus.NormalClosure, this._webSocket.CloseStatusDescription ?? string.Empty, timeoutCts.Token);
				}
				CancellationTokenSource timeoutCts = null;
				break;
			}
			case WebSocketState.Closed:
			case WebSocketState.Aborted:
				break;
			default:
				throw new ArgumentOutOfRangeException("state", this._webSocket.State, string.Empty);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002EFF File Offset: 0x000010FF
		private static WebSocketMessageType OpCodeToEnum(int messageType)
		{
			if (messageType == 1)
			{
				return WebSocketMessageType.Text;
			}
			if (messageType == 2)
			{
				return WebSocketMessageType.Binary;
			}
			if (messageType != 8)
			{
				throw new ArgumentOutOfRangeException("messageType", messageType, string.Empty);
			}
			return WebSocketMessageType.Close;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002F2A File Offset: 0x0000112A
		private static int EnumToOpCode(WebSocketMessageType webSocketMessageType)
		{
			switch (webSocketMessageType)
			{
			case WebSocketMessageType.Text:
				return 1;
			case WebSocketMessageType.Binary:
				return 2;
			case WebSocketMessageType.Close:
				return 8;
			default:
				throw new ArgumentOutOfRangeException("webSocketMessageType", webSocketMessageType, string.Empty);
			}
		}

		// Token: 0x04000055 RID: 85
		private readonly WebSocket _webSocket;

		// Token: 0x04000056 RID: 86
		private readonly IDictionary<string, object> _environment;

		// Token: 0x04000057 RID: 87
		private readonly CancellationToken _cancellationToken;

		// Token: 0x04000058 RID: 88
		private readonly WebSocketContext _context;
	}
}
