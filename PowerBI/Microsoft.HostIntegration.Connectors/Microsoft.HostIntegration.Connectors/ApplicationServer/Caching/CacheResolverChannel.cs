using System;
using System.Diagnostics;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200029D RID: 669
	internal class CacheResolverChannel : IDuplexSessionChannel, IDuplexChannel, IInputChannel, IOutputChannel, IChannel, ICommunicationObject, ISessionChannel<global::System.ServiceModel.Channels.IDuplexSession>
	{
		// Token: 0x0600185A RID: 6234 RVA: 0x00049C78 File Offset: 0x00047E78
		public CacheResolverChannel(CacheResolverCommunicatorType communicatorType, IDuplexSessionChannel innerChannel, DataCacheSecurity dataCacheSecurity)
		{
			this._openDelegate = new CacheResolverChannel.OpenDelegate(this.Open);
			this._innerChannel = innerChannel;
			this._communicatorType = communicatorType;
			this._dataCacheSecurity = dataCacheSecurity;
			this._desiredRoutingChannelProps = new CloudRoutingChannelProperties();
			if (this._dataCacheSecurity != null && this._dataCacheSecurity.SecurityMode == DataCacheSecurityMode.Message && this._dataCacheSecurity.AuthorizationType == AuthorizationType.Token)
			{
				string text = null;
				try
				{
					text = this._dataCacheSecurity.AcsTokenManager.GetOrRefreshToken(ConfigManager.CLIENT_CHANNEL_OPEN_TIMEOUT);
				}
				catch (DataCacheException ex)
				{
					if (ex.ErrorCode == 30 || ex.ErrorCode == 19002)
					{
						throw new CommunicationException(ex.Message, ex);
					}
					throw;
				}
				ReleaseAssert.IsTrue(!string.IsNullOrEmpty(text));
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00049D5C File Offset: 0x00047F5C
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerChannel.BeginReceive(timeout, callback, state);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00049D6C File Offset: 0x00047F6C
		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			return this._innerChannel.BeginReceive(callback, state);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00049D7B File Offset: 0x00047F7B
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerChannel.BeginTryReceive(timeout, callback, state);
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00049D8B File Offset: 0x00047F8B
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerChannel.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00049D9B File Offset: 0x00047F9B
		public Message EndReceive(IAsyncResult result)
		{
			return this._innerChannel.EndReceive(result);
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00049DA9 File Offset: 0x00047FA9
		public bool EndTryReceive(IAsyncResult result, out Message message)
		{
			return this._innerChannel.EndTryReceive(result, out message);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00049DB8 File Offset: 0x00047FB8
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return this._innerChannel.EndWaitForMessage(result);
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x00049DC6 File Offset: 0x00047FC6
		public EndpointAddress LocalAddress
		{
			get
			{
				return this._innerChannel.LocalAddress;
			}
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00049DD3 File Offset: 0x00047FD3
		public Message Receive(TimeSpan timeout)
		{
			return this._innerChannel.Receive(timeout);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00049DE1 File Offset: 0x00047FE1
		public Message Receive()
		{
			return this._innerChannel.Receive();
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00049DEE File Offset: 0x00047FEE
		public bool TryReceive(TimeSpan timeout, out Message message)
		{
			return this._innerChannel.TryReceive(timeout, out message);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00049DFD File Offset: 0x00047FFD
		public bool WaitForMessage(TimeSpan timeout)
		{
			return this._innerChannel.WaitForMessage(timeout);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00049E0C File Offset: 0x0004800C
		public T GetProperty<T>() where T : class
		{
			if (typeof(T) == typeof(CacheConnectionProperty))
			{
				return (T)((object)this._cacheResolver);
			}
			if (typeof(T) == typeof(CloudRoutingChannelProperties))
			{
				return (T)((object)this._desiredRoutingChannelProps);
			}
			if (typeof(T) == typeof(RemoteEndpoint))
			{
				return (T)((object)this._remoteEndpoint);
			}
			return this._innerChannel.GetProperty<T>();
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00049E99 File Offset: 0x00048099
		public void Abort()
		{
			this._innerChannel.Abort();
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00003CAB File Offset: 0x00001EAB
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x00003CAB File Offset: 0x00001EAB
		public IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00049EA6 File Offset: 0x000480A6
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._openDelegate.BeginInvoke(timeout, callback, state);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00049EB6 File Offset: 0x000480B6
		public IAsyncResult BeginOpen(AsyncCallback callback, object state)
		{
			return this.BeginOpen(ConfigManager.CLIENT_CHANNEL_OPEN_TIMEOUT, callback, state);
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00049EC5 File Offset: 0x000480C5
		public void Close(TimeSpan timeout)
		{
			this._innerChannel.Close(timeout);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00049ED3 File Offset: 0x000480D3
		public void Close()
		{
			this._innerChannel.Close();
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600186F RID: 6255 RVA: 0x00049EE0 File Offset: 0x000480E0
		// (remove) Token: 0x06001870 RID: 6256 RVA: 0x00049EEE File Offset: 0x000480EE
		public event EventHandler Closed
		{
			add
			{
				this._innerChannel.Closed += value;
			}
			remove
			{
				this._innerChannel.Closed -= value;
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06001871 RID: 6257 RVA: 0x00049EFC File Offset: 0x000480FC
		// (remove) Token: 0x06001872 RID: 6258 RVA: 0x00049F0A File Offset: 0x0004810A
		public event EventHandler Closing
		{
			add
			{
				this._innerChannel.Closing += value;
			}
			remove
			{
				this._innerChannel.Closing -= value;
			}
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void EndClose(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00049F18 File Offset: 0x00048118
		public void EndOpen(IAsyncResult result)
		{
			this._openDelegate.EndInvoke(result);
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06001875 RID: 6261 RVA: 0x00049F26 File Offset: 0x00048126
		// (remove) Token: 0x06001876 RID: 6262 RVA: 0x00049F34 File Offset: 0x00048134
		public event EventHandler Faulted
		{
			add
			{
				this._innerChannel.Faulted += value;
			}
			remove
			{
				this._innerChannel.Faulted -= value;
			}
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00049F44 File Offset: 0x00048144
		public void Open(TimeSpan timeout)
		{
			TimeSpan timeSpan = timeout;
			DateTime now = DateTime.Now;
			this._innerChannel.Open(timeout);
			if (timeSpan != TimeSpan.MaxValue)
			{
				timeSpan -= DateTime.Now - now;
			}
			this.PerformCacheResolutionSequence(timeSpan);
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00049F8C File Offset: 0x0004818C
		private void PerformCacheResolutionSequence(TimeSpan timeout)
		{
			Message message = null;
			try
			{
				if (this._communicatorType == CacheResolverCommunicatorType.Client)
				{
					MessageVersion property = this._innerChannel.GetProperty<MessageVersion>();
					string text = "cache://Microsoft.ApplicationServer.Caching/Resolution";
					message = Message.CreateMessage(property, text);
					if (this._desiredRoutingChannelProps != null && !string.IsNullOrEmpty(this._desiredRoutingChannelProps.DestinationHostAddress))
					{
						MessageHeader messageHeader = MessageHeader.CreateHeader("DHint", "urn:AppFabricCaching", this._desiredRoutingChannelProps, true);
						message.Headers.Add(messageHeader);
					}
					if (this._dataCacheSecurity != null && this._dataCacheSecurity.SecurityMode == DataCacheSecurityMode.Message && this._dataCacheSecurity.AuthorizationType != AuthorizationType.None)
					{
						try
						{
							TransportUtility.AddAuthenticationHeader(message, this._dataCacheSecurity, timeout);
						}
						catch (DataCacheException ex)
						{
							if (ex.ErrorCode == 30 || ex.ErrorCode == 19002)
							{
								throw new CommunicationException(ex.Message, ex);
							}
							throw;
						}
					}
					if (timeout == TimeSpan.MaxValue)
					{
						this._innerChannel.Send(message);
					}
					else
					{
						this._innerChannel.Send(message, timeout);
					}
				}
				else if (this._communicatorType == CacheResolverCommunicatorType.Service)
				{
					if (timeout == TimeSpan.MaxValue)
					{
						message = this._innerChannel.Receive();
					}
					else
					{
						message = this._innerChannel.Receive(timeout);
					}
					if (message == null)
					{
						throw new CommunicationException(string.Format(CultureInfo.InvariantCulture, "Terminating connection. Reason: Resolution message not received.", new object[0]));
					}
					if (!message.Headers.Action.Equals("cache://Microsoft.ApplicationServer.Caching/Resolution", StringComparison.Ordinal) || message.Headers.To == null)
					{
						throw new CommunicationException(string.Format(CultureInfo.InvariantCulture, "Terminating connection. Reason: Cache Name Resolution Failed : {0}", new object[] { message.Headers.Action }));
					}
					RemoteEndpointMessageProperty remoteEndpointMessageProperty = message.Properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
					ClientLocationType? clientLocationType = null;
					if (remoteEndpointMessageProperty != null)
					{
						this._remoteEndpoint = new RemoteEndpoint(remoteEndpointMessageProperty.Address, remoteEndpointMessageProperty.Port);
						clientLocationType = Utility.GetClientLocationForIP(this._remoteEndpoint.HostAddress, this._remoteEndpoint.Port);
					}
					if (Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo(this._logSource, "Client location for the request is: {0}", new object[] { clientLocationType });
					}
					this._cacheResolver = new CacheConnectionProperty(message.Headers.To.ToString(), TransportUtility.GetAuthenticationHeader(message), clientLocationType);
					if (CloudUtility.IsVASDeployment)
					{
						string currentEndpointAddress = CloudUtility.CloudProvider.GetCurrentEndpointAddress();
						CloudRoutingChannelProperties messageHeader2 = Utility.GetMessageHeader<CloudRoutingChannelProperties>(message, "DHint", "urn:AppFabricCaching");
						if (messageHeader2 != null && !string.IsNullOrEmpty(messageHeader2.DestinationHostAddress))
						{
							string destinationHostAddress = messageHeader2.DestinationHostAddress;
							if (Provider.IsEnabled(TraceLevel.Info))
							{
								EventLogWriter.WriteInfo(this._logSource, "Desired address: {0}, Current address: {1}", new object[] { destinationHostAddress, currentEndpointAddress });
							}
							if (messageHeader2.RoutingChannelConnectAction == CloudRoutingChannelConnectAction.ConnectDesiredEndpointOnly && string.CompareOrdinal(currentEndpointAddress, destinationHostAddress) != 0)
							{
								this._cacheResolver.TerminateCacheConnection = true;
								if (Provider.IsEnabled(TraceLevel.Info))
								{
									EventLogWriter.WriteInfo(this._logSource, "Terminating connection desired:{0} current:{1}", new object[] { destinationHostAddress, currentEndpointAddress });
								}
							}
							if (string.IsNullOrEmpty(messageHeader2.VipEndpoint))
							{
								this._cacheResolver.InitCloudChannelRoutingProperties(true);
							}
							else
							{
								this._cacheResolver.InitCloudChannelRoutingProperties(true, messageHeader2.VipEndpoint);
							}
						}
					}
				}
			}
			finally
			{
				if (message != null)
				{
					message.Close();
				}
			}
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0004A318 File Offset: 0x00048518
		public void Open()
		{
			this.Open(ConfigManager.CLIENT_CHANNEL_OPEN_TIMEOUT);
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600187A RID: 6266 RVA: 0x0004A325 File Offset: 0x00048525
		// (remove) Token: 0x0600187B RID: 6267 RVA: 0x0004A333 File Offset: 0x00048533
		public event EventHandler Opened
		{
			add
			{
				this._innerChannel.Opened += value;
			}
			remove
			{
				this._innerChannel.Opened -= value;
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600187C RID: 6268 RVA: 0x0004A341 File Offset: 0x00048541
		// (remove) Token: 0x0600187D RID: 6269 RVA: 0x0004A34F File Offset: 0x0004854F
		public event EventHandler Opening
		{
			add
			{
				this._innerChannel.Opening += value;
			}
			remove
			{
				this._innerChannel.Opening -= value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0004A35D File Offset: 0x0004855D
		public CommunicationState State
		{
			get
			{
				return this._innerChannel.State;
			}
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0004A36A File Offset: 0x0004856A
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerChannel.BeginSend(message, timeout, callback, state);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0004A37C File Offset: 0x0004857C
		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			return this._innerChannel.BeginSend(message, callback, state);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0004A38C File Offset: 0x0004858C
		public void EndSend(IAsyncResult result)
		{
			this._innerChannel.EndSend(result);
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0004A39A File Offset: 0x0004859A
		public EndpointAddress RemoteAddress
		{
			get
			{
				return this._innerChannel.RemoteAddress;
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0004A3A7 File Offset: 0x000485A7
		public void Send(Message message, TimeSpan timeout)
		{
			this._innerChannel.Send(message, timeout);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0004A3B6 File Offset: 0x000485B6
		public void Send(Message message)
		{
			this._innerChannel.Send(message);
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0004A3C4 File Offset: 0x000485C4
		public Uri Via
		{
			get
			{
				return this._innerChannel.Via;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x0004A3D1 File Offset: 0x000485D1
		public global::System.ServiceModel.Channels.IDuplexSession Session
		{
			get
			{
				return this._innerChannel.Session;
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0004A3DE File Offset: 0x000485DE
		public override int GetHashCode()
		{
			return this._id;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0004A3E8 File Offset: 0x000485E8
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			CacheResolverChannel cacheResolverChannel = obj as CacheResolverChannel;
			return cacheResolverChannel != null && cacheResolverChannel.GetHashCode() == this.GetHashCode();
		}

		// Token: 0x04000D6A RID: 3434
		private const string resolutionHeaderNamespace = "cache://Microsoft.ApplicationServer.Caching/";

		// Token: 0x04000D6B RID: 3435
		private const string resolutionHeaderName = "Resolution";

		// Token: 0x04000D6C RID: 3436
		private static OptimizedThreadSafeCounter safeCounter = new OptimizedThreadSafeCounter(0);

		// Token: 0x04000D6D RID: 3437
		private CloudRoutingChannelProperties _desiredRoutingChannelProps;

		// Token: 0x04000D6E RID: 3438
		private CacheResolverChannel.OpenDelegate _openDelegate;

		// Token: 0x04000D6F RID: 3439
		private CacheConnectionProperty _cacheResolver;

		// Token: 0x04000D70 RID: 3440
		private IDuplexSessionChannel _innerChannel;

		// Token: 0x04000D71 RID: 3441
		private CacheResolverCommunicatorType _communicatorType;

		// Token: 0x04000D72 RID: 3442
		private DataCacheSecurity _dataCacheSecurity;

		// Token: 0x04000D73 RID: 3443
		private RemoteEndpoint _remoteEndpoint;

		// Token: 0x04000D74 RID: 3444
		protected string _logSource = "DistributedCache.CacheResolverChannel";

		// Token: 0x04000D75 RID: 3445
		private int _id = CacheResolverChannel.safeCounter.Next();

		// Token: 0x0200029E RID: 670
		// (Invoke) Token: 0x0600188B RID: 6283
		private delegate void OpenDelegate(TimeSpan timeout);
	}
}
