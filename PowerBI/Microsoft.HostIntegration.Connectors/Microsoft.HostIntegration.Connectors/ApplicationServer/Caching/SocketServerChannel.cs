using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel.Channels;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000191 RID: 401
	internal class SocketServerChannel : SocketTransportChannel, IServerChannel, IDisposable
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0002C26B File Offset: 0x0002A46B
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x0002C273 File Offset: 0x0002A473
		public IServerSocketProtocol Protocol { get; private set; }

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002C27C File Offset: 0x0002A47C
		public SocketServerChannel(ServiceConfigurationManager serviceConfigurationManager, int port, IServerSocketProtocol protocol)
			: this(serviceConfigurationManager, port, protocol, "DistributedCache.SocketServerChannel.")
		{
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002C28C File Offset: 0x0002A48C
		public SocketServerChannel(ServiceConfigurationManager serviceConfigurationManager, int port, IServerSocketProtocol protocol, string logPrefix)
			: base(serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxBufferPoolSize, serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxBufferSize)
		{
			this._serviceConfigurationManager = serviceConfigurationManager;
			this._isEmulatedEnvironment = serviceConfigurationManager.IsEmulatedEnvironment;
			this._nodeName = serviceConfigurationManager.NodeProperties.Name;
			this._serverAcsSecurity = new ServerAcsSecurity(serviceConfigurationManager);
			this.InitializeTransportProperties(serviceConfigurationManager);
			this.InitializeChannel(port, protocol, logPrefix);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0002C300 File Offset: 0x0002A500
		public SocketServerChannel(int port, bool isEmulatedEnvironment, string nodeName, IServerSocketProtocol protocol)
			: base(268435456L, 8388608)
		{
			this._isEmulatedEnvironment = isEmulatedEnvironment;
			this._nodeName = nodeName;
			this._transportProperty = new TcpTransportProperty();
			this._transportProperty.ChnlOpenTimeout = ConfigManager.SERVER_CHANNEL_OPEN_TIMEOUT;
			this._transportProperty.ReceiveTimeout = ConfigManager.RECEIVE_TIMEOUT;
			this._transportProperty.ListenBackLog = 100;
			this._transportProperty.ConnectionBufferSize = -1;
			this._transportProperty.SendTimeout = new TimeSpan(0, 0, 0, 0, ConfigManager.TIMEOUT);
			this.InitializeChannel(port, protocol, "DistributedCache.SocketServerChannel.");
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002C398 File Offset: 0x0002A598
		private void InitializeChannel(int port, IServerSocketProtocol protocol, string logPrefix)
		{
			this._hostPort = port;
			this._logSource = logPrefix + this._hostPort.ToString(CultureInfo.InvariantCulture);
			this.ConnectionVerificationEvent += this.ConnectionVerificationEventHandler;
			this._serverReceiveCallback = new OnMessageReceived(this.OnMessageReceived);
			this._acceptHandler = new AsyncCallback(this.OnAccept);
			this._tcpChannels = new Hashtable();
			long num = (long)this._transportProperty.ReceiveTimeout.TotalMilliseconds / 2L;
			if (num > 2147483647L)
			{
				num = 2147483647L;
			}
			this._timer = new global::System.Threading.Timer(new TimerCallback(this.RemoveIdleChannels), null, num, num);
			this.Protocol = protocol;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0002C454 File Offset: 0x0002A654
		public void StartListening()
		{
			this.BindToSocket();
			this._listenerSocket.Listen(this._transportProperty.ListenBackLog);
			this.StartAccept();
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Server channel opened", new object[0]);
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0002C4A4 File Offset: 0x0002A6A4
		private void RemoveIdleChannels(object state)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "RemoveIdleChannels called", new object[0]);
			}
			HashSet<ITcpChannel> hashSet = new HashSet<ITcpChannel>();
			lock (this._tcpChannels)
			{
				foreach (object obj in this._tcpChannels)
				{
					ITcpChannel tcpChannel = (ITcpChannel)((DictionaryEntry)obj).Key;
					if (tcpChannel.EndReceiveTime < DateTime.UtcNow)
					{
						hashSet.Add(tcpChannel);
					}
				}
			}
			foreach (ITcpChannel tcpChannel2 in hashSet)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(this._logSource, "Idle {0} channel is cleanedup", new object[] { tcpChannel2 });
				}
				tcpChannel2.CloseGracefully();
			}
			hashSet.Clear();
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0002C5E0 File Offset: 0x0002A7E0
		private void InitializeTransportProperties(ServiceConfigurationManager configurationManager)
		{
			this._transportProperty = new TcpTransportProperty();
			TransportElement transportProperties = configurationManager.AdvancedProperties.TransportProperties;
			if (transportProperties.ChannelInitializationTimeout != -1)
			{
				this._transportProperty.ChnlOpenTimeout = new TimeSpan(0, 0, 0, 0, transportProperties.ChannelInitializationTimeout);
			}
			else
			{
				this._transportProperty.ChnlOpenTimeout = ConfigManager.SERVER_CHANNEL_OPEN_TIMEOUT;
			}
			if (transportProperties.ReceiveTimeout != -1)
			{
				if ((long)transportProperties.ReceiveTimeout * 10000L > TcpUtility.MaxTimeout.Ticks)
				{
					this._transportProperty.ReceiveTimeout = TcpUtility.MaxTimeout;
				}
				else
				{
					this._transportProperty.ReceiveTimeout = new TimeSpan(0, 0, 0, 0, transportProperties.ReceiveTimeout);
				}
			}
			else
			{
				this._transportProperty.ReceiveTimeout = ConfigManager.RECEIVE_TIMEOUT;
			}
			this._transportProperty.SendTimeout = new TimeSpan(0, 0, 0, 0, ConfigManager.TIMEOUT);
			if (transportProperties.ListenBacklog != -1)
			{
				this._transportProperty.ListenBackLog = transportProperties.ListenBacklog;
			}
			else
			{
				this._transportProperty.ListenBackLog = 100;
			}
			if (transportProperties.ConnectionBufferSize != -1)
			{
				this._transportProperty.ConnectionBufferSize = transportProperties.ConnectionBufferSize;
			}
			else
			{
				this._transportProperty.ConnectionBufferSize = -1;
			}
			this._transportProperty.SendTimeout = new TimeSpan(0, 0, 0, 0, ConfigManager.TIMEOUT);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002C720 File Offset: 0x0002A920
		private void BindToSocket()
		{
			IPEndPoint ipendPoint;
			if (Socket.OSSupportsIPv6)
			{
				IPAddress ipAddress = this.GetIpAddress(IPAddress.IPv6Any);
				ipendPoint = new IPEndPoint(ipAddress, this._hostPort);
				this._listenerSocket = new Socket(ipendPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				this._listenerSocket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, 0);
			}
			else
			{
				IPAddress ipAddress2 = this.GetIpAddress(IPAddress.Any);
				ipendPoint = new IPEndPoint(ipAddress2, this._hostPort);
				this._listenerSocket = new Socket(ipendPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			}
			this._listenerSocket.Bind(ipendPoint);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0002C7AC File Offset: 0x0002A9AC
		private IPAddress GetIpAddress(IPAddress defaultValue)
		{
			if (this._isEmulatedEnvironment)
			{
				IPAddress[] hostAddresses = Dns.GetHostAddresses(this._nodeName);
				ReleaseAssert.IsTrue(hostAddresses.Length == 1);
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(this._logSource, " ********** Opening Channel for IP ADDRESS - {0}", new object[] { hostAddresses[0].ToString() });
				}
				return hostAddresses[0];
			}
			return defaultValue;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002C808 File Offset: 0x0002AA08
		private void OnAccept(IAsyncResult asyncResult)
		{
			if (asyncResult.CompletedSynchronously)
			{
				return;
			}
			this.CompleteAccept(asyncResult);
			this.StartAccept();
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0002C820 File Offset: 0x0002AA20
		protected virtual void CompleteAccept(IAsyncResult asyncResult)
		{
			Socket socket = null;
			try
			{
				socket = this._listenerSocket.EndAccept(asyncResult);
				ITcpChannel tcpChannel;
				if (this._serviceConfigurationManager != null)
				{
					tcpChannel = new TcpSocketChannel(socket, this._serviceConfigurationManager.AdvancedProperties.DnsDomain);
				}
				else
				{
					tcpChannel = new TcpSocketChannel(socket, true);
				}
				tcpChannel.Initialize(new TcpChnlClosed(this.OnTcpChnlClosed), this._serverReceiveCallback, new GetRecvBuffers(this.GetBuffers), this._transportProperty, this.Protocol, "DistributedCache.SocketServerChannel.");
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(this._logSource, "ChannelID = {0} Connection opened.", new object[] { tcpChannel.ToString() });
				}
				lock (this._tcpChannels)
				{
					this._tcpChannels.Add(tcpChannel, DateTime.UtcNow.Ticks);
				}
				this.OnConnectionCreated();
				tcpChannel.ReceiveMessage();
			}
			catch (Exception ex)
			{
				if (socket != null)
				{
					socket.Close();
				}
				if (!TcpUtility.HandleException(this._logSource, ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0002C94C File Offset: 0x0002AB4C
		private void StartAccept()
		{
			try
			{
				IL_0000:
				while (!this._isClosed)
				{
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose(this._logSource, "New Accept is being poseted");
					}
					IAsyncResult asyncResult = this._listenerSocket.BeginAccept(this._acceptHandler, null);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteAccept(asyncResult);
				}
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(this._logSource, "Server is closed, no more accepts", new object[0]);
				}
			}
			catch (Exception ex)
			{
				if (!TcpUtility.HandleException(this._logSource, ex))
				{
					throw;
				}
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning(this._logSource, "Begin Accept failed, expected only when channel is closed {0}.", new object[] { this._isClosed });
				}
				goto IL_0000;
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0002CA1C File Offset: 0x0002AC1C
		private void OnMessageReceived(IReplyContext context)
		{
			if (this._receiveCallback != null)
			{
				if (context.PacketException == null && this._serviceConfigurationManager != null && this._serviceConfigurationManager.AdvancedProperties.SecurityProperties.UseAcsForClient)
				{
					CacheConnectionProperty connectionProperty = context.ConnectionProperty;
					ReleaseAssert.IsTrue(connectionProperty != null);
					string messageAuthorizationToken = context.MessageAuthorizationToken;
					if (messageAuthorizationToken == null || messageAuthorizationToken == connectionProperty.AuthenticationToken)
					{
						if (connectionProperty.AcsValidTill < DateTime.UtcNow)
						{
							if (connectionProperty.AuthenticationToken == null)
							{
								throw new VelocityAuthorizationException(ErrStatus.AUTH_HEADER_INVALID);
							}
							throw new VelocityAuthorizationException(ErrStatus.AUTH_HEADER_EXPIRED);
						}
					}
					else
					{
						DateTime dateTime;
						ErrStatus errStatus;
						if (!this._serverAcsSecurity.IsHeaderValid(messageAuthorizationToken, out dateTime, out errStatus))
						{
							connectionProperty.AcsValidTill = DateTime.MinValue;
							throw new VelocityAuthorizationException(errStatus);
						}
						connectionProperty.AcsValidTill = dateTime;
						connectionProperty.AuthenticationToken = messageAuthorizationToken;
						if (Provider.IsEnabled(TraceLevel.Verbose))
						{
							EventLogWriter.WriteVerbose<object, DateTime, string>(this._logSource, "Channel '{0}' authenticated until {1} with token '{2}'", context.State, dateTime, messageAuthorizationToken);
						}
					}
				}
				DiagOperation.ChannelName = (context.State ?? (-1)).ToString();
				this._receiveCallback(context);
				return;
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<object>(this._logSource, "Message dropped as there is no callback registered", context.State);
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0002CB54 File Offset: 0x0002AD54
		protected void OnTcpChnlClosed(ITcpChannel chnl)
		{
			lock (this._tcpChannels)
			{
				this._tcpChannels.Remove(chnl);
			}
			if (this.ConnectionDestroyedEvent != null)
			{
				this.ConnectionDestroyedEvent(null);
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00003CAB File Offset: 0x00001EAB
		private void ConnectionVerificationEventHandler(IEnumerable<IDuplexSessionChannel> verifyingChannels)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002CBB0 File Offset: 0x0002ADB0
		protected IEnumerable<ArraySegment<byte>> GetBuffers(out IList<IVelocityPacket> packets)
		{
			packets = new List<IVelocityPacket>();
			return this.Protocol.GetReadRequestBuffer(packets, SocketTransportChannel.BufferManager);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0002CBCB File Offset: 0x0002ADCB
		protected void OnConnectionCreated()
		{
			if (this.ConnectionCreatedEvent != null)
			{
				this.ConnectionCreatedEvent(null);
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000D01 RID: 3329 RVA: 0x0002CBE4 File Offset: 0x0002ADE4
		// (remove) Token: 0x06000D02 RID: 3330 RVA: 0x0002CC1C File Offset: 0x0002AE1C
		public event ConnectionCreatedEventHandler ConnectionCreatedEvent;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000D03 RID: 3331 RVA: 0x0002CC54 File Offset: 0x0002AE54
		// (remove) Token: 0x06000D04 RID: 3332 RVA: 0x0002CC8C File Offset: 0x0002AE8C
		public event ConnectionDestroyedEventHandler ConnectionDestroyedEvent;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000D05 RID: 3333 RVA: 0x000036A9 File Offset: 0x000018A9
		// (remove) Token: 0x06000D06 RID: 3334 RVA: 0x000036A9 File Offset: 0x000018A9
		public event ConnectionVerificationEventHandler ConnectionVerificationEvent
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void RegisterDefaultReceiveCallback(OnMessageReceived callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void UnregisterDefaultReceiveCallback(OnMessageReceived callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void UnregisterDefaultReceiveCallback()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void RegisterRequestReplyCallback(string action, RequestReply callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void UnregisterRequestReplyCallback(string action, RequestReply callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void UnregisterRequestReplyCallback(string action)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002CCC1 File Offset: 0x0002AEC1
		public void StartThrottle()
		{
			this._throttled = true;
			this.ThrottleConnections();
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002CCD0 File Offset: 0x0002AED0
		private void ThrottleConnections()
		{
			IEnumerable<ITcpChannel> enumerable = null;
			int num = 0;
			if (this._throttled)
			{
				lock (this._tcpChannels)
				{
					if (this._throttled && this._tcpChannels.Count > this._serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxConnectionsHigh)
					{
						int maxConnectionsLow = this._serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxConnectionsLow;
						num = this._tcpChannels.Count - maxConnectionsLow;
						enumerable = Utility.GetChannelsToCleanup<ITcpChannel>(this._tcpChannels, maxConnectionsLow);
					}
				}
				if (enumerable != null)
				{
					foreach (ITcpChannel tcpChannel in enumerable)
					{
						if (Provider.IsEnabled(TraceLevel.Info))
						{
							EventLogWriter.WriteInfo(this._logSource, "Connection is closed because of throttling {0}", new object[] { tcpChannel });
						}
						tcpChannel.CloseGracefully();
					}
					this._throttledConnectionsCount += num;
				}
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002CDEC File Offset: 0x0002AFEC
		public void StopThrottle()
		{
			this._throttled = false;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void EnablePeriodicConnectionVerification()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002CDF5 File Offset: 0x0002AFF5
		public long GetTotalConnectionsCount()
		{
			return (long)this._tcpChannels.Count;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002CE03 File Offset: 0x0002B003
		public long GetThrottledConnectionsCount()
		{
			return (long)this._throttledConnectionsCount;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002CE0C File Offset: 0x0002B00C
		public void Dispose()
		{
			if (this._isClosed)
			{
				return;
			}
			this._isClosed = true;
			if (this._listenerSocket != null)
			{
				this._listenerSocket.Close();
			}
			Hashtable hashtable;
			lock (this._tcpChannels)
			{
				hashtable = new Hashtable(this._tcpChannels);
			}
			foreach (object obj in hashtable)
			{
				ITcpChannel tcpChannel = (ITcpChannel)((DictionaryEntry)obj).Key;
				tcpChannel.CloseGracefully();
			}
			if (this._timer != null)
			{
				this._timer.Dispose();
			}
			hashtable.Clear();
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Socket Server is closed", new object[0]);
			}
		}

		// Token: 0x0400091A RID: 2330
		private const string _logSourcePrefix = "DistributedCache.SocketServerChannel.";

		// Token: 0x0400091B RID: 2331
		private int _hostPort;

		// Token: 0x0400091C RID: 2332
		private AsyncCallback _acceptHandler;

		// Token: 0x0400091D RID: 2333
		private global::System.Threading.Timer _timer;

		// Token: 0x0400091E RID: 2334
		private bool _throttled;

		// Token: 0x0400091F RID: 2335
		private int _throttledConnectionsCount;

		// Token: 0x04000920 RID: 2336
		private bool _isEmulatedEnvironment;

		// Token: 0x04000921 RID: 2337
		private string _nodeName;

		// Token: 0x04000922 RID: 2338
		private volatile bool _isClosed;

		// Token: 0x04000923 RID: 2339
		private readonly ServerAcsSecurity _serverAcsSecurity;

		// Token: 0x04000924 RID: 2340
		protected Socket _listenerSocket;

		// Token: 0x04000925 RID: 2341
		protected Hashtable _tcpChannels;

		// Token: 0x04000926 RID: 2342
		protected TcpTransportProperty _transportProperty;

		// Token: 0x04000927 RID: 2343
		protected OnMessageReceived _serverReceiveCallback;

		// Token: 0x04000928 RID: 2344
		protected readonly ServiceConfigurationManager _serviceConfigurationManager;
	}
}
