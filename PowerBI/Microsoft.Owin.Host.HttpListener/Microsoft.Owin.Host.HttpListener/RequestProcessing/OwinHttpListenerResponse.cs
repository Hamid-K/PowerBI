using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Host.HttpListener.WebSockets;

namespace Microsoft.Owin.Host.HttpListener.RequestProcessing
{
	// Token: 0x02000014 RID: 20
	internal class OwinHttpListenerResponse
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00006034 File Offset: 0x00004234
		internal OwinHttpListenerResponse(HttpListenerContext context, CallEnvironment environment)
		{
			this._context = context;
			this._response = this._context.Response;
			this._environment = environment;
			this._requestState = 1;
			this._environment.ResponseStatusCode = 200;
			HttpListenerStreamWrapper outputStream = new HttpListenerStreamWrapper(this._response.OutputStream);
			outputStream.OnFirstWrite(OwinHttpListenerResponse._responseBodyStarted, this);
			this._environment.ResponseBody = outputStream;
			this._environment.ResponseHeaders = new ResponseHeadersDictionary(this._response);
			this._onSendingHeadersActions = new List<Tuple<Action<object>, object>>();
			this._environment.OnSendingHeaders = new Action<Action<object>, object>(this.RegisterForOnSendingHeaders);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000060E0 File Offset: 0x000042E0
		private void DoWebSocketUpgrade(IDictionary<string, object> acceptOptions, Func<IDictionary<string, object>, Task> callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (!this.TryStartResponse())
			{
				throw new InvalidOperationException(Resources.Exception_ResponseAlreadySent);
			}
			this._environment["owin.ResponseStatusCode"] = 101;
			this._acceptOptions = acceptOptions;
			this._webSocketFunc = callback;
			string subProtocol = this.GetWebSocketSubProtocol();
			int receiveBufferSize = this.GetWebSocketReceiveBufferSize();
			TimeSpan keepAliveInterval = this.GetWebSocketKeepAliveInterval();
			ArraySegment<byte>? internalBuffer = this.GetWebSocketBuffer();
			this.PrepareResponse(false);
			this._webSocketAction = this.WebSocketUpgrade(subProtocol, receiveBufferSize, keepAliveInterval, internalBuffer);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006168 File Offset: 0x00004368
		private async Task WebSocketUpgrade(string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte>? internalBuffer)
		{
			HttpListenerWebSocketContext context;
			if (internalBuffer == null)
			{
				HttpListenerWebSocketContext httpListenerWebSocketContext = await this._context.AcceptWebSocketAsync(subProtocol, receiveBufferSize, keepAliveInterval);
				context = httpListenerWebSocketContext;
			}
			else
			{
				context = await this._context.AcceptWebSocketAsync(subProtocol, receiveBufferSize, keepAliveInterval, internalBuffer.Value);
			}
			OwinWebSocketWrapper wrapper = new OwinWebSocketWrapper(context, this._environment.Get("owin.CallCancelled"));
			await this._webSocketFunc(wrapper.Environment);
			await wrapper.CleanupAsync();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000061CC File Offset: 0x000043CC
		private string GetWebSocketSubProtocol()
		{
			IDictionary<string, string[]> reponseHeaders = this._environment.Get("owin.ResponseHeaders");
			string subProtocol = null;
			string[] subProtocols;
			if (reponseHeaders.TryGetValue("Sec-WebSocket-Protocol", out subProtocols) && subProtocols.Length != 0)
			{
				subProtocol = subProtocols[0];
				reponseHeaders.Remove("Sec-WebSocket-Protocol");
			}
			if (this._acceptOptions != null && this._acceptOptions.ContainsKey("websocket.SubProtocol"))
			{
				subProtocol = this._acceptOptions.Get("websocket.SubProtocol");
			}
			return subProtocol;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000623C File Offset: 0x0000443C
		private int GetWebSocketReceiveBufferSize()
		{
			int? receiveBufferSize = null;
			if (this._acceptOptions != null)
			{
				receiveBufferSize = this._acceptOptions.Get("websocket.ReceiveBufferSize");
			}
			int? num = receiveBufferSize;
			if (num == null)
			{
				return 16384;
			}
			return num.GetValueOrDefault();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006284 File Offset: 0x00004484
		private TimeSpan GetWebSocketKeepAliveInterval()
		{
			TimeSpan? keepAliveInterval = null;
			if (this._acceptOptions != null)
			{
				keepAliveInterval = this._acceptOptions.Get("websocket.KeepAliveInterval");
			}
			TimeSpan? timeSpan = keepAliveInterval;
			if (timeSpan == null)
			{
				return WebSocket.DefaultKeepAliveInterval;
			}
			return timeSpan.GetValueOrDefault();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000062CC File Offset: 0x000044CC
		private ArraySegment<byte>? GetWebSocketBuffer()
		{
			if (this._acceptOptions != null)
			{
				return this._acceptOptions.Get("websocket.Buffer");
			}
			return null;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000062FB File Offset: 0x000044FB
		internal bool TryStartResponse()
		{
			return Interlocked.CompareExchange(ref this._requestState, 2, 1) == 1;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000630D File Offset: 0x0000450D
		internal bool TryFinishResponse()
		{
			return Interlocked.CompareExchange(ref this._requestState, 3, 2) == 2;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006320 File Offset: 0x00004520
		private static void OnResponseBodyStarted(object state)
		{
			OwinHttpListenerResponse thisPtr = (OwinHttpListenerResponse)state;
			thisPtr.ResponseBodyStarted();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000633A File Offset: 0x0000453A
		private void ResponseBodyStarted()
		{
			this.PrepareResponse(true);
			if (!this.TryStartResponse())
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000635C File Offset: 0x0000455C
		internal Task CompleteResponseAsync()
		{
			this.PrepareResponse(false);
			return this._webSocketAction ?? Task.FromResult<int>(0);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006375 File Offset: 0x00004575
		public void Close()
		{
			this.TryStartResponse();
			if (this.TryFinishResponse())
			{
				this._context.Response.Close();
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006398 File Offset: 0x00004598
		private void PrepareResponse(bool mayHaveBody)
		{
			if (this._responsePrepared)
			{
				return;
			}
			this._responsePrepared = true;
			this.NotifyOnSendingHeaders();
			this.SetStatusCode();
			this.SetReasonPhrase();
			if (!mayHaveBody && !this._response.SendChunked && this._response.ContentLength64 <= 0L)
			{
				this._response.ContentLength64 = 0L;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000063F4 File Offset: 0x000045F4
		private void SetStatusCode()
		{
			int statusCode = this._environment.ResponseStatusCode;
			if (statusCode != 0)
			{
				if (statusCode == 100 || statusCode < 100 || statusCode >= 1000)
				{
					throw new ArgumentOutOfRangeException("owin.ResponseStatusCode", statusCode, string.Empty);
				}
				this._response.StatusCode = statusCode;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006444 File Offset: 0x00004644
		private void SetReasonPhrase()
		{
			string reasonPhrase = this._environment.ResponseReasonPhrase;
			if (!string.IsNullOrWhiteSpace(reasonPhrase))
			{
				this._response.StatusDescription = reasonPhrase;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006474 File Offset: 0x00004674
		private void RegisterForOnSendingHeaders(Action<object> callback, object state)
		{
			IList<Tuple<Action<object>, object>> actions = this._onSendingHeadersActions;
			if (actions == null)
			{
				throw new InvalidOperationException("Headers already sent");
			}
			actions.Add(new Tuple<Action<object>, object>(callback, state));
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000064A4 File Offset: 0x000046A4
		private void NotifyOnSendingHeaders()
		{
			IList<Tuple<Action<object>, object>> actions = Interlocked.Exchange<IList<Tuple<Action<object>, object>>>(ref this._onSendingHeadersActions, null);
			for (int i = actions.Count - 1; i >= 0; i--)
			{
				Tuple<Action<object>, object> actionPair = actions[i];
				actionPair.Item1(actionPair.Item2);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000064EC File Offset: 0x000046EC
		internal void End()
		{
			int priorState = Interlocked.Exchange(ref this._requestState, 3);
			if (priorState == 1)
			{
				this._context.Response.StatusCode = 500;
				this._context.Response.ContentLength64 = 0L;
				this._context.Response.Headers.Clear();
				try
				{
					this._context.Response.Close();
					return;
				}
				catch (HttpListenerException)
				{
					return;
				}
			}
			if (priorState == 2)
			{
				this._context.Response.Abort();
				return;
			}
			this._context.Response.Abort();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006590 File Offset: 0x00004790
		internal bool TryGetWebSocketAccept(ref Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>> websocketAccept)
		{
			if (this._context.Request.IsWebSocketRequest)
			{
				string versionString = this._context.Request.Headers["Sec-WebSocket-Version"];
				int version;
				if (!string.IsNullOrWhiteSpace(versionString) && int.TryParse(versionString, NumberStyles.Integer, CultureInfo.InvariantCulture, out version) && version >= 13)
				{
					websocketAccept = new Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>>(this.DoWebSocketUpgrade);
					return true;
				}
			}
			websocketAccept = null;
			return false;
		}

		// Token: 0x04000091 RID: 145
		private const int RequestInProgress = 1;

		// Token: 0x04000092 RID: 146
		private const int ResponseInProgress = 2;

		// Token: 0x04000093 RID: 147
		private const int Completed = 3;

		// Token: 0x04000094 RID: 148
		private static Action<object> _responseBodyStarted = new Action<object>(OwinHttpListenerResponse.OnResponseBodyStarted);

		// Token: 0x04000095 RID: 149
		private readonly CallEnvironment _environment;

		// Token: 0x04000096 RID: 150
		private readonly HttpListenerResponse _response;

		// Token: 0x04000097 RID: 151
		private readonly HttpListenerContext _context;

		// Token: 0x04000098 RID: 152
		private bool _responsePrepared;

		// Token: 0x04000099 RID: 153
		private IList<Tuple<Action<object>, object>> _onSendingHeadersActions;

		// Token: 0x0400009A RID: 154
		private int _requestState;

		// Token: 0x0400009B RID: 155
		private IDictionary<string, object> _acceptOptions;

		// Token: 0x0400009C RID: 156
		private Func<IDictionary<string, object>, Task> _webSocketFunc;

		// Token: 0x0400009D RID: 157
		private Task _webSocketAction;
	}
}
