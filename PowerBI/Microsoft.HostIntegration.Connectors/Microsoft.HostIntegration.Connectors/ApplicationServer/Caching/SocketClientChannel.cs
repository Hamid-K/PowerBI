using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002C9 RID: 713
	internal sealed class SocketClientChannel : SocketTransportChannel, IClientChannel, IDisposable
	{
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06001A54 RID: 6740 RVA: 0x0004F95C File Offset: 0x0004DB5C
		// (remove) Token: 0x06001A55 RID: 6741 RVA: 0x0004F994 File Offset: 0x0004DB94
		internal event Action ChannelCreatedEvent;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06001A56 RID: 6742 RVA: 0x0004F9CC File Offset: 0x0004DBCC
		// (remove) Token: 0x06001A57 RID: 6743 RVA: 0x0004FA04 File Offset: 0x0004DC04
		internal event Action ConnectionClosedEvent;

		// Token: 0x06001A58 RID: 6744 RVA: 0x0004FA3C File Offset: 0x0004DC3C
		public SocketClientChannel(DataCacheTransportProperties transportProperties, int maxChannelCount, TimeSpan chnlOpenTimeout, TimeSpan sendTimeout, string id, DataCacheSecurity securityProperties)
			: base(transportProperties.MaxBufferPoolSize, transportProperties.MaxBufferSize)
		{
			this.InitializeTransportProperty(transportProperties, sendTimeout, chnlOpenTimeout);
			this._maxChannelCount = maxChannelCount;
			this._tcpChnlClosed = new TcpChnlClosed(this.OnTcpChnlClosed);
			this._tcpChnlOpened = new TcpChnlOpened(this.OnTcpChnlOpened);
			this._logSource = "DistributedCache.SocketClientChannel." + id;
			this.ConnectionClosedEvent += NetworkStatistics.OnConnectionClosed;
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				this.ChannelCreatedEvent += ClientPerfCounterUpdate.OnChannelCreated;
				this.ConnectionClosedEvent += ClientPerfCounterUpdate.OnConnectionsClosed;
			}
			this._dataCacheSecurityProperties = securityProperties;
			this._factory = this.CreateClientChannelFactory(id);
			this._receiveCallback = new OnMessageReceived(this.OnMessageReceived);
			this.Protocol = new VelocityWireProtocol();
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Socket Client Channel created", new object[0]);
			}
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0004FB48 File Offset: 0x0004DD48
		private IClientChannelFactory CreateClientChannelFactory(string logPrefix)
		{
			SocketConnectionFactory socketConnectionFactory = new SocketConnectionFactory(logPrefix);
			IClientChannelFactory clientChannelFactory;
			if (this._dataCacheSecurityProperties.IsChannelSecurityNeeded)
			{
				clientChannelFactory = new TcpSecureClientChannelFactory(this._tcpChnlClosed, this._tcpChnlOpened, new OnMessageReceived(this.OnMessageReceived), new GetRecvBuffers(this.GetRecvBuffers), socketConnectionFactory, this._dataCacheSecurityProperties, this._transportProperty, logPrefix);
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose(this._logSource, "Created a Secure Client Channel Factory");
				}
			}
			else
			{
				clientChannelFactory = new TcpClientChannelFactory(this._tcpChnlClosed, this._tcpChnlOpened, new OnMessageReceived(this.OnMessageReceived), new GetRecvBuffers(this.GetRecvBuffers), socketConnectionFactory, this._transportProperty, logPrefix);
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose(this._logSource, "Created a Client Channel Factory");
				}
			}
			return clientChannelFactory;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0004FC08 File Offset: 0x0004DE08
		private void InitializeTransportProperty(DataCacheTransportProperties transportProperties, TimeSpan sendTimeout, TimeSpan chnlOpenTimeout)
		{
			this._transportProperty = new TcpTransportProperty();
			this._transportProperty.ReceiveTimeout = ConfigManager.RECEIVE_TIMEOUT;
			if ((int)transportProperties.ReceiveTimeout.TotalMilliseconds != DataCacheTransportProperties.NOT_ASSIGNED)
			{
				if (transportProperties.ReceiveTimeout > TcpUtility.MaxTimeout)
				{
					this._transportProperty.ReceiveTimeout = TcpUtility.MaxTimeout;
				}
				else
				{
					this._transportProperty.ReceiveTimeout = transportProperties.ReceiveTimeout;
				}
			}
			if (sendTimeout > TcpUtility.MaxTimeout)
			{
				sendTimeout = TcpUtility.MaxTimeout;
			}
			this._transportProperty.SendTimeout = sendTimeout;
			this._transportProperty.ChnlOpenTimeout = chnlOpenTimeout;
			this._transportProperty.ConnectionBufferSize = transportProperties.ConnectionBufferSize;
			this._transportProperty.NoDelay = true;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0004FCC4 File Offset: 0x0004DEC4
		internal void OnMessageReceived(IReplyContext replyContext)
		{
			if (this._receiveCallback != null)
			{
				this._receiveCallback(replyContext);
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0004FCDA File Offset: 0x0004DEDA
		// (set) Token: 0x06001A5D RID: 6749 RVA: 0x0004FCE2 File Offset: 0x0004DEE2
		public IClientSocketProtocol Protocol { get; private set; }

		// Token: 0x06001A5E RID: 6750 RVA: 0x0004FCEC File Offset: 0x0004DEEC
		private TcpClientChannel GetTcpChannel(Uri host)
		{
			TcpChannelList tcpChannelList;
			if (!this._connections.Contains(host))
			{
				tcpChannelList = this.CreateChannelList(host);
			}
			else
			{
				tcpChannelList = (TcpChannelList)this._connections[host];
			}
			TcpClientChannel channel = tcpChannelList.GetChannel();
			ITcpChannel tcpChannel = channel.TcpChannel;
			if (TcpUtility.GetDeadlineSafe(this._transportProperty.ReceiveTimeout, channel.LastSendTime) < DateTime.UtcNow && tcpChannel != null)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(this._logSource, "Client Channel is idle , being closed {0}", new object[] { tcpChannel });
				}
				tcpChannel.CloseGracefully();
			}
			return channel;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x0004FD84 File Offset: 0x0004DF84
		private OperationResult TryGetTcpChannelUntilTimeout(TcpClientChannel clientChannel, out ITcpChannel chnl)
		{
			TimeSpan chnlOpenTimeout = this._transportProperty.ChnlOpenTimeout;
			clientChannel.ConnectAsync();
			OperationResult operationResult;
			if (clientChannel.ConnectionWaitHandle.WaitOne(chnlOpenTimeout, false))
			{
				chnl = clientChannel.TcpChannel;
				if (chnl != null)
				{
					operationResult = new OperationResult(OperationStatus.Success);
				}
				else
				{
					if (Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo(this._logSource, "Channel open to host {0} failed with exception {1}", new object[] { clientChannel.RemoteUri, clientChannel.LastException });
					}
					OperationStatus channelOpenFailureResponseCode = TcpUtility.GetChannelOpenFailureResponseCode(clientChannel.LastException);
					operationResult = new OperationResult(channelOpenFailureResponseCode, clientChannel.LastException);
				}
			}
			else
			{
				chnl = null;
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(this._logSource, "Channel open to host {0} didn't complete in time {1}", new object[] { clientChannel.RemoteUri, chnlOpenTimeout });
				}
				operationResult = new OperationResult(OperationStatus.ChannelOpening);
			}
			return operationResult;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0004FE5A File Offset: 0x0004E05A
		private IEnumerable<ArraySegment<byte>> GetRecvBuffers(out IList<IVelocityPacket> packets)
		{
			packets = new List<IVelocityPacket>();
			return this.Protocol.GetReadResponseBuffer(packets, SocketTransportChannel.BufferManager);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0004FE78 File Offset: 0x0004E078
		private TcpChannelList CreateChannelList(Uri host)
		{
			TcpChannelList tcpChannelList;
			lock (this._lock)
			{
				if (this._connections.Contains(host))
				{
					tcpChannelList = (TcpChannelList)this._connections[host];
				}
				else
				{
					TcpClientChannel[] array = new TcpClientChannel[this._maxChannelCount];
					for (int i = 0; i < this._maxChannelCount; i++)
					{
						array[i] = new TcpClientChannel(host, this._factory);
						array[i].ConnectAsync();
					}
					TcpChannelList tcpChannelList2 = new TcpChannelList(array);
					this._connections[host] = tcpChannelList2;
					tcpChannelList = tcpChannelList2;
				}
			}
			return tcpChannelList;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0004FF24 File Offset: 0x0004E124
		private void OnTcpChnlClosed(ITcpChannel chnl)
		{
			if (this._deadServerCallback != null)
			{
				if (this.ConnectionClosedEvent != null)
				{
					this.ConnectionClosedEvent();
				}
				EndpointID endpointID = new EndpointID(EndpointID.TcpIdentifier + chnl.RemoteEndpoint.ToString());
				this._deadServerCallback(endpointID, chnl.GetHashCode(), null);
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0004FF7F File Offset: 0x0004E17F
		private void OnTcpChnlOpened()
		{
			if (this.ChannelCreatedEvent != null)
			{
				this.ChannelCreatedEvent();
			}
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0004FF94 File Offset: 0x0004E194
		public OperationResult Send(EndpointID endpoint, ICreateMessage message)
		{
			RequestBody requestBody = message as RequestBody;
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, " Socket Send invoked for request {0}", new object[] { requestBody });
			}
			IVelocityRequestPacket requestPacket = VelocityWireProtocol.GetRequestPacket(requestBody);
			OperationResult operationResult = this.Send(endpoint, requestPacket);
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, " Send status {0}", new object[] { operationResult });
			}
			return operationResult;
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00003CAB File Offset: 0x00001EAB
		public OperationResult Send(EndpointID endpoint, ICreateMessage message, TimeSpan timeout)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00003CAB File Offset: 0x00001EAB
		public OperationResult AsyncSend(EndpointID endpoint, ICreateMessage message, WaitCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00003CAB File Offset: 0x00001EAB
		public OperationResult AsyncSend(EndpointID endpoint, ICreateMessage message, TimeSpan timeout, WaitCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00003CAB File Offset: 0x00001EAB
		public OperationResult SendReceive(EndpointID endpoint, ICreateMessage request, out Message response)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00003CAB File Offset: 0x00001EAB
		public OperationResult SendReceive(EndpointID endpoint, ICreateMessage request, TimeSpan timeout, out Message response)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void RegisterDefaultReceiveCallback(OnMessageReceived callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void UnregisterDefaultReceiveCallback(OnMessageReceived callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void UnregisterDefaultReceiveCallback()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00050004 File Offset: 0x0004E204
		public void RegisterDeadCallback(OnRemoteGoingDown callback)
		{
			this._deadServerCallback = callback;
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void UnregisterDeadCallback(OnRemoteGoingDown callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void UnregisterDeadCallback()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00050010 File Offset: 0x0004E210
		public void Dispose()
		{
			if (this._isClosed)
			{
				return;
			}
			this._isClosed = true;
			foreach (object obj in this._connections.Values)
			{
				TcpChannelList tcpChannelList = (TcpChannelList)obj;
				tcpChannelList.Close();
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00050080 File Offset: 0x0004E280
		private OperationResult Send(EndpointID endpoint, IVelocityRequestPacket packet)
		{
			ReleaseAssert.IsTrue(packet != null, "Packet should never be null in SocketClientChannel.cs. RequestBody used over socket path in test code?");
			TcpClientChannel tcpChannel = this.GetTcpChannel(endpoint.URI);
			ITcpChannel tcpChannel2 = tcpChannel.TcpChannel;
			try
			{
				if (tcpChannel2 == null || !tcpChannel2.IsOpened)
				{
					if (tcpChannel2 != null && Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo(this._logSource, "The socket channel is closed but not cleared. Opening afresh. {0}", new object[] { tcpChannel2 });
					}
					OperationResult operationResult = this.TryGetTcpChannelUntilTimeout(tcpChannel, out tcpChannel2);
					if (operationResult.Status != OperationStatus.Success)
					{
						if (Provider.IsEnabled(TraceLevel.Warning))
						{
							EventLogWriter.WriteWarning(this._logSource, "Request {0} to host {1} failed {2}", new object[] { packet.MessageID, tcpChannel.RemoteUri, operationResult });
						}
						return operationResult;
					}
				}
			}
			catch (ObjectDisposedException ex)
			{
				return new OperationResult(OperationStatus.InstanceClosed, ex);
			}
			try
			{
				IAuthorizedChannel authorizedChannel = tcpChannel2 as IAuthorizedChannel;
				if (!this.TryAuthorizeMessage(authorizedChannel, packet))
				{
					return new OperationResult(OperationStatus.AuthorizationFailed);
				}
			}
			catch (DataCacheException ex2)
			{
				return new OperationResult(OperationStatus.AuthorizationFailed, ex2);
			}
			tcpChannel.LastSendTime = DateTime.UtcNow;
			IList<AllocationType> list = null;
			IList<ArraySegment<byte>> list2 = null;
			OperationResult operationResult3;
			try
			{
				list2 = VelocityWireProtocol.GetWriteRequestBuffer(packet, SocketTransportChannel.BufferManager, out list);
				OperationResult operationResult2 = tcpChannel2.Send(list2);
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(this._logSource, "Request {0} to host {1} SendStatus {2}, Channel {3} ", new object[] { packet.MessageID, tcpChannel.RemoteUri, operationResult2, tcpChannel2 });
				}
				operationResult3 = operationResult2;
			}
			catch (VelocityPacketFormatFatalException ex3)
			{
				operationResult3 = new OperationResult(OperationStatus.MessageLargerThanConfiguredSize, ex3);
			}
			finally
			{
				if (list2 != null)
				{
					VelocityWireProtocol.ReleaseMemory(SocketTransportChannel.BufferManager, list2, list);
				}
			}
			return operationResult3;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00050258 File Offset: 0x0004E458
		public void MarkAuthorizationTokenInvalid(string currentToken)
		{
			if (this._dataCacheSecurityProperties.SecurityMode == DataCacheSecurityMode.Message && this._dataCacheSecurityProperties.AuthorizationType == AuthorizationType.Token)
			{
				this._dataCacheSecurityProperties.AcsTokenManager.ForceAuthRetry(currentToken);
			}
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00050288 File Offset: 0x0004E488
		private bool TryAuthorizeMessage(IAuthorizedChannel channel, IVelocityRequestPacket packet)
		{
			if (this._dataCacheSecurityProperties.SecurityMode == DataCacheSecurityMode.None)
			{
				return true;
			}
			switch (this._dataCacheSecurityProperties.AuthorizationType)
			{
			case AuthorizationType.None:
				return true;
			case AuthorizationType.Token:
			{
				if (this._dataCacheSecurityProperties.SecurityMode != DataCacheSecurityMode.Message)
				{
					return false;
				}
				string orRefreshToken = this._dataCacheSecurityProperties.AcsTokenManager.GetOrRefreshToken(this._transportProperty.SendTimeout);
				channel.ConnectionProperty.AuthenticationToken = orRefreshToken;
				packet.PropertyBag.SetAuthorizationToken(orRefreshToken);
				return true;
			}
			default:
				return false;
			}
		}

		// Token: 0x04000E08 RID: 3592
		private object _lock = new object();

		// Token: 0x04000E09 RID: 3593
		private readonly int _maxChannelCount;

		// Token: 0x04000E0A RID: 3594
		private readonly Hashtable _connections = new Hashtable();

		// Token: 0x04000E0B RID: 3595
		private readonly TcpChnlClosed _tcpChnlClosed;

		// Token: 0x04000E0C RID: 3596
		private readonly TcpChnlOpened _tcpChnlOpened;

		// Token: 0x04000E0D RID: 3597
		private TcpTransportProperty _transportProperty;

		// Token: 0x04000E0E RID: 3598
		private readonly IClientChannelFactory _factory;

		// Token: 0x04000E0F RID: 3599
		private OnRemoteGoingDown _deadServerCallback;

		// Token: 0x04000E10 RID: 3600
		private bool _isClosed;

		// Token: 0x04000E11 RID: 3601
		private readonly DataCacheSecurity _dataCacheSecurityProperties;
	}
}
