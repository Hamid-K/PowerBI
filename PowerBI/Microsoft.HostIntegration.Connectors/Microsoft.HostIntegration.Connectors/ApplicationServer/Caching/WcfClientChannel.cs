using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Xml;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002BD RID: 701
	internal sealed class WcfClientChannel : WcfTransportChannel, IClientChannel, IDisposable
	{
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x0004C391 File Offset: 0x0004A591
		internal bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x0004C39B File Offset: 0x0004A59B
		internal bool IsVasRoutingEnabled
		{
			get
			{
				return this._routingType == DataCacheDeploymentMode.HybridClient || this._routingType == DataCacheDeploymentMode.DIPClient;
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0004C3B2 File Offset: 0x0004A5B2
		internal WcfClientChannel(DataCacheSecurity dataCacheSecurity, string id, IEndpointIdentityProvider endpointIdentityProvider)
			: base(null, dataCacheSecurity)
		{
			this.Initialize(null, CommonTransportElement.UNINITIALIZED_TIMESPAN, id, true, endpointIdentityProvider, 1);
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0004C3CC File Offset: 0x0004A5CC
		internal WcfClientChannel(DataCacheSecurity dataCacheSecurity, DataCacheTransportProperties transportProps, string id, object verifyObject, VerifyResponseCallback verifyCallback, int maxChannelCount, IEndpointIdentityProvider endpointIdentityProvider)
			: base(dataCacheSecurity)
		{
			this.Initialize(transportProps, CommonTransportElement.UNINITIALIZED_TIMESPAN, id, true, endpointIdentityProvider, maxChannelCount);
			this._verifier = new ClientChannelVerifier(verifyObject, verifyCallback);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0004C3F8 File Offset: 0x0004A5F8
		internal WcfClientChannel(DataCacheSecurity dataCacheSecurity, DataCacheTransportProperties transportProps, string id, object verifyObject, VerifyResponseCallback verifyCallback, TimeSpan chnlOpenTimeout, TimeSpan sendTimeout, int maxChannelCount, IEndpointIdentityProvider endpointIdentityProvider, int nodeCount, EndpointID vipEndpoint, DataCacheDeploymentMode routingType)
			: base(null, chnlOpenTimeout, sendTimeout, dataCacheSecurity)
		{
			this._hybridChannelConfig = new VasHybridChannelConfig(vipEndpoint, nodeCount * maxChannelCount);
			this._routingType = routingType;
			this._maxChannelCount = maxChannelCount;
			this.Initialize(transportProps, CommonTransportElement.UNINITIALIZED_TIMESPAN, id, true, endpointIdentityProvider, maxChannelCount);
			this._verifier = new ClientChannelVerifier(verifyObject, verifyCallback);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0004C454 File Offset: 0x0004A654
		internal WcfClientChannel(DataCacheSecurity dataCacheSecurity, DataCacheTransportProperties transportProps, string id, object verifyObject, VerifyResponseCallback verifyCallback, TimeSpan chnlOpenTimeout, TimeSpan sendTimeout, int maxChannelCount, IEndpointIdentityProvider endpointIdentityProvider)
			: base(null, chnlOpenTimeout, sendTimeout, dataCacheSecurity)
		{
			this.Initialize(transportProps, CommonTransportElement.UNINITIALIZED_TIMESPAN, id, true, endpointIdentityProvider, maxChannelCount);
			this._verifier = new ClientChannelVerifier(verifyObject, verifyCallback);
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0004C484 File Offset: 0x0004A684
		internal WcfClientChannel(DataCacheSecurity dataCacheSecurity, DataCacheTransportProperties transportProps, string id, object verifyObject, VerifyResponseCallback verifyCallback, TimeSpan chnlOpenTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout, int maxChannelCount, IEndpointIdentityProvider endpointIdentityProvider, int nodeCount, EndpointID vipEndpoint, DataCacheDeploymentMode routingType)
			: base(null, chnlOpenTimeout, sendTimeout, dataCacheSecurity)
		{
			this._hybridChannelConfig = new VasHybridChannelConfig(vipEndpoint, nodeCount * maxChannelCount);
			this._routingType = routingType;
			this._maxChannelCount = maxChannelCount;
			this.Initialize(transportProps, receiveTimeout, id, true, endpointIdentityProvider, this._maxChannelCount);
			this._verifier = new ClientChannelVerifier(verifyObject, verifyCallback);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0004C4E1 File Offset: 0x0004A6E1
		internal WcfClientChannel(DataCacheSecurity dataCacheSecurity, DataCacheTransportProperties transportProps, string id, object verifyObject, VerifyResponseCallback verifyCallback, TimeSpan chnlOpenTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout, int maxChannelCount, IEndpointIdentityProvider endpointIdentityProvider)
			: base(null, chnlOpenTimeout, sendTimeout, dataCacheSecurity)
		{
			this.Initialize(transportProps, receiveTimeout, id, true, endpointIdentityProvider, maxChannelCount);
			this._verifier = new ClientChannelVerifier(verifyObject, verifyCallback);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0004C50D File Offset: 0x0004A70D
		internal WcfClientChannel(OnMessageReceived defaultReceiveCallback, DataCacheSecurity dataCacheSecurity, string id, IEndpointIdentityProvider endpointIdentityProvider)
			: base(defaultReceiveCallback, dataCacheSecurity)
		{
			this.Initialize(null, CommonTransportElement.UNINITIALIZED_TIMESPAN, id, true, endpointIdentityProvider, 1);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0004C528 File Offset: 0x0004A728
		private void Initialize(DataCacheTransportProperties transportProps, TimeSpan receiveTimeout, string id, bool syncSendForSendReceive, IEndpointIdentityProvider endpointIdentityProvider, int maxChannelCount)
		{
			this._logSource = "DistributedCache.ClientChannel." + id;
			this._endSend = new AsyncCallback(this.EndSend);
			this._syncSendForSendReceive = syncSendForSendReceive;
			this._maxChannelCount = maxChannelCount;
			this._connections = new Hashtable();
			this._requestReplyTable = DataStructureFactory.CreateBaseHashTable(new ObjectDirectoryNodeFactory());
			this._clientTransportProps = transportProps;
			if (receiveTimeout != CommonTransportElement.UNINITIALIZED_TIMESPAN)
			{
				this._receiveTimeout = receiveTimeout;
			}
			else if (this._clientTransportProps != null && this._clientTransportProps.ReceiveTimeout.TotalMilliseconds != (double)DataCacheTransportProperties.NOT_ASSIGNED)
			{
				this._receiveTimeout = this._clientTransportProps.ReceiveTimeout;
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Set receiveTimeout to {0}.", new object[] { this._receiveTimeout });
			}
			this.RegisterReceiveCallback("http://schemas.microsoft.com/velocity/msgs/ErrorNotificationAction", new OnMessageReceived(this.ErrorNotificationCallback));
			this._channelToUse = new OptimizedSafeCounter(this._maxChannelCount);
			base.SetupBindings();
			this._tcpBinding.Elements.Insert(0, new EndPointIdentityBindingElement(endpointIdentityProvider));
			this._tcpFactory = this._tcpBinding.BuildChannelFactory<IDuplexSessionChannel>(new object[0]);
			this._tcpFactory.Open();
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose(this._logSource, "Client channel opened.");
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0004C684 File Offset: 0x0004A884
		private void ErrorNotificationCallback(IReplyContext errorNotification)
		{
			ResponseBody response = errorNotification.GetResponse();
			this._serverErrorInfo = response.Value;
			this._serverErrorStatus = response.ResponseCode;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0004C6B0 File Offset: 0x0004A8B0
		internal IDuplexSessionChannel CreateChannel(EndpointID endpoint)
		{
			IDuplexSessionChannel duplexSessionChannel = null;
			EndpointAddress endpointAddress;
			if (this._routingType == DataCacheDeploymentMode.HybridClient)
			{
				endpointAddress = new EndpointAddress(this._hybridChannelConfig.VipEndpoint.UriString);
			}
			else
			{
				endpointAddress = new EndpointAddress(endpoint.UriString);
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Creating channel for {0}.", new object[] { endpoint });
			}
			if (endpoint.IsTcp())
			{
				duplexSessionChannel = this._tcpFactory.CreateChannel(endpointAddress);
				if (this._routingType == DataCacheDeploymentMode.HybridClient)
				{
					CloudRoutingChannelProperties property = duplexSessionChannel.GetProperty<CloudRoutingChannelProperties>();
					if (property != null && !string.IsNullOrEmpty(endpoint.UriString))
					{
						property.InitCloudRoutingChannel(endpoint.URI.Host, CloudRoutingChannelConnectAction.ConnectAlways);
					}
				}
				else if (this._routingType == DataCacheDeploymentMode.DIPClient)
				{
					CloudRoutingChannelProperties property2 = duplexSessionChannel.GetProperty<CloudRoutingChannelProperties>();
					if (property2 != null && !string.IsNullOrEmpty(endpoint.UriString))
					{
						property2.InitCloudRoutingChannel(endpoint.URI.Host, CloudRoutingChannelConnectAction.ConnectAlways, this._hybridChannelConfig.VipEndpoint.UriString);
					}
				}
			}
			if (Provider.IsEnabled(TraceLevel.Info) && duplexSessionChannel != null)
			{
				EventLogWriter.WriteInfo(this._logSource, "channel for {0} ChannelID = {1}.", new object[]
				{
					endpoint,
					duplexSessionChannel.GetHashCode()
				});
			}
			return duplexSessionChannel;
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0004C7DC File Offset: 0x0004A9DC
		internal OperationResult VerifyAndStartReceiving(IChannelContainer container, out ServerInformation serverInfo)
		{
			serverInfo = null;
			OperationResult operationResult = OperationResult.Success;
			if (this._verifier != null)
			{
				operationResult = this._verifier.Verify(container, this._sendTimeout, this._receiveTimeout, out serverInfo);
				if (operationResult.HasVerificationFailed || operationResult.IsAsyncFailure)
				{
					base.CleanupChannel(container.Channel);
					return operationResult;
				}
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Channel verification result for endpoint {0} - {1}.", new object[]
				{
					container.Channel.RemoteAddress,
					operationResult
				});
			}
			IAsyncResult asyncResult = container.Channel.BeginReceive(this._receiveTimeout, this._receiver, container);
			if (asyncResult.CompletedSynchronously)
			{
				ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
				{
					IAsyncResult asyncResult2 = state as IAsyncResult;
					this.CompleteProcessing(asyncResult2);
				}, asyncResult);
			}
			return operationResult;
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0004C8A0 File Offset: 0x0004AAA0
		protected override TcpTransportBindingElement PrepareTcpTransportBindingElement()
		{
			TcpTransportBindingElement tcpTransportBindingElement = new TcpTransportBindingElement();
			WcfClientChannel.AddTransportProperties(tcpTransportBindingElement, this._clientTransportProps);
			return tcpTransportBindingElement;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0004C8C0 File Offset: 0x0004AAC0
		internal static void AddTransportProperties(TcpTransportBindingElement tcpTransportBE, DataCacheTransportProperties clientTransportProps)
		{
			if (clientTransportProps != null)
			{
				if (clientTransportProps.ConnectionBufferSize != DataCacheTransportProperties.NOT_ASSIGNED)
				{
					tcpTransportBE.ConnectionBufferSize = clientTransportProps.ConnectionBufferSize;
				}
				if (clientTransportProps.MaxOutputDelay.TotalMilliseconds != (double)DataCacheTransportProperties.NOT_ASSIGNED)
				{
					tcpTransportBE.MaxOutputDelay = clientTransportProps.MaxOutputDelay;
				}
				if (clientTransportProps.MaxBufferSize != DataCacheTransportProperties.NOT_ASSIGNED)
				{
					tcpTransportBE.MaxBufferSize = clientTransportProps.MaxBufferSize;
				}
				if (clientTransportProps.MaxBufferPoolSize != (long)DataCacheTransportProperties.NOT_ASSIGNED)
				{
					tcpTransportBE.MaxBufferPoolSize = clientTransportProps.MaxBufferPoolSize;
				}
				if (clientTransportProps.ChannelInitializationTimeout.TotalMilliseconds != (double)DataCacheTransportProperties.NOT_ASSIGNED)
				{
					tcpTransportBE.ChannelInitializationTimeout = clientTransportProps.ChannelInitializationTimeout;
				}
			}
			tcpTransportBE.MaxReceivedMessageSize = 2147483647L;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0004C970 File Offset: 0x0004AB70
		protected override void AddSecurityBinding(BindingElementCollection bindingCollection)
		{
			if (base.DataCacheSecurity.SecurityMode == DataCacheSecurityMode.Transport)
			{
				WindowsStreamSecurityBindingElement windowsStreamSecurityBindingElement = new WindowsStreamSecurityBindingElement();
				if (base.DataCacheSecurity.ProtectionLevel == DataCacheProtectionLevel.Sign)
				{
					windowsStreamSecurityBindingElement.ProtectionLevel = ProtectionLevel.Sign;
				}
				else if (base.DataCacheSecurity.ProtectionLevel == DataCacheProtectionLevel.EncryptAndSign)
				{
					windowsStreamSecurityBindingElement.ProtectionLevel = ProtectionLevel.EncryptAndSign;
				}
				else if (base.DataCacheSecurity.ProtectionLevel == DataCacheProtectionLevel.None)
				{
					windowsStreamSecurityBindingElement.ProtectionLevel = ProtectionLevel.None;
				}
				bindingCollection.Add(windowsStreamSecurityBindingElement);
				return;
			}
			if (base.DataCacheSecurity.SslEnabled)
			{
				bindingCollection.Add(new SslStreamSecurityBindingElement
				{
					RequireClientCertificate = false
				});
			}
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0004C9FC File Offset: 0x0004ABFC
		protected override void AddCustomBindingElements(BindingElementCollection bindingCollection)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Added cache resolution binding element.", new object[0]);
			}
			bindingCollection.Insert(0, new CacheResolverBindingElement(base.DataCacheSecurity));
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0004CA30 File Offset: 0x0004AC30
		internal void InvokeDeadCallback(EndpointID endpoint, object context, Exception e)
		{
			OnRemoteGoingDown deadCallback = this._deadCallback;
			if (deadCallback != null)
			{
				deadCallback(endpoint, context, e);
			}
			this.CleanupPendingSendReceiveRequests(endpoint);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0004CA58 File Offset: 0x0004AC58
		private void CleanupChannel(IDuplexSessionChannel channel, Exception e)
		{
			Uri uri = channel.RemoteAddress.Uri;
			bool flag = false;
			if (this._routingType == DataCacheDeploymentMode.HybridClient || this._routingType == DataCacheDeploymentMode.DIPClient)
			{
				CloudRoutingChannelProperties property = channel.GetProperty<CloudRoutingChannelProperties>();
				if (!string.IsNullOrEmpty(property.DestinationHostAddress))
				{
					uri = new Uri(property.DestinationHostAddress);
					flag = true;
				}
			}
			List<ChannelContainer> list = this._connections[uri] as List<ChannelContainer>;
			lock (this._connections.SyncRoot)
			{
				int num = 0;
				while (list != null && num < list.Count)
				{
					if (list[num].Channel == channel)
					{
						list[num].RecycleChannel(e, false);
						if (flag)
						{
							this.RemoveChannelFromPool(list[num], uri);
						}
					}
					num++;
				}
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<Uri, int>(this._logSource, "Channel cleaned up for {0} ChannelID = {1}", uri, channel.GetHashCode());
			}
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0004CB58 File Offset: 0x0004AD58
		protected override bool InvokeCallback(Message message, IChannelContainer container)
		{
			if (!base.InvokeCallback(message, container))
			{
				if (string.Equals(message.Headers.Action, "http://schemas.microsoft.com/velocity/msgs/RequestReplyAction"))
				{
					this.ProcessResponse(message, message.Headers.MessageId);
				}
				else if (!base.InvokeDefaultCallback(message, container))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x0004CBA8 File Offset: 0x0004ADA8
		private void ProcessResponse(Message message, UniqueId messageId)
		{
			RequestReplyState requestReplyState = this._requestReplyTable.Delete(messageId) as RequestReplyState;
			if (requestReplyState != null)
			{
				requestReplyState.ProcessResponse(message);
			}
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0004CBD4 File Offset: 0x0004ADD4
		protected override void CompleteProcessing(IAsyncResult result)
		{
			while (!this._isClosed)
			{
				IChannelContainer channelContainer = result.AsyncState as IChannelContainer;
				IDuplexSessionChannel channel = channelContainer.Channel;
				Message message;
				try
				{
					message = channel.EndReceive(result);
				}
				catch (Exception ex)
				{
					if (!base.LogAndCheckIfCommunicationException(ex, channel))
					{
						throw;
					}
					this.CleanupChannel(channel, ex);
					return;
				}
				if (message != null && !this._isClosed)
				{
					try
					{
						try
						{
							result = channel.BeginReceive(this._receiveTimeout, this._receiver, channelContainer);
							if (Provider.IsEnabled(TraceLevel.Info))
							{
								EventLogWriter.WriteInfo(this._logSource, "Receive posted on client channel {0}", new object[] { channel.GetHashCode() });
							}
							if (!result.CompletedSynchronously)
							{
								return;
							}
						}
						catch (Exception ex2)
						{
							if (!base.LogAndCheckIfCommunicationException(ex2, channel))
							{
								throw;
							}
							this.CleanupChannel(channel, ex2);
							return;
						}
						continue;
					}
					finally
					{
						this.InvokeCallback(message, channelContainer);
					}
					return;
				}
				this.CleanupChannel(channel, null);
				return;
			}
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0004CCDC File Offset: 0x0004AEDC
		private List<ChannelContainer> CreateContainer(EndpointID endpoint)
		{
			return this.CreateContainer(endpoint, this._maxChannelCount);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0004CCEC File Offset: 0x0004AEEC
		private List<ChannelContainer> CreateContainer(EndpointID endpoint, int maxChannelCount)
		{
			List<ChannelContainer> list = new List<ChannelContainer>();
			for (int i = 0; i < maxChannelCount; i++)
			{
				ChannelContainer channelContainer = new ChannelContainer(endpoint, this);
				list.Add(channelContainer);
				if (this._routingType == DataCacheDeploymentMode.HybridClient || this._routingType == DataCacheDeploymentMode.DIPClient)
				{
					Interlocked.Increment(ref this._hybridChannelConfig.InProgressChannelCount);
				}
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(this._logSource, "Container created for {0} - {1}.", new object[]
					{
						endpoint,
						list[i]
					});
				}
			}
			return list;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0004CD6C File Offset: 0x0004AF6C
		private static void InvokeOperationFailureCallback(WaitCallback callback, object state)
		{
			if (callback != null)
			{
				callback(state);
			}
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0004CD78 File Offset: 0x0004AF78
		internal void ConvertChannelMode(DataCacheDeploymentMode newRoutingType)
		{
			if (this._routingType != newRoutingType && newRoutingType == DataCacheDeploymentMode.SimpleClient)
			{
				lock (this._connections.SyncRoot)
				{
					if (this._routingType != newRoutingType)
					{
						this._routingType = newRoutingType;
						foreach (object obj in this._connections.Values)
						{
							List<ChannelContainer> list = (List<ChannelContainer>)obj;
							for (int i = 0; i < list.Count; i++)
							{
								list[i].Dispose();
							}
						}
					}
				}
			}
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0004CE44 File Offset: 0x0004B044
		private OperationResult SendMessage(EndpointID endpoint, ICreateMessage data, TimeSpan timeout, WaitCallback callback, object state, bool async, UniqueId id)
		{
			if (endpoint == null)
			{
				throw new ArgumentNullException("endpoint");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (this._isClosed)
			{
				WcfClientChannel.InvokeOperationFailureCallback(callback, state);
				return OperationResult.InstanceClosed;
			}
			ErrStatus serverErrorStatus = this._serverErrorStatus;
			if (serverErrorStatus != ErrStatus.UNINITIALIZED_ERROR)
			{
				this._serverErrorStatus = ErrStatus.UNINITIALIZED_ERROR;
				DataCacheException ex = Utility.CreateClientException(serverErrorStatus, this._logSource);
				Utility.AddInformationToException(ex, serverErrorStatus, this._serverErrorInfo);
				return new OperationResult(OperationStatus.AsyncFailureReceived, ex);
			}
			bool flag;
			ChannelContainer channelContainer = this.GetChannelContainer(endpoint, callback, state, out flag);
			if (channelContainer == null)
			{
				return OperationResult.InstanceClosed;
			}
			if (!channelContainer.IsActive)
			{
				channelContainer.RecycleChannel(null, false);
				if (this._channelOpenTimeout != TimeSpan.Zero)
				{
					channelContainer.ChannelOpenHandle.WaitOne(this._channelOpenTimeout, false);
				}
				OperationResult status = channelContainer.Status;
				if (!status.IsSuccess)
				{
					WcfClientChannel.InvokeOperationFailureCallback(callback, state);
					return status;
				}
			}
			IDuplexSessionChannel channel = channelContainer.Channel;
			Message message = data.CreateWcfMessage(channelContainer.InnerContainer.RemoteVersionInfo);
			if (id != null)
			{
				message.Headers.MessageId = id;
			}
			if (base.DataCacheSecurity.SecurityMode == DataCacheSecurityMode.Message)
			{
				TransportUtility.AddAuthenticationHeader(message, base.DataCacheSecurity, timeout);
			}
			if (this._routingType == DataCacheDeploymentMode.DIPClient)
			{
				TransportUtility.AddVipToDipHeader(message, this._hybridChannelConfig.VipEndpoint);
			}
			OperationResult operationResult;
			try
			{
				operationResult = this.SendOnChannel(endpoint, ref timeout, callback, state, async, channel, message);
			}
			finally
			{
				if (!async)
				{
					message.Close();
				}
			}
			return operationResult;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0004CFC4 File Offset: 0x0004B1C4
		private ChannelContainer GetChannelContainer(EndpointID endpoint, WaitCallback callback, object state, out bool isSmartRoutedInVas)
		{
			isSmartRoutedInVas = false;
			if (this._routingType == DataCacheDeploymentMode.HybridClient || this._routingType == DataCacheDeploymentMode.DIPClient)
			{
				return this.GetChannelContainerHybrid(endpoint, callback, state, out isSmartRoutedInVas);
			}
			return this.GetChannelContainerSimple(endpoint, callback, state);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0004CFF4 File Offset: 0x0004B1F4
		private ChannelContainer GetChannelContainerSimple(EndpointID endpoint, WaitCallback callback, object state)
		{
			List<ChannelContainer> list = this._connections[endpoint.URI] as List<ChannelContainer>;
			if (list == null || list.Count == 0)
			{
				lock (this._connections.SyncRoot)
				{
					list = this._connections[endpoint.URI] as List<ChannelContainer>;
					if (list == null || list.Count == 0)
					{
						list = this.CreateContainer(endpoint);
						foreach (ChannelContainer channelContainer in list)
						{
							if (channelContainer.Status == OperationResult.InstanceClosed)
							{
								WcfClientChannel.InvokeOperationFailureCallback(callback, state);
								return null;
							}
						}
						this._connections[endpoint.URI] = list;
					}
				}
			}
			return list[this._channelToUse.Next()];
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0004D0FC File Offset: 0x0004B2FC
		private ChannelContainer GetChannelContainerHybrid(EndpointID endpoint, WaitCallback callback, object state, out bool isSmartRouted)
		{
			List<ChannelContainer> list = this._connections[endpoint.URI] as List<ChannelContainer>;
			isSmartRouted = false;
			if (list == null || list.Count == 0)
			{
				list = this.GetVipChannelList(callback, state);
				if (this._hybridChannelConfig.InProgressChannelCount + this._hybridChannelConfig.TotalActiveClientConnections <= this._hybridChannelConfig.OptimalConnectionCount)
				{
					this.CreateContainer(endpoint);
				}
			}
			else
			{
				isSmartRouted = true;
				int num = this._maxChannelCount - list.Count;
				if (num > 0)
				{
					this.CreateContainer(endpoint, num);
				}
			}
			ChannelContainer channelContainer;
			try
			{
				channelContainer = list[this._channelToUse.Next(list.Count)];
			}
			catch (Exception ex)
			{
				if (!(ex is IndexOutOfRangeException) && !(ex is DivideByZeroException))
				{
					throw;
				}
				list = this.GetVipChannelList(callback, state);
				channelContainer = list[this._channelToUse.Next(list.Count)];
				isSmartRouted = false;
			}
			return channelContainer;
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0004D1EC File Offset: 0x0004B3EC
		private List<ChannelContainer> CreateVipChannelPool(WaitCallback callback, object state)
		{
			List<ChannelContainer> list;
			lock (this._connections.SyncRoot)
			{
				if (!this._hybridChannelConfig.IsVipPoolCreated)
				{
					list = this.CreateContainer(this._hybridChannelConfig.VipEndpoint);
					foreach (ChannelContainer channelContainer in list)
					{
						if (channelContainer.Status == OperationResult.InstanceClosed)
						{
							WcfClientChannel.InvokeOperationFailureCallback(callback, state);
							return null;
						}
					}
					this.AddChannelContainerToVipPool(list);
					this._hybridChannelConfig.IsVipPoolCreated = true;
				}
				else
				{
					list = this._connections[this._hybridChannelConfig.VipEndpoint.URI] as List<ChannelContainer>;
				}
			}
			return list;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x0004D2D4 File Offset: 0x0004B4D4
		private List<ChannelContainer> GetVipChannelList(WaitCallback callback, object state)
		{
			List<ChannelContainer> list;
			if (!this._hybridChannelConfig.IsVipPoolCreated)
			{
				list = this.CreateVipChannelPool(callback, state);
			}
			else
			{
				list = this._connections[this._hybridChannelConfig.VipEndpoint.URI] as List<ChannelContainer>;
			}
			return list;
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0004D31C File Offset: 0x0004B51C
		internal void ConvertToVasRoutingChannel(EndpointID vipEndpoint, int nodeCount, DataCacheDeploymentMode routingType)
		{
			if (this._routingType != DataCacheDeploymentMode.HybridClient && this._routingType != DataCacheDeploymentMode.DIPClient)
			{
				lock (this._connections.SyncRoot)
				{
					if (this._routingType != DataCacheDeploymentMode.HybridClient && this._routingType != DataCacheDeploymentMode.DIPClient)
					{
						this._hybridChannelConfig = new VasHybridChannelConfig(vipEndpoint, nodeCount * this._maxChannelCount);
						this._hybridChannelConfig.VipConnectionPool = new List<ChannelContainer>();
						List<ChannelContainer> list = this._connections[vipEndpoint.URI] as List<ChannelContainer>;
						if (list != null && list.Count > 0)
						{
							foreach (ChannelContainer channelContainer in list)
							{
								channelContainer.Dispose();
							}
						}
						this._connections[this._hybridChannelConfig.VipEndpoint.URI] = this._hybridChannelConfig.VipConnectionPool;
						this._hybridChannelConfig.IsVipPoolCreated = false;
						this._routingType = routingType;
					}
				}
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0004D448 File Offset: 0x0004B648
		public bool UpdateCacheRedirectionForHybridClient(EndpointID newVipEndpoint)
		{
			if (newVipEndpoint.Equals(this._hybridChannelConfig.VipEndpoint))
			{
				return false;
			}
			lock (this._connections.SyncRoot)
			{
				if (newVipEndpoint.Equals(this._hybridChannelConfig.VipEndpoint))
				{
					return false;
				}
				List<ChannelContainer> list = new List<ChannelContainer>();
				this._connections[newVipEndpoint] = list;
				this._hybridChannelConfig.VipEndpoint = newVipEndpoint;
				this._hybridChannelConfig.IsVipPoolCreated = false;
			}
			return true;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0004D4E4 File Offset: 0x0004B6E4
		private OperationResult SendOnChannel(EndpointID endpoint, ref TimeSpan timeout, WaitCallback callback, object state, bool async, IDuplexSessionChannel channel, Message message)
		{
			OperationResult operationResult;
			try
			{
				if (async)
				{
					CompositeSendState compositeSendState = new CompositeSendState(callback, state, channel, endpoint, message);
					channel.BeginSend(message, timeout, this._endSend, compositeSendState);
					operationResult = new OperationResult(OperationStatus.Success, channel.GetHashCode());
				}
				else
				{
					channel.Send(message, timeout);
					if (Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo(this._logSource, "Message sent to {0} ChannelID = {1}.", new object[]
						{
							endpoint,
							channel.GetHashCode()
						});
					}
					operationResult = new OperationResult(OperationStatus.Success, channel.GetHashCode());
				}
			}
			catch (Exception ex)
			{
				if (!this.HandleSendFailure(ex, endpoint, channel, callback, state))
				{
					throw;
				}
				operationResult = new OperationResult(OperationStatus.SendFailed, ex);
			}
			return operationResult;
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0004D5B4 File Offset: 0x0004B7B4
		internal void ChannelOpened(ChannelContainer container, Uri instanceAddress)
		{
			if (this._routingType == DataCacheDeploymentMode.HybridClient || this._routingType == DataCacheDeploymentMode.DIPClient)
			{
				this.AddChannelToPool(container, instanceAddress);
			}
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0004D5D0 File Offset: 0x0004B7D0
		internal void AddChannelToPool(ChannelContainer container, Uri instanceAddr)
		{
			if (instanceAddr == null)
			{
				instanceAddr = this._hybridChannelConfig.VipEndpoint.URI;
			}
			CloudRoutingChannelProperties property = container.Channel.GetProperty<CloudRoutingChannelProperties>();
			property.DestinationHostAddress = instanceAddr.OriginalString;
			lock (this._connections.SyncRoot)
			{
				Interlocked.Decrement(ref this._hybridChannelConfig.InProgressChannelCount);
				List<ChannelContainer> list;
				if (this._connections.ContainsKey(instanceAddr))
				{
					list = this._connections[instanceAddr] as List<ChannelContainer>;
					if (list.Count >= this._maxChannelCount && !this._hybridChannelConfig.VipConnectionPool.Contains(container))
					{
						container.Channel.Abort();
						return;
					}
				}
				else
				{
					list = new List<ChannelContainer>();
				}
				list.Add(container);
				this._connections[instanceAddr] = list;
				this._hybridChannelConfig.TotalActiveClientConnections++;
			}
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0004D6D0 File Offset: 0x0004B8D0
		private void RemoveChannelFromPool(ChannelContainer channel, Uri instanceAddr)
		{
			if (this._routingType != DataCacheDeploymentMode.HybridClient && this._routingType != DataCacheDeploymentMode.DIPClient)
			{
				return;
			}
			if (this._connections.ContainsKey(instanceAddr))
			{
				List<ChannelContainer> list = this._connections[instanceAddr] as List<ChannelContainer>;
				if (!instanceAddr.Equals(this._hybridChannelConfig.VipEndpoint.URI))
				{
					list.Remove(channel);
				}
				this._hybridChannelConfig.TotalActiveClientConnections--;
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x0004D744 File Offset: 0x0004B944
		private void AddChannelContainerToVipPool(List<ChannelContainer> containers)
		{
			Uri uri = this._hybridChannelConfig.VipEndpoint.URI;
			List<ChannelContainer> list;
			if (this._connections.ContainsKey(uri))
			{
				list = this._connections[uri] as List<ChannelContainer>;
			}
			else
			{
				list = new List<ChannelContainer>();
			}
			foreach (ChannelContainer channelContainer in containers)
			{
				list.Add(channelContainer);
			}
			this._connections[uri] = list;
			this._hybridChannelConfig.VipConnectionPool = list;
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0004D7E4 File Offset: 0x0004B9E4
		private void EndSend(IAsyncResult result)
		{
			CompositeSendState compositeSendState = result.AsyncState as CompositeSendState;
			IDuplexSessionChannel channel = compositeSendState.Channel;
			try
			{
				channel.EndSend(result);
			}
			catch (Exception ex)
			{
				if (!this.HandleSendFailure(ex, compositeSendState.Endpoint, channel, compositeSendState.Callback, compositeSendState.State))
				{
					throw;
				}
			}
			finally
			{
				compositeSendState.Message.Close();
			}
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0004D858 File Offset: 0x0004BA58
		private bool HandleSendFailure(Exception e, EndpointID endpoint, IDuplexSessionChannel channel, WaitCallback callback, object state)
		{
			if (base.LogAndCheckIfCommunicationException(e, TraceLevel.Error, channel))
			{
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(this._logSource, "Send failed for {0} ChannelID = {1}.", new object[]
					{
						endpoint,
						channel.GetHashCode()
					});
				}
				if (!(e is TimeoutException))
				{
					this.CleanupChannel(channel, e);
				}
				if (callback != null)
				{
					callback(state);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x0004D8C4 File Offset: 0x0004BAC4
		private void CleanupPendingSendReceiveRequests(EndpointID endpoint)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Cleaning send-receive requests which went to {0}.", new object[] { endpoint });
			}
			IEnumerator keyValueEnumerator = this._requestReplyTable.GetKeyValueEnumerator();
			while (keyValueEnumerator.MoveNext())
			{
				object obj = keyValueEnumerator.Current;
				IBaseDataNode baseDataNode = obj as IBaseDataNode;
				UniqueId uniqueId = baseDataNode.Key as UniqueId;
				RequestReplyState requestReplyState = baseDataNode.Data as RequestReplyState;
				if (requestReplyState.RemoteAddress.Equals(endpoint) && requestReplyState.InTransit)
				{
					this.ProcessResponse(null, uniqueId);
				}
			}
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x0004D950 File Offset: 0x0004BB50
		private void Close()
		{
			this._isClosed = true;
			lock (this._connections.SyncRoot)
			{
				this._tcpFactory.Abort();
				foreach (object obj in this._connections.Values)
				{
					List<ChannelContainer> list = (List<ChannelContainer>)obj;
					for (int i = 0; i < list.Count; i++)
					{
						list[i].Dispose();
					}
				}
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose(this._logSource, "Client channel closed.");
			}
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x0004DA28 File Offset: 0x0004BC28
		public OperationResult Send(EndpointID endpoint, ICreateMessage message)
		{
			return this.Send(endpoint, message, this._sendTimeout);
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0004DA38 File Offset: 0x0004BC38
		public OperationResult Send(EndpointID endpoint, ICreateMessage message, TimeSpan timeout)
		{
			return this.SendMessage(endpoint, message, timeout, null, null, false, null);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0004DA47 File Offset: 0x0004BC47
		public OperationResult AsyncSend(EndpointID endpoint, ICreateMessage message, WaitCallback callback, object state)
		{
			return this.AsyncSend(endpoint, message, this._sendTimeout, callback, state);
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x0004DA5A File Offset: 0x0004BC5A
		public OperationResult AsyncSend(EndpointID endpoint, ICreateMessage message, TimeSpan timeout, WaitCallback callback, object state)
		{
			return this.SendMessage(endpoint, message, timeout, callback, state, true, null);
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x0004DA6B File Offset: 0x0004BC6B
		public OperationResult SendReceive(EndpointID endpoint, ICreateMessage request, out Message response)
		{
			return this.SendReceive(endpoint, request, this._sendReceiveTimeout, out response);
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x0004DA7C File Offset: 0x0004BC7C
		public OperationResult SendReceive(EndpointID endpoint, ICreateMessage request, TimeSpan timeout, out Message response)
		{
			if (endpoint == null)
			{
				throw new ArgumentNullException("endpoint");
			}
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (this._isClosed)
			{
				response = null;
				return OperationResult.InstanceClosed;
			}
			UniqueId uniqueId = new UniqueId();
			RequestReplyState requestReplyState = new RequestReplyState(endpoint);
			this._requestReplyTable.Add(uniqueId, requestReplyState);
			OperationResult operationResult = this.SendMessage(endpoint, request, timeout, null, null, !this._syncSendForSendReceive, uniqueId);
			requestReplyState.InTransit = operationResult.IsSuccess;
			if (!operationResult.IsSuccess)
			{
				this.ProcessResponse(null, uniqueId);
			}
			response = requestReplyState.Wait(timeout);
			if (response == null && !(this._requestReplyTable.Delete(uniqueId) is RequestReplyState))
			{
				response = requestReplyState.ResponseMessage;
			}
			return operationResult;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x0004DB34 File Offset: 0x0004BD34
		public void RegisterDeadCallback(OnRemoteGoingDown callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			lock (this._lockObject)
			{
				this._deadCallback = (OnRemoteGoingDown)Delegate.Combine(this._deadCallback, callback);
			}
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x0004DB94 File Offset: 0x0004BD94
		public void UnregisterDeadCallback(OnRemoteGoingDown callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			lock (this._lockObject)
			{
				if (this._deadCallback != null)
				{
					this._deadCallback = (OnRemoteGoingDown)Delegate.Remove(this._deadCallback, callback);
				}
			}
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x0004DBFC File Offset: 0x0004BDFC
		public void UnregisterDeadCallback()
		{
			this._deadCallback = null;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void MarkAuthorizationTokenInvalid(string currentToken)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0004DC05 File Offset: 0x0004BE05
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x04000DDB RID: 3547
		private IChannelFactory<IDuplexSessionChannel> _tcpFactory;

		// Token: 0x04000DDC RID: 3548
		private Hashtable _connections;

		// Token: 0x04000DDD RID: 3549
		private IBaseHashTable _requestReplyTable;

		// Token: 0x04000DDE RID: 3550
		private OnRemoteGoingDown _deadCallback;

		// Token: 0x04000DDF RID: 3551
		private AsyncCallback _endSend;

		// Token: 0x04000DE0 RID: 3552
		private ErrStatus _serverErrorStatus;

		// Token: 0x04000DE1 RID: 3553
		private object _serverErrorInfo;

		// Token: 0x04000DE2 RID: 3554
		private ClientChannelVerifier _verifier;

		// Token: 0x04000DE3 RID: 3555
		private bool _syncSendForSendReceive;

		// Token: 0x04000DE4 RID: 3556
		private int _maxChannelCount;

		// Token: 0x04000DE5 RID: 3557
		private OptimizedSafeCounter _channelToUse;

		// Token: 0x04000DE6 RID: 3558
		private DataCacheTransportProperties _clientTransportProps;

		// Token: 0x04000DE7 RID: 3559
		private DataCacheDeploymentMode _routingType;

		// Token: 0x04000DE8 RID: 3560
		private VasHybridChannelConfig _hybridChannelConfig;
	}
}
