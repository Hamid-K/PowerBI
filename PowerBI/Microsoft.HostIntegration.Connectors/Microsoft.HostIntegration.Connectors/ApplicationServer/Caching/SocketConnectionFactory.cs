using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000198 RID: 408
	internal class SocketConnectionFactory
	{
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000D50 RID: 3408 RVA: 0x0002D7D0 File Offset: 0x0002B9D0
		// (remove) Token: 0x06000D51 RID: 3409 RVA: 0x0002D808 File Offset: 0x0002BA08
		internal event Action ConnectionRequestFailedEvent;

		// Token: 0x06000D52 RID: 3410 RVA: 0x0002D840 File Offset: 0x0002BA40
		internal SocketConnectionFactory(string logPrefix)
		{
			this._logSource = "DistributedCache.SocketConnectionFactory." + logPrefix;
			this.ConnectionRequestFailedEvent += NetworkStatistics.OnConnectionRequestFailed;
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				this.ConnectionRequestFailedEvent += ClientPerfCounterUpdate.OnConnectionRequestFailed;
			}
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0002D890 File Offset: 0x0002BA90
		public IAsyncResult BeginConnect(string host, int port, AsyncCallback callback, object state, TimeSpan timeout)
		{
			Socket socket = null;
			SocketConnectionFactory.ConnectOperationState connectOperationState = null;
			IAsyncResult asyncResult2;
			try
			{
				socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				socket.Blocking = true;
				AsyncResult<Socket> asyncResult = new AsyncResult<Socket>(callback, state);
				connectOperationState = new SocketConnectionFactory.ConnectOperationState(socket, asyncResult, host, port);
				connectOperationState.ConnectionTimer = new global::System.Threading.Timer(new TimerCallback(this.OnTimerExpired), connectOperationState, timeout, TimeSpan.FromMilliseconds(-1.0));
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<string, int>(this._logSource, "BeginConnect posted for host {0}:{1}", host, port);
				}
				socket.BeginConnect(host, port, new AsyncCallback(this.AsynConnectionCallback), connectOperationState);
				asyncResult2 = asyncResult;
			}
			catch
			{
				if (connectOperationState != null && connectOperationState.ConnectionTimer != null)
				{
					connectOperationState.ConnectionTimer.Dispose();
				}
				if (socket != null)
				{
					socket.Dispose();
				}
				throw;
			}
			return asyncResult2;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0002D954 File Offset: 0x0002BB54
		public Socket EndConnect(IAsyncResult asyncResult)
		{
			return ((AsyncResult<Socket>)asyncResult).EndInvoke();
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002D964 File Offset: 0x0002BB64
		private void OnTimerExpired(object state)
		{
			SocketConnectionFactory.ConnectOperationState connectOperationState = (SocketConnectionFactory.ConnectOperationState)state;
			if (!connectOperationState.TryComplete())
			{
				return;
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, int>(this._logSource, "OnTimerExpired called for connection to host {0}:{1}", connectOperationState.Host, connectOperationState.Port);
			}
			connectOperationState.Socket.Close();
			connectOperationState.ConnectionTimer.Dispose();
			connectOperationState.AsyncResult.SetAsCompleted(new TimeoutException("Connection open timed out"), false);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002D9D4 File Offset: 0x0002BBD4
		private void AsynConnectionCallback(IAsyncResult result)
		{
			SocketConnectionFactory.ConnectOperationState connectOperationState = (SocketConnectionFactory.ConnectOperationState)result.AsyncState;
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, int>(this._logSource, "AsyncConnectionCallback called for connection to host {0}:{1}", connectOperationState.Host, connectOperationState.Port);
			}
			if (!connectOperationState.TryComplete())
			{
				return;
			}
			connectOperationState.ConnectionTimer.Dispose();
			try
			{
				connectOperationState.Socket.EndConnect(result);
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<string, int>(this._logSource, "Connection completed successfully to host {0}:{1}", connectOperationState.Host, connectOperationState.Port);
				}
				connectOperationState.AsyncResult.SetAsCompleted(connectOperationState.Socket, false);
			}
			catch (Exception ex)
			{
				if (this.ConnectionRequestFailedEvent != null)
				{
					this.ConnectionRequestFailedEvent();
				}
				connectOperationState.Socket.Close();
				if (!(ex is SocketException) && !(ex is ObjectDisposedException))
				{
					throw;
				}
				connectOperationState.AsyncResult.SetAsCompleted(ex, false);
			}
		}

		// Token: 0x04000943 RID: 2371
		private readonly string _logSource;

		// Token: 0x02000199 RID: 409
		private class ConnectOperationState
		{
			// Token: 0x170002FA RID: 762
			// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0002DABC File Offset: 0x0002BCBC
			// (set) Token: 0x06000D58 RID: 3416 RVA: 0x0002DAC4 File Offset: 0x0002BCC4
			public Socket Socket { get; set; }

			// Token: 0x170002FB RID: 763
			// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0002DACD File Offset: 0x0002BCCD
			// (set) Token: 0x06000D5A RID: 3418 RVA: 0x0002DAD5 File Offset: 0x0002BCD5
			public global::System.Threading.Timer ConnectionTimer { get; set; }

			// Token: 0x170002FC RID: 764
			// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0002DADE File Offset: 0x0002BCDE
			// (set) Token: 0x06000D5C RID: 3420 RVA: 0x0002DAE6 File Offset: 0x0002BCE6
			public string Host { get; private set; }

			// Token: 0x170002FD RID: 765
			// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0002DAEF File Offset: 0x0002BCEF
			// (set) Token: 0x06000D5E RID: 3422 RVA: 0x0002DAF7 File Offset: 0x0002BCF7
			public int Port { get; private set; }

			// Token: 0x170002FE RID: 766
			// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0002DB00 File Offset: 0x0002BD00
			// (set) Token: 0x06000D60 RID: 3424 RVA: 0x0002DB08 File Offset: 0x0002BD08
			public AsyncResult<Socket> AsyncResult { get; private set; }

			// Token: 0x06000D61 RID: 3425 RVA: 0x0002DB11 File Offset: 0x0002BD11
			public ConnectOperationState(Socket socket, AsyncResult<Socket> userState, string host, int port)
			{
				this.Socket = socket;
				this.AsyncResult = userState;
				this.Host = host;
				this.Port = port;
				this._completed = 0;
			}

			// Token: 0x06000D62 RID: 3426 RVA: 0x0002DB3D File Offset: 0x0002BD3D
			public bool TryComplete()
			{
				return Interlocked.CompareExchange(ref this._completed, 1, 0) == 0;
			}

			// Token: 0x04000944 RID: 2372
			private int _completed;
		}
	}
}
