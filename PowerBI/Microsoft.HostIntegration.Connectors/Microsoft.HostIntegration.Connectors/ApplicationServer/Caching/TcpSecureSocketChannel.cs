using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000196 RID: 406
	internal class TcpSecureSocketChannel : ITcpChannel, IAuthorizedChannel
	{
		// Token: 0x06000D35 RID: 3381 RVA: 0x0002D21C File Offset: 0x0002B41C
		public TcpSecureSocketChannel(SecureStream secureStream)
			: this(secureStream, null)
		{
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0002D228 File Offset: 0x0002B428
		public TcpSecureSocketChannel(SecureStream secureStream, string cacheEndpoint)
		{
			this._secureStream = secureStream;
			this._endReceiveCallback = new AsyncCallback(this.AsyncReceiveCallback);
			if (this._chnlName == null)
			{
				this._chnlName = secureStream.LocalEndPoint + "; " + secureStream.RemoteEndPoint;
			}
			this._cacheConnectionProperty = new CacheConnectionProperty(cacheEndpoint, null, null);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0002D29C File Offset: 0x0002B49C
		public void Initialize(TcpChnlClosed chnlClosed, OnMessageReceived msgReceived, GetRecvBuffers getBuffers, TcpTransportProperty tcpTransportProperty, IServerSocketProtocol protocol, string logPrefix)
		{
			this._chnlClosedCallback = chnlClosed;
			this._msgReceivedCallback = msgReceived;
			this._getBuffers = getBuffers;
			this._logSource = logPrefix + this._chnlName;
			this._tcpProperty = tcpTransportProperty;
			this._endRecvTime = DateTime.UtcNow.Add(tcpTransportProperty.ReceiveTimeout);
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Setting timeout on channel SendTimeout {0}, ReceiveTimeout {1}", new object[]
				{
					(int)tcpTransportProperty.SendTimeout.TotalMilliseconds,
					(int)tcpTransportProperty.ReceiveTimeout.TotalMilliseconds
				});
			}
			TcpUtility.InitializeSocketTransportProperties(this._secureStream.InnerSocket, this._tcpProperty);
			this._protocol = protocol;
		}

		// Token: 0x170002F4 RID: 756
		// (set) Token: 0x06000D38 RID: 3384 RVA: 0x0002D361 File Offset: 0x0002B561
		public TcpChnlClosed TcpChannelClosed
		{
			set
			{
				this._chnlClosedCallback = value;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0002D36A File Offset: 0x0002B56A
		public EndPoint RemoteEndpoint
		{
			get
			{
				return this._secureStream.RemoteEndPoint;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0002D377 File Offset: 0x0002B577
		public DateTime EndReceiveTime
		{
			get
			{
				return this._endRecvTime;
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00003CAB File Offset: 0x00001EAB
		public OperationResult Send(IList<ArraySegment<byte>> buffers, TcpPacketSendTypes sendType, int? sequenceNo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0002D37F File Offset: 0x0002B57F
		public OperationResult Send(IList<ArraySegment<byte>> buffers)
		{
			return this.Send(buffers, this._tcpProperty.SendTimeout);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0002D394 File Offset: 0x0002B594
		private OperationResult Send(IEnumerable<ArraySegment<byte>> buffers, TimeSpan timeout)
		{
			OperationResult operationResult = null;
			if (Monitor.TryEnter(this._sendLock, (int)timeout.TotalMilliseconds))
			{
				try
				{
					try
					{
						foreach (ArraySegment<byte> arraySegment in buffers)
						{
							this._secureStream.Write(arraySegment);
						}
						this._secureStream.Flush();
						operationResult = new OperationResult(OperationStatus.Success, this.GetHashCode());
					}
					catch (Exception ex)
					{
						if (!TcpUtility.HandleSecureMessageFailure(this._logSource, ex))
						{
							throw;
						}
						this.Abort();
						operationResult = new OperationResult(OperationStatus.SendFailed, ex);
					}
					goto IL_00A6;
				}
				finally
				{
					Monitor.Exit(this._sendLock);
				}
			}
			operationResult = new OperationResult(OperationStatus.SendFailed, new TimeoutException("Failed to acquire send lock"));
			IL_00A6:
			if (Provider.IsEnabled(TraceLevel.Verbose) && operationResult != OperationResult.Success)
			{
				EventLogWriter.WriteVerbose<OperationResult>(this._logSource, " Send operation status {0}", operationResult);
			}
			return operationResult;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0002D494 File Offset: 0x0002B694
		public int Receive(ArraySegment<byte> buffer, TimeSpan timeout)
		{
			TcpUtility.Log(this._logSource, "Receive", buffer, timeout);
			this._endRecvTime = DateTime.UtcNow.Add(timeout);
			return this.Receive(buffer);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0002D4D0 File Offset: 0x0002B6D0
		public int Receive(ArraySegment<byte> buffer)
		{
			TcpUtility.Log(this._logSource, "Receive", buffer);
			int num = 0;
			int num2 = buffer.Count - buffer.Offset;
			int num4;
			try
			{
				for (;;)
				{
					int num3 = this._secureStream.Read(buffer.Array, num, num2);
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose<int, int>(this._logSource, "Sync receive completed with bytes received {0} expected {1}", num3, num2);
					}
					if (num3 == 0)
					{
						break;
					}
					num += num3;
					num2 -= num3;
					if (num >= buffer.Count)
					{
						goto Block_4;
					}
				}
				TcpUtility.HandleZeroBytesPacket(this, this._recvMessageState.IOState, this._logSource);
				return num;
				Block_4:
				num4 = num;
			}
			catch (Exception ex)
			{
				if (!TcpUtility.HandleSecureMessageFailure(this._logSource, ex))
				{
					throw;
				}
				this.Abort();
				num4 = -1;
			}
			return num4;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0002D594 File Offset: 0x0002B794
		public bool ReceiveMessage()
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>(this._logSource, "Receive message started on channel {0}", this._chnlName);
			}
			IList<IVelocityPacket> list = null;
			IEnumerator<ArraySegment<byte>> enumerator = this._getBuffers(out list).GetEnumerator();
			if (!enumerator.MoveNext())
			{
				throw new InvalidOperationException("Expect the header at the very least");
			}
			this._endRecvTime = DateTime.UtcNow.Add(this._tcpProperty.ReceiveTimeout);
			this._recvMessageState = new RecvMessageState(enumerator, list);
			this.BeginReceive(enumerator.Current);
			return false;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002D620 File Offset: 0x0002B820
		private void BeginReceive(ArraySegment<byte> buffer)
		{
			this._recvMessageState.IOState = new IOState(buffer);
			try
			{
				this._secureStream.BeginRead(buffer, this._endReceiveCallback, this);
			}
			catch (Exception ex)
			{
				if (!TcpUtility.HandleSecureMessageFailure(this._logSource, ex))
				{
					throw;
				}
				this.Abort();
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0002D67C File Offset: 0x0002B87C
		private void AsyncReceiveCallback(IAsyncResult result)
		{
			try
			{
				int num = this._secureStream.EndRead(result);
				if (num == 0)
				{
					TcpUtility.HandleZeroBytesPacket(this, this._recvMessageState.IOState, this._logSource);
					return;
				}
				this._recvMessageState.IOState.ByteTransferred = num;
			}
			catch (Exception ex)
			{
				if (!TcpUtility.HandleSecureMessageFailure(this._logSource, ex))
				{
					throw;
				}
				this.Abort();
				return;
			}
			TcpUtility.CompleteAsyncReceiveMessage(this, this._recvMessageState, new InvokeMessageCallback(this.InvokeMessageRecvCallback), this._logSource);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0002D70C File Offset: 0x0002B90C
		private void InvokeMessageRecvCallback(VelocityPacketException exception)
		{
			IReplyContext replyContext = this._protocol.CreateReplyContext(this, this._recvMessageState.Packets, exception);
			this._msgReceivedCallback(replyContext);
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0002D73E File Offset: 0x0002B93E
		public bool IsOpened
		{
			get
			{
				return this._isClosed == 0;
			}
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0002D749 File Offset: 0x0002B949
		public void CloseGracefully()
		{
			this.Abort();
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0002D754 File Offset: 0x0002B954
		public void Abort()
		{
			if (Interlocked.CompareExchange(ref this._isClosed, 1, 0) != 0)
			{
				return;
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Aborting channel {0}.", new object[] { this._chnlName });
			}
			this._secureStream.Dispose();
			this._chnlClosedCallback(this);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0002D7B1 File Offset: 0x0002B9B1
		public override int GetHashCode()
		{
			return this._secureStream.GetHashCode();
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0002D7BE File Offset: 0x0002B9BE
		public override string ToString()
		{
			return this._chnlName;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0002D7C6 File Offset: 0x0002B9C6
		public CacheConnectionProperty ConnectionProperty
		{
			get
			{
				return this._cacheConnectionProperty;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00002B16 File Offset: 0x00000D16
		public bool IsSecureChannel
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000934 RID: 2356
		private TcpChnlClosed _chnlClosedCallback;

		// Token: 0x04000935 RID: 2357
		private OnMessageReceived _msgReceivedCallback;

		// Token: 0x04000936 RID: 2358
		private GetRecvBuffers _getBuffers;

		// Token: 0x04000937 RID: 2359
		private readonly AsyncCallback _endReceiveCallback;

		// Token: 0x04000938 RID: 2360
		private readonly SecureStream _secureStream;

		// Token: 0x04000939 RID: 2361
		private readonly object _sendLock = new object();

		// Token: 0x0400093A RID: 2362
		private int _isClosed;

		// Token: 0x0400093B RID: 2363
		private DateTime _endRecvTime;

		// Token: 0x0400093C RID: 2364
		private RecvMessageState _recvMessageState;

		// Token: 0x0400093D RID: 2365
		private TcpTransportProperty _tcpProperty;

		// Token: 0x0400093E RID: 2366
		private IServerSocketProtocol _protocol;

		// Token: 0x0400093F RID: 2367
		private string _logSource;

		// Token: 0x04000940 RID: 2368
		private readonly string _chnlName;

		// Token: 0x04000941 RID: 2369
		private readonly CacheConnectionProperty _cacheConnectionProperty;
	}
}
