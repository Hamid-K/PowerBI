using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002D0 RID: 720
	internal class TcpSocketChannel : ITcpChannel, IAuthorizedChannel
	{
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x000507C0 File Offset: 0x0004E9C0
		internal static int TotalIncomingChannelCount
		{
			get
			{
				return TcpSocketChannel.totalIncomingChannelCount;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x000507C7 File Offset: 0x0004E9C7
		internal static int CurrentIncomingChannelCount
		{
			get
			{
				return TcpSocketChannel.currentIncomingChannelCount;
			}
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x000507CE File Offset: 0x0004E9CE
		public TcpSocketChannel(Socket socket, bool isIncoming)
			: this(socket, isIncoming, null)
		{
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000507D9 File Offset: 0x0004E9D9
		public TcpSocketChannel(Socket socket, string cacheEndpoint)
			: this(socket, true, cacheEndpoint)
		{
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x000507E4 File Offset: 0x0004E9E4
		private TcpSocketChannel(Socket socket, bool isIncoming, string cacheEndpoint)
		{
			this._socket = socket;
			this._isIncoming = isIncoming;
			this._remoteEndpoint = this._socket.RemoteEndPoint;
			this._endReceiveCallback = new AsyncCallback(this.AsyncReceiveCallback);
			this._cacheConnectionProperty = new CacheConnectionProperty(cacheEndpoint, null, null);
			if (this._chnlName == null)
			{
				this._chnlName = this._socket.LocalEndPoint.ToString() + "; " + this._socket.RemoteEndPoint.ToString();
			}
			this._buffersList = new SortedList<int, KeyValuePair<IList<ArraySegment<byte>>, TcpPacketSendTypes>>();
			if (this._isIncoming)
			{
				Interlocked.Increment(ref TcpSocketChannel.totalIncomingChannelCount);
				Interlocked.Increment(ref TcpSocketChannel.currentIncomingChannelCount);
			}
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000508AC File Offset: 0x0004EAAC
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
			TcpUtility.InitializeSocketTransportProperties(this._socket, this._tcpProperty);
			this._protocol = protocol;
		}

		// Token: 0x17000585 RID: 1413
		// (set) Token: 0x06001AA6 RID: 6822 RVA: 0x0005096C File Offset: 0x0004EB6C
		public TcpChnlClosed TcpChannelClosed
		{
			set
			{
				this._chnlClosedCallback = value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x00050975 File Offset: 0x0004EB75
		public EndPoint RemoteEndpoint
		{
			get
			{
				return this._remoteEndpoint;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x0005097D File Offset: 0x0004EB7D
		public DateTime EndReceiveTime
		{
			get
			{
				return this._endRecvTime;
			}
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00050988 File Offset: 0x0004EB88
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

		// Token: 0x06001AAA RID: 6826 RVA: 0x00050A14 File Offset: 0x0004EC14
		public OperationResult Send(IList<ArraySegment<byte>> buffers, TcpPacketSendTypes sendType, int? sequenceNo)
		{
			OperationResult operationResult = OperationResult.Success;
			if (sequenceNo != null)
			{
				operationResult = this.SendMemcacheBuffers(buffers, sendType, sequenceNo.Value);
			}
			else
			{
				operationResult = this.Send(buffers, this._tcpProperty.SendTimeout);
			}
			return operationResult;
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00050A56 File Offset: 0x0004EC56
		public OperationResult Send(IList<ArraySegment<byte>> buffers)
		{
			return this.Send(buffers, this._tcpProperty.SendTimeout);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00050A6C File Offset: 0x0004EC6C
		private OperationResult Send(IList<ArraySegment<byte>> buffers, TimeSpan timeout)
		{
			int length = TcpUtility.GetLength(buffers);
			int i = 0;
			OperationResult operationResult = null;
			if (Monitor.TryEnter(this._sendLock, (int)timeout.TotalMilliseconds))
			{
				try
				{
					try
					{
						IList<ArraySegment<byte>> list = buffers;
						while (i < length)
						{
							i += this._socket.Send(list);
							list = TcpUtility.GetRemainingArraySegment(buffers, length, i);
						}
						operationResult = new OperationResult(OperationStatus.Success, this._socket.GetHashCode());
					}
					catch (Exception ex)
					{
						if (!TcpUtility.HandleException(this._logSource, ex))
						{
							throw;
						}
						this.Abort();
						operationResult = new OperationResult(OperationStatus.SendFailed, ex);
					}
					goto IL_009C;
				}
				finally
				{
					Monitor.Exit(this._sendLock);
				}
			}
			operationResult = new OperationResult(OperationStatus.SendFailed, new TimeoutException("Failed to acquire send lock"));
			IL_009C:
			if (Provider.IsEnabled(TraceLevel.Verbose) && operationResult != OperationResult.Success)
			{
				EventLogWriter.WriteVerbose<OperationResult>(this._logSource, " Send operation status {0}", operationResult);
			}
			return operationResult;
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00050B54 File Offset: 0x0004ED54
		public int Receive(ArraySegment<byte> buffer, TimeSpan timeout)
		{
			TcpUtility.Log(this._logSource, "Receive", buffer, timeout);
			this._endRecvTime = DateTime.UtcNow.Add(timeout);
			return this.Receive(buffer);
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x00050B90 File Offset: 0x0004ED90
		public int Receive(ArraySegment<byte> buffer)
		{
			TcpUtility.Log(this._logSource, "Receive", buffer);
			int num = 0;
			ArraySegment<byte> arraySegment = buffer;
			try
			{
				for (;;)
				{
					int num2 = this._socket.Receive(arraySegment.Array, arraySegment.Offset, arraySegment.Count, SocketFlags.None);
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose<int, int>(this._logSource, "Sync receive completed with bytes received {0} expected {1}", num2, arraySegment.Count);
					}
					if (num2 == 0)
					{
						break;
					}
					num += num2;
					arraySegment = TcpUtility.GetRemainingArraySegment(buffer, num);
					if (num >= buffer.Count)
					{
						goto Block_4;
					}
				}
				TcpUtility.HandleZeroBytesPacket(this, this._recvMessageState.IOState, this._logSource);
				return num;
				Block_4:;
			}
			catch (Exception ex)
			{
				if (!TcpUtility.HandleException(this._logSource, ex))
				{
					throw;
				}
				this.Abort();
			}
			return num;
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00050C58 File Offset: 0x0004EE58
		private void BeginReceive(ArraySegment<byte> buffer)
		{
			this._recvMessageState.IOState = new IOState(buffer);
			try
			{
				this._socket.BeginReceive(buffer.Array, buffer.Offset, buffer.Count, SocketFlags.None, this._endReceiveCallback, this);
			}
			catch (Exception ex)
			{
				if (!TcpUtility.HandleException(this._logSource, ex))
				{
					throw;
				}
				this.Abort();
			}
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00050CCC File Offset: 0x0004EECC
		private void AsyncReceiveCallback(IAsyncResult result)
		{
			int num = 0;
			try
			{
				num = this._socket.EndReceive(result);
				if (num == 0)
				{
					TcpUtility.HandleZeroBytesPacket(this, this._recvMessageState.IOState, this._logSource);
					return;
				}
			}
			catch (Exception ex)
			{
				if (!TcpUtility.HandleException(this._logSource, ex))
				{
					throw;
				}
				this.Abort();
				return;
			}
			this._recvMessageState.IOState.ByteTransferred += num;
			if (this._recvMessageState.IOState.ByteTransferred != this._recvMessageState.IOState.Length)
			{
				ArraySegment<byte> remainingArraySegment = TcpUtility.GetRemainingArraySegment(this._recvMessageState.IOState.CurrentBuffer, this._recvMessageState.IOState.ByteTransferred);
				this.BeginReceive(remainingArraySegment);
				return;
			}
			TcpUtility.CompleteAsyncReceiveMessage(this, this._recvMessageState, new InvokeMessageCallback(this.InvokeMessageRecvCallback), this._logSource);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x00050DBC File Offset: 0x0004EFBC
		private void InvokeMessageRecvCallback(VelocityPacketException exception)
		{
			if (this._recvMessageState.Packets[0].MessageID == 0)
			{
				this._recvMessageState.Packets[0].MessageID = Interlocked.Increment(ref this._currentPacketId);
			}
			IReplyContext replyContext = this._protocol.CreateReplyContext(this, this._recvMessageState.Packets, exception);
			this._msgReceivedCallback(replyContext);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x00050E28 File Offset: 0x0004F028
		private OperationResult SendMemcacheBuffers(IList<ArraySegment<byte>> buffers, TcpPacketSendTypes sendType, int sequenceNo)
		{
			OperationResult operationResult = OperationResult.Success;
			if (sequenceNo == -1)
			{
				operationResult = this.Send(buffers, this._tcpProperty.SendTimeout);
				return operationResult;
			}
			lock (this._buffersList)
			{
				if (this._buffersList.ContainsKey(sequenceNo))
				{
					ReleaseAssert.Fail("Duplicate response for same Request Id: {0}", new object[] { sequenceNo });
					return operationResult;
				}
				this._buffersList.Add(sequenceNo, new KeyValuePair<IList<ArraySegment<byte>>, TcpPacketSendTypes>(buffers, sendType));
				for (int num = this.GetSendBatchSize(); num != 0; num = this.GetSendBatchSize())
				{
					for (int i = 0; i < num; i++)
					{
						int num2 = this._buffersList.Keys[0];
						IList<ArraySegment<byte>> key = this._buffersList[num2].Key;
						this._processedPacketId = num2;
						TcpPacketSendTypes value = this._buffersList[num2].Value;
						try
						{
							if ((value & TcpPacketSendTypes.Ignore) == TcpPacketSendTypes.None)
							{
								MemcacheProtocolHelper.LogSent(key);
								operationResult = this.Send(key, this._tcpProperty.SendTimeout);
							}
						}
						finally
						{
							MemcacheReplyContext.ReleaseBuffers(key);
						}
						if ((value & TcpPacketSendTypes.Quit) == TcpPacketSendTypes.Quit)
						{
							this.CloseGracefully();
						}
						this._buffersList.RemoveAt(0);
					}
				}
			}
			return operationResult;
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00050FB0 File Offset: 0x0004F1B0
		private int GetSendBatchSize()
		{
			lock (this._buffersList)
			{
				int num = 0;
				int num2 = this._processedPacketId;
				foreach (KeyValuePair<int, KeyValuePair<IList<ArraySegment<byte>>, TcpPacketSendTypes>> keyValuePair in this._buffersList)
				{
					if (keyValuePair.Key > num2 + 1)
					{
						return 0;
					}
					num++;
					num2++;
					TcpPacketSendTypes value = keyValuePair.Value.Value;
					if ((value & TcpPacketSendTypes.Immediate) == TcpPacketSendTypes.Immediate || (value & TcpPacketSendTypes.Quit) == TcpPacketSendTypes.Quit)
					{
						return num;
					}
				}
			}
			return 0;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00051070 File Offset: 0x0004F270
		public void CloseGracefully()
		{
			try
			{
				if (Interlocked.CompareExchange(ref this._isClosed, 1, 0) != 0)
				{
					return;
				}
				this._socket.Shutdown(SocketShutdown.Both);
			}
			catch (Exception ex)
			{
				if (!TcpUtility.HandleException(this._logSource, ex))
				{
					throw;
				}
			}
			finally
			{
				if (this._isIncoming)
				{
					Interlocked.Decrement(ref TcpSocketChannel.currentIncomingChannelCount);
				}
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Closing channel {0} gracefully.", new object[] { this._chnlName });
			}
			this._socket.Close();
			this._chnlClosedCallback(this);
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x00051124 File Offset: 0x0004F324
		public bool IsOpened
		{
			get
			{
				return this._isClosed == 0;
			}
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00051130 File Offset: 0x0004F330
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
			this._socket.Close();
			this._chnlClosedCallback(this);
			if (this._isIncoming)
			{
				Interlocked.Decrement(ref TcpSocketChannel.currentIncomingChannelCount);
			}
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000511A0 File Offset: 0x0004F3A0
		public override int GetHashCode()
		{
			return this._socket.GetHashCode();
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000511AD File Offset: 0x0004F3AD
		public override string ToString()
		{
			return this._chnlName;
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x000511B5 File Offset: 0x0004F3B5
		public CacheConnectionProperty ConnectionProperty
		{
			get
			{
				return this._cacheConnectionProperty;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x00006F04 File Offset: 0x00005104
		public bool IsSecureChannel
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000E20 RID: 3616
		private TcpChnlClosed _chnlClosedCallback;

		// Token: 0x04000E21 RID: 3617
		private OnMessageReceived _msgReceivedCallback;

		// Token: 0x04000E22 RID: 3618
		private GetRecvBuffers _getBuffers;

		// Token: 0x04000E23 RID: 3619
		private AsyncCallback _endReceiveCallback;

		// Token: 0x04000E24 RID: 3620
		private Socket _socket;

		// Token: 0x04000E25 RID: 3621
		private readonly object _sendLock = new object();

		// Token: 0x04000E26 RID: 3622
		private int _isClosed;

		// Token: 0x04000E27 RID: 3623
		private DateTime _endRecvTime;

		// Token: 0x04000E28 RID: 3624
		private RecvMessageState _recvMessageState;

		// Token: 0x04000E29 RID: 3625
		private TcpTransportProperty _tcpProperty;

		// Token: 0x04000E2A RID: 3626
		private EndPoint _remoteEndpoint;

		// Token: 0x04000E2B RID: 3627
		private IServerSocketProtocol _protocol;

		// Token: 0x04000E2C RID: 3628
		private string _logSource;

		// Token: 0x04000E2D RID: 3629
		private readonly string _chnlName;

		// Token: 0x04000E2E RID: 3630
		private int _currentPacketId;

		// Token: 0x04000E2F RID: 3631
		private int _processedPacketId;

		// Token: 0x04000E30 RID: 3632
		private SortedList<int, KeyValuePair<IList<ArraySegment<byte>>, TcpPacketSendTypes>> _buffersList;

		// Token: 0x04000E31 RID: 3633
		private readonly bool _isIncoming;

		// Token: 0x04000E32 RID: 3634
		private static int totalIncomingChannelCount;

		// Token: 0x04000E33 RID: 3635
		private static int currentIncomingChannelCount;

		// Token: 0x04000E34 RID: 3636
		private readonly CacheConnectionProperty _cacheConnectionProperty;
	}
}
