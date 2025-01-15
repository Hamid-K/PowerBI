using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000456 RID: 1110
	internal class TcpTransportFactory : ISessionTransportFactory, IDatagramTransportFactory
	{
		// Token: 0x060026C2 RID: 9922 RVA: 0x00076B95 File Offset: 0x00074D95
		public TcpTransportFactory()
			: this(null, int.MaxValue, 0, 0, TimeSpan.Zero)
		{
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x00076BAC File Offset: 0x00074DAC
		public TcpTransportFactory(ISecurityProvider securityProvider, int maxMessageSize, int maxBufferPoolSize, int ConnectionBufferSize, TimeSpan maxOutputDelay)
		{
			TcpTransportBindingElement tcpTransportBindingElement = new TcpTransportBindingElement();
			tcpTransportBindingElement.ManualAddressing = true;
			tcpTransportBindingElement.MaxOutputDelay = TimeSpan.Zero;
			tcpTransportBindingElement.ConnectionPoolSettings.MaxOutboundConnectionsPerEndpoint = 0;
			tcpTransportBindingElement.MaxBufferSize = maxMessageSize;
			tcpTransportBindingElement.MaxReceivedMessageSize = (long)maxMessageSize;
			tcpTransportBindingElement.MaxPendingAccepts = 200;
			tcpTransportBindingElement.ListenBacklog = 200;
			tcpTransportBindingElement.MaxOutputDelay = maxOutputDelay;
			if (ConnectionBufferSize > 0)
			{
				tcpTransportBindingElement.ConnectionBufferSize = ConnectionBufferSize;
			}
			if (maxBufferPoolSize > 0)
			{
				tcpTransportBindingElement.MaxBufferPoolSize = (long)maxBufferPoolSize;
			}
			TransportBindingElement transportBindingElement = new UnreliableTransportBindingElement(tcpTransportBindingElement);
			BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = new BinaryMessageEncodingBindingElement();
			binaryMessageEncodingBindingElement.ReaderQuotas.MaxArrayLength = maxMessageSize;
			binaryMessageEncodingBindingElement.ReaderQuotas.MaxStringContentLength = maxMessageSize;
			binaryMessageEncodingBindingElement.ReaderQuotas.MaxNameTableCharCount = maxMessageSize;
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			bindingElementCollection.Add(binaryMessageEncodingBindingElement);
			bindingElementCollection.Add(transportBindingElement);
			if (securityProvider != null)
			{
				bindingElementCollection = securityProvider.InitializeCommunication(bindingElementCollection);
			}
			this.m_binding = new CustomBinding(bindingElementCollection);
			this.m_binding.ReceiveTimeout = TimeSpan.MaxValue;
			this.m_channelFactory = this.m_binding.BuildChannelFactory<IDuplexSessionChannel>(new object[0]);
			this.m_channelFactory.Open();
			this.m_datagramConnections = new Dictionary<Uri, WeakReference>();
			this.m_maxConnectionRetry = 0;
			this.m_connectionRetryInterval = TimeSpan.FromSeconds(5.0);
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x00076CE8 File Offset: 0x00074EE8
		// (set) Token: 0x060026C5 RID: 9925 RVA: 0x00076CF0 File Offset: 0x00074EF0
		internal int MaxConnectionRetry
		{
			get
			{
				return this.m_maxConnectionRetry;
			}
			set
			{
				this.m_maxConnectionRetry = value;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x00076CF9 File Offset: 0x00074EF9
		// (set) Token: 0x060026C7 RID: 9927 RVA: 0x00076D01 File Offset: 0x00074F01
		internal TimeSpan ConnectionRetryInterval
		{
			get
			{
				return this.m_connectionRetryInterval;
			}
			set
			{
				this.m_connectionRetryInterval = value;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060026C8 RID: 9928 RVA: 0x00076D0A File Offset: 0x00074F0A
		internal string Scheme
		{
			get
			{
				return this.m_binding.Scheme;
			}
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x00076D18 File Offset: 0x00074F18
		internal IDuplexSessionChannel CreateChannel(Uri remoteAddress)
		{
			EndpointAddress endpointAddress = new EndpointAddress(remoteAddress, new AddressHeader[0]);
			return this.m_channelFactory.CreateChannel(endpointAddress, remoteAddress);
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x00076D3F File Offset: 0x00074F3F
		public IOutputSession CreateOutputSession(Uri remoteAddress)
		{
			return new TcpOutputSession(remoteAddress, this);
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x00076D48 File Offset: 0x00074F48
		public DatagramConnection CreateDatagramConnection(Uri remoteAddress)
		{
			DatagramConnection datagramConnection;
			lock (this.m_datagramConnections)
			{
				WeakReference weakReference;
				if (this.m_datagramConnections.TryGetValue(remoteAddress, out weakReference))
				{
					datagramConnection = (DatagramConnection)weakReference.Target;
					if (datagramConnection != null)
					{
						datagramConnection.AddReference();
						return datagramConnection;
					}
				}
				else
				{
					weakReference = new WeakReference(null);
					this.m_datagramConnections.Add(remoteAddress, weakReference);
				}
				datagramConnection = new DatagramConnection(remoteAddress, this);
				datagramConnection.AddReference();
				weakReference.Target = datagramConnection;
			}
			return datagramConnection;
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x00076DD4 File Offset: 0x00074FD4
		public bool ReleaseDatagramConnection(DatagramConnection connection, bool forceRemove)
		{
			lock (this.m_datagramConnections)
			{
				if (!forceRemove && connection.References > 0)
				{
					return false;
				}
				bool flag = this.m_datagramConnections.Remove(connection.RemoteAddress);
				ReleaseAssert.IsTrue(flag);
			}
			return true;
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x00076E34 File Offset: 0x00075034
		public ISessionListener CreateSessionListener(Uri listenerAddress)
		{
			ListenUriMode listenUriMode = ((listenerAddress.Port <= 0) ? ListenUriMode.Unique : ListenUriMode.Explicit);
			IChannelListener<IDuplexSessionChannel> channelListener = this.m_binding.BuildChannelListener<IDuplexSessionChannel>(listenerAddress, string.Empty, listenUriMode, new object[0]);
			return new TcpListener(channelListener);
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x00076E70 File Offset: 0x00075070
		public IMessageListener CreateMessageListener(Uri listenerAddress)
		{
			ListenUriMode listenUriMode = ((listenerAddress.Port <= 0) ? ListenUriMode.Unique : ListenUriMode.Explicit);
			IChannelListener<IDuplexSessionChannel> channelListener = this.m_binding.BuildChannelListener<IDuplexSessionChannel>(listenerAddress, string.Empty, listenUriMode, new object[0]);
			return new MessageListener(channelListener);
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x00076EAA File Offset: 0x000750AA
		public bool IsRemoteDownException(Exception e)
		{
			return Utility.IsException<RemoteDownException>(e);
		}

		// Token: 0x04001701 RID: 5889
		private CustomBinding m_binding;

		// Token: 0x04001702 RID: 5890
		private int m_maxConnectionRetry;

		// Token: 0x04001703 RID: 5891
		private TimeSpan m_connectionRetryInterval;

		// Token: 0x04001704 RID: 5892
		private IChannelFactory<IDuplexSessionChannel> m_channelFactory;

		// Token: 0x04001705 RID: 5893
		private Dictionary<Uri, WeakReference> m_datagramConnections = new Dictionary<Uri, WeakReference>();
	}
}
