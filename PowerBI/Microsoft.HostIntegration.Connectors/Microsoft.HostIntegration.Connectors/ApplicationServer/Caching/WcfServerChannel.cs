using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002BE RID: 702
	internal sealed class WcfServerChannel : WcfTransportChannel, IServerChannel, IDisposable
	{
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060019EA RID: 6634 RVA: 0x0004DC2C File Offset: 0x0004BE2C
		// (remove) Token: 0x060019EB RID: 6635 RVA: 0x0004DC64 File Offset: 0x0004BE64
		public event ConnectionCreatedEventHandler ConnectionCreatedEvent;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060019EC RID: 6636 RVA: 0x0004DC9C File Offset: 0x0004BE9C
		// (remove) Token: 0x060019ED RID: 6637 RVA: 0x0004DCD4 File Offset: 0x0004BED4
		public event ConnectionVerificationEventHandler ConnectionVerificationEvent;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060019EE RID: 6638 RVA: 0x0004DD0C File Offset: 0x0004BF0C
		// (remove) Token: 0x060019EF RID: 6639 RVA: 0x0004DD44 File Offset: 0x0004BF44
		public event ConnectionDestroyedEventHandler ConnectionDestroyedEvent;

		// Token: 0x060019F0 RID: 6640 RVA: 0x0004DD79 File Offset: 0x0004BF79
		internal WcfServerChannel(EndpointID[] endpoints, ServiceConfigurationManager serviceConfigurationManager)
			: this(endpoints, null, serviceConfigurationManager)
		{
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0004DD84 File Offset: 0x0004BF84
		internal WcfServerChannel(EndpointID[] endpoints, RequestReply verifyCallback, ServiceConfigurationManager serviceConfigurationManager)
			: this(endpoints, false, false, verifyCallback, null, serviceConfigurationManager)
		{
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x0004DD92 File Offset: 0x0004BF92
		internal WcfServerChannel(EndpointID[] endpoints, TimeSpan receiveTimeout, RequestReply verifyCallback, ServiceConfigurationManager serviceConfigurationManager)
			: this(endpoints, false, false, receiveTimeout, verifyCallback, null, serviceConfigurationManager)
		{
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0004DDA2 File Offset: 0x0004BFA2
		internal WcfServerChannel(EndpointID[] endpoints, bool sslEnabled, bool cacheResolutionEnabled, RequestReply verifyCallback, ServiceConfigurationManager serviceConfigurationManager)
			: this(endpoints, sslEnabled, cacheResolutionEnabled, verifyCallback, null, serviceConfigurationManager)
		{
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0004DDB2 File Offset: 0x0004BFB2
		internal WcfServerChannel(EndpointID[] endpoints, bool sslEnabled, bool cacheResolutionEnabled, RequestReply verifyCallback, OnMessageReceived defaultReceiveCallback, ServiceConfigurationManager serviceConfigurationManager)
			: this(endpoints, sslEnabled, cacheResolutionEnabled, CommonTransportElement.UNINITIALIZED_TIMESPAN, verifyCallback, defaultReceiveCallback, serviceConfigurationManager)
		{
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x0004DDC8 File Offset: 0x0004BFC8
		internal WcfServerChannel(EndpointID[] endpoints, bool sslEnabled, bool cacheResolutionEnabled, TimeSpan receiveTimeout, RequestReply verifyCallback, OnMessageReceived defaultReceiveCallback, ServiceConfigurationManager serviceConfigurationManager)
			: base(defaultReceiveCallback, ConfigManager.SERVER_CHANNEL_OPEN_TIMEOUT, new TimeSpan(0, 0, 0, 0, ConfigManager.TIMEOUT), serviceConfigurationManager.AdvancedProperties.SecurityProperties.GetDataCacheSecurity())
		{
			this._endpoints = endpoints;
			this._serviceConfigurationManager = serviceConfigurationManager;
			if (receiveTimeout != CommonTransportElement.UNINITIALIZED_TIMESPAN)
			{
				this._receiveTimeout = receiveTimeout;
			}
			else if (this._serviceConfigurationManager.AdvancedProperties.TransportProperties.ReceiveTimeout != -1)
			{
				this._receiveTimeout = TimeSpan.FromMilliseconds((double)this._serviceConfigurationManager.AdvancedProperties.TransportProperties.ReceiveTimeout);
			}
			this._listener = new AsyncCallback(this.OnWaitForChannelComplete);
			this._acceptor = new AsyncCallback(this.OnAccept);
			this._opener = new AsyncCallback(this.OnOpen);
			this._listeners = new IChannelListener<IDuplexSessionChannel>[endpoints.Length];
			this._channels = new Hashtable();
			this._sslEnabled = sslEnabled;
			this._cacheResolutionEnabled = cacheResolutionEnabled;
			this._requestReplyCallbackTable = new Hashtable();
			if (verifyCallback != null)
			{
				this.InternalRegisterRequestReplyCallback("http://schemas.microsoft.com/velocity/msgs/VerificationAction", verifyCallback);
			}
			StringBuilder stringBuilder = new StringBuilder("DistributedCache.ServerChannel.");
			if (this._sslEnabled)
			{
				stringBuilder.Append("SSLChannel");
			}
			foreach (EndpointID endpointID in this._endpoints)
			{
				stringBuilder.Append(endpointID.ToString());
			}
			this._logSource = stringBuilder.ToString();
			this._listenerParameters = new BindingParameterCollection();
			base.SetupBindings();
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Cache resolution : {0}, SSL :{1}", new object[] { this._cacheResolutionEnabled, this._sslEnabled });
			}
			for (int j = 0; j < endpoints.Length; j++)
			{
				if (endpoints[j].IsTcp())
				{
					this._listeners[j] = this._tcpBinding.BuildChannelListener<IDuplexSessionChannel>(endpoints[j].URI, this._listenerParameters);
				}
				this._listeners[j].Open();
				this._listeners[j].BeginWaitForChannel(TimeSpan.MaxValue, this._listener, this._listeners[j]);
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Set receiveTimeout to {0}.", new object[] { this._receiveTimeout });
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose(this._logSource, "Server channel opened.");
			}
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0004E040 File Offset: 0x0004C240
		protected override TcpTransportBindingElement PrepareTcpTransportBindingElement()
		{
			long maxBufferPoolSize = this._serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxBufferPoolSize;
			int maxBufferSize = this._serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxBufferSize;
			TcpTransportBindingElement tcpTransportBindingElement = new TcpTransportBindingElement();
			if (maxBufferPoolSize != -1L)
			{
				tcpTransportBindingElement.MaxBufferPoolSize = maxBufferPoolSize;
			}
			else
			{
				tcpTransportBindingElement.MaxBufferPoolSize = 268435456L;
			}
			if (maxBufferSize != -1)
			{
				tcpTransportBindingElement.MaxBufferSize = maxBufferSize;
				tcpTransportBindingElement.MaxReceivedMessageSize = (long)maxBufferSize;
			}
			else
			{
				tcpTransportBindingElement.MaxReceivedMessageSize = 8388608L;
			}
			if (this._serviceConfigurationManager.AdvancedProperties.TransportProperties.ConnectionBufferSize != -1)
			{
				tcpTransportBindingElement.ConnectionBufferSize = this._serviceConfigurationManager.AdvancedProperties.TransportProperties.ConnectionBufferSize;
			}
			if (this._serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxPendingAccepts != -1)
			{
				tcpTransportBindingElement.MaxPendingAccepts = this._serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxPendingAccepts;
			}
			else
			{
				tcpTransportBindingElement.MaxPendingAccepts = 5;
			}
			if (this._serviceConfigurationManager.AdvancedProperties.TransportProperties.ListenBacklog != -1)
			{
				tcpTransportBindingElement.ListenBacklog = this._serviceConfigurationManager.AdvancedProperties.TransportProperties.ListenBacklog;
			}
			else
			{
				tcpTransportBindingElement.ListenBacklog = 100;
			}
			if (this._serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxPendingConnections != -1)
			{
				tcpTransportBindingElement.MaxPendingConnections = this._serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxPendingConnections;
			}
			else
			{
				tcpTransportBindingElement.MaxPendingConnections = 10;
			}
			if (this._serviceConfigurationManager.AdvancedProperties.TransportProperties.ChannelInitializationTimeout != -1)
			{
				tcpTransportBindingElement.ChannelInitializationTimeout = new TimeSpan(0, 0, 0, 0, this._serviceConfigurationManager.AdvancedProperties.TransportProperties.ChannelInitializationTimeout);
			}
			if (this._serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxOutputDelay != -1)
			{
				tcpTransportBindingElement.MaxOutputDelay = new TimeSpan(0, 0, 0, 0, this._serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxOutputDelay);
			}
			return tcpTransportBindingElement;
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0004E21C File Offset: 0x0004C41C
		protected override void AddSecurityBinding(BindingElementCollection bindingCollection)
		{
			if (base.DataCacheSecurity.SecurityMode == DataCacheSecurityMode.Transport)
			{
				this._securityBindingElement = new VelocityStreamSecurityBindingElement(this._serviceConfigurationManager, true);
				bindingCollection.Add(this._securityBindingElement);
				return;
			}
			if (this._sslEnabled)
			{
				bindingCollection.Add(new SslStreamSecurityBindingElement
				{
					RequireClientCertificate = false
				});
				string sslCertIdentity = this._serviceConfigurationManager.AdvancedProperties.SecurityProperties.SslProperties.SslCertIdentity;
				int certificateCount = WcfServerChannel.GetCertificateCount(sslCertIdentity);
				if (certificateCount == 0 && Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(this._logSource, "Certificate with name {0} doesn't exist", new object[] { sslCertIdentity });
				}
				if (certificateCount > 1 && Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(this._logSource, "More than one certificate with the name {0} exist.", new object[] { sslCertIdentity });
				}
				ServiceCredentials serviceCredentials = new ServiceCredentials();
				serviceCredentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectDistinguishedName, sslCertIdentity);
				this._listenerParameters.Add(serviceCredentials);
			}
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0004E30D File Offset: 0x0004C50D
		protected override void AddCustomBindingElements(BindingElementCollection bindingCollection)
		{
			if (this._cacheResolutionEnabled)
			{
				bindingCollection.Insert(0, new CacheResolverBindingElement(base.DataCacheSecurity));
			}
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0004E32C File Offset: 0x0004C52C
		private static int GetCertificateCount(string key)
		{
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.ReadOnly);
			int count;
			try
			{
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, key, false);
				count = x509Certificate2Collection.Count;
			}
			finally
			{
				x509Store.Close();
			}
			return count;
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x0004E378 File Offset: 0x0004C578
		private bool InvokeRequestReplyCallback(Message message, IChannelContainer container)
		{
			RequestReply requestReply = this._requestReplyCallbackTable[message.Headers.Action] as RequestReply;
			if (requestReply != null)
			{
				UniqueId messageId = message.Headers.MessageId;
				using (Message message2 = requestReply(message, container))
				{
					if (message2 != null)
					{
						message2.Headers.Action = "http://schemas.microsoft.com/velocity/msgs/RequestReplyAction";
						message2.Headers.MessageId = messageId;
						this.ReplyMessage(message2, container);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0004E400 File Offset: 0x0004C600
		private void ReplyMessage(Message message, IChannelContainer container)
		{
			try
			{
				container.Channel.Send(message, base.SendTimeout);
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(this._logSource, "Message sent synchronously {0}", new object[] { container.Channel.GetHashCode() });
				}
			}
			catch (Exception ex)
			{
				if (!ReplyContext.TryHandleFailure(container.Channel, ex))
				{
					throw;
				}
			}
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x0004E478 File Offset: 0x0004C678
		private void SetChannelAuthorization(InnerChannelContainer container, Message message)
		{
			WindowsPrincipal windowsPrincipal = new WindowsPrincipal(message.Properties.Security.ServiceSecurityContext.WindowsIdentity);
			container.Authorization = this._securityBindingElement.GetRemoteAuthorization(windowsPrincipal);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0004E4B4 File Offset: 0x0004C6B4
		protected override void CompleteProcessing(IAsyncResult result)
		{
			IDuplexSessionChannel channel;
			for (;;)
			{
				InnerChannelContainer innerChannelContainer = result.AsyncState as InnerChannelContainer;
				channel = innerChannelContainer.Channel;
				if (this._isClosed)
				{
					break;
				}
				Message message = null;
				try
				{
					message = channel.EndReceive(result);
				}
				catch (Exception ex)
				{
					if (!base.LogAndCheckIfCommunicationException(ex, channel) && !WcfTransportChannel.IsNotSupportedException(ex))
					{
						throw;
					}
				}
				if (message == null || this._isClosed)
				{
					goto IL_0055;
				}
				if (!this.ValidateConnection(innerChannelContainer))
				{
					return;
				}
				if (this._securityBindingElement != null && innerChannelContainer.IsUnauthorized)
				{
					this.SetChannelAuthorization(innerChannelContainer, message);
				}
				this.InvokeCallback(message, innerChannelContainer);
				try
				{
					result = channel.BeginReceive(this._receiveTimeout, this._receiver, innerChannelContainer);
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose<int>(this._logSource, "Receive posted on server channel {0}", channel.GetHashCode());
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
					this.CleanupChannel(channel);
					return;
				}
				if (this._isClosed)
				{
					goto Block_7;
				}
			}
			this.CleanupChannel(channel);
			return;
			IL_0055:
			this.CleanupChannel(channel);
			return;
			Block_7:
			this.CleanupChannel(channel);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0004E5D0 File Offset: 0x0004C7D0
		protected override bool InvokeCallback(Message message, IChannelContainer container)
		{
			if (message.Headers.Action != null)
			{
				if (!base.InvokeCallback(message, container) && !this.InvokeRequestReplyCallback(message, container) && !base.InvokeDefaultCallback(message, container))
				{
					return false;
				}
			}
			else if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "message with action null received on channel {0}", new object[] { container.Channel.GetHashCode() });
			}
			return true;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0004E640 File Offset: 0x0004C840
		private new void CleanupChannel(IDuplexSessionChannel channel)
		{
			lock (this._channels)
			{
				if (this._channels.Contains(channel))
				{
					this.FireConnectionDestroyedEvents(channel);
					this._channels.Remove(channel);
					base.CleanupChannel(channel);
				}
			}
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0004E6A4 File Offset: 0x0004C8A4
		private void OnWaitForChannelComplete(IAsyncResult result)
		{
			IChannelListener<IDuplexSessionChannel> channelListener = result.AsyncState as IChannelListener<IDuplexSessionChannel>;
			if (this._isClosed)
			{
				return;
			}
			if (!channelListener.EndWaitForChannel(result))
			{
				channelListener.BeginWaitForChannel(TimeSpan.MaxValue, this._listener, channelListener);
				return;
			}
			try
			{
				channelListener.BeginAcceptChannel(this._acceptor, channelListener);
			}
			catch (Exception ex)
			{
				if (!WcfTransportChannel.StaticLogAndCheckIfCommunicationException(ex, this._logSource, TraceLevel.Warning, 0))
				{
					throw;
				}
				channelListener.BeginWaitForChannel(TimeSpan.MaxValue, this._listener, channelListener);
			}
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0004E730 File Offset: 0x0004C930
		private void OnAccept(IAsyncResult ar)
		{
			IChannelListener<IDuplexSessionChannel> channelListener = ar.AsyncState as IChannelListener<IDuplexSessionChannel>;
			IDuplexSessionChannel duplexSessionChannel = null;
			try
			{
				duplexSessionChannel = channelListener.EndAcceptChannel(ar);
			}
			catch (Exception ex)
			{
				if (!base.LogAndCheckIfCommunicationException(ex, duplexSessionChannel))
				{
					throw;
				}
			}
			channelListener.BeginWaitForChannel(TimeSpan.MaxValue, this._listener, channelListener);
			if (duplexSessionChannel != null)
			{
				try
				{
					duplexSessionChannel.BeginOpen(this._channelOpenTimeout, this._opener, duplexSessionChannel);
				}
				catch (SecurityAccessDeniedException ex2)
				{
					duplexSessionChannel.Abort();
					Utility.LogException(ex2, this._logSource, TraceLevel.Error, duplexSessionChannel.GetHashCode());
				}
				catch (Exception ex3)
				{
					duplexSessionChannel.Abort();
					if (!base.LogAndCheckIfCommunicationException(ex3, duplexSessionChannel))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0004E7EC File Offset: 0x0004C9EC
		private void OnOpen(IAsyncResult result)
		{
			IDuplexSessionChannel duplexSessionChannel = result.AsyncState as IDuplexSessionChannel;
			try
			{
				duplexSessionChannel.EndOpen(result);
			}
			catch (SecurityAccessDeniedException ex)
			{
				duplexSessionChannel.Abort();
				Utility.LogException(ex, this._logSource, TraceLevel.Error, duplexSessionChannel.GetHashCode());
				return;
			}
			catch (Exception ex2)
			{
				duplexSessionChannel.Abort();
				if (!base.LogAndCheckIfCommunicationException(ex2, duplexSessionChannel))
				{
					throw;
				}
				return;
			}
			lock (this._channels)
			{
				this._channels.Add(duplexSessionChannel, DateTime.UtcNow.Ticks);
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<int>(this._logSource, "ChannelID = {0} Connection opened.", duplexSessionChannel.GetHashCode());
			}
			this.FireConnectionCreatedEvents(duplexSessionChannel);
			InnerChannelContainer innerChannelContainer = new InnerChannelContainer(duplexSessionChannel);
			if (this.ValidateConnection(innerChannelContainer))
			{
				try
				{
					IAsyncResult asyncResult = duplexSessionChannel.BeginReceive(this._receiveTimeout, this._receiver, innerChannelContainer);
					if (asyncResult.CompletedSynchronously)
					{
						ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
						{
							IAsyncResult asyncResult2 = state as IAsyncResult;
							this.CompleteProcessing(asyncResult2);
						}, asyncResult);
					}
					this.ThrottleConnections();
				}
				catch (Exception ex3)
				{
					if (!base.LogAndCheckIfCommunicationException(ex3, duplexSessionChannel))
					{
						throw;
					}
					this.CleanupChannel(duplexSessionChannel);
				}
				return;
			}
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0004E94C File Offset: 0x0004CB4C
		private void InternalRegisterRequestReplyCallback(string action, RequestReply callback)
		{
			if (string.IsNullOrEmpty(action))
			{
				throw new ArgumentNullException("action");
			}
			if (string.Equals(action, "http://schemas.microsoft.com/velocity/msgs/RequestReplyAction", StringComparison.Ordinal))
			{
				throw new InvalidOperationException();
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (this._receiveCallbackTable.ContainsKey(action))
			{
				throw new InvalidOperationException("Same action string cannot be registered for both Receive and RequestReply callbacks.");
			}
			lock (this._requestReplyCallbackTable.SyncRoot)
			{
				RequestReply requestReply = (RequestReply)this._requestReplyCallbackTable[action];
				requestReply = (RequestReply)Delegate.Combine(requestReply, callback);
				this._requestReplyCallbackTable[action] = requestReply;
			}
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0004EA08 File Offset: 0x0004CC08
		private void ThrottleConnections()
		{
			if (this._throttled)
			{
				lock (this._channels)
				{
					if (this._throttled && this._channels.Count > this._serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxConnectionsHigh)
					{
						int maxConnectionsLow = this._serviceConfigurationManager.AdvancedProperties.TransportProperties.MaxConnectionsLow;
						int num = this._channels.Count - maxConnectionsLow;
						IEnumerable<IDuplexSessionChannel> channelsToCleanup = Utility.GetChannelsToCleanup<IDuplexSessionChannel>(this._channels, maxConnectionsLow);
						foreach (IDuplexSessionChannel duplexSessionChannel in channelsToCleanup)
						{
							this.CleanupChannel(duplexSessionChannel);
						}
						this._throttledConnectionsCount += num;
					}
				}
			}
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0004EB00 File Offset: 0x0004CD00
		public void StartThrottle()
		{
			this._throttled = true;
			this.ThrottleConnections();
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0004EB11 File Offset: 0x0004CD11
		public void StopThrottle()
		{
			this._throttled = false;
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0004EB1C File Offset: 0x0004CD1C
		private void Close()
		{
			this._isClosed = true;
			if (this._connectionVerificationTimer != null)
			{
				this._connectionVerificationTimer.Dispose();
			}
			lock (this._listeners.SyncRoot)
			{
				for (int i = 0; i < this._listeners.Length; i++)
				{
					this._listeners[i].Abort();
				}
				lock (this._channels)
				{
					while (this._channels.Count > 0)
					{
						IEnumerator enumerator = this._channels.Keys.GetEnumerator();
						if (enumerator.MoveNext())
						{
							this.CleanupChannel((IDuplexSessionChannel)enumerator.Current);
						}
					}
				}
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose(this._logSource, "Server channel closed.");
			}
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0004EC18 File Offset: 0x0004CE18
		public override void RegisterReceiveCallback(string action, OnMessageReceived callback)
		{
			if (action != null && this._requestReplyCallbackTable.ContainsKey(action))
			{
				throw new InvalidOperationException("Same action string cannot be registered for both Receive and RequestReply callbacks.");
			}
			base.RegisterReceiveCallback(action, callback);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0004EC3E File Offset: 0x0004CE3E
		public void RegisterRequestReplyCallback(string action, RequestReply callback)
		{
			if (string.Equals(action, "http://schemas.microsoft.com/velocity/msgs/VerificationAction", StringComparison.Ordinal))
			{
				throw new InvalidOperationException();
			}
			this.InternalRegisterRequestReplyCallback(action, callback);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0004EC5C File Offset: 0x0004CE5C
		public void UnregisterRequestReplyCallback(string action, RequestReply callback)
		{
			if (string.IsNullOrEmpty(action))
			{
				throw new ArgumentNullException("action");
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			lock (this._requestReplyCallbackTable.SyncRoot)
			{
				RequestReply requestReply = (RequestReply)this._requestReplyCallbackTable[action];
				if (requestReply != null)
				{
					requestReply = (RequestReply)Delegate.Remove(requestReply, callback);
					if (requestReply == null)
					{
						this._requestReplyCallbackTable.Remove(action);
					}
					else
					{
						this._requestReplyCallbackTable[action] = requestReply;
					}
				}
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0004ECFC File Offset: 0x0004CEFC
		public void UnregisterRequestReplyCallback(string action)
		{
			if (string.IsNullOrEmpty(action))
			{
				throw new ArgumentNullException("action");
			}
			lock (this._requestReplyCallbackTable.SyncRoot)
			{
				this._requestReplyCallbackTable.Remove(action);
			}
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0004ED5C File Offset: 0x0004CF5C
		private void FireConnectionCreatedEvents(IDuplexSessionChannel channel)
		{
			if (this.ConnectionCreatedEvent != null)
			{
				CacheConnectionProperty property = channel.GetProperty<CacheConnectionProperty>();
				this.ConnectionCreatedEvent(property);
			}
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x0004ED84 File Offset: 0x0004CF84
		private void FireConnectionDestroyedEvents(IDuplexSessionChannel channel)
		{
			if (this.ConnectionDestroyedEvent != null)
			{
				CacheConnectionProperty property = channel.GetProperty<CacheConnectionProperty>();
				this.ConnectionDestroyedEvent(property);
			}
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0004EDAC File Offset: 0x0004CFAC
		private void FireConnectionVerificationEvents(IEnumerable<IDuplexSessionChannel> channels)
		{
			if (this.ConnectionVerificationEvent != null)
			{
				this.ConnectionVerificationEvent(channels);
			}
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0004EDC4 File Offset: 0x0004CFC4
		private bool ValidateConnection(IChannelContainer channelContainer)
		{
			IDuplexSessionChannel channel = channelContainer.Channel;
			CacheConnectionProperty property = channel.GetProperty<CacheConnectionProperty>();
			if (property != null && property.TerminateCacheConnection)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(this._logSource, "Terminating connection for : {0}. Reason: {1}", new object[] { property.CacheName, property.TerminationReason });
				}
				this.NotifyClientAndCleanupAsync(channelContainer, property.TerminationReason);
				return false;
			}
			return true;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0004EE34 File Offset: 0x0004D034
		private void NotifyClientAndCleanupAsync(IChannelContainer channelContainer, ErrStatus errorStatus)
		{
			ResponseBody responseBody = new ResponseBody(AckNack.Nack);
			responseBody.ResponseCode = errorStatus;
			IDuplexSessionChannel channel = channelContainer.Channel;
			if (errorStatus == ErrStatus.QUOTA_EXCEEDED)
			{
				responseBody.ValObject = UsageResourceType.Connections.ToString();
			}
			Message message = responseBody.CreateClientMessage(channelContainer.RemoteVersionInfo, "http://schemas.microsoft.com/velocity/msgs/ErrorNotificationAction");
			try
			{
				channel.BeginSend(message, this._channelAbortionNotificationTimeout, new AsyncCallback(this.NotifyClientAndCleanupAsyncCallback), channel);
			}
			catch (Exception ex)
			{
				Utility.LogMessage(ex.ToString(), this._logSource, TraceLevel.Info);
				if (!WcfTransportChannel.IsCommunicationException(ex) && !(ex is SecurityAccessDeniedException) && !(ex is TimeoutException))
				{
					throw;
				}
				this.CleanupChannel(channel);
			}
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0004EEE8 File Offset: 0x0004D0E8
		private void NotifyClientAndCleanupAsyncCallback(IAsyncResult result)
		{
			IDuplexSessionChannel duplexSessionChannel = result.AsyncState as IDuplexSessionChannel;
			try
			{
				duplexSessionChannel.EndSend(result);
			}
			catch (Exception ex)
			{
				Utility.LogMessage(ex.ToString(), this._logSource, TraceLevel.Info);
				if (!WcfTransportChannel.IsCommunicationException(ex) && !(ex is SecurityAccessDeniedException) && !(ex is TimeoutException))
				{
					throw;
				}
			}
			this.CleanupChannel(duplexSessionChannel);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0004EF50 File Offset: 0x0004D150
		private void ConnectionVerificationCallback(object obj)
		{
			TimeSpan timeSpan = (TimeSpan)obj;
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Inititating connection verification at : {0}", new object[] { DateTime.UtcNow });
			}
			ICollection<IDuplexSessionChannel> collection;
			lock (this._channels)
			{
				collection = Utility.CreateList<IDuplexSessionChannel>(this._channels, (DictionaryEntry entry) => (IDuplexSessionChannel)entry.Key);
			}
			this.FireConnectionVerificationEvents(collection);
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Ending connection verification at : {0}", new object[] { DateTime.UtcNow });
			}
			if (this._connectionVerificationTimer != null)
			{
				this._connectionVerificationTimer.Change((int)timeSpan.TotalMilliseconds, -1);
			}
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0004F040 File Offset: 0x0004D240
		public void EnablePeriodicConnectionVerification()
		{
			TimeSpan timeSpan = default(TimeSpan);
			if (this._serviceConfigurationManager.AdvancedProperties.UsageProperties.UsageEnabled)
			{
				timeSpan = this._serviceConfigurationManager.AdvancedProperties.UsageProperties.GetResourceSyncInterval("quota", UsageResourceType.Connections);
			}
			if (timeSpan != default(TimeSpan) && timeSpan != TimeSpan.MaxValue)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<double>(this._logSource, "Running connection verification every {0} secs ", timeSpan.TotalSeconds / 2.0);
				}
				this._connectionVerificationTimer = new global::System.Threading.Timer(new TimerCallback(this.ConnectionVerificationCallback), timeSpan, (int)(timeSpan.TotalMilliseconds / 2.0), -1);
			}
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0004F103 File Offset: 0x0004D303
		public long GetTotalConnectionsCount()
		{
			return (long)this._channels.Count;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0004F111 File Offset: 0x0004D311
		public long GetThrottledConnectionsCount()
		{
			return (long)this._throttledConnectionsCount;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void StartListening()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0004F11A File Offset: 0x0004D31A
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x04000DE9 RID: 3561
		private const int _lohDataItemSize = 65536;

		// Token: 0x04000DEA RID: 3562
		private IChannelListener<IDuplexSessionChannel>[] _listeners;

		// Token: 0x04000DEB RID: 3563
		private EndpointID[] _endpoints;

		// Token: 0x04000DEC RID: 3564
		private AsyncCallback _listener;

		// Token: 0x04000DED RID: 3565
		private AsyncCallback _acceptor;

		// Token: 0x04000DEE RID: 3566
		private AsyncCallback _opener;

		// Token: 0x04000DEF RID: 3567
		private Hashtable _channels;

		// Token: 0x04000DF0 RID: 3568
		private volatile bool _throttled;

		// Token: 0x04000DF1 RID: 3569
		private BindingParameterCollection _listenerParameters;

		// Token: 0x04000DF2 RID: 3570
		private Hashtable _requestReplyCallbackTable;

		// Token: 0x04000DF3 RID: 3571
		private bool _sslEnabled;

		// Token: 0x04000DF4 RID: 3572
		private bool _cacheResolutionEnabled;

		// Token: 0x04000DF5 RID: 3573
		private ServiceConfigurationManager _serviceConfigurationManager;

		// Token: 0x04000DF6 RID: 3574
		private VelocityStreamSecurityBindingElement _securityBindingElement;

		// Token: 0x04000DF7 RID: 3575
		private global::System.Threading.Timer _connectionVerificationTimer;

		// Token: 0x04000DF8 RID: 3576
		private TimeSpan _channelAbortionNotificationTimeout = new TimeSpan(0, 0, 2);

		// Token: 0x04000DFC RID: 3580
		private int _throttledConnectionsCount;
	}
}
