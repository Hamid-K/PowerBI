using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Security.Authentication;
using NLog.Common;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x0200015B RID: 347
	internal class TcpNetworkSender : NetworkSender
	{
		// Token: 0x0600104D RID: 4173 RVA: 0x0002A1C7 File Offset: 0x000283C7
		public TcpNetworkSender(string url, AddressFamily addressFamily)
			: base(url)
		{
			this.AddressFamily = addressFamily;
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0002A1E2 File Offset: 0x000283E2
		// (set) Token: 0x0600104F RID: 4175 RVA: 0x0002A1EA File Offset: 0x000283EA
		internal AddressFamily AddressFamily { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0002A1F3 File Offset: 0x000283F3
		// (set) Token: 0x06001051 RID: 4177 RVA: 0x0002A1FB File Offset: 0x000283FB
		internal int MaxQueueSize { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0002A204 File Offset: 0x00028404
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x0002A20C File Offset: 0x0002840C
		internal SslProtocols SslProtocols { get; set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0002A215 File Offset: 0x00028415
		// (set) Token: 0x06001055 RID: 4181 RVA: 0x0002A21D File Offset: 0x0002841D
		internal TimeSpan KeepAliveTime { get; set; }

		// Token: 0x06001056 RID: 4182 RVA: 0x0002A228 File Offset: 0x00028428
		protected internal virtual ISocket CreateSocket(string host, AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			SocketProxy socketProxy = new SocketProxy(addressFamily, socketType, protocolType);
			if (this.KeepAliveTime.TotalSeconds >= 1.0)
			{
				bool? enableKeepAliveSuccessful = TcpNetworkSender.EnableKeepAliveSuccessful;
				bool flag = false;
				if (!((enableKeepAliveSuccessful.GetValueOrDefault() == flag) & (enableKeepAliveSuccessful != null)))
				{
					TcpNetworkSender.EnableKeepAliveSuccessful = new bool?(TcpNetworkSender.TryEnableKeepAlive(socketProxy.UnderlyingSocket, (int)this.KeepAliveTime.TotalSeconds));
				}
			}
			if (this.SslProtocols != SslProtocols.None)
			{
				return new SslSocketProxy(host, this.SslProtocols, socketProxy);
			}
			return socketProxy;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0002A2B0 File Offset: 0x000284B0
		private static bool TryEnableKeepAlive(Socket underlyingSocket, int keepAliveTimeSeconds)
		{
			if (TcpNetworkSender.TrySetSocketOption(underlyingSocket, SocketOptionName.KeepAlive, true))
			{
				SocketOptionName socketOptionName = SocketOptionName.TypeOfService;
				SocketOptionName socketOptionName2 = SocketOptionName.BlockSource;
				if (PlatformDetector.CurrentOS == RuntimeOS.Linux)
				{
					socketOptionName = SocketOptionName.ReuseAddress;
					socketOptionName2 = (SocketOptionName)5;
				}
				else if (PlatformDetector.CurrentOS == RuntimeOS.MacOSX)
				{
					socketOptionName = SocketOptionName.DontRoute;
					socketOptionName2 = (SocketOptionName)257;
				}
				if (TcpNetworkSender.TrySetTcpOption(underlyingSocket, socketOptionName, keepAliveTimeSeconds))
				{
					TcpNetworkSender.TrySetTcpOption(underlyingSocket, socketOptionName2, 1);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0002A304 File Offset: 0x00028504
		private static bool TrySetSocketOption(Socket underlyingSocket, SocketOptionName socketOption, bool value)
		{
			bool flag;
			try
			{
				underlyingSocket.SetSocketOption(SocketOptionLevel.Socket, socketOption, value);
				flag = true;
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "NetworkTarget: Failed to configure Socket-option {0} = {1}", new object[] { socketOption, value });
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0002A35C File Offset: 0x0002855C
		private static bool TrySetTcpOption(Socket underlyingSocket, SocketOptionName socketOption, int value)
		{
			bool flag;
			try
			{
				underlyingSocket.SetSocketOption(SocketOptionLevel.Tcp, socketOption, value);
				flag = true;
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "NetworkTarget: Failed to configure TCP-option {0} = {1}", new object[] { socketOption, value });
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0002A3B0 File Offset: 0x000285B0
		protected override void DoInitialize()
		{
			Uri uri = new Uri(base.Address);
			TcpNetworkSender.MySocketAsyncEventArgs mySocketAsyncEventArgs = new TcpNetworkSender.MySocketAsyncEventArgs();
			mySocketAsyncEventArgs.RemoteEndPoint = this.ParseEndpointAddress(new Uri(base.Address), this.AddressFamily);
			mySocketAsyncEventArgs.Completed += this.SocketOperationCompleted;
			mySocketAsyncEventArgs.UserToken = null;
			this._socket = this.CreateSocket(uri.Host, mySocketAsyncEventArgs.RemoteEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			this._asyncOperationInProgress = true;
			bool flag = false;
			try
			{
				flag = this._socket.ConnectAsync(mySocketAsyncEventArgs);
			}
			catch (SocketException ex)
			{
				mySocketAsyncEventArgs.SocketError = ex.SocketErrorCode;
			}
			catch (Exception ex2)
			{
				SocketException ex3;
				if ((ex3 = ex2.InnerException as SocketException) != null)
				{
					mySocketAsyncEventArgs.SocketError = ex3.SocketErrorCode;
				}
				else
				{
					mySocketAsyncEventArgs.SocketError = SocketError.OperationAborted;
				}
			}
			if (!flag)
			{
				this.SocketOperationCompleted(this._socket, mySocketAsyncEventArgs);
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0002A4A4 File Offset: 0x000286A4
		protected override void DoClose(AsyncContinuation continuation)
		{
			Queue<SocketAsyncEventArgs> pendingRequests = this._pendingRequests;
			lock (pendingRequests)
			{
				if (this._asyncOperationInProgress)
				{
					this._closeContinuation = continuation;
				}
				else
				{
					this.CloseSocket(continuation);
				}
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0002A4F8 File Offset: 0x000286F8
		protected override void DoFlush(AsyncContinuation continuation)
		{
			Queue<SocketAsyncEventArgs> pendingRequests = this._pendingRequests;
			lock (pendingRequests)
			{
				if (!this._asyncOperationInProgress && this._pendingRequests.Count == 0)
				{
					continuation(null);
				}
				else
				{
					this._flushContinuation = continuation;
				}
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0002A558 File Offset: 0x00028758
		protected override void DoSend(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation)
		{
			TcpNetworkSender.MySocketAsyncEventArgs mySocketAsyncEventArgs = new TcpNetworkSender.MySocketAsyncEventArgs();
			mySocketAsyncEventArgs.SetBuffer(bytes, offset, length);
			mySocketAsyncEventArgs.UserToken = asyncContinuation;
			mySocketAsyncEventArgs.Completed += this.SocketOperationCompleted;
			Queue<SocketAsyncEventArgs> pendingRequests = this._pendingRequests;
			lock (pendingRequests)
			{
				if (this.MaxQueueSize != 0 && this._pendingRequests.Count >= this.MaxQueueSize)
				{
					SocketAsyncEventArgs socketAsyncEventArgs = this._pendingRequests.Dequeue();
					if (socketAsyncEventArgs != null)
					{
						socketAsyncEventArgs.Dispose();
					}
				}
				this._pendingRequests.Enqueue(mySocketAsyncEventArgs);
			}
			this.ProcessNextQueuedItem();
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0002A600 File Offset: 0x00028800
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

		// Token: 0x0600105F RID: 4191 RVA: 0x0002A654 File Offset: 0x00028854
		private void SocketOperationCompleted(object sender, SocketAsyncEventArgs e)
		{
			Queue<SocketAsyncEventArgs> pendingRequests = this._pendingRequests;
			lock (pendingRequests)
			{
				this._asyncOperationInProgress = false;
				AsyncContinuation asyncContinuation = e.UserToken as AsyncContinuation;
				if (e.SocketError != SocketError.Success)
				{
					this._pendingError = new IOException("Error: " + e.SocketError);
				}
				e.Dispose();
				if (asyncContinuation != null)
				{
					asyncContinuation(this._pendingError);
				}
			}
			this.ProcessNextQueuedItem();
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0002A6E8 File Offset: 0x000288E8
		private void ProcessNextQueuedItem()
		{
			Queue<SocketAsyncEventArgs> pendingRequests = this._pendingRequests;
			lock (pendingRequests)
			{
				if (!this._asyncOperationInProgress)
				{
					if (this._pendingError != null)
					{
						while (this._pendingRequests.Count != 0)
						{
							SocketAsyncEventArgs socketAsyncEventArgs = this._pendingRequests.Dequeue();
							AsyncContinuation asyncContinuation = (AsyncContinuation)socketAsyncEventArgs.UserToken;
							socketAsyncEventArgs.Dispose();
							asyncContinuation(this._pendingError);
						}
					}
					if (this._pendingRequests.Count == 0)
					{
						AsyncContinuation flushContinuation = this._flushContinuation;
						if (flushContinuation != null)
						{
							this._flushContinuation = null;
							flushContinuation(this._pendingError);
						}
						AsyncContinuation closeContinuation = this._closeContinuation;
						if (closeContinuation != null)
						{
							this._closeContinuation = null;
							this.CloseSocket(closeContinuation);
						}
					}
					else
					{
						SocketAsyncEventArgs socketAsyncEventArgs = this._pendingRequests.Dequeue();
						this._asyncOperationInProgress = true;
						bool flag2 = false;
						try
						{
							flag2 = this._socket.SendAsync(socketAsyncEventArgs);
						}
						catch (SocketException ex)
						{
							socketAsyncEventArgs.SocketError = ex.SocketErrorCode;
						}
						catch (Exception ex2)
						{
							SocketException ex3;
							if ((ex3 = ex2.InnerException as SocketException) != null)
							{
								socketAsyncEventArgs.SocketError = ex3.SocketErrorCode;
							}
							else
							{
								socketAsyncEventArgs.SocketError = SocketError.OperationAborted;
							}
						}
						if (!flag2)
						{
							this.SocketOperationCompleted(this._socket, socketAsyncEventArgs);
						}
					}
				}
			}
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0002A864 File Offset: 0x00028A64
		public override void CheckSocket()
		{
			if (this._socket == null)
			{
				this.DoInitialize();
			}
		}

		// Token: 0x04000460 RID: 1120
		private static bool? EnableKeepAliveSuccessful;

		// Token: 0x04000461 RID: 1121
		private readonly Queue<SocketAsyncEventArgs> _pendingRequests = new Queue<SocketAsyncEventArgs>();

		// Token: 0x04000462 RID: 1122
		private ISocket _socket;

		// Token: 0x04000463 RID: 1123
		private Exception _pendingError;

		// Token: 0x04000464 RID: 1124
		private bool _asyncOperationInProgress;

		// Token: 0x04000465 RID: 1125
		private AsyncContinuation _closeContinuation;

		// Token: 0x04000466 RID: 1126
		private AsyncContinuation _flushContinuation;

		// Token: 0x02000290 RID: 656
		internal class MySocketAsyncEventArgs : SocketAsyncEventArgs
		{
			// Token: 0x060016BC RID: 5820 RVA: 0x0003BE16 File Offset: 0x0003A016
			public void RaiseCompleted()
			{
				this.OnCompleted(this);
			}
		}
	}
}
